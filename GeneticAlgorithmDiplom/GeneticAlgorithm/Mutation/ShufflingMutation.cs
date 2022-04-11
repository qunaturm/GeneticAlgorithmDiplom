namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Mutation
{
    public static class ShufflingMutation
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
                    // get indexes of chromosome to exchange
                    var firstChromosomeIndex = random.Next(0, individ.matrix.Length - 1);
                    var secondChromosomeIndex = random.Next(0, individ.matrix.Length - 1);

                    // eliminate repeat
                    while (secondChromosomeIndex == firstChromosomeIndex)
                    {
                        secondChromosomeIndex = random.Next(0, individ.matrix.Length - 1);
                    }

                    var amountOfChromosomesToExchange = firstChromosomeIndex > secondChromosomeIndex ? firstChromosomeIndex - secondChromosomeIndex + 1 : secondChromosomeIndex - firstChromosomeIndex + 1;

                    //exchange chromosomes
                    while (amountOfChromosomesToExchange > 1 && firstChromosomeIndex + 1 <= secondChromosomeIndex)
                    {
                        var individMatrix = individ.matrix;
                        MatrixOperations.SwapColls(ref individMatrix, firstChromosomeIndex, random.Next(firstChromosomeIndex + 1, secondChromosomeIndex));
                        individ.matrix = individMatrix;
                        firstChromosomeIndex++;
                        amountOfChromosomesToExchange--;
                    }
                    mutationCounter++;
                }
                mutated.Add(individ);
            }
            return mutated;
        };
    }
}
