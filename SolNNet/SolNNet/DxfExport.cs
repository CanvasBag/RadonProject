using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;
using BaseCoordinates.Seed;
using GeoMathLib.Calc;
using GeoMathLib.Calc.AjustNet2D;
using AjustLeastSquare.Statistics;
using DXFLibrary;


namespace SolNNet
{
    public class DxfExport
    {
        DXFLibrary.Document doc;

        private CheckedListBox clBoxTrig, clBoxControl, clBoxFix, clBoxDist, clBoxDir;
        private List<Ellipse> errorEllipseList, confidanceEllipseList, relativeEllipseList;
        private List<double> displacements;
        double ellipseScale, deltaScale;
        double minN, minE, maxSu, maxSv, maxDeltaX;

        public DxfExport(string dxfFileName, List<Tuple<bool, string, short>> layersAndColors, List<CheckedListBox> clBoxList,
                        List<double> displacements, List<Ellipse> errorEllipseList, List<Ellipse> confidanceEllipseList,
                        List<Ellipse> relativeEllipseList, double ellipseScale, double deltaScale)
        {
            clBoxTrig = clBoxList[0];
            clBoxControl = clBoxList[1];
            clBoxFix = clBoxList[2];
            clBoxDir = clBoxList[3];
            clBoxDist = clBoxList[4];
            this.errorEllipseList = errorEllipseList;
            this.confidanceEllipseList = confidanceEllipseList;
            this.relativeEllipseList = relativeEllipseList;
            this.displacements = displacements;
            this.ellipseScale = ellipseScale;
            this.deltaScale = deltaScale;
            doc = new DXFLibrary.Document();

            DXFLibrary.Tables tables = new DXFLibrary.Tables();
            doc.SetTables(tables);

            DxfLayers(tables, layersAndColors);

            //trig points
            if (layersAndColors[0].Item1)
            {
                DrawPoints(doc, layersAndColors[0].Item2, clBoxTrig);
            }
            //control points
            if (layersAndColors[1].Item1)
            {
                DrawPoints(doc, layersAndColors[1].Item2, clBoxControl);
            }
            //fixed points
            if (layersAndColors[2].Item1)
            {
                DrawPoints(doc, layersAndColors[2].Item2, clBoxFix);
            }
            //directions
            if (layersAndColors[3].Item1)
            {
                DrawDirections(doc, layersAndColors[3].Item2);
            }
            //distances
            if (layersAndColors[4].Item1)
            {
                DrawDistances(doc, layersAndColors[4].Item2);
            }
            //azimuths
            if (layersAndColors[5].Item1)
            {

            }
            //delta
            if (layersAndColors[6].Item1)
            {
                DrawDeltas(doc, layersAndColors[6].Item2, clBoxTrig, displacements, deltaScale);
            }
            //error ellipses
            if (layersAndColors[7].Item1)
            {
                foreach (Ellipse ellipseTmp in errorEllipseList)
                {
                    DxfEllipse(doc, layersAndColors[7].Item2, ellipseScale, ellipseTmp.Point.E, ellipseTmp.Point.N, ellipseTmp.T,
                                ellipseTmp.Su, ellipseTmp.Sv);
                }
                DrawEscala(doc, layersAndColors[7].Item2, ellipseScale);
            }
            //confidence ellipses
            if (layersAndColors[8].Item1)
            {
                foreach (Ellipse ellipseTmp in confidanceEllipseList)
                {
                    DxfEllipse(doc, layersAndColors[8].Item2, ellipseScale, ellipseTmp.Point.E, ellipseTmp.Point.N, ellipseTmp.T,
                                ellipseTmp.SuC, ellipseTmp.SvC);
                }
            }
            //error ellipses
            if (layersAndColors[9].Item1)
            {

            }

            //cria e feixa o ficheiro dxf

            FileStream f = new FileStream(dxfFileName, System.IO.FileMode.Create);
            DXFLibrary.Writer.Write(doc, f);
            f.Close();

        }


