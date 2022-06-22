using MatrixModule;

namespace OptimizedGeneticAlgorithm.GeneticAlgorithm.Mutation
{
    public static class ExchangeMutation
    {
        public static Func<List<DependentMatrix>, List<DependentMatrix>, double, List<DependentMatrix>> Mutator = (individuals, list, mutationPercent) =>
        {
            var mutated = new List<DependentMatrix>();
            var random = new Random();
            var mutationCounter = 0;
            foreach (var individ in individuals)
            {
                var willMutate = random.NextDouble();
                if (willMutate <= mutationPercent)
                {
                    // get indexex of chromosome to exchange
                    var firstChromosomeIndex = random.Next(0, individ.Count - 1);
                    var secondChromosomeIndex = random.Next(0, individ.Count - 1);

                    // eliminate repeat
                    while (secondChromosomeIndex == firstChromosomeIndex)
                    {
                        secondChromosomeIndex = random.Next(0, individ.Count - 1);
                    }

                    //exchange chromosomes
                    individ.SwapColumns(individ, new List<(int, int)> { (firstChromosomeIndex, secondChromosomeIndex) });
                    mutationCounter++;
                }
                mutated.Add(individ);
            }
            return mutated;
        };
    }
}
