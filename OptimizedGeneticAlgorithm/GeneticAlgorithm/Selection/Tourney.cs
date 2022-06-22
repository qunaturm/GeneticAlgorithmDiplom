using MatrixModule;

namespace OptimizedGeneticAlgorithm.GeneticAlgorithm.Selection
{
    public static class Tourney
    {
        public static Func<List<DependentMatrix>, int, bool, List<DependentMatrix>> Selector = (firstGeneration, bestFromSelection, enableElitism) =>
        {
            List<DependentMatrix> bestIndividuals = new List<DependentMatrix>();
            if (enableElitism == true)
            {
                var parents = firstGeneration.OrderByDescending(u => u.Determinant).Take(2);
                bestIndividuals.AddRange(parents);
            }
            List<DependentMatrix> firstTourney = new List<DependentMatrix>();
            List<DependentMatrix> secondTourney = new List<DependentMatrix>();
            int counter = 1;
            foreach (DependentMatrix individual in firstGeneration)
            {
                if (counter < bestFromSelection + 1)
                {
                    if (counter % 2 == 0)
                    {
                        if (double.IsNaN(individual.Determinant) || double.IsInfinity(individual.Determinant)) { }
                        else
                        {
                            firstTourney.Add(individual);
                            counter++;
                        }
                    }
                    else
                    {
                        if (double.IsNaN(individual.Determinant) == true || double.IsInfinity(individual.Determinant)) { }
                        else
                        {
                            secondTourney.Add(individual);
                            counter++;
                        }
                    }

                }
            }
            // sort from min det to max
            firstTourney = firstTourney.OrderBy(u => u.Determinant).ToList();
            secondTourney = secondTourney.OrderBy(u => u.Determinant).ToList();

            if (firstTourney.Count + secondTourney.Count <= bestFromSelection / 2) throw new Exception("not enough individeals");
            for (int i = firstTourney.Count - 1; i >= firstTourney.Count - bestFromSelection / 2; --i)
            {
                bestIndividuals.Add(firstTourney[i]);
            }

            for (int i = secondTourney.Count - 1; i >= secondTourney.Count - bestFromSelection / 2; --i)
            {
                bestIndividuals.Add(secondTourney[i]);
            }

            // case if elitism enable
            if (bestIndividuals.Count != bestFromSelection)
            {
                bestIndividuals = bestIndividuals.OrderBy(u => u.Determinant).ToList();
                bestIndividuals.RemoveRange(0, 2);
            }
            return bestIndividuals;
        };
    }
}
