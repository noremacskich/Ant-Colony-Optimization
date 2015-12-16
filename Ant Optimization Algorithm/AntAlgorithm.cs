using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ant_Optimization_Algorithm
{
    class AntAlgorithm
    {
        const int GRIDSIZEX = 200;
        const int GRIDSIZEY = 200;
        const int DEFAULT_PHEROMONE_LEVEL = 5;
        const int NUMBER_OF_CITIES = 10;
        const int NumOfAnts = 4;

        Random rand = new Random();

        gridCell[,] currentGrid = new gridCell[GRIDSIZEX,GRIDSIZEY];

        public List<Ant> lstOfAnts = new List<Ant>();

        public List<City> lstOfCities = new List<City>();

        public List<Edge> lstOfEdges = new List<Edge>();

        public gridCell[,] getSurrounding(int cellx, int celly, int radius = 1)
        {

            int radiusx = radius * 2 + 1;
            int radiusy = radius * 2 + 1;

            gridCell[,] surroundingCells = new gridCell[3,3];

            int y2, x2 = 0;

            int sum = 0;

            
            int y = 0;

            //go through neighbors that exist and sum them
            for (y2 = (celly - radius); y2 <= (celly + radius); ++y2)
            {
                int x = 0;
                for (x2 = (cellx - radius); x2 <= (cellx + radius); ++x2)
                {
                    //if the value in in the array
                    if (x2 >= 0 && y2 >= 0 && x2 < GRIDSIZEX && y2 < GRIDSIZEY)
                    {
                        surroundingCells[x, y] = currentGrid[x2, y2];
                    }
                    else
                    {
                        surroundingCells[x, y] = new gridCell { isBeyoundEdge = true };
                    }
                    x++;
                }
                y++;
            }

            return surroundingCells;
        }



        private void initializeGrid()
        {
            for (int x = 0; x < GRIDSIZEX; x++)
            {
                for (int y = 0; y < GRIDSIZEY; y++)
                {
                    currentGrid[x, y] = new gridCell { difficultyPassing = (y%4)*(x%4) };
                }
            }
        }

        private void initializeEdges()
        {
            int nodeID = 0;

            List<City> completedNodes = new List<City>();

            foreach(City node in lstOfCities)
            {
                // Add the node to the completed list, to avoid 
                // links to itself.
                completedNodes.Add(node);

                // Create a link to every other city that this node currently doesn't have access to.
                foreach (City otherNode in lstOfCities.Except(completedNodes).ToList())
                {
                    Edge tmpEdge = new Edge()
                    {
                        source = node,
                        destination = otherNode,
                        ID = nodeID,
                        PheromoneLevel = DEFAULT_PHEROMONE_LEVEL
                    };

                    lstOfEdges.Add(tmpEdge);

                    // Add this edge to both the source and destination cities.
                    // This will allow us to easily get the list of paths for each city
                    node.connectedEdges.Add(tmpEdge);
                    otherNode.connectedEdges.Add(tmpEdge);

                    nodeID++;
                }

                
            }

        }
        
        private void initializeCities(int numberOfCities)
        {

            int[,] arrCityLocation = { { 56, 92 }, { 105, 98 }, { 20, 145 }, { 37, 38 }, { 19, 198 }, { 50, 150 }, { 75, 137 }, { 72, 48 }, { 102, 30 }, { 80, 147 } };

            for (int i = 0; i<numberOfCities; i++)
            {

                City tmpCity = new City { ID = i };

                // Get the city location, based on static values provided
                tmpCity.locationX = arrCityLocation[i, 0];
                tmpCity.locationY = arrCityLocation[i, 1];

                // Keep assigning random locations until we find a grid cell that doesn't have a city.
                //do
                //{
                //    tmpCity.locationX = rand.Next(GRIDSIZEX);
                //    tmpCity.locationY = rand.Next(GRIDSIZEY);
                //} while (currentGrid[tmpCity.locationX, tmpCity.locationY].ThisCity != null);


                // Assign the city to the grid
                //currentGrid[tmpCity.locationX, tmpCity.locationY].ThisCity = tmpCity;
                
                // Add it to the list of Cities
                lstOfCities.Add(tmpCity);

            }
        }

        /// <summary>Create an ant for each city.</summary>
        public void initializeAnts()
        {
            
            
            foreach( City city in lstOfCities)
            {
                Ant tmpAnt = new Ant(city.ID, lstOfCities);

                tmpAnt.visitedCities.Add(city);

                city.currentAnt = tmpAnt;

                lstOfAnts.Add(tmpAnt);
            }
            


        }

        public AntAlgorithm(int numberOfCities = NUMBER_OF_CITIES)
        {
            initializeGrid();
            initializeCities(NUMBER_OF_CITIES);
            initializeEdges();
            initializeAnts();
        }

    }
}
