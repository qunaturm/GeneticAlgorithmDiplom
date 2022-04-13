namespace GeneticAlgorithmDiplom.Genitor.Selection
{
    public static class RouletteWheel
    {

        public static Func<List<Individual>, List<Individual>> Selector = (generation) =>
        {
            List<Individual> parents = new List<Individual>();
            var distributionValues = new double[generation.Count];
            for (int individIndex = 0; individIndex < generation.Count; individIndex++)
            {
                distributionValues[individIndex] = generation[individIndex].Determinant;
            }
            var vers = Perc(distributionValues);

            var random = new Random();
            var firstParentIndex = GetRNDIndex(random, vers);
            var secondParentIndex = GetRNDIndex(random, vers);
            while (firstParentIndex == secondParentIndex)
            {
                secondParentIndex = GetRNDIndex(random, vers);
            }
            parents.Add(new Individual { Matrix = generation[firstParentIndex].Matrix, Determinant = generation[firstParentIndex].Determinant });
            parents.Add(new Individual { Matrix = generation[secondParentIndex].Matrix, Determinant = generation[secondParentIndex].Determinant });
            return parents;
        };
        private static double[] Perc(double[] vers)
        {
            double sum = vers.Sum();
            vers[0] /= sum;
            for (int i = 1; i < vers.Length; i++)
            {
                vers[i] = vers[i] / sum + vers[i - 1];
            }
            vers[vers.Length - 1] = 1.0;
            return vers;
        }

        private static int GetRNDIndex(Random rnd, double[] vers)
        {
            double rndval = rnd.NextDouble();
            for (int i = 0; i < vers.Length; i++)
                if (vers[i] > rndval)
                    return i;
            return 1;
        }
    }
}
