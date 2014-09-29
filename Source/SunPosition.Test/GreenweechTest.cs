using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SunPosition.Test
{
    [TestFixture]
    class GreenweechTest
    {
        private const double Delta = 2;

        [Test]
        public void EquatorTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(0,0,new DateTime(2014, 6,22, 12,0,0,0), out height, out azimuth) ;

            Assert.AreEqual(height, 66.5627, Delta);
            Assert.AreEqual(azimuth, 1.1459, Delta);
        }

        [Test]
        public void EquatorMidnightTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(0, 0, new DateTime(2014, 6, 22, 23, 59, 0, 0), out height, out azimuth);
                
            Assert.AreEqual(height, -66.562, Delta);
            Assert.AreEqual(azimuth, 358.215, Delta);
        }


        [Test]
        public void EnglandTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(60, 0, new DateTime(2014, 6, 22, 12, 0, 0, 0), out height, out azimuth);

            Assert.AreEqual(height, 53.4283, Delta);
            Assert.AreEqual(azimuth, 179.2351, Delta);
        }

        [Test]
        public void EnglandMidnightTest()
        {
            double height;
            double azimuth;
            SunPosition.GetSunPosition(60, 0, new DateTime(2014, 6, 22, 23, 59, 0, 0), out height, out azimuth);

            Assert.AreEqual(height, -6.5731, Delta);
            Assert.AreEqual(azimuth, 359.2854, Delta);
        }
    }
}
