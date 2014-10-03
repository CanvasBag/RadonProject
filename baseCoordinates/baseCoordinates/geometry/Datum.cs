using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCoordinates.Geometry
{
    /// <summary>
    /// Classe datum que contém as componentes:
    /// Desvio da vertical (E/N)
    /// Ondulação do geóide na origem
    /// Latitude Origem
    /// Longitude Origem
    /// Diferença de Az astronómico com geodésico 
    /// Elipsoide de Ref (objecto Elipsoid)
    /// </summary>
    public class Datum
    {
        private Elipsoid elipsd;
        private Double desvVert_N;
        private Double desvVert_E;
        private Double N_zero;
        private Double delta_Az;
        private Double lat_Origem;
        private Double long_Origem;

        /// <summary>
        /// define um objecto Datum
        /// </summary>
        /// <param name="elipsoidIn"></param>
        /// <param name="longOrigem"></param>
        /// <param name="latOrigem"></param>
        public Datum(Elipsoid elipsoidIn, Double longOrigem, Double latOrigem)
        {
            elipsd = elipsoidIn;
            lat_Origem = latOrigem;
            long_Origem = longOrigem;
        }

        /// <summary>
        /// define um objecto Datum
        /// </summary>
        /// <param name="elipsoidIn"></param>
        public Datum(Elipsoid elipsoidIn)
        {
            elipsd = elipsoidIn;
        }

        /// <summary>
        /// define e retorna o valor da ondulação do geóide na origem
        /// </summary>
        public Double NZero
        {
            get { return N_zero; }
            set { N_zero = value; }
        }

        /// <summary>
        /// define e retorna o valor da longitude na origem
        /// </summary>
        public Double LongOrigem
        {
            get { return long_Origem; }
            set { long_Origem = value; }
        }

        /// <summary>
        /// define e retorna o valor da latitude na origem
        /// </summary>
        public Double LatOrigem
        {
            get { return lat_Origem; }
            set { lat_Origem = value; }
        }

        /// <summary>
        /// define e retorna o valor do desvio da vertical na origem na componente Norte
        /// </summary>
        public Double DesvVert_N
        {
            get { return desvVert_N; }
            set { desvVert_N = value; }
        }

        /// <summary>
        /// define e retorna o valor do desvio da vertical na origem na componente Este
        /// </summary>
        public Double DesvVert_E
        {
            get { return desvVert_E; }
            set { desvVert_E = value; }
        }

        /// <summary>
        /// define e retorna o valor da diferença entre o Az Astronómico com o Az Geodésico
        /// </summary>
        public Double Delta_Az
        {
            get { return delta_Az; }
            set { delta_Az = value; }
        }

        /// <summary>
        /// define e retorna o elipsoide 
        /// </summary>
        public Elipsoid Elipsoid
        {
            get { return elipsd; }
            set { elipsd = value; }
        }

    }
}
