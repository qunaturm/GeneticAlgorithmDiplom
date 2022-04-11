namespace GeneticAlgorithmDiplom.Genitor.Crossing
{
    public static class One_Point_Crossover
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            List<Individual> children = new List<Individual>();
            var random = new Random();
            var firstParent = parents[0];
            var secondParent = parents[1];

            //get index of chromosome for children
            var firstHalf = random.Next(1, firstParent.matrix.Length - 2);

            var child1Matrix = MatrixOperations.CopyColumn(firstParent.matrix, secondParent.matrix, firstHalf);
            var child1Det = MatrixOperations.GetDeterminant(child1Matrix);

            var child2Matrix = MatrixOperations.CopyColumn(secondParent.matrix, firstParent.matrix, firstHalf);
            var child2Det = MatrixOperations.GetDeterminant(child2Matrix);

            children.Add(new Individual { matrix = child1Matrix, determinant = child1Det });
            children.Add(new Individual { matrix = child2Matrix, determinant = child2Det });
            return children;
        };
    }
}
