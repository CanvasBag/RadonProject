using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;

namespace BaseCoordinates.Seed
{    
    /// <summary>
    /// Classe de coordenadas geográficas. 
    /// É composta por uma lista de objectos mp, ll ou ret.
    /// A cada objecto GeoCoord pode ser atribuido um conjunto de definições geométricas em função do datum e projecção associados        
    /// </summary>
    public class GeoCoord
    {
        private Datum paramDatum;
        private Projection paramPrj;
        private List<Height> pontosHeight;
        private List<EastingNorthing> pontosEN;
        private List<Ll> pontosLL;
        private List<Ret> pontosRET;

        /// <summary>
        /// Cria um objecto geoCoord apartir de uma lista de objectos EastingNorthing
        /// </summary>
        /// <param name="listaEN"></param>
        /// <param name="datm"></param>
        /// <param name="prj"></param>
        public GeoCoord(List<EastingNorthing> listaEN, Datum datm, Projection prj)
        {
            pontosEN = listaEN;
            paramDatum = datm;
            paramPrj = prj;
        }
        
        /// <summary>
        /// Cria um objecto geoCoord apartir de uma lista de objectos Ll
        /// </summary>
        /// <param name="listaLL"></param>
        /// <param name="datm"></param>
        public GeoCoord(List<Ll> listaLL, Datum datm)
        {
            pontosLL = listaLL;
            paramDatum = datm;
        }

        
        /// <summary>
        /// Cria um objecto geoCoord apartir de uma lista de objectos Ret
        /// </summary>
        /// <param name="listaRET"></param>
        public GeoCoord(List<Ret> listaRET)
        {
            pontosRET = listaRET;
        }

        /// <summary>
        /// Cria um objecto geoCoord apartir de uma lista de objectos Height
        /// </summary>
        /// <param name="listaHeight"></param>
        public GeoCoord(List<Height> listaHeight)
        {
            pontosHeight = listaHeight;
        }

        /// <summary>
        /// Cria um objecto GeoCoord
        /// </summary>        
        public GeoCoord()
        {
            pontosHeight = new List<Height>();
            pontosEN = new List<EastingNorthing>();
            pontosLL = new List<Ll>();
            pontosRET = new List<Ret>();
        }

        /// <summary>
        /// Adiciona um Objecto EastingNorthing
        /// </summary>
        /// <param name="pointMpIn">Objecto EastingNorthing</param>
        public void addENPoint(EastingNorthing pointMpIn)
        {
            try
            {
                pontosEN.Add(pointMpIn);
            }
            catch
            {
                throw(new NullReferenceException("O objecto GeoCoord não contém nenhuma lista<EastingNorthing> declarada"));
            }

        }

        /// <summary>
        /// Adiciona um Objecto ll
        /// </summary>
        /// <param name="pointLLIn">Objecto Ll</param>
        public void addllPoint(Ll pointLLIn)
        {
            try
            {
                pontosLL.Add(pointLLIn);
            }
            catch
            {
                throw (new NullReferenceException("O objecto GeoCoord não contém nenhuma lista<Ll> declarada"));
            }
        }

        /// <summary>
        /// Adiciona um Objecto ret
        /// </summary>
        /// <param name="pointRetIn">Objecto Ret</param>
        public void addRetPoint(Ret pointRetIn)
        {
            try
            {
                pontosRET.Add(pointRetIn);
            }
            catch
            {
                throw (new NullReferenceException("O objecto GeoCoord não contém nenhuma lista<Ret> declarada"));
            }
        }

        /// <summary>
        /// Adiciona um Objecto Height
        /// </summary>
        /// <param name="pointHeight">Objecto Height</param>
        public void addHeightPoint(Height pointHeigh)
        {
            try
            {
                pontosHeight.Add(pointHeigh);
            }
            catch
            {
                throw (new NullReferenceException("O objecto GeoCoord não contém nenhuma lista<Height> declarada"));
            }
        }

