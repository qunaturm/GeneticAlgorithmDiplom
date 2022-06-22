using OptimizedGeneticAlgorithm.GeneticAlgorithm;
using OptimizedGeneticAlgorithm.GeneticAlgorithm.Crossing;
using OptimizedGeneticAlgorithm.GeneticAlgorithm.Mutation;
using System.Diagnostics;

namespace OptimizedGeneticAlgorithm
{
    public static class Diplom
    {
        public static void Main(string[] args)
        {
            var source = MatrixRandom(3000, 10);
            var matrixSource = new MatrixModule.MatrixSource(source);


            Console.WriteLine("GA:");
            var fitnessFunctionGA = new FitnessFunction();
            var ga = new GeneticEngine(matrixSource: matrixSource,
                                       fitnessFunction: fitnessFunctionGA,
                                       generationCount: 10000,
                                       individualCount: 100,
                                       selectionType: GeneticAlgorithm.Selection.Tourney.Selector,
                                       crossingType: OnePointCrossing.Crossover,
                                       mutationType: ExchangeMutation.Mutator,
                                       useMutation: true,
                                       mutationPercent: 0.2,
                                       enableElitism: true,
                                       stopAfterNGenerations: false,
                                       elementInVector: 3000,
                                       vectorsAmount: 10);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ga.RunGA();
            stopwatch.Stop();
            Console.WriteLine("Best Individual matrix:");
            MatrixOperations.PrintMatrix(fitnessFunctionGA.BestIndividual.GetRowMajorCopyMatrix());
            Console.WriteLine();
            Console.WriteLine($"Best determinant found at {fitnessFunctionGA.BestGenerationNumber} generation. It is: {fitnessFunctionGA.BestIndividual.Determinant}");
            Console.WriteLine($"Time = {stopwatch.Elapsed}");
        }

        public static double[][] CreateMatrix(int cols, int rows)
        {
            double[][] result = new double[cols][];
            for (int i = 0; i < cols; ++i)
                result[i] = new double[rows];
            return result;
        }

        public static double[][] MatrixRandom(int cols, int rows)
        {
            var minVal = 1;
            var maxVal = 10;  // not included
            Random random = new Random();
            double[][] result = CreateMatrix(cols, rows);
            for (int i = 0; i < cols; ++i)
                for (int j = 0; j < rows; ++j)
                    result[i][j] = random.Next(minVal, maxVal);
            return result;
        }
    }
}