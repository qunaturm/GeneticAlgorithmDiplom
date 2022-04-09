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
            var matrix = MatrixOperations.MatrixRandom(5, 2);
            var squareMatrix = MatrixOperations.GetSquareMatrix(matrix, 5, 2);
            double[] rowToSwap = new double[] { 99, 99 };
            MatrixOperations.SwapRows(ref squareMatrix, rowToSwap, 0);
            squareMatrix[0][0].Should().Be(99);
            squareMatrix[0][1].Should().Be(99);
        }

        [Fact]
        public void SwapTwoColls()
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

            MatrixOperations.SwapColls(ref m1, ref m2, 1);
            for (int i = 0; i < 3; ++i)
            {
                m1[i][1].Should().Be(2);
                m2[i][1].Should().Be(1);
            }
        }

        [Fact]
        public void SwapTwoCollsInOneMatrix()
        {
            double[][] m1 = MatrixOperations.CreateMatrix(3, 3);

            m1[0][0] = 1;
            m1[0][1] = 2;
            m1[0][2] = 3;

            m1[1][0] = 1;
            m1[1][1] = 2;
            m1[1][2] = 3;

            m1[2][0] = 1;
            m1[2][1] = 2;
            m1[2][2] = 3;

            MatrixOperations.SwapColls(ref m1, 0, 2);
            for (int i = 0; i < 3; ++i)
            {
                m1[i][0].Should().Be(3);
                m1[i][2].Should().Be(1);
            }
        }

        [Fact]
        public void GetDeterminant3x3()
        {
            var matrix = MatrixOperations.MatrixRandom(7, 3);
            var squareMatrix = MatrixOperations.GetSquareMatrix(matrix, 7, 3);
            double[] row1ToSwap = new double[] { 4, 8, 12 };
            double[] row2ToSwap = new double[] { 16, 29, 98 };
            double[] row3ToSwap = new double[] { 63, 11, 74 };

            MatrixOperations.SwapRows(ref squareMatrix, row1ToSwap, 0);
            MatrixOperations.SwapRows(ref squareMatrix, row2ToSwap, 1);
            MatrixOperations.SwapRows(ref squareMatrix, row3ToSwap, 2);
            var determinant = MatrixOperations.GetDeterminant(squareMatrix);
            Math.Round(determinant).Should().Be(24380);
        }

        [Fact]
        public void GetDeterminant3x3_v2()
        {
            double[][] m = MatrixOperations.CreateMatrix(3, 3);

            m[0][0] = 0;
            m[0][1] = 1;
            m[0][2] = 1;

            m[1][0] = 0;
            m[1][1] = 0;
            m[1][2] = 0;

            m[2][0] = 0;
            m[2][1] = 1;
            m[2][2] = 0;

            var det = MatrixOperations.GetDeterminant(m);
            det.Should().BeInRange(0, 0.1);
        }

        [Fact]
        public void GetDeterminant4x4()
        {
            var matrix = MatrixOperations.MatrixRandom(9, 4);
            var squareMatrix = MatrixOperations.GetSquareMatrix(matrix, 9, 4);
            double[] row1ToSwap = new double[] { 4, 8, 12, 55};
            double[] row2ToSwap = new double[] { 16, 29, 98, 3 };
            double[] row3ToSwap = new double[] { 63, 11, 74, 49 };
            double[] row4ToSwap = new double[] { 91, 33, 2, 86 };


            MatrixOperations.SwapRows(ref squareMatrix, row1ToSwap, 0);
            MatrixOperations.SwapRows(ref squareMatrix, row2ToSwap, 1);
            MatrixOperations.SwapRows(ref squareMatrix, row3ToSwap, 2);
            MatrixOperations.SwapRows(ref squareMatrix, row4ToSwap, 3);
            var determinant = MatrixOperations.GetDeterminant(squareMatrix);
            Math.Round(determinant).Should().Be(-13575088);
        }

        [Fact]
        public void GetDeterminant7x7()
        {
            var matrix = MatrixOperations.MatrixRandom(20, 7);
            var squareMatrix = MatrixOperations.GetSquareMatrix(matrix, 20, 7);
            double[] row1ToSwap = new double[] { 1, 2, 1, 1, 1, 1, 1 };
            double[] row2ToSwap = new double[] { 1, 1, 2, 1, 1, 1, 1 };
            double[] row3ToSwap = new double[] { 1, 1, 1, 2, 1, 1, 1 };
            double[] row4ToSwap = new double[] { 1, 1, 1, 1, 2, 1, 1 };
            double[] row5ToSwap = new double[] { 1, 1, 1, 1, 1, 2, 1 };
            double[] row6ToSwap = new double[] { 1, 1, 1, 1, 1, 1, 2 };
            double[] row7ToSwap = new double[] { 2, 1, 1, 1, 1, 1, 1 };

            MatrixOperations.SwapRows(ref squareMatrix, row1ToSwap, 0);
            MatrixOperations.SwapRows(ref squareMatrix, row2ToSwap, 1);
            MatrixOperations.SwapRows(ref squareMatrix, row3ToSwap, 2);
            MatrixOperations.SwapRows(ref squareMatrix, row4ToSwap, 3);
            MatrixOperations.SwapRows(ref squareMatrix, row5ToSwap, 4);
            MatrixOperations.SwapRows(ref squareMatrix, row6ToSwap, 5);
            MatrixOperations.SwapRows(ref squareMatrix, row7ToSwap, 6);

            var determinant = MatrixOperations.GetDeterminant(squareMatrix);
            Math.Round(determinant).Should().Be(8);
        }
    }
}
