using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace GeneticAlgorithmDiplom
{
    public class Matrix
    {
        public int columns { get; set; } // elements in each vector
        public int rows { get; set; } // vectors amount 
        public int[,] matrix = null;

        public Matrix(int vectorsAmount, int elementsInVector)
        {
            columns = vectorsAmount;
            rows = elementsInVector;
            matrix = new int[columns, elementsInVector];
        }

        #region [chore]
        public void FillVectors()
        {
            Random random = new Random();
            for (int i = 0; i < columns; ++i)
            {
                for (int j = 0; j < rows; ++j)
                {
                    matrix[i, j] = random.Next(1, 100);
                }
            }
        }

        public int this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }

        public int[] this[int x]
        {
            get
            {
                int[] row = new int[rows];
                for (int i = 0; i < rows; ++i)
                {
                    for (int j = 0; j < columns; ++j)
                    {
                        row[j] = matrix[x, j];
                    }
                }
                return row;
            }
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            if (matrix == null) return ret.ToString();

            for (int i = 0; i < rows; i++)
            {
                for (int t = 0; t < columns; t++)
                {
                    ret.Append(matrix[i, t]);
                    ret.Append("\t");
                }
                ret.Append("\n");
            }
            return ret.ToString();
        }

        public static void PrintMatrix(Matrix m)
        {
            for (int row = 0; row < m.rows; ++row)
            {
                for (int column = 0; column < m.columns; ++column)
                {
                    Console.Write($"{m.matrix[row, column]}  ");
                }
                Console.WriteLine();
            }
        }
        #endregion

        public int[] getRow(int rowNumber)
        {
            int[] row = new int[rows];
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    row[j] = matrix[rowNumber, j];
                }
            }
            return row;
        }

        public static void SwapTwoRows(ref Matrix m, int[] newRow, int index)
        {
            for (int i = 0; i < m.columns; ++i)
            {
                m.matrix[index, i] = newRow[i];
            }
        }

        public static Matrix GetSquareMatrix(Matrix m)
        {
            var squareMetrixSize = m.rows;
            var random = new Random();
            int[] usedVectors = new int[squareMetrixSize];

            // eliminate repeat
            for (int i = 0; i < squareMetrixSize; ++i)
            {
                var nextVector = random.Next(0, m.columns);
                while (usedVectors.Contains(nextVector))
                {
                    nextVector = random.Next(0, m.columns);
                }
                usedVectors[i] = nextVector;
            }
            var squareMatrix = new Matrix(squareMetrixSize, squareMetrixSize);
            for (int row = 0; row < squareMetrixSize; ++row)
            {
                for (int column = 0; column < squareMetrixSize; ++column)
                {
                    squareMatrix[row, column] = m.matrix[usedVectors[row], column];
                }
            }
            return squareMatrix;
        }

        /// <summary>
        /// LUP. Принимает квадратную матрицу и возвращает три значения. Явно 
        /// возвращается переставленная матрица LU. Остальные два значения возвращаются через выходные параметры.
        /// Один из них является массивом перестановки (permutation array), где хранится информация о том, как 
        /// переставлены строки. Второй выходной параметр — переключатель, принимающий значение либо +1, 
        /// либо –1 в зависимости от того, было количество перестановок четным (+1) или нечетным (–1).
        /// </summary>
        /// <param name="m">Матрица</param>
        /// <param name="perm">Массив перестановки</param>
        /// <param name="toggle">Переключатель</param>
        /// <returns></returns>
        public static Matrix Decompose(Matrix m, out int[] perm, out int toggle)
        {
            int size = m.rows;
            var duplicatedMatrix = new Matrix(m.rows, m.columns);
            for (int row = 0; row < m.rows; ++row)
            {
                for (int column = 0; column < m.columns; ++column)
                {
                    duplicatedMatrix.matrix[row, column] = m.matrix[row, column];
                }    
            }

            perm = new int[size];
            for (int i = 0; i < size; ++i)
            {
                perm[i] = i;
            }
            toggle = 1;
            for (int j = 0; j < size - 1; ++j)
            {
                int columnMax = Math.Abs(duplicatedMatrix[j, j]);
                int pRow = j;
                for (int i = j + 1; i < size; ++i)
                {
                    if (duplicatedMatrix[i, j] > columnMax)
                    {
                        columnMax = duplicatedMatrix[i, j];
                        pRow = i;
                    }
                }
                if (pRow != j)
                {
                    int[] rowPtr = duplicatedMatrix[pRow];
                    SwapTwoRows(ref duplicatedMatrix, duplicatedMatrix[j], pRow);
                    SwapTwoRows(ref duplicatedMatrix, rowPtr, j);
                    int tmp = perm[pRow];
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle;
                }
                if (Math.Abs(duplicatedMatrix.matrix[j, j]) < 1.0E-20)
                {
                    return null;
                }
                for (int i = j + 1; i < size; ++i)
                {
                    duplicatedMatrix[i, j] /= duplicatedMatrix[j, j];
                    for (int k = j + 1; k < size; ++k)
                    {
                        duplicatedMatrix[i, k] -= duplicatedMatrix[i, j] * duplicatedMatrix[j, k];
                    }
                }
            }
            return duplicatedMatrix;
        }

        public static int[] HelperSolve(Matrix luMatrix, int[] b)
        {
            int size = luMatrix.rows;
            int[] x = new int[size];
        }
    }
}
