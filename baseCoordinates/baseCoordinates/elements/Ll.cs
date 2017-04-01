using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using baseTime.Seed;
using baseTime.Elements;

namespace BaseCoordinates.Elements
{
    public class Ll : IComparable<Ll>
    {
        private double log;
        private double lat;
        private double h;
        private double sig_log, sig_lat, sig_h;
        private double vel_log, vel_lat, vel_h;
        private double sig_Vlog, sig_Vlat, sig_Vh;
        private double acel_log, acel_lat, acel_h;
        private double sig_Alog, sig_Alat, sig_Ah;
        private List<String> dadosDivS;
        private List<Double> dadosDivD;
        private List<Int32> dadosDivInt;
        private String id;
        private DateTime tempo;

        /// <summary>
        /// cria um objecto ll
        /// </summary>
        /// <param name="log_"></param>
        /// <param name="lat_"></param>
        /// <param name="h_"></param>
        public Ll(double log_, double lat_, double h_, String id_)
        {
            log = log_;
            lat = lat_;
            h = h_;
            id = id_;
            dadosDivD = new List<double>();
            dadosDivS = new List<string>();
            dadosDivInt = new List<Int32>();
        }

        /// <summary>
        /// cria um objecto ll
        /// </summary>
        /// <param name="log_"></param>
        /// <param name="lat_"></param>
        /// <param name="h_"></param>
        public Ll(double log_, double lat_, double h_)
        {
            log = log_;
            lat = lat_;
            h = h_;
            dadosDivD = new List<double>();
            dadosDivS = new List<string>();
            dadosDivInt = new List<Int32>();
        }

