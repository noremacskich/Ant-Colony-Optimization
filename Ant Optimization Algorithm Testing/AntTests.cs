using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ant_Optimization_Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Optimization_Algorithm.Tests
{
    [TestClass()]
    public class AntTests
    {
        [TestMethod()]
        public void AntTest()
        {
            throw new NotImplementedException();
        }
        [TestMethod()]
        public void citiesToVisit()
        {
            // Create a new ant
            Ant testAnt = new Ant(0);


            // create a dummy list of cities that we use
            List<City> fullCityList = new List<City>();
            for(int i = 0; i < 5; i++)
            {
                fullCityList.Add(new City
                {
                    ID = i
                });
            }

            // Add the first three cities to the ant
            for(int i=0; i<3; i++)
            {
                testAnt.visitedCities.Add(fullCityList[i]);
            }

            // The last two cities should now be returned
            Assert.AreEqual<int>(testAnt.citiesToVisit(fullCityList).Count(), 2, "citiesToVisit did not return the correct number of cities.");
            Assert.AreEqual<bool>(testAnt.citiesToVisit(fullCityList).Contains(fullCityList[3]), true, "citiesToVisit didn't return the first of two cities.");
            Assert.AreEqual<bool>(testAnt.citiesToVisit(fullCityList).Contains(fullCityList[4]), true, "citiesToVisit didn't return the second of two cities.");

        }
    }
}