using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace baseTime.Elements
{
    public class DateTimer : IComparable<DateTimer>
    {
        private DateTime timer;
        private String id;

        public DateTimer(DateTime timer_, String id_)
        {
            timer = timer_;
            id = id_;
        }

        public DateTimer(DateTime timer_)
        {
            timer = timer_;
        }

        public DateTimer()
        {   
        }

        /// <summary>
        /// retorna o objecto DateTime
        /// </summary>
        /// <returns></returns>
        public DateTime getDateTime()
        {
            return timer;
        }

        /// <summary>
        /// define e retorna a data
        /// </summary>
        public DateTime Date
        {
            get { return timer; }
            set { timer = value; }
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
            DateTimer obDateTimer = obj as DateTimer;
            if (obDateTimer != null)
                return CompareTo(obDateTimer);
            throw new ArgumentException("Both objects being compared must be of type DateTimer.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto JulianDate
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DateTimer other)
        {
            return this.CompareTo(other);
        }
    }

    /// <summary>
    /// comparar Objecto DateTimer pelo seu ID
    /// </summary>
    public class CompareID_DateTimer : IComparer<DateTimer>
    {

        public int Compare(Object x, Object y)
        {
            DateTimer x_ = x as DateTimer;
            DateTimer y_ = y as DateTimer;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type DateTimer."));
            else
                return Compare(x, y);
        }

        public int Compare(DateTimer primeiro, DateTimer segundo)
        {
            return primeiro.ID.CompareTo(segundo.ID);
        }
    }
}
