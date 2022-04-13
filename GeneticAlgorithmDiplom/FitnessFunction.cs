namespace GeneticAlgorithmDiplom
{
    public class FitnessFunction
    {
        public Individual BestIndividual { get; set; }
        public int GenerationWithoutProgressCounter { get; set; }
        public int BestGenerationNumber { get; set; }
        public FitnessFunction()
        {
            BestIndividual = new Individual { Determinant = double.MinValue};
            GenerationWithoutProgressCounter = 0;
        }
        public Individual Fitness(Individual currentBestIndividual, int generationNumber)
        {
            if (currentBestIndividual.Determinant > BestIndividual.Determinant)
            {
                BestIndividual.Determinant = currentBestIndividual.Determinant;
                BestIndividual.Matrix = currentBestIndividual.Matrix;
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
