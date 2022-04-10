namespace GeneticAlgorithmDiplom
{
    public static class Diplom
    {
        public static void Main(string[] args)
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                            fitnessFunction,
                            100,
                            100,
                            SelectionType.Tourney,
                            CrossingType.One_Point_Crossover,
                            MutationType.ExchangeMutation,
                            true,
                            0.05,
                            true,
                            300,
                            15);
            var bestIndivid = ga.RunGA();
            MatrixOperations.PrintMatrix(bestIndivid.matrix);
            Console.WriteLine();
            Console.WriteLine($"Best determinant = {bestIndivid.determinant}");
        }
    }
}