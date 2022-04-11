namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing
{
    public static class OnePointCrossing
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            List<Individual> children = new List<Individual>();
            var random = new Random();
            while (parents.Count > 0)
            {
                // get two parent
                var firstParent = parents[random.Next(0, parents.Count)];
                parents.Remove(firstParent);
                var secondParent = parents[random.Next(0, parents.Count)];
                parents.Remove(secondParent);

                //get index of chromosome for children
                var firstHalf = random.Next(1, firstParent.matrix.Length - 2);

                var child1Matrix = MatrixOperations.CopyColumn(firstParent.matrix, secondParent.matrix, firstHalf);
                var child1Det = MatrixOperations.GetDeterminant(child1Matrix);

                var child2Matrix = MatrixOperations.CopyColumn(secondParent.matrix, firstParent.matrix, firstHalf);
                var child2Det = MatrixOperations.GetDeterminant(child2Matrix);

                children.Add(new Individual { matrix = child1Matrix, determinant = child1Det });
                children.Add(new Individual { matrix = child2Matrix, determinant = child2Det });
            }
            return children;
        };
    }
}
