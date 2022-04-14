namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing
{
    public static class TwoPointsCrossing
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

                //get indexes of chromosome for children
                var firstHalf = random.Next(1, firstParent.Matrix.Length - 2);
                var secondHalf = 0;
                secondHalf = firstHalf < firstParent.Matrix.Length ? secondHalf = random.Next(firstHalf + 1, firstParent.Matrix.Length - 2) : secondHalf = random.Next(1, firstHalf);

                var listTwoChildren = MatrixOperations.CopyColumn(firstParent.Matrix, secondParent.Matrix, firstHalf, secondHalf);

                parents.Add(listTwoChildren[0]);
                parents.Add(listTwoChildren[1]);
            }
            return parents;
        };
    }
}