        //cria os layers necessários
        private void DxfLayers(DXFLibrary.Tables tables, List<Tuple<bool, string, short>> layersAndColors)
        {
            //* * * e necessario fazer um remove antes de criar uma nova
            DXFLibrary.Table layers = new DXFLibrary.Table("LAYER");
            tables.addTable(layers);

            foreach (Tuple<bool, string, short> layerAndColor in layersAndColors)
            {
                if (layerAndColor.Item1)
                {
                    DXFLibrary.Layer layer = new DXFLibrary.Layer(layerAndColor.Item2, layerAndColor.Item3, "CONTINUOUS");
                    layers.AddTableEntry(layer);
                }
            }
        }

        //gera uma elipses
        private void DxfEllipse(DXFLibrary.Document doc, string layer, double esc, double x0, double y0, double teta, double su, double sv)
        {
            double c1 = Math.Cos(teta - Math.PI / 2);
            double s1 = Math.Sin(teta - Math.PI / 2);
            double c2 = Math.Cos(teta);
            double s2 = Math.Sin(teta);
            DXFLibrary.Line line;
            double x1, y1, x2, y2, x, y;

            x1 = esc * (su * c1 * Math.Cos(0.0) + sv * s1 * Math.Sin(0.0)) + x0;
            y1 = esc * (su * c2 * Math.Cos(0.0) + sv * s2 * Math.Sin(0.0)) + y0;

            double plus = 5 * Math.PI / 180;
            double af = plus;
            for (int i = 0; i < 72; i++)
            {
                x2 = esc * (su * c1 * Math.Cos(af) + sv * s1 * Math.Sin(af)) + x0;
                y2 = esc * (su * c2 * Math.Cos(af) + sv * s2 * Math.Sin(af)) + y0;

                DXFLibrary.Line lineElip = new DXFLibrary.Line(layer, x1, y1, x2, y2);
                doc.add(lineElip);

                x1 = x2;
                y1 = y2;

                af += plus;

                if (i == 71) plus = 2 * Math.PI;
            }           
        }

        private void DrawEscala(DXFLibrary.Document doc, string layer, double esc)
        {
            
            DXFLibrary.Line line;
            double x1, y1, x2, y2, x, y;
            //Escala elipses
            x = MinE;
            y = minN - MaxSv * esc * 2;
            x2 = MinE + MaxSu * esc;
            line = new DXFLibrary.Line(layer, x, y, x2, y);
            doc.add(line);
            DXFLibrary.Text texto = new Text(String.Format("{0,5:0.00} ", MaxSu * 100) + "cm", x, y - MaxSu * esc / 5, 2, layer);
            doc.add(texto);
            x = MinE;
            y = minN - MaxSv * esc * 2 - MaxSu * esc / 10;
            y2 = minN - MaxSv * esc * 2 + MaxSu * esc / 10;
            line = new DXFLibrary.Line(layer, x, y, x, y2);
            doc.add(line);
            x = MinE + MaxSu * esc;
            y = minN - MaxSv * esc * 2 - MaxSu * esc / 20;
            y2 = minN - MaxSv * esc * 2 + MaxSu * esc / 20;
            line = new DXFLibrary.Line(layer, x, y, x, y2);
            doc.add(line);
        }
        private void DrawPoints(DXFLibrary.Document doc, string layer, CheckedListBox points)
        {
            foreach (EastingNorthing point in points.CheckedItems)
            {
                double x, y;
                x = point.E;
                y = point.N;

                DXFLibrary.Point pointDxf = new DXFLibrary.Point(layer, x, y);

                doc.add(pointDxf);
            }
        }

        private void DrawDistances(DXFLibrary.Document doc, string layer)
        {
            foreach (NetAdjust2D.ReadStationDist readStDist in clBoxDist.CheckedItems)
            {
                double xi, yi, xf, yf;
                xi = readStDist.occupied.E;
                yi = readStDist.occupied.N;
                xf = readStDist.observed.E;
                yf = readStDist.observed.N;

                DXFLibrary.Line line = new DXFLibrary.Line(layer, xi, yi, xf, yf);
                doc.add(line);
            }
        }

        private void DrawDirections(DXFLibrary.Document doc, string layer)
        {
            foreach (NetAdjust2D.ReadStationDir readStDir in clBoxDir.CheckedItems)
            {
                double xi, yi, xf, yf;
                xi = readStDir.occupied.E;
                yi = readStDir.occupied.N;
                xf = readStDir.observed.E;
                yf = readStDir.observed.N;

                DXFLibrary.Line line = new DXFLibrary.Line(layer, xi, yi, xf, yf);
                doc.add(line);
            }
        }

