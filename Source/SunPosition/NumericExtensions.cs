using System;

namespace SunPosition
{
    public static class NumericExtensions
    {
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }

        public static double ToDegree(this double val)
        {
            return (180 / Math.PI ) * val;
        }

        
    }
}