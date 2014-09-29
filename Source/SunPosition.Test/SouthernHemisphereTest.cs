using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SunPosition.Test
{
    [TestFixture]
    class SouthernHemisphereTest
    {
        private const double Delta = 2;

        [Test]
        public void AutomntTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(-50, 30, new DateTime(2014, 3, 1, 15 , 15, 0), out height, out azimuth);

            Assert.AreEqual(height, 15.66, Delta);
            Assert.AreEqual(azimuth, 275.79, Delta);
        }



    }
}