        private void DrawDeltas(DXFLibrary.Document doc, string layer, CheckedListBox points, List<double> displacments, double escala)
        {
            int i = 0;
            double x, y;
            double x2, y2;
            double displacementX;
            double displacementY;
            DXFLibrary.Line line;
            foreach (EastingNorthing point in points.CheckedItems)
            {                
                displacementX = displacements[i++];
                displacementY = displacements[i++];
                x = point.E + displacementX * escala;
                y = point.N + displacementY * escala;

                line = new DXFLibrary.Line(layer, point.E, point.N, x, y);
                doc.add(line);

                //seta

                double rumoDeslocamento = Math.Atan2(displacementX, displacementY);
                rumoDeslocamento = rumoDeslocamento < 0 ? rumoDeslocamento + Math.PI * 2 : rumoDeslocamento;
                double rumoGraus = rumoDeslocamento * 180 / Math.PI;
                double Rumo1 = Math.PI + rumoDeslocamento - Math.PI / 6;
                double rumoGraus2 = Rumo1 * 180 / Math.PI;
                double Rumo2 = Math.PI + rumoDeslocamento + Math.PI / 6;
                double rumoGraus3 = Rumo2 * 180 / Math.PI;
                double moduloDesl = Math.Sqrt(displacementX * displacementX + displacementY * displacementY);

                x2 = x + (moduloDesl * escala / 10) * Math.Sin(Rumo1);
                y2 = y + (moduloDesl * escala / 10) * Math.Cos(Rumo1);
                line = new DXFLibrary.Line(layer, x, y, x2, y2);
                doc.add(line);

                x2 = x + (moduloDesl * escala / 10) * Math.Sin(Rumo2);
                y2 = y + (moduloDesl * escala / 10) * Math.Cos(Rumo2);
                line = new DXFLibrary.Line(layer, x, y, x2, y2);
                doc.add(line);
            }           
        }

        public double MaxSu
        {
            get
            {
                defineMaxSu();
                return maxSu;
            }
            set { maxSu = value; }
        }

        private void defineMaxSu()
        {
            double su = double.MinValue;
            foreach (Ellipse elipsTmp in errorEllipseList)
            {
                su = elipsTmp.Su > su ? elipsTmp.Su : su;
            }
            MaxSu = su;
        }

        public double MaxSv
        {
            get
            {
                defineMaxSv();
                return maxSv;
            }
            set { maxSv = value; }
        }

        private void defineMaxSv()
        {
            double sv = double.MinValue;
            foreach (Ellipse elipsTmp in errorEllipseList)
            {
                sv = elipsTmp.Su > sv ? elipsTmp.Su : sv;
            }
            MaxSv = sv;
        }

        public double MinE
        {
            get
            {
                defineMinE();
                return minE;
            }
            set { minE = value; }
        }

        private void defineMinE()
        {
            double min = double.MaxValue;
            foreach (EastingNorthing enzTmp in clBoxTrig.Items)
            {
                min = enzTmp.E < min ? enzTmp.E : min;
            }
            foreach (EastingNorthing enzTmp in clBoxControl.Items)
            {
                min = enzTmp.E < min ? enzTmp.E : min;
            }
            foreach (EastingNorthing enzTmp in clBoxFix.Items)
            {
                min = enzTmp.E < min ? enzTmp.E : min;
            }
            MinE = min;
        }

        public double MinN
        {
            get
            {
                defineMinN();
                return minN;
            }
            set { minN = value; }
        }

        private void defineMinN()
        {
            double min = double.MaxValue;
            foreach (EastingNorthing enzTmp in clBoxTrig.Items)
            {
                min = enzTmp.N < min ? enzTmp.N : min;
            }
            foreach (EastingNorthing enzTmp in clBoxControl.Items)
            {
                min = enzTmp.N < min ? enzTmp.N : min;
            }
            foreach (EastingNorthing enzTmp in clBoxFix.Items)
            {
                min = enzTmp.N < min ? enzTmp.N : min;
            }
            MinN = min;
        }
    }
}

