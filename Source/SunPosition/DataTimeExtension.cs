using System;

namespace SunPosition
{
    /// <summary>
    /// Раширение для преобразования даты в юлианское число
    /// </summary>
    public static class DataTimeExtension
    {
        //https://ru.wikipedia.org/wiki/%D0%AE%D0%BB%D0%B8%D0%B0%D0%BD%D1%81%D0%BA%D0%B0%D1%8F_%D0%B4%D0%B0%D1%82%D0%B0
        public static double ToJulian(this DateTime date)
        {
            int a = (14 - date.Month)/12;
            int y = date.Year + 4800 - a;
            int m = date.Month + 12*a - 3;
            double jdn = date.Day + (153*m + 2)/5 + 365*y + y/4 - y/100 + y/400 - 32045;
            return jdn + (date.Hour - 12)/24.0 + date.Minute/1440.0 + date.Second/86400.0;
        }
    }
}