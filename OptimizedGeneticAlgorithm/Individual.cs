namespace OptimizedGeneticAlgorithm
{
    public sealed class Individual
    {
        public double[][] Matrix { get; set; } = null!;
        public double Determinant { get; set; }
        public static List<Individual> MergeSort(List<Individual> list)
        {
            if (list.Count == 1) return list;

            var left = new List<Individual>();
            var right = new List<Individual>();
            for (int i = 0; i < list.Count / 2; ++i)
            {
                left.Add(list[i]);
            }
            for (int i = 0; i < list.Count - left.Count; ++i)
            {
                right.Add(list[left.Count + i]);
            }

            return Merge(MergeSort(left), MergeSort(right));
        }

        public static List<Individual> Merge(List<Individual> left, List<Individual> right)
        {
            List<Individual> result = new List<Individual>(left.Count + right.Count);
            int l = 0;
            int r = 0;
            for (int i = 0; i < left.Count + right.Count; ++i)
            {
                if (l < left.Count && r < right.Count)
                {
                    result.Add(left[l].Determinant > right[r].Determinant ? right[r++] : left[l++]);
                }
                else
                {
                    result.Add(r < right.Count ? right[r++] : left[l++]);
                }
            }
            return result;
        }
    }
}
