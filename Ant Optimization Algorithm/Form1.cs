using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ant_Optimization_Algorithm
{
    public partial class Form1 : Form
    {

        const int SIZE_OF_CITY_CIRCLE = 15;

        AntAlgorithm algorithm = new AntAlgorithm(9);
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            System.Drawing.Graphics graphic;

            graphic = pictureBox1.CreateGraphics();

            drawEverything(graphic);

            algorithm.mainDriver();

            System.Drawing.Graphics graphic2;

            graphic2 = pictureBox2.CreateGraphics();

            drawEverything(graphic2);

            //drawAllPaths(graphic);

            drawBestMap();

            button1.Text = "Continue Simulation";

            button2.Enabled = true;
        }

        private void drawCity(City currentCity, Graphics canvas, bool isFirstCity = false, bool isSecondCity = false)
        {
            // Give the city the appropriate color of background, dependin on the location of the city.
            // Default city background is white
            SolidBrush myBrush = new SolidBrush(Color.White);


            if (isFirstCity)
            {
                myBrush.Color = Color.Green;
            }
            else if (isSecondCity)
            {
                myBrush.Color = Color.Yellow;
            }

            canvas.FillEllipse(myBrush, currentCity.locationX, currentCity.locationY, SIZE_OF_CITY_CIRCLE, SIZE_OF_CITY_CIRCLE);

            // Give the city a border of blue
            Pen pen1 = new Pen(Color.Blue, 2);
            canvas.DrawEllipse(pen1, currentCity.locationX, currentCity.locationY, SIZE_OF_CITY_CIRCLE, SIZE_OF_CITY_CIRCLE);

            // Give the city an ID number
            StringFormat centerText = new StringFormat();
            centerText.Alignment = StringAlignment.Center;
            centerText.LineAlignment = StringAlignment.Center;
            canvas.DrawString(currentCity.ID.ToString(), DefaultFont, Brushes.Blue, new RectangleF(currentCity.locationX, currentCity.locationY, SIZE_OF_CITY_CIRCLE, SIZE_OF_CITY_CIRCLE), centerText);

        }

        private Point getMidpoint(Edge thisPath)
        {
            Point midPoint = new Point();

            midPoint.X = ((thisPath.destination.locationX + thisPath.source.locationX) / 2);
            midPoint.Y = ((thisPath.destination.locationY + thisPath.source.locationY) / 2);

            return midPoint;
        }

        private void drawPath(Edge thisPath, Graphics canvas)
        {

            Pen pen1 = new Pen(Color.Gray, 2);

            canvas.DrawLine(pen1, 
                new Point(
                    thisPath.source.locationX + (SIZE_OF_CITY_CIRCLE / 2), 
                    thisPath.source.locationY + (SIZE_OF_CITY_CIRCLE / 2)
                ), 
                new Point(
                    thisPath.destination.locationX + (SIZE_OF_CITY_CIRCLE / 2), 
                    thisPath.destination.locationY + (SIZE_OF_CITY_CIRCLE / 2)
                )
            );

            canvas.DrawString(thisPath.PheromoneLevel.ToString("F"), DefaultFont, Brushes.Red, getMidpoint(thisPath));

        }

        /// <summary>
        /// Differentiate between the first and second city.  This will help determine the direction 
        /// the ants paths are following.  Since all ants visit all cities, we can simply use 
        /// the list of cities that the ant followed.
        /// </summary>
        private void drawCities(List<City> lstOfCities, Graphics graphic)
        {

            int i = 0;
            foreach (City selectedCity in lstOfCities)
            {
                // First city will have a green background
                if (i == 0) {
                    drawCity(selectedCity, graphic, isFirstCity: true);
                }
                // Second city will have yellow background
                else if (i == 1) {
                    drawCity(selectedCity, graphic, isSecondCity: true);
                }
                // Otherwise the city will be white.
                else 
                {
                    drawCity(selectedCity, graphic);
                }

                i++;
            }

        }

        /// <summary>Keep this seperate, since each ant will want to do this.</summary>
        private void drawPaths(List<Edge> lstOfEdges, Graphics graphic)
        {
            foreach (Edge path in lstOfEdges)
            {
                drawPath(path, graphic);
            }
        }
        
        private void drawBestMap()
        {
            System.Drawing.Graphics graphic;

            graphic = pictureBox3.CreateGraphics();

            graphic.Clear(Color.White);

            foreach (Edge path in algorithm.lstBestPath)
            {
                drawPath(path, graphic);
            }

            foreach (City thisCity in algorithm.lstBestCities)
            {
                drawCity(thisCity, graphic);
            }
        }

        /// <summary>Draws the default map.</summary>
        private void drawEverything(Graphics graphic)
        {
            graphic.Clear(Color.White);

            foreach (Edge path in algorithm.lstOfEdges)
            {
                drawPath(path, graphic);
            }

            foreach (City thisCity in algorithm.lstOfCities)
            {
                drawCity(thisCity, graphic);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            algorithm = new AntAlgorithm(9);

            

            button2.Enabled = false;
        }
    }
}
