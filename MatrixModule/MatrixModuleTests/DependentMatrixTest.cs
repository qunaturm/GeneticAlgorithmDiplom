using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MatrixModule;
using Microsoft.VisualBasic;
using Xunit;

namespace MatrixModuleTests;

public class DependentMatrixTest
{
    [Fact]
    private void InitTest()
    {
        var depMatrix = new DependentMatrix(
            new List<IEnumerable<double>>
                { new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 7, 8, 9 } }
            );

        var plain = depMatrix.SelectMany(u => u).ToArray();
        plain.Should().BeEquivalentTo(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }
    [Fact]
    private void EnumeratorTest()
    {
        var depMatrix = new DependentMatrix(
            new List<IEnumerable<double>>
                { new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 7, 8, 9 } }
        );

        var plain = depMatrix.SelectMany(u => u).ToArray();
        plain.Should().BeEquivalentTo(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }
    
    [Fact]
    private void CopyMatrixTest()
    {
        var depMatrix = new DependentMatrix(
            new List<IEnumerable<double>>
                { new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 7, 8, 9 } }
        );

        var plain = depMatrix.GetCopyMatrix().SelectMany(u => u).ToArray();
        plain.Should().BeEquivalentTo(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }
    
    [Fact]
    private void CopyRowMajorMatrixTest()
    {
        var depMatrix = new DependentMatrix(
            new List<IEnumerable<double>>
                { new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 7, 8, 9 } }
        );

        var plain = depMatrix.GetRowMajorCopyMatrix().SelectMany(u => u).ToArray();
        plain.Should().BeEquivalentTo(new double[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 });
    }
    
    [Fact]
    private void GetDeterminant3X3()
    {
        var depMatrix = new DependentMatrix(
            new List<IEnumerable<double>>
                { new double[] { 4, 16, 63 }, new double[] { 8, 29, 11 }, new double[] { 12, 98, 74 } }
        );

        Math.Round(depMatrix.Determinant).Should().Be(24380);
    }
}