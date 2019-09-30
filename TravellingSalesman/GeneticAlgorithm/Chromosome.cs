using Extensions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Chromosome
    {

        private static Random r = new Random();
        List<Town> tour;

        internal List<Town> Travel { get => tour; set => tour = value; }

        public Chromosome(List<Town> towns)
        {
            var shuffled = towns.ToList();
            shuffled.Shuffle(r);
            tour = shuffled;
        }

        public Chromosome(int cnt)
        {
            tour = new List<Town>();
            for (int i = 0; i < cnt; i++)
            {
                tour.Add(null);
            }
        }

        /// <summary>
        /// Total distance of the tour
        /// </summary>
        /// <returns></returns>
        public float TotalDistance
        {
            get
            {
                float distance = 0;
                for (int i = 0; i < tour.Count - 1; i++)
                {
                    distance += tour[i].Distance(tour[i + 1]);
                }
                return distance;
            }
        }

        /// <summary>
        /// Ensures that there are no duplicate towns
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return tour.Count == tour.Distinct().Count();
        }

        internal void AddTown(int i, Town t)
        {
            tour[i] = t;
        }
    }
}
