using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCoordinates.Geometry;
using BaseCoordinates.Seed;
using BaseCoordinates.Elements;
using MathNet.Numerics.LinearAlgebra;

namespace GeoMathLib.Projections
{
    public static class TransverseMercator
    {   
        /// <summary>
        /// Transversa de Mercator inversa
        /// </summary>
        /// <param name="coordMPH">GeoCoord de coordenadas M, P e H</param>
        /// <returns>GeoCoord de coordenadas Longitude, Latitude & Height</returns>
        public static GeoCoord GaussInversa(GeoCoord coordMPH)
        {
            List<EastingNorthing> listMpTMp = coordMPH.EastingNorthingList;
            List<Ll> listLlTMp = new List<Ll>();

            foreach (EastingNorthing coordenadasMpTMp in listMpTMp)
                listLlTMp.Add(GaussInversa(coordenadasMpTMp, coordMPH.Datum, coordMPH.Projection));

            GeoCoord coordGeodTMp = new GeoCoord(listLlTMp, coordMPH.Datum);

            return coordGeodTMp;
        }

        /// <summary>
        /// Transversa de Mercator inversa
        /// </summary>
        /// <param name="coordMPH"></param>
        /// <param name="datumIn"></param>
        /// <param name="projectionIn"></param>
        /// <returns></returns>
        public static Ll GaussInversa(EastingNorthing coordMPH, Datum datumIn, Projection projectionIn)
        {
            Ll pointllOut = new Ll();

            double[] sigma = new double[2];
            double fi, N, latO, logO, k0, f, e2, a;
            double ro = 0;
            double deltaFi = 1, t, psi;

            
            double m = coordMPH.E - projectionIn.FalseEasting;
            double p = coordMPH.N - projectionIn.FalseNorthing;
            latO = projectionIn.LatOrigem;
            logO = projectionIn.LongOrigem;
            a = datumIn.Elipsoid.A;
            f = datumIn.Elipsoid.F;
            e2 = datumIn.Elipsoid.E2_1;
            k0 = projectionIn.K0;
            sigma[0] = p / k0;
            fi = latO + sigma[0] / (a * (1 - e2));

            while (Math.Abs(deltaFi) > 1e-10)
            {

                sigma[1] = arcMeridiano(latO, fi, datumIn.Elipsoid);
                ro = a * (1 - e2) / Math.Pow(1 - e2 * Math.Pow(Math.Sin(fi), 2), 1.5);
                deltaFi = (sigma[0] - sigma[1]) / ro;
                fi = fi + deltaFi;
            }

            t = Math.Tan(fi);
            N = a / Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(fi), 2));
            psi = N / ro;
            pointllOut.Long = (m / (k0 * N) - Math.Pow(m, 3) / (6 * Math.Pow(k0, 3) * Math.Pow(N, 3)) * (psi + 2 * t * t)
                            + (Math.Pow(m, 5) / (120 * Math.Pow(k0, 5) * Math.Pow(N, 5))) * (-4 * Math.Pow(psi, 3) * (1 - 6 * t * t) +
                              Math.Pow(psi, 2) * (9 - 68 * t * t) + 72 * psi * t * t + 24 * Math.Pow(t, 4))
                            - Math.Pow(m, 7) / (5040 * Math.Pow(k0, 7) * Math.Pow(N, 7)) * (61 + 662 * t * t + 1320 * Math.Pow(t, 4) + 720 * Math.Pow(t, 6))) / Math.Cos(fi)
                            + logO;

            pointllOut.Lat = fi - (t / (k0 * ro)) * (m * m / (2 * k0 * N)
                            - (Math.Pow(m, 4) / (24 * Math.Pow(k0, 3) * Math.Pow(N, 3))) * (-4 * Math.Pow(psi, 2) + 9 * psi * (1 - t * t) + 12 * t * t)
                            + (Math.Pow(m, 6) / (720 * Math.Pow(k0, 5) * Math.Pow(N, 5))) * (8 * Math.Pow(psi, 4) * (11 - 24 * t * t) -
                              12 * Math.Pow(psi, 3) * (21 - 71 * t * t) + 15 * Math.Pow(psi, 2) * (15 - 98 * t * t + 15 * Math.Pow(t, 4)) +
                              180 * psi * (5 * Math.Pow(t, 2) - 3 * Math.Pow(t, 4)) - 360 * Math.Pow(t, 4))
                            - (Math.Pow(m, 8) / (40320 * Math.Pow(k0, 7) * Math.Pow(N, 7))) * (1385 + 3633 * t * t + 4095 * Math.Pow(t, 4) + 1575 * Math.Pow(t, 6)));
            

