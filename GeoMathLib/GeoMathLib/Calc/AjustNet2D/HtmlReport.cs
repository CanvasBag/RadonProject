using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;
using BaseCoordinates.Seed;
using MathNet.Numerics.LinearAlgebra;
using System.Xml;
using GeoMathLib;
using AjustLeastSquare;
using BaseCoordinates.Elements;
using BaseCoordinates.Seed;

namespace GeoMathLib.Calc.AjustNet2D
{
    public class HtmlReport
    {
        string projectName = "grupo1";
        string cooUnits = "meters", angUnits = "degree", sisCoo = "none", residuesDirUnit;

        int iterations = 23;
        double varPriori = 1.0, varPosteriori = 2000.654321;
        bool chiSquareTest = true;
        double alpha = 95;
        int df = 10;

        int numberOfPoints = 10, numberOfConstrained = 2, numberOfFixedPoints = 1;

        double DataSnoopingRejectionLevel = 2.8;
        int nDist, nBDist, nDir, nBDir;
        double convertDir, convertResidues;
        List<bool> dataSnoopingTestDir = new List<bool>();
        List<bool> dataSnoopingTestDist = new List<bool>();
        string htmlRed = "";
        string distUnit = "m", dirUnit = "rad";
        List<Double> deslocamentos;
        GeoCoord trigPoints;
        List<NetAdjust2D.ReadStationDist> listDist;
        List<NetAdjust2D.ReadStationDir> listDir;

        /// <summary>
        /// Export report do html
        /// </summary>
        /// <param name="outPutName"></param>
        /// <param name="iterations"></param>
        /// <param name="varPriori"></param>
        /// <param name="varPosteriori"></param>
        /// <param name="chiSquareTest"></param>
        /// <param name="alpha"></param>
        /// <param name="df"></param>
        /// <param name="numberOfPoints"></param>
        /// <param name="numberOfConstrained"></param>
        /// <param name="numberOfFixedPoints"></param>
        /// <param name="DataSnoopingRejectionLevel"></param>
        /// <param name="numDistances"></param>
        /// <param name="numBlunderDistances"></param>
        /// <param name="dataSnoopingTestDist"></param>
        /// <param name="numDirections"></param>
        /// <param name="numBlunderDirections"></param>
        /// <param name="dataSnoopingTestDir"></param>
        /// <param name="distUnit"></param>
        /// <param name="dirUnit"></param>
        /// <param name="convertDirUnit"></param>
        /// <param name="residuesDirUnit"></param>
        /// <param name="convertResDirUnit"></param>
        /// <param name="pointsDisplacements"></param>
        /// <param name="trigPoints"></param>
        /// <param name="listReadDist"></param>
        /// <param name="listReadDir"></param>
        public HtmlReport(string outPutName, int iterations, double varPriori, double varPosteriori, bool chiSquareTest, double alpha, int df, 
                          int numberOfPoints, int numberOfConstrained, int numberOfFixedPoints, double DataSnoopingRejectionLevel, 
                          int numDistances, int numBlunderDistances, List<bool> dataSnoopingTestDist, int numDirections, int numBlunderDirections, 
                          List<bool> dataSnoopingTestDir, String distUnit, String dirUnit, double convertDirUnit, String residuesDirUnit, 
                          double convertResDirUnit, List<double> pointsDisplacements, GeoCoord trigPoints,
                          List<NetAdjust2D.ReadStationDist> listReadDist, List<NetAdjust2D.ReadStationDir> listReadDir)        
        {
            this.iterations = iterations;
            this.varPriori = varPriori;
            this.varPosteriori = varPosteriori;
            this.chiSquareTest = chiSquareTest;
            this.alpha = alpha;
            this.df = df;
            this.numberOfPoints = numberOfPoints;
            this.numberOfConstrained = numberOfConstrained;
            this.numberOfFixedPoints = numberOfFixedPoints;
            this.DataSnoopingRejectionLevel = DataSnoopingRejectionLevel;
            this.nDist = numDistances;
            this.nBDist = numBlunderDistances;
            this.nDir = numDirections;
            this.nBDir = numBlunderDirections;
            this.dataSnoopingTestDist = dataSnoopingTestDist;
            this.dataSnoopingTestDir = dataSnoopingTestDir;
            this.distUnit = distUnit;
            this.dirUnit = dirUnit;
            this.convertDir = convertDirUnit;
            this.residuesDirUnit = residuesDirUnit;
            this.convertResidues = convertResDirUnit;
            this.deslocamentos = pointsDisplacements;
            this.trigPoints = trigPoints;
            this.listDist = listReadDist;
            this.listDir = listReadDir;
            angUnits = dirUnit;
            GenerateReport(outPutName);
        }

