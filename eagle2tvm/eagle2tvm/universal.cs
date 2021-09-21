using System;
using System.IO;
using System.ComponentModel;

namespace eagle2tvm
{
    class universal_importer
    {
        public SortableBindingList<device> tdevlist = new SortableBindingList<device>();
        public SortableBindingList<device> bdevlist = new SortableBindingList<device>();
        public Int32 units;

        bool isInList(device dev, BindingList<device>lst)
        {
            foreach(device d in lst)
            {
                if (dev.location == d.location)
                    return true;
            }
            return false;
        }

        public int Load()
        {
            String filename = info.LastDir + "//" + info.LastFile;
            String ext = info.LastFile.ToLower();
            char delimeter;
            ext=ext.Substring(ext.Length-3);
            if ((ext=="csv")||(ext=="pnp"))
                delimeter=',';
            else
                delimeter=' ';
            
            // Lade TOP layer
            StreamReader sr = null;
            bool tfidfound = false;
            bool bfidfound = false;
            try
            {
                using (sr = new StreamReader(filename))
                {
                    tdevlist.Clear();
                    bdevlist.Clear();
                    info.bfiducialslist.Clear();
                    info.tfiducialslist.Clear();
                    fiducialitem tfi = new fiducialitem();
                    fiducialitem bfi = new fiducialitem();
                    Int32 dataorder = 0;
                    Int32 fieldsnumber = 8;
                    Int32 side_index = 2;
                    // data order:
                    // 0 Altium/Circuit Maker - new format (8 fields)
                    // Designator,Comment,Layer,Footprint,Center-X,Center-Y,Rotation,Description
                    // 1 Altium or Protel 98/99/99SE/DXP - older format (11 fields)
                    // Desgnator,Footprint,Mid X,Mid Y,Ref X,Ref Y,Pad X,Pad Y,TB,Rotation,Comment
                    // 2 Orcad/Allegro pnp file (7 fields)
                    // RefDes,Layer,LocationX,LocationY,Rotation,PatternName,Value
                    // 3 KiCad (7 fields)
                    // Ref,Val,Package,PosX,PosY,Rot,Side
                    // this is very naive way of doing this, but due to limited time i have, and need to reuse as much code as i could, it is what is is
                    while (true)
                    {
                        String header = sr.ReadLine();
                        if (header == null) break;
                        String[] header_fields = header.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (header_fields.Length > 1)
                        {
                            if (header_fields[0] == "Designator")
                            {
                                if (header_fields[1] == "Footprint") // Old Altium (17 and below) or Protel
                                {
                                    dataorder = 1;
                                    fieldsnumber = 11;
                                    side_index = 8;
                                    break;
                                }
                                else if (header_fields[1] == "Comment") // New Altium/Circuit Maker
                                {
                                    dataorder = 0;
                                    fieldsnumber = 8;
                                    side_index = 2;
                                    break;
                                }
                            }
                            else if (header_fields[0] == "RefDes") // OrCad
                            {
                                dataorder = 2;
                                fieldsnumber = 7;
                                side_index = 1;
                                break;
                            }
                            else if (header_fields[1] == "Ref") // KiCad
                            {
                                dataorder = 3;
                                fieldsnumber = 7;
                                side_index = 6;
                                break;
                            } else if (header_fields[0]=="Units")
                            {
                                if (header_fields[(header_fields.Length) - 1].ToLower() == "mm")
                                    units = 0;
                                else
                                    units = 1;
                            }
                        }
                    }
                    sr.DiscardBufferedData();
                    sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

                    while (true)
                    {
                        String s = sr.ReadLine();
                        if (s == null) break;
                        String[] sa = s.Split(new char[] { delimeter }, StringSplitOptions.RemoveEmptyEntries);
                        if (sa.Length == fieldsnumber)
                        {
                            // name x-coord y-coord rotation value package
                            if (((sa[0].ToLower()!="designator")&&(sa[0].ToLower()!="refdes"))&&(sa[0].Length<10) &&
                                ((sa[0]!="#")&&(sa[0]!="##")&&(sa[0]!="###")))
                            {
                                device dev = null;
                                    switch (dataorder)
                                    {
                                        case 0:
                                            dev = new device(sa[0], sa[4], sa[5], sa[6], sa[1], sa[3]);
                                            break;
                                        case 1:
                                            dev = new device(sa[0], sa[2], sa[3], sa[9], sa[10], sa[1]);
                                            if (sa[2].Contains("mil")) units = 1;   // set to imperial units
                                            break;
                                        case 2:
                                            dev = new device(sa[0], sa[2], sa[3], sa[4], sa[6], sa[5]);
                                            break;
                                        case 3:
                                            dev = new device(sa[0], sa[3], sa[4], sa[5], sa[1], sa[2]);
                                            break;
                                        default:
                                            break;
                                    }

                                if (dev.location == "FID1")
                                {
                                    tfi.mark1x = dev.x;
                                    tfi.mark1y = dev.y;
                                    tfidfound = true;
                                }
                                else if (dev.location == "FID2")
                                {
                                    tfi.mark2x = dev.x;
                                    tfi.mark2y = dev.y;
                                    tfidfound = true;
                                }
                                else if (dev.location == "FID3")
                                {
                                    bfi.mark1x = dev.x;
                                    bfi.mark1y = dev.y;
                                    bfidfound = true;
                                }
                                else if (dev.location == "FID4")
                                {
                                    bfi.mark2x = dev.x;
                                    bfi.mark2y = dev.y;
                                    bfidfound = true;
                                }
                                else if ((sa[side_index].ToLower() == "toplayer") || (sa[side_index].ToLower() == "top")||(sa[side_index].ToLower() == "t"))
                                    tdevlist.Add(dev);
                                else
                                    bdevlist.Add(dev);
                                if (tfidfound)
                                {
                                    // bei Nutzen müssen alle Real Koordinaten die gleichen sein
                                    // trage daher für alle 5 Einzelplatinen das gleiche ein
                                    // die Maschinenkoords müssen natürlich auf die jeweiligen FIDs eingemessen werden
                                    info.tfiducialslist.Add(tfi);
                                    info.tfiducialslist.Add(tfi);
                                    info.tfiducialslist.Add(tfi);
                                    info.tfiducialslist.Add(tfi);
                                    info.tfiducialslist.Add(tfi);
                                }
                                if (bfidfound)
                                {
                                    // bei Nutzen müssen alle Real Koordinaten die gleichen sein
                                    // trage daher für alle 5 Einzelplatinen das gleiche ein
                                    // die Maschinenkoords müssen natürlich auf die jeweiligen FIDs eingemessen werden
                                    info.bfiducialslist.Add(bfi);
                                    info.bfiducialslist.Add(bfi);
                                    info.bfiducialslist.Add(bfi);
                                    info.bfiducialslist.Add(bfi);
                                    info.bfiducialslist.Add(bfi);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            

      
            // Spiegle den Bottom Layer am Pad der rechts am weitesten außen liegt
            double right = -1000000;
            // suche den rechtesten Punkt
            foreach(device dev in bdevlist)
            {
                if (dev.x > right)
                {
                    right = dev.x;
                    info.rightmostdevice = "Mitte von " + dev.location + " ist X=0";
                }
            }

            // schau ob evt einer der FIDs noch weiter rechts außen liegt, wenn ja dann nehme diesen
            foreach(fiducialitem fi in info.bfiducialslist)
            {
                if (fi.mark1x > right) right = fi.mark1x;
            }
            foreach (fiducialitem fi in info.bfiducialslist)
            {
                if (fi.mark2x > right) right = fi.mark2x;
            }

            // Spiegle SMD Pads
            foreach (device dev in bdevlist)
            {
                dev.x = Math.Round(right - dev.x,3);
                // und spiegle auch die Rotation
                //dev.rot = (360-dev.rot) % 360; nicht erforderlich, da diese von Eagle bereits gespiegelt ist
            }

            // Spiegle Fiducials
            foreach (fiducialitem fi in info.bfiducialslist)
            {
                fi.mark1x = Math.Round(right - fi.mark1x, 3);
                fi.mark2x = Math.Round(right - fi.mark2x, 3);
            }
                return 0;
        }
        
        public void DeleteRow(BindingList<device> lst, int idx)
        {
            lst.RemoveAt(idx);
        }

        public int getTopNum()
        {
            return tdevlist.Count;
        }

        public int getBotNum()
        {
            return bdevlist.Count;
        }

    }
}
