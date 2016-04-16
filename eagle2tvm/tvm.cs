using System;
using System.IO;
using System.ComponentModel;

namespace eagle2tvm
{
    class tvm
    {
        public String ttailtext = "";
        public String btailtext = "";

        // extension: *_tvm802_top(bottom).csv
        public void generateTVMfilenames(String s)
        {
            // entferne Pfad
            int idx = s.LastIndexOf('\\');
            if (idx != -1)
            {
                try
                {
                    s = s.Substring(idx + 1);
                }
                catch { }
            }

            // behandle bereits existierende Files mit anderen Namensgebungen
            // wenn das File existiert so wie es angegeben ist, nehme dieses
            if(File.Exists(info.tvmDir + '/' + s) && !s.Contains("_tvm802_bottom.csv") && !s.Contains("_tvm802_top.csv"))
            {
                if(s.ToUpper().Contains("TOP"))
                {
                    info.ttvmfile = s;
                    idx = s.LastIndexOf(".csv");
                    if(idx != -1) s = s.Substring(0, idx); 
                    info.btvmfile = s + "_tvm802_bottom.csv";
                }
                else
                {
                    info.btvmfile = s;
                    idx = s.LastIndexOf(".csv");
                    if (idx != -1) s = s.Substring(0, idx);
                    info.ttvmfile = s + "_tvm802_top.csv";
                }
                return;
            }

            // behandle Namensgebungen wie in diesem Programm üblich
            // entferne Extension
            idx = s.LastIndexOf("_tvm802_top.csv");
            if(idx == -1)
                idx = s.LastIndexOf("_tvm802_bottom.csv");
            if (idx == -1)
                idx = s.LastIndexOf(".csv");

            if(idx != -1)
            {
                // entferne eine alte extension sodass der Dateiname allein übrig bleibt
                s = s.Substring(0, idx);
            }

            // wir haben jetzt den reinen Namen, für die extensions hinzu
            info.ttvmfile = s + "_tvm802_top.csv";
            info.btvmfile = s + "_tvm802_bottom.csv";
        }

        public void WriteCSV(eagle egl, bool all=true)
        {
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(info.tvmDir + "/" + info.ttvmfile))
                {
                    Header(sw);
                    Devices(sw,egl.tdevlist,all);
                    WriteTail(sw, "top");
                }
            }
            catch
            {

            }