        /// <summary>
        /// Define e retorna os parâmetros do Datum:
        /// Elipsoide de Ref (objecto Elipsoid);
        /// Desvio da vertical (E/N);
        /// Ondulação do geóide na origem;
        /// Latitude Origem;
        /// Longitude Origem;
        /// Diferença de Az astronómico com geodésico.
        /// </summary>     
        public Datum Datum
        {
            set { paramDatum = value; }
            get { return paramDatum; }
        }

        /// <summary>
        /// Define e retorna os parâmetros de projecção (LongO, LatO, falsE, falseN, k0)
        /// </summary>        
        public Projection Projection
        {
            set { paramPrj = value; }
            get { return paramPrj; }            
        }

        /// <summary>
        /// devolve uma lista de obj Height
        /// </summary>
        /// <returns></returns>
        public List<Height> HeightList
        {
            set { pontosHeight = value; }
            get { return pontosHeight; }

        }

        /// <summary>
        /// devolve uma lista de obj EastingNorthing
        /// </summary>
        /// <returns></returns>
        public List<EastingNorthing> EastingNorthingList
        {
            set { pontosEN = value; }
            get { return pontosEN; }
            
        }

        /// <summary>
        /// devolve uma lista de obj EastingNorthing
        /// </summary>
        /// <param name="id_"></param>
        /// <returns></returns>
        public List<EastingNorthing> getENList(String id_)
        {
            List<EastingNorthing> tmpMp = new List<EastingNorthing>();
            foreach (EastingNorthing ptoMpTmp in pontosEN)
            {
                if (ptoMpTmp.ID == id_)
                    tmpMp.Add(ptoMpTmp);
            }
            return tmpMp;
        }

        /// <summary>
        /// devolve uma lista de obj Ll
        /// </summary>
        /// <returns></returns>
        public List<Ll> LlList
        {
            set { pontosLL = value; }
            get { return pontosLL; }
        }

        /// <summary>
        /// devolve uma lista de obj Ll
        /// </summary>
        /// <param name="id_"></param>
        /// <returns></returns>
        public List<Ll> getLlList(String id_)
        {
            List<Ll> tmpLl = new List<Ll>();
            foreach (Ll ptoLlTmp in pontosLL)
            {
                if (ptoLlTmp.ID == id_)
                    tmpLl.Add(ptoLlTmp);
            }
            return tmpLl;
        }

        /// <summary>
        /// devolve uma lista de obj Ret
        /// </summary>
        /// <returns></returns>
        public List<Ret> RetList
        {
            set { pontosRET = value; }
            get { return pontosRET; }
        }

        /// <summary>
        /// devolve uma lista de obj Ret
        /// </summary>
        /// <param name="id_"></param>
        /// <returns></returns>
        public List<Ret> getRetList(String id_)
        {
            List<Ret> tmpRet = new List<Ret>();
            foreach (Ret ptoRetTmp in pontosRET)
            {
                if (ptoRetTmp.ID == id_)
                    tmpRet.Add(ptoRetTmp);
            }
            return tmpRet;
        }

        public Double EastNorthingListMaxValeu_N()
        {
            if (EastingNorthingList.Count > 0)
            {
                Double max = EastingNorthingList[0].N;
                foreach (EastingNorthing enzTmp in EastingNorthingList)
                    max = max >= enzTmp.N ? max : enzTmp.N;
                return max;
            }
            else
                return -1;
        }

        public Double EastNorthingListMaxValeu_E()
        {
            if (EastingNorthingList.Count > 0)
            {
                Double max = EastingNorthingList[0].E;
                foreach (EastingNorthing enzTmp in EastingNorthingList)
                    max = max >= enzTmp.E ? max : enzTmp.E;
                return max;
            }
            else
                return -1;
        }

