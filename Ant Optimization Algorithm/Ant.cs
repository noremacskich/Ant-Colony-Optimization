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

        public bool visitedAllCities { get; set; }

        /// <summary>Get the distance traveled based on what edges we have visited.</summary>
        public double distanceTraveled
        {
            get
            {
                return lstPathsTraveled.Sum(x => x.distance);
            }
        }

        public List<Edge> lstPathsTraveled = new List<Edge>();

        public List<City> visitedCities = new List<City>();

        /// <summary>returns any city that hasn't been visited yet from the passed in parameter</summary>
        public List<City> citiesToVisit(List<City> completeCityList)
        {
            return completeCityList.Except(visitedCities).ToList();
        }

        /// <summary>primarily needed to get the list of possible paths to take.</summary>
        public City currentCity
        {
            get
            {
                return visitedCities.Last();
            }
        }
        
        /// <summary>will ensure that the path traveled is accounted for, and that the destination is correctly set.</summary>
        public void travelToCity(City source, City destination)
        {

            // Add the path between the current city and the destination city
            lstPathsTraveled.Add((from thisEdge in currentCity.connectedEdges
                                 where thisEdge.source == source && thisEdge.destination == destination
                                 select thisEdge).Single());

            // Switch the current city to the destination city
            // This can be done by adding the destination to the end of the list of cities visited
            visitedCities.Add(destination);

        }

        public void constructAntSolution()
        {
            throw new NotImplementedException("Construct Ant Solution not yet implemented.");
        }

        public City getNextCity(List<City> completeCityList, double Alpha = .5, double Beta = .5)
        {

            City nextCity = new City();

            List<Edge> EdgesToChooseFrom = currentCity.connectedEdges;

            // Remove any edges whose destination is a city that we have already been to
            EdgesToChooseFrom = (from thisEdge in currentCity.connectedEdges
                                 where !visitedCities.Contains(thisEdge.destination)
                                 select thisEdge).ToList();

            double denominatorSum = 0;

            denominatorSum = EdgesToChooseFrom.Sum(x => x.subProbability(Alpha, Beta));

            // Basically run through each edge, calculate each probability, and compare it to a random double value.
            // Repeat until we get an edge.
            while (true)
            {
                // If we have visited all cities, return the first city
                if(EdgesToChooseFrom.Count == 0)
                {
                    // State that this ant is done finding a complete path
                    visitedAllCities = true;

                    // Return the first city.
                    return visitedCities.First();
                }

                // If we only have one city to choose from, no need to calculate all possibilities.
                if(EdgesToChooseFrom.Count == 1)
                {
                    return EdgesToChooseFrom.First().destination;
                }


                // Go through each edge, and see if we want to visit this destination.
                foreach( Edge thisEdge in EdgesToChooseFrom)
                {
                    double probability = thisEdge.subProbability(Alpha, Beta) / denominatorSum;

                    Random tmpRand = new Random();

                    if(probability >= tmpRand.NextDouble())
                    {
                        return thisEdge.destination;
                    }

                }
            }

        }

        public Ant(int thisAntID)
        {
            antID = thisAntID;
            pheromoneDepositeStrength = 10;

        }
    }
}
