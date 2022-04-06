using GeneticAlgorithmDiplom;

namespace GeneticAlgorithmDiplom
{
    public static class Diplom
    {
        public static void Main(string[] args)
        {
            int vectorsAmount = 7; // vectors amount >= elements in vector
            int elementsInVector = 3;
            var vectors = new Matrix(vectorsAmount, elementsInVector);
            vectors.FillVectors();
            var squareMatrix = Matrix.GetSquareMatrix(vectors);
            Matrix.PrintMatrix(squareMatrix);
        }
    }
}