        public Double EastNorthingListMinValeu_N()
        {
            if (EastingNorthingList.Count > 0)
            {
                Double min = EastingNorthingList[0].N;
                foreach (EastingNorthing enzTmp in EastingNorthingList)
                    min = min <= enzTmp.N ? min : enzTmp.N;
                return min;
            }
            else
                return -1;
        }

        public Double EastNorthingListMinValeu_E()
        {
            if (EastingNorthingList.Count > 0)
            {
                Double min = EastingNorthingList[0].E;
                foreach (EastingNorthing enzTmp in EastingNorthingList)
                    min = min <= enzTmp.E ? min : enzTmp.E;
                return min;
            }
            else
                return -1;
        }

        /// <summary>
        /// ordenar a lista de obj Height por ID
        /// </summary>
        public void sortHeight_By_ID()
        {   
            pontosHeight.Sort();
        }

        /// <summary>
        /// ordenar a lista de obj Height pela componente H
        /// </summary>
        public void sortHeight_By_H()
        {
            IComparer<Height> cotaCompare = new CompareHeight();
            pontosHeight.Sort(cotaCompare);
        }

        /// <summary>
        /// ordenar a lista de obj EastingNorthing por ID
        /// </summary>
        public void sortMp_By_ID()
        {
            pontosEN.Sort();
        }

        /// <summary>
        /// ordenar a lista de obj EastingNorthing pela componente M
        /// </summary>
        public void sortEN_By_E()
        {
            IComparer<EastingNorthing> mCompare = new CompareE();
            pontosEN.Sort(mCompare);
        }

        /// <summary>
        /// ordenar a lista de obj EastingNorthing pela componente P
        /// </summary>
        public void sortEN_By_N()
        {
            IComparer<EastingNorthing> pCompare = new CompareN();
            pontosEN.Sort(pCompare);
        }

        /// <summary>
        /// ordenar a lista de obj EastingNorthing pela componente H
        /// </summary>
        public void sortEN_By_H()
        {
            IComparer<EastingNorthing> hCompare = new CompareH();
            pontosEN.Sort(hCompare);
        }

        /// <summary>
        /// ordenar a lista de obj Ll por ID
        /// </summary>
        public void sortLL_By_ID()
        {
            pontosLL.Sort();
        }

        /// <summary>
        /// ordenar a lista de obj Ll pela componente latitude
        /// </summary>
        public void sortLL_By_Lat()
        {
            IComparer<Ll> latCompare = new CompareLat();
            pontosLL.Sort(latCompare);
        }

        /// <summary>
        /// ordenar a lista de obj Ll pela componente longitude
        /// </summary>
        public void sortLL_By_Long()
        {
            IComparer<Ll> longCompare = new CompareLog();
            pontosLL.Sort(longCompare);
        }

        /// <summary>
        /// ordenar a lista de obj Ll pela componente altitude
        /// </summary>
        public void sortLL_By_h()
        {
            IComparer<Ll> hCompare = new Compareh();
            pontosLL.Sort(hCompare);
        }

        /// <summary>
        /// ordenar a lista de obj Ret por ID
        /// </summary>
        public void sortRet_By_ID()
        {
            pontosRET.Sort();
        }

        /// <summary>
        /// ordenar a lista de obj Ret pela componente X
        /// </summary>
        public void sortRet_By_X()
        {
            IComparer<Ret> xCompare = new CompareX();
            pontosRET.Sort(xCompare);
        }

        /// <summary>
        /// ordenar a lista de obj Ret pela componente Y
        /// </summary>
        public void sortRet_By_Y()
        {
            IComparer<Ret> yCompare = new CompareY();
            pontosRET.Sort(yCompare);
        }

        /// <summary>
        /// ordenar a lista de obj Ret pela componente Z
        /// </summary>
        public void sortRet_By_Z()
        {
            IComparer<Ret> zCompare = new CompareZ();
            pontosRET.Sort(zCompare);
        }
    }    
}
