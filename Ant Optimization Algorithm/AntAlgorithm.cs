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
        const int GRIDSIZEX = 18;
        const int GRIDSIZEY = 18;
        const int NumOfAnts = 4;

        Random rand = new Random();

        gridCell[,] currentGrid = new gridCell[GRIDSIZEX,GRIDSIZEY];

        List<Ant> lstOfAnts = new List<Ant>();

        RichTextBox updateTextbox;

        private string updateOutput(gridCell[,] grid, int sizeX, int sizeY)
        {

            string currentOutput = "";

            int paddingSize = 3;

            for (int x = 0; x < sizeX; x++)
            { 
                for (int y = 0; y < sizeY; y++)
                {
                    if (grid[x,y].isBeyoundEdge == true) {
                        currentOutput += "x".PadLeft(paddingSize);
                    }
                    else
                    {
                        currentOutput += grid[x, y].difficultyPassing.ToString().PadLeft(paddingSize);
                    }
                    
                }

                currentOutput += "\r\n";

            }

            return currentOutput;
        }

        private string printAntsandFood(gridCell[,] grid, int sizeX, int sizeY)
        {

            string currentOutput = "";

            int paddingSize = 3;

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (grid[x, y].currentAnt != null)
                    {
                        currentOutput += "x".PadLeft(paddingSize);
                    }
                    else
                    {
                        currentOutput += "".PadLeft(paddingSize);
                    }

                }

                currentOutput += "\r\n";

            }

            return currentOutput;
        }

        public string updateOutput()
        {
            return updateOutput(currentGrid, GRIDSIZEX, GRIDSIZEY);
        }

        public string testGetSurrounding(int positionX, int positionY)
        {

            gridCell[,] test = getSurrounding(positionX, positionY);

            return updateOutput(test, 3, 3);

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

        private void placeAllAnts()
        {
            foreach(Ant ant in lstOfAnts)
            {




                placeAnt(ant, )
            }
        }

        private int findNextMovementSpotx(int currentX, int currentY)
        {

            gridCell[,] tempSurrounding = getSurrounding(currentX, currentY);


            for(int x=0; x<3; x++)
            {
                for(int y=0; y<3; y++)
                {

                    if (tempSurrounding[x, y].isBeyoundEdge)
                    {
                        // Skip this one
                        continue;
                    }
                    else
                    {

                    }

                }

            }


        }

        private void placeAnt(Ant antToPlace, int x, int y)
        {

            if(x > GRIDSIZEX || y > GRIDSIZEY)
            {
                throw new Exception("Not able to place ant on the grid, out of bounds.");
            }

            // First add it to the visitors list
            currentGrid[antToPlace.currentXPosition, antToPlace.currentYPosition].AntVisitors.Add(antToPlace);

            // Next remove the ant from this gridcell
            currentGrid[antToPlace.currentXPosition, antToPlace.currentYPosition].currentAnt = antToPlace;

            // Now place the ant in the new cell
            currentGrid[x, y].currentAnt = antToPlace;

            // and give it new values
            antToPlace.currentXPosition = x;
            antToPlace.currentYPosition = y;

        }
        /// <summary>
        /// Places the Ants on the grid.
        /// </summary>
        private void initializeAnts()
        {

            for(int i = 0; i< NumOfAnts; i++)
            {

                Ant newAnt = new Ant(i)
                {
                    currentXPosition = i,
                    currentYPosition = i * 2
                };

                lstOfAnts.Add(newAnt);

            }

        }

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

        public AntAlgorithm()
        {
            initializeGrid();

        }

    }
}
