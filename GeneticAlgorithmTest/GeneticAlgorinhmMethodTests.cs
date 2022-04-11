using Xunit;
using FluentAssertions;
using GeneticAlgorithmDiplom;
using System.Collections.Generic;
using GeneticAlgorithmDiplom.GeneticAlgorithm;

namespace GeneticAlgorithmTest
{
    public class GeneticAlgorinhmMethodTests
    {
        [Fact]
        public void MergeSort()
        {
            var individ1 = new Individual { determinant = 3 };
            var individ2 = new Individual { determinant = -1 };
            var individ3 = new Individual { determinant = 2};
            var individ4 = new Individual { determinant = 6 };
            var individ5 = new Individual { determinant = -8 };

            var list = new List<Individual>(); 
            list.Add(individ1);
            list.Add(individ2);
            list.Add(individ3);
            list.Add(individ4);
            list.Add(individ5);


            var sortedList = Individual.MergeSort(list);
            sortedList[4].determinant.Should().Be(6);
            sortedList[3].determinant.Should().Be(3);
            sortedList[2].determinant.Should().Be(2);
            sortedList[1].determinant.Should().Be(-1);
            sortedList[0].determinant.Should().Be(-8);
        }

        [Fact]
        public void Run()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         GeneticAlgorithmDiplom.GeneticAlgorithm.Selection.Tourney.Selector,
                         GeneticAlgorithmDiplom.GeneticAlgorithm.Crossing.OnePointCrossing.Crossover,
                         GeneticAlgorithmDiplom.GeneticAlgorithm.Mutation.ExchangeMutation.Mutator,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            ga.RunGA();
        }
    }
}
