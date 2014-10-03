using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCoordinates.Geometry
{
    /// <summary>
    /// Definição do elipsoide
    /// </summary>
    public class Elipsoid
    {
        private Double a;
        private Double b;
        private Double excentr2_1;
        private Double excentr2_2;
        private Double f;

        /// <summary>
        /// definição do datum
        /// </summary>
        /// <param name="a_">semieixo maior</param>
        /// <param name="f_">achatamento</param>
        public Elipsoid(Double a_, Double f_)
        {
            a = a_;
            f = f_;
            b = a - f * a;
            excentr2_1 = (a * a - b * b) / (a * a);
            excentr2_2 = (a * a - b * b) / (b * b);
        }

        /// <summary>
        /// define e retorna o valor do semieixo maior
        /// </summary>
        public Double A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// define e retorna o valor do semieixo menor
        /// </summary>
        public Double B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// define e retorna o valor do achatamento
        /// </summary>
        public Double F
        {
            get { return f; }
            set { f = value; }
        }

        /// <summary>
        /// define e retorna o valor do quadrado da primeira excentricidade
        /// </summary>
        public Double E2_1
        {
            get { return excentr2_1; }
            set { excentr2_1 = value; }
        }

        /// <summary>
        /// define e retorna o valor do quadrado da segunda excentricidade
        /// </summary>
        public Double E2_2
        {
            get { return excentr2_2; }
            set { excentr2_2 = value; }
        }


        /// <summary>
        /// retorna o valor do raio de curvatura do Meridiano (M)
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public Double getR_M(Double latitude)
        {
            return a * (1 - excentr2_1) / Math.Pow(1 - excentr2_1 * Math.Sin(latitude) * Math.Sin(latitude), 3 / 2);
        }

        /// <summary>
        /// retorna o valor do raio de curvatura do 1º vertical (N)
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public Double getR_N(Double latitude)
        {
            return a/Math.Sqrt(1-excentr2_1*Math.Pow(Math.Sin(latitude),2));
        }

        /// <summary>
        /// retorna o valor de raio de curvatura do Paralelo
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public Double getR_P(Double latitude)
        {
            return getR_N(latitude) * Math.Cos(latitude);
        }

        /// <summary>
        /// retorna o valor do raio de curvatura da secção alpha
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public Double getR_alpha(Double alpha, Double latitude)
        {
            return getR_M(latitude) * getR_N(latitude) / (getR_N(latitude) * Math.Pow(Math.Cos(alpha), 2) + getR_M(latitude) * Math.Pow(Math.Sin(alpha), 2));
        }
    }
}
