using System;
using System.Data;

namespace SunPosition
{
    public static class SunPosition
    {
        private const double J2000 = 2451545.0;

        public static double GetSunDeclination(DateTime date)
        {
            // http://en.wikipedia.org/wiki/Ecliptic
            double T = (date.ToJulian() - J2000)/36525;
            // ε = 23°26′21,448″ — 46,8150″ t — 0,00059″ t² + 0,001813″ t³
            double sunDeclination = 23.4392911 - (46.8150/3600.0)*T - (0.00059/3600)*T*T + (0.001813/3600)*T*T*T;


            //http://en.wikipedia.org/wiki/Position_of_the_Sun
            var B = 360.0*(date.DayOfYear + 9)/365.0;
            return -sunDeclination *Math.Cos(B.ToRadians());
        }


        /// <summary>
        /// Преобразование из экваториальных координат в горизонтальные.
        /// Вычисляет восоту и азимут, по широту, склонени и часовому уголу.
        /// http://crydee.sai.msu.ru/ak4/Bakulin_1_29.htm
        /// </summary>
        /// <param name="lat">широта</param>
        /// <param name="decl">склонение</param>
        /// <param name="hAngle">часовой угол</param>
        /// <param name="height">высота</param>
        /// <param name="azimuth">азимут</param>
        /// <returns></returns>
        public static void GetHorFromEqu(double lat, double decl, double hAngle, out double height, out double azimuth)
        {
            height = Math.Asin(Math.Sin(lat.ToRadians()) * Math.Sin(decl.ToRadians()) + Math.Cos(lat.ToRadians()) * Math.Cos(decl.ToRadians()) * Math.Cos(hAngle.ToRadians())).ToDegree();
            
            var a = (Math.Sin(decl.ToRadians())*Math.Cos(lat.ToRadians()) - Math.Cos(decl.ToRadians())*Math.Sin(lat.ToRadians())*Math.Cos(hAngle.ToRadians()))/Math.Cos (height.ToRadians());

            azimuth = a < 1 ? Math.Acos(a).ToDegree() : 0;
        }

        public static void GetSunPosition(double lat, double lon, DateTime date, out double height,
                                          out double azimuth)
        {

            var hoursAngle = GetHoursAngle(date, lon);

            var decl = GetSunDeclination(date);

            GetHorFromEqu(lat, decl,hoursAngle, out height, out azimuth);
        }


        /// <summary>
        /// Вычисление часового угла солнца с учетом коррекции по солнечному времени
        /// </summary>
        /// <param name="date">дата</param>
        /// <param name="lon">долгота</param>
        /// <returns></returns>
        private static double GetHoursAngle(DateTime date, double lon)
        {
            //// https://ru.wikipedia.org/wiki/%D0%A3%D1%80%D0%B0%D0%B2%D0%BD%D0%B5%D0%BD%D0%B8%D0%B5_%D0%B2%D1%80%D0%B5%D0%BC%D0%B5%D0%BD%D0%B8
            var B = (360.0*(date.DayOfYear - 81)/365.0).ToRadians();
            double equationOfTime = 9.87 * Math.Sin(2*B) - 7.53*Math.Cos(B) - 1.5*Math.Sin(B);


            /// http://crydee.sai.msu.ru/ak4/Bakulin_1_23.htm
            double timeCorrection = 4*lon + equationOfTime;


            double localTime = date.Hour + date.Minute / 60.0; // время в минутах
            var localSolarTime = localTime + timeCorrection/60;

            return 15*(localSolarTime - 12);
        }
    }
}
