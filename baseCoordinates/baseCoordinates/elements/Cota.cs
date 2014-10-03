using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCoordinates.Elements
{
    public class Height : IComparable<Height>
    {
        private Double h, vel_h, acel_h;
        private Double sig_h, sig_Vh, sig_Ah;
        private List<String> dadosDivS;
        private List<Double> dadosDivD;
        private String id;

        public Height(Double h_, String id_)
        {
            h = h_;
            id = id_;
            dadosDivD = new List<double>();
            dadosDivS = new List<string>();
        }

        public Height(Double h_)
        {
            h = h_;
            dadosDivD = new List<double>();
            dadosDivS = new List<string>();
        }

        public Height()
        {
            dadosDivD = new List<double>();
            dadosDivS = new List<string>();
        }

        /// <summary>
        /// define e retorna o valor de ID
        /// </summary>
        public String ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// define e retorna o valor da coordenada H
        /// </summary>
        public Double H
        {
            get { return h; }
            set { h = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma de H
        /// </summary>
        public Double SigmaH
        {
            get { return sig_h; }
            set { sig_h = value; }
        }

        /// <summary>
        /// define e retorna o valor de velocidade da componente H
        /// </summary>
        public Double Vel_H
        {
            get { return vel_h; }
            set { vel_h = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da velocidade da componente H
        /// </summary>
        public Double Sigma_Vel_H
        {
            get { return sig_Vh; }
            set { sig_Vh = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente H
        /// </summary>
        public Double Acel_H
        {
            get { return acel_h; }
            set { acel_h = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da aceleração da componente H
        /// </summary>
        public Double Sigma_Acel_H
        {
            get { return sig_Ah; }
            set { sig_Ah = value; }
        }

        /// <summary>
        /// define e retorna a lista de dados diversos em formato string
        /// </summary>
        public List<String> ListDadosDivS
        {
            get { return dadosDivS; }
            set { dadosDivS = value; }
        }

        /// <summary>
        /// define e retorna a lista de dados diversos em formato double
        /// </summary>
        public List<Double> ListDadosDivD
        {
            get { return dadosDivD; }
            set { dadosDivD = value; }
        }

        /// <summary>
        /// Adiciona um valor à lista de Doubles de dados diversos
        /// </summary>
        public void addDados(Double valor)
        {
            dadosDivD.Add(valor);
        }

        /// <summary>
        /// Adiciona um valor à lista de Strings de dados diversos
        /// </summary>
        public void addDados(String valor)
        {
            dadosDivS.Add(valor);
        }

        public int CompareTo(object obj)
        {
            Height coordMp = obj as Height;
            if (coordMp != null)
                return CompareTo(coordMp);
            throw new ArgumentException("Both objects being compared must be of type Height.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto Height
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Height other)
        {
            String id1 = this.ID;
            String id2 = other.ID;

            return id1.CompareTo(id2);
        }        
    }

    /// <summary>
    /// comparar Objecto Height pela componente Height
    /// </summary>
    public class CompareCota : IComparer<Height>
    {

        public int Compare(Object x, Object y)
        {
            Height x_ = x as Height;
            Height y_ = y as Height;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type Height."));
            else
                return Compare(x, y);
        }

        public int Compare(Height primeiro, Height segundo)
        {
            return primeiro.H.CompareTo(segundo.H);
        }
    }
}
