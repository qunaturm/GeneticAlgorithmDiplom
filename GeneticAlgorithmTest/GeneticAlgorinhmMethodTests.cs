﻿using Xunit;
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
                         16,
                         SelectionType.Tourney,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var bestParents = ga.SelectionProcess(firstGeneration, 4);
            bestParents.Count.Should().Be(4);
        }

        [Fact]
        public void CopyColumn()
        {
            double[][] m1 = MatrixOperations.CreateMatrix(3, 3);

            m1[0][0] = 1;
            m1[0][1] = 1;
            m1[0][2] = 1;

            m1[1][0] = 1;
            m1[1][1] = 1;
            m1[1][2] = 1;

            m1[2][0] = 1;
            m1[2][1] = 1;
            m1[2][2] = 1;

            double[][] m2 = MatrixOperations.CreateMatrix(3, 3);

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
            double[][] m1 = MatrixOperations.CreateMatrix(3, 3);

            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Tourney,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var bestParents = ga.SelectionProcess(firstGeneration, 4);
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
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var bestParents = ga.SelectionProcess(firstGeneration, 8);
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
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var children = ga.SelectionProcess(firstGeneration, 8);
            children.Count.Should().Be(8);
        }

        [Fact]
        public void MutationShuffling()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Tourney,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var children = ga.SelectionProcess(firstGeneration, 8);
            var mutated = ga.MutationProces(children);
            mutated.Count.Should().Be(8);
        }
        [Fact]
        public void MutationApproximation()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Tourney,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var bestParents = ga.SelectionProcess(firstGeneration, 8);
            var children = ga.CrossingProcess(bestParents);
            var mutated = ga.MutationProces(children);
            mutated.Count.Should().Be(8);
        }

        [Fact]
        public void SelectionRouletteWheel()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Roulette_Wheel,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var bestParents = ga.SelectionProcess(firstGeneration, 8);
            var children = ga.CrossingProcess(bestParents);
            var mutated = ga.MutationProces(children);
            mutated.Count.Should().Be(8);
        }

        [Fact]
        public void SelectionTourneyElitism()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Tourney  ,            
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var bestParents = ga.SelectionProcess(firstGeneration, 8);
            var children = ga.CrossingProcess(bestParents);
            var mutated = ga.MutationProces(children);
            mutated.Count.Should().Be(8);
        }

        [Fact]
        public void SelectionRouletteWheelElitism()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Roulette_Wheel,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            var firstGeneration = ga.GenerateFirstGeneration();
            var bestParents = ga.SelectionProcess(firstGeneration, 8);
            var children = ga.CrossingProcess(bestParents);
            var mutated = ga.MutationProces(children);
            mutated.Count.Should().Be(8);
        }

        [Fact]
        public void Run()
        {
            var fitnessFunction = new FitnessFunction();
            var ga = new GeneticEngine(
                         fitnessFunction,
                         100,
                         16,
                         SelectionType.Roulette_Wheel,
                         CrossingType.Shuffler_Crossover,
                         MutationType.ApproximateMutation,
                         true,
                         0.2,
                         true,
                         90,
                         7);
            ga.RunGA();
        }
    }
}
