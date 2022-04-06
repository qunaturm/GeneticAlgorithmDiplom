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
    public class VectorsTests
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

        [Fact]
        public void GetDeterminant2x2()
        {
            var vectors = new Matrix(4, 2);
            vectors.FillVectors();
            var squareMatrix = Matrix.GetSquareMatrix(vectors);
            int[] rowToSwap = new int[] { 99, 99 };
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 0);
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 1);
            var determinant = Matrix.GetDeterminant(squareMatrix);
            determinant.Should().Be(0);
        }

        [Fact]
        public void GetDeterminant3x3()
        {
            var vectors = new Matrix(7, 3);
            vectors.FillVectors();
            var squareMatrix = Matrix.GetSquareMatrix(vectors);
            int[] row1ToSwap = new int[] { 4, 8, 12 };
            int[] row2ToSwap = new int[] { 16, 29, 98 };
            int[] row3ToSwap = new int[] { 63, 11, 74 };

            Matrix.SwapTwoRows(ref squareMatrix, row1ToSwap, 0);
            Matrix.SwapTwoRows(ref squareMatrix, row2ToSwap, 1);
            Matrix.SwapTwoRows(ref squareMatrix, row3ToSwap, 2);
            var determinant = Matrix.GetDeterminant(squareMatrix);
            determinant.Should().Be(24380);
        }

        [Fact]
        public void GetDeterminant4x4()
        {
            var vectors = new Matrix(7, 4);
            vectors.FillVectors();
            var squareMatrix = Matrix.GetSquareMatrix(vectors);
            int[] rowToSwap = new int[] { 1, 4, 8, 12 };
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 0);
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 1);
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 2);
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 3);
            var determinant = Matrix.GetDeterminant(squareMatrix);
            determinant.Should().Be(0);
        }
    }
}
