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
                    if (duplicatedMatrix.matrix[i, j] > columnMax)
                    {
                        columnMax = duplicatedMatrix.matrix[i, j];
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
                    duplicatedMatrix.matrix[i, j] /= duplicatedMatrix.matrix[j, j];
                    for (int k = j + 1; k < size; ++k)
                    {
                        duplicatedMatrix.matrix[i, k] -= duplicatedMatrix.matrix[i, j] * duplicatedMatrix.matrix[j, k];
                    }
                }
            }
            return duplicatedMatrix;
        }

        /// <summary>
        /// Находит массив x, который при умножении на матрицу LU дает массив b.
        /// Решает систему уравнений.
        /// </summary>
        /// <param name="luMatrix"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int[] HelperSolve(Matrix luMatrix, int[] b)
        {
            int size = luMatrix.rows;
            int[] x = new int[size];
            b.CopyTo(x, 0);
            for (int i = 1; i < size; ++i)
            {
                int sum = x[i];
                for (int j = 0; j < i; ++j)
                {
                    sum -= luMatrix.matrix[i, j] * x[j];
                    x[i] = sum;
                }
            }
            x[size - 1] /= luMatrix.matrix[size - 1, size - 1];
            for (int i = size - 2; i >= 0; --i)
            {
                int sum = x[i];
                for (int j = i + 1; j < size; ++j)
                {
                    sum -= luMatrix.matrix[i, j] * x[j];
                }
                x[i] = sum / luMatrix.matrix[i, i];
            }
            return x;
        }

        /// <summary>
        /// Алгоритм обращения/ Результат перемножения матрицы M и ее обращения 
        /// является единичной матрицей. Метод MatrixInverse в основном отвечает за решение 
        /// системы уравнений Ax = b, где A — матрица разложения LU , а константы b равны 
        /// либо 1, либо 0 и соответствуют единичной матрице. Заметьте, что MatrixInverse 
        /// использует массив perm, возвращаемый после вызова MatrixDecompose.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Matrix Inverse(Matrix m)
        {
            int size = m.rows;
            var result = new Matrix(m.rows, m.columns);
            for (int row = 0; row < m.rows; ++row)
            {
                for (int column = 0; column < m.columns; ++column)
                {
                    result.matrix[row, column] = m.matrix[row, column];
                }
            }
            int[] perm;
            int toggle;
            var lum = Matrix.Decompose(m, out perm, out toggle);
/*            if (lum == null)
                throw new Exception("Unable to compute inverse");*/
            int[] b = new int[size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1;
                    else
                        b[j] = 0;
                }
                int[] x = Matrix.HelperSolve(lum, b);
                for (int j = 0; j < size; ++j)
                    result[j][i] = x[j];
            }
            return result;
        }

        /// <summary>
        /// Определитель матрицы — это просто результат перемножения значений 
        /// на основной диагонали матрицы разложения LUP со знаком «плюс» или 
        /// «минус» в зависимости от значения toggle.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int GetDeterminant(Matrix m)
        {
            int[] perm;
            int toggle;
            Matrix lum = Matrix.Decompose(m, out perm, out toggle);
            /*if (lum == null)
                throw new Exception("Unable to compute MatrixDeterminant"); */
            int result = toggle;
            for (int i = 0; i < lum.rows; ++i)
                result *= lum.matrix[i, i];
            return result;
        }
    }
}
