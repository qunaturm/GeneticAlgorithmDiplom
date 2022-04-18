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
            var maxVal = 10;  // not included
            Random random = new Random();
            double[][] result = CreateMatrix(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = random.Next(minVal, maxVal);
            return result;
        }
        public static double[][] MatrixRandomOneMinusOne(int rows, int cols)
        {
            Random random = new Random();
            double[][] result = CreateMatrix(rows, cols);
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    var check = random.NextDouble();
                    if (check < 0.5) result[i][j] = 1;
                    else result[i][j] = -1;

                }
            }
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
            if (matrix != null)
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
            else Console.WriteLine("matrix is empty");
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
            double[][] result = new double[squareMetrixSize][];

            // eliminate repeat
            for (int i = 0; i < squareMetrixSize; ++i)
            {
                result[i] = new double[squareMetrixSize];
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
            // Разложение LUP Дулитла. Предполагается,
            // что матрица квадратная.
            int n = matrix.Length; // для удобства
            double[][] result = MatrixDuplicate(matrix);
            perm = new int[n];
            for (int i = 0; i < n; ++i) { perm[i] = i; }
            toggle = 1;
            for (int j = 0; j < n - 1; ++j) // каждый столбец
            {
                double colMax = Math.Abs(result[j][j]); // Наибольшее значение в столбце j
                int pRow = j;
                for (int i = j + 1; i < n; ++i)
                {
                    if (result[i][j] > colMax)
                    {
                        colMax = result[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) // перестановка строк
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;
                    int tmp = perm[pRow]; // Меняем информацию о перестановке
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle; // переключатель перестановки строк
                }
                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                        result[i][k] -= result[i][j] * result[j][k];
                }
            } // основной цикл по столбцу j
            return result;
        }

        /// <summary>
        /// Находит массив x, который при умножении на матрицу LU дает массив b.
        /// </summary>
        /// <param name="luMatrix"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[] HelperSolve(double[][] luMatrix, double[] b)
        {
            // Решаем luMatrix * x = b
            int n = luMatrix.Length;
            double[] x = new double[n];
            b.CopyTo(x, 0);
            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum;
            }
            x[n - 1] /= luMatrix[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum / luMatrix[i][i];
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
        public static double[][] MatrixInverse(double[][] matrix)
        {
            int n = matrix.Length;
            double[][] result = MatrixDuplicate(matrix);
            int[] perm;
            int toggle;
            double[][] lum = Decompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute inverse");
            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }
                double[] x = HelperSolve(lum, b);
                for (int j = 0; j < n; ++j)
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
        public static double GetDeterminant(double[][] matrix)
        {
            if (matrix == null) return 0.0;
            int[] perm;
            int toggle;
            double[][] lum = Decompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute MatrixDeterminant");
            double result = toggle;
            for (int i = 0; i < lum.Length; ++i)
                result *= lum[i][i];
            return result;
        }
    }
}
