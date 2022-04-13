namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Mutation
{
    public static class ClassicMutation
    {

        public static Func<List<Individual>, List<Individual>, double, List<Individual>> Mutator = (individuals, vectors, mutationPercent) =>
        {
            var mutated = new List<Individual>();
            var random = new Random();
            var mutationCounter = 0;
            foreach (var individ in individuals)
            {
                var willMutate = random.NextDouble();
                if (willMutate <= mutationPercent)
                {
                    // get index of chromosome to exchange
                    var firstChromosomeIndex = random.Next(0, individ.Matrix.Length - 1);

                    // get index of new chromosome
                    var secondChromosomeIndex = random.Next(0, vectors.Count - 1);

                    //exchange chromosomes
                    var individMatrix = individ.Matrix;
                    MatrixOperations.SwapColls(ref individMatrix, firstChromosomeIndex, vectors[secondChromosomeIndex].Matrix[random.Next(0, individMatrix.Length - 1)]);
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
