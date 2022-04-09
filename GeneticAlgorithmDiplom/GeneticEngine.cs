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
        public long generationCount { get; set; } //Кол-во поколений
        public int individualCount { get; set; } //Кол-во Геномов(Индивидов,Особей) в поколении
        public SelectionType selectionType { get; set; } //Тип Селекции
        public CrossingType crossingType { get; set; } //Тип Скрещивания
        public bool useMutation { get; set; } //Использовать мутацю
        public double mutationPercent { get; set; } //Как часто происходит мутация
        public int elementInVector { get; set; }
        public int vectorsAmount { get; set; }

        /// <summary>
        /// Инициализация движка ГА с пользовательскими параметрами
        /// </summary>
        /// <param name="fitnessFunction">Fitness function</param>
        /// <param name="individualCount">Количество особей в поколении</param>
        /// <param name="selectionType">Тип селекции</param>
        /// <param name="crossingType">Тип скрещивания</param>
        /// <param name="useMutation">Мутация(да/нет)</param>
        /// <param name="mutationPercent">Вероятность мутации</param>
        /// <param name="elementInVector">Количество столбцов</param>
        /// <param name="vectorsAmount">Количество строк</param>
        public GeneticEngine(FitnessFunction fitnessFunction,
                             long generationCount,
                             int individualCount,
                             SelectionType selectionType,
                             CrossingType crossingType,
                             bool useMutation,
                             double mutationPercent,
                             int elementInVector,
                              int vectorsAmount)
        {
            this.fitnessFunction = fitnessFunction;
            this.generationCount = generationCount;
            this.generationCount = generationCount;
            this.individualCount = individualCount;
            this.selectionType = selectionType;
            this.crossingType = crossingType;
            this.mutationPercent = mutationPercent;
            this.elementInVector = elementInVector;
            this.vectorsAmount = vectorsAmount;
        }

        private List<Individual> GenerateFirstGeneration()
        {
            var vectors = MatrixRandom(elementInVector, vectorsAmount);
            var individualsList = new List<Individual>();
            for (int i = 0; i < individualCount; i++)
            {
                var squareMatrix = GetSquareMatrix(vectors, elementInVector, vectorsAmount);
                var det = GetDeterminant(squareMatrix);
                individualsList.Add(new Individual { matrix =  squareMatrix, determinant = det });
            }
            return individualsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementInVector"></param>
        /// <param name="vectorsAmount"></param>
        /// <param name="generationAmount"></param>
        /// <param name="bestFromTourney">should be % 2 == 0</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Individual> SelectionProcess(int bestFromTourney)
        {
            var firstGeneration = GenerateFirstGeneration();
            List<Individual> bestIndividuals = new List<Individual>();
            switch (selectionType)
            {
                case Roulette_Wheel:
                {
                    double[] wheel = new double[individualCount];
                    wheel[0] = FitnessFunction.GetFitness();
                    for (int i = 1; i < individualCount; i++)
                    {
                       wheel[i] = wheel[i - 1] + FitnessFunction.GetFitness();
                    }
                    return bestIndividuals;
                    break;
                }
                case Tourney:
                {
                        List<Individual> firstTourney = new List<Individual>();
                        List<Individual> secondTourney = new List<Individual>();
                        int counter = 1;
                        foreach (Individual individual in firstGeneration)
                        {
                            if (counter % 2 == 0)
                            {
                                if (double.IsNaN(individual.determinant) == true) { }
                                else
                                {
                                    firstTourney.Add(individual);
                                    counter++;
                                }
                            }
                            else
                            {
                                if (double.IsNaN(individual.determinant) == true) { }
                                else
                                {
                                    secondTourney.Add(individual);
                                    counter++;
                                }
                            }
                        }
                        // sort from min det to max
                        firstTourney = Individual.MergeSort(firstTourney);
                        secondTourney = Individual.MergeSort(secondTourney);

                        if (firstTourney.Count <= bestFromTourney / 2|| secondTourney.Count <= bestFromTourney / 2) throw new Exception("not enough individeals");
                        for (int i = firstTourney.Count - 1; i >= firstTourney.Count  - bestFromTourney / 2; --i)
                        {
                            bestIndividuals.Add(firstTourney[i]);
                        }

                        for (int i = secondTourney.Count - 1; i >= secondTourney.Count - bestFromTourney / 2; --i)
                        {
                            bestIndividuals.Add(secondTourney[i]);
                        }
                        return bestIndividuals;
                }
            }
            throw new Exception("cannot make selection process");
        }

        public List<Individual> CrossingProcess(List<Individual> parents)
        {
            List<Individual> children = new List<Individual>();
            switch(crossingType)
            {
                case CrossingType.One_Point_Crossover:
                {
                    var random = new Random();
                    while(parents.Count > 0)
                    {
                        // get two parent
                        var firstParent = parents[random.Next(0, parents.Count)];
                        parents.Remove(firstParent);
                        var secondParent = parents[random.Next(0, parents.Count)];
                        parents.Remove(secondParent);

                        //get index of chromosome for children
                        var firstHalf = random.Next(1, firstParent.matrix.Length - 2);

                        var child1Matrix = CopyColumn(firstParent.matrix, secondParent.matrix, firstHalf);
                        var child1Det = GetDeterminant(child1Matrix);

                        var child2Matrix = CopyColumn(secondParent.matrix, firstParent.matrix, firstHalf);
                        var child2Det = GetDeterminant(child2Matrix);

                        children.Add(new Individual { matrix = child1Matrix, determinant = child1Det });
                        children.Add(new Individual { matrix = child2Matrix, determinant = child2Det });

                    }
                    break;
                }
                case CrossingType.Two_Point_Crossover:
                {
                    var random = new Random();
                    while (parents.Count > 0)
                    {
                        // get two parent
                        var firstParent = parents[random.Next(0, parents.Count)];
                        parents.Remove(firstParent);
                        var secondParent = parents[random.Next(0, parents.Count)];
                        parents.Remove(secondParent);

                        //get indexes of chromosome for children
                        var firstHalf = random.Next(1, firstParent.matrix.Length - 2);
                        var secondHalf = 0;
                        secondHalf = firstHalf < firstParent.matrix.Length ? secondHalf = random.Next(firstHalf + 1, firstParent.matrix.Length - 2) : secondHalf = random.Next(1, firstHalf);

                        var listTwoChildren = CopyColumn(firstParent.matrix, secondParent.matrix, firstHalf, secondHalf);

                        children.Add(listTwoChildren[0]);
                        children.Add(listTwoChildren[1]);

                    }
                    break;
                }

                case CrossingType.Shuffler_Crossover:
                {
                        var random = new Random();
                        while (parents.Count > 0)
                        {
                            // get two parents index
                            var firstParentIndex = random.Next(0, parents.Count);
                            var secondParentIndex = random.Next(0, parents.Count);
                            // eliminate repeat
                            while(secondParentIndex == firstParentIndex)
                            {
                                secondParentIndex = random.Next(0, parents.Count);
                            }

                            // get parents
                            var firstParent = parents[firstParentIndex];
                            var secondParent = parents[secondParentIndex];
                            var half = random.Next(1,  parents[firstParentIndex].matrix.Length - 1);
                            var firstParentMatrix = firstParent.matrix;
                            var secondParentMatrix = secondParent.matrix;

                            // remove parents from initial sample
                            parents.Remove(firstParent);
                            parents.Remove(secondParent);
                            // swap genom for both
                            Swap(ref firstParentMatrix, ref secondParentMatrix, half);

                            // get first child data
                            var child1Matrix = CopyColumn(firstParentMatrix, secondParentMatrix, random.Next(0, firstParentMatrix.Length - 1));
                            var child1Det = GetDeterminant(child1Matrix);
                            children.Add(new Individual { matrix = child1Matrix, determinant = child1Det } );

                            //get second child data
                            var child2Matrix = CopyColumn(firstParentMatrix, secondParentMatrix, random.Next(0, firstParentMatrix.Length - 1));
                            var child2Det = GetDeterminant(child2Matrix);
                            children.Add(new Individual { matrix = child2Matrix, determinant = child2Det });
                        }
                        break;
                }
            }
            return children;
        }

        private void MutationProces()
        {

        }

        public static double[][] CopyColumn(double[][] sourceLeft, double[][] sourceRight, int index)
        {
            var result = new double[sourceLeft.Length][];
            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                result[i] = new double[sourceLeft.Length];
                for (int j = 0; j < index; ++j)
                {
                    result[i][j] = sourceLeft[i][j];
                }
            }

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                for (var j = index; j < sourceLeft.Length; ++j)
                {
                    result[i][j] = sourceRight[i][j];
                }
            }
            return result;
        }

        public static List<Individual> CopyColumn(double[][] sourceLeft, double[][] sourceRight, int firstIndex, int secondIndex)
        {
            var result1 = new double[sourceLeft.Length][];
            var result2 = new double[sourceLeft.Length][];

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                result1[i] = new double[sourceLeft.Length];
                result2[i] = new double[sourceLeft.Length];
                for (int j = 0; j < firstIndex; ++j)
                {
                    result1[i][j] = sourceLeft[i][j];
                    result2[i][j] = sourceRight[i][j];
                }
            }

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                for (var j = firstIndex; j < secondIndex; ++j)
                {
                    result1[i][j] = sourceRight[i][j];
                    result2[i][j] = sourceLeft[i][j];
                }
            }

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                for (var j = secondIndex; j < sourceLeft.Length; ++j)
                {
                    result1[i][j] = sourceLeft[i][j];
                    result2[i][j] = sourceRight[i][j];
                }
            }
            var child1 = new Individual { matrix = result1 };
            child1.determinant = GetDeterminant(child1.matrix);
            var child2 = new Individual { matrix = result2 };
            child2.determinant = GetDeterminant(child2.matrix);

            var list = new List<Individual>();
            list.Add(child1);
            list.Add(child2);
            return list;
        }
    }
}
