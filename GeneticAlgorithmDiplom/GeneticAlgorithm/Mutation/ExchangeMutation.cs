namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Mutation
{
    public static class ExchangeMutation
    {
        public static Func<List<Individual>, List<Individual>, double, List<Individual>> Mutator = (individuals, list,  mutationPercent) =>
        {
            var mutated = new List<Individual>();
            var random = new Random();
            var mutationCounter = 0;
            foreach (var individ in individuals)
            {
                var willMutate = random.NextDouble();
                if (willMutate <= mutationPercent)
                {
                    // get indexex of chromosome to exchange
                    var firstChromosomeIndex = random.Next(0, individ.Matrix.Length - 1);
                    var secondChromosomeIndex = random.Next(0, individ.Matrix.Length - 1);

                    // eliminate repeat
                    while (secondChromosomeIndex == firstChromosomeIndex)
                    {
                        secondChromosomeIndex = random.Next(0, individ.Matrix.Length - 1);
                    }

                    //exchange chromosomes
                    var individMatrix = individ.Matrix;
                    MatrixOperations.SwapColls(ref individMatrix, firstChromosomeIndex, secondChromosomeIndex);
                    individ.Matrix = individMatrix;
                    individ.Determinant = MatrixOperations.GetDeterminant(individ.Matrix);
                    mutationCounter++;
                }
                mutated.Add(individ);
            }
            return mutated;
        };
    }
}