        /// <summary>
        /// cria um objecto ll
        /// </summary>
        public Ll()
        {
            dadosDivD = new List<double>();
            dadosDivS = new List<string>();
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
        /// define e retorna o valor de Latitude
        /// </summary>
        public Double Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        /// <summary>
        /// define e retorna o valor de Longitude
        /// </summary>
        public Double Long
        {
            get { return log; }
            set { log = value; }
        }

        /// <summary>
        /// define e retorna o valor de Altitude
        /// </summary>
        public Double H
        {
            get { return h; }
            set { h = value; }
        }

        /// <summary>
        /// define e retorna o valor o tempo
        /// </summary>
        public DateTime time
        {
            get { return tempo; }
            set { tempo = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da Latitude
        /// </summary>
        public Double SigmaLat
        {
            get { return sig_lat; }
            set { sig_lat = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da Longitude
        /// </summary>
        public Double SigmaLong
        {
            get { return sig_log; }
            set { sig_log = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da Longitude
        /// </summary>
        public Double SigmaH
        {
            get { return sig_h; }
            set { sig_h = value; }
        }

        /// <summary>
        /// define e retorna o valor da velocidade da componente Latitude
        /// </summary>
        public Double Vel_Lat
        {
            get { return vel_lat; }
            set { vel_lat = value; }
        }

        /// <summary>
        /// define e retorna o valor da velocidade da componente Longitude
        /// </summary>
        public Double Vel_Log
        {
            get { return vel_log; }
            set { vel_log = value; }
        }

        /// <summary>
        /// define e retorna o valor da velocidade da componente H
        /// </summary>
        public Double Vel_H
        {
            get { return vel_h; }
            set { vel_h = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da velocidade da componente Latitude
        /// </summary>
        public Double Sigma_Vel_Lat
        {
            get { return sig_Vlat; }
            set { sig_Vlat = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da velocidade da componente Longitude
        /// </summary>
        public Double Sigma_Vel_Long
        {
            get { return sig_Vlog; }
            set { sig_Vlog = value; }
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
        /// define e retorna o valor da aceleração da componente Latitude
        /// </summary>
        public Double Acel_Lat
        {
            get { return acel_lat; }
            set { acel_lat = value; }
        }

        /// <summary>
        /// define e retorna o valor da aceleração da componente Longitude
        /// </summary>
        public Double Acel_Long
        {
            get { return acel_log; }
            set { acel_log = value; }
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
        /// define e retorna o valor de sigma da aceleração da componente Latitude
        /// </summary>
        public Double Sigma_Acel_Lat
        {
            get { return sig_Alat; }
            set { sig_Alat = value; }
        }

        /// <summary>
        /// define e retorna o valor de sigma da aceleração da componente Longitude
        /// </summary>
        public Double Sigma_Acel_Long
        {
            get { return sig_Alog; }
            set { sig_Alog = value; }
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
        /// define o valor das componentes Longitude, Latitude e h respectivamente
        /// </summary>
        /// <param name="log_"></param>
        /// <param name="lat_"></param>
        /// <param name="h_"></param>
        public void setLatLongH(double log_, double lat_, double h_)
        {
            log = log_;
            lat = lat_;
            h = h_;
        }

        /// <summary>
        /// define o valor de sigma das componentes  Longitude, Latitude e h respectivamente
        /// </summary>
        /// <param name="siglog"></param>
        /// <param name="siglat"></param>
        /// <param name="sigh"></param>
        public void setSigmaLatLongH(double siglog, double siglat, double sigh)
        {
            sig_log = siglog;
            sig_lat = siglat;
            sig_h = sigh;
        }

        /// <summary>
        /// define o valor das velocidadades das componentes Longitude, Latitude e h respectivamente
        /// </summary>
        /// <param name="vellog"></param>
        /// <param name="vellat"></param>
        /// <param name="velh"></param>
        public void setVel_LatLongH(double vellog, double vellat, double velh)
        {
            vel_log = vellog;
            vel_lat = vellat;
            vel_h = velh;
        }

        /// <summary>
        /// define o valor de sigma das velocidadades das componentes Longitude, Latitude e h respectivamente
        /// </summary>
        /// <param name="sigVellog"></param>
        /// <param name="sigVellat"></param>
        /// <param name="sigVelh"></param>
        public void setSigmaVel_LatLongH(double sigVellog, double sigVellat, double sigVelh)
        {
            sig_Vlog = sigVellog;
            sig_Vlat = sigVellat;
            sig_Vh = sigVelh;
        }

        /// <summary>
        /// define o valor das aceleraçõess das componentes Longitude, Latitude e h respectivamente
        /// </summary>
        /// <param name="acellog"></param>
        /// <param name="acellat"></param>
        /// <param name="acelh"></param>
        public void setAcel_LatLongH(double acellog, double acellat, double acelh)
        {
            acel_log = acellog;
            acel_lat = acellat;
            acel_h = acelh;
        }

        /// <summary>
        /// define o valor de sigma das acelerações das componentes Longitude, Latitude e h respectivamente
        /// </summary>
        /// <param name="sigAcellog"></param>
        /// <param name="sigAcellat"></param>
        /// <param name="sigAcelh"></param>
        public void setSigmaAcel_LatLongH(double sigAcellog, double sigAcellat, double sigAcelh)
        {
            sig_Alog = sigAcellog;
            sig_Alat = sigAcellat;
            sig_Ah = sigAcelh;
        }



        public int CompareTo(object obj)
        {
            Ll coordLl = obj as Ll;
            if (coordLl != null)
                return CompareTo(coordLl);
            throw new ArgumentException("Both objects being compared must be of type Mp.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto Ll
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Ll other)
        {
            String id1 = this.id;
            String id2 = other.id;

            return id1.CompareTo(id2);
        }
    }

    
    /// <summary>
    /// comparar Objectos Ll pela componente Latitude
    /// </summary>
    public class CompareLat : IComparer<Ll>
    {
        public int Compare(Object x, Object y)
        {
            Ll primeiro = x as Ll;
            Ll segundo = y as Ll;
            if (primeiro == null || segundo == null)
                throw (new ArgumentException("Both parameters must be of type Ll."));
            else
                return Compare(primeiro, segundo);
        }

        public int Compare(Ll primeiro, Ll segundo)
        {
            return primeiro.Lat.CompareTo(segundo.Lat);
        }
    }

    /// <summary>
    /// comparar Objectos Ll pela componente Longitude
    /// </summary>
    public class CompareLog : IComparer<Ll>
    {
        public int Compare(Object x, Object y)
        {
            Ll primeiro = x as Ll;
            Ll segundo = y as Ll;
            if (primeiro == null || segundo == null)
                throw (new ArgumentException("Both parameters must be of type Ll."));
            else
                return Compare(primeiro, segundo);
        }

        public int Compare(Ll primeiro, Ll segundo)
        {
            return primeiro.Long.CompareTo(segundo.Long);
        }
    }

    /// <summary>
    /// comparar Objectos Ll pela componente h
    /// </summary>
    public class Compareh : IComparer<Ll>
    {
        public int Compare(Object x, Object y)
        {
            Ll primeiro = x as Ll;
            Ll segundo = y as Ll;
            if (primeiro == null || segundo == null)
                throw (new ArgumentException("Both parameters must be of type Ll."));
            else
                return Compare(primeiro, segundo);
        }

        public int Compare(Ll primeiro, Ll segundo)
        {
            return primeiro.H.CompareTo(segundo.H);
        }
    }
    
}
