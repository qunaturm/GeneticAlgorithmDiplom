namespace GeneticAlgorithmDiplom.Genitor.Selection
{
    public static class SimpleRandom
    {
        public static Func<List<Individual>, List<Individual>> Selector = (generation) =>
        {
            List<Individual> parents = new List<Individual>();
            var random = new Random();
            var firstParentIndex = random.Next(0, generation.Count - 1);
            var secondParentIndex = random.Next(0, generation.Count - 1);

            // eliminate repeat
            while (secondParentIndex == firstParentIndex)
            {
                secondParentIndex = random.Next(0, generation.Count - 1);
            }
            parents.Add(new Individual { matrix = generation[firstParentIndex].matrix, determinant = generation[firstParentIndex].determinant });
            parents.Add(new Individual { matrix = generation[secondParentIndex].matrix, determinant = generation[secondParentIndex].determinant });
            return parents;
        };
    }
}
