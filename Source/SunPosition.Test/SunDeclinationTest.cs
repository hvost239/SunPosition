
using System;
using NUnit.Framework;

namespace SunPosition.Test
{
    [TestFixture]
    public class SunDeclinationTest
    {
        private const double Delta = 1;

        [Test]
        public void MidsummerTest()
        {  
            Assert.AreEqual(SunPosition.GetSunDeclination(new DateTime(2014,6,22)), 23.0 + 27.0 / 60, Delta);
        }

        [Test]
        public void MidwinterTest()
        {
            Assert.AreEqual(SunPosition.GetSunDeclination(new DateTime(2013,12,22)), -23.0 - 27.0 / 60, Delta);
        }

        [Test]
        public void AutumnalEquinoxTest()
        {   
            Assert.AreEqual(SunPosition.GetSunDeclination(new DateTime(2014,9,22,2,29,0)), 0, Delta);
        }

        [Test]
        public void VernalEquinoxTest()
        {
            Assert.AreEqual(SunPosition.GetSunDeclination(new DateTime(2012, 3, 21)), 0, Delta);
        }


    }
}
