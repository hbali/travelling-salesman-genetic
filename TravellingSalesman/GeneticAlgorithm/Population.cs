using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Population
    {
        List<Chromosome> chromosomes;
        public Chromosome fittest;

        public Population(int count, List<Town> towns)
        {
            chromosomes = new List<Chromosome>();
            for (int i = 0; i < count; i++)
            {
                chromosomes.Add(new Chromosome(towns));
            }
            fittest = GetFittest(0);
        }

        public Population()
        {
            chromosomes = new List<Chromosome>();
        }

        internal List<Chromosome> Chromosomes { get => chromosomes; set => chromosomes = value; }

        /// <summary>
        /// Gets the idx fittest element from the population
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Chromosome GetFittest(int idx)
        {
            return chromosomes.OrderBy(x => x.TotalDistance).ToArray()[idx];
        }

        internal void Add(Chromosome kid)
        {
            chromosomes.Add(kid);
            fittest = kid;
        }
    }
}
