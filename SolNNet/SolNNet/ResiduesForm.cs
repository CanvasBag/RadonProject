using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeoMathLib.Calc.AjustNet2D;
using GeoMathLib.Calc;
using GeoMathLib;
using BaseCoordinates.Seed;
using BaseCoordinates.Geometry;
using BaseCoordinates.Elements;
using MathNet.Numerics.LinearAlgebra;
using AjustLeastSquare;
using AjustLeastSquare.Statistics;
using Meta.Numerics.Statistics.Distributions;

namespace SolNNet
{
    public partial class ResiduesForm : Form
    {
        private List<NetAdjust2D.ReadStationDist> listProcessDist, listOutDist;
        private List<NetAdjust2D.ReadStationDir> listProcessDir, listOutDir;
        private GeoCoord processarTrigPts, outTrigPts;
        private NonLinearParametric ajustamento;
        private DataSnooping dataSnooping;
        private SolNNetPrincipalForm form1;
        NetAdjust2D network2DTmp;

        public ResiduesForm(SolNNetPrincipalForm form1, List<NetAdjust2D.ReadStationDist> listProcessDistIn, List<NetAdjust2D.ReadStationDir> listProcessDirIn, GeoCoord processarTrigPtsIn, NonLinearParametric ajustamento)
        {
            try
            {
                InitializeComponent();
                listProcessDist = listProcessDistIn;
                listProcessDir = listProcessDirIn;
                processarTrigPts = processarTrigPtsIn;
                this.ajustamento = ajustamento;
                this.form1 = form1;
                dataSnooping = new DataSnooping(ajustamento.WeightsObs, ajustamento.FstDesignMatrix, ajustamento.CovariancesAjustedParam, ajustamento.Residuals, ajustamento.VarApriori, 2.8);
                dataSnooping.ComputeStandardizedResiduals();
                preencherGidView();
            }
            catch
            {
            }

        }

        private void preencherGidView()
        {
            int i = 0;
            int j = 0;//contador das linhas da data grid view

            Matrix residues = ajustamento.Residuals;
            Matrix a = ajustamento.FstDesignMatrix;
            Matrix aT = a.Clone();
            aT.Transpose();
            Matrix nInv = ajustamento.InvNormalMatrix;
            Matrix w = ajustamento.WeightsObs;
            Matrix qvv = ajustamento.CovariancesAjustedResiduals;
            Matrix covarParam = ajustamento.CovariancesAjustedParam;

            // Redundância Local
            Matrix localRedundancy = w * qvv;
            //Residuos Standardizados
            Matrix absVStand = dataSnooping.AbsVStand;

            foreach (EastingNorthing enzTmp in processarTrigPts.EastingNorthingList)
            {
                
                String easting = String.Format("{0,12:0.0000}", enzTmp.E);
                String northing = String.Format("{0,12:0.0000}", enzTmp.N);

                if (enzTmp.ListDadosDivInt[0] >= 0)
                {
                    String eastAcc = String.Format("{0,12:0.00000}", Math.Sqrt(Math.Abs(covarParam[i, i++])));
                    String northAcc = String.Format("{0,12:0.00000}", Math.Sqrt(Math.Abs(covarParam[i, i++])));                    
                    String eastVariance = String.Format("{0,12:0.0000}", enzTmp.E - enzTmp.ListDadosDivD[0]);
                    String northVariance = String.Format("{0,12:0.0000}", enzTmp.N - enzTmp.ListDadosDivD[1]);

                    dataGVStations.Rows.Add(true, enzTmp, easting, northing, eastAcc, northAcc, eastVariance, northVariance);
                    if (enzTmp.ListDadosDivInt[1] == 1)
                        dataGVStations.Rows[j].Cells[1].Style.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    dataGVStations.Rows.Add(true, enzTmp, easting, northing, String.Format("{0,12:0.00000}", 0), String.Format("{0,12:0.00000}", 0), String.Format("{0,12:0.00000}", 0), String.Format("{0,12:0.00000}", 0));
                    dataGVStations.Rows[j].Cells[1].Style.ForeColor = System.Drawing.Color.Maroon;
                }
                j++;
            }

            i = 0;
            j = 0;
            foreach (NetAdjust2D.ReadStationDir rStDirTmp in listProcessDir)
            {
                String direction = String.Format("{0,12:0.0000000}", rStDirTmp.direction / SIUnits.Deg2Rad);
                String obsResidue = String.Format("{0,12:0.0000}", residues[i, 0] / SIUnits.Seg2Rad);
                String sLocalRedundancy = String.Format("{0,12:0.0000}", localRedundancy[i, i]);
                String sAbsVStand = String.Format("{0,12:0.0000}", absVStand[i, 0]);

                dataGVDirections.Rows.Add(true, rStDirTmp, direction, obsResidue, sLocalRedundancy, sAbsVStand);
                if (!dataSnooping.VStandTest[i])
                    dataGVDirections.Rows[j].Cells[5].Style.ForeColor = System.Drawing.Color.Red;
                i++;
                j++;
            }

            j = 0;
            foreach (NetAdjust2D.ReadStationDist rStDistTmp in listProcessDist)
            {
                String distance = String.Format("{0,12:0.0000000}", rStDistTmp.distance);
                String obsResidue = String.Format("{0,12:0.0000}", residues[i, 0]);
                String sLocalRedundancy = String.Format("{0,12:0.0000}", localRedundancy[i, i]);
                String sAbsVStand = String.Format("{0,12:0.0000}", absVStand[i, 0]);

                dataGVDistances.Rows.Add(true, rStDistTmp, distance, obsResidue, sLocalRedundancy, sAbsVStand);
                if (!dataSnooping.VStandTest[i])
                    dataGVDistances.Rows[j].Cells[5].Style.ForeColor = System.Drawing.Color.Red;
                i++;
                j++;
            }
        }

        private void acceptBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateBut_Click(object sender, EventArgs e)
        {
            listOutDist = new List<NetAdjust2D.ReadStationDist>();
            listOutDir = new List<NetAdjust2D.ReadStationDir>();
            outTrigPts = new GeoCoord();

            foreach (DataGridViewRow rowTmp in dataGVStations.Rows)
            {
                if ((Boolean)rowTmp.Cells[0].Value)
                {
                    EastingNorthing enzTmp = (EastingNorthing)rowTmp.Cells[1].Value;
                    enzTmp.E = enzTmp.ListDadosDivD[0];
                    enzTmp.N = enzTmp.ListDadosDivD[1];
                    outTrigPts.EastingNorthingList.Add(enzTmp);
                }
            }

            foreach (DataGridViewRow rowTmp in dataGVDistances.Rows)
            {
                if ((Boolean)rowTmp.Cells[0].Value)
                    listOutDist.Add((NetAdjust2D.ReadStationDist)rowTmp.Cells[1].Value);
            }

            foreach (DataGridViewRow rowTmp in dataGVDirections.Rows)
            {
                if ((Boolean)rowTmp.Cells[0].Value)
                    listOutDir.Add((NetAdjust2D.ReadStationDir)rowTmp.Cells[1].Value);
            }

            network2DTmp = new NetAdjust2D(listOutDist, listOutDir, outTrigPts);
            form1.fillCheckBoxsFromResidue(network2DTmp);
            form1.acrescentarRestantes();
            this.Close();
        }

        private void dataGVStations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = 0;
        }
    }
}
