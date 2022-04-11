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
                parents.Remove(firstParent);
                var secondParent = parents[random.Next(0, parents.Count)];
                parents.Remove(secondParent);

                //get indexes of chromosome for children
                var firstHalf = random.Next(1, firstParent.matrix.Length - 2);
                var secondHalf = 0;
                secondHalf = firstHalf < firstParent.matrix.Length ? secondHalf = random.Next(firstHalf + 1, firstParent.matrix.Length - 2) : secondHalf = random.Next(1, firstHalf);

                var listTwoChildren = MatrixOperations.CopyColumn(firstParent.matrix, secondParent.matrix, firstHalf, secondHalf);

                children.Add(listTwoChildren[0]);
                children.Add(listTwoChildren[1]);
            }
            return children;
        };
    }
}
