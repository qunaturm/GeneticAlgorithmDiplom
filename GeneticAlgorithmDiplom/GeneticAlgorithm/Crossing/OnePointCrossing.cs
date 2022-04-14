namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing
{
    public static class OnePointCrossing
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            var random = new Random();
            var parentsCopy = new List<Individual>(parents);
            while (parentsCopy.Count > 0)
            {
                // get two parent
                var firstParent = parentsCopy[random.Next(0, parentsCopy.Count)];
                parentsCopy.Remove(firstParent);
                var secondParent = parentsCopy[random.Next(0, parentsCopy.Count)];
                parentsCopy.Remove(secondParent);

                //get index of chromosome for children
                var firstHalf = random.Next(1, firstParent.Matrix.Length - 2);

                var child1Matrix = MatrixOperations.CopyColumn(firstParent.Matrix, secondParent.Matrix, firstHalf);
                var child1Det = MatrixOperations.GetDeterminant(child1Matrix);

                var child2Matrix = MatrixOperations.CopyColumn(secondParent.Matrix, firstParent.Matrix, firstHalf);
                var child2Det = MatrixOperations.GetDeterminant(child2Matrix);

                parents.Add(new Individual { Matrix = child1Matrix, Determinant = child1Det });
                parents.Add(new Individual { Matrix = child2Matrix, Determinant = child2Det });
            }
            return parents;
        };
    }
}
