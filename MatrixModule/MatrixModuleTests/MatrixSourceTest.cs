using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MatrixModule;
using Xunit;

namespace MatrixModuleTests;

public class MatrixSourceTest
{
    [Fact]
    private void InitTest()
    {
        var source = new MatrixSource(
            new List<IEnumerable<double>>
            {
                new double[]
                {
                    1,
                    2,
                    3
                },
                new double[]
                {
                    4,
                    5,
                    6
                }
            });
        var rowOutOfRange = () => source[0][4];
        rowOutOfRange.Should().Throw<ArgumentOutOfRangeException>();
        var colsOutOfRange = () => source[4][0];
        colsOutOfRange.Should().Throw<ArgumentOutOfRangeException>();
        source[0][0].Should().Be(1);
        source[1][1].Should().Be(5);
    }

    [Fact]
    private void EnumeratorTest()
    {
        var source = new MatrixSource(
            new List<IEnumerable<double>>
                { new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 7, 8, 9 } }
        );

        var plain = source.SelectMany(u => u).ToArray();
        plain.Should().BeEquivalentTo(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }

    [Fact]
    private void RandomTest()
    {
        var source = new MatrixSource(
            new List<IEnumerable<double>>
                { new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 7, 8, 9 } }
        );
        
        var indexes = source.GetRandomIndexes(100,100).Distinct().ToList();
        indexes.Count().Should().Be(100);
        indexes.Select(u => indexes.Count(p => p == u)).Count(u => u != 1).Should().Be(0);
    }
    [Fact]
    private void RandomTest2()
    {
        var source = new MatrixSource(
            new List<IEnumerable<double>>
                { new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 7, 8, 9 } }
        );
        
        var indexes = source.GetRandomIndexes(100,10000).Distinct().ToList();
        indexes.Count().Should().Be(100);
        indexes.Select(u => indexes.Count(p => p == u)).Count(u => u != 1).Should().Be(0);
    }
}