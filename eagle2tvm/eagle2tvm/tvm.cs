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
                if (egl.tdevlist.Count > 0)
                { 
                    using (sw = new StreamWriter(info.tvmDir + "/" + info.ttvmfile))
                    {
                        Header(sw);
                        Devices(sw, egl.tdevlist, all);
                        WriteTail(sw, "top");
                    }
                }
            }
            catch
            {

            }

            sw = null;
            try
            {
                if (egl.bdevlist.Count > 0)
                {
                    using (sw = new StreamWriter(info.tvmDir + "/" + info.btvmfile))
                    {
                        Header(sw);
                        Devices(sw, egl.bdevlist, all);
                        sw.WriteLine(" ");
                        WriteTail(sw, "bottom");
                    }
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
            int cnt = 0;
            String s1 = "";
            String s2 = "";
            String s3 = "";
            String s4 = "";
            String s5 = "";
            String s6 = "";
            String s7 = "";
            String s8 = "";
            if (side == "top")
            {
                foreach(fiducialitem fi in info.tfiducialslist)
                {
                    s1 += fi.real1x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s2 += fi.real1y.ToString("0.00").Replace(',', '.') + "\r\n";
                    s3 += fi.mark1x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s4 += fi.mark1y.ToString("0.00").Replace(',', '.') + "\r\n";
                    s5 += fi.real2x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s6 += fi.real2y.ToString("0.00").Replace(',', '.') + "\r\n";
                    s7 += fi.mark2x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s8 += fi.mark2y.ToString("0.00").Replace(',', '.') + "\r\n";
                    cnt++;
                }
            }
            else
            {
                foreach (fiducialitem fi in info.bfiducialslist)
                {
                    s1 += fi.real1x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s2 += fi.real1y.ToString("0.00").Replace(',', '.') + "\r\n";
                    s3 += fi.mark1x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s4 += fi.mark1y.ToString("0.00").Replace(',', '.') + "\r\n";
                    s5 += fi.real2x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s6 += fi.real2y.ToString("0.00").Replace(',', '.') + "\r\n";
                    s7 += fi.mark2x.ToString("0.00").Replace(',', '.') + "\r\n";
                    s8 += fi.mark2y.ToString("0.00").Replace(',', '.') + "\r\n";
                    cnt++;
                }
            }

            for (int i = cnt; i < 50; i++)
            {
                s1 += "0.00\r\n";
                s2 += "0.00\r\n";
                s3 += "0.00\r\n";
                s4 += "0.00\r\n";
                s5 += "0.00\r\n";
                s6 += "0.00\r\n";
                s7 += "0.00\r\n";
                s8 += "0.00\r\n";
            }
            s1 += "\r\n";
            s2 += "\r\n";
            s3 += "\r\n";
            s4 += "\r\n";
            s5 += "\r\n";
            s6 += "\r\n";
            s7 += "\r\n";
            s8 += "\r\n";

            sw.Write("\r\n\r\n\r\nPuzzle\r\n");
            sw.Write(s1);
            sw.Write(s2);
            sw.Write(s3);
            sw.Write(s4);
            sw.Write(s5);
            sw.Write(s6);
            sw.Write(s7);
            sw.Write(s8);

            // schreibe den Rest
            String dd = info.platinendicke.ToString().Replace(',', '.');

            sw.Write("Other\r\n" + dd + "\r\n\r\n");
            // jetzt wird angegeben welche Fids aktiv sind
            cnt = 0;
            if (side == "top")
            {
                foreach (fiducialitem fi in info.tfiducialslist)
                {
                    if(fi.real1x != 0 && fi.real1y != 0)
                        sw.Write("True\r\n");
                    else
                        sw.Write("False\r\n");
                    cnt++;
                }
            }
            else
            {
                foreach (fiducialitem fi in info.bfiducialslist)
                {
                    if (fi.real1x != 0 && fi.real1y != 0)
                        sw.Write("True\r\n");
                    else
                        sw.Write("False\r\n");
                    cnt++;
                }
            }
            for (int i = cnt; i < 50; i++)
            {
                sw.Write("False\r\n");
            }
            sw.Write("\r\n");

            cnt = 0;
            if (side == "top")
            {
                foreach (fiducialitem fi in info.tfiducialslist)
                {
                    if (fi.real2x != 0 && fi.real2y != 0)
                        sw.Write("True\r\n");
                    else
                        sw.Write("False\r\n");
                    cnt++;
                }
            }
            else
            {
                foreach (fiducialitem fi in info.bfiducialslist)
                {
                    if (fi.real2x != 0 && fi.real2y != 0)
                        sw.Write("True\r\n");
                    else
                        sw.Write("False\r\n");
                    cnt++;
                }
            }
            for (int i = cnt; i < 50; i++)
            {
                sw.Write("False\r\n");
            }
            sw.Write("\r\n");

            sw.Write("Mark\r\n");
            sw.Write("2\r\n");
            sw.Write("0\r\n");
            sw.Write("True\r\n");
            if(side == "top")
                sw.Write(info.exposure_top.ToString() + "\r\n");
            else
                sw.Write(info.exposure_bottom.ToString() + "\r\n");
        }


        public void LoadCSV(eagle egl)
        {
            egl.bdevlist.Clear();
            egl.tdevlist.Clear();
            StreamReader sr = null;
            try
            {
                using (sr = new StreamReader(info.tvmDir + "/" + info.ttvmfile))
                {
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

            // zerteile den Tail in die einzelnen Zeilen
            String[] sa = s.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int idx = 0;
            foreach(String sx in sa)
            {
                if(sx.Contains("Puzzle"))
                {
                    // Start gefunden
                    // Lese die Werte ein
                    try
                    {
                        double[,] tailinfo = new double[8, 50];
                        idx++;
                        for(int x=0; x<8; x++)
                        {
                            for(int i=0; i<50; i++)
                            {
                                tailinfo[x, i] = info.MyToDouble(sa[idx++]);
                            }
                        }

                        if (side == "top")
                            info.tfiducialslist.Clear();
                        else
                            info.bfiducialslist.Clear();
                        for (int f = 0; f < 50; f++)
                        {
                            fiducialitem fi = new fiducialitem();
                            fi.real1x = tailinfo[0, f];
                            fi.real1y = tailinfo[1, f];
                            fi.mark1x = tailinfo[2, f];
                            fi.mark1y = tailinfo[3, f];
                            fi.real2x = tailinfo[4, f];
                            fi.real2y = tailinfo[5, f];
                            fi.mark2x = tailinfo[6, f];
                            fi.mark2y = tailinfo[7, f];
                            if (side == "top")
                                info.tfiducialslist.Add(fi);
                            else
                                info.bfiducialslist.Add(fi);
                        }
                        break;

                    }
                    catch
                    {
                        return;
                    }
                }
                idx++;
            }
            idx = 0;
            foreach (String sx in sa)
            {
                if (sx.Contains("Other"))
                {
                    info.platinendicke = info.MyToDouble(sa[idx + 1]);
                    if (side == "top")
                    {
                        info.exposure_top = info.MyToInt32(sa[idx + 106]);
                    }
                    else
                    {
                        info.exposure_bottom = info.MyToInt32(sa[idx + 106]);
                    }
                    break;
                }
                idx++;
            }
        }
    }

    public class fiducialitem
    {
        public double mark1x { get; set; }
        public double mark1y { get; set; }
        public double real1x { get; set; }
        public double real1y { get; set; }
        public double mark2x { get; set; }
        public double mark2y { get; set; }
        public double real2x { get; set; }
        public double real2y { get; set; }

        public fiducialitem()
        {
            mark1x = mark1y = real1x = real1y = mark2x = mark2y = real2x = real2y = 0.00;
        }
    }
}
