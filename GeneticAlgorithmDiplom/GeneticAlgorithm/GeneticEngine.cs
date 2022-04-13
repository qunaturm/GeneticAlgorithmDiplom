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
        public Func<List<Individual>, List<Individual>?, double, List<Individual>> mutationType { get; set; } // Тип мутации
        public bool useMutation { get; set; } // Использовать мутацию
        public double mutationPercent { get; set; } // Как часто происходит мутация
        public bool enableElitism { get; set; } // Включить элитарность
        public bool stopAfterNGenerations { get; set; } // Прекратить работу в случае, если в течение длительного периода не наблюдается улучшения характеристик особей в поколении
        public int elementInVector { get; set; }
        public int vectorsAmount { get; set; }

        /// <summary>
        /// Инициализация движка ГА
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
        /// <param name="stopAfterNGenerations">Прекратить работу в случае, если в течение длительного периода не наблюдается улучшения характеристик особей в поколении</param>
        /// <param name="elementInVector">Количество столбцов</param>
        /// <param name="vectorsAmount">Количество строк</param>
        public GeneticEngine(FitnessFunction fitnessFunction,
                             long generationCount,
                             int individualCount,
                             Func<List<Individual>, int, bool, List<Individual>> selectionType,
                             Func<List<Individual>, List<Individual>> crossingType,
                             Func<List<Individual>, List<Individual>?, double, List<Individual>> mutationType,
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
        /// Метод запуска работы ГА. Останавливается либо по достижению предельного числа поколений,
        /// либо по отсутствию улучшений за n-поколений
        /// </summary>
        /// <returns>Возвращает лучшего полученного индивина</returns>
        public void RunGA()
        {
            var firstGeneration =  GenerateFirstGeneration();
            var currentGeneration = firstGeneration;
            for (int i = 0; i < generationCount; ++i)
            {
                // selection, crossing and mutation
                currentGeneration = selectionType(currentGeneration, individualCount, enableElitism);
                currentGeneration = crossingType(currentGeneration);
                if (mutationType == Mutation.ClassicMutation.Mutator)
                    currentGeneration = mutationType(currentGeneration, firstGeneration, mutationPercent);
                else currentGeneration = mutationType(currentGeneration, null, mutationPercent);


                // sort i-generation to get best
                var sortedCurrentGeneration = Individual.MergeSort(currentGeneration);

                fitnessFunction.Fitness(sortedCurrentGeneration[sortedCurrentGeneration.Count - 1], i);

                // if stopAfterNGenerations == true
                if (stopAfterNGenerations == true && fitnessFunction.GenerationWithoutProgressCounter == 10)
                {
                    Console.WriteLine($"GA was stopped at {i}-generation due to the lack of improvements in the characteristics of individuals");
                    break;
                }
                Console.WriteLine($"Generation {i}, best determinant = {sortedCurrentGeneration[sortedCurrentGeneration.Count - 1].Determinant}");
                Console.WriteLine("____________________________");
            }
            Console.WriteLine("____________________________");
        }

        /// <summary>
        /// Метод генерации первого поколения особей(индивидов)
        /// </summary>
        /// <returns></returns>
        private List<Individual> GenerateFirstGeneration()
        {
            var vectors = MatrixRandom(elementInVector, vectorsAmount);
            var individualsList = new List<Individual>();
            for (int i = 0; i < individualCount * 5; i++)
            {
                var squareMatrix = GetSquareMatrix(vectors, elementInVector, vectorsAmount);
                var det = GetDeterminant(squareMatrix);
                individualsList.Add(new Individual { Matrix = squareMatrix, Determinant = det });
            }
            return individualsList;
        }
    }
}
