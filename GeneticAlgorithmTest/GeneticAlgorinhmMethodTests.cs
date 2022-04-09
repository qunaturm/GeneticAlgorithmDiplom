using Xunit;
using FluentAssertions;
using GeneticAlgorithmDiplom;
using System.Collections.Generic;

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
        public void Constructor()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         10,
                         SelectionType.Tourney,
                         CrossingType.One_Point_Crossover,
                         MutationType.ExchangeMutation,
                         true,
                         0.5,
                         30,
                         3);
            var bestParents = ga.SelectionProcess(4);
            bestParents.Count.Should().Be(4);
        }

        [Fact]
        public void CopyColumn()
        {
            double[][] m1 = Matrix.CreateMatrix(3, 3);

            m1[0][0] = 1;
            m1[0][1] = 1;
            m1[0][2] = 1;

            m1[1][0] = 1;
            m1[1][1] = 1;
            m1[1][2] = 1;

            m1[2][0] = 1;
            m1[2][1] = 1;
            m1[2][2] = 1;

            double[][] m2 = Matrix.CreateMatrix(3, 3);

            m2[0][0] = 2;
            m2[0][1] = 2;
            m2[0][2] = 2;

            m2[1][0] = 2;
            m2[1][1] = 2;
            m2[1][2] = 2;

            m2[2][0] = 2;
            m2[2][1] = 2;
            m2[2][2] = 2;

            var res = GeneticEngine.CopyColumn(m1, m2, 1);
        }

        [Fact]
        public void CrossingProcessOnePoint()
        {
            double[][] m1 = Matrix.CreateMatrix(3, 3);

            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         10,
                         SelectionType.Tourney,
                         CrossingType.One_Point_Crossover,
                         MutationType.ExchangeMutation,
                         true,
                         0.5,
                         30,
                         3);
            var bestParents = ga.SelectionProcess(4);
            var children = ga.CrossingProcess(bestParents);
            children.Count.Should().Be(4);
        }

        [Fact]
        public void CrossingProcessTwoPoint()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Tourney,
                         CrossingType.Two_Point_Crossover,
                         MutationType.ExchangeMutation,
                         true,
                         0.5,
                         90,
                         7);
            var bestParents = ga.SelectionProcess(8);
            var children = ga.CrossingProcess(bestParents);
            children.Count.Should().Be(8);

        }

        [Fact]
        public void CrossingShuffler()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Tourney,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ExchangeMutation,
                         true,
                         0.5,
                         90,
                         7);
            var bestParents = ga.SelectionProcess(8);
            var children = ga.CrossingProcess(bestParents);
            children.Count.Should().Be(8);

        }
    }
}
