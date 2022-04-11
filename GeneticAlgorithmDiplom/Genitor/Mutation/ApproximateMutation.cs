﻿namespace GeneticAlgorithmDiplom.Genitor.Mutation
{
    public static class ApproximateMutation
    {
        public static Func<List<Individual>, double, double, List<Individual>> Mutator = (individuals, mutationPercent, stddev) =>
        {
            var mutated = new List<Individual>();
            var random = new Random();
            var mutationCounter = 0;
            foreach (var individ in individuals)
            {
                var willMutate = random.NextDouble();
                if (willMutate > mutationPercent)
                {
                    // get chromosome to exchange
                    var chromosomeToExchange = random.Next(0, individ.matrix.Length - 1);

                    // exchange chromosomes
                    double newValue = 0.0;
                    var individMatrix = individ.matrix;
                    for (int i = 0; i < individMatrix.Length; i++)
                    {
                        newValue = GaussForShufflerMutation(individ.matrix[chromosomeToExchange], stddev);
                        individMatrix[i][chromosomeToExchange] = newValue;
                    }
                    individ.matrix = individMatrix;
                    mutationCounter++;
                }
                mutated.Add(individ);
            }
            return mutated;
        };
        private static double GaussForShufflerMutation(double[] chromosome, double stddev)
        {
            var random = new Random();
            var mutationRate = 2;
            double mean = 0;
            for (int i = 0; i < chromosome.Length; i++)
            {
                mean += chromosome[i];
            }
            mean /= chromosome.Length;
            return Math.Round(SampleGaussian(random, mean, stddev));
        }
        public static double SampleGaussian(Random random, double mean, double stddev)
        {
            double x1 = 1 - random.NextDouble();
            double x2 = 1 - random.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            return y1 * stddev + mean;
        }
    }
}
