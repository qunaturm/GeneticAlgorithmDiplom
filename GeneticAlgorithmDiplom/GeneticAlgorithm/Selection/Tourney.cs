namespace GeneticAlgorithmDiplom.GeneticAlgorithm.Selection
{
    public static class Tourney
    {
        public static Func<List<Individual>, int, bool, List<Individual>> Selector = (firstGeneration, bestFromSelection, enableElitism) =>
        {
            List<Individual> bestIndividuals = new List<Individual>();
            if (enableElitism == true)
            {
                var parents = Individual.MergeSort(firstGeneration);
                bestIndividuals.Add(parents[parents.Count - 1]);
                bestIndividuals.Add(parents[parents.Count - 2]);
            }
            List<Individual> firstTourney = new List<Individual>();
            List<Individual> secondTourney = new List<Individual>();
            int counter = 1;
            foreach (Individual individual in firstGeneration)
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
            firstTourney = Individual.MergeSort(firstTourney);
            secondTourney = Individual.MergeSort(secondTourney);

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
                bestIndividuals = Individual.MergeSort(bestIndividuals);
                bestIndividuals.RemoveRange(0, 2);
            }
            return bestIndividuals;
        };
    }
}
