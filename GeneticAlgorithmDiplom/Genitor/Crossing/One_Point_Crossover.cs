namespace GeneticAlgorithmDiplom.Genitor.Crossing
{
    public static class One_Point_Crossover
    {
        public static Func<List<Individual>, Individual> Crossover = (parents) =>
        {
            var random = new Random();
            var firstParent = parents[0];
            var secondParent = parents[1];

            //get index of chromosome for child
            var firstHalf = random.Next(1, firstParent.Matrix.Length - 2);

            var childMatrix = MatrixOperations.CopyColumn(firstParent.Matrix, secondParent.Matrix, firstHalf);
            var child1Det = MatrixOperations.GetDeterminant(childMatrix);

            return (new Individual { Matrix = childMatrix, Determinant = child1Det });
        };
    }
}
