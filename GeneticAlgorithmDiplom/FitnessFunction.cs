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
            if (currentBestIndividual.Determinant > BestIndividual.Determinant && !double.IsNaN(currentBestIndividual.Determinant) && !double.IsInfinity(currentBestIndividual.Determinant))
            {
                var copy = MatrixOperations.MatrixDuplicate(currentBestIndividual.Matrix);
                BestIndividual.Matrix = copy;
                BestIndividual.Determinant = MatrixOperations.GetDeterminant(BestIndividual.Matrix);
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
