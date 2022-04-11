namespace GeneticAlgorithmDiplom.Genitor.Crossing
{
    public static class Two_Point_Crossover
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            List<Individual> children = new List<Individual>();
            var random = new Random();
            var firstParent = parents[0];
            var secondParent = parents[1];

            //get indexes of chromosome for children
            var firstHalf = random.Next(1, firstParent.matrix.Length - 2);
            var secondHalf = 0;
            secondHalf = firstHalf < firstParent.matrix.Length ? secondHalf = random.Next(firstHalf + 1, firstParent.matrix.Length - 2) : secondHalf = random.Next(1, firstHalf);

            var listTwoChildren = MatrixOperations.CopyColumn(firstParent.matrix, secondParent.matrix, firstHalf, secondHalf);

            children.Add(listTwoChildren[0]);
            children.Add(listTwoChildren[1]);
            return children;
        };
    }
}
