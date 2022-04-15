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
                         generationCount: 1000,
                         individualCount: 100,
                         selectionType: GeneticAlgorithm.Selection.RouletteWheel.Selector,
                         crossingType: GeneticAlgorithm.Crossing.ShufflerCrossing.Crossover,
                         mutationType: GeneticAlgorithm.Mutation.ClassicMutation.Mutator,
                         useMutation: true,
                         mutationPercent: 0.2,
                         enableElitism: false,
                         stopAfterNGenerations: false,
                         vectorsAmount: 3000,
                         elementInVector: 10);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ga.RunGA();
            stopwatch.Stop();
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