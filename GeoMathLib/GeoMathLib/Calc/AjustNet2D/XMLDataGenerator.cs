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
    public class XmlDataGenerator
    {
        public XmlDataGenerator(string coo, string dist, string dir, string dirUnit, char separator, string fileOut)
        {
            Txt2Xml(coo, dist, dir, dirUnit, separator, fileOut);
        }

        public static void Txt2Xml(string pts, string dist, string dir, string dirUnit, char separator, string fileOut)
        {
            XmlDocument xmldoc;
            XmlNode xmlnode;
            XmlElement xmlElemRoot, xmlElemTrigpoints, xmlElemDistances, xmlElemDirections;

            xmldoc = new XmlDocument();
            //let's add the XML declaration section
            xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);

            //let's add the root element
            xmlElemRoot = xmldoc.CreateElement("", "data_radon", "");
            xmldoc.AppendChild(xmlElemRoot);

            xmlElemTrigpoints = xmldoc.CreateElement("", "trigpoints", "");
            xmlElemRoot.AppendChild(xmlElemTrigpoints);
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(pts))
                {
                    String line;
                    string[] result;
                    char[] charSeparators = new char[] { separator };

                    XmlElement xmlPoint, xmlId, xmlEasting, xmlNorthing;
                    XmlText xmlTextId, xmlTextEasting, xmlTextNorthing;

                    for (int i = 0; (line = sr.ReadLine()) != null; i++)
                    {
                        result = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                        //let's add the root element
                        xmlPoint = xmldoc.CreateElement("", "point", "");

                        xmlId = xmldoc.CreateElement("", "id", "");
                        xmlTextId = xmldoc.CreateTextNode(result[0]);
                        xmlId.AppendChild(xmlTextId);

                        xmlEasting = xmldoc.CreateElement("", "easting", "");
                        xmlTextEasting = xmldoc.CreateTextNode(result[1]);
                        xmlEasting.AppendChild(xmlTextEasting);

                        xmlNorthing = xmldoc.CreateElement("", "northing", "");
                        xmlTextNorthing = xmldoc.CreateTextNode(result[2]);
                        xmlNorthing.AppendChild(xmlTextNorthing);

                        xmlPoint.AppendChild(xmlId);
                        xmlPoint.AppendChild(xmlEasting);
                        xmlPoint.AppendChild(xmlNorthing);

                        xmlElemTrigpoints.AppendChild(xmlPoint);

                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            xmlElemDistances = xmldoc.CreateElement("", "distances", "");
            xmlElemRoot.AppendChild(xmlElemDistances);
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(dist))
                {
                    String line;
                    string[] result;
                    char[] charSeparators = new char[] { separator };

                    XmlElement xmlStation = null, xmlStationId, xmlStationObserved;
                    XmlElement xmlStationObservedId, xmlStationObservedReading;

                    XmlText xmlTextId, xmlTextReading;

                    string ocAnt = null;
                    for (int i = 0; (line = sr.ReadLine()) != null; i++)
                    {

                        result = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                        if (result[0] != ocAnt)
                        {
                            xmlStation = xmldoc.CreateElement("", "station", "");
                            xmlStationId = xmldoc.CreateElement("", "id", "");

                            xmlTextId = xmldoc.CreateTextNode(result[0]);
                            xmlStationId.AppendChild(xmlTextId);

                            xmlStation.AppendChild(xmlStationId);
                            xmlElemDistances.AppendChild(xmlStation);
                        }

                        //let's add the root element
                        xmlStationObserved = xmldoc.CreateElement("", "observed", "");

                        xmlStationId = xmldoc.CreateElement("", "id", "");
                        xmlTextId = xmldoc.CreateTextNode(result[1]);
                        xmlStationId.AppendChild(xmlTextId);

                        xmlStationObservedReading = xmldoc.CreateElement("", "reading", "");
                        xmlTextReading = xmldoc.CreateTextNode(result[2]);
                        xmlStationObservedReading.AppendChild(xmlTextReading);

                        xmlStationObserved.AppendChild(xmlStationId);
                        xmlStationObserved.AppendChild(xmlStationObservedReading);

                        xmlStation.AppendChild(xmlStationObserved);

                        ocAnt = result[0];

                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            xmlElemDirections = xmldoc.CreateElement("", "directions", "");
            XmlAttribute xmlDirUnit = xmldoc.CreateAttribute("unit");
            xmlElemDirections.SetAttributeNode(xmlDirUnit);
            xmlElemDirections.SetAttribute("unit", dirUnit);
            xmlElemRoot.AppendChild(xmlElemDirections);

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(dir))
                {
                    String line;
                    string[] result;
                    char[] charSeparators = new char[] { separator };

                    XmlElement xmlStation = null, xmlStationId, xmlStationObserved;
                    XmlElement xmlStationObservedId, xmlStationObservedReading;

                    XmlText xmlTextId, xmlTextReading;

                    string ocAnt = null;
                    for (int i = 0; (line = sr.ReadLine()) != null; i++)
                    {

                        result = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                        if (result[0] != ocAnt)
                        {
                            xmlStation = xmldoc.CreateElement("", "station", "");
                            xmlStationId = xmldoc.CreateElement("", "id", "");

                            xmlTextId = xmldoc.CreateTextNode(result[0]);
                            xmlStationId.AppendChild(xmlTextId);

                            xmlStation.AppendChild(xmlStationId);
                            xmlElemDirections.AppendChild(xmlStation);
                        }

                        //let's add the root element
                        xmlStationObserved = xmldoc.CreateElement("", "observed", "");

                        xmlStationId = xmldoc.CreateElement("", "id", "");
                        xmlTextId = xmldoc.CreateTextNode(result[1]);
                        xmlStationId.AppendChild(xmlTextId);

                        xmlStationObservedReading = xmldoc.CreateElement("", "reading", "");
                        if (dirUnit == "dms")
                        {
                            XmlElement d, m, s;
                            XmlText dText, mText, sText;

                            d = xmldoc.CreateElement("", "d", "");
                            m = xmldoc.CreateElement("", "m", "");
                            s = xmldoc.CreateElement("", "s", "");

                            dText = xmldoc.CreateTextNode(result[2]);
                            mText = xmldoc.CreateTextNode(result[3]);
                            sText = xmldoc.CreateTextNode(result[4]);

                            d.AppendChild(dText);
                            m.AppendChild(mText);
                            s.AppendChild(sText);

                            xmlStationObservedReading.AppendChild(d);
                            xmlStationObservedReading.AppendChild(m);
                            xmlStationObservedReading.AppendChild(s);
                        }
                        else
                        {
                            xmlTextReading = xmldoc.CreateTextNode(result[2]);
                            xmlStationObservedReading.AppendChild(xmlTextReading);
                        }

                        xmlStationObserved.AppendChild(xmlStationId);
                        xmlStationObserved.AppendChild(xmlStationObservedReading);

                        xmlStation.AppendChild(xmlStationObserved);

                        ocAnt = result[0];

                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            //let's try to save the XML document in a file: res.xml
            try
            {
                xmldoc.Save(fileOut);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
