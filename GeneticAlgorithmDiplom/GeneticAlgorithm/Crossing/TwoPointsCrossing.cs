namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing
{
    public static class TwoPointsCrossing
    {
        public static Func<List<Individual>, List<Individual>> Crossover = (parents) =>
        {
            var children = new List<Individual>();
            var random = new Random();
            while (parents.Count > 0)
            {
                // get two parent
                var firstParent = parents[random.Next(0, parents.Count)];
                var secondParent = parents[random.Next(0, parents.Count)];

                //get indexes of chromosome for children
                var firstHalf = random.Next(1, firstParent.Matrix.Length - 2);
                var secondHalf = 0;
                secondHalf = firstHalf < firstParent.Matrix.Length ? secondHalf = random.Next(firstHalf + 1, firstParent.Matrix.Length - 2) : secondHalf = random.Next(1, firstHalf);

                var listTwoChildren = MatrixOperations.CopyColumn(firstParent.Matrix, secondParent.Matrix, firstHalf, secondHalf);

                children.Add(listTwoChildren[0]);
                children.Add(listTwoChildren[1]);
            }
            return children;
        };
    }
}
