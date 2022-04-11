using static GeneticAlgorithmDiplom.MatrixOperations;

namespace GeneticAlgorithmDiplom.Genitor
{
    public class GenitorEngine
    {
        private static double stddev = 0.5; // Среднее отклонение для распределения Гаусса
        private static List<Individual>? currentGeneration { get; set; }
        public FitnessFunction fitnessFunction { get; set; }
        public long generationCount { get; set; } // Кол-во поколений
        public int individualCount { get; set; } // Кол-во индивидов в поколении
        public Func<List<Individual>, List<Individual>> selectionType { get; set; } // Тип селекции
        public Func<List<Individual>, List<Individual>> crossingType { get; set; } // Тип скрещивания
        public Func<List<Individual>, List<Individual>> mutationType { get; set; } // Тип мутации
        public bool useMutation { get; set; } // Использовать мутацию
        public double mutationPercent { get; set; } // Как часто происходит мутация
        public int elementInVector { get; set; }
        public int vectorsAmount { get; set; }

        /// <summary>
        /// Инициализация движка Генитора с пользовательскими параметрами
        /// </summary>
        /// <param name="fitnessFunction">Fitness function</param>
        /// <param name="generationCount">Количество поколений</param>
        /// <param name="individualCount">Количество особей в поколении</param>
        /// <param name="selectionType">Тип селекции</param>
        /// <param name="crossingType">Тип скрещивания</param>
        /// <param name="mutationType">Тип мутации</param>
        /// <param name="useMutation">Мутация(да/нет)</param>
        /// <param name="mutationPercent">Вероятность мутации</param>
        /// <param name="elementInVector">Количество столбцов</param>
        /// <param name="vectorsAmount">Количество строк</param>
        public GenitorEngine(FitnessFunction fitnessFunction,
                             long generationCount,
                             int individualCount,
                             Func<List<Individual>, List<Individual>> selectionType,
                             Func<List<Individual>, List<Individual>> crossingType,
                             Func<List<Individual>, List<Individual>> mutationType,
                             bool useMutation,
                             double mutationPercent,
                             int elementInVector,
                             int vectorsAmount)
        {
            this.fitnessFunction = fitnessFunction;
            this.generationCount = generationCount;
            this.individualCount = individualCount;
            this.selectionType = selectionType;
            this.crossingType = crossingType;
            this.mutationType = mutationType;
            this.useMutation = useMutation;
            this.mutationPercent = mutationPercent;
            this.elementInVector = elementInVector;
            this.vectorsAmount = vectorsAmount;
        }

        /// <summary>
        /// Метод запуска работы ГА
        /// </summary>
        /// <returns></returns>
        public Individual RunGenitor()
        {
            var bestIndivids = new List<Individual>();
            currentGeneration = GenerateFirstGeneration();
            var list = new List<Individual>();
            for (int i = 0; i < generationCount; ++i)
            {
                var supaBestGenerationIndividuals = currentGeneration.OrderByDescending(u => u.determinant).First();
                list.Add(supaBestGenerationIndividuals);
                bestIndivids = Individual.MergeSort(currentGeneration);
                //Console.WriteLine($"Current generation - {i}");
                //Console.WriteLine($"Best individ from current generation:");
                //PrintMatrix(bestIndivids[individualCount - 1].matrix);
                Console.WriteLine($"Generation {i}, best determinant = {bestIndivids[bestIndivids.Count - 1].determinant}");
                Console.WriteLine("____________________________");
            }
            Console.WriteLine($"Generation , best determinant = {list.OrderByDescending(u => u.determinant).FirstOrDefault().determinant}");
            Console.WriteLine("____________________________");
            return bestIndivids[bestIndivids.Count - 1];
        }

        /// <summary>
        /// Метод генерации первого поколения особей(индивидов)
        /// </summary>
        /// <returns>Возвращает сгенерированный список особей</returns>
        private List<Individual> GenerateFirstGeneration()
        {
            var vectors = MatrixRandom(elementInVector, vectorsAmount);
            var individualsList = new List<Individual>();
            for (int i = 0; i < individualCount; i++)
            {
                var squareMatrix = GetSquareMatrix(vectors, elementInVector, vectorsAmount);
                var det = GetDeterminant(squareMatrix);
                individualsList.Add(new Individual { matrix = squareMatrix, determinant = det });
            }
            return individualsList;
        }

        #region [ methods for selection tourney ]
        private double[] Perc(double[] vers)
        {
            double sum = vers.Sum();
            vers[0] /= sum;
            for (int i = 1; i < vers.Length; i++)
            {
                vers[i] = vers[i] / sum + vers[i - 1];
            }
            vers[vers.Length - 1] = 1.0;
            return vers;
        }

        private int GetRNDIndex(Random rnd, double[] vers)
        {
            double rndval = rnd.NextDouble();
            for (int i = 0; i < vers.Length; i++)
                if (vers[i] > rndval)
                    return i;
            return 1;
        }
        #endregion

        #region [ methods for shuffler mutation ]
        private double GaussForShufflerMutation(double[] chromosome)
        {
            var random = new Random();
            var mutationRate = 2;
            double mean = 0;
            for (int i = 0; i < chromosome.Length; i++)
            {
                mean += chromosome[i];
            }
            mean /= chromosome.Length;
            return Math.Round(SampleGaussian(random, mean, stddev));
        }

        /// <summary>
        /// Распределение Гаусса
        /// </summary>
        /// <param name="random"></param>
        /// <param name="mean">Среднее значение</param>
        /// <param name="stddev">Стандартное отклонение</param>
        /// <returns></returns>
        private static double SampleGaussian(Random random, double mean, double stddev)
        {
            double x1 = 1 - random.NextDouble();
            double x2 = 1 - random.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            return y1 * stddev + mean;
        }
        #endregion

        #region [ methods for crossing ]
        private static double[][] CopyColumn(double[][] sourceLeft, double[][] sourceRight, int index)
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

        private static List<Individual> CopyColumn(double[][] sourceLeft, double[][] sourceRight, int firstIndex, int secondIndex)
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
        #endregion
    }

}

