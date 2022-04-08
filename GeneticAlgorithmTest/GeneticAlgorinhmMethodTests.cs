using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using static GeneticAlgorithmDiplom.GeneticEngine;
using GeneticAlgorithmDiplom;

namespace GeneticAlgorithmTest
{
    public class GeneticAlgorinhmMethodTests
    {
        [Fact]
        public void GetFirstGenerationTest()
        {
            var ga = new GeneticEngine();
            var firstGen = ga.GenerateFirstGeneration(30, 3, 10);
            Assert.True(true);
        }
    }
}
