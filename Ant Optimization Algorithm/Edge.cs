using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    /// <summary>An edge is the fancy term for a path that an ant decides to follow between cities.</summary>
    public class Edge
    {
        public City destination { get; set; }
        public City source { get; set; }

        public int ID { get; set; }

        public int PheromoneLevel { get; set; }

    }
}
