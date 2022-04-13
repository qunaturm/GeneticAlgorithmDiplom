using System.Diagnostics;

namespace GeneticAlgorithmDiplom
{
    public static class Diplom
    {
        public static void Main(string[] args)
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticAlgorithm.GeneticEngine(
                         fitnessFunction: fitnessFunction,
                         generationCount: 100,
                         individualCount: 40,
                         selectionType: GeneticAlgorithm.Selection.Tourney.Selector,
                         crossingType: GeneticAlgorithm.Crossing.TwoPointsCrossing.Crossover,
                         mutationType: GeneticAlgorithm.Mutation.ClassicMutation.Mutator,
                         useMutation: true,
                         mutationPercent: 0.2,
                         enableElitism: true,
                         stopAfterNGenerations: false,
                         elementInVector: 3000,
                         vectorsAmount: 5);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ga.RunGA();
            stopwatch.Stop();

            Console.WriteLine("Best Individual matrix:");
            MatrixOperations.PrintMatrix(fitnessFunction.BestIndividual.Matrix);
            Console.WriteLine();
            Console.WriteLine($"Best determinant found at {fitnessFunction.BestGenerationNumber} generation. It is: {fitnessFunction.BestIndividual.Determinant}");
            Console.WriteLine($"Time = {stopwatch.Elapsed}");
        }
    }
}