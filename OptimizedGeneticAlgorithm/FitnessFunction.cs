using MatrixModule;

namespace OptimizedGeneticAlgorithm
{
    public class FitnessFunction
    {
        public DependentMatrix BestIndividual { get; set; }
        public double BestDeterminant { get; set; }
        public int GenerationWithoutProgressCounter { get; set; }
        public int BestGenerationNumber { get; set; }
        public FitnessFunction()
        {
            BestDeterminant = double.MinValue;
            GenerationWithoutProgressCounter = 0;
        }
        public DependentMatrix Fitness(DependentMatrix currentBestIndividual, int generationNumber)
        {
            if (currentBestIndividual.Determinant > BestDeterminant)
            {
                BestDeterminant = currentBestIndividual.Determinant;
                BestIndividual = currentBestIndividual;
                BestGenerationNumber = generationNumber;
                GenerationWithoutProgressCounter = 0;
            }
            else
            {
                GenerationWithoutProgressCounter++;
            }
            return BestIndividual;
        }
    }
}
