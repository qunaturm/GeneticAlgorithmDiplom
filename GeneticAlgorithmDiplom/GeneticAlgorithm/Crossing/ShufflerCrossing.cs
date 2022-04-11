namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing
{
    public static class ShufflerCrossing
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            var children = new List<Individual>();
            var random = new Random();
            while (parents.Count > 0)
            {
                // get two parents index
                var firstParentIndex = random.Next(0, parents.Count);
                var secondParentIndex = random.Next(0, parents.Count);
                // eliminate repeat
                while (secondParentIndex == firstParentIndex)
                {
                    secondParentIndex = random.Next(0, parents.Count);
                }

                // get parents
                var firstParent = parents[firstParentIndex];
                var secondParent = parents[secondParentIndex];
                var half = random.Next(1, parents[firstParentIndex].matrix.Length - 1);
                var firstParentMatrix = firstParent.matrix;
                var secondParentMatrix = secondParent.matrix;

                // remove parents from initial sample
                parents.Remove(firstParent);
                parents.Remove(secondParent);
                // swap genom for both
                MatrixOperations.SwapColls(ref firstParentMatrix, ref secondParentMatrix, half);

                // get first child data
                var child1Matrix = MatrixOperations.CopyColumn(firstParentMatrix, secondParentMatrix, random.Next(0, firstParentMatrix.Length - 1));
                var child1Det = MatrixOperations.GetDeterminant(child1Matrix);
                children.Add(new Individual { matrix = child1Matrix, determinant = child1Det });

                // get second child data
                var child2Matrix = MatrixOperations.CopyColumn(firstParentMatrix, secondParentMatrix, random.Next(0, firstParentMatrix.Length - 1));
                var child2Det = MatrixOperations.GetDeterminant(child2Matrix);
                children.Add(new Individual { matrix = child2Matrix, determinant = child2Det });
            }
            return children;
        };
    }
}
