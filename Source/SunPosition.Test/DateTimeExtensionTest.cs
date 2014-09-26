using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SunPosition.Test
{
    [TestFixture]
    public class DateTimeExtension
    {   
        [Test]
        public void MillinumTest()
        {
            var date = new DateTime(2000, 1, 1, 12, 0, 0);  
            Assert.AreEqual(date.ToJulian(), 2451545);
        }

        [Test]
        public void SomeTest()
        {
            var date = new DateTime(1942, 8, 2, 15, 0, 0);
            Assert.AreEqual(date.ToJulian(), 2430574.125);
        }
    }
}
