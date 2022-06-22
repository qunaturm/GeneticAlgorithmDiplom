using MatrixModule;

namespace OptimizedGeneticAlgorithm.GeneticAlgorithm.Crossing
{
    public static class OnePointCrossing
    {
        public static Func<List<DependentMatrix>, List<DependentMatrix>> Crossover = (parents) =>
        {
            List<DependentMatrix> children = new List<DependentMatrix>();
            var random = new Random();
            while (parents.Count > 0)
            {
                // get two parent
                var firstParent = parents[random.Next(0, parents.Count)];
                var secondParent = parents[random.Next(0, parents.Count)];

                var firstHalf = random.Next(1, firstParent.Count - 2);

                var firstChild = new DependentMatrix(firstParent, secondParent, Enumerable.Range(0, firstHalf), Enumerable.Range(firstHalf, secondParent.Count - firstHalf));
                var secondChild = new DependentMatrix(secondParent, firstParent, Enumerable.Range(0, firstHalf), Enumerable.Range(firstHalf, secondParent.Count - firstHalf));

                children.Add(firstParent);
                children.Add(secondParent);
                children.Add(firstChild);
                children.Add(secondChild);
            }
            return children;
        };
    }
}
