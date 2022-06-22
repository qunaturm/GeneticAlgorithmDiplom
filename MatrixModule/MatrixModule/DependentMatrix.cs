using System.Collections;

namespace MatrixModule;

public class DependentMatrix : IReadOnlyList<IReadOnlyList<double>>
{
    private readonly MatrixSource _matrixSource;
    private readonly int _squadMatrixSize;
    private double _determinant = double.NaN;
    private readonly int[] _sourceIndexes;
    private bool _isChanged;

    public IReadOnlyList<int> Indexes => _sourceIndexes;

    public double Determinant
    {
        get
        {
            if (!_isChanged) return _determinant;
            _determinant = GetDeterminant();
            _isChanged = false;
            return _determinant;
        }
    }

    public IReadOnlyList<double> this[int key] => _matrixSource[Indexes[key]];

    public IEnumerator<IReadOnlyList<double>> GetEnumerator()
    {
        foreach (var index in Indexes) yield return this[index];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _sourceIndexes.Length;

    internal DependentMatrix(MatrixSource matrixSource, IEnumerable<int> sourceIndexes, int squadMatrixSize)
    {
        _matrixSource = matrixSource;
        _sourceIndexes = sourceIndexes.ToArray();
        _squadMatrixSize = squadMatrixSize;
        _isChanged = true;
    }

    public DependentMatrix(IEnumerable<IEnumerable<double>> initData, int? squadMatrixSize = null)
    {
        var data = initData as IEnumerable<double>[] ?? initData.ToArray();
        _matrixSource = new MatrixSource(data);
        _sourceIndexes = data.Select((_, i) => i).ToArray();
        _squadMatrixSize = squadMatrixSize ?? data.First().Count();
        _isChanged = true;
    }

    public DependentMatrix(DependentMatrix left, DependentMatrix right, IEnumerable<int> leftIndexesRange, IEnumerable<int> rightIndexesRange)
    {
        if (left._matrixSource != right._matrixSource) throw new ArgumentException("left and right matrix should be associate with same matrix source");
        _matrixSource = left._matrixSource;
        var leftIndexes = leftIndexesRange.Select(u => left.Indexes[u]).ToList();
        leftIndexes.AddRange(rightIndexesRange.Select(u => right.Indexes[u]).ToList());
        _sourceIndexes = leftIndexes.ToArray();
        _squadMatrixSize = left._squadMatrixSize;
        _isChanged = true;
    }

    public double[][] GetCopyMatrix()
    {
        var copy = new double[_squadMatrixSize][];
        for (var i = 0; i < _squadMatrixSize; i++) copy[i] = new double[_squadMatrixSize];

        for (var i = 0; i < _squadMatrixSize; i++)
        for (var j = 0; j < _squadMatrixSize; j++)
            copy[i][j] = this[i][j];
        return copy;
    }

    public double[][] GetRowMajorCopyMatrix()
    {
        var copy = new double[_squadMatrixSize][];
        for (var i = 0; i < _squadMatrixSize; i++) copy[i] = new double[_squadMatrixSize];

        for (var i = 0; i < _squadMatrixSize; i++)
        for (var j = 0; j < _squadMatrixSize; j++)
            copy[i][j] = this[j][i];

        return copy;
    }
    
    public void SwapColumns(DependentMatrix other, IEnumerable<(int, int)> columnsIndexes) 
    {
        _isChanged = true;
        foreach (var columnsIndex in columnsIndexes)
            (_sourceIndexes[columnsIndex.Item1], other._sourceIndexes[columnsIndex.Item2])
                = (other._sourceIndexes[columnsIndex.Item2], _sourceIndexes[columnsIndex.Item1]);
    }
    
    
    private double GetDeterminant()
    {
        var lum = Decompose(out var toggle);
        if (lum == null) throw new Exception("unable compute determinant");
        double result = toggle;
        for (var i = 0; i < lum.Length; ++i)
            result *= lum[i][i];
        return result;
    }

    private double[][] Decompose(out int toggle)
    {
        var matrix = GetRowMajorCopyMatrix();
        var perm = new int[_squadMatrixSize];
        for (var i = 0; i < _squadMatrixSize; ++i) perm[i] = i;
        toggle = 1;
        for (var j = 0; j < _squadMatrixSize - 1; ++j)
        {
            var columnMax = Math.Abs(matrix[j][j]);
            var pRow = j;
            for (var i = j + 1; i < _squadMatrixSize; ++i)
                if (matrix[i][j] > columnMax)
                {
                    columnMax = matrix[i][j];
                    pRow = i;
                }

            if (pRow != j)
            {
                (matrix[pRow], matrix[j]) = (matrix[j], matrix[pRow]);
                (perm[pRow], perm[j]) = (perm[j], perm[pRow]);
                toggle = -toggle;
            }

            for (var i = j + 1; i < _squadMatrixSize; ++i)
            {
                var current = matrix[i][j] / matrix[j][j];
                if (double.IsNaN(current) || double.IsInfinity(current))
                    matrix[i][j] = 0;
                else
                    matrix[i][j] = current;
                for (var k = j + 1; k < _squadMatrixSize; ++k) matrix[i][k] -= matrix[i][j] * matrix[j][k];
            }
        }

        return matrix;
    }

}