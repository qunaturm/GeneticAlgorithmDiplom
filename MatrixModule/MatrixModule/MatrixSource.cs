using System.Collections;

namespace MatrixModule;

public class MatrixSource : IEnumerable<IReadOnlyList<double>>
{
    private readonly double[][] _source;
    private readonly int _squadMatrixSize;

    public MatrixSource(IEnumerable<IEnumerable<double>> initData, int? squadMatrixSize = null)
    {
        var data = initData as IEnumerable<double>[] ?? initData.ToArray();
        _squadMatrixSize = squadMatrixSize ?? data.First().Count();
        _source = data.Select(u => u.ToArray()).ToArray();
    }

    public IReadOnlyList<IReadOnlyList<double>> Source => _source;

    public IReadOnlyList<double> this[int key] => Source[key];

    public IEnumerator<IReadOnlyList<double>> GetEnumerator()
    {
        return Source.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public DependentMatrix GenerateDependentMatrix()
    {
        var indexes = GetRandomIndexes(_squadMatrixSize, _source.Length);

        return new DependentMatrix(this, indexes, _squadMatrixSize);
    }
    
    public List<int> GetRandomIndexes(int countOfElements, int maxValue)
    {
        var random = new Random();
        List<int> numbers;
        if (maxValue < countOfElements) throw new ArgumentException("Count of elements can't be great than max value");
        if (maxValue / 10 <= countOfElements)
        {
            var possibleIndexes = Enumerable.Range(0, maxValue).ToList();
            List<int> tmpNumbers = new List<int>();
            for (int i = 0; i < countOfElements; i++)
            {
                int index = random.Next(0, possibleIndexes.Count);
                tmpNumbers.Add(possibleIndexes[index]);
                possibleIndexes.RemoveAt(index);
            }

            numbers = tmpNumbers.ToList();
        }
        else
        {
            HashSet<int> tmpNumbers = new HashSet<int>();
            while (tmpNumbers.Count < countOfElements)
            {
                tmpNumbers.Add(random.Next(0, maxValue));
            }
            
            numbers = tmpNumbers.ToList();
        }

        return numbers;
    }
}