using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace baseTime.Elements
{
    public class JulianDate : IComparable<JulianDate>
    {
        private double julianD;
        private String id;

        public JulianDate(int julianD_, String id_)
        {
            julianD = julianD_;
            id = id_;
        }

        public JulianDate(int julianD_)
        {
            julianD = julianD_;
        }

        public JulianDate()
        {
        }

        /// <summary>
        /// define e retorna o valor da data Juliana
        /// </summary>
        public double JD
        {
            get { return julianD; }
            set { julianD = value; }
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
            JulianDate objJulianDate = obj as JulianDate;
            if (objJulianDate != null)
                return CompareTo(objJulianDate);
            throw new ArgumentException("Both objects being compared must be of type JulianDate.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// compara o id com outro objecto JulianDate
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(JulianDate other)
        {
            double tempo1 = this.JD;
            double tempo2 = other.JD;

            return tempo1.CompareTo(tempo2);
        }
    }

    /// <summary>
    /// comparar Objecto JulianDate pelo seu ID
    /// </summary>
    public class CompareID_JD : IComparer<JulianDate>
    {

        public int Compare(Object x, Object y)
        {
            JulianDate x_ = x as JulianDate;
            JulianDate y_ = y as JulianDate;
            if (x_ == null || y_ == null)
                throw (new ArgumentException("Both parameters must be of type JulianDate."));
            else
                return Compare(x, y);
        }

        public int Compare(JulianDate primeiro, JulianDate segundo)
        {
            return primeiro.ID.CompareTo(segundo.ID);
        }
    }
}
