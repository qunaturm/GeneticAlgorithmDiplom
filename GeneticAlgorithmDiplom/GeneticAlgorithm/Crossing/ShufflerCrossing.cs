namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing
{
    public static class ShufflerCrossing
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            var random = new Random();
            var parentsCopy = new List<Individual>(parents);
            while (parentsCopy.Count > 0)
            {
                // get two parents index
                var firstParentIndex = random.Next(0, parentsCopy.Count);
                var secondParentIndex = random.Next(0, parentsCopy.Count);

                // eliminate repeat
                while (secondParentIndex == firstParentIndex)
                {
                    secondParentIndex = random.Next(0, parentsCopy.Count);
                }

                // get parents
                var firstParent = parentsCopy[firstParentIndex];
                var secondParent = parentsCopy[secondParentIndex];
                var half = random.Next(1, parentsCopy[firstParentIndex].Matrix.Length - 1);
                var firstParentMatrix = firstParent.Matrix;
                var secondParentMatrix = secondParent.Matrix;

                // swap genom for both
                MatrixOperations.SwapColls(ref firstParentMatrix, ref secondParentMatrix, half);

                // get first child data
                var child1Matrix = MatrixOperations.CopyColumn(firstParentMatrix, secondParentMatrix, random.Next(0, firstParentMatrix.Length - 1));
                var child1Det = MatrixOperations.GetDeterminant(child1Matrix);
                parents.Add(new Individual { Matrix = child1Matrix, Determinant = child1Det });

                // get second child data
                var child2Matrix = MatrixOperations.CopyColumn(firstParentMatrix, secondParentMatrix, random.Next(0, firstParentMatrix.Length - 1));
                var child2Det = MatrixOperations.GetDeterminant(child2Matrix);
                parents.Add(new Individual { Matrix = child2Matrix, Determinant = child2Det });

                // delete parents from initial sample
                parentsCopy.Remove(firstParent);
                parentsCopy.Remove(secondParent);

            }
            return parents;
        };
    }
}
