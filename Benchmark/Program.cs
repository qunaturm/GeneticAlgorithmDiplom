using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using GeneticAlgorithmDiplom;

public class BenchmarkGA
{
    [Benchmark]
    public void Test_GA_Engine()
    {
        var fitnessFunction = new FitnessFunction();
        var ga = new GeneticAlgorithmDiplom.GeneticAlgorithm.GeneticEngine(
                     fitnessFunction: fitnessFunction,
                     generationCount: 100,
                     individualCount: 40,
                     selectionType: GeneticAlgorithmDiplom.GeneticAlgorithm.Selection.Tourney.Selector,
                     crossingType: GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing.TwoPointsCrossing.Crossover,
                     mutationType: GeneticAlgorithmDiplom.GeneticAlgorithm.Mutation.ClassicMutation.Mutator,
                     useMutation: true,
                     mutationPercent: 0.2,
                     enableElitism: true,
                     stopAfterNGenerations: false,
                     elementInVector: 3000,
                     vectorsAmount: 5);
        ga.RunGA();
    }
}

public class TestGA
{
    public static void Main()
    {
        BenchmarkRunner.Run<BenchmarkGA>();
    }
}
