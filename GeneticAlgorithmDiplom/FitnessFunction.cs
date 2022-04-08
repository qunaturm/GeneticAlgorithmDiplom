using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmDiplom
{
    public class FitnessFunction
    {
        //public int GetArity(); // get bits amount from genom
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
