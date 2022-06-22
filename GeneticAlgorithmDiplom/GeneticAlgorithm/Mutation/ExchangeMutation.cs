namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Mutation
{
    public static class ExchangeMutation
    {
        public static Func<List<Individual>, List<Individual>, double, List<Individual>> Mutator = (individuals, list,  mutationPercent) =>
        {
            var mutated = new List<Individual>();
            var random = new Random();

            var mutationFactors = individuals.Select(u => new MutationFactor
            {
                Individual = u,
                ChromosomePair = random.NextDouble() <= mutationPercent ? (random.Next(0, u.Matrix.Length - 1), random.Next(0, u.Matrix.Length - 1)) : (0, 0)
            }); ;

            var resutl = Parallel.ForEach(mutationFactors, u => Mutate(u));
            mutated = mutationFactors.Select(x => x.Individual).ToList();
            return mutated;
        };

        internal static Individual Mutate(MutationFactor mutationFactor)
        {
            if (mutationFactor.ChromosomePair == (0, 0)) return mutationFactor.Individual;
            var individMatrix = mutationFactor.Individual.Matrix;
            MatrixOperations.SwapColls(ref individMatrix, mutationFactor.ChromosomePair.Item1, mutationFactor.ChromosomePair.Item2);
            mutationFactor.Individual.Matrix = individMatrix;
            mutationFactor.Individual.Determinant = MatrixOperations.GetDeterminant(mutationFactor.Individual.Matrix);
            return mutationFactor.Individual;
        }

        internal class MutationFactor
        {
            internal Individual Individual { get; set; }
            internal (int, int) ChromosomePair { get; set; }
        }
    }
}
