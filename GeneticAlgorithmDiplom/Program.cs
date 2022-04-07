﻿using GeneticAlgorithmDiplom;
using MatrixExample;

namespace GeneticAlgorithmDiplom
{
    public static class Diplom
    {
        public static void Main(string[] args)
        {
            double[][] m = MatrixExample.MatrixExample.MatrixCreate(4, 4);

            m[0][0] = 4;
            m[0][1] = 8;
            m[0][2] = 12;
            m[0][3] = 55;

            m[1][0] = 16;
            m[1][1] = 29;
            m[1][2] = 98;
            m[1][3] = 3;

            m[2][0] = 63;
            m[2][1] = 11;
            m[2][2] = 74;
            m[2][3] = 49;

            m[3][0] = 91;
            m[3][1] = 33;
            m[3][2] = 2;
            m[3][3] = 86;

            var det = MatrixExample.MatrixExample.MatrixDeterminant(m);
            Console.WriteLine(det);
        }
    }
}