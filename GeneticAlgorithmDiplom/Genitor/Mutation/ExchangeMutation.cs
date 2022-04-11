namespace GeneticAlgorithmDiplom.Genitor.Mutation
{
    public static class ExchangeMutation
    {
        public static Func<List<Individual>, double, List<Individual>> Mutator = (individuals, mutationPercent) =>
        {
            var mutated = new List<Individual>();
            var random = new Random();
            var mutationCounter = 0;
            foreach (var individ in individuals)
            {
                var willMutate = random.NextDouble();
                if (willMutate > mutationPercent)
                {
                    // get indexex of chromosome to exchange
                    var firstChromosomeIndex = random.Next(0, individ.matrix.Length - 1);
                    var secondChromosomeIndex = random.Next(0, individ.matrix.Length - 1);

                    // eliminate repeat
                    while (secondChromosomeIndex == firstChromosomeIndex)
                    {
                        secondChromosomeIndex = random.Next(0, individ.matrix.Length - 1);
                    }

                    //exchange chromosomes
                    var individMatrix = individ.matrix;
                    MatrixOperations.SwapColls(ref individMatrix, firstChromosomeIndex, secondChromosomeIndex);
                    individ.matrix = individMatrix;
                    mutationCounter++;
                }
                mutated.Add(individ);
            }
            return mutated;
        };
    }
}
