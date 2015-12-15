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
        private void button2_Click(object sender, EventArgs e)
        {

            System.Drawing.Graphics graphic;

            graphic = pictureBox1.CreateGraphics();

            foreach(City selectedCity in algorithm.cityNodes)
            {
                drawCity(selectedCity, graphic);
            }

        }
    }
}
