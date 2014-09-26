using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SunPosition.Test
{
    [TestFixture]
    class SPBTest
    {
        public double Delta = 2;

        [Test]
        public void BerthDayTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(60, 30, new DateTime(2014, 12, 21, 20, 0, 0), out height, out azimuth);

            Assert.AreEqual(height, -49.2412, Delta);
            Assert.AreEqual(azimuth, 322.2894, Delta);
        }

        [Test]
        public void NYTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(60, 30, new DateTime(2014, 12, 21, 20, 0, 0), out height, out azimuth);

            Assert.AreEqual(height, -48.0327, Delta);
            Assert.AreEqual(azimuth, 317.4155, Delta);
        }

        [Test]
        public void SEDayTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(60, 30, new DateTime(2014, 9, 13, 6, 0, 0), out height, out azimuth);

            Assert.AreEqual(height, 18.3266, Delta);
            Assert.AreEqual(azimuth, 115.7067, Delta);
        }
    }
}
