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

            //rtbOutput.Text = algorithm.showCities();

        }
        private void drawCity(City currentCity, Graphics canvas)
        {

            Pen pen1 = new Pen(Color.Blue, 3);

            canvas.DrawEllipse(pen1, currentCity.locationX, currentCity.locationY, 15, 15);
            canvas.DrawString(currentCity.ID.ToString(), DefaultFont, Brushes.Blue, new PointF(currentCity.locationX + 2, currentCity.locationY + 2));

        }
        private void drawPath(Edge thisPath, Graphics canvas)
        {

            Pen pen1 = new Pen(Color.Blue, 3);

            pen1.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            canvas.DrawLine(pen1, new Point(thisPath.source.locationX, thisPath.source.locationY), new Point(thisPath.destination.locationX, thisPath.destination.locationY));

        }
        private void button2_Click(object sender, EventArgs e)
        {

            System.Drawing.Graphics graphic;

            graphic = pictureBox1.CreateGraphics();

            foreach(City selectedCity in algorithm.lstOfCities)
            {
                drawCity(selectedCity, graphic);
            }


            //foreach( Edge path in algorithm)

            //Ant thisAnt = algorithm.lstOfAnts.First();

            //thisAnt.constructAntSolution();

            //foreach(Edge path in thisAnt.lstPathsTraveled)
            //{
            //    drawPath(path, graphic);
            //}

        }
    }
}
