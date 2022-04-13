 namespace GeneticAlgorithmDiplom.Genitor.Mutation
{
    public static class ExchangeMutation
    {
        public static Func<Individual, double, Individual> Mutator = (individual, mutationPercent) =>
        {
            var random = new Random();

            var willMutate = random.NextDouble();
            if (willMutate > mutationPercent)
            {
                // get indexex of chromosome to exchange
                var firstChromosomeIndex = random.Next(0, individual.Matrix.Length - 1);
                var secondChromosomeIndex = random.Next(0, individual.Matrix.Length - 1);

                // eliminate repeat
                while (secondChromosomeIndex == firstChromosomeIndex)
                {
                    secondChromosomeIndex = random.Next(0, individual.Matrix.Length - 1);
                }

                //exchange chromosomes
                var individMatrix = individual.Matrix;
                MatrixOperations.SwapColls(ref individMatrix, firstChromosomeIndex, secondChromosomeIndex);
                individual.Matrix = individMatrix;
                individual.Determinant = MatrixOperations.GetDeterminant(individual.Matrix);
            }
            return individual;
        };
    }
}
