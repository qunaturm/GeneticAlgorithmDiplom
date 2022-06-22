namespace GeneticAlgorithmDiplom
{
    public class AmadarMatrixDeterminant
    {
        public static int CheckWithAmadar(int dimention)
        {
            switch (dimention)
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 4;
                case 4: return 16;
                case 5: return 48;
                case 6: return 160;
                case 7: return 576;
                case 8: return 4096;
                case 9: return 14336;
                case 10: return 73728;
                case 11: return 327680;
                case 12: return 2985984;
                case 13: return 14929920;
                default: break;
            }
            throw new ArgumentException($"Determinant for {dimention} dimention doesn't found for now");
        }
    }
}
