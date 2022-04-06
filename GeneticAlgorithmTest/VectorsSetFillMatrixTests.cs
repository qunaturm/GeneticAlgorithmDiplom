using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using GeneticAlgorithmDiplom;

namespace GeneticAlgorithmTest
{
    public class VectorsSetFillMatrixTests
    {
        [Fact]
        public void SwapTwoRows()
        {
            var vectors = new Matrix(4, 2);
            vectors.FillVectors();
            var squareMatrix = Matrix.GetSquareMatrix(vectors);
            int[] rowToSwap = new int[]{ 99, 99 };
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 0);
            squareMatrix.matrix[0, 0].Should().Be(99);
            squareMatrix.matrix[0, 1].Should().Be(99);
        }

        [Fact]
        public void GetRowFromMatrix()
        {
            var vectors = new Matrix(4, 2);
            vectors.FillVectors();
            var squareMatrix = Matrix.GetSquareMatrix(vectors);
            int[] rowToSwap = new int[] { 99, 99 };
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 0);
            var row = squareMatrix[0];
            row[0].Should().Be(99);
            row[1].Should().Be(99);
        }
    }
}
