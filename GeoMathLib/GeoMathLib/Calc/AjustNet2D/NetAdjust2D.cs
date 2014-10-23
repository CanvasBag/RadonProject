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
    /// <summary>
    /// PT - Classe de Ajustamento de Redes 2D com pontos fixos ou de Constrangimento Mínimo
    /// EN - 2D Network ajustment class, using fixed or constrained points         
    /// </summary>
    public class NetAdjust2D
    {
        private List<ReadStationDist> obsListDist = new List<ReadStationDist>();
        private List<ReadStationDir> obsListDir = new List<ReadStationDir>();
        private int nObsDist, nObsDir;
        private MatrixFilIn ex2XML;

        private GeoCoord trigPoints = new GeoCoord();
        private int nTrigPoints;

        /// <summary>
        /// PT - Inicializa uma nova instância do NetAdjust2D
        /// EN - Initializes a new instance of the NetAdjust2D
        /// </summary>
        /// <param name="xmlFile">XML path</param>
        public NetAdjust2D(string xmlFile)
        {
            try
            {
                ReadXmlData(xmlFile);
            }
            catch
            {
                Console.WriteLine("Erro de processamento no construtor");
            }
        }

        /// <summary>
        /// PT - Inicializa uma nova instância do NetAdjust2D
        /// EN - Initializes a new instance of the NetAdjust2D 
        /// </summary>
        /// <param name="obsListDistIn">List with of Distances</param>
        /// <param name="obsListDirIn">List with of Directions</param>
        /// <param name="trigPointsIn">List with of Trignometric Points</param>
        public NetAdjust2D(List<ReadStationDist> obsListDistIn, List<ReadStationDir> obsListDirIn, GeoCoord trigPointsIn)
        {
            obsListDist = obsListDistIn;
            obsListDir = obsListDirIn;
            trigPoints = trigPointsIn;
        }

        public NetAdjust2D()
        {
        }

        #region XML Reader
        //Colheita de dados a partir de um ficheiro XML
        private void ReadXmlData(string xmlFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            XmlNodeList trigpoints = doc.GetElementsByTagName("trigpoints");
            int idObservable = 0;

            int a1;
            int a2;
            double a3;

            //pontos fixos
            foreach (XmlNode trigpoint in trigpoints)
            {
                string id;
                double easting, northing;
                int i = 0;
                foreach (XmlNode point in trigpoint.ChildNodes)
                {
                    id = point.ChildNodes[0].FirstChild.InnerText;
                    easting = Double.Parse(point.ChildNodes[1].FirstChild.InnerText);
                    northing = Double.Parse(point.ChildNodes[2].FirstChild.InnerText);

                    //Console.WriteLine("<{0}><{1}><{2}>\n", id, easting, northing);

                    EastingNorthing newPoint = new EastingNorthing(easting, northing, 0.0, id);
                    newPoint.ListDadosDivInt.Add(i++); //id : 0 > fixos - 0 <= nao fixos
                    newPoint.ListDadosDivInt.Add(0);   //constrangimento
                    //As coordenadas Easting e Northing 
                    //sao guaradadas na lista de doubles
                    //posicao 0 -> Easting; posicao 1 -> Northing
                    newPoint.ListDadosDivD.Add(newPoint.E);
                    newPoint.ListDadosDivD.Add(newPoint.N);
                    trigPoints.addENPoint(newPoint);
                }
                nTrigPoints = trigPoints.EastingNorthingList.Count;
            }

            //distancias
            XmlNodeList distances = doc.GetElementsByTagName("distances");
            foreach (XmlNode distance in distances)
            {
                foreach (XmlNode station in distance)
                {
                    EastingNorthing occupied = null, observed = null;
                    foreach (XmlNode element in station.ChildNodes)
                    {
                        if (element.Name == "id")
                        {
                            occupied = GetEastingNorthing(element.InnerText);
                        }
                        else
                        {

                            observed = GetEastingNorthing(element.ChildNodes[0].FirstChild.InnerText);

                            obsListDist.Add(new ReadStationDist(idObservable++, occupied, observed, Double.Parse(element.ChildNodes[1].FirstChild.InnerText)));

                        }
                        //Console.WriteLine("<{0}><{1}><{2}>\n", observation.ChildNodes[0].InnerText, observation.ChildNodes[1].InnerText, observation.ChildNodes[2].InnerText);


                    }
                }
                nObsDist = obsListDist.Count;
            }

            //direccoes
            XmlNodeList directions = doc.GetElementsByTagName("directions");
            idObservable = 0;
            foreach (XmlNode direction in directions)
            {
                int iStation = -1; // numero associado ah mudanca de estacao

                int unit = 0; //por omissão fica radianos
                if (direction.Attributes["unit"] != null)
                {
                    if (direction.Attributes["unit"].InnerText == "degree")
                    {
                        unit = 1;
                    }
                    else if (direction.Attributes["unit"].InnerText == "dms")
                    {
                        unit = 2;
                    }
                }


                foreach (XmlNode station in direction)
                {
                    EastingNorthing occupied = null, observed = null;
                    double reading = 0;


                    foreach (XmlNode element in station.ChildNodes)
                    {
                        if (element.Name == "id")
                        {
                            occupied = GetEastingNorthing(element.InnerText);
                            iStation++; // muda de estacao
                        }
                        else
                        {

                            observed = GetEastingNorthing(element.ChildNodes[0].FirstChild.InnerText);

                            switch (unit)
                            {
                                case 0:
                                    reading = Double.Parse(element.ChildNodes[1].FirstChild.InnerText);
                                    break;
                                case 1:
                                    double tmp = SIUnits.Deg2Rad;
                                    reading = Double.Parse(element.ChildNodes[1].FirstChild.InnerText) * SIUnits.Deg2Rad;
                                    break;
                                case 2:
                                    Console.WriteLine(Convert.ToInt32(element.ChildNodes[1].ChildNodes[0].InnerText));
                                    Console.WriteLine(Convert.ToInt32(element.ChildNodes[1].ChildNodes[1].InnerText));
                                    Console.WriteLine(Convert.ToDouble(element.ChildNodes[1].ChildNodes[2].InnerText));
                                    reading = SIUnits.Degree(new Tuple<int, int, double>(Convert.ToInt32(element.ChildNodes[1].ChildNodes[0].InnerText), Convert.ToInt32(element.ChildNodes[1].ChildNodes[1].InnerText), Convert.ToDouble(element.ChildNodes[1].ChildNodes[2].InnerText))) * SIUnits.Deg2Rad;
                                    break;
                            }

                            obsListDir.Add(new ReadStationDir(idObservable++, occupied, observed, iStation, reading));

                        }
                        //Console.WriteLine("<{0}><{1}><{2}>\n", observation.ChildNodes[0].InnerText, observation.ChildNodes[1].InnerText, observation.ChildNodes[2].InnerText);


                    }
                }
                nObsDir = obsListDir.Count;
            }
        }
        #endregion

        #region Auxiliars methods
        //Procurar um EastNorting numa lista
        private EastingNorthing GetEastingNorthing(string namePoint)
        {
            foreach (EastingNorthing p in trigPoints.EastingNorthingList)
            {
                if (p.ID == namePoint)
                {
                    return p;
                }
            }
            return null;
        }

        //Definição das control stations
        public void DefineControlSations(string[] controlSt)
        {
            EastingNorthing controlStation;
            foreach (string id in controlSt)
            {
                //trigpoint com o nome id
                controlStation = GetEastingNorthing(id);
                //Indentificacao deste trigpoint como sendo uma control station
                controlStation.ListDadosDivInt[1] = 1;
                ////As coordenadas Easting e Northing 
                ////sao guaradadas na lista de doubles
                ////posicao 0 -> Easting; posicao 1 -> Northing
                //controlStation.ListDadosDivD.Add(controlStation.E);
                //controlStation.ListDadosDivD.Add(controlStation.N);
            }
        }

        //Definição das fixed stations
        public void DefineFixedSations(string[] fixedlSt)
        {
            int i = -1;
            foreach (string id in fixedlSt)
            {
                GetEastingNorthing(id).ListDadosDivInt[0] = i--;
            }

            i = 0;
            foreach (EastingNorthing p in trigPoints.EastingNorthingList)
            {
                if (p.ListDadosDivInt[0] >= 0)
                {
                    p.ListDadosDivInt[0] = i++;
                }
            }
        }

        /// <summary>
        /// PT - ajustamento da rede
        /// EN - network ajustment
        /// </summary>
        /// <param name="a">1º Parâmetro da equação de var das distâncias</param>
        /// <param name="b">2º Parâmetro da equação de var das distâncias</param>
        /// <param name="varDirections">Stdv direcções em segundos</param>
        /// <param name="varAzimuth">Stdv Azimutes em segundos</param>
        /// <returns></returns>
        public NonLinearParametric ajustNet(Double a, Double b, Double sigmaDirections, Double sigmaAzimuth)
        {
            ex2XML = new MatrixFilIn(obsListDir, obsListDist, trigPoints);
            Matrix x = ex2XML.defineX();
            Matrix l = ex2XML.defineL();
            ex2XML.defineQll(a, b, sigmaDirections, sigmaAzimuth);

            NonLinearParametric ajustamento = new NonLinearParametric(ex2XML.NumParameters, ex2XML.NumObservations, x, l, null, ex2XML.qll, 1);
            ajustamento.Compute(ex2XML.defineA, ex2XML.defineF);
            ex2XML.trigPointsXUpdate(ajustamento.AjustedParam);
            return ajustamento;
        }

        #endregion

        #region Set & Gets

        public List<ReadStationDist> ObsListDist
        {
            get { return obsListDist; }
            set { obsListDist = value; }
        }

        public List<ReadStationDir> ObsListDir
        {
            get { return obsListDir; }
            set { obsListDir = value; }
        }

        public GeoCoord TrigPts
        {
            get  { return trigPoints; }
            set {  trigPoints = value; }
        }

        public int NumFixedPts
        {
            get { return ex2XML.NumPtsFixed; }
        }

        public int NumControlPts
        {
            get { return ex2XML.NumControlPts; }
        }

        public int NumNormalPts
        {
            get { return ex2XML.NumPtsNotFixed - ex2XML.NumControlPts; }
        }
        #endregion

        #region Structs to observables
        public class ReadStationDist
        {
            public double residue;
            public double stdResidue;
            public double localRedundancy;
            public double bias;
            public ReadStationDist(int id, EastingNorthing occupied, EastingNorthing observed, double distance)
            {
                this.occupied = occupied;
                this.observed = observed;
                this.distance = distance;
                this.id = id;
            }

            public ReadStationDist() { }

            public EastingNorthing occupied;
            public EastingNorthing observed;
            public double distance;
            public int id;
            public override String ToString()
            {
                return occupied.ID + "->" + observed.ID;
            }
        }

        public class ReadStationDir
        {
            public double residue;
            public double stdResidue;
            public double localRedundancy;
            public double bias;
            public ReadStationDir(int id, EastingNorthing occupied, EastingNorthing observed, int az0, double direction)
            {
                this.occupied = occupied;
                this.observed = observed;
                this.az0 = az0;
                this.direction = direction;
                this.id = id;
            }

            public ReadStationDir() { }

            public EastingNorthing occupied;
            public EastingNorthing observed;
            public int az0; // os ReadStationDir que teem o mesmo az0 pertencem ao mesmo estacionamento e por isso tem o mesmo Rumo (azimute) inicial
            public double direction; //radianos
            public int id;
            public override String ToString()
            {
                return occupied.ID + "->" + observed.ID;
            }
        }
        #endregion

        #region Class to FilInMatrices
        //enzTmp.ListDadosDivInt[0] se é fixo (-1) ou não. Se não este número indica a posição da estação
        //enzTmp.ListDadosDivInt[1] se é ou não pto de constrangimento mínimo
        //enzTmp.ListDadosDivD[0] posição 0 - valor original da coordenada m
        //enzTmp.ListDadosDivD[1] posição 1 - valor original da coordenada p

        public class MatrixFilIn
        {
            private List<NetAdjust2D.ReadStationDir> obsListDir;
            private List<NetAdjust2D.ReadStationDist> obsListDist;
            private GeoCoord trigPoints;
            private int nPointsNotFixed, nFixedlPoints, nControlPoints, numParam, numEquations, numObs, numR0;
            private List<int> r0sID;
            public Matrix a, f, qll;
            public Matrix x, l;

            public MatrixFilIn(List<NetAdjust2D.ReadStationDir> obsListDirIn, List<NetAdjust2D.ReadStationDist> obsListDistIn, GeoCoord trigPointsIn)
            {
                obsListDir = obsListDirIn;
                obsListDist = obsListDistIn;
                trigPoints = trigPointsIn;
                a = new Matrix(NumEquations, NumParameters);
                x = new Matrix(NumParameters, 1);
                l = new Matrix(NumObservations, 1);
                f = new Matrix(NumEquations, 1);
                qll = new Matrix(NumObservations, NumObservations);
                r0sID = new List<int>();
            }

            #region Defenir Matriz A

            /// <summary>
            /// Matriz de Configuração - Matriz x deve já estar definida
            /// </summary>
            public Matrix defineA(Matrix aIn, Matrix lIn, Matrix xIn)
            {
                a = aIn == null ? new Matrix(NumEquations, NumParameters) : aIn;
                x = xIn;
                l = lIn;

                int i = 0, j1 = 0, j2 = 0, j3 = 0; // posições
                Double m1 = 0, p1 = 0, m2 = 0, p2 = 0; // coordenadas
                // Derivadas
                int dfdAlpha;
                Double dfdM1, dfdP1, dfdM2, dfdP2;

                //Update das Coordenadas
                trigPointsXUpdate();

                foreach (NetAdjust2D.ReadStationDir obsDir in obsListDir)
                {
                    j1 = obsDir.occupied.ListDadosDivInt[0] >= 0 ? obsDir.occupied.ListDadosDivInt[0] * 2 : -1; //posição na matriz A da 1ª estação
                    j2 = obsDir.observed.ListDadosDivInt[0] >= 0 ? obsDir.observed.ListDadosDivInt[0] * 2 : -1; //posição na matriz A da 2ª estação

                    m1 = obsDir.occupied.E;
                    p1 = obsDir.occupied.N;
                    m2 = obsDir.observed.E;
                    p2 = obsDir.observed.N;

                    dfdAlpha = -1;
                    j3 = (NumPtsNotFixed * 2 - 1) + (obsDir.az0 + 1); //posição na matriz A do Rumo0

                    if (j1 >= 0)
                    {
                        //dfdM1 = -1 / ((p2 - p1) * (Math.Pow((m2 - m1), 2) / Math.Pow((p2 - p1), 2) + 1));
                        //dfdP1 = (m2 - m1) / (Math.Pow((p2 - p1), 2) * (Math.Pow((m2 - m1), 2) / Math.Pow((p2 - p1), 2) + 1));

                        dfdM1 = - (p2 - p1) / ( Math.Pow((m2 - m1), 2) + Math.Pow((p2 - p1), 2) );
                        dfdP1 = (m2 - m1) / ( Math.Pow((m2 - m1), 2) + Math.Pow((p2 - p1), 2) );
                        
                        a[i, j1] = dfdM1;
                        a[i, j1 + 1] = dfdP1;
                    }
                    if (j2 >= 0)
                    {
                        //dfdM2 = 1 / ((p2 - p1) * (Math.Pow((m2 - m1), 2) / Math.Pow((p2 - p1), 2) + 1));
                        //dfdP2 = -(m2 - m1) / (Math.Pow((p2 - p1), 2) * (Math.Pow((m2 - m1), 2) / Math.Pow((p2 - p1), 2) + 1));

                        dfdM2 = (p2 - p1) / ( Math.Pow((m2 - m1), 2) + Math.Pow((p2 - p1), 2) );
                        dfdP2 = -(m2 - m1) / ( Math.Pow((m2 - m1), 2) + Math.Pow((p2 - p1), 2) );

                        a[i, j2] = dfdM2;
                        a[i, j2 + 1] = dfdP2;
                    }
                    a[i, j3] = dfdAlpha;
                    i++;
                }


                foreach (NetAdjust2D.ReadStationDist obsDist in obsListDist)
                {
                    j1 = obsDist.occupied.ListDadosDivInt[0] >= 0 ? obsDist.occupied.ListDadosDivInt[0] * 2 : -1; //posição na matriz A da 1ª estação
                    j2 = obsDist.observed.ListDadosDivInt[0] >= 0 ? obsDist.observed.ListDadosDivInt[0] * 2 : -1; //posição na matriz A da 2ª estação

                    m1 = obsDist.occupied.E;
                    p1 = obsDist.occupied.N;
                    m2 = obsDist.observed.E;
                    p2 = obsDist.observed.N;


                    if (j1 >= 0)
                    {
                        dfdM1 = -(m2 - m1) / Math.Sqrt((Math.Pow((p2 - p1), 2) + Math.Pow((m2 - m1), 2)));
                        dfdP1 = -(p2 - p1) / Math.Sqrt((Math.Pow((p2 - p1), 2) + Math.Pow((m2 - m1), 2)));
                        a[i, j1] = dfdM1;
                        a[i, j1 + 1] = dfdP1;
                    }
                    if (j2 >= 0)
                    {
                        dfdM2 = (m2 - m1) / Math.Sqrt((Math.Pow((p2 - p1), 2) + Math.Pow((m2 - m1), 2)));
                        dfdP2 = (p2 - p1) / Math.Sqrt((Math.Pow((p2 - p1), 2) + Math.Pow((m2 - m1), 2)));
                        a[i, j2] = dfdM2;
                        a[i, j2 + 1] = dfdP2;
                    }
                    i++;
                }

                foreach (EastingNorthing enzTmp in trigPoints.EastingNorthingList)
                {
                    int j;
                    if (enzTmp.ListDadosDivInt[1] == 1)
                    {
                        j = enzTmp.ListDadosDivInt[0] * 2;
                        a[i++, j] = 1;
                        a[i++, j + 1] = 1;
                    }
                }
                return a;
            }
            #endregion

            #region Definir Matriz F

            /// <summary>
            /// Matriz de Fecho
            /// </summary>
            public Matrix defineF(Matrix fIn, Matrix lIn, Matrix xIn)
            {
                f = fIn == null ? new Matrix(NumEquations, 1) : fIn;
                l = lIn;
                x = xIn;

                Double c = 0;
                Double m1 = 0, p1 = 0, m2 = 0, p2 = 0; // coordenadas
                Double rumo_Limbo, rumo, distancia, alpha, residuo = 0;
                int i = 0;

                foreach (NetAdjust2D.ReadStationDir obsDir in obsListDir)
                {
                    m1 = obsDir.occupied.E;
                    p1 = obsDir.occupied.N;
                    m2 = obsDir.observed.E;
                    p2 = obsDir.observed.N;
                    alpha = obsDir.direction;// *Math.PI / 180; //SOTAVENTO /%$%&#$#$%"%#$"#$"$#"$%#"$%#"#$%"$%#"$%#"#$%"$#%"$%#;
                    rumo_Limbo = x[obsDir.az0 + NumPtsNotFixed * 2, 0];
                    
                    rumo = Math.Atan2((m2 - m1), (p2 - p1)) > 0 ? Math.Atan2((m2 - m1), (p2 - p1)) : Math.Atan2((m2 - m1), (p2 - p1)) + Math.PI * 2;
                    residuo = rumo - rumo_Limbo - alpha;

                    while (Math.Abs(residuo) > Math.PI)
                        residuo -= (2 * Math.PI * Math.Abs(residuo)) / residuo;

                    f[i, 0] = residuo;
                    i++;
                }
                foreach (NetAdjust2D.ReadStationDist obsDist in obsListDist)
                {
                    m1 = obsDist.occupied.E;
                    p1 = obsDist.occupied.N;
                    m2 = obsDist.observed.E;
                    p2 = obsDist.observed.N;
                    distancia = obsDist.distance;

                    residuo = Math.Sqrt(Math.Pow((m2 - m1), 2) + Math.Pow((p2 - p1), 2)) - distancia;
                    f[i, 0] = residuo;
                    i++;
                }
                foreach (EastingNorthing enzTmp in trigPoints.EastingNorthingList)
                {
                    int j;
                    if (enzTmp.ListDadosDivInt[1] == 1)
                    {
                        f[i++, 0] = enzTmp.E - enzTmp.ListDadosDivD[0];//posição 0 - valor original da coordenada m
                        f[i++, 0] = enzTmp.N - enzTmp.ListDadosDivD[1];//posição 1 - valor original da coordenada p
                    }
                }
                return f;
            }

            #endregion

            #region Definir matriz de var e covar das obs

            public Matrix defineQll(Double a, Double b, Double sigmaDir, Double sigmaAzimuth)
            {
                int i = -1;
                //% sig_dist = ones(size(ndist,1),1)*.002;
                foreach (NetAdjust2D.ReadStationDir obsDir in obsListDir)
                {
                    qll[++i, i] = Math.Pow(sigmaDir * SIUnits.Seg2Rad, 2); //variancia das direcçoes
                }
                foreach (NetAdjust2D.ReadStationDist obsDist in obsListDist)
                {
                    qll[++i, i] = a * a + Math.Pow(b * SIUnits.PPM, 2) * Math.Pow(obsDist.distance, 2); //variancia das distâncias

                }
                foreach (EastingNorthing enzTmp in trigPoints.EastingNorthingList)
                {
                    if (enzTmp.ListDadosDivInt[1] == 1)
                    {
                        qll[++i, i] = enzTmp.SigmaE * enzTmp.SigmaE;
                        qll[++i, i] = enzTmp.SigmaN * enzTmp.SigmaN;
                    }
                }

                return qll;
            }

            #endregion

            #region Definir a Matriz x e l

            public Matrix defineX()
            {
                if (x == null)
                    x = new Matrix(NumParameters, 1);

                Double m1 = 0, p1 = 0, m2 = 0, p2 = 0; // coordenadas
                Double rumo_Limbo = 0, alpha, c = 0;
                int i = -1;
                foreach (EastingNorthing tmpEst in trigPoints.EastingNorthingList)
                {
                    if (tmpEst.ListDadosDivInt[0] >= 0)
                    {
                        x[++i, 0] = tmpEst.E;
                        x[++i, 0] = tmpEst.N;
                    }
                }

                i++;
                int r0Alvo, r0Antigo = -1;
                foreach (NetAdjust2D.ReadStationDir obsDir in obsListDir)
                {
                    r0Alvo = obsDir.az0;
                    if (r0Alvo != r0Antigo)
                    {
                        r0Antigo = r0Alvo;
                        m1 = obsDir.occupied.E;
                        p1 = obsDir.occupied.N;
                        m2 = obsDir.observed.E;
                        p2 = obsDir.observed.N;
                        alpha = obsDir.direction;// *Math.PI / 180; //SOTAVENTO /%$%&#$#$%"%#$"#$"$#"$%#"$%#"#$%"$%#"$%#"#$%"$#%"$%#

                        rumo_Limbo = Math.Atan2((m2 - m1), (p2 - p1));
                        rumo_Limbo = rumo_Limbo> 0 ? rumo_Limbo : rumo_Limbo + Math.PI * 2;
                        rumo_Limbo -= alpha;
                        
                        rumo_Limbo = rumo_Limbo < 0 ? 2 * Math.PI + rumo_Limbo :
                                     rumo_Limbo > 2 * Math.PI ? rumo_Limbo - 2 * Math.PI : rumo_Limbo;
                        x[i + r0Alvo, 0] = rumo_Limbo;
                    }
                }

                return x;
            }

            public Matrix defineL()
            {
                if (l == null)
                    l = new Matrix(NumObservations, 1);

                int i = 0;

                foreach (NetAdjust2D.ReadStationDir read in obsListDir)
                {
                    l[i++, 0] = read.direction;
                }

                foreach (NetAdjust2D.ReadStationDist read in obsListDist)
                {
                    l[i++, 0] = read.distance;
                }

                foreach (EastingNorthing enzTmp in trigPoints.EastingNorthingList)
                {
                    if (enzTmp.ListDadosDivInt[1] == 1)
                    {
                        l[i++, 0] = enzTmp.E;
                        l[i++, 0] = enzTmp.N;
                    }
                }
                return l;
            }

            public void trigPointsXUpdate(Matrix xTmp)
            {
                int i = -1;
                foreach (EastingNorthing enzTmp in trigPoints.EastingNorthingList)
                {
                    if (enzTmp.ListDadosDivInt[0] >= 0)
                    {
                        enzTmp.E = xTmp[++i, 0];
                        enzTmp.N = xTmp[++i, 0];
                    }
                }
            }

            public void trigPointsXUpdate()
            {
                int i = -1;
                foreach (EastingNorthing enzTmp in trigPoints.EastingNorthingList)
                {
                    if (enzTmp.ListDadosDivInt[0] >= 0)
                    {
                        enzTmp.E = x[++i, 0];
                        enzTmp.N = x[++i, 0];
                    }
                }
            }

            #endregion

            #region SETs & GETs

            public int NumPtsFixed
            {
                get
                {
                    calcPtsFixed();
                    return nFixedlPoints;
                }

                set
                {
                    nFixedlPoints = value;
                }
            }

            private void calcPtsFixed()
            {
                int iTmp = 0;
                foreach (EastingNorthing enzPto in trigPoints.EastingNorthingList)
                {
                    iTmp += enzPto.ListDadosDivInt[0] < 0 ? 1 : 0;
                }
                NumPtsFixed = iTmp;
            }

            public int NumPtsNotFixed
            {
                get
                {
                    calcPtsNotFixed();
                    return nPointsNotFixed;
                }

                set
                {
                    nPointsNotFixed = value;
                }
            }

            private void calcPtsNotFixed()
            {
                int iTmp = 0;
                foreach (EastingNorthing enzPto in trigPoints.EastingNorthingList)
                {
                    iTmp += enzPto.ListDadosDivInt[0] >= 0 ? 1 : 0;
                }
                NumPtsNotFixed = iTmp;
            }

            public int NumControlPts
            {
                get
                {
                    calcControlPts();
                    return nControlPoints;
                }

                set
                {
                    nControlPoints = value;
                }
            }

            private void calcControlPts()
            {
                int iTmp = 0;
                foreach (EastingNorthing enzPto in trigPoints.EastingNorthingList)
                {
                    iTmp += enzPto.ListDadosDivInt[1] == 1 ? 1 : 0;
                }
                NumControlPts = iTmp;
            }

            public int NumParameters
            {
                get
                {
                    NumParameters = NumPtsNotFixed * 2 + NumR0;
                    return numParam;
                }

                set
                {
                    numParam = value;
                }
            }

            public int NumR0
            {

                get
                {
                    calcNumR0();
                    return numR0;
                }
                set
                {
                    numR0 = value;
                }

            }

            private void calcNumR0()
            {

                List<int> num = new List<int>();
                foreach (NetAdjust2D.ReadStationDir dirTmp in obsListDir)
                {
                    if (!num.Contains(dirTmp.az0))
                        num.Add(dirTmp.az0);                    
                }
                R0sID = num;
                NumR0 = num.Count;
            }

            public List<int> R0sID
            {
                set  { r0sID = value; }
                get 
                {
                    calcNumR0();
                    return r0sID; 
                }
            }

            public int NumEquations
            {
                get
                {
                    NumEquations = obsListDir.Count + obsListDist.Count + NumControlPts * 2;
                    return numEquations;
                }

                set
                {
                    numEquations = value;
                }
            }

            public int NumObservations
            {
                get
                {
                    NumObservations = obsListDir.Count + obsListDist.Count + NumControlPts * 2;
                    return numObs;
                }

                set
                {
                    numObs = value;
                }
            }
            #endregion
        }
        #endregion
    }


    
}
