using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmDiplom
{
    public class GeneticEngine
    {
        private int genomLength; //Длина генома в битах
        private long generationCount; //Кол-во поколений
        private int individualCount; //Кол-во Геномов(Индивидов,Особей) в поколении
        //private SelectionType selectionType; //Тип Селекции
        //private CrossingType crossingType; //Тип Скрещивания
        private bool useMutation; //Использовать мутацю
        private double mutationPercent; //Как часто происходит мутация

        public GeneticEngine(FitnessFunction fitnessFunction)
        {

        }

        private void GenerateFirstGeneration()
        {

        }

        private void SelectionProcess()
        {

        }

        private void CrossingProcess()
        {

        }

        private void MutationProces()
        {

        }
    }
}
