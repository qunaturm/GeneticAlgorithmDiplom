using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using GeneticAlgorithmDiplom;

[Config(typeof(HabrExampleConfig))]
public class BenchmarkGA
{
    internal class HabrExampleConfig : ManualConfig
    {
        public HabrExampleConfig()
        {
            Add(StatisticColumn.Max);
            Add(MarkdownExporter.Default, HtmlExporter.Default, CsvExporter.Default);
        }
    }

    [Benchmark]
    public void Test_GA_Engine()
    {
        var fitnessFunction = new FitnessFunction();
        var ga = new GeneticAlgorithmDiplom.GeneticAlgorithm.GeneticEngine(
                     fitnessFunction: fitnessFunction,
                     generationCount: 10000,
                     individualCount: 100,
                     selectionType: GeneticAlgorithmDiplom.GeneticAlgorithm.Selection.Tourney.Selector,
                     crossingType: GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing.OnePointCrossing.Crossover,
                     mutationType: GeneticAlgorithmDiplom.GeneticAlgorithm.Mutation.ExchangeMutation.Mutator,
                     useMutation: true,
                     mutationPercent: 0.2,
                     enableElitism: true,
                     stopAfterNGenerations: false,
                     elementInVector: 3000,
                     vectorsAmount: 10);
        ga.RunGA();
    }

    [Benchmark]
    public void Test_Optimize_GA_Engine()
    {
        var source = OptimizedGeneticAlgorithm.Diplom.MatrixRandom(3000, 10);
        var matrixSource = new MatrixModule.MatrixSource(source);

        var fitnessFunction = new OptimizedGeneticAlgorithm.FitnessFunction();
        var ga = new OptimizedGeneticAlgorithm.GeneticAlgorithm.GeneticEngine(
            matrixSource: matrixSource,
                     fitnessFunction: fitnessFunction,
                     generationCount: 10000,
                     individualCount: 100,
                     selectionType: OptimizedGeneticAlgorithm.GeneticAlgorithm.Selection.Tourney.Selector,
                     crossingType: OptimizedGeneticAlgorithm.GeneticAlgorithm.Crossing.OnePointCrossing.Crossover,
                     mutationType: OptimizedGeneticAlgorithm.GeneticAlgorithm.Mutation.ExchangeMutation.Mutator,
                     useMutation: true,
                     mutationPercent: 0.2,
                     enableElitism: true,
                     stopAfterNGenerations: false,
                     elementInVector: 3000,
                     vectorsAmount: 10);
        ga.RunGA();
    }

}

public class TestGA
{
    public static void Main()
    {
        //var result = BenchmarkRunner.Run<BenchmarkGA>();

        var matrix = new MatrixModule.DependentMatrix(new List<List<double>>
        {
            new List<double> {1, 1, 1, 1, 1, 1, -1, -1, 1, -1, -1, -1},
            new List<double> {1, -1, -1, -1, -1, 1, -1, -1, 1, -1, 1, -1},
            new List<double> {1, 1, 1, -1, -1, -1, 1, 1, 1, 1, 1, 1},
            new List<double> {-1, -1, 1, 1, -1, -1, -1, -1, -1, -1, 1, 1},
            new List<double> {-1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1},
            new List<double> {-1, 1, -1, 1, -1, -1, -1, 1, 1, -1, -1, -1},
            new List<double> {-1, 1, -1, -1, -1, 1, 1, 1, -1, 1, 1, 1},
            new List<double> {1, -1, -1, -1, 1, -1, -1, -1, -1, 1, 1, -1},
            new List<double> {-1, -1, 1, -1, 1, -1, -1, 1, 1, -1, 1, 1},
            new List<double> {1, 1, -1, -1, 1, -1, 1, -1, -1, -1, 1, -1},
            new List<double> {1, -1, 1, -1, -1, 1, -1, 1, -1, -1, -1, -1},
            new List<double> {1, -1, -1, 1, 1, -1, -1, 1, -1, 1, -1, -1}
        }, 12);

        var a = matrix.GetCopyMatrix();
        var matrix2 = new MatrixModule.DependentMatrix(a, 12);


        //Console.WriteLine(Determinant(m)); // -12288
        Console.WriteLine(matrix2.Determinant);

    }
}
