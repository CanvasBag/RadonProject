using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoMathLib
{

    public static class SIUnits
    {
        /// <summary>
        /// Transformação de graus decimais para radianos
        /// </summary>
        public const Double Deg2Rad = Math.PI / 180;

        /// <summary>
        /// Transformação de segundos para radianos
        /// </summary>
        public const Double Seg2Rad = 1 / 206264.806;

        /// <summary>
        /// Número em partes por milhão (1e-6)
        /// </summary>
        public const Double PPM = 1e-6;

        /// <summary>
        /// Número em partes por bilião (1e-9)
        /// </summary>
        public const Double PPB = 1e-9;

        public const Double Gon2Rad = Math.PI / 200;


        /// <summary>
        /// Transformação de gruas decimais para DMS (Degree Minuts & Seconds)
        /// </summary>
        /// <param name="degIn"></param>
        /// <returns></returns>
        public static Tuple<int, int, Double> DMS(Double degIn)
        {
            int d = (int)degIn;
            int m = (int)(degIn % 1) * 60;
            Double s = (((degIn % 1) * 60) % 1) * 60;
            return Tuple.Create(d, m, s);
        }

        /// <summary>
        /// Transformação de gruas decimais para DMS 
        /// </summary>
        /// <param name="dms"></param>
        /// <returns></returns>
        public static Double Degree(Tuple<int, int, Double> dms)
        {
            if (dms.Item1>0)
                return dms.Item1 + dms.Item2 / 60.0 + dms.Item3 / 3600.0;
            else
                return dms.Item1 - dms.Item2 / 60.0 - dms.Item3 / 3600.0;
        }
    }
}
