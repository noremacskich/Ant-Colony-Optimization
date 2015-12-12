using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    /// <summary>An edge is the fancy term for a path that an ant decides to follow between cities.</summary>
    class Edge
    {
        City destination { get; set; }
        City source { get; set; }
        int ID { get; set; }

    }
}
