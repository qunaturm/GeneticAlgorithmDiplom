using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneticAlgorithmDiplom.SelectionType;
using static GeneticAlgorithmDiplom.Matrix;

namespace GeneticAlgorithmDiplom
{
    public class GeneticEngine
    {
        public FitnessFunction fitnessFunction { get; set; }
        public int genomLength { get; set; } //Длина генома в битах
        public long generationCount { get; set; } //Кол-во поколений
        public int individualCount { get; set; } //Кол-во Геномов(Индивидов,Особей) в поколении
        public SelectionType selectionType { get; set; } //Тип Селекции
        public CrossingType crossingType { get; set; } //Тип Скрещивания
        public bool useMutation { get; set; } //Использовать мутацю
        public double mutationPercent { get; set; } //Как часто происходит мутация

        public GeneticEngine()
        {

        }

        public GeneticEngine(FitnessFunction fitnessFunction,
                             int genomLength,
                             int individualCount,
                             SelectionType selectionType,
                             CrossingType crossingType,
                             bool useMutation,
                             double mutationPercent)
        {
            this.fitnessFunction = fitnessFunction;
            this.genomLength = genomLength;
            this.generationCount = generationCount;
            this.individualCount = individualCount;
            this.selectionType = selectionType;
            this.crossingType = crossingType;
            this.mutationPercent = mutationPercent;
        }

        public List<Individual> GenerateFirstGeneration(int elementInVector, int vectorsAmount, int generationCount)
        {
            var vectors = MatrixRandom(elementInVector, vectorsAmount);
            var individualsList = new List<Individual>();
            for (int i = 0; i < generationCount; i++)
            {
                var squareMatrix = GetSquareMatrix(vectors, elementInVector, vectorsAmount);
                var det = GetDeterminant(squareMatrix);
                individualsList.Add(new Individual { matrix =  squareMatrix, determinant = det });
            }
            return individualsList;
        }

        private void SelectionProcess(int elementInVector, int vectorsAmount, int generationAmount)
        {
            var firstGeneration = GenerateFirstGeneration(elementInVector, vectorsAmount, generationAmount);
            switch(selectionType)
            {
                case Roulette_Wheel:
                {
                    double[] wheel = new double[individualCount];
                    wheel[0] = FitnessFunction.GetFitness();
                    for (int i = 1; i < individualCount; i++)
                    {
                       wheel[i] = wheel[i - 1] + FitnessFunction.GetFitness();
                    }
                    break;
                }
                case Tourney:
                {
                        break;
                }
            }
        }

        private void CrossingProcess()
        {

        }

        private void MutationProces()
        {

        }
    }
}
