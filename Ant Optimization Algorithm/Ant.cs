using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    class Ant
    {
        public int antID { get; set; }

        public int pheromoneDepositeStrength { get; set; }

        public int currentXPosition;
        public int currentYPosition;

        public Ant(int thisAntID)
        {
            antID = thisAntID;
            pheromoneDepositeStrength = 10;

        }
    }
}
