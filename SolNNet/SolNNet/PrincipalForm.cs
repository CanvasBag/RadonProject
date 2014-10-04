using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeoMathLib;
using GeoMathLib.Calc;
using GeoMathLib.Calc.AjustNet2D;
using BaseCoordinates.Seed;
using BaseCoordinates.Geometry;
using BaseCoordinates.Elements;
using System.Drawing.Drawing2D;
using MathNet.Numerics.LinearAlgebra;
using AjustLeastSquare;
using Meta.Numerics.Statistics.Distributions;
using AjustLeastSquare.Statistics;

namespace SolNNet
{
    public partial class SolNNetPrincipalForm : Form
    {
        private NetAdjust2D network2D, networ2DTmp;
        private Boolean selectButTrigState = true, selectButContrState = false, selectButFixState = false, selectButDistState = true, selectButDirState = true, selectButAzState = true;
        private Drawing drawDisplayPainel_1;
        private Boolean testQuiSquareValidated;
        private NonLinearParametric ajustamento;
        private List<NetAdjust2D.ReadStationDist> listProcessDist;
        private List<NetAdjust2D.ReadStationDir> listProcessDir;
        private GeoCoord processarTrigPts;
        private Boolean mouseClicked = false, zoomIn = false;
        private int mouseX, mouseY, labelX = -100, labelY = -100;
        MouseEventArgs mouseClickedEnvent;
        //List<Ellipse> errorElipses = new List<Ellipse>();

        DataSnooping dataSnooping;

        public SolNNetPrincipalForm()
        {
            InitializeComponent();
        }

        #region Buttons

        private void browseXmlBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml files (*.xml)|*.xml";