            pointllOut.H = coordMPH.Z;
            pointllOut.ID = coordMPH.ID;

            return pointllOut;
        }

        /// <summary>
        /// Aplica da projecção Transversa de Mercator
        /// </summary>
        /// <param name="coordLLIn"></param>
        /// <param name="projeccaoIn"></param>
        /// <returns></returns>
        public static GeoCoord GaussProjection(GeoCoord coordLLIn, Projection projeccaoIn)
        {
            GeoCoord coordMpTMp = new GeoCoord();

            foreach (Ll ptoLLtMp in coordLLIn.LlList)            
                coordMpTMp.addENPoint(GaussProjection(ptoLLtMp, coordLLIn.Datum, projeccaoIn));
            

            coordMpTMp.Projection = projeccaoIn;
            return coordMpTMp;
        }

        /// <summary>
        /// Aplica da projecção Transversa de Mercator
        /// </summary>
        /// <param name="coordLLIn"></param>
        /// <param name="datumIn"></param>
        /// <param name="projeccaoIn"></param>
        /// <returns></returns>
        public static EastingNorthing GaussProjection(Ll coordLLIn, Datum datumIn, Projection projeccaoIn)
        {
            EastingNorthing ptoMptMp = new EastingNorthing();

            double e2 = datumIn.Elipsoid.E2_1;
            double a = datumIn.Elipsoid.A;
            double f = datumIn.Elipsoid.F;
            double k0 = projeccaoIn.K0;
            double N, ro, sigma;
            Vector kap = new Vector(6);
            double lambda = coordLLIn.Long - projeccaoIn.LongOrigem;

            N = a / Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(coordLLIn.Lat), 2));
            ro = a * (1 - e2) / Math.Pow(1 - e2 * Math.Pow(Math.Sin(coordLLIn.Lat), 2), 1.5);

            kap[0] = N / ro - Math.Pow(Math.Tan(coordLLIn.Lat), 2);
            kap[1] = N / ro + 4 * Math.Pow(N / ro, 2) - Math.Pow(Math.Tan(coordLLIn.Lat), 2);
            kap[2] = 4 * Math.Pow(N / ro, 3) * (1 - 6 * Math.Pow(Math.Tan(coordLLIn.Lat), 2)) +
                    Math.Pow(N / ro, 2) * (1 + 8 * Math.Pow(Math.Tan(coordLLIn.Lat), 2)) - 2 * N / ro * Math.Pow(Math.Tan(coordLLIn.Lat), 2) +
                    Math.Pow(Math.Tan(coordLLIn.Lat), 4);
            kap[3] = 8 * Math.Pow(N / ro, 4) * (11.0 - 24.0 * Math.Pow(Math.Tan(coordLLIn.Lat), 2)) - 28.0 * Math.Pow(N / ro, 3) * (1.0 - 6 * Math.Pow(Math.Tan(coordLLIn.Lat), 2))
                    + Math.Pow(N / ro, 2) * (1.0 - 32.0 * Math.Pow(Math.Tan(coordLLIn.Lat), 2)) - 2 * (N / ro) * Math.Pow(Math.Tan(coordLLIn.Lat), 2) + Math.Pow(Math.Tan(coordLLIn.Lat), 4);
            kap[4] = 61.0 - 479.0 * Math.Pow(Math.Tan(coordLLIn.Lat), 2) + 179 * Math.Pow(Math.Tan(coordLLIn.Lat), 4) - Math.Pow(Math.Tan(coordLLIn.Lat), 6);
            kap[5] = 1385.0 - 3111.0 * Math.Pow(Math.Tan(coordLLIn.Lat), 2) + 543 * Math.Pow(Math.Tan(coordLLIn.Lat), 4) + Math.Pow(Math.Tan(coordLLIn.Lat), 6);

            sigma = arcMeridiano(projeccaoIn.LatOrigem, coordLLIn.Lat, datumIn.Elipsoid);

            ptoMptMp.E = k0 * (sigma + N * Math.Sin(coordLLIn.Lat) * ((Math.Pow(lambda, 2) / 2.0) * Math.Cos(coordLLIn.Lat) +
                        (Math.Pow(lambda, 4) / 24.0) * Math.Pow(Math.Cos(coordLLIn.Lat), 3) * kap[1] +
                        (Math.Pow(lambda, 6) / 720.0) * Math.Pow(Math.Cos(coordLLIn.Lat), 5) * kap[3] +
                        (Math.Pow(lambda, 8) / 40320.0) * Math.Pow(Math.Cos(coordLLIn.Lat), 7) * kap[5]));

            ptoMptMp.N = k0 * (lambda * N * Math.Cos(coordLLIn.Lat)
                       + Math.Pow(lambda, 3) / 6.0 * N * Math.Pow(Math.Cos(coordLLIn.Lat), 3) * kap[0]
                       + Math.Pow(lambda, 5) / 120.0 * N * Math.Pow(Math.Cos(coordLLIn.Lat), 5) * kap[2]
                       + Math.Pow(lambda, 7) / 5040.0 * N * Math.Pow(Math.Cos(coordLLIn.Lat), 7) * kap[4]);

            ptoMptMp.E = ptoMptMp.E + projeccaoIn.FalseNorthing;
            ptoMptMp.N = ptoMptMp.N + projeccaoIn.FalseEasting;            
            ptoMptMp.Z = coordLLIn.H;
            ptoMptMp.ID = coordLLIn.ID;

            return ptoMptMp;
        }

        /// <summary>
        /// Calcula o arco de meridiano entre duas latitudes
        /// </summary>
        /// <param name="lat1">Latitude 1</param>
        /// <param name="lat2">Latitude 2</param>
        /// <param name="elipsoidIn">Elipsoide de referência</param>
        /// <returns>Valor do arco em radianos</returns>
        public static double arcMeridiano(double lat1, double lat2, Elipsoid elipsoidIn)
        {
            double e2, f, a;
            double sigma, A, B, C, D, E, F;

            e2 = elipsoidIn.E2_1;
            a = elipsoidIn.A;
            f = elipsoidIn.F;

            A = 1 + 3.0 / 4 * e2 + 45.0 / 64 * Math.Pow(e2, 2) + 175.0 / 256 * Math.Pow(e2, 3) + 11025.0 / 16384 * Math.Pow(e2, 4) +
            43659.0 / 65536 * Math.Pow(e2, 5);
            B = 3.0 / 4 * e2 + 15.0 / 16 * Math.Pow(e2, 2) + 525.0 / 512 * Math.Pow(e2, 3) + 2205.0 / 2048 * Math.Pow(e2, 4) +
                72765.0 / 65536 * Math.Pow(e2, 5);
            C = 15.0 / 64 * Math.Pow(e2, 2) + 105.0 / 256 * Math.Pow(e2, 3) + 2205.0 / 4096 * Math.Pow(e2, 4) + 10395.0 / 16384 * Math.Pow(e2, 5);
            D = 35.0 / 512 * Math.Pow(e2, 3) + 315.0 / 2048 * Math.Pow(e2, 4) + 31185.0 / 131072 * Math.Pow(e2, 5);
            E = 315.0 / 16384 * Math.Pow(e2, 4) + 3465.0 / 65536 * Math.Pow(e2, 5);
            F = (693.0 / 131072) * Math.Pow(e2, 5);

            sigma = a * (1 - e2) * (A * (lat2 - lat1)
                - B / 2 * (Math.Sin(2 * lat2) - Math.Sin(2 * lat1))
                + C / 4 * (Math.Sin(4 * lat2) - Math.Sin(4 * lat1))
                - D / 6 * (Math.Sin(6 * lat2) - Math.Sin(6 * lat1))
                + E / 8 * (Math.Sin(8 * lat2) - Math.Sin(8 * lat1))
                - F / 10 * (Math.Sin(10 * lat2) - Math.Sin(10 * lat1)));

            return sigma;
        }
    }   
}
