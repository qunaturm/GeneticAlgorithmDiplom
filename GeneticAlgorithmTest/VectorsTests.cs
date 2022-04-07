using System;
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
            var matrix = Matrix.MatrixRandom(5, 2, 1);
            var squareMatrix = Matrix.GetSquareMatrix(matrix, 3, 2);
            double[] rowToSwap = new double[] { 99, 99 };
            Matrix.SwapTwoRows(ref squareMatrix, rowToSwap, 0);
            squareMatrix[0][0].Should().Be(99);
            squareMatrix[0][1].Should().Be(99);
        }

        [Fact]
        public void GetDeterminant3x3()
        {
            var matrix = Matrix.MatrixRandom(7, 3, 1);
            var squareMatrix = Matrix.GetSquareMatrix(matrix, 7, 3);
            double[] row1ToSwap = new double[] { 4, 8, 12 };
            double[] row2ToSwap = new double[] { 16, 29, 98 };
            double[] row3ToSwap = new double[] { 63, 11, 74 };

            Matrix.SwapTwoRows(ref squareMatrix, row1ToSwap, 0);
            Matrix.SwapTwoRows(ref squareMatrix, row2ToSwap, 1);
            Matrix.SwapTwoRows(ref squareMatrix, row3ToSwap, 2);
            var determinant = Matrix.GetDeterminant(squareMatrix);
            Math.Round(determinant).Should().Be(24380);
        }

        [Fact]
        public void GetDeterminant3x3_TestMatrix()
        {
            double[][] m = MatrixExample.MatrixExample.MatrixCreate(3, 3);

            m[0][0] = 4;
            m[0][1] = 8;
            m[0][2] = 12;

            m[1][0] = 16;
            m[1][1] = 29;
            m[1][2] = 98;

            m[2][0] = 63;
            m[2][1] = 11;
            m[2][2] = 74;

            var det = MatrixExample.MatrixExample.MatrixDeterminant(m);
            det.Should().BeInRange(24380, 24381);
        }

        [Fact]
        public void GetDeterminant4x4()
        {
            var matrix = Matrix.MatrixRandom(9, 4, 1);
            var squareMatrix = Matrix.GetSquareMatrix(matrix, 9, 4);
            double[] row1ToSwap = new double[] { 4, 8, 12, 55};
            double[] row2ToSwap = new double[] { 16, 29, 98, 3 };
            double[] row3ToSwap = new double[] { 63, 11, 74, 49 };
            double[] row4ToSwap = new double[] { 91, 33, 2, 86 };


            Matrix.SwapTwoRows(ref squareMatrix, row1ToSwap, 0);
            Matrix.SwapTwoRows(ref squareMatrix, row2ToSwap, 1);
            Matrix.SwapTwoRows(ref squareMatrix, row3ToSwap, 2);
            Matrix.SwapTwoRows(ref squareMatrix, row4ToSwap, 3);
            var determinant = Matrix.GetDeterminant(squareMatrix);
            Math.Round(determinant).Should().Be(-13575088);
        }

        [Fact]
        public void GetDeterminant7x7()
        {
            var matrix = Matrix.MatrixRandom(20, 7, 1);
            var squareMatrix = Matrix.GetSquareMatrix(matrix, 20, 7);
            double[] row1ToSwap = new double[] { 1, 2, 1, 1, 1, 1, 1 };
            double[] row2ToSwap = new double[] { 1, 1, 2, 1, 1, 1, 1 };
            double[] row3ToSwap = new double[] { 1, 1, 1, 2, 1, 1, 1 };
            double[] row4ToSwap = new double[] { 1, 1, 1, 1, 2, 1, 1 };
            double[] row5ToSwap = new double[] { 1, 1, 1, 1, 1, 2, 1 };
            double[] row6ToSwap = new double[] { 1, 1, 1, 1, 1, 1, 2 };
            double[] row7ToSwap = new double[] { 2, 1, 1, 1, 1, 1, 1 };

            Matrix.SwapTwoRows(ref squareMatrix, row1ToSwap, 0);
            Matrix.SwapTwoRows(ref squareMatrix, row2ToSwap, 1);
            Matrix.SwapTwoRows(ref squareMatrix, row3ToSwap, 2);
            Matrix.SwapTwoRows(ref squareMatrix, row4ToSwap, 3);
            Matrix.SwapTwoRows(ref squareMatrix, row5ToSwap, 4);
            Matrix.SwapTwoRows(ref squareMatrix, row6ToSwap, 5);
            Matrix.SwapTwoRows(ref squareMatrix, row7ToSwap, 6);

            var determinant = Matrix.GetDeterminant(squareMatrix);
            Math.Round(determinant).Should().Be(8);
        }
    }
}
