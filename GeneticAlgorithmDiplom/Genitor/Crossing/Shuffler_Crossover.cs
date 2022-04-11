namespace GeneticAlgorithmDiplom.Genitor
{
    public static class Shuffler_Crossover
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            List<Individual> children = new List<Individual>();
            var random = new Random();
            var firstParent = parents[0];
            var secondParent = parents[1];

            // get parents
            var half = random.Next(1, firstParent.matrix.Length - 1);
            var firstParentMatrix = firstParent.matrix;
            var secondParentMatrix = secondParent.matrix;

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
            return children;
        };
    }
}