        public void GenerateReport(string outPutName)
        {
            StreamWriter reportFile = new StreamWriter(outPutName);
            reportFile.WriteLine("<html>");

            HtmlHead(reportFile);

            reportFile.WriteLine("<body bgcolor= \"FFFFFF\" text=\"#000000\">");
            reportFile.WriteLine("<font face=\"Arial\" size=\"2\">");
            reportFile.WriteLine("<a name=\"Top\"></a>");

            HtmlTopTitle(reportFile);

            reportFile.WriteLine("<br />");

            HtmlInfoReport(reportFile);

            HtmlContents(reportFile);

            HtmlBackToTop(reportFile);

            HtmlStatisticalSummary(reportFile);

            HtmlBackToTop(reportFile);

            HtmlAdjustedCoordinates(reportFile);

            HtmlBackToTop(reportFile);

            HtmlAdjustedObservations(reportFile);

            HtmlBackToTop(reportFile);

            reportFile.WriteLine("<font size = \"-1\"><b>RP SolNNet 0.5 beta</b></font>");
            reportFile.WriteLine("</font>");
            reportFile.WriteLine("</body>");
            reportFile.WriteLine("</html>");
            reportFile.Close();
        }

        private void HtmlHead(StreamWriter reportFile)
        {
            reportFile.WriteLine("<head>");
            reportFile.WriteLine("<title>Network Adjustment Report</title>");
            reportFile.WriteLine("</head>");
        }

        private void HtmlTopTitle(StreamWriter reportFile)
        {
            reportFile.WriteLine("<center>");
            reportFile.WriteLine("<p><font size= \"+3\"><b>Network Adjustment Report</b></font></p>");
            reportFile.WriteLine("<p><font size= \"+1\"><i><b>RP SolNNet 0.5 beta</b></i></font></p>");
            reportFile.WriteLine("</center>");
        }

        private void HtmlInfoReport(StreamWriter reportFile)
        {
            DateTime CurrTime = DateTime.Now;

            reportFile.WriteLine("<table>");
            reportFile.WriteLine("    <tr> <td><b>Project name</b></td><td>:</td><td>" + projectName + "</td> </tr>");
            reportFile.WriteLine("    <tr> <td><b>Date printed</b></td><td>:</td><td>" + CurrTime + "</td> </tr>");
            reportFile.WriteLine("    <tr> <td><b>Coordinate Units</b></td><td>:</td><td>" + distUnit + "</td> </tr>");
            reportFile.WriteLine("    <tr> <td><b>Angular Units</b></td><td>:</td><td>" + dirUnit + "</td> </tr>");
            reportFile.WriteLine("    <tr> <td><b>Coordinate System</b></td><td>:</td><td>" + sisCoo + "</td> </tr>");
            reportFile.WriteLine("</table>");
        }

