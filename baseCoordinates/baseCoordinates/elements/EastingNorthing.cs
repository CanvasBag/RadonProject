using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCoordinates.Elements
{
    /// <summary>
    /// Classe de coordenadas rectangulares cartográficas (m, p, H)
    /// </summary>  
    public class EastingNorthing : IComparable<EastingNorthing>
    {
        private Double m, p, h;
        private Double sig_m, sig_p, sig_h;
        private Double vel_m, vel_p, vel_h;
        private Double sig_Vm, sig_Vp, sig_Vh;
        private Double acel_m, acel_p, acel_h;
        private Double sig_Am, sig_Ap, sig_Ah;
        private List<String> dadosDivS;
        private List<Double> dadosDivD;
        private List<Int32> dadosDivInt;
        private String id;

        /// <summary>
        /// Cria um objecto EastingNorthing
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        /// <param name="h"></param>
        /// <param name="id"></param>
        public EastingNorthing(double e, double n, double h, String id)
        {
            m = e;
            p = n;
            this.h = h;
            this.id = id;
            dadosDivD = new List<Double>();
            dadosDivS = new List<String>();
            dadosDivInt = new List<Int32>();
        }

        /// <summary>
        /// Cria um objecto EastingNorthing
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        /// <param name="h"></param>
        public EastingNorthing(double e, double n, double h)
        {
            m = e;
            p = n;
            this.h = h;
            dadosDivD = new List<Double>();
            dadosDivS = new List<String>();
            dadosDivInt = new List<Int32>();
        }

        /// <summary>
        /// Cria um objecto EastingNorthing vazio
        /// </summary>
        public EastingNorthing()
        {
            dadosDivD = new List<Double>();
            dadosDivS = new List<String>();
            dadosDivInt = new List<Int32>();
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
        /// define e retorna o valor da coordenada E
        /// </summary>
        public Double E
        {
            get { return m; }
            set { m = value; }
        }

        /// <summary>
        /// define e retorna o valor da coordenada N
        /// </summary>
        public Double N
        {
            get { return p; }
            set { p = value; }
        }

        /// <summary>
        /// define e retorna o valor da coordenada Z
        /// </summary>
        public Double Z
        {
            get { return h; }
            set { h = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma de E
        /// </summary>
        public Double SigmaE
        {
            get { return sig_m; }
            set { sig_m = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma de N
        /// </summary>
        public Double SigmaN
        {
            get { return sig_p; }
            set { sig_p = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma de Z
        /// </summary>
        public Double SigmaZ
        {
            get { return sig_h; }
            set { sig_h = value; }
        }

        /// <summary>
        /// define e retorna o valor de velocidade da componente E
        /// </summary>
        public Double Vel_E
        {
            get { return vel_m; }
            set { vel_m = value; }
        }

        /// <summary>
        /// define e retorna o valor de velocidade da componente N
        /// </summary>
        public Double Vel_N
        {
            get { return vel_p; }
            set { vel_p = value; }
        }

        /// <summary>
        /// define e retorna o valor de velocidade da componente Z
        /// </summary>
        public Double Vel_Z
        {
            get { return vel_h; }
            set { vel_h = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da velocidade da componente E
        /// </summary>
        public Double Sigma_Vel_E
        {
            get { return sig_Vm; }
            set { sig_Vm = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da velocidade da componente N
        /// </summary>
        public Double Sigma_Vel_N
        {
            get { return sig_Vp; }
            set { sig_Vp = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da velocidade da componente Z
        /// </summary>
        public Double Sigma_Vel_Z
        {
            get { return sig_Vh; }
            set { sig_Vh = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente E
        /// </summary>
        public Double Acel_E
        {
            get { return acel_m; }
            set { acel_m = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente N
        /// </summary>
        public Double Acel_N
        {
            get { return acel_p; }
            set { acel_p = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente Z
        /// </summary>
        public Double Acel_Z
        {
            get { return acel_h; }
            set { acel_h = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da aceleração da componente E
        /// </summary>
        public Double Sigma_Acel_E
        {
            get { return sig_Am; }
            set { sig_Am = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da aceleração da componente N
        /// </summary>
        public Double Sigma_Acel_N
        {
            get { return sig_Ap; }
            set { sig_Ap = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da aceleração da componente Z
        /// </summary>
        public Double Sigma_Acel_Z
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
        /// define e retorna a lista de dados diversos em formato string
        /// </summary>
        public List<Int32> ListDadosDivInt
        {
            get { return dadosDivInt; }
            set { dadosDivInt = value; }
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

        /// <summary>
        /// define o valor das coordenadas E, N e h respectivamente
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        /// <param name="h"></param>
        public void setENH(double e, double n, double h)
        {
            m = e;
            p = n;
            h = this.h;
        }

        /// <summary>
        /// define e retorna o valor dos sigmas das coordenadas E, N e h respectivamente
        /// </summary>
        /// <param name="sigE"></param>
        /// <param name="sigN"></param>
        /// <param name="sigH"></param>
        public void setSigmaENH(double sigE, double sigN, double sigH)
        {
            sig_m = sigE;
            sig_p = sigN;
            sig_h = sigH;
        }

        /// <summary>
        /// definie o valor das velocidades respectivas a cada coordenada: E, N e h
        /// </summary>
        /// <param name="velE"></param>
        /// <param name="velN"></param>
        /// <param name="velH"></param>
        public void setVelMPH(double velE, double velN, double velH)
        {
            vel_m = velE;
            vel_p = velN;
            vel_h = velH;
        }

        /// <summary>
        /// define os sígmas associados às velocidades
        /// </summary>
        /// <param name="sigvelE"></param>
        /// <param name="sigvelN"></param>
        /// <param name="sigvelH"></param>
        public void setSigmaVel_MPH(double sigvelE, double sigvelN, double sigvelH)
        {
            sig_Vm = sigvelE;
            sig_Vp = sigvelN;
            sig_Vh = sigvelH;
        }

        /// <summary>
        /// definie o valor das acelerações respectivas a cada coordenada: E, N e h
        /// </summary>
        /// <param name="acelE"></param>
        /// <param name="acelN"></param>
        /// <param name="acelH"></param>
        public void setAcelENH(double acelE, double acelN, double acelH)
        {
            acel_m = acelE;
            acel_p = acelN;
            acel_h = acelH;
        }

        /// <summary>
        /// define os sígmas associados às acelerações
        /// </summary>
        /// <param name="sigacelE"></param>
        /// <param name="sigacelN"></param>
        /// <param name="sigacelH"></param>
        public void setSigmaAcel_ENH(double sigacelE, double sigacelN, double sigacelH)
        {
            sig_Am = sigacelE;
            sig_Ap = sigacelN;
            sig_Ah = sigacelH;
        }

        public override String ToString()
        {
            return ID;
        }

        /// <summary>
        /// PT - Copia Superficial
        /// EN - Superficial Copy
        /// </summary>
        /// <returns></returns>
        public EastingNorthing Clone()
        {
            EastingNorthing enzTmp = new EastingNorthing();
            enzTmp.E = this.E;
            enzTmp.N = this.N;
            enzTmp.Z = this.Z;
            enzTmp.Vel_E = this.Vel_E;
            enzTmp.Vel_N = this.Vel_N;
            enzTmp.Vel_Z = this.Vel_Z;
            enzTmp.Acel_E = this.Acel_E;
            enzTmp.Acel_N = this.Acel_N;
            enzTmp.Acel_Z = this.Acel_Z;
            enzTmp.SigmaE = this.SigmaE;
            enzTmp.SigmaN = this.SigmaN;
            enzTmp.SigmaZ = this.SigmaZ;
            enzTmp.Sigma_Vel_E = this.Sigma_Vel_E;
            enzTmp.Sigma_Vel_N = this.Sigma_Vel_N;
            enzTmp.Sigma_Vel_Z = this.Sigma_Vel_Z;
            enzTmp.Sigma_Acel_E = this.Sigma_Acel_E;
            enzTmp.Sigma_Acel_N = this.Sigma_Acel_N;
            enzTmp.Sigma_Acel_Z = this.Sigma_Acel_Z;
            enzTmp.ListDadosDivD = this.ListDadosDivD;
            enzTmp.ListDadosDivInt = this.ListDadosDivInt;
            enzTmp.ListDadosDivS = this.ListDadosDivS;
            enzTmp.ID = this.ID;

            return enzTmp;
        }
        public int CompareTo(object obj)
        {
            EastingNorthing coordMp = obj as EastingNorthing;
            if (coordMp != null)
                return CompareTo(coordMp);
            throw new ArgumentException("Both objects being compared must be of type EastingNorthing.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto EastingNorthing
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(EastingNorthing other)
        {
            String id1 = this.ID;
            String id2 = other.ID;

            return id1.CompareTo(id2);
        }        
    }


    /// <summary>
    /// comparar Objecto EastingNorthing pela componente E
    /// </summary>
    public class CompareE : IComparer<EastingNorthing>
    {

        public int Compare(Object x, Object y)
        {
            EastingNorthing x_ = x as EastingNorthing;
            EastingNorthing y_ = y as EastingNorthing;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type Mp."));
            else
                return Compare(x, y);
        }

        public int Compare(EastingNorthing primeiro, EastingNorthing segundo)
        {
            return primeiro.E.CompareTo(segundo.E);
        }
    }

    /// <summary>
    /// comparar Objecto EastingNorthing pela componente N
    /// </summary>
    public class CompareN : IComparer<EastingNorthing>
    {

        public int Compare(Object x, Object y)
        {
            EastingNorthing x_ = x as EastingNorthing;
            EastingNorthing y_ = y as EastingNorthing;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type Mp."));
            else
                return Compare(x, y);
        }

        public int Compare(EastingNorthing primeiro, EastingNorthing segundo)
        {
            return primeiro.N.CompareTo(segundo.N);
        }
    }

    /// <summary>
    /// comparar Objecto EastingNorthing pela componente H
    /// </summary>
    public class CompareH : IComparer<EastingNorthing>
    {

        public int Compare(Object x, Object y)
        {
            EastingNorthing x_ = x as EastingNorthing;
            EastingNorthing y_ = y as EastingNorthing;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type Mp."));
            else
                return Compare(x, y);
        }

        public int Compare(EastingNorthing primeiro, EastingNorthing segundo)
        {
            return primeiro.Z.CompareTo(segundo.Z);
        }
    }


}
