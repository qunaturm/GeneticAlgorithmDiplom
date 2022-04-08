namespace GeneticAlgorithmDiplom
{
    public class Matrix
    {

        public static double[][] CreateMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        #region [chore]
        public static double[][] MatrixRandom(int rows, int cols)
        {
            var minVal = 0;
            var maxVal = 2;  // not included
            Random random = new Random();
            double[][] result = CreateMatrix(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = random.Next(minVal, maxVal);
            return result;
        }

        public static bool MatrixAreEqual(double[][] matrixA, double[][] matrixB, double epsilon)
        {
            int aRows = matrixA.Length;
            int bCols = matrixB[0].Length;
            for (int i = 0; i < aRows; ++i)
                for (int j = 0; j < bCols; ++j)
                    if (Math.Abs(matrixA[i][j] - matrixB[i][j]) > epsilon)
                        return false;
            return true;
        }

        public static void PrintMatrix(double[][] matrix)
        {
            for (int row = 0; row < matrix.Length; ++row)
            {
                for (int column = 0; column < matrix.Length; ++column)
                {
                    Console.Write($"{matrix[row][column]}  ");
                }
                Console.WriteLine();
            }
        }
        #endregion

        public double[] getRow(double[][] matrix, int rowNumber)
        {
            var size = matrix.Length;
            double[] row = new double[size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    row[j] = matrix[rowNumber][j];
                }
            }
            return row;
        }

        public static void SwapTwoRows(ref double[][] matrix, double[] newRow, int index)
        {
            for (int i = 0; i < matrix.Length; ++i)
            {
                matrix[index][i] = newRow[i];
            }
        }

        public static double[][] GetSquareMatrix(double[][] vectors, int vectorRow, int vectorCol)
        {
            var squareMetrixSize = vectorCol;
            var random = new Random();
            int[] usedVectors = new int[squareMetrixSize];
            double[][] result = CreateMatrix(squareMetrixSize, squareMetrixSize);

            // eliminate repeat
            for (int i = 0; i < squareMetrixSize; ++i)
            {
                var nextVector = random.Next(0, vectorRow);
                while (usedVectors.Contains(nextVector))
                {
                    nextVector = random.Next(0, vectorRow);
                }
                usedVectors[i] = nextVector;
            }
            for (int row = 0; row < squareMetrixSize; ++row)
            {
                for (int column = 0; column < squareMetrixSize; ++column)
                {
                    result[row][column] = vectors[usedVectors[row]][column];
                }
            }
            return result;
        }

        public static double[][] MatrixDuplicate(double[][] matrix)
        {
            double[][] result = CreateMatrix(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i)
                for (int j = 0; j < matrix[i].Length; ++j)
                    result[i][j] = matrix[i][j];
            return result;
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
        /// 

        public static double[][] Decompose(double[][] matrix, out int[] perm, out int toggle)
        {
            int size = matrix.Length;
            var duplicatedMatrix = MatrixDuplicate(matrix);
            perm = new int[size];
            for (int i = 0; i < size; ++i)
            {
                perm[i] = i;
            }
            toggle = 1;
            for (int j = 0; j < size - 1; ++j)
            {
                double columnMax = Math.Abs(duplicatedMatrix[j][j]);
                int pRow = j;
                for (int i = j + 1; i < size; ++i)
                {
                    if (duplicatedMatrix[i][j] > columnMax)
                    {
                        columnMax = duplicatedMatrix[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j)
                {
                    double[] rowPtr = duplicatedMatrix[pRow];
                    duplicatedMatrix[pRow] = duplicatedMatrix[j];
                    duplicatedMatrix[j] = rowPtr;
                    int tmp = perm[pRow];
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle;
                }
                for (int i = j + 1; i < size; ++i)
                {
                    double current = duplicatedMatrix[i][j] / duplicatedMatrix[j][j];
                    if (double.IsNaN(current))
                    {
                        duplicatedMatrix[i][j] = 0;
                    }
                    else
                    {
                        duplicatedMatrix[i][j] = current;
                    }
                    for (int k = j + 1; k < size; ++k)
                    {
                        duplicatedMatrix[i][k] -= duplicatedMatrix[i][j] * duplicatedMatrix[j][k];
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
        public static double[] HelperSolve(double[][] luMatrix, double[] b)
        {
            int size = luMatrix.Length;
            double[] x = new double[size];
            b.CopyTo(x, 0);
            for (int i = 1; i < size; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                {
                    sum -= luMatrix[i][j] * x[j];
                }
                x[i] = sum;
            }
            x[size - 1] /= luMatrix[size - 1][size - 1];
            for (int i = size - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < size; ++j)
                {
                    sum -= luMatrix[i][j] * x[j];
                }
                x[i] = sum / luMatrix[i][i];
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
        public static double[][] Inverse(double[][] matrix)
        {
            int size = matrix.Length;
            var duplicatedMatrix = MatrixDuplicate(matrix);
            int[] perm;
            int toggle;
            var lum = Decompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute inverse");
            double[] b = new double[size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }
                double[] x = HelperSolve(lum, b);
                for (int j = 0; j < size; ++j)
                    duplicatedMatrix[j][i] = x[j];
            }
            return duplicatedMatrix;
        }

        /// <summary>
        /// Определитель матрицы — это просто результат перемножения значений 
        /// на основной диагонали матрицы разложения LUP со знаком «плюс» или 
        /// «минус» в зависимости от значения toggle.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double GetDeterminant(double[][] matrix)
        {
            int[] perm;
            int toggle;
            var lum = Decompose(matrix, out perm, out toggle);
            if (lum == null) throw new Exception("unable compute determinant");
            double result = toggle;
            for (int i = 0; i < lum.Length; ++i)
                result *= lum[i][i];
            return result;
        }
    }
}