        private void HtmlContents(StreamWriter reportFile)
        {
            reportFile.WriteLine("<p><font size=\"+1\"><b><a name=\"contents\">Contents</a></b></font></p>");

            reportFile.WriteLine("<dl>");
            reportFile.WriteLine("  <dt><a href=\"#Statistical Summary\"><b>Statistical Summary</b></a></dt>");
            reportFile.WriteLine("     <dd>- <a href=\"#Number of Iterations\"><b>Number of Iterations</b></a></dd>");
            reportFile.WriteLine("     <dd>- <a href=\"#Global Statistics\"><b>Global Statistics</b></a></dd>");
            reportFile.WriteLine("  <dt><a href=\"#Adjusted Coordinates\"><b>Adjusted Coordinates</b></a></dt>");
            reportFile.WriteLine("     <dd>- <a href=\"#Grid Coordinates\"><b>Adjusted Grid Coordinates</b></a></dd>");
            reportFile.WriteLine("	 <dd>- <a href=\"#Coordinate Deltas\"><b>Coordinate Deltas</b></a></dd>");
            reportFile.WriteLine("  <dt><a href=\"#Adjusted Observations\"><b>Adjusted Observations</b></a></dt>");
            reportFile.WriteLine("     <dd>- <a href=\"#Distances\"><b>Distances</b></a></dd>");
            reportFile.WriteLine("	 <dd>- <a href=\"#Directions\"><b>Directions</b></a></dd>");
            reportFile.WriteLine("</dl>");
        }

        private void HtmlBackToTop(StreamWriter reportFile)
        {
            reportFile.WriteLine("<br />");
            reportFile.WriteLine("<a href=\"#Top\"><b>Back to top</b></a>");
            reportFile.WriteLine("<hr />");
            reportFile.WriteLine("<br />");
        }

