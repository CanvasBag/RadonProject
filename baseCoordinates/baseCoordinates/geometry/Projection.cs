using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCoordinates.Geometry
{
    /// <summary>
    /// Parâmetros de projecção: 
    /// Latitude Origem de Projecção
    /// Longitude Origem de Projecção
    /// False East
    /// False Northing
    /// k0 - coeficiente de deformação dos comprimentos
    /// </summary>
    public class Projection
    {
        private Double latOrigemProj;
        private Double longOrigemProj;
        private Double falseEast;
        private Double falseNorthing;
        private Double k0;

        /// <summary>
        /// define um objecto Projection
        /// </summary>
        /// <param name="longOrigemProj_">Longitude de Origem de Projecção</param>
        /// <param name="latOrigemProj_">Latitude de Origem de Projecção</param>
        /// <param name="k0_">coeficiente de deformação dos comprimentos</param>
        public Projection(Double longOrigemProj_, Double latOrigemProj_, Double k0_, Double falseEast_, Double falseNorthing_)
        {
            latOrigemProj = latOrigemProj_;
            longOrigemProj = longOrigemProj_;
            falseEast = falseEast_;
            falseNorthing = falseNorthing_;
            k0 = k0_;
        }

        /// <summary>
        /// define e retorna a longitude de origem da projecção
        /// </summary>
        public Double LongOrigem
        {
            get { return longOrigemProj; }
            set { longOrigemProj = value; }
        }

        /// <summary>
        /// define e retorna a latitude de origem da projecção
        /// </summary>
        public Double LatOrigem
        {
            get { return latOrigemProj; }
            set { latOrigemProj = value; }
        }

        /// <summary>
        /// define e retorna o valor de falsa origem componente Este
        /// </summary>
        public Double FalseEasting
        {
            get { return falseEast; }
            set { falseEast = value; }
        }

        /// <summary>
        /// define e retorna o valor de falsa origem componente Norte
        /// </summary>
        public Double FalseNorthing
        {
            get { return falseNorthing; }
            set { falseNorthing = value; }
        }

        /// <summary>
        /// define e retorna o coeficiente de deformação dos comprimentos
        /// </summary>
        public Double K0
        {
            get { return k0; }
            set { k0 = value; }
        }        
    }
}
