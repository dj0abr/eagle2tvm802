using System;
using System.IO;
using System.ComponentModel;

namespace eagle2tvm
{
    class eagle
    {
        public SortableBindingList<device> tdevlist = new SortableBindingList<device>();
        public SortableBindingList<device> bdevlist = new SortableBindingList<device>();

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
            // input data from Eagle mnt/mnb files is without any headers and is organised as:
            // designator x-coord y-coord rotation value footprint
            String tfilename = filename.Substring(0,filename.Length - 1) + "t";
            String bfilename = filename.Substring(0,filename.Length - 1) + "b";

            // Lade TOP layer
            StreamReader sr = null;
            bool fidfound = false;
            try
            {
                using (sr = new StreamReader(tfilename))
                {
                    tdevlist.Clear();
                    info.tfiducialslist.Clear();
                    fiducialitem tfi = new fiducialitem();
                    while (true)
                    {
                        String s = sr.ReadLine();
                        if (s == null) break;

                        String[] sa = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        device dev;
                        if (sa.Length < 6)
                            dev = new device(sa[0], sa[1], sa[2], sa[3], sa[4], "???");
                        else
                            dev = new device(sa[0], sa[1], sa[2], sa[3], sa[4], sa[5]);

                        if(dev.location == "FID1")
                        {
                            tfi.mark1x = dev.x;
                            tfi.mark1y = dev.y;
                            fidfound = true;
                        }
                        else if (dev.location == "FID2")
                        {
                            tfi.mark2x = dev.x;
                            tfi.mark2y = dev.y;
                            fidfound = true;
                        }
                        else
                            tdevlist.Add(dev);
                    }
                    if (fidfound)
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            

            // Lade Bottom Layer
            sr = null;
            fidfound = false;
            try
            {
                using (sr = new StreamReader(bfilename))
                {
                    bdevlist.Clear();
                    info.bfiducialslist.Clear();
                    fiducialitem bfi = new fiducialitem();
                    while (true)
                    {
                        String s = sr.ReadLine();
                        if (s == null) break;

                        String[] sa = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        device dev;
                        if (sa.Length < 6)
                            dev = new device(sa[0], sa[1], sa[2], sa[3], sa[4], "???");
                        else
                            dev = new device(sa[0], sa[1], sa[2], sa[3], sa[4], sa[5]);

                        if (dev.location == "FID3")
                        {
                            bfi.mark1x = dev.x;
                            bfi.mark1y = dev.y;
                            fidfound = true;
                        }
                        else if (dev.location == "FID4")
                        {
                            bfi.mark2x = dev.x;
                            bfi.mark2y = dev.y;
                            fidfound = true;
                        }
                        else
                            bdevlist.Add(dev);
                    }
                    if (fidfound)
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
                dev.x = Math.Round(right - dev.x,2);
                // und spiegle auch die Rotation
                //dev.rot = (360-dev.rot) % 360; nicht erforderlich, da diese von Eagle bereits gespiegelt ist
            }

            // Spiegle Fiducials
            foreach (fiducialitem fi in info.bfiducialslist)
            {
                fi.mark1x = Math.Round(right - fi.mark1x, 2);
                fi.mark2x = Math.Round(right - fi.mark2x, 2);
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

    // C7 32.86 41.59 270 1u C0603
    class device
    {
        public String location { get; set; }
        public String name { get; set; }
        public String footprint { get; set; }
        public String nozzle { get; set; }
        public String stackname { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double rot { get; set; }
        public double height { get; set; }
        public int speed { get; set; }
        public String vision { get; set; }
        public String check { get; set; }
        public bool pressure { get; set; }

        public device(String loc, String xi, String yi, String r, String n, String f,
                        String noz = "1", String sn = "L???", String h = "0.5", String spd = "50", String vis = "None", String prs = "True", String check = "Pressure")
        {
            location = loc.Trim();
            name = n.Trim();
            footprint = f.Trim();
            x = info.MyToDouble(xi);
            y = info.MyToDouble(yi);
            rot = info.MyToDouble(r);

            nozzle = noz;
            stackname = sn;
            height = info.MyToDouble(h);
            speed = info.MyToInt32(spd);
            vision = vis;
            if(prs.ToLower() == "true")
                pressure = true;
            else
                pressure = false;
        }

        public void Save(StreamWriter sw)
        {
            String line = location + " " + x.ToString() + " " + y.ToString() + " " + rot.ToString() + " " + name + " " + footprint;
            sw.WriteLine(line);
        }
    }
}
