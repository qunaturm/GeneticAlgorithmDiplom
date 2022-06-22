using MatrixModule;
using static OptimizedGeneticAlgorithm.MatrixOperations;

namespace OptimizedGeneticAlgorithm.GeneticAlgorithm
{
    public sealed class GeneticEngine
    {
        public MatrixSource MatrixSource { get; init; }
        public FitnessFunction fitnessFunction { get; set; } // Фитнесс функция
        public long generationCount { get; set; } // Кол-во поколений
        public int individualCount { get; set; } // Кол-во индивидов в поколении
        public Func<List<DependentMatrix>, int, bool, List<DependentMatrix>> selectionType { get; set; } // Тип селекции
        public Func<List<DependentMatrix>, List<DependentMatrix>> crossingType { get; set; } // Тип скрещивания
        public Func<List<DependentMatrix>, List<DependentMatrix>?, double, List<DependentMatrix>> mutationType { get; set; } // Тип мутации
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
        public GeneticEngine(MatrixSource matrixSource,
                             FitnessFunction fitnessFunction,
                             long generationCount,
                             int individualCount,
                             Func<List<DependentMatrix>, int, bool, List<DependentMatrix>> selectionType,
                             Func<List<DependentMatrix>, List<DependentMatrix>> crossingType,
                             Func<List<DependentMatrix>, List<DependentMatrix>?, double, List<DependentMatrix>> mutationType,
                             bool useMutation,
                             double mutationPercent,
                             bool enableElitism,
                             bool stopAfterNGenerations,
                             int elementInVector,
                             int vectorsAmount)
        {
            MatrixSource = matrixSource;
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
            var currentGeneration = GenerateFirstGeneration();
            for (int i = 0; i < generationCount; ++i)
            {
                // selection, crossing and mutation
                currentGeneration = selectionType(currentGeneration, individualCount, enableElitism);
                currentGeneration = crossingType(currentGeneration);
                currentGeneration = mutationType(currentGeneration, null, mutationPercent);

                // sort i-generation to get best
                var currentBestIndividual = currentGeneration.OrderByDescending(u => u.Determinant).FirstOrDefault();

                fitnessFunction.Fitness(currentBestIndividual, i);

                // if stopAfterNGenerations == true
                if (stopAfterNGenerations == true && fitnessFunction.GenerationWithoutProgressCounter == 10)
                {
                    Console.WriteLine($"GA was stopped at {i}-generation due to the lack of improvements in the characteristics of individuals");
                    break;
                }
                Console.WriteLine($"Generation {i}, best determinant = {currentBestIndividual.Determinant}");
                Console.WriteLine("____________________________");
            }
            Console.WriteLine("____________________________");
        }

        /// <summary>
        /// Метод генерации первого поколения особей(индивидов)
        /// </summary>
        /// <returns></returns>
        private List<DependentMatrix> GenerateFirstGeneration()
        {
            var result = new List<DependentMatrix>();
            for (int i = 0; i < individualCount * 5; i++)
            {
                result.Add(MatrixSource.GenerateDependentMatrix());
            }
            return result;
        }
    }
}
