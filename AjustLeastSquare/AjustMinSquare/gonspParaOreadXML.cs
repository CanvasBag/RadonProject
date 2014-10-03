            //direccoes
            XmlNodeList directions = doc.GetElementsByTagName("directions");

            foreach (XmlNode direction in directions)
            {
                int iStation = -1;
                
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
                    else if (direction.Attributes["unit"].InnerText == "gradian")
                    {
                        unit = 3;
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
                            iStation++;
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
                                    reading = Double.Parse(element.ChildNodes[1].FirstChild.InnerText) * SIUnits.Deg2Rad;
                                    break;
                                case 2:
                                    reading = SIUnits.Degree(new Tuple<int, int, double>(int.Parse(element.ChildNodes[1].ChildNodes[0].InnerText), int.Parse(element.ChildNodes[1].ChildNodes[1].InnerText), Double.Parse(element.ChildNodes[1].ChildNodes[2].InnerText))) * SIUnits.Deg2Rad;
                                    break;
                                case 3:
                                    reading = Double.Parse(element.ChildNodes[1].FirstChild.InnerText) * SIUnits.Gon2Rad;
                                    break;
                            }

                            obsListDir.Add(new ReadStationDir(occupied, observed, iStation, reading));

                        }
                        //Console.WriteLine("<{0}><{1}><{2}>\n", observation.ChildNodes[0].InnerText, observation.ChildNodes[1].InnerText, observation.ChildNodes[2].InnerText);


                    }
                }
                nObsDir = obsListDir.Count;
            }