using static GeneticAlgorithmDiplom.MatrixOperations;

namespace GeneticAlgorithmDiplom.GeneticAlgorithm
{
    public sealed class GeneticEngine
    {
        public FitnessFunction fitnessFunction { get; set; }
        public long generationCount { get; set; } // Кол-во поколений
        public int individualCount { get; set; } // Кол-во индивидов в поколении
        public Func<List<Individual>, int, bool, List<Individual>> selectionType { get; set; } // Тип селекции
        public Func<List<Individual>, List<Individual>> crossingType { get; set; } // Тип скрещивания
        public Func<List<Individual>, double, List<Individual>> mutationType { get; set; } // Тип мутации
        public bool useMutation { get; set; } // Использовать мутацию
        public double mutationPercent { get; set; } // Как часто происходит мутация
        public bool enableElitism { get; set; } // Включить элитарность
        public bool stopAfterNGenerations { get; set; } // Прекратить работу в случае, если в течение длительного периода не наблюдается улучшения характеристик особей в поколении
        public int elementInVector { get; set; }
        public int vectorsAmount { get; set; }

        /// <summary>
        /// Инициализация движка ГА с пользовательскими параметрами
        /// </summary>
        /// <param name="fitnessFunction">Fitness function</param>
        /// <param name="generationCount">Количество поколений</param>
        /// <param name="individualCount">Количество особей в поколении</param>
        /// <param name="selectionType">Тип селекции</param>
        /// <param name="crossingType">Тип скрещивания</param>
        /// <param name="mutationType">Тип мутации</param>
        /// <param name="useMutation">Мутация(да/нет)</param>
        /// <param name="mutationPercent">Вероятность мутации</param>
        /// <param name="enableElitism">Включить элитарность</param>
        /// <param name="elementInVector">Количество столбцов</param>
        /// <param name="vectorsAmount">Количество строк</param>
        public GeneticEngine(FitnessFunction fitnessFunction,
                             long generationCount,
                             int individualCount,
                             Func<List<Individual>, int, bool, List<Individual>> selectionType,
                             Func<List<Individual>, List<Individual>> crossingType,
                             Func<List<Individual>, double, List<Individual>> mutationType,
                             bool useMutation,
                             double mutationPercent,
                             bool enableElitism,
                             bool stopAfterNGenerations,
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
            this.enableElitism = enableElitism;
            this.stopAfterNGenerations = stopAfterNGenerations;
            this.elementInVector = elementInVector;
            this.vectorsAmount = vectorsAmount;
        }

        /// <summary>
        /// Метод запуска работы ГА
        /// </summary>
        /// <returns></returns>
        public Individual RunGA()
        {
            var bestIndivid = new Individual { determinant = double.MinValue };
            var currentGeneration = GenerateFirstGeneration();
            var list = new List<Individual>();
            var generationWithoutProgressCounter = 0;
            for (int i = 0; i < generationCount; ++i)
            {
                // selection, crossing and mutation
                currentGeneration = selectionType(currentGeneration, individualCount, enableElitism);
                currentGeneration = crossingType(currentGeneration);
                currentGeneration = mutationType(currentGeneration, mutationPercent);

                // sort i-generation to get best and check stopAfterNGenerations
                var sortedCurrentGeneration = Individual.MergeSort(currentGeneration);
                if (stopAfterNGenerations == true)
                {
                    if (bestIndivid.determinant > sortedCurrentGeneration[sortedCurrentGeneration.Count - 1].determinant)
                    {
                        generationWithoutProgressCounter++;
                    }
                    else
                    {
                        generationWithoutProgressCounter = 0;
                        bestIndivid = sortedCurrentGeneration[sortedCurrentGeneration.Count - 1];
                    }
                }
                if (generationWithoutProgressCounter == 10)
                {
                    Console.WriteLine($"GA was stopped at {i}-generation due to the lack of improvements in the characteristics of individuals");
                    return bestIndivid;
                }
                Console.WriteLine($"Generation {i}, best determinant = {bestIndivid.determinant}");
                Console.WriteLine("____________________________");
            }
            Console.WriteLine("____________________________");
            return bestIndivid;
        }

        /// <summary>
        /// Метод генерации первого поколения особей(индивидов)
        /// </summary>
        /// <returns></returns>
        private List<Individual> GenerateFirstGeneration()
        {
            var vectors = MatrixRandom(elementInVector, vectorsAmount);
            var individualsList = new List<Individual>();
            for (int i = 0; i < individualCount * 2; i++)
            {
                var squareMatrix = GetSquareMatrix(vectors, elementInVector, vectorsAmount);
                var det = GetDeterminant(squareMatrix);
                individualsList.Add(new Individual { matrix = squareMatrix, determinant = det });
            }
            return individualsList;
        }
    }
}
