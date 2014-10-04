using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;
using BaseCoordinates.Seed;
using System.Drawing.Drawing2D;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;
using GeoMathLib.Calc;
using GeoMathLib.Calc.AjustNet2D;
using AjustLeastSquare.Statistics;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace SolNNet
{
    public class Drawing
    {
        private CheckedListBox clBoxTrig, clBoxControl, clBoxFix, clBoxDist, clBoxDir;
        private Label eixoX1, eixoX2, eixoX3, eixoY1, eixoY2, eixoY3, eixoY4, escalaElipseLabel;
        private Panel displayPanel;
        Double scale, scaleFitView;
        Double bX, bY, aX, aY; //P2 = a.P1 + b
        private double maxE;
        private double maxN;
        private double minE;
        private double minN;
        private double zeroPanelX = 0, zeroPanelY = 0, lastZeroPanelX = 0, lastZeroPanelY = 0;
        private double border = 0, translacaoX = 0, translacaoY = 0;
        private double viewWidth, viewHeight;
        private Graphics graphic;
        private Boolean distCheck = true, dirCheck = true, azCheck = true, arrowOn = true;
        private int elipseOn;
        private int gridOn;
        private List<Ellipse> errorElipses, confidenceElipses;
        List<double> deslocamentos;
        private double escalaElipsesMM = 0;
        


        public Drawing(CheckedListBox clBoxTrigIn, CheckedListBox clBoxControlIn, CheckedListBox clBoxFixeIn, CheckedListBox clBoxDistIn, CheckedListBox clBoxDirIn, Panel displayPanelIn,
                       Label eixoYY1, Label eixoYY2, Label eixoYY3, Label eixoYY4, Label eixoX1, Label eixoX2, Label eixoX3, Label escalaElipseLabel, double ellipseScale)
        {
            clBoxTrig = clBoxTrigIn;
            clBoxControl = clBoxControlIn;
            clBoxFix = clBoxFixeIn;
            clBoxDist = clBoxDistIn;
            clBoxDir = clBoxDirIn;
            displayPanel = displayPanelIn;
            graphic = displayPanel.CreateGraphics();
            ViewWidth = DisplayPanelWidth;
            ViewHeight = DisplayPanelHeight;
            ScaleView = ScaleFitView;
            this.eixoX1 = eixoX1;
            this.eixoX2 = eixoX2;
            this.eixoX3 = eixoX3;
            this.eixoY1 = eixoYY1;
            this.eixoY2 = eixoYY2;
            this.eixoY3 = eixoYY3;
            this.eixoY4 = eixoYY4;
            this.escalaElipseLabel = escalaElipseLabel;
            EscalaElipsesMM = ellipseScale;
            gridOn = 0;
            elipseOn = 1;
            errorElipses = new List<Ellipse>();
            confidenceElipses = new List<Ellipse>();
            PanelBorder = 8;
            DrawFitView();
        }

        public void DrawFitView()
        {
            graphic.Clear(Color.White);
            ViewWidth = DisplayPanelWidth;
            ViewHeight = DisplayPanelHeight;
            ScaleView = ScaleFitView;
            LastZeroPanelX = 0;
            LastZeroPanelY = 0;
            TranslacaoX = 0;
            TranslacaoY = 0;
            ZeroPanelX = 0;
            ZeroPanelY = 0;

            //BX = ViewWidth - PanelBorder - zeroPanelX + ScaleView * MinE;
            //BY = ViewHeight - PanelBorder - zeroPanelY + ScaleView * MinN;
            BX = PanelBorder - zeroPanelX - ScaleView * MinE;
            BY = ViewHeight - PanelBorder - zeroPanelY + ScaleView * MinN;
            AX = ScaleView;
            AY = -ScaleView;

            updateAxis();
            if (gridOn > 0) drawAxisLines();
            drawTrigPts();
            drawControlPts();
            drawFixedPts();
            if (DistCheck) drawDistancias();
            if (DirCheck) drawDireccoes();
            if (elipseOn > 0) drawElipses();
            if (arrowOn) drawDeslocamentos();
        }

        public void draw()
        {
            graphic.Clear(Color.White);
            updateAxis();
            if (gridOn > 0) drawAxisLines();
            drawTrigPts();
            drawControlPts();
            drawFixedPts();
            if (DistCheck) drawDistancias();
            if (DirCheck) drawDireccoes();
            if (elipseOn > 0) drawElipses();
            if (arrowOn) drawDeslocamentos();
        }

        public void zoom(Double scaleIn)
        {
            graphic.Clear(Color.White);
            EastingNorthing centroPainel = new EastingNorthing(DisplayPanelWidth / 2, DisplayPanelHeight / 2, 0);

            BX = centroPainel.E - scaleIn * (centroPainel.E - BX);
            BY = centroPainel.N - scaleIn * (centroPainel.N - BY);
            AX = AX * scaleIn;
            AY = AY * scaleIn;

            updateAxis();
            if (gridOn > 0) drawAxisLines();
            drawTrigPts();
            drawControlPts();
            drawFixedPts();
            if (DistCheck) drawDistancias();
            if (DirCheck) drawDireccoes();
            if (elipseOn > 0) drawElipses();
            if (arrowOn) drawDeslocamentos();
        }

        public void Translade()
        {
            graphic.Clear(Color.White);
            BX = BX + (LastZeroPanelX - ZeroPanelX);
            BY = BY + (LastZeroPanelY - ZeroPanelY);
            LastZeroPanelX = ZeroPanelX;
            LastZeroPanelY = ZeroPanelY;

            updateAxis();
            if (gridOn > 0) drawAxisLines();
            drawTrigPts();
            drawControlPts();
            drawFixedPts();
            if (DistCheck) drawDistancias();
            if (DirCheck) drawDireccoes();
            if (elipseOn > 0) drawElipses();
            if (arrowOn) drawDeslocamentos();
        }



        #region Auxiliars Methods

        public void drawDeslocamentos()
        {
            int i = 0;
            double escala = EscalaElipsesMM;
            try
            {
                foreach (EastingNorthing enzTmp in clBoxTrig.CheckedItems)
                {
                    EastingNorthing enzTmp2 = new EastingNorthing(enzTmp.E + Deslocamentos[i++] * escala, enzTmp.N + Deslocamentos[i++] * escala, 0);
                    EastingNorthing coordTmp1 = new EastingNorthing();
                    EastingNorthing coordTmp2 = new EastingNorthing();
                    coordTmp1 = calcTransfCoord(enzTmp, ScaleView);
                    coordTmp2 = calcTransfCoord(enzTmp2, ScaleView);


                    GraphicsPath hPath = new GraphicsPath();

                    // Criar um modelo de seta
                    hPath.AddLine(new Point(2, -3), new Point(0, 0));
                    hPath.AddLine(new Point(0, 0), new Point(-2, -3));

                    // Adicionar o modelo de seta ao objecto HookCap
                    CustomLineCap HookCap = new CustomLineCap(null, hPath);

                    // Set the start cap and end cap of the HookCap to be rounded.
                    HookCap.SetStrokeCaps(LineCap.Round, LineCap.Round);

                    Pen p2 = new Pen(Color.Black, 1F);
                    p2.CustomEndCap = HookCap;
                    p2.StartCap = LineCap.Round;
                    p2.DashStyle = DashStyle.Solid;

                    Point partida = new Point((int)coordTmp1.E, (int)coordTmp1.N);
                    Point chegada = new Point((int)coordTmp2.E, (int)coordTmp2.N);
                    List<Point> desfasamento = desfasamentoPos(partida, chegada, 5);//pto partida, chegada e módulo do desfasamento

                    graphic.DrawLine(p2, desfasamento[0], desfasamento[1]);
                }

                foreach (EastingNorthing enzTmp in clBoxControl.CheckedItems)
                {
                    EastingNorthing enzTmp2 = new EastingNorthing(enzTmp.E + Deslocamentos[i++] * escala, enzTmp.N + Deslocamentos[i++] * escala, 0);
                    EastingNorthing coordTmp1 = new EastingNorthing();
                    EastingNorthing coordTmp2 = new EastingNorthing();
                    coordTmp1 = calcTransfCoord(enzTmp, ScaleView);
                    coordTmp2 = calcTransfCoord(enzTmp2, ScaleView);


                    GraphicsPath hPath = new GraphicsPath();

                    // Criar um modelo de seta
                    hPath.AddLine(new Point(2, -3), new Point(0, 0));
                    hPath.AddLine(new Point(0, 0), new Point(-2, -3));

                    // Adicionar o modelo de seta ao objecto HookCap
                    CustomLineCap HookCap = new CustomLineCap(null, hPath);

                    // Set the start cap and end cap of the HookCap to be rounded.
                    HookCap.SetStrokeCaps(LineCap.Round, LineCap.Round);

                    Pen p2 = new Pen(Color.Black, 1F);
                    p2.CustomEndCap = HookCap;
                    p2.StartCap = LineCap.Round;
                    p2.DashStyle = DashStyle.Solid;

                    Point partida = new Point((int)coordTmp1.E, (int)coordTmp1.N);
                    Point chegada = new Point((int)coordTmp2.E, (int)coordTmp2.N);
                    List<Point> desfasamento = desfasamentoPos(partida, chegada, 5);//pto partida, chegada e módulo do desfasamento

                    graphic.DrawLine(p2, desfasamento[0], desfasamento[1]);
                }
            }
            catch
            {
            }
        }

        public void drawElipses()
        {
            if (elipseOn == 1)
            {
                double escala = EscalaElipsesMM;
                //EscalaElipsesMM = (transfInversa(40) - transfInversa(0)) * 1000;
                //escalaElipseLabel.Text = string.Format("{0,3:0.0} mm", EscalaElipsesMM / escala);
                escalaElipseLabel.Text = string.Format("{0,3:0.0} mm", (transfInversa(40) - transfInversa(0)) * 1000 / EscalaElipsesMM);
                for (int i = 0; i < ErrorElipses.Count; i++)
                {
                    List<List<Point>> elipses = calcElipses(escala, ErrorElipses[i].Point.E, ErrorElipses[i].Point.N, ErrorElipses[i].T, ErrorElipses[i].Su, ErrorElipses[i].Sv);
                    foreach (List<Point> ptosTmp in elipses)
                    {
                        graphic.DrawLine(Pens.Red, ptosTmp[0], ptosTmp[1]);
                    }
                }
            }
            else
            {
                double escala = EscalaElipsesMM;
                //EscalaElipsesMM = (transfInversa(40) - transfInversa(0)) * 1000;
                escalaElipseLabel.Text = string.Format("{0,4:0.0} mm", (transfInversa(40) - transfInversa(0)) * 1000 / EscalaElipsesMM);
                for (int i = 0; i < ErrorElipses.Count; i++)
                {
                    List<List<Point>> elipses = calcElipses(escala, ConfidenceElipses[i].Point.E, ConfidenceElipses[i].Point.N, ConfidenceElipses[i].T, ConfidenceElipses[i].SuC, ConfidenceElipses[i].SvC);
                    foreach (List<Point> ptosTmp in elipses)
                    {
                        graphic.DrawLine(Pens.Red, ptosTmp[0], ptosTmp[1]);
                    }
                }
            }

        }

        private List<List<Point>> calcElipses(double esc, double x0, double y0, double teta, double su, double sv)
        {
            //double c1 = Math.Cos(teta);
            //double s1 = Math.Sin(teta);
            double c1 = Math.Cos(teta - Math.PI / 2);
            double s1 = Math.Sin(teta - Math.PI / 2);

            //double c2 = Math.Cos(teta + Math.PI / 2);
            //double s2 = Math.Sin(teta + Math.PI / 2);
            double c2 = Math.Cos(teta);
            double s2 = Math.Sin(teta);
            List<List<Point>> elipse = new List<List<Point>>();
            double x1, y1, x2, y2;
            EastingNorthing enzTmp1, enzTmp2;
            EastingNorthing enzTmpDrawCoord1, enzTmpDrawCoord2;
            double plus = 5 * Math.PI / 180;
            double af = plus;

            enzTmp1 = new EastingNorthing();
            enzTmpDrawCoord1 = new EastingNorthing();
            enzTmp1.E = esc * (su * c1 * Math.Cos(0.0) + sv * s1 * Math.Sin(0.0)) + x0;
            enzTmp1.N = esc * (su * c2 * Math.Cos(0.0) + sv * s2 * Math.Sin(0.0)) + y0;
            enzTmpDrawCoord1 = calcTransfCoord(enzTmp1, ScaleView);

            for (int i = 0; i < 72; i++)
            {
                enzTmp2 = new EastingNorthing();
                enzTmpDrawCoord2 = new EastingNorthing();
                enzTmp2.E = esc * (su * c1 * Math.Cos(af) + sv * s1 * Math.Sin(af)) + x0;
                enzTmp2.N = esc * (su * c2 * Math.Cos(af) + sv * s2 * Math.Sin(af)) + y0;
                enzTmpDrawCoord2 = calcTransfCoord(enzTmp2, ScaleView);

                Point pt1 = new Point(Convert.ToInt32(enzTmpDrawCoord1.E), Convert.ToInt32(enzTmpDrawCoord1.N));
                Point pt2 = new Point(Convert.ToInt32(enzTmpDrawCoord2.E), Convert.ToInt32(enzTmpDrawCoord2.N));

                List<Point> ptsTmp = new List<Point>();
                ptsTmp.Add(pt1);
                ptsTmp.Add(pt2);

                elipse.Add(ptsTmp);
                //DXFLibrary.Line lineElip = new DXFLibrary.Line("Ellipses", x1, y1, x2, y2);
                //doc.add(lineElip);
                enzTmpDrawCoord1 = enzTmpDrawCoord2;

                af += plus;

                if (i == 71) plus = 2 * Math.PI;
            }
            return elipse;
        }

        private void updateAxis()
        {
            eixoY1.Text = String.Format("{0,5:00.0}", (DisplayPanelHeight - BY) / AY);
            eixoY2.Text = String.Format("{0,5:00.0}", (DisplayPanelHeight - DisplayPanelWidth / 3 - BY) / AY);
            eixoY3.Text = String.Format("{0,5:00.0}", (DisplayPanelHeight - 2 * DisplayPanelWidth / 3 - BY) / AY);
            eixoY4.Text = String.Format("{0,5:00.0}", (DisplayPanelHeight - DisplayPanelWidth - BY) / AY);

            eixoX1.Text = String.Format("{0,5:00.0}", (-BX) / AX);
            eixoX2.Text = String.Format("{0,5:00.0}", (DisplayPanelWidth / 3 - BX) / AX);
            eixoX3.Text = String.Format("{0,5:00.0}", (2 * DisplayPanelWidth / 3 - BX) / AX);
        }

        private void drawAxisLines()
        {

            Pen pen = new Pen(Color.Gray, 0.5F);
            Pen pen2 = new Pen(Color.Gray, 0.5F);
            pen.DashStyle = DashStyle.Dot;
            pen2.DashStyle = DashStyle.Dot;
            int count = gridOn * 6 - 3;
            for (int i = 0; i < DisplayPanelWidth; i += 19)
            {
                for (int j = 1; j < count + 3; j++)
                {
                    graphic.DrawLine(pen, new Point(i, (int)DisplayPanelHeight - j * (int)DisplayPanelWidth / count), new Point(i + 5, (int)DisplayPanelHeight - j * (int)DisplayPanelWidth / count));
                    graphic.DrawLine(pen2, new Point(i + 12, (int)DisplayPanelHeight - j * (int)DisplayPanelWidth / count), new Point(i + 13, (int)DisplayPanelHeight - j * (int)DisplayPanelWidth / count));
                }

            }
            for (int i = 0; i < DisplayPanelHeight; i += 19)
            {
                for (int j = 1; j < count; j++)
                {
                    graphic.DrawLine(pen, new Point(j * (int)DisplayPanelWidth / count, i), new Point(j * (int)DisplayPanelWidth / count, i + 5));
                    graphic.DrawLine(pen2, new Point(j * (int)DisplayPanelWidth / count, i + 12), new Point(j * (int)DisplayPanelWidth / count, i + 13));
                }
            }

        }

        private void drawDireccoes()
        {
            foreach (NetAdjust2D.ReadStationDir readStDir in clBoxDir.CheckedItems)
            {
                EastingNorthing enzTmp = readStDir.occupied;
                EastingNorthing enzTmp2 = readStDir.observed;
                //transformação entre referenciais
                EastingNorthing coordTmp1 = new EastingNorthing();
                EastingNorthing coordTmp2 = new EastingNorthing();
                coordTmp1 = calcTransfCoord(enzTmp, ScaleView);
                coordTmp2 = calcTransfCoord(enzTmp2, ScaleView);


                GraphicsPath hPath = new GraphicsPath();

                // Create the outline for our custom end cap.
                hPath.AddLine(new Point(-4, -28), new Point(4, -28));
                hPath.AddLine(new Point(4, -28), new Point(0, -23));
                hPath.AddLine(new Point(0, -23), new Point(-4, -28));

                // Construct the hook-shaped end cap.
                CustomLineCap HookCap = new CustomLineCap(null, hPath);

                // Set the start cap and end cap of the HookCap to be rounded.
                HookCap.SetStrokeCaps(LineCap.Round, LineCap.Round);

                Pen p2 = new Pen(Color.YellowGreen, 1F);
                p2.CustomEndCap = HookCap;
                p2.StartCap = LineCap.Round;
                p2.DashStyle = DashStyle.Dot;

                Point partida = new Point((int)coordTmp1.E, (int)coordTmp1.N);
                Point chegada = new Point((int)coordTmp2.E, (int)coordTmp2.N);
                List<Point> desfasamento = desfasamentoPos(partida, chegada, 8);//pto partida, chegada e módulo do desfasamento

                graphic.DrawLine(p2, desfasamento[0], desfasamento[1]);
            }
        }

        private void drawDistancias()
        {
            foreach (NetAdjust2D.ReadStationDist readStDist in clBoxDist.CheckedItems)
            {
                EastingNorthing enzTmp = readStDist.occupied;
                EastingNorthing enzTmp2 = readStDist.observed;
                //transformação entre referenciais
                EastingNorthing coordTmp1 = new EastingNorthing();
                EastingNorthing coordTmp2 = new EastingNorthing();
                coordTmp1 = calcTransfCoord(enzTmp, ScaleView);
                coordTmp2 = calcTransfCoord(enzTmp2, ScaleView);


                GraphicsPath hPath = new GraphicsPath();

                // Criar um modelo de seta
                hPath.AddLine(new Point(-4, -8), new Point(4, -8));
                hPath.AddLine(new Point(4, -8), new Point(0, -3));
                hPath.AddLine(new Point(0, -3), new Point(-4, -8));

                // Adicionar o modelo de seta ao objecto HookCap
                CustomLineCap HookCap = new CustomLineCap(null, hPath);

                // Set the start cap and end cap of the HookCap to be rounded.
                HookCap.SetStrokeCaps(LineCap.Round, LineCap.Round);

                Pen p2 = new Pen(Color.Gold, 1F);
                p2.CustomEndCap = HookCap;
                p2.StartCap = LineCap.Round;
                p2.DashStyle = DashStyle.Dot;

                Point partida = new Point((int)coordTmp1.E, (int)coordTmp1.N);
                Point chegada = new Point((int)coordTmp2.E, (int)coordTmp2.N);
                List<Point> desfasamento = desfasamentoPos(partida, chegada, 8);//pto partida, chegada e módulo do desfasamento

                graphic.DrawLine(p2, desfasamento[0], desfasamento[1]);
            }
        }

        private void drawTrigPts()
        {
            foreach (EastingNorthing enzTmp in clBoxTrig.CheckedItems)
            {
                //transformação entre referenciais
                EastingNorthing coordTmp = new EastingNorthing();
                coordTmp = calcTransfCoord(enzTmp, ScaleView);

                Rectangle rectgle = new Rectangle((int)coordTmp.E - 2, (int)coordTmp.N - 2, 4, 4);
                graphic.DrawArc(new Pen(Color.Black, 2), rectgle, 0.0F, 360.0F);
            }
        }
        private void drawControlPts()
        {
            foreach (EastingNorthing enzTmp in clBoxControl.CheckedItems)
            {
                //transformação entre referenciais
                EastingNorthing coordTmp = new EastingNorthing();
                coordTmp = calcTransfCoord(enzTmp, ScaleView);

                PointF[] triangl = new PointF[3];
                triangl[0] = new PointF((float)coordTmp.E + 3, (float)coordTmp.N + 3);
                triangl[1] = new PointF((float)coordTmp.E - 0, (float)coordTmp.N - 3);
                triangl[2] = new PointF((float)coordTmp.E - 3, (float)coordTmp.N + 3);
                graphic.DrawPolygon(new Pen(Color.Brown, 2), triangl);
            }
        }

        private void drawFixedPts()
        {
            foreach (EastingNorthing enzTmp in clBoxFix.CheckedItems)
            {
                //transformação entre referenciais
                EastingNorthing coordTmp = new EastingNorthing();
                coordTmp = calcTransfCoord(enzTmp, ScaleView);

                PointF[] triangl = new PointF[4];
                triangl[0] = new PointF((float)coordTmp.E + 3, (float)coordTmp.N + 3);
                triangl[1] = new PointF((float)coordTmp.E + 3, (float)coordTmp.N - 3);
                triangl[2] = new PointF((float)coordTmp.E - 3, (float)coordTmp.N - 3);
                triangl[3] = new PointF((float)coordTmp.E - 3, (float)coordTmp.N + 3);
                graphic.DrawPolygon(new Pen(Color.Brown, 2), triangl);
            }
        }

        /// <summary>
        /// Desfasamento - Distância à qual as linhas de direcções e distâncias estão afastadas do ponto
        /// Calcula a posição do ponto de partida e chegada das linhas de direcção e distância
        /// </summary>
        /// <param name="pto1"></param>
        /// <param name="pto2"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        private List<Point> desfasamentoPos(Point pto1, Point pto2, Double delta)
        {
            Double m1 = pto1.X, p1 = pto1.Y, m2 = pto2.X, p2 = pto2.Y;
            Double rumo = 0, c = 0;
            List<Point> listPts = new List<Point>();

            rumo = Math.Atan2(m2 - m1, p2 - p1) > 0 ? Math.Atan2(m2 - m1, p2 - p1) : Math.Atan2(m2 - m1, p2 - p1) + 2 * Math.PI;

            pto1.X = (int)(Math.Sin(rumo) * delta + pto1.X);
            pto1.Y = (int)(Math.Cos(rumo) * delta + pto1.Y);
            pto2.X = (int)(-Math.Sin(rumo) * delta + pto2.X);
            pto2.Y = (int)(-Math.Cos(rumo) * delta + pto2.Y);

            listPts.Add(pto1);
            listPts.Add(pto2);

            return listPts;
        }

        /// <summary>
        /// Transformacao entre o ref de origem e o ref do painel
        /// </summary>
        /// <param name="enzTmp"></param>
        /// <param name="escala"></param>
        /// <returns></returns>
        private EastingNorthing calcTransfCoord(EastingNorthing enzTmp, Double escala)
        {
            EastingNorthing enzOut = new EastingNorthing();
            //matrixTmp[0, 0] = (DisplayPanelWidth - PanelBorder - ZeroPanelX) - escala[0, 0] * (enzTmp.E - MinE);
            //matrixTmp[1, 0] = (DisplayPanelHeight - PanelBorder - ZeroPanelX) - escala[1, 0] * (enzTmp.N - MinN);
            //Double escalaMenor = escala[0, 0] < escala[1, 0] ? escala[0, 0] : escala[1, 0];
            //matrixTmp[0, 0] = (ViewWidth - PanelBorder - zeroPanelX) - escalaMenor * (enzTmp.E - MinE);
            //matrixTmp[1, 0] = (ViewHeight - PanelBorder - zeroPanelY) - escalaMenor * (enzTmp.N - MinN);            
            enzOut.E = AX * enzTmp.E + BX;
            enzOut.N = AY * enzTmp.N + BY;
            return enzOut;
        }

        private double transfInversa(int pixel)
        {
            return (pixel - BX) / AX;
        }
        #endregion

        #region Sets & Gets

        public double EscalaElipsesMM
        {
            get { return escalaElipsesMM; }
            set { escalaElipsesMM = value; }
        }

        public bool ArrowOn
        {
            get { return arrowOn; }
            set { arrowOn = value; }
        }

        public int ElipseOn
        {
            get { return elipseOn; }
            set { elipseOn = value; }
        }

        public int GridOn
        {
            get { return gridOn; }
            set { gridOn = value; }
        }

        public List<double> Deslocamentos
        {
            get { return deslocamentos; }
            set { deslocamentos = value; }
        }

        public List<Ellipse> ConfidenceElipses
        {
            get { return confidenceElipses; }
            set { confidenceElipses = value; }
        }

        public List<Ellipse> ErrorElipses
        {
            get { return errorElipses; }
            set { errorElipses = value; }
        }

        public Double BX
        {
            get { return bX; }
            set { bX = value; }
        }

        public Double BY
        {
            get { return bY; }
            set { bY = value; }
        }

        public Double AX
        {
            get { return aX; }
            set { aX = value; }
        }

        public Double AY
        {
            get { return aY; }
            set { aY = value; }
        }

        /// <summary>
        /// Factor de escala - Matrix (2,1) [x, y]
        /// </summary>
        public Double ScaleView
        {
            get { return scale; }
            set
            {
                try
                {
                    scale = value;
                }
                catch
                {
                }
            }
        }

        public Double ScaleFitView
        {
            get
            {
                defineScaleFitView();
                return scaleFitView;
            }
            private set { scaleFitView = value; }
        }


        public void defineScaleFitView()
        {
            MathNet.Numerics.LinearAlgebra.Matrix escala = new MathNet.Numerics.LinearAlgebra.Matrix(2, 1);
            escala[0, 0] = ((DisplayPanelWidth - PanelBorder * 2) / (MaxE - MinE));
            escala[1, 0] = ((DisplayPanelHeight - PanelBorder * 2) / (MaxN - MinN));
            Double escalaMenor = escala[0, 0] < escala[1, 0] ? escala[0, 0] : escala[1, 0];
            ScaleFitView = escalaMenor;
        }

        public void defineScaleView()
        {
            MathNet.Numerics.LinearAlgebra.Matrix escala = new MathNet.Numerics.LinearAlgebra.Matrix(2, 1);
            escala[0, 0] = ((ViewWidth - PanelBorder * 2) / (MaxE - MinE));
            escala[1, 0] = ((ViewHeight - PanelBorder * 2) / (MaxN - MinN));
            Double escalaMenor = escala[0, 0] < escala[1, 0] ? escala[0, 0] : escala[1, 0];
            ScaleView = escalaMenor;
        }

        public double MaxE
        {
            get
            {
                defineMaxE();
                return maxE;
            }
            set { maxE = value; }
        }

        private void defineMaxE()
        {
            double max = double.MinValue;
            foreach (EastingNorthing enzTmp in clBoxTrig.Items)
            {
                max = enzTmp.E > max ? enzTmp.E : max;
            }
            foreach (EastingNorthing enzTmp in clBoxControl.Items)
            {
                max = enzTmp.E > max ? enzTmp.E : max;
            }
            foreach (EastingNorthing enzTmp in clBoxFix.Items)
            {
                max = enzTmp.E > max ? enzTmp.E : max;
            }
            MaxE = max;
        }

        public double MaxN
        {
            get
            {
                defineMaxN();
                return maxN;
            }
            set { maxN = value; }
        }

        private void defineMaxN()
        {
            double max = double.MinValue;
            foreach (EastingNorthing enzTmp in clBoxTrig.Items)
            {
                max = enzTmp.N > max ? enzTmp.N : max;
            }
            foreach (EastingNorthing enzTmp in clBoxControl.Items)
            {
                max = enzTmp.N > max ? enzTmp.N : max;
            }
            foreach (EastingNorthing enzTmp in clBoxFix.Items)
            {
                max = enzTmp.N > max ? enzTmp.N : max;
            }
            MaxN = max;
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

        public double ViewWidth
        {
            get { return viewWidth; }
            set { viewWidth = value; }
        }

        public double ViewHeight
        {
            get { return viewHeight; }
            set { viewHeight = value; }
        }

        public double DisplayPanelWidth
        {
            get { return displayPanel.Width; }
        }

        public double DisplayPanelHeight
        {
            get { return displayPanel.Height; }
        }

        public double PanelBorder
        {
            get { return border; }
            set { border = value; }
        }

        public double TranslacaoX
        {
            get { return translacaoX; }
            set { translacaoX = value; }
        }

        public double TranslacaoY
        {
            get { return translacaoY; }
            set { translacaoY = value; }
        }

        public Boolean DistCheck
        {
            get { return distCheck; }
            set { distCheck = value; }
        }

        public Boolean DirCheck
        {
            get { return dirCheck; }
            set { dirCheck = value; }
        }

        public Double ZeroPanelX
        {
            get { return zeroPanelX; }
            set { zeroPanelX = value; }
        }

        public Double ZeroPanelY
        {
            get { return zeroPanelY; }
            set { zeroPanelY = value; }
        }

        public Double LastZeroPanelX
        {
            get { return lastZeroPanelX; }
            set { lastZeroPanelX = value; }
        }

        public Double LastZeroPanelY
        {
            get { return lastZeroPanelY; }
            set { lastZeroPanelY = value; }
        }
        #endregion
    }
}
