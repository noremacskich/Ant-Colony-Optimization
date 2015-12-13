using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    public class Ant
    {
        public int antID { get; set; }

        public int pheromoneDepositeStrength { get; set; }

        public List<City> visitedCities = new List<City>();

        /// <summary>returns any city that hasn't been visited yet from the passed in parameter</summary>
        public List<City> citiesToVisit(List<City> completeCityList)
        {
            return completeCityList.Except(visitedCities).ToList();
        }

        public Ant(int thisAntID)
        {
            antID = thisAntID;
            pheromoneDepositeStrength = 10;

        }
    }
}
