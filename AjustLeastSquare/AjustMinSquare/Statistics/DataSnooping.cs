using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace AjustLeastSquare.Statistics
{
    public class DataSnooping
    {
        private Matrix w, a, qxx, v;

        private Matrix qvv, vStand, absVStand;

        private double rejectionLevel, var;

        private bool[] vStandTest;


        public DataSnooping(Matrix w, Matrix a, Matrix qxx, Matrix v, double var, double rejectionLevel)
        {
            this.w = w;
            this.a = a;
            this.qxx = qxx;
            this.v = v;
            this.var = var;
            this.rejectionLevel = rejectionLevel;

            ComputeStandardizedResiduals();
        }

        public void ComputeStandardizedResiduals()
        {
            vStand = new Matrix(v.RowCount, v.ColumnCount);
            absVStand = new Matrix(v.RowCount, v.ColumnCount);
            vStandTest = new bool[v.RowCount];

            double s = Math.Sqrt(var);

            ComputeQvv();

            for (int i = 0; i < v.RowCount; i++)
            {
                vStand[i, 0] = v[i, 0] / Math.Sqrt(Math.Abs(qvv[i, i]));
                absVStand[i, 0] = Math.Abs(v[i, 0]) / Math.Sqrt(Math.Abs(qvv[i, i]));

                vStandTest[i] = absVStand[i, 0] > s * rejectionLevel ? false : true;
            }
        }

        private void ComputeQvv()
        {
            Matrix aT = a.Clone();
            aT.Transpose();

            Matrix qLAjustlLAjust = a * qxx * aT;
            qvv = w.Inverse() - qLAjustlLAjust;
        }

        public double RejectionLevel
        {
            get { return rejectionLevel; }
            set { rejectionLevel = value; }
        }

        public Matrix VStand
        {
            get { return vStand; }
            set { vStand = value; }
        }

        public Matrix AbsVStand
        {
            get { return absVStand; }
            set { absVStand = value; }
        }

        public double Var
        {
            get { return var; }
            set { var = value; }
        }

        public bool[] VStandTest
        {
            get { return vStandTest; }
            set { vStandTest = value; }
        }
    }
}
