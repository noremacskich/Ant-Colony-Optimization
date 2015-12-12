using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    class gridCell
    {
        public List<Ant> AntVisitors = new List<Ant>();

        public Ant currentAnt;

        public int pheromoneLevel { get; set; }

        public int difficultyPassing { get; set; }

        /// <summary>This is used to indicate if this particular gridCell is beyond the edge of the grid.</summary>
        public bool isBeyoundEdge { get; set; }

        public void depositPheromone(Ant visitor)
        {

            AntVisitors.Add(visitor);

            pheromoneLevel += visitor.pheromoneDepositeStrength;
            
        }


        public void Overwrite(gridCell newGridCell) {


        }


        public gridCell()
        {
            pheromoneLevel = 0;
            difficultyPassing = -1;
        }
    }
}
