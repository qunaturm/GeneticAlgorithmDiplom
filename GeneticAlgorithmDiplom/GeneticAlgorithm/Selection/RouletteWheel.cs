namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Selection
{
    public static class RouletteWheel
    {
        public static Func<List<Individual>, int, bool, List<Individual>> Selector = (firstGeneration, bestFromSelection, enableElitism) =>
        {
            List<Individual> bestIndividuals = new List<Individual>();
            if (enableElitism == true)
            {
                var parents = Individual.MergeSort(firstGeneration);
                bestIndividuals.Add(parents[parents.Count - 1]);
                bestIndividuals.Add(parents[parents.Count - 2]);
            }
            var random = new Random();
            var distributionValues = new double[firstGeneration.Count];
            for (int individIndex = 0; individIndex < firstGeneration.Count; individIndex++)
            {
                distributionValues[individIndex] = firstGeneration[individIndex].determinant;
            }
            var vers = new double[firstGeneration.Count];
            var index = 0;
            for (int i = 0; i < bestFromSelection; i++)
            {
                vers = Perc(distributionValues);
                random = new Random();
                index = GetRNDIndex(random, vers);
                bestIndividuals.Add(firstGeneration[index]);

                // remove used individual frome sample
                firstGeneration.RemoveAt(index);
                distributionValues = distributionValues.Where((val, idx) => idx != index).ToArray();
            }
            // case if elitism enable
            if (bestIndividuals.Count != bestFromSelection)
            {
                bestIndividuals = Individual.MergeSort(bestIndividuals);
                bestIndividuals.RemoveRange(0, 2);
            }
            return bestIndividuals;
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
