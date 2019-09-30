using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Models
{
    class Salesman
    {
        private const int GENCOUNT = 5000;
        private const int POPCOUNT = 1000;
        

        private static Random r = new Random();

        private List<Population> generations;

        public Salesman(List<Town> towns)
        {
            Population pop = new Population(POPCOUNT, towns);
            generations = new List<Population>();
            for (int i = 0; i < GENCOUNT; i++)
            {
                generations.Add(null);
            }
            generations[0] = pop;
        }
        Chromosome parent1;
        Chromosome parent2;
        Chromosome kid;

        public void Travel()
        {
            for (int i = 0; i < GENCOUNT-1; i++)
            {
                generations[i + 1] = new Population();
                for (int j = 0; j < POPCOUNT;)
                {
                    parent1 = generations[i].Chromosomes[r.Next(generations[i].Chromosomes.Count)];// pop.GetFittest(r.Next(pop.Chromosomes.Count));
                    parent2 = generations[i].Chromosomes[r.Next(generations[i].Chromosomes.Count)];// pop.GetFittest(r.Next(pop.Chromosomes.Count));

                    kid = Crossover(parent1, parent2);
                    if (kid.TotalDistance < parent1.TotalDistance && kid.TotalDistance < parent1.TotalDistance)
                    {
                        generations[i+1].Add(kid);
                        SetFittest(kid);
                        j++;
                    }
                }
            }
        }

        Chromosome child;
        private List<Point> fittest;

        private Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            child = new Chromosome(parent1.Travel.Count);

            int start = (r.Next(0, parent1.Travel.Count));
            int end = (r.Next(0, parent1.Travel.Count));

            for (int i = 0; i < parent1.Travel.Count; i++)
            {
                if (start < end && i > start && i < end)
                {
                    child.AddTown(i, parent1.Travel[i]);
                }
                else if (start > end)
                {
                    if (!(i < start && i > end))
                    {
                        child.AddTown(i, parent1.Travel[i]);
                    }
                }
            }

            for (int i = 0; i < parent2.Travel.Count; i++)
            {
                if (!child.Travel.Contains(parent2.Travel[i]))
                {
                    for (int j = 0; j < parent2.Travel.Count; j++)
                    {
                        if (child.Travel[j] == null)
                        {
                            child.AddTown(j, parent2.Travel[i]);
                            break;
                        }
                    }
                }
            }
            return child;
        }
        
        private void SetFittest(Chromosome fit)
        {
            fittest = fit.Travel.Select(x => new Point(x.x, x.y)).ToList();// generations.Contains(null) ? generations[generations.IndexOf(null) - 1].fittest.Travel.Select(x => new Point(x.x, x.y)).ToList() : null;
        }

        public List<Point> Fittest()
        {
            return fittest;
        }
    }
}
