using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCoordinates.Elements
{
    /// <summary>
    /// Classe de coordenadas rectangulares (X, Y, Z)    
    /// </summary>
    public class Ret : IComparable<Ret>
    {
        private double x;
        private double y;
        private double z;
        private double sig_x, sig_y, sig_z;
        private double vel_x, vel_y, vel_z;
        private double sig_Vx, sig_Vy, sig_Vz;
        private double acel_x, acel_y, acel_z;
        private double sig_Ax, sig_Ay, sig_Az;
        private List<String> dadosDivS;
        private List<Double> dadosDivD;
        private List<Int32> dadosDivInt;
        private String id;

        /// <summary>
        /// Cria um objecto ret
        /// </summary>
        /// <param name="xx"></param>
        /// <param name="yy"></param>
        /// <param name="zz"></param>
        /// <param name="id_"></param>
        public Ret(double xx, double yy, double zz, String id_)
        {
            x = xx;
            y = yy;
            z = zz;
            id = id_;
            dadosDivD = new List<Double>();
            dadosDivS = new List<String>();
            dadosDivInt = new List<Int32>();
        }

        /// <summary>
        /// Cria um objecto ret
        /// </summary>
        /// <param name="xx"></param>
        /// <param name="yy"></param>
        /// <param name="zz"></param>
        public Ret(double xx, double yy, double zz)
        {
            x = xx;
            y = yy;
            z = zz;
            dadosDivD = new List<Double>();
            dadosDivS = new List<String>();
            dadosDivInt = new List<Int32>();
        }

        /// <summary>
        /// Cria um objecto ret
        /// </summary>
        public Ret()
        {
            dadosDivD = new List<Double>();
            dadosDivS = new List<String>();
            dadosDivInt = new List<Int32>();
        }

        /// <summary>
        /// define e retorna o valor da componente X
        /// </summary>
        public Double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// define e retorna o valor da componente Y
        /// </summary>
        public Double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// define e retorna o valor da componente Z
        /// </summary>
        public Double Z
        {
            get { return z; }
            set { z = value; }
        }

        /// <summary>
        /// define e retorna o valor do Id do Objecto
        /// </summary>
        public String ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da componente X
        /// </summary>
        public Double SigmaX
        {
            get { return sig_x; }
            set { sig_x = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da componente Y
        /// </summary>
        public Double SigmaY
        {
            get { return sig_y; }
            set { sig_y = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da componente Z
        /// </summary>
        public Double SigmaZ
        {
            get { return sig_z; }
            set { sig_z = value; }
        }

        /// <summary>
        /// define e retorna o valor da velocidade da componente X
        /// </summary>
        public Double Vel_X
        {
            get { return vel_x; }
            set { vel_x = value; }
        }

        /// <summary>
        /// define e retorna o valor da velocidade da componente Y
        /// </summary>
        public Double Vel_Y
        {
            get { return vel_y; }
            set { vel_y = value; }
        }

        /// <summary>
        /// define e retorna o valor da velocidade da componenteZ
        /// </summary>
        public Double Vel_Z
        {
            get { return vel_z; }
            set { vel_z = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da velocidade da componente X
        /// </summary>
        public Double Sigma_Vel_X
        {
            get { return sig_Vx; }
            set { sig_Vx = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da velocidade da componente Y
        /// </summary>
        public Double Sigma_Vel_Y
        {
            get { return sig_Vy; }
            set { sig_Vy = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da velocidade da componente Z
        /// </summary>
        public Double Sigma_Vel_Z
        {
            get { return sig_Vz; }
            set { sig_Vz = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente X
        /// </summary>
        public Double Acel_X
        {
            get { return acel_x; }
            set { acel_x = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente Y
        /// </summary>
        public Double Acel_Y
        {
            get { return acel_y; }
            set { acel_y = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente Z
        /// </summary>
        public Double Acel_Z
        {
            get { return acel_z; }
            set { acel_z = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da aceleração da componente X
        /// </summary>
        public Double Sigma_Acel_X
        {
            get { return sig_Ax; }
            set { sig_Ax = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da aceleração da componente Y
        /// </summary>
        public Double Sigma_Acel_Y
        {
            get { return sig_Ay; }
            set { sig_Ay = value; }
        }

        /// <summary>
        /// define e retorna o valor de signa da aceleração da componente Z
        /// </summary>
        public Double Sigma_Acel_Z
        {
            get { return sig_Az; }
            set { sig_Az = value; }
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
        /// define o valor das componentes X, Y e Z respectivamente
        /// </summary>
        /// <param name="x_"></param>
        /// <param name="y_"></param>
        /// <param name="z_"></param>
        public void setXYZ(double x_, double y_, double z_)
        {
            x = x_;
            y = y_;
            z = z_;
        }

        /// <summary>
        /// define o valor de sigma das componentes X, Y e Z respectivamente
        /// </summary>
        /// <param name="sx_"></param>
        /// <param name="sy_"></param>
        /// <param name="sz_"></param>
        public void setSgimaXYZ(double sx_, double sy_, double sz_)
        {
            sig_x = sx_;
            sig_y = sy_;
            sig_z = sz_;
        }

        /// <summary>
        /// define o valor da velocidade das componentes X, Y e Z respectivamente
        /// </summary>
        /// <param name="vx_"></param>
        /// <param name="vy_"></param>
        /// <param name="vz_"></param>
        public void setVel_XYZ(double vx_, double vy_, double vz_)
        {
            vel_x = vx_;
            vel_y = vy_;
            vel_z = vz_;
        }

        /// <summary>
        /// define o valor de sigma das velocidades das componentes X, Y e Z respectivamente
        /// </summary>
        /// <param name="svx_"></param>
        /// <param name="svy_"></param>
        /// <param name="svz_"></param>
        public void setSgimaVel_XYZ(double svx_, double svy_, double svz_)
        {
            sig_Vx = svx_;
            sig_Vy = svy_;
            sig_Vz = svz_;
        }

        /// <summary>
        /// define o valor das acelerações das componentes X, Y e Z respectivamente
        /// </summary>
        /// <param name="ax_"></param>
        /// <param name="ay_"></param>
        /// <param name="az_"></param>
        public void setAcel_XYZ(double ax_, double ay_, double az_)
        {
            acel_x = ax_;
            acel_y = ay_;
            acel_z = az_;
        }

        /// <summary>
        /// define o valor de sigma das acelerações das componentes X, Y e Z respectivamente
        /// </summary>
        /// <param name="sax_"></param>
        /// <param name="say_"></param>
        /// <param name="saz_"></param>
        public void setSgimaAcel_XYZ(double sax_, double say_, double saz_)
        {
            sig_Ax = sax_;
            sig_Ay = say_;
            sig_Az = saz_;
        }

        public int CompareTo(Object obj)
        {
            Ret coordRet = obj as Ret;
            if (coordRet != null)
                return CompareTo(coordRet);
            throw new ArgumentException("Both objects being compared must be of type Mp.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto Ret
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Ret other)
        {
            String id1 = this.id;
            String id2 = other.id;

            return id1.CompareTo(id2);
        }
    }

    /// <summary>
    /// comparar Objectos Ret pela componente X
    /// </summary>
    public class CompareX : IComparer<Ret>
    {
        public int Compare(Object x, Object y)
        {
            Ret primeiro = x as Ret;
            Ret segundo = y as Ret;
            if (primeiro == null || segundo == null)
                throw (new ArgumentException("Both parameters must be of type Ret."));
            else
                return Compare(primeiro, segundo);
        }

        public int Compare(Ret primeiro, Ret segundo)
        {
            return primeiro.X.CompareTo(segundo.X);
        }
    }

    /// <summary>
    /// comparar Objectos Ret pela componente Y
    /// </summary>
    public class CompareY : IComparer<Ret>
    {
        public int Compare(Object x, Object y)
        {
            Ret primeiro = x as Ret;
            Ret segundo = y as Ret;
            if (primeiro == null || segundo == null)
                throw (new ArgumentException("Both parameters must be of type Ret."));
            else
                return Compare(primeiro, segundo);
        }

        public int Compare(Ret primeiro, Ret segundo)
        {
            return primeiro.Y.CompareTo(segundo.Y);
        }
    }

    /// <summary>
    /// comparar Objectos Ret pela componente Z
    /// </summary>
    public class CompareZ : IComparer<Ret>
    {
        public int Compare(Object x, Object y)
        {
            Ret primeiro = x as Ret;
            Ret segundo = y as Ret;
            if (primeiro == null || segundo == null)
                throw (new ArgumentException("Both parameters must be of type Ret."));
            else
                return Compare(primeiro, segundo);
        }

        public int Compare(Ret primeiro, Ret segundo)
        {
            return primeiro.Z.CompareTo(segundo.Z);
        }
    }
    
}
