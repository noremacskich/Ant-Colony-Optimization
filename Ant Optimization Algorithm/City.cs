﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm
{
    /// <summary>This is a city that an ant can travel to.  At some point it will have a list of food it contains.</summary>
    public class City
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Ant currentAnt { get; set; }

        public int locationX { get; set; }
        public int locationY { get; set; }
    }
}
