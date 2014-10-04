using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace baseTime.Elements
{
    public class WeekGPSTime : IComparable<WeekGPSTime>
    {
        private double gpsWeekTime;
        private int semanaGps;
        private String id;

        /// <summary>
        /// cria um objecto gpsWeekTime
        /// </summary>
        /// <param name="gpsWeekTime_, must be grater than 0 and less than 604800"></param>
        /// <param name="id_"></param>
        public WeekGPSTime(double gpsWeekTime, int semanaGps, String id)
        {
            if (gpsWeekTime > 0 & gpsWeekTime <= 3600 * 24 * 7)
            {
                this.gpsWeekTime = gpsWeekTime;
                this.semanaGps = semanaGps;
                this.id = id;
            }
            else
                throw new ArgumentOutOfRangeException("Input value of GPSWeekTime not accepted");
        }

        /// <summary>
        /// cria um objecto gpsWeekTime
        /// </summary>
        /// <param name="gpsWeekTime_, must be grater than 0 and less than 604800"></param>
        /// <param name="id_"></param>
        public WeekGPSTime(double gpsWeekTime_, String id_)
        {
            if (gpsWeekTime_ > 0 & gpsWeekTime_ <= 3600 * 24 * 7)
            {
                gpsWeekTime = gpsWeekTime_;
                id = id_;
            }
            else
                throw new ArgumentOutOfRangeException("Input value of GPSWeekTime not accepted");
        }

        /// <summary>
        /// cria um objecto gpsWeekTime
        /// </summary>
        /// <param name="gpsWeekTime_, must be grater than 0 and less than 604800"></param>
        public WeekGPSTime(double gpsWeekTime_)
        {
            if (gpsWeekTime_ > 0 & gpsWeekTime_ <= 3600 * 24 * 7)
            {
                gpsWeekTime = gpsWeekTime_;
            }
            else
                throw new ArgumentOutOfRangeException("Input value of GPSWeekTime not accepted");        
        }

        public WeekGPSTime()
        {
        }

        /// <summary>
        /// define e retorna a semana GPS
        /// </summary>
        public int GPSW
        {
            get { return semanaGps; }
            set { semanaGps = value; }
        }

        /// <summary>
        /// define e retorna o valor do tempo semanal GPS
        /// </summary>
        /// <value "value must be grater than 0 and less than 604800"></value>        
        public double GPSWTime
        {
            get { return gpsWeekTime; }
            set 
            {
                if (value > 0 & value <= 3600 * 24 * 7)
                    gpsWeekTime = value;
                else
                    throw new ArgumentOutOfRangeException("Input value of GPSWeekTime not accepted"); 
            }
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
            WeekGPSTime objGpsTime = obj as WeekGPSTime;
            if (objGpsTime != null)
                return CompareTo(objGpsTime);
            throw new ArgumentException("Both objects being compared must be of type GPSTime.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto WeekGPSTime
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(WeekGPSTime other)
        {
            double tempo1 = this.GPSWTime;
            double tempo2 = other.GPSWTime;

            return tempo1.CompareTo(tempo2);
        }
    }

    /// <summary>
    /// comparar Objecto WeekGPSTime pelo seu ID
    /// </summary>
    public class CompareID_WeekGPSTime : IComparer<WeekGPSTime>
    {

        public int Compare(Object x, Object y)
        {
            WeekGPSTime x_ = x as WeekGPSTime;
            WeekGPSTime y_ = y as WeekGPSTime;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type GPSTime."));
            else
                return Compare(x, y);
        }

        public int Compare(WeekGPSTime primeiro, WeekGPSTime segundo)
        {
            return primeiro.ID.CompareTo(segundo.ID);
        }
    }
}
