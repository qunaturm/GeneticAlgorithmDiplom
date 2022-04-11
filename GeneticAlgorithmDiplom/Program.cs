namespace GeneticAlgorithmDiplom
{
    public static class Diplom
    {
        public static void Main(string[] args)
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticAlgorithm.GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         GeneticAlgorithm.Selection.RouletteWheel.Selector,
                         GeneticAlgorithm.Crossing.TwoPointsCrossing.Crossover,
                         GeneticAlgorithm.Mutation.ExchangeMutation.Mutator,
                         true,
                         0.2,
                         true,
                         true,
                         200,
                         15);
            var bestIndivid = ga.RunGA();
            MatrixOperations.PrintMatrix(bestIndivid.matrix);
            Console.WriteLine();
            Console.WriteLine($"Best determinant = {bestIndivid.determinant}");
        }
    }
}