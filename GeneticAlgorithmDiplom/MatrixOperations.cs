namespace GeneticAlgorithmDiplom
{
    public class MatrixOperations
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
            var minVal = 1;
            var maxVal = 101;  // not included
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
                    Console.Write($"{matrix[row][column]}\t");
                }
                Console.WriteLine();
            }
        }
        #endregion

        public double[] GetRow(double[][] matrix, int rowNumber)
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

        public static double[] GetCol(double[][] m, int index)
        {
            double[] col = new double[m.Length];
            for (int i = 0; i < m.Length; ++i)
            {
                col[i] = m[index][i];
            }
            return col;
        }

        public static void SwapRows(ref double[][] matrix, double[] newRow, int index)
        {
            for (int i = 0; i < matrix.Length; ++i)
            {
                matrix[index][i] = newRow[i];
            }
        }

        public static void SwapRows(ref double[][] m1, ref double[][] m2, int index)
        {
            var tmp = m1[index];
            m1[index] = m2[index];
            m2[index] = tmp;
        }

        /// <summary>
        /// Поменять местами столбцы с индексом index в двух матрицах
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <param name="index"></param>
        public static void SwapColls(ref double[][] m1, ref double[][] m2, int index)
        {
            for (int i = 0; i < m2.Length; ++i)
            {
                var tmp = m1[i][index];
                m1[i][index] = m2[i][index];
                m2[i][index] = tmp;
            }
        }

        /// <summary>
        /// Поменять местами стоблцы с указанными индексами
        /// </summary>
        /// <param name="m"></param>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        public static void SwapColls(ref double[][] m, int firstIndex, int secondIndex)
        {
            for (int i = 0; i < m.Length; ++i)
            {
                var tmp = m[i][firstIndex];
                m[i][firstIndex] = m[i][secondIndex];
                m[i][secondIndex] = tmp;
            }
        }

        /// <summary>
        /// Поменять в матрице столбец с индексом index на столбец coll
        /// </summary>
        /// <param name="m"></param>
        /// <param name="index"></param>
        /// <param name="coll"></param>
        public static void SwapColls(ref double[][] m, int index, double[] coll)
        {
            for (int i = 0; i < m.Length; ++i)
            {
                m[i][index] = coll[i];
            }
        }

        /// <summary>
        /// Скопировать в результирующую матрицу столбцы из первой матрицы
        /// с индексами [0, index) и столбцы из второй матрицы с индекса
        /// index до конца
        /// </summary>
        /// <param name="sourceLeft"></param>
        /// <param name="sourceRight"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static double[][] CopyColumn(double[][] sourceLeft, double[][] sourceRight, int index)
        {
            var result = new double[sourceLeft.Length][];
            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                result[i] = new double[sourceLeft.Length];
                for (int j = 0; j < index; ++j)
                {
                    result[i][j] = sourceLeft[i][j];
                }
            }

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                for (var j = index; j < sourceLeft.Length; ++j)
                {
                    result[i][j] = sourceRight[i][j];
                }
            }
            return result;
        }

        /// <summary>
        /// Скопировать в первого потомка столбцы из первой матрицы с индексами [0, firstIndex), [secondIndex, (end))
        /// и из второй матрицы с индексами [firstIndex, secondIndex). Во второго потока - наоборот
        /// </summary>
        /// <param name="sourceLeft"></param>
        /// <param name="sourceRight"></param>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        /// <returns></returns>
        public static List<Individual> CopyColumn(double[][] sourceLeft, double[][] sourceRight, int firstIndex, int secondIndex)
        {
            var result1 = new double[sourceLeft.Length][];
            var result2 = new double[sourceLeft.Length][];

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                result1[i] = new double[sourceLeft.Length];
                result2[i] = new double[sourceLeft.Length];
                for (int j = 0; j < firstIndex; ++j)
                {
                    result1[i][j] = sourceLeft[i][j];
                    result2[i][j] = sourceRight[i][j];
                }
            }

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                for (var j = firstIndex; j < secondIndex; ++j)
                {
                    result1[i][j] = sourceRight[i][j];
                    result2[i][j] = sourceLeft[i][j];
                }
            }

            for (int i = 0; i < sourceLeft.Length; ++i)
            {
                for (var j = secondIndex; j < sourceLeft.Length; ++j)
                {
                    result1[i][j] = sourceLeft[i][j];
                    result2[i][j] = sourceRight[i][j];
                }
            }
            var child1 = new Individual { Matrix = result1 };
            child1.Determinant = GetDeterminant(child1.Matrix);
            var child2 = new Individual { Matrix = result2 };
            child2.Determinant = GetDeterminant(child2.Matrix);

            var list = new List<Individual>();
            list.Add(child1);
            list.Add(child2);
            return list;
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
        /// <param name="matrix">Матрица</param>
        /// <param name="perm">Массив перестановки</param>
        /// <param name="toggle">Переключатель</param>
        /// <returns></returns>
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
                    if (double.IsNaN(current) || Double.IsInfinity(current))
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
            var current = luMatrix[size - 1][size - 1];
            if (current == 0)
            {
                x[size - 1] = 0;
            }
            else
            {
                x[size - 1] /= luMatrix[size - 1][size - 1];
            }
            for (int i = size - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < size; ++j)
                {
                    sum -= luMatrix[i][j] * x[j];
                }
                current = luMatrix[i][i];
                if (current == 0)
                {
                    x[i] = 0;
                }
                else
                {
                    x[i] = sum / luMatrix[i][i];
                }
            }
            return x;
        }

        /// <summary>
        /// Алгоритм обращения. Результат перемножения матрицы M и ее обращения 
        /// является единичной матрицей. Метод Inverse в основном отвечает за решение 
        /// системы уравнений Ax = b, где A — матрица разложения LU, а константы b равны 
        /// либо 1, либо 0 и соответствуют единичной матрице. Inverse 
        /// использует массив perm, возвращаемый после вызова Decompose.
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
