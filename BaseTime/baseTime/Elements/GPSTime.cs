using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace baseTime.Elements
{
    public class GPSTime : IComparable<GPSTime>
    {
        private int tempoGps;
        private string semanaGps;
        private String id;

        public GPSTime(int tempoGps_, String id_)
        {
            tempoGps = tempoGps_;
            id = id_;
        }

        public GPSTime(int tempoGps_)
        {
            tempoGps = tempoGps_;         
        }

        public GPSTime()
        {
        }

        /// <summary>
        /// define e retorna o valor do tempo Gps
        /// </summary>
        public int GPS_Time
        {
            get { return tempoGps; }
            set { tempoGps = value; }
        }

        /// <summary>
        /// define e retorna o ID
        /// </summary>
        public String ID
        {
            get { return id; }
            set { id = value; }
        }

        public int CompareTo(object obj)
        {
            GPSTime objGpsTime = obj as GPSTime;
            if (objGpsTime != null)
                return CompareTo(objGpsTime);
            throw new ArgumentException("Both objects being compared must be of type GPSTime.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto GPSTime
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(GPSTime other)
        {
            int tempo1 = this.GPS_Time;
            int tempo2 = other.GPS_Time;

            return tempo1.CompareTo(tempo2);
        }
    }

    /// <summary>
    /// comparar Objecto GPSTime pelo seu ID
    /// </summary>
    public class CompareID_GPSTime : IComparer<GPSTime>
    {

        public int Compare(Object x, Object y)
        {
            GPSTime x_ = x as GPSTime;
            GPSTime y_ = y as GPSTime;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type GPSTime."));
            else
                return Compare(x, y);
        }

        public int Compare(GPSTime primeiro, GPSTime segundo)
        {
            return primeiro.ID.CompareTo(segundo.ID);
        }
    }
}
