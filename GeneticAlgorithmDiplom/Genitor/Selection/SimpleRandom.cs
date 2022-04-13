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
            parents.Add(new Individual { Matrix = generation[firstParentIndex].Matrix, Determinant = generation[firstParentIndex].Determinant });
            parents.Add(new Individual { Matrix = generation[secondParentIndex].Matrix, Determinant = generation[secondParentIndex].Determinant });
            return parents;
        };
    }
}
