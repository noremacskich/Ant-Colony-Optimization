using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    /// <summary>This is a city that an ant can travel to.  At some point it will have a list of food it contains.</summary>
    [DebuggerDisplay("ID = '{ID}', X = '{locationX}', Y = '{locationY}'")]
    public class City
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Ant currentAnt { get; set; }

        public List<Edge> connectedEdges = new List<Edge>();

        public City(City thisCity)
        {
            Name = thisCity.Name;
            ID = thisCity.ID;
            currentAnt = thisCity.currentAnt;
            connectedEdges = new List<Edge>(thisCity.connectedEdges);
            locationX = thisCity.locationX;
            locationY = thisCity.locationY;
        }

        // Allow default constructor
        public City() { } 

        /// <summary>Returns the list of edges with a normalized source, meaning the source property is all the same.
        /// needed to allow the getNextCity function to correctly filter out the paths to the cities we have already visited, based 
        /// off the the destination variable.  Without this, it was causing some of the later cities (8, 9) etc to filter out both valid and invalid paths.
        /// </summary>
        public List<Edge> CorrectedConnectedEdges()
        {
            List<Edge> correctedEdges = new List<Edge>();

            foreach(Edge path in connectedEdges)
            {
                
                if(path.destination == this)
                {
                    Edge tmpEdge = new Edge {
                        ID = path.ID,
                        destination = path.source,
                        source = path.destination,
                        PheromoneLevel = path.PheromoneLevel
                    };

                    correctedEdges.Add(tmpEdge);
                }
                else
                {
                    correctedEdges.Add(path);
                }

            }

            return correctedEdges;
        }


        public int locationX { get; set; }
        public int locationY { get; set; }
    }
}
