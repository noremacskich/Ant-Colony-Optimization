using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    /// <summary>An edge is the fancy term for a path that an ant decides to follow between cities.</summary>
    [DebuggerDisplay("Source = '{source.ID}', Destination = '{destination.ID}'")]
    public class Edge
    {
        public City destination { get; set; }
        public City source { get; set; }

        public int ID { get; set; }

        public Edge() { }

        public Edge(Edge path)
        {
            ID = path.ID;
            destination = new City(path.destination);
            source = new City(path.source);
            PheromoneLevel = path.PheromoneLevel;
        }

        public double PheromoneLevel { get; set; }

        public double subProbability(double Alpha, double Beta)
        {
            return Math.Pow(PheromoneLevel, Alpha) * Math.Pow((1 / distance), Beta);
        }

        public double distance
        {
            get {

                double xPart = Math.Pow((source.locationX - destination.locationX), 2);
                double yPart = Math.Pow((source.locationY - destination.locationY), 2);

                return Math.Sqrt(xPart + yPart);
            }

            set { }
            
        }
    }
}
