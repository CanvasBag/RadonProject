using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;
using BaseCoordinates.Seed;
using MathNet.Numerics.LinearAlgebra;
using AjustLeastSquare;

namespace GeoMathLib.Calc
{
    

    public class Nivela
    {
        private List<ReadStation> obsList = new List<ReadStation>();
        private int nObs;

        private GeoCoord benchmarks = new GeoCoord();
        private int nBenchmarks;

        private GeoCoord points = new GeoCoord();
        private int nPoints;

        private Matrix designMatrix;
        private Matrix obsMatrix;
        private Matrix weightsObs;

        private Matrix adjustedPoints;
        private Matrix residuals;


        public Nivela()
        {
            obsList.Add(new ReadStation("X", "A", 5.10, 4.0));
            obsList.Add(new ReadStation("A", "Y", 2.34, 3.0));
            obsList.Add(new ReadStation("Y", "C", -1.25, 2.0));
            obsList.Add(new ReadStation("C", "X", -6.13, 3.0));
            obsList.Add(new ReadStation("A", "B", -0.68, 2.0));
            obsList.Add(new ReadStation("Y", "B", -3.00, 2.0));
            obsList.Add(new ReadStation("B", "C", 1.70, 2.0));
            nObs = obsList.Count;

            benchmarks.addHeightPoint(new Height(100.00, "X"));
            benchmarks.addHeightPoint(new Height(107.50, "Y"));
            nBenchmarks = 2;
        }

        public void MakePoints()
        {
            bool isBenchmark, isPoint;
            nPoints = 0; //temp

            foreach (ReadStation readstation in obsList)
            {
                //behindObs
                isBenchmark = false;
                foreach (Height benchmark in benchmarks.HeightList)
                {
                    if (readstation.behindObs == benchmark.ID)
                    {
                        isBenchmark = true;
                        break;
                    }
                }

                if (!isBenchmark)
                {
                    isPoint = false;
                    foreach (Height point in points.HeightList)
                    {
                        if (readstation.behindObs == point.ID)
                        {
                            isPoint = true;
                            break;
                        }
                    }

                    if (!isPoint)
                    {
                        points.addHeightPoint(new Height(0.0, readstation.behindObs));
                        nPoints++; //temp
                    }
                }

                //aheadObs
                isBenchmark = false;
                foreach (Height benchmark in benchmarks.HeightList)
                {
                    if (readstation.aheadObs == benchmark.ID)
                    {
                        isBenchmark = true;
                        break;
                    }
                }

                if (!isBenchmark)
                {
                    isPoint = false;
                    foreach (Height point in points.HeightList)
                    {
                        if (readstation.aheadObs == point.ID)
                        {
                            isPoint = true;
                            break;
                        }
                    }

                    if (!isPoint)
                    {
                        points.addHeightPoint(new Height(0.0, readstation.aheadObs));
                        nPoints++; //temp
                    }
                }
            }
        }


        public void BuildAjustmentElements()
        {
            designMatrix = new Matrix(nObs, nPoints, 0.0);
            obsMatrix = new Matrix(nObs, 1);
            weightsObs = new Matrix(nObs, nObs, 0.0);

            //preenche a obsMatrix com as observações
            for (int i = 0; i < nObs; i++)
            {
                obsMatrix[i, 0] = obsList[i].slope;
            }

            int j = 0;
            int isBenchmark;
            int isPoint;
            foreach (ReadStation readstation in obsList)
            {
                //behindObs
                isBenchmark = IsBenchmark(readstation.behindObs);
                if (isBenchmark != -1) //se for ponto fixo
                {
                    obsMatrix[j, 0] += benchmarks.HeightList[isBenchmark].H;
                }
                else
                {
                    //-
                    isPoint = IsPoint(readstation.behindObs);
                    designMatrix[j, isPoint] = -1;
                }

                //aheadObs

                isBenchmark = IsBenchmark(readstation.aheadObs);
                if (isBenchmark != -1) //se for ponto fixo
                {
                    //-
                    obsMatrix[j, 0] -= benchmarks.HeightList[isBenchmark].H;
                }
                else
                {
                    //+
                    isPoint = IsPoint(readstation.aheadObs);
                    designMatrix[j, isPoint] = 1;
                }

                //Matriz de pesos ********
                double var = 12.0;
                weightsObs[j, j] = var / readstation.distance;
                // ********

                j++; //incrementa
            }
        }

