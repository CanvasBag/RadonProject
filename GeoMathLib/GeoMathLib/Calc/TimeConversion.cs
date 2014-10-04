using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using baseTime.Elements;
using baseTime.Seed;

namespace GeoMathLib.Calc
{
    /// <summary>
    /// (PT) Class de conversão de diferentes tipos de datas
    /// (EN) Class to convert between diferent date types
    /// </summary>
    public static class TimeConversion
    {
        /// <summary>
        /// (PT) Cálculo da data juliana a partir de data gregoriano pelo método de Langley
        /// (EN) Caculate julian date from grogorian date using Langley method
        /// </summary>
        /// <param name="gregorianDate">Gregorian Date: Year, month and day</param>
        /// <returns>Copy of DateTimer ID to JulianDate Obj</returns>
        public static JulianDate JD_Langley(DateTimer gregorianDate)
        {
            int ano = gregorianDate.Date.Year;
            int mes = gregorianDate.Date.Month;
            int dia = gregorianDate.Date.Day;
            JulianDate jdTmp = new JulianDate();

            jdTmp.JD = 367 * ano - 7 * (ano + (mes + 9) / 12) / 4 - 3 * ((ano + (mes - 9) / 7) / 100 + 1) / 4 + 275 * mes / 9 + dia + 1721029;
            jdTmp.ID = gregorianDate.ID;
            return jdTmp;
        }

        /// <summary>
        /// (PT) Cálculo da data juliana a partir de data gregoriano pelo método de Langley
        /// (EN) Caculate julian date from grogorian date using Langley method 
        /// </summary>
        /// <param name="gregorianDate"></param>
        /// <returns>Copy of DateTimer ID to JulianDate Obj</returns>
        public static GeoTime JD_Langley(GeoTime gregorianDate)
        {
            GeoTime gtTemp = new GeoTime();
            foreach (DateTimer dtTmp in gregorianDate.ListDateTimer)
            {
                gtTemp.ListJulianDate.Add(JD_Langley(dtTmp));
            }
            return gtTemp;
        }

        /// <summary>
        /// (PT) Cálculo da data juliana a partir de data gregoriano pelo método de Leick
        /// (EN) Caculate julian date from grogorian date using Leick method 
        /// </summary>
        /// <param name="gregorianDate">Gregorian Date: Year, month and day</param>
        /// <returns>Copy of DateTimer ID to JulianDate Obj</returns>
        public static JulianDate JD_Leick(DateTimer gregorianDate)
        {
            int ano = gregorianDate.Date.Year;
            int mes = gregorianDate.Date.Month;
            int dia = gregorianDate.Date.Day;
            JulianDate jdTmp = new JulianDate();

            jdTmp.JD = 367 * ano - 7 * (ano + (mes + 9) / 12) / 4 + 275 * mes / 9 + dia + 1721014;
            jdTmp.ID = gregorianDate.ID;
            return jdTmp;
        }

        /// <summary>
        /// (PT) Cálculo da data juliana a partir de data gregoriano pelo método de Leick
        /// (EN) Caculate julian date from grogorian date using Leick method 
        /// </summary>
        /// <param name="gregorianDate"></param>
        /// <returns>Copy of DateTimer ID to JulianDate Obj</returns>
        public static GeoTime JD_Leick(GeoTime gregorianDate)
        {
            GeoTime gtTemp = new GeoTime();
            foreach (DateTimer dtTmp in gregorianDate.ListDateTimer)
            {
                gtTemp.ListJulianDate.Add(JD_Leick(dtTmp));
            }
            return gtTemp;
        }

        /// <summary>
        /// (PT) Cálculo da data juliana a partir de data gregoriano pelo método de Hoffman
        /// (EN) Caculate julian date from grogorian date using Hoffman method 
        /// </summary>
        /// <param name="gregorianDate">Gregorian Date: Year, month, day, hour and minute</param>
        /// <returns>Copy of DateTimer ID to JulianDate Obj</returns>
        public static JulianDate JD_Hoffman(DateTimer gregorianDate)
        {

            int ano = gregorianDate.Date.Year;
            int mes = gregorianDate.Date.Month;
            int dia = gregorianDate.Date.Day;
            double ut = gregorianDate.Date.Hour + gregorianDate.Date.Minute / 60;
            int y, m;

            y = mes <= 2 ? ano - 1 : ano;
            m = mes <= 2 ? mes + 12 : mes;

            JulianDate jdTmp = new JulianDate();

            jdTmp.JD = (int)(365.25 * y) + (int)(30.6001 * (m + 1)) + dia + ut / 24 + 1720981.5;
            jdTmp.ID = gregorianDate.ID;
            return jdTmp;
        }

        /// <summary>
        /// (PT) Cálculo da data juliana a partir de data gregoriano pelo método de Hoffman
        /// (EN) Caculate julian date from grogorian date using Hoffman method 
        /// </summary>
        /// <param name="gregorianDate"></param>
        /// <returns>Copy of DateTimer ID to JulianDate Obj</returns>
        public static GeoTime JD_Hoffman(GeoTime gregorianDate)
        {
            GeoTime gtTemp = new GeoTime();
            foreach (DateTimer dtTmp in gregorianDate.ListDateTimer)
            {
                gtTemp.ListJulianDate.Add(JD_Hoffman(dtTmp));
            }
            return gtTemp;
        }

        /// <summary>
        /// (PT) Cálculo da data gregoriana através da data juliana
        /// (EN) Calcuate gregorian date from julian date
        /// </summary>
        /// <param name="julianD"></param>
        /// <returns>Copy of JulianDate ID to DateTimer Obj</returns>
        public static DateTimer GregorianDate(JulianDate julianD)
        {
            
            int a = (int)(julianD.JD + .5);
            int b = a + 1537;
            int c = (int)((b - 122.1) / 365.25);
            int d = (int)(365.25 * c);
            int e = (int)((b - d) / 30.6001);

            double dia = b - d - (int)(30.6001 * e) + (julianD.JD + 0.5 - a);
            int mes = e - 1 - 12 * (int)(e / 14);
            int ano = c - 4715 - (int)((7 + mes) / 10);
            double hora = (dia - (int)(dia)) * 24;
            double min = (hora - (int)(hora)) * 60;
            double seg = (min - (int)(min)) * 60;

            DateTime dataTmp = new DateTime(ano, mes, (int)(dia), (int)(hora), (int)(min), (int)(seg));
            DateTimer dtTmp = new DateTimer(dataTmp, julianD.ID);
            return dtTmp;
        }

        /// <summary>
        /// (PT) Cálculo da data gregoriana através da data juliana
        /// (EN) Calcuate gregorian date from julian date
        /// </summary>
        /// <param name="gregorianDate"></param>
        /// <returns>Copy of JulianDate ID to DateTimer Obj</returns>
        public static GeoTime GregorianDate(GeoTime gregorianDate)
        {
            GeoTime jdTemp = new GeoTime();
            foreach (JulianDate dtTmp in gregorianDate.ListJulianDate)
            {
                jdTemp.ListDateTimer.Add(GregorianDate(dtTmp));
            }
            return jdTemp;
        }

        public static WeekGPSTime GPSW(JulianDate julianD)
        {
            double mjd = julianD.JD-2444244.5;
            int gpsw = (int)(julianD.JD-2444244.5)/7;
            double gpss = ((int)mjd - ((gpsw + 349178) * 7 - 2400002) + mjd % 1) * 86400;
            return new WeekGPSTime(gpss, gpsw, julianD.ID);
        }

    }
}
