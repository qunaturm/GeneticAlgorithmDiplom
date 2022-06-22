using GeneticAlgorithmDiplom.Genitor;
using System.Diagnostics;

namespace GeneticAlgorithmDiplom
{
    public static class Diplom
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("GA:");
            var fitnessFunctionGA = new FitnessFunction();
            var ga = new GeneticAlgorithm.GeneticEngine(
                         fitnessFunction: fitnessFunctionGA,
                         generationCount: 100,
                         individualCount: 100,
                         selectionType: GeneticAlgorithm.Selection.Tourney.Selector,
                         crossingType: GeneticAlgorithm.Crossing.OnePointCrossing.Crossover,
                         mutationType: GeneticAlgorithm.Mutation.ClassicMutation.Mutator,
                         useMutation: true,
                         mutationPercent: 0.1,
                         enableElitism: false,
                         stopAfterNGenerations: false,
                         vectorsAmount: 10000,
                         elementInVector: 5);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ga.RunGA();
            stopwatch.Stop();
            //Console.WriteLine($"Dimension = {ga.elementInVector}, max determinant for this dimension = {AmadarMatrixDeterminant.CheckWithAmadar(ga.elementInVector)}");
            Console.WriteLine("Best Individual matrix:");
            MatrixOperations.PrintMatrix(fitnessFunctionGA.BestIndividual.Matrix);
            Console.WriteLine();
            Console.WriteLine($"Best determinant found at {fitnessFunctionGA.BestGenerationNumber} generation. It is: {fitnessFunctionGA.BestIndividual.Determinant}");
            Console.WriteLine($"Time = {stopwatch.Elapsed}");


/*            var fitnessFunctionGenitor = new FitnessFunction();
            Console.WriteLine("Genitor:");
            var genitor = new GenitorEngine(
                         fitnessFunction: fitnessFunctionGenitor,
                         generationCount: 1000,
                         individualCount: 100,
                         selectionType: Genitor.Selection.RouletteWheel.Selector,
                         crossingType: Genitor.Crossing.Two_Point_Crossover.Crossover,
                         mutationType: Genitor.Mutation.ExchangeMutation.Mutator,
                         useMutation: true,
                         mutationPercent: 0.2,
                         stopAfterNGenerations: false,
                         elementInVector: 3000,
                         vectorsAmount: 5);
            genitor.RunGenitor();
            Console.WriteLine("Best Individual matrix:");
            MatrixOperations.PrintMatrix(fitnessFunctionGenitor.BestIndividual.Matrix);
            Console.WriteLine();
            Console.WriteLine($"Best determinant found at {fitnessFunctionGenitor.BestGenerationNumber} generation. It is: {fitnessFunctionGenitor.BestIndividual.Determinant}");*/

        }
    }
}