using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using baseTime.Elements;

namespace baseTime.Seed
{
    public class GeoTime
    {
        private List<GPSTime> gpsTimer;
        private List<JulianDate> jdDate;
        private List<WeekGPSTime> gpswTimer;
        private List<DateTimer> DateTime;

        public GeoTime(List<GPSTime> gpsTimer_)
        {
            gpsTimer = gpsTimer_;
        }

        public GeoTime(List<JulianDate> jdDate_)
        {
            jdDate = jdDate_;
        }

        public GeoTime(List<WeekGPSTime> gpswTimer_)
        {
            gpswTimer = gpswTimer_;
        }

        public GeoTime(List<DateTimer> DateTime_)
        {
            DateTime = DateTime_;
        }

        public GeoTime()
        {
            gpsTimer = new List<GPSTime>();
            jdDate = new List<JulianDate>();
            gpswTimer = new List<WeekGPSTime>();
            DateTime = new List<DateTimer>();
        }

        /// <summary>
        /// adiciona um objecto GPSTime
        /// </summary>
        /// <param name="obj"></param>
        public void add_GPSTimeObj(GPSTime gpsObj)
        {
            gpsTimer.Add(gpsObj);
        }

        /// <summary>
        /// adiciona um objecto JulianDate
        /// </summary>
        /// <param name="obj"></param>
        public void add_JDTimeObj(JulianDate jdObj)
        {
            jdDate.Add(jdObj);
        }

        /// <summary>
        /// adiciona um objecto WeekGPSTime
        /// </summary>
        /// <param name="obj"></param>
        public void add_W_GPSTimeObj(WeekGPSTime wgpsObj)
        {
            gpswTimer.Add(wgpsObj);
        }

        /// <summary>
        /// adiciona um objecto DateTimer
        /// </summary>
        /// <param name="dateObj"></param>
        public void add_DateTimerObj(DateTimer dateObj)
        {
            DateTime.Add(dateObj);
        }

        /// <summary>
        /// define e retorna a lista de objectos GPSTime 
        /// </summary>
        /// <returns></returns>
        public List<GPSTime> ListGPSTime
        {
            get { return gpsTimer; }
            set { gpsTimer = value; }
        }

        /// <summary>
        /// devolve a lista de objectos GPSTime com o id definido 
        /// </summary>
        /// <returns></returns>
        public List<GPSTime> getListGPSTime(String id)
        {
            List<GPSTime> listGPSTime = new List<GPSTime>();
            foreach (GPSTime tmpGPSTimer in listGPSTime)
            {
                if (tmpGPSTimer.ID == id)
                    listGPSTime.Add(tmpGPSTimer);
            }
            return listGPSTime;
        }

        /// <summary>
        /// devolve a lista de objectos JulianDate
        /// </summary>
        /// <returns></returns>
        public List<JulianDate> ListJulianDate
        {
            get { return jdDate; }
            set { jdDate = value; }           
        }

        /// <summary>
        /// devolve a lista de objectos JulianDate com o id definido 
        /// </summary>
        /// <returns></returns>
        public List<JulianDate> getListJulianDate(String id)
        {
            List<JulianDate> listJDTime = new List<JulianDate>();
            foreach (JulianDate tmpJD in listJDTime)
            {
                if (tmpJD.ID == id)
                    listJDTime.Add(tmpJD);
            }
            return listJDTime;
        }

        /// <summary>
        /// devolve a lista de objectos WeekGPSTime
        /// </summary>
        /// <returns></returns>
        public List<WeekGPSTime> ListWeekGPSTime
        {
            get { return gpswTimer; }
            set { gpswTimer = value; }          
        }

        /// <summary>
        /// devolve a lista de objectos WeekGPSTime com o id definido
        /// </summary>
        /// <returns></returns>
        public List<WeekGPSTime> getListWeekGPSTime(String id)
        {
            List<WeekGPSTime> listWGPSTime = new List<WeekGPSTime>();
            foreach (WeekGPSTime tmpWGPS in listWGPSTime)
            {
                if (tmpWGPS.ID == id)
                    listWGPSTime.Add(tmpWGPS);
            }
            return listWGPSTime;
        }

        /// <summary>
        /// devolve a lista de objectos DateTimer
        /// </summary>
        /// <returns></returns>
        public List<DateTimer> ListDateTimer
        {
            get { return DateTime; }
            set { DateTime = value; }            
        }

        /// <summary>
        /// devolve a lista de objectos DateTimer com o id definido 
        /// </summary>
        /// <returns></returns>
        public List<DateTimer> getListDateTimer(String id)
        {
            List<DateTimer> listDateTimer = new List<DateTimer>();
            foreach (DateTimer tmpDate in listDateTimer)
            {
                if (tmpDate.ID == id)
                    listDateTimer.Add(tmpDate);
            }
            return listDateTimer;
        }

        public void sortGPSTime_By_Date()
        {
            gpsTimer.Sort();
        }

        public void sortGPSTime_By_ID()
        {
            IComparer<GPSTime> idCompare = new CompareID_GPSTime();
            gpsTimer.Sort(idCompare);
        }

        public void sortJD_By_Date()
        {
            jdDate.Sort();
        }

        public void sortJD_By_ID()
        {
            IComparer<JulianDate> idCompare = new CompareID_JD();
            jdDate.Sort(idCompare);
        }

        public void sortWGPSTime_By_Date()
        {
            gpswTimer.Sort();
        }

        public void sortWGPSTime_By_ID()
        {
            IComparer<WeekGPSTime> idCompare = new CompareID_WeekGPSTime();
            gpswTimer.Sort(idCompare);
        }

        public void sortDateTimer_By_Date()
        {
            DateTime.Sort();
        }

        public void sortDateTimer_By_ID()
        {
            IComparer<DateTimer> idCompare = new CompareID_DateTimer();
            DateTime.Sort(idCompare);
        }
    }
}
