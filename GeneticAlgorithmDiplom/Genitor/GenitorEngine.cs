using static GeneticAlgorithmDiplom.MatrixOperations;

namespace GeneticAlgorithmDiplom.Genitor
{
    public class GenitorEngine
    {
        public FitnessFunction fitnessFunction { get; set; }
        public long generationCount { get; set; } // Кол-во поколений
        public int individualCount { get; set; } // Кол-во индивидов в поколении
        public Func<List<Individual>, List<Individual>> selectionType { get; set; } // Тип селекции
        public Func<List<Individual>, Individual> crossingType { get; set; } // Тип скрещивания
        public Func<Individual, double, Individual> mutationType { get; set; } // Тип мутации
        public bool useMutation { get; set; } // Использовать мутацию
        public double mutationPercent { get; set; } // Как часто происходит мутация
        public bool stopAfterNGenerations { get; set; } // Прекратить работу в случае, если в течение длительного периода не наблюдается улучшения характеристик особей в поколении
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
                             Func<List<Individual>, Individual> crossingType,
                             Func<Individual, double, Individual> mutationType,
                             bool useMutation,
                             double mutationPercent,
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
            this.stopAfterNGenerations = stopAfterNGenerations;
            this.elementInVector = elementInVector;
            this.vectorsAmount = vectorsAmount;
        }

        /// <summary>
        /// Метод запуска работы Генитора
        /// </summary>
        /// <returns></returns>
        public void RunGenitor()
        {
            var currentGeneration = GenerateFirstGeneration();
            for (int i = 0; i < generationCount; ++i)
            {
                var twoParents = selectionType(currentGeneration);
                var child = crossingType(twoParents);
                var childAfterMutation = mutationType(child, mutationPercent);
                currentGeneration = Individual.MergeSort(currentGeneration);
                if (childAfterMutation.Determinant > currentGeneration[0].Determinant)
                {
                    currentGeneration.RemoveAt(0);
                    currentGeneration.Add(childAfterMutation);
                }
                currentGeneration = Individual.MergeSort(currentGeneration);
                fitnessFunction.Fitness(currentGeneration[currentGeneration.Count - 1], i);
                Console.WriteLine($"Generation {i}, best determinant = {currentGeneration[currentGeneration.Count - 1].Determinant}");
                Console.WriteLine("____________________________");
            }
        }

        /// <summary>
        /// Метод генерации первого поколения особей(индивидов)
        /// </summary>
        /// <returns>Возвращает сгенерированный список особей</returns>
        private List<Individual> GenerateFirstGeneration()
        {
            //var vectors = MatrixRandom(elementInVector, vectorsAmount);
            var vectors = MatrixRandomOneMinusOne(elementInVector, vectorsAmount);

            var individualsList = new List<Individual>();
            for (int i = 0; i < individualCount; i++)
            {
                var squareMatrix = GetSquareMatrix(vectors, elementInVector, vectorsAmount);
                var det = GetDeterminant(squareMatrix);
                individualsList.Add(new Individual { Matrix = squareMatrix, Determinant = det });
            }
            return individualsList;
        }
    }
}

