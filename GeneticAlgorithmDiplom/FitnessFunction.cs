namespace GeneticAlgorithmDiplom
{
    public class FitnessFunction
    {
        public double fitnessValue = 1.0;
        public FitnessFunction()
        {

        }
        public static double GetFitness()
        {
            var fitnessValue = 1.0;
            return fitnessValue;
        }
    }

    public enum SelectionType
    {
        Tourney,
        Roulette_Wheel
    }

    public enum CrossingType
    {
        One_Point_Recombination,
        Two_Point_Recombination,
        Elementwise_Recombination,
        One_Element_Exchange
    }
}
