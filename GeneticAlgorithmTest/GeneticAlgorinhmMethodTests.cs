using Xunit;
using FluentAssertions;
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
            firstGen.Count.Should().Be(10);
        }
    }
}
