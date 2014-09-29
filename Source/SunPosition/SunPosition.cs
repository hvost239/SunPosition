using System;
using System.Data;

namespace SunPosition
{
    public static class SunPosition
    {
        /// <summary>
        /// Расчитываем солнечное склонение по дате и времени
        /// </summary>
        /// <param name="date">дата и время</param>
        /// <returns>солнечное склонение в градусах</returns>
        public static double GetSunDeclination(DateTime date)
        {
            // http://en.wikipedia.org/wiki/Ecliptic
            // ε = 23°26′21,448″ — 46,8150″ t — 0,00059″ t² + 0,001813″ t³
            double T = (date.ToJulian() -  JulianDataTime.J2000)/36525;
            double obliquity = 23.4392911 - (46.8150 / 3600.0) * T - (0.00059 / 3600) * T * T + (0.001813 / 3600) * T * T * T;


            //http://en.wikipedia.org/wiki/Position_of_the_Sun
            return -obliquity * Math.Cos((360.0 * (date.DayOfYear + 9) / 365.0).ToRadians());
        }


        /// <summary>
        /// Преобразование из экваториальных координат в горизонтальные.
        /// Вычисляет восоту и азимут, по широту, склонени и часовому уголу.
        /// http://crydee.sai.msu.ru/ak4/Bakulin_1_29.htm
        /// </summary>
        /// <param name="lat">широта в градусах</param>
        /// <param name="decl">склонение в градусах</param>
        /// <param name="hAngle">часовой угол в градусах</param>
        /// <param name="height">высота в градусах</param>
        /// <param name="azimuth">азимут в градусах</param>
        public static void GetHorFromEqu(double lat, double decl, double hAngle, out double height, out double azimuth)
        {
            var latR = lat.ToRadians();
            var declR = decl.ToRadians();
            var hAngleR = hAngle.ToRadians();

            var heightR = Math.Asin(Math.Sin(latR) * Math.Sin(declR) + Math.Cos(latR) * Math.Cos(declR) * Math.Cos(hAngleR));

            var azimutR =Math.Acos((Math.Sin(declR) * Math.Cos(latR) - Math.Cos(declR) * Math.Sin(latR) * Math.Cos(hAngleR)) / Math.Cos(heightR));

            azimuth = (hAngle < 0) ? azimutR.ToDegree() : 360 - azimutR.ToDegree();

            height = heightR.ToDegree();

        }


        /// <summary>
        /// Получаем положение солнца в заивисимости от наблюдателя и времени
        /// </summary>
        /// <param name="lat">широта в градусах</param>
        /// <param name="lon">долгота в градусах</param>
        /// <param name="date">дата и время</param>
        /// <param name="height">высота в градусах</param>
        /// <param name="azimuth">азимут в градусах</param>
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
        /// <param name="date">дата и время</param>
        /// <param name="lon">долгота в градсуах</param>
        /// <returns>Часовой угол в градусах</returns>
        private static double GetHoursAngle(DateTime date, double lon)
        {
            // https://ru.wikipedia.org/wiki/%D0%A3%D1%80%D0%B0%D0%B2%D0%BD%D0%B5%D0%BD%D0%B8%D0%B5_%D0%B2%D1%80%D0%B5%D0%BC%D0%B5%D0%BD%D0%B8
            // https://ru.wikipedia.org/wiki/%D0%9C%D0%B5%D1%81%D1%82%D0%BD%D0%BE%D0%B5_%D1%81%D0%BE%D0%BB%D0%BD%D0%B5%D1%87%D0%BD%D0%BE%D0%B5_%D0%B2%D1%80%D0%B5%D0%BC%D1%8F
            
            var B = (360.0*(date.DayOfYear - 81)/365.0).ToRadians();
            double equationOfTime = 9.87 * Math.Sin(2*B) - 7.53*Math.Cos(B) - 1.5*Math.Sin(B); // уравнение времени


            double localTime = date.Hour + date.Minute / 60.0 + date.Second/3600.0;

            // http://crydee.sai.msu.ru/ak4/Bakulin_1_19.htm
            // http://crydee.sai.msu.ru/ak4/Bakulin_1_22.htm
            // среднее солнечное время в часах
            var localSolarTime = localTime + equationOfTime/60 + 4*lon/60;

            // Один час 15 грудусов. 0 Считается на юге.
            return 15*(localSolarTime - 12);
        }
    }
}
