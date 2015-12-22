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

        /// <summary>
        /// Largest value a rand function can return.
        /// </summary>
        private const int RAND_MAX = 2147483647;

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

        /// <summary>Needs to be assigned when the ant is created.</summary>
        public List<City> lstAllCities = new List<City>();

        /// <summary>returns any city that hasn't been visited yet from the passed in parameter</summary>
        public List<City> citiesToVisit
        {
            get
            {
                return lstAllCities.Except(visitedCities).ToList();
            }
            
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

            if(source == destination && visitedAllCities != true)
            {
                throw new Exception("Trying to travel to the same city!  This won't work out!");
            }

            // Add the path between the current city and the destination city
            lstPathsTraveled.Add((from thisEdge in currentCity.CorrectedConnectedEdges()
                                 where thisEdge.source == source && thisEdge.destination == destination
                                 select thisEdge).Single());

            // Switch the current city to the destination city
            // This can be done by adding the destination to the end of the list of cities visited

            // If this is the last city, we don't actually need to put the city here, all we need is the path between them
            if (!visitedAllCities)
            {
                visitedCities.Add(destination);
            }
            


        }

        /// <summary>The core of the ants creating their best path.</summary>
        public void constructAntSolution(double Alpha = .5, double Beta = .5)
        {

            if (visitedAllCities)
            {
                throw new Exception("This ant currently has a constructed solution!  Please clear it before moving on.");
            }

            // Keep moving to new cities, while we have them.
            while(citiesToVisit.Count() != 0)
            {

                City nextCity = getNextCity(Alpha, Beta);

                travelToCity(currentCity, nextCity);

            }

            visitedAllCities = true;

            // Take care of the case for the last city
            travelToCity(currentCity, visitedCities.First());
        }


        public City getNextCity(double Alpha = .5, double Beta = .5)
        {

            City nextCity = new City();

            List<Edge> EdgesToChooseFrom = currentCity.CorrectedConnectedEdges();

            // Remove any edges whose destination is a city that we have already been to
            EdgesToChooseFrom = (from thisEdge in EdgesToChooseFrom
                                 where !visitedCities.Contains(thisEdge.destination)
                                 select thisEdge).ToList();

            double denominatorSum = 0;

            denominatorSum = EdgesToChooseFrom.Sum(x => x.subProbability(Alpha, Beta));

            // Basically run through each edge, calculate each probability, and compare it to a random double value.
            // Repeat until we get an edge.
            while (true)
            {

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

        /// <summary>Resets an ant to allow another run at a solution.</summary>
        public void resetAntSolution()
        {

            lstPathsTraveled.Clear();
            visitedCities.Clear();

            visitedAllCities = false;

            // Give the ant a random initial city

            Random tmpRand = new Random();

            int startingCity = tmpRand.Next(0, lstAllCities.Count());

            visitedCities.Add(lstAllCities[startingCity]);

        }

        public Ant(int thisAntID, List<City> lstOfCities)
        {
            antID = thisAntID;
            pheromoneDepositeStrength = 10;
            lstAllCities = lstOfCities;

        }
    }
}