        //verifica se um determinado ponto é um ponto fixo
        private int IsBenchmark(string pointName)
        {
            int index = -1;
            int i = 0;
            foreach (Height benchmark in benchmarks.HeightList)
            {
                if (pointName == benchmark.ID)
                {
                    index = i;
                    break;
                }
                i++;
            }
            return index;
        }

        private int IsPoint(string pointName)
        {
            int index = -1;
            int i = 0;

            foreach (Height point in points.HeightList)
            {
                if (pointName == point.ID)
                {
                    index = i;
                    break;
                }
                i++;
            }
            return index;
        }

        public void ajusmentProcess()
        {
            LinearParametric ajusmentprocess = new LinearParametric(nPoints, nObs, obsMatrix, designMatrix, weightsObs, null, 1.0);

            ajusmentprocess.Compute();

            residuals = ajusmentprocess.Residuals;
            adjustedPoints = ajusmentprocess.AjustedParam;
        }

        public struct ReadStation
        {
            public ReadStation(string behindObs, string aheadObs, double slope, double distance)
            {
                this.behindObs = behindObs;
                this.aheadObs = aheadObs;
                this.slope = slope;
                this.distance = distance;
            }

            public string behindObs;
            public string aheadObs;
            public double slope;
            public double distance;
        }

        //***temporario***
        public void imprimeListaObs()
        {
            Console.WriteLine("Observacoes" + "  " + nObs);
            foreach (ReadStation obs in obsList)
            {
                Console.WriteLine(obs.aheadObs + " " + obs.behindObs + " " + obs.slope + " " + obs.distance);
            }
            
            Console.WriteLine();
            Console.WriteLine("Pontos fixos" + "  " + nBenchmarks);

            foreach (Height benchmark in benchmarks.HeightList)
            {
                Console.WriteLine(benchmark.ID + "  " + benchmark.H);
            }

            Console.WriteLine();
            Console.WriteLine("Pontos" + "  " + nPoints);

            foreach (Height point in points.HeightList)
            {
                Console.WriteLine(point.ID + "  " + point.H);
            }

            Console.WriteLine();
            Console.WriteLine("Matriz l" + "  " + obsMatrix.RowCount);

            for (int i = 0; i < obsMatrix.RowCount; i++)
            {
                for (int j = 0; j < obsMatrix.ColumnCount; j++)
                {
                    Console.Write(obsMatrix[i,j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Matriz A" + "  " + designMatrix.RowCount);

            for (int i = 0; i < designMatrix.RowCount; i++)
            {
                for (int j = 0; j < designMatrix.ColumnCount; j++)
                {
                    Console.Write("{0,3}", designMatrix[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Matriz W" + "  " + weightsObs.RowCount);

            for (int i = 0; i < weightsObs.RowCount; i++)
            {
                for (int j = 0; j < weightsObs.ColumnCount; j++)
                {
                    Console.Write("{0,3}", weightsObs[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Matriz X_" + "  " + adjustedPoints.RowCount);

            for (int i = 0; i < adjustedPoints.RowCount; i++)
            {
                for (int j = 0; j < adjustedPoints.ColumnCount; j++)
                {
                    Console.Write("{0,8:0.000}", adjustedPoints[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Matriz v_" + "  " + residuals.RowCount);

            for (int i = 0; i < residuals.RowCount; i++)
            {
                for (int j = 0; j < residuals.ColumnCount; j++)
                {
                    Console.Write("{0,8:0.000}", residuals[i, j]);
                }
                Console.WriteLine();
            }
        }
        //***temporario***

    }
}