            dialog.Title = "Select a SolNNet XML File";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                browseXmltextBox.Text = dialog.FileName;
                network2D = new NetAdjust2D(browseXmltextBox.Text);
                dataGVControlPts.Rows.Clear();
                fillCheckBoxs(network2D);
                drawDisplayPainel_1 = new Drawing(clBoxTrigPts, clBoxControlPts, clBoxFixedPts, clBoxDist, clBoxDir, displayPanel,
                                                  eixoYY1Label, eixoYY2Label, eixoYY3Label, eixoYY4Label, eixoXX1Label, eixoXX2Label, eixoXX3Label,
                                                  escalaElipseLabel, Convert.ToDouble(ellipseScaleTBox.Text));
            }
        }

        private void drawBut_Click(object sender, EventArgs e)
        {
            try
            {
                drawDisplayPainel_1.PanelBorder = 8;
                drawDisplayPainel_1.DrawFitView();
            }
            catch
            {
            }
        }


        private void computeBut_Click(object sender, EventArgs e)
        {
            try
            {
                listProcessDist = new List<NetAdjust2D.ReadStationDist>();
                listProcessDir = new List<NetAdjust2D.ReadStationDir>();
                processarTrigPts = new GeoCoord();
                int i = 0, fixedPts = 0, indexDataGridView = 0;

                foreach (EastingNorthing trigTmp in clBoxTrigPts.CheckedItems)
                {
                    EastingNorthing trigTmpCopy = trigTmp.Clone();//copia para não alterar os dados originais
                    trigTmpCopy.ListDadosDivInt[0] = i++;
                    trigTmpCopy.ListDadosDivInt[1] = 0;
                    processarTrigPts.EastingNorthingList.Add(trigTmpCopy);
                }
                foreach (EastingNorthing trigTmp in clBoxControlPts.CheckedItems)
                {
                    EastingNorthing trigTmpCopy = trigTmp.Clone();//copia para não alterar os dados originais
                    trigTmpCopy.ListDadosDivInt[0] = i++;
                    trigTmpCopy.ListDadosDivInt[1] = 1;
                    indexDataGridView = rowIndexDataGridView(trigTmpCopy);//index do control point na dataGridView (coluna)
                    if (indexDataGridView >= 0)
                    {
                        if (Convert.ToDouble(dataGVControlPts.Rows[indexDataGridView].Cells[1].Value) == 0 || Convert.ToDouble(dataGVControlPts.Rows[indexDataGridView].Cells[2].Value) == 0)//verificar se a var do pto é maior q zero
                        {
                            trigTmp.ListDadosDivInt[0] = -1;
                            trigTmp.ListDadosDivInt[1] = 0;
                            processarTrigPts.EastingNorthingList.Add(trigTmpCopy);
                        }
                        else
                        {
                            trigTmpCopy.SigmaE = Convert.ToDouble(dataGVControlPts.Rows[indexDataGridView].Cells[1].Value) / 1000;
                            trigTmpCopy.SigmaN = Convert.ToDouble(dataGVControlPts.Rows[indexDataGridView].Cells[2].Value) / 1000;
                            processarTrigPts.EastingNorthingList.Add(trigTmpCopy);
                        }
                    }
                    fixedPts++;
                }
                foreach (EastingNorthing trigTmp in clBoxFixedPts.CheckedItems)
                {
                    EastingNorthing trigTmpCopy = trigTmp.Clone();
                    trigTmpCopy.ListDadosDivInt[0] = -1;
                    trigTmpCopy.ListDadosDivInt[1] = 0;
                    processarTrigPts.EastingNorthingList.Add(trigTmpCopy);
                    fixedPts++;
                }
                foreach (NetAdjust2D.ReadStationDist readStDist in clBoxDist.CheckedItems)
                {
                    NetAdjust2D.ReadStationDist readStDistNova = new NetAdjust2D.ReadStationDist();
                    readStDistNova.occupied = replaceTrigInObservations(readStDist.occupied); //substituir as estacoes occupied e observed pelas novas do geocoord processarTrigPts
                    readStDistNova.observed = replaceTrigInObservations(readStDist.observed); //substituir as estacoes occupied e observed pelas novas do geocoord processarTrigPts
                    readStDistNova.distance = readStDist.distance;
                    readStDistNova.id = readStDist.id;
                    listProcessDist.Add(readStDistNova);
                }

                List<int> r0sID = new List<int>();
                foreach (NetAdjust2D.ReadStationDir readStDir in clBoxDir.CheckedItems)
                {
                    NetAdjust2D.ReadStationDir readStDirNova = new NetAdjust2D.ReadStationDir();

                    int j = 0;

                    //adicionar novo R0 à lista
                    if (!r0sID.Contains(readStDir.az0))
                        r0sID.Add(readStDir.az0);
                    //pesquisar a posicao do R0 na lista de R0s
                    for (j = 0; j < r0sID.Count; j++)
                    {
                        if (r0sID[j] == readStDir.az0)
                            break;
                    }

                    readStDirNova.occupied = replaceTrigInObservations(readStDir.occupied); //substituir as estacoes occupied e observed pelas novas do geocoord processarTrigPts
                    readStDirNova.observed = replaceTrigInObservations(readStDir.observed); //substituir as estacoes occupied e observed pelas novas do geocoord processarTrigPts

                    readStDirNova.az0 = j;
                    readStDirNova.direction = readStDir.direction;
                    readStDirNova.id = readStDir.id;
                    listProcessDir.Add(readStDirNova);
                }

                if (fixedPts < 2)
                {
                    MessageBox.Show("Network not fixed!\r\n" + "Fixed + Control points shall at least 2.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (Double.Parse(aValueTextB.Text) < 0
                    || Double.Parse(bValueTextB.Text) < 0
                    || Double.Parse(stdvDirectionsTextB.Text) < 0
                    || Double.Parse(stdvAzTextB.Text) < 0)
                {
                    MessageBox.Show("Observations Stdv not all difined!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                networ2DTmp = new NetAdjust2D();
                networ2DTmp.ObsListDist = listProcessDist;
                networ2DTmp.ObsListDir = listProcessDir;
                networ2DTmp.TrigPts = processarTrigPts;
                
                try
                {

                    ajustamento = networ2DTmp.ajustNet(Convert.ToDouble(aValueTextB.Text) / 1000, Convert.ToDouble(bValueTextB.Text),
                                                       Convert.ToDouble(stdvDirectionsTextB.Text), Convert.ToDouble(stdvAzTextB.Text));
                    dataSnooping = new DataSnooping(ajustamento.WeightsObs, ajustamento.FstDesignMatrix, ajustamento.CovariancesAjustedParam, ajustamento.Residuals, ajustamento.VarApriori, 2.8);
                    fillEastNorthingSigma(ajustamento);
                    fillObservationsBias(ajustamento, dataSnooping);
                    testQuiSquareValidated = quiSquareTest(ajustamento.VarApriori, ajustamento.VarAposteriori, ajustamento.DegreeOfFreedom, .05);
                    varPosterioriLabel.ForeColor = testQuiSquareValidated ? Color.Green : Color.Red;

                    drawDisplayPainel_1.EscalaElipsesMM = Convert.ToDouble(ellipseScaleTBox.Text);
                    drawDisplayPainel_1.ErrorElipses = determinarElipsesErro(ajustamento);
                    drawDisplayPainel_1.ConfidenceElipses = determinarElipsesConfianca(ajustamento, Convert.ToDouble(confidanceElipseTBox.Text));
                    //drawDisplayPainel_1.draw();

                    drawDisplayPainel_1.Deslocamentos = pointDisplacements();
                    drawDisplayPainel_1.draw();

                    varPosterioriLabel.Text = String.Format("{0,10:0.0000}", ajustamento.VarAposteriori);
                    meanResidueLabel.Text = String.Format("{0,10:0.0000}", resideusAverage(ajustamento));
                    degreeOfFreedomLabel.Text = String.Format("{0,10:0}", ajustamento.DegreeOfFreedom);
                }
                catch (Exception erro)
                {
                    if (ajustamento.DegreeOfFreedom <= 0)
                        MessageBox.Show("Singular Matrix\n\rInsuficient number of observations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (Convert.ToDouble(confidanceElipseTBox.Text) >= 1 || Convert.ToDouble(confidanceElipseTBox.Text) <= 0)                    
                        MessageBox.Show("Confidence level value not correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                    else
                        MessageBox.Show("Process Aborted\r\n\r\n" + erro.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Process Aborted\r\n\r\n" + erro.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void residualsBut_Click(object sender, EventArgs e)
        {
            ResiduesForm residuos = new ResiduesForm((SolNNetPrincipalForm)SolNNetPrincipalForm.ActiveForm, listProcessDist, listProcessDir, processarTrigPts, ajustamento);
            residuos.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            drawDisplayPainel_1.zoom(.5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //zoomIn = true;
            //SolNNetPrincipalForm.ActiveForm.Cursor = Cursors.Cross;
            drawDisplayPainel_1.zoom(1.5);
        }

        private void reportBut_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "xml files (*.html)|*.html";

            dialog.Title = "Save Report";
            int numBlunderDir = dataSnoopingBlunderDir(dataSnooping);
            int numBlunderDist = dataSnoopingBlunderDist(dataSnooping);
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                HtmlReport export = new HtmlReport(dialog.FileName, ajustamento.NumIterations, ajustamento.VarApriori, ajustamento.VarAposteriori,
                    testQuiSquareValidated, .05, ajustamento.DegreeOfFreedom, networ2DTmp.NumNormalPts, networ2DTmp.NumControlPts, networ2DTmp.NumFixedPts,
                    dataSnooping.RejectionLevel, listProcessDist.Count, numBlunderDist, dataSnoopingTestListDist(dataSnooping), listProcessDir.Count,
                    numBlunderDir, dataSnoopingTestListDir(dataSnooping), "m", " d", 1 / SIUnits.Deg2Rad, " s", 1 / SIUnits.Seg2Rad, pointDisplacements(), processarTrigPts, listProcessDist, listProcessDir);
            }

        }

        private void elipseBut_Click(object sender, EventArgs e)
        {
            drawDisplayPainel_1.ElipseOn = drawDisplayPainel_1.ElipseOn == 2 ? 0 : drawDisplayPainel_1.ElipseOn + 1;
            drawDisplayPainel_1.draw();
        }

        private void gridBut_Click(object sender, EventArgs e)
        {
            try
            {
                drawDisplayPainel_1.GridOn = drawDisplayPainel_1.GridOn == 2 ? 0 : drawDisplayPainel_1.GridOn + 1;
                drawDisplayPainel_1.draw();
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drawDisplayPainel_1.ArrowOn = !drawDisplayPainel_1.ArrowOn;
            drawDisplayPainel_1.draw();
        }

        private void dXFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ajustamento != null)
            {
                List<CheckedListBox> clBoxList = new List<CheckedListBox>();
                clBoxList.Add(clBoxTrigPts);
                clBoxList.Add(clBoxControlPts);
                clBoxList.Add(clBoxFixedPts);
                clBoxList.Add(clBoxDir);
                clBoxList.Add(clBoxDist);
                clBoxList.Add(clBoxAzimuths);


                FormDxfExport dxfExportForm = new FormDxfExport(clBoxList, pointDisplacements(), determinarElipsesErro(ajustamento),
                                              determinarElipsesConfianca(ajustamento, Convert.ToDouble(confidanceElipseTBox.Text)));
                dxfExportForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ajustment not processed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Auxiliar methods

        public List<Ellipse> determinarElipsesErro(NonLinearParametric ajust)
        {
            List<Ellipse> errorElipses = new List<Ellipse>();
            foreach (EastingNorthing enzTmp in processarTrigPts.EastingNorthingList)
            {
                //if (enzTmp.ListDadosDivInt[0] >= 0 && enzTmp.ListDadosDivInt[1] == 0)
                if (enzTmp.ListDadosDivInt[0] >= 0)

                {
                    Ellipse elipseTmp = new Ellipse(enzTmp, ajust.CovariancesAjustedParam, ajust.VarApriori);
                    elipseTmp.ComputeStandardErrorEllipse();
                    errorElipses.Add(elipseTmp);
                }
            }
            return errorElipses;
        }

        public List<Ellipse> determinarElipsesConfianca(NonLinearParametric ajust, double alpha)
        {
            List<Ellipse> errorElipses;

            errorElipses = new List<Ellipse>();
            foreach (EastingNorthing enzTmp in processarTrigPts.EastingNorthingList)
            {
                if (enzTmp.ListDadosDivInt[0] >= 0)
                {
                    Ellipse elipseTmp = new Ellipse(enzTmp, ajust.CovariancesAjustedParam, ajust.VarApriori, ajust.DegreeOfFreedom, 1 - alpha);
                    elipseTmp.ComputeErrorEllipseConfidenceLevel();
                    errorElipses.Add(elipseTmp);
                }
            }
            return errorElipses;

        }

        public double resideusAverage(NonLinearParametric ajustamento)
        {
            double average = 0;
            for (int i = 0; i < ajustamento.Residuals.RowCount; i++)
            {
                average += ajustamento.Residuals[i, 0];
            }
            average /= ajustamento.Residuals.RowCount;
            return average;
        }

        public void fillObservationsBias(NonLinearParametric ajustamento, DataSnooping dataSnooping)
        {
            MathNet.Numerics.LinearAlgebra.Matrix residues = ajustamento.Residuals;
            MathNet.Numerics.LinearAlgebra.Matrix w = ajustamento.WeightsObs;
            MathNet.Numerics.LinearAlgebra.Matrix qvv = ajustamento.CovariancesAjustedResiduals;
            MathNet.Numerics.LinearAlgebra.Matrix localRedundancy = w * qvv;
            MathNet.Numerics.LinearAlgebra.Matrix absVStand = dataSnooping.AbsVStand;
            int i = 0;
            int j = 0;
            foreach (NetAdjust2D.ReadStationDir rStDirTmp in listProcessDir)
            {
                rStDirTmp.bias = Math.Sqrt(ajustamento.CovariancesObs[i, i]);
                rStDirTmp.residue = residues[i, 0];
                rStDirTmp.localRedundancy = localRedundancy[i, i];
                rStDirTmp.stdResidue = absVStand[i, 0];
                i++;
            }
            foreach (NetAdjust2D.ReadStationDist rStDistTmp in listProcessDist)
            {
                rStDistTmp.bias = Math.Sqrt(ajustamento.CovariancesObs[i, i]);
                rStDistTmp.residue = residues[i, 0];
                rStDistTmp.localRedundancy = localRedundancy[i, i];
                rStDistTmp.stdResidue = absVStand[i, 0];
                i++;
            }

        }

        public void fillEastNorthingSigma(NonLinearParametric ajustamento)
        {
            int i = 0;
            MathNet.Numerics.LinearAlgebra.Matrix covarParam = ajustamento.CovariancesAjustedParam;
            foreach (EastingNorthing enzTmp in processarTrigPts.EastingNorthingList)
            {
                if (enzTmp.ListDadosDivInt[0] >= 0)
                {
                    enzTmp.SigmaE = Math.Sqrt(Math.Abs(covarParam[i, i++]));
                    enzTmp.SigmaN = Math.Sqrt(Math.Abs(covarParam[i, i++]));
                }
            }
        }
        public List<double> pointDisplacements()
        {
            List<double> displacments = new List<double>();
            foreach (EastingNorthing enzTmp in processarTrigPts.EastingNorthingList)
            {
                if (enzTmp.ListDadosDivInt[0] >= 0)
                {
                    displacments.Add(enzTmp.E - enzTmp.ListDadosDivD[0]);
                    displacments.Add(enzTmp.N - enzTmp.ListDadosDivD[1]);
                }
            }
            return displacments;
        }
        public int dataSnoopingBlunderDir(DataSnooping dataSnooping)
        {
            int numBlunder = 0;
            for (int i = 0; i < listProcessDir.Count; i++)
            {
                numBlunder += dataSnooping.VStandTest[i] ? 1 : 0;
            }
            return numBlunder;
        }

        public int dataSnoopingBlunderDist(DataSnooping dataSnooping)
        {
            int numBlunder = 0;
            for (int i = listProcessDir.Count; i < listProcessDist.Count + listProcessDir.Count; i++)
            {
                numBlunder += dataSnooping.VStandTest[i] ? 1 : 0;
            }
            return numBlunder;
        }

        public List<bool> dataSnoopingTestListDir(DataSnooping dataSnooping)
        {
            List<bool> listTmp = new List<bool>();
            for (int i = 0; i < listProcessDir.Count; i++)
            {
                listTmp.Add(dataSnooping.VStandTest[i]);
            }
            return listTmp;
        }

        public List<bool> dataSnoopingTestListDist(DataSnooping dataSnooping)
        {
            List<bool> listTmp = new List<bool>();
            for (int i = listProcessDir.Count; i < listProcessDist.Count + listProcessDir.Count; i++)
            {
                listTmp.Add(dataSnooping.VStandTest[i]);
            }
            return listTmp;
        }

        public void fillCheckBoxs(NetAdjust2D network2DIn)
        {
            int i = 0;
            clBoxTrigPts.Items.Clear();
            clBoxControlPts.Items.Clear();
            clBoxFixedPts.Items.Clear();
            clBoxDist.Items.Clear();
            clBoxDir.Items.Clear();
            foreach (EastingNorthing enzTmp in network2DIn.TrigPts.EastingNorthingList)
            {
                if (enzTmp.ListDadosDivInt[0] >= 0)
                {
                    if (enzTmp.ListDadosDivInt[1] == 0)
                        clBoxTrigPts.Items.Add(enzTmp, true);
                    else
                        clBoxControlPts.Items.Add(enzTmp, true);
                }
                else
                    clBoxFixedPts.Items.Add(enzTmp, true);
            }

            foreach (NetAdjust2D.ReadStationDist readStationDist in network2DIn.ObsListDist)
            {
                clBoxDist.Items.Add(readStationDist, true);
            }

            foreach (NetAdjust2D.ReadStationDir readStationDir in network2DIn.ObsListDir)
            {
                clBoxDir.Items.Add(readStationDir, true);
            }
        }

        public void fillCheckBoxsFromResidue(NetAdjust2D network2DIn)
        {
            int i = 0;
            clBoxTrigPts.Items.Clear();
            clBoxControlPts.Items.Clear();
            clBoxFixedPts.Items.Clear();
            clBoxDist.Items.Clear();
            clBoxDir.Items.Clear();
            foreach (EastingNorthing enzTmp in network2DIn.TrigPts.EastingNorthingList)
            {
                if (enzTmp.ListDadosDivInt[0] >= 0)
                {
                    if (enzTmp.ListDadosDivInt[1] == 0)
                        clBoxTrigPts.Items.Add(replaceOldTrigInObservations(enzTmp), true);
                    else
                        clBoxControlPts.Items.Add(replaceOldTrigInObservations(enzTmp), true);
                }
                else
                    clBoxFixedPts.Items.Add(replaceOldTrigInObservations(enzTmp), true);
            }

            foreach (NetAdjust2D.ReadStationDist readStationDist in network2DIn.ObsListDist)
            {
                clBoxDist.Items.Add(replaceOldDistances(readStationDist), true);
            }

            foreach (NetAdjust2D.ReadStationDir readStationDir in network2DIn.ObsListDir)
            {
                clBoxDir.Items.Add(replaceOldDirection(readStationDir), true);
            }
        }

        public void acrescentarRestantes()
        {
            foreach (EastingNorthing enzTmp in network2D.TrigPts.EastingNorthingList)
            {
                Boolean miss = true;
                foreach (EastingNorthing enzTmp2 in clBoxTrigPts.Items)
                {
                    miss = enzTmp.ID == enzTmp2.ID ? false : miss;
                }
                foreach (EastingNorthing enzTmp2 in clBoxControlPts.Items)
                {
                    miss = enzTmp.ID == enzTmp2.ID ? false : miss;
                }
                foreach (EastingNorthing enzTmp2 in clBoxFixedPts.Items)
                {
                    miss = enzTmp.ID == enzTmp2.ID ? false : miss;
                }
                if (miss)
                {
                    clBoxTrigPts.Items.Add(enzTmp);
                    clBoxControlPts.Items.Add(enzTmp);
                    clBoxFixedPts.Items.Add(enzTmp);
                    //remover da tab others - variancias do ponto de controlo
                    int indexDataGridView = -1;
                    indexDataGridView = rowIndexDataGridView(enzTmp);
                    if (indexDataGridView >= 0)
                        dataGVControlPts.Rows.Remove(dataGVControlPts.Rows[indexDataGridView]);
                }
            }

            foreach (NetAdjust2D.ReadStationDist distTmp in network2D.ObsListDist)
            {
                Boolean miss = true;
                foreach (NetAdjust2D.ReadStationDist distTmp2 in clBoxDist.Items)
                {
                    miss = distTmp.id == distTmp2.id ? false : miss;
                }
                if (miss)
                {
                    clBoxDist.Items.Add(distTmp);
                }
            }

            foreach (NetAdjust2D.ReadStationDir dirTmp in network2D.ObsListDir)
            {
                Boolean miss = true;
                foreach (NetAdjust2D.ReadStationDir dirTmp2 in clBoxDir.Items)
                {
                    miss = dirTmp.id == dirTmp2.id ? false : miss;
                }
                if (miss)
                {
                    clBoxDir.Items.Add(dirTmp);
                }
            }
        }

        private void distCheck_CheckedChanged(object sender, EventArgs e)
        {
            drawDisplayPainel_1.DistCheck = distCheck.Checked;
        }

        private void dirCheck_CheckedChanged(object sender, EventArgs e)
        {
            drawDisplayPainel_1.DirCheck = dirCheck.Checked;
        }

        private void azCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private Boolean quiSquareTest(Double varPriori, Double varPost, int df, Double alpha)
        {
            ChiSquaredDistribution quiSquare = new ChiSquaredDistribution(df);
            Double quiResultInf = quiSquare.InverseRightProbability(1 - alpha / 2);
            Double quiResultSup = quiSquare.InverseRightProbability(alpha / 2);
            Double varTeste = varPost / varPriori * df;
            return quiResultInf < varTeste && varTeste < quiResultSup;
        }

        public int rowIndexDataGridView(EastingNorthing enzTmp)
        {
            int indexDataGridView = -1;
            foreach (DataGridViewRow linha in dataGVControlPts.Rows)
            {
                EastingNorthing enzTmp2 = (EastingNorthing)linha.Cells[0].Value;
                indexDataGridView = enzTmp2.ID == enzTmp.ID ? linha.Index : indexDataGridView;
            }
            return indexDataGridView;
        }

        private EastingNorthing replaceTrigInObservations(EastingNorthing enzIN)
        {
            EastingNorthing enzTmp = new EastingNorthing();
            foreach (EastingNorthing enzTmp1 in processarTrigPts.EastingNorthingList)
            {
                if (enzTmp1.ID == enzIN.ID)
                {
                    enzTmp = enzTmp1;
                    break;
                }
            }
            return enzTmp;
        }

        private EastingNorthing replaceOldTrigInObservations(EastingNorthing enzIN)
        {
            EastingNorthing enzTmp = new EastingNorthing();
            foreach (EastingNorthing enzTmp1 in network2D.TrigPts.EastingNorthingList)
            {
                if (enzTmp1.ID == enzIN.ID)
                {
                    enzTmp = enzTmp1;
                    break;
                }
            }
            return enzTmp;
        }

        private NetAdjust2D.ReadStationDir replaceOldDirection(NetAdjust2D.ReadStationDir readDirIn)
        {
            NetAdjust2D.ReadStationDir readDirTmp = new NetAdjust2D.ReadStationDir();
            foreach (NetAdjust2D.ReadStationDir readDirTmp1 in network2D.ObsListDir)
            {
                if (readDirTmp1.id == readDirIn.id)
                {
                    readDirTmp = readDirTmp1;
                    break;
                }
            }
            return readDirTmp;
        }

        private NetAdjust2D.ReadStationDist replaceOldDistances(NetAdjust2D.ReadStationDist readDistIn)
        {
            NetAdjust2D.ReadStationDist readDistTmp = new NetAdjust2D.ReadStationDist();
            foreach (NetAdjust2D.ReadStationDist readDistTmp1 in network2D.ObsListDist)
            {
                if (readDistTmp1.id == readDistIn.id)
                {
                    readDistTmp = readDistTmp1;
                    break;
                }
            }
            return readDistTmp;
        }

        private void drawLabelCoord(MouseEventArgs e, bool stay)
        {
            if (!stay)
            {
                labelX = e.X;
                labelY = e.Y;
            }

            Double BX = drawDisplayPainel_1.BX;
            Double BY = drawDisplayPainel_1.BY;
            Double AX = drawDisplayPainel_1.AX;
            Double AY = drawDisplayPainel_1.AY;

            mouseCoord.Visible = true;
            //mouseCoord.Text = String.Format("{0,5:00.0} , {0,5:00.0}", (e.X - BX) / AX) (e.Y - BY) / AY;
            mouseCoord.Text = String.Format("({0,5:00.00} , {1,5:00.00})", (labelX - BX) / AX, (labelY - BY) / AY);

            mouseCoord.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            mouseCoord.Location = new System.Drawing.Point(labelX, labelY);
            mouseCoord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }

        private void deSelectObservations(EastingNorthing enzTmp)
        {
            for (int i = 0; i < clBoxDist.Items.Count; i++)
            {
                NetAdjust2D.ReadStationDist stsTmp = (NetAdjust2D.ReadStationDist)clBoxDist.Items[i];
                if (stsTmp.observed.ID == enzTmp.ID || stsTmp.occupied.ID == enzTmp.ID)
                {
                    clBoxDist.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
            for (int i = 0; i < clBoxDir.Items.Count; i++)
            {
                NetAdjust2D.ReadStationDir stsTmp = (NetAdjust2D.ReadStationDir)clBoxDir.Items[i];
                if (stsTmp.observed.ID == enzTmp.ID || stsTmp.occupied.ID == enzTmp.ID)
                {
                    clBoxDir.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }
        #endregion

        #region CheckedBoxList Events

        private void clBoxTrigPts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckedListBox clBoxTmp = (CheckedListBox)sender;
                EastingNorthing enzTmp = (EastingNorthing)clBoxTmp.Items[clBoxTmp.SelectedIndex];
                if (clBoxTmp.GetItemChecked(clBoxTmp.SelectedIndex))
                {
                    int indexFixedPt = clBoxFixedPts.FindStringExact(enzTmp.ID);
                    int indexControlPt = clBoxControlPts.FindStringExact(enzTmp.ID);
                    clBoxFixedPts.Items.RemoveAt(indexFixedPt);
                    clBoxControlPts.Items.RemoveAt(indexControlPt);
                }
                else
                {
                    if (clBoxFixedPts.FindStringExact(enzTmp.ID) < 0)
                        clBoxFixedPts.Items.Add(enzTmp, false);
                    if (clBoxControlPts.FindStringExact(enzTmp.ID) < 0)
                        clBoxControlPts.Items.Add(enzTmp, false);
                    
                    deSelectObservations(enzTmp);
                }
                if (cBoxSincDisplayAndObs.Checked)
                {
                    drawDisplayPainel_1.PanelBorder = 8;
                    drawDisplayPainel_1.draw();
                }
            }
            catch (Exception eerror)
            {
            }
        }

        private void clBoxControlPts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                CheckedListBox clBoxTmp = (CheckedListBox)sender;
                EastingNorthing enzTmp = (EastingNorthing)clBoxTmp.Items[clBoxTmp.SelectedIndex];
                if (clBoxTmp.GetItemChecked(clBoxTmp.SelectedIndex))
                {
                    int indexFixedPt = clBoxFixedPts.FindStringExact(enzTmp.ID);
                    clBoxFixedPts.Items.RemoveAt(indexFixedPt);
                    int indexTrigPt = clBoxTrigPts.FindStringExact(enzTmp.ID);
                    clBoxTrigPts.Items.RemoveAt(indexTrigPt);
                    dataGVControlPts.Rows.Add(enzTmp, "0", "0");
                }
                else
                {
                    if (clBoxFixedPts.FindStringExact(enzTmp.ID) < 0)
                        clBoxFixedPts.Items.Add(enzTmp, false);
                    if (clBoxTrigPts.FindStringExact(enzTmp.ID) < 0)
                        clBoxTrigPts.Items.Add(enzTmp, false);
                    int indexDataGridView = 0;
                    indexDataGridView = rowIndexDataGridView(enzTmp);
                    if (indexDataGridView >= 0)
                        dataGVControlPts.Rows.Remove(dataGVControlPts.Rows[indexDataGridView]);
                    deSelectObservations(enzTmp);
                }
                if (cBoxSincDisplayAndObs.Checked)
                {
                    drawDisplayPainel_1.PanelBorder = 8;
                    drawDisplayPainel_1.draw();
                }
            }
            catch
            {
            }
        }

        private void clBoxFixedPts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckedListBox clBoxTmp = (CheckedListBox)sender;
                EastingNorthing enzTmp = (EastingNorthing)clBoxTmp.Items[clBoxTmp.SelectedIndex];
                if (clBoxTmp.GetItemChecked(clBoxTmp.SelectedIndex))
                {
                    int indexFixedPt = clBoxControlPts.FindStringExact(enzTmp.ID);
                    clBoxControlPts.Items.RemoveAt(indexFixedPt);
                    int indexTrigPt = clBoxTrigPts.FindStringExact(enzTmp.ID);
                    clBoxTrigPts.Items.RemoveAt(indexTrigPt);
                }
                else
                {
                    if (clBoxControlPts.FindStringExact(enzTmp.ID) < 0)
                        clBoxControlPts.Items.Add(enzTmp, false);
                    if (clBoxTrigPts.FindStringExact(enzTmp.ID) < 0)
                        clBoxTrigPts.Items.Add(enzTmp, false);
                    deSelectObservations(enzTmp);
                }
                if (cBoxSincDisplayAndObs.Checked)
                {
                    drawDisplayPainel_1.PanelBorder = 8;
                    drawDisplayPainel_1.draw();
                }
            }
            catch
            {
            }
        }

        private void clBoxDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxSincDisplayAndObs.Checked)
            {
                drawDisplayPainel_1.PanelBorder = 8;
                drawDisplayPainel_1.draw();
            }
        }

        private void clBoxDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxSincDisplayAndObs.Checked)
            {
                drawDisplayPainel_1.PanelBorder = 8;
                drawDisplayPainel_1.draw();
            }
        }

        #endregion

        #region Selection buttons

        private void selectButTrigPts_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clBoxTrigPts.Items.Count; i++)
            {
                if (selectButTrigState)
                {
                    if (clBoxTrigPts.GetItemChecked(i))
                    {
                        clBoxTrigPts.SetItemChecked(i, false);
                        clBoxControlPts.Items.Add(clBoxTrigPts.Items[i], false);
                        clBoxFixedPts.Items.Add(clBoxTrigPts.Items[i], false);
                    }
                }
                else
                {
                    if (!clBoxTrigPts.GetItemChecked(i))
                    {
                        clBoxTrigPts.SetItemChecked(i, true);
                        EastingNorthing enzTmp = (EastingNorthing)clBoxTrigPts.Items[i];
                        int indexFixedPt = clBoxFixedPts.FindStringExact(enzTmp.ID);
                        int indexControlPt = clBoxControlPts.FindStringExact(enzTmp.ID);
                        if (indexFixedPt >= 0)
                            clBoxFixedPts.Items.RemoveAt(indexFixedPt);
                        if (indexControlPt >= 0)
                            clBoxControlPts.Items.RemoveAt(indexControlPt);
                    }
                }
            }
            selectButTrigState = selectButTrigState ? false : true;
        }

        private void selectButContrPts_Click(object sender, EventArgs e)
        {
            dataGVControlPts.Rows.Clear();
            for (int i = 0; i < clBoxControlPts.Items.Count; i++)
            {
                if (selectButContrState)
                {
                    if (clBoxControlPts.GetItemChecked(i))
                    {
                        clBoxControlPts.SetItemChecked(i, false);
                        clBoxTrigPts.Items.Add(clBoxControlPts.Items[i], false);
                        clBoxFixedPts.Items.Add(clBoxControlPts.Items[i], false);
                    }
                }
                else
                {
                    clBoxControlPts.SetItemChecked(i, true);
                    EastingNorthing enzTmp = (EastingNorthing)clBoxControlPts.Items[i];
                    int indexFixedPt = clBoxFixedPts.FindStringExact(enzTmp.ID);
                    int indexTrigPt = clBoxTrigPts.FindStringExact(enzTmp.ID);
                    if (indexFixedPt >= 0)
                        clBoxFixedPts.Items.RemoveAt(indexFixedPt);
                    if (indexTrigPt >= 0)
                        clBoxTrigPts.Items.RemoveAt(indexTrigPt);
                    dataGVControlPts.Rows.Add(enzTmp, "0", "0");
                }

            }
            selectButContrState = selectButContrState ? false : true;
        }

        private void selectButFixedPts_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clBoxFixedPts.Items.Count; i++)
            {
                if (selectButFixState)
                {
                    if (clBoxFixedPts.GetItemChecked(i))
                    {
                        clBoxFixedPts.SetItemChecked(i, false);
                        clBoxTrigPts.Items.Add(clBoxFixedPts.Items[i], false);
                        clBoxControlPts.Items.Add(clBoxFixedPts.Items[i], false);
                    }
                }
                else
                {
                    clBoxFixedPts.SetItemChecked(i, true);
                    EastingNorthing enzTmp = (EastingNorthing)clBoxFixedPts.Items[i];
                    int indexContrPt = clBoxControlPts.FindStringExact(enzTmp.ID);
                    int indexTrigPt = clBoxTrigPts.FindStringExact(enzTmp.ID);
                    if (indexContrPt >= 0)
                        clBoxControlPts.Items.RemoveAt(indexContrPt);
                    if (indexTrigPt >= 0)
                        clBoxTrigPts.Items.RemoveAt(indexTrigPt);
                }
            }
            selectButFixState = selectButFixState ? false : true;
        }

        private void selectButDistance_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clBoxDist.Items.Count; i++)
            {
                if (selectButDistState)
                    clBoxDist.SetItemChecked(i, false);
                else
                    clBoxDist.SetItemChecked(i, true);
            }
            selectButDistState = selectButDistState ? false : true;
        }

        private void selectButDirections_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clBoxDir.Items.Count; i++)
            {
                if (selectButDirState)
                    clBoxDir.SetItemChecked(i, false);
                else
                    clBoxDir.SetItemChecked(i, true);
            }
            selectButDirState = selectButDirState ? false : true;
        }

        private void selectButAzimuths_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clBoxAzimuths.Items.Count; i++)
            {
                if (selectButAzState)
                    clBoxAzimuths.SetItemChecked(i, false);
                else
                    clBoxAzimuths.SetItemChecked(i, true);
            }
            selectButAzState = selectButAzState ? false : true;
        }

        #endregion

        #region MenuButtons

        private void dataConvertFormatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataConvertFormatsForm convertForm = new DataConvertFormatsForm();
            convertForm.ShowDialog();
        }

        #endregion

        private void displayPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //ZoomIn ZoomOut
                if (zoomIn)
                {
                    mouseX = e.X;
                    mouseY = e.Y;
                    drawDisplayPainel_1.ViewHeight /= 2;
                    drawDisplayPainel_1.ViewWidth /= 2;
                    //drawDisplayPainel_1.zoom();
                }
                //Drag image
                else
                {
                    SolNNetPrincipalForm.ActiveForm.Cursor = Cursors.Hand;
                    mouseX = e.X;
                    mouseY = e.Y;
                    mouseClickedEnvent = e;
                    mouseClicked = true;
                }
            }
        }

        private void displayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    mouseClicked = false;
                    drawDisplayPainel_1.ZeroPanelX += drawDisplayPainel_1.TranslacaoX;
                    drawDisplayPainel_1.ZeroPanelY += drawDisplayPainel_1.TranslacaoY;
                    drawDisplayPainel_1.Translade();
                    drawLabelCoord(mouseClickedEnvent, true);
                    SolNNetPrincipalForm.ActiveForm.Cursor = Cursors.Default;
                }

                if (e.Button == MouseButtons.Right)
                {
                    drawLabelCoord(e, false);
                }
            }
            catch
            {
                //SolNNetPrincipalForm.ActiveForm.Cursor = Cursors.Default;
            }

        }

        private void displayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((mouseClicked) && (drawDisplayPainel_1 != null))
            {
                drawDisplayPainel_1.TranslacaoX = (mouseX - e.X);
                drawDisplayPainel_1.TranslacaoY = (mouseY - e.Y);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                drawDisplayPainel_1.PanelBorder = 8;
                drawDisplayPainel_1.DrawFitView();
            }
            catch
            {
            }

        }

        private void groupBox2_MouseHover(object sender, EventArgs e)
        {
            if (mouseCoord.Visible)
            {
                mouseCoord.Visible = false;
                labelX = -100;
                labelY = -100;
            }
        }

        private void clBoxDist_MouseHover(object sender, EventArgs e)
        {
            if (mouseCoord.Visible)
            {
                mouseCoord.Visible = false;
                labelX = -100;
                labelY = -100;
            }
        }

        private void tabPage3_MouseHover(object sender, EventArgs e)
        {
            if (mouseCoord.Visible)
            {
                mouseCoord.Visible = false;
                labelX = -100;
                labelY = -100;
            }
        }

        private void selectButTrigPts_MouseHover(object sender, EventArgs e)
        {
            if (mouseCoord.Visible)
            {
                mouseCoord.Visible = false;
                labelX = -100;
                labelY = -100;
            }
        }
    }
}