            sw = null;
            try
            {
                using (sw = new StreamWriter(info.tvmDir + "/" + info.btvmfile))
                {
                    Header(sw);
                    Devices(sw, egl.bdevlist,all);
                    sw.WriteLine(" ");
                    WriteTail(sw, "bottom");
                }
            }
            catch
            {

            }

        }

        void Header(StreamWriter sw)
        {
            String s =
@"""Designator"",""NozzleNum"",""StackNum"",""Mid X"",""Mid Y"",""Rotation"",""Height"",""Speed"",""Vision"",""Pressure"",""Explanation""
""""";
            sw.WriteLine(s);
        }

        void Devices(StreamWriter sw, BindingList<device> lst, bool all)
        {
            foreach (device dev in lst)
            {
                if (all == false && dev.stackname.Contains("???")) continue;
                // prüfe ob Stack eine zusätzliche Rotation erfordert
                double newrot = dev.rot;
                foreach(stackitem si in info.stacklist)
                {
                    if(si.stackname == dev.stackname)
                    {
                        newrot = (dev.rot + si.rot) % 360;
                        break;
                    }
                }

                // ziehe 180 Grad ab für das CSV File
                newrot = (360 + (newrot - 180)) % 360;

                // beim Tray zusätzlich 90 Grad abziehen
                if(dev.stackname.Substring(0,1).ToUpper() == "I")
                    newrot = (360 + (newrot - 90)) % 360;

                String s = "\"" + dev.location.Replace(",", ".") + "\",";
                s += "\"" + dev.nozzle.ToString().Replace(",", ".") + "\",";
                s += "\"" + dev.stackname.Replace(",", ".") + "\",";
                s += "\"" + dev.x.ToString("0.00").Replace(",", ".") + "\",";
                s += "\"" + dev.y.ToString("0.00").Replace(",", ".") + "\",";
                s += "\"" + newrot.ToString("0.00").Replace(",", ".") + "\",";
                s += "\"" + dev.height.ToString("0.00").Replace(",", ".") + "\",";
                s += "\"" + dev.speed.ToString().Replace(",", ".") + "\",";
                s += "\"" + dev.vision.Replace(",", ".") + "\",";
                if (dev.pressure)
                    s += "\"True\",";
                else
                    s += "\"False\",";
                s += "\"" + dev.name.Replace(",", ".") + " (" + dev.footprint.Replace(",", ".") + ") " + "{" + dev.rot.ToString("0.00").Replace(",", ".") + "}\"";

                sw.WriteLine(s);
            }
        }

        void WriteTail(StreamWriter sw, String side)
        {
            String s = ttailtext;
            if (side == "bottom")
                s = btailtext;

            if (s == null || s.Length < 1)
                s = MakeDefaultTail();

            sw.Write(s);
        }

        String MakeDefaultTail()
        {
            return info.sDefaultTail;
        }

        public void LoadCSV(eagle egl)
        {
            StreamReader sr = null;
            try
            {
                using (sr = new StreamReader(info.tvmDir + "/" + info.ttvmfile))
                {
                    egl.tdevlist.Clear();
                    while (true)
                    {
                        String s = sr.ReadLine();
                        if (s == null) break;
                        if (s == "\"\"")
                        {
                            // ab der nächsten Zeile beginnen die Bauteildaten
                            readDevices(sr,egl.tdevlist);
                            readTail(sr, "top");
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine(egl.ToString());
            }

            sr = null;
            try
            {
                using (sr = new StreamReader(info.tvmDir + "/" + info.btvmfile))
                {
                    egl.bdevlist.Clear();
                    while (true)
                    {
                        String s = sr.ReadLine();
                        if (s == null) break;
                        if (s == "\"\"")
                        {
                            // ab der nächsten Zeile beginnen die Bauteildaten
                            readDevices(sr, egl.bdevlist);
                            readTail(sr, "bottom");
                        }
                    }
                }
            }
            catch
            {

            }

            // Suche im Bottom Layer den Pad der links am weitesten außen liegt,
            // das war der welcher beim Eagle Import als Spiegel diente
            double left = 1000000;
            // suche den rechtesten Punkt
            foreach (device dev in egl.bdevlist)
            {
                if (dev.x < left)
                {
                    left = dev.x;
                    info.rightmostdevice = "Mitte von " + dev.location + " ist X=0";
                }
            }
        }

        void readDevices(StreamReader sr, BindingList<device> lst)
        {
            while(true)
            {
                String s = sr.ReadLine();
                if (s == null) return;

                String[] sa = s.Split(new char[] { ',' });
                if (sa.Length != 11)
                    return;
                // "Designator","NozzleNum","StackNum","Mid X","Mid Y","Rotation","Height","Speed","Vision","Pressure","Explanation"
                //String loc, String xi, String yi, String r, String n, String f, 
                //int noz = 1, String sn = "L1", double h = 0.5, int spd = 50, String vis = "None", bool prs = true)
                for (int i = 0; i < sa.Length; i++) sa[i] = sa[i].Trim(new char[] { '\"' });
                // Lese footprint, der hinter dem Devicenamen in runden Klammern steht
                String[] sn = sa[10].Split(new char[] { '(', ')' });
                String name = "";
                String foot = "";
                if (sn.Length >= 2)
                {
                    name = sn[0];
                    foot = sn[1];
                }
                // Lese die originale, unkorrigierte, Rotation, welche hinten in geschweiften Klammern steht
                String[] srotarr = sa[10].Split(new char[] { '{', '}' });
                String srot = "0";
                if (srotarr.Length >= 2)
                {
                    srot = srotarr[1];
                }
                else
                {
                    // noch keine Orig-Rotation Angabe vorhanden
                    srot = sa[5];
                }
                device dev = new device(sa[0], sa[3], sa[4], srot, name, foot, sa[1], sa[2], sa[6], sa[7], sa[8], sa[9]);
                lst.Add(dev);
            }
        }

        void readTail(StreamReader sr, String side)
        {
            String s = sr.ReadToEnd();
            if (side == "top")
                ttailtext = s;
            else
                btailtext = s;
        }
    }
}