        private void HtmlStatisticalSummary(StreamWriter reportFile)
        {
            reportFile.WriteLine("<center>");
            reportFile.WriteLine("<a name=\"Statistical Summary\"></a><font size = \"+2\"><b>Statistical Summary</b></font>");
            reportFile.WriteLine("</center>");
            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<font size=\"+1\"><a name=\"Number of Iterations\"></a><b>Number of Iterations</b></font>");
            reportFile.WriteLine("<br />");
            reportFile.WriteLine("<b>Successful Adjustment in {0} iteration(s)</b>", iterations);
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<font size=\"+1\"><a name=\"Global Statistics\"></a><b>Global Statistics</b></font>");
            reportFile.WriteLine("<table>");
            reportFile.WriteLine(" <tr><td><b>Reference Variance (<i>a priori</i>)</b></td><td>:</td><td>{0:0.000}</td></tr>", String.Format("{0,12:0.0000}",varPriori));
            reportFile.WriteLine(" <tr><td><b>Reference Variance (<i>a posteriori</i>)</b></td><td>:</td><td>{0:0.000}</td></tr>", String.Format("{0,12:0.0000}",varPosteriori));
            reportFile.WriteLine(" <tr><td><b>Chi Square Test (<font face=\"symbol\">a</font>={0}%)</b></td> <td>:</td>{1}</tr>", alpha, chiSquareTest ? "<td>PASS</td>" : "<td style=\"color:red;\">REJECT</td>");
            reportFile.WriteLine(" <tr><td><b>Degrees of Freedom </b></td><td>:</td><td>{0}</td></tr>", df);
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");
        }

        private void HtmlAdjustedCoordinates(StreamWriter reportFile)
        {

            reportFile.WriteLine("<center>");
            reportFile.WriteLine("<a name=\"Adjusted Coordinates\"></a><font size = \"+2\"><b>Adjusted Coordinates</b></font>");
            reportFile.WriteLine("</center>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<table>");
            reportFile.WriteLine(" <tr><td><b>Number of Points</b></td><td>:</td><td>{0}</td></tr>", numberOfPoints);
            reportFile.WriteLine(" <tr><td><b>Number of Constrained Points</b></td><td>:</td><td>{0}</td></tr>", numberOfConstrained);
            reportFile.WriteLine(" <tr><td><b>Number of Fixed Points</b></td><td>:</td><td>{0}</td></tr>", numberOfFixedPoints);
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<font size=\"+1\"><a name=\"Grid Coordinates\"></a><b>Adjusted Grid Coordinates</b></font>");
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<table border=\"1\">");
            reportFile.WriteLine("<tr style=\"vertical-align:center;\">");
            reportFile.WriteLine(" <th style=\"text-align:center;\">Point Name</th>");
            reportFile.WriteLine(" <th style=\"text-align:center;\">Easting</th>");
            reportFile.WriteLine(" <th style=\"text-align:center;\">E<br />error</th>");
            reportFile.WriteLine(" <th style=\"text-align:center;\">Northing</th>");
            reportFile.WriteLine(" <th style=\"text-align:center;\">N<br />error</th>");
            reportFile.WriteLine(" <th style=\"text-align:center;\">Fix</th>");
            reportFile.WriteLine("</tr>");
            foreach (EastingNorthing enzTmp in trigPoints.EastingNorthingList)//estaçoes
            {
                reportFile.WriteLine("<tr style=\"vertical-align:center;\">");
                reportFile.WriteLine(" <td style=\"text-align:left;\">{0}</td>", enzTmp.ID);
                reportFile.WriteLine(" <td style=\"text-align:right;\">{0} {1}</td>", String.Format("{0,12:0.0000}",enzTmp.E), distUnit);
                reportFile.WriteLine(" <td style=\"text-align:right;\">{0} {1}</td>", String.Format("{0,12:0.0000}",enzTmp.SigmaE), distUnit);
                reportFile.WriteLine(" <td style=\"text-align:right;\">{0} {1}</td>", String.Format("{0,12:0.0000}",enzTmp.N), distUnit);
                reportFile.WriteLine(" <td style=\"text-align:right;\">{0} {1}</td>", String.Format("{0,12:0.0000}",enzTmp.SigmaN), distUnit);
                reportFile.WriteLine(" <td style=\"text-align:right;\">{0}</td>", enzTmp.ListDadosDivInt[0] < 0?"E N":"");
                reportFile.WriteLine("</tr>");
            }
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<font size=\"+1\"><a name=\"Coordinate Deltas\"></a><b>Coordinate Deltas</b></font>");
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<table border=\"1\">");

            reportFile.WriteLine("<tr style=\"vertical-align:center;\">");
            reportFile.WriteLine(" <th style=\"text-align:center;\">Point Name</th>");
            reportFile.WriteLine(" <th style=\"text-align:center;\"><font face=\"symbol\">D</font>Easting</th>");
            reportFile.WriteLine(" <th style=\"text-align:center;\"><font face=\"symbol\">D</font>Northing</th>");
            reportFile.WriteLine("</tr>");

            int j = 0;
            for (int i = 0; i < deslocamentos.Count; i++) //deslocamentos
            {
                reportFile.WriteLine("<tr style=\"vertical-align:center;\">");
                reportFile.WriteLine(" <td style=\"text-align:left;\">{0}</td>", trigPoints.EastingNorthingList[j++].ID);
                reportFile.WriteLine(" <td style=\"text-align:right;\">{0} {1}</td>", String.Format("{0,12:0.0000}",deslocamentos[i]), distUnit);
                reportFile.WriteLine(" <td style=\"text-align:right;\">{0} {1}</td>", String.Format("{0,12:0.0000}",deslocamentos[++i]), distUnit);
                reportFile.WriteLine("</tr>");
            }
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");
        }

        private void HtmlAdjustedObservations(StreamWriter reportFile)
        {
            reportFile.WriteLine("<center>");
            reportFile.WriteLine("<a name=\"Adjusted Observations\"></a><font size = \"+2\"><b>Adjusted Observations</b></font>");
            reportFile.WriteLine("</center>");

            

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<a name=\"Directions\"></a><font size=\"+1\"><b>Directions</b></font>");
            reportFile.WriteLine("</p>");
            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<table>");
            reportFile.WriteLine(" <tr><td><b>Number of Observations</b></td> <td>:</td><td>{0}</td></tr>", nDir);
            reportFile.WriteLine(" <tr><td><b>Number of Blunders</b></td>  <td>:</td><td>{0}</td></tr>", nBDir);
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<b>Observation Adjustment (Data Snooping Rejection Level = {0:0.00}).&nbsp; Any blunders are in <font color=\"#ff0000\">red</font>.</b>", DataSnoopingRejectionLevel);
            reportFile.WriteLine("</p>");
            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<table border=\"1\"> ");
            reportFile.WriteLine("<tr style=\"vertical-align:center;\">");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Observation ID</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">From Point</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">To Point</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Direction</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Error</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Residual</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Local Redundancy</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Std. Residual</td>");
            reportFile.WriteLine("</tr>");

            int j = 0;
            foreach (NetAdjust2D.ReadStationDir dirTmp in listDir) //todas as observacoes de direccao
            {
                htmlRed = dataSnoopingTestDir[j++] ? "" : " color:red;";

                reportFile.WriteLine("<tr style=\"vertical-align:center;" + htmlRed + "\">");
                reportFile.WriteLine(" <td style=\"text-align:left;" + htmlRed + "\">{0}</td>", dirTmp.id);
                reportFile.WriteLine(" <td style=\"text-align:left;" + htmlRed + "\">{0}</td>", dirTmp.occupied.ID);
                reportFile.WriteLine(" <td style=\"text-align:left;" + htmlRed + "\">{0}</td>", dirTmp.observed.ID);
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0} " + dirUnit + "</td>", String.Format("{0,12:0.0000}",dirTmp.direction * convertDir));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0} " + residuesDirUnit + "</td>", String.Format("{0,12:0.0000}", dirTmp.bias * convertResidues));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0} " + residuesDirUnit + "</td>", String.Format("{0,12:0.0000}", dirTmp.residue * convertResidues));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0}</td>", String.Format("{0,12:0.0000}", dirTmp.localRedundancy));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0}</td>", String.Format("{0,12:0.0000}",dirTmp.stdResidue));
                reportFile.WriteLine("</tr>");
            }
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<a name=\"Distances\"></a><font size=\"+1\"><b>Distances</b></font>");
            reportFile.WriteLine("</p>");
            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<table>");
            reportFile.WriteLine(" <tr><td><b>Number of Observations</b></td> <td>:</td><td>{0}</td></tr>", nDist);
            reportFile.WriteLine(" <tr><td><b>Number of Blunders</b></td>  <td>:</td><td>{0}</td></tr>", nBDist);
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");

            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<b>Observation Adjustment (Data Snooping Rejection Level = {0:0.00}).&nbsp; Any blunders are in <font color=\"#ff0000\">red</font>.</b>", DataSnoopingRejectionLevel);
            reportFile.WriteLine("</p>");
            reportFile.WriteLine("<p>");
            reportFile.WriteLine("<table border=\"1\"> ");
            reportFile.WriteLine("<tr style=\"vertical-align:center;\">");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Observation ID</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">From Point</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">To Point</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Distance</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Error</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Residual</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Local Redundancy</td>");
            reportFile.WriteLine(" <td style=\"text-align:center;\">Std. Residual</td>");
            reportFile.WriteLine("</tr>");
            j = 0;
            foreach (NetAdjust2D.ReadStationDist distTmp in listDist) //todas as observacoes de distancia
            {
                htmlRed = dataSnoopingTestDist[j++] ? "" : " color:red;";

                reportFile.WriteLine("<tr style=\"vertical-align:center;" + htmlRed + "\">");
                reportFile.WriteLine(" <td style=\"text-align:left;" + htmlRed + "\">{0}</td>", distTmp.id);
                reportFile.WriteLine(" <td style=\"text-align:left;" + htmlRed + "\">{0}</td>", distTmp.occupied.ID);
                reportFile.WriteLine(" <td style=\"text-align:left;" + htmlRed + "\">{0}</td>", distTmp.observed.ID);
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0} " + distUnit + "</td>", String.Format("{0,12:0.0000}",distTmp.distance));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0} " + distUnit + "</td>", String.Format("{0,12:0.0000}",distTmp.bias));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0} " + distUnit + "</td>", String.Format("{0,12:0.0000}",distTmp.residue));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0}</td>", String.Format("{0,12:0.0000}", distTmp.localRedundancy));
                reportFile.WriteLine(" <td style=\"text-align:right;" + htmlRed + "\">{0} " + distUnit + "</td>", String.Format("{0,12:0.0000}",distTmp.stdResidue));
                reportFile.WriteLine("</tr>");
            }
            reportFile.WriteLine("</table>");
            reportFile.WriteLine("</p>");
        }

    }
}
