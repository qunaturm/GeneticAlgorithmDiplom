namespace GeneticAlgorithmDiplom.Genitor.Crossing
{
    public static class Two_Point_Crossover
    {
        public static Func<List<Individual>, Individual> Crossover = (parents) =>
        {
            var random = new Random();
            var firstParent = parents[0];
            var secondParent = parents[1];

            //get indexes of chromosome for children
            var firstHalf = random.Next(1, firstParent.Matrix.Length - 2);
            var secondHalf = 0;
            secondHalf = firstHalf < firstParent.Matrix.Length ? secondHalf = random.Next(firstHalf + 1, firstParent.Matrix.Length - 2) : secondHalf = random.Next(1, firstHalf);

            var listTwoChildren = MatrixOperations.CopyColumn(firstParent.Matrix, secondParent.Matrix, firstHalf, secondHalf);

            return listTwoChildren[0];
        };
    }
}
