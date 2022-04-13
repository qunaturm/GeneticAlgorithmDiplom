namespace GeneticAlgorithmDiplom.Genitor.Mutation
{
    public static class ShufflingMutation
    {
        public static Func<Individual, double, Individual> Mutator = (individual, mutationPercent) =>
        {
            var random = new Random();

            var willMutate = random.NextDouble();
            if (willMutate > mutationPercent)
            {
                // get indexes of chromosome to exchange
                var firstChromosomeIndex = random.Next(0, individual.Matrix.Length - 1);
                var secondChromosomeIndex = random.Next(0, individual.Matrix.Length - 1);

                // eliminate repeat
                while (secondChromosomeIndex == firstChromosomeIndex)
                {
                    secondChromosomeIndex = random.Next(0, individual.Matrix.Length - 1);
                }

                var amountOfChromosomesToExchange = firstChromosomeIndex > secondChromosomeIndex ? firstChromosomeIndex - secondChromosomeIndex + 1 : secondChromosomeIndex - firstChromosomeIndex + 1;

                //exchange chromosomes
                while (amountOfChromosomesToExchange > 1 && firstChromosomeIndex + 1 <= secondChromosomeIndex)
                {
                    var individMatrix = individual.Matrix;
                    MatrixOperations.SwapColls(ref individMatrix, firstChromosomeIndex, random.Next(firstChromosomeIndex + 1, secondChromosomeIndex));
                    individual.Matrix = individMatrix;
                    firstChromosomeIndex++;
                    amountOfChromosomesToExchange--;
                }
                individual.Determinant = MatrixOperations.GetDeterminant(individual.Matrix);
            }
            return individual;
        };
    }
}
