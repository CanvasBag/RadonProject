using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;
using BaseCoordinates.Seed;

namespace GeoMathLib.Transformations
{
    /// <summary>
    /// Transform coordinates from geodetic to geocentric and vice versa (Forward & Reverse)
    /// </summary>
    public static class Geocentric
    {
        /// <summary>
        /// Convert from geodetic to geocentric coordinates (mantains the datum)
        /// </summary>
        /// <param name="coordGeod">GeoCoord object with geodetic coordinates</param>
        /// <Returns>GeoCoord object with geocentric coordinates</Returns>
        public static GeoCoord Forward(GeoCoord coordGeod)
        {
            if (coordGeod.Datum != null)
            {                
                GeoCoord coordRetTmp = new GeoCoord();
                double logTmp, latTmp, hTmp;

                for (int i = 0; i < coordGeod.LlList.Count; i++)
                {
                    Ret ptoRetTmp = new Ret();

                    logTmp = coordGeod.LlList.ElementAt<Ll>(i).Long;
                    latTmp = coordGeod.LlList.ElementAt<Ll>(i).Lat;
                    hTmp = coordGeod.LlList.ElementAt<Ll>(i).H;

                    ptoRetTmp.ID = coordGeod.LlList.ElementAt<Ll>(i).ID;
                    ptoRetTmp.ListDadosDivD = coordGeod.LlList.ElementAt<Ll>(i).ListDadosDivD;
                    ptoRetTmp.ListDadosDivS = coordGeod.LlList.ElementAt<Ll>(i).ListDadosDivS;

                    double N = coordGeod.Datum.Elipsoid.A / Math.Sqrt(1 - coordGeod.Datum.Elipsoid.E2_1 * Math.Pow(Math.Sin(latTmp), 2));
                    ptoRetTmp.X = (N + hTmp) * Math.Cos(latTmp) * Math.Cos(logTmp);
                    ptoRetTmp.Y = (N + hTmp) * Math.Cos(latTmp) * Math.Sin(logTmp);
                    ptoRetTmp.Z = (N * (1 - coordGeod.Datum.Elipsoid.E2_1) + hTmp) * Math.Sin(latTmp);
                    coordRetTmp.addRetPoint(ptoRetTmp);
                }

                coordRetTmp.Datum = coordGeod.Datum;
                return coordRetTmp;
            }
            else
                throw new MissingFieldException("GeoCoord", "Datum");
        }

        /// <summary>
        /// Convert from geocentric to geodetic coordinates (mantains the datum)
        /// </summary>
        /// <param name="coordRet"></param>
        /// <Returns></Returns>
        public static GeoCoord Reverse(GeoCoord coordRet)
        {
            if (coordRet.Datum != null)
            {
                GeoCoord coordLlTmp = new GeoCoord();
                double xTmp, yTmp, zTmp, latTmp, N;

                for (int i = 0; i < coordRet.RetList.Count; i++)
                {
                    Ll ptoLlTmp = new Ll();
                    xTmp = coordRet.RetList.ElementAt<Ret>(i).X;
                    yTmp = coordRet.RetList.ElementAt<Ret>(i).Y;
                    zTmp = coordRet.RetList.ElementAt<Ret>(i).Z;
                    ptoLlTmp.ID = coordRet.RetList.ElementAt<Ret>(i).ID;
                    ptoLlTmp.ListDadosDivD = coordRet.RetList.ElementAt<Ret>(i).ListDadosDivD;
                    ptoLlTmp.ListDadosDivS = coordRet.RetList.ElementAt<Ret>(i).ListDadosDivS;

                    if (xTmp > 0)
                        ptoLlTmp.Long = Math.Atan(yTmp / xTmp);
                    else if (xTmp < 0 && yTmp > 0)
                    {
                        ptoLlTmp.Long = Math.Atan(yTmp / xTmp) + Math.PI;
                    }
                    else if (xTmp < 0 && yTmp < 0)
                    {
                        ptoLlTmp.Long = Math.Atan(yTmp / xTmp) - Math.PI;
                    }

                    else if (xTmp == 0)
                        ptoLlTmp.Long = 0;

                    ptoLlTmp.Lat = Math.Atan(zTmp / ((1 - coordRet.Datum.Elipsoid.E2_1) * Math.Sqrt(xTmp * xTmp + yTmp * yTmp)));
                    latTmp = ptoLlTmp.Lat + 1;

                    //Determinar a latitude
                    while (Math.Abs(latTmp - ptoLlTmp.Lat) > Math.Pow(10, -10))
                    {
                        latTmp = ptoLlTmp.Lat;
                        N = coordRet.Datum.Elipsoid.A / Math.Sqrt(1 - coordRet.Datum.Elipsoid.E2_1 * Math.Pow(Math.Sin(latTmp), 2));
                        ptoLlTmp.Lat = Math.Atan(zTmp / Math.Sqrt(xTmp * xTmp + yTmp * yTmp) * (1 + coordRet.Datum.Elipsoid.E2_1 * N * Math.Sin(latTmp) / zTmp));
                    }

                    N = coordRet.Datum.Elipsoid.A / Math.Sqrt(1 - coordRet.Datum.Elipsoid.E2_1 * Math.Pow(Math.Sin(ptoLlTmp.Lat), 2));
                    ptoLlTmp.H = zTmp / Math.Sin(ptoLlTmp.Lat) - N + coordRet.Datum.Elipsoid.E2_1 * N;


                    coordLlTmp.addllPoint(ptoLlTmp);
                }
                coordLlTmp.Datum = coordRet.Datum;
                return coordLlTmp;
            }
            else
                throw new MissingFieldException("GeoCoord", "Datum");
        }
    }
}
