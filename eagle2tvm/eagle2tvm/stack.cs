using System;
using System.IO;

namespace eagle2tvm
{
    class stack
    {
        public void SaveStack(String stackfile)
        {
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(stackfile))
                {
                    foreach (stackitem si in info.stacklist)
                    {
                        si.Save(sw);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void LoadStack(String stackfile)
        {
            StreamReader sr = null;
            try
            {
                info.stacklist.Clear();
                using (sr = new StreamReader(stackfile))
                {
                    while (true)
                    {
                        stackitem si = new stackitem();
                        bool ret = si.Load(sr);
                        if (!ret) break;
                        info.stacklist.Add(si);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

    public class stackitem
    {
        public String stackname { get; set; }
        public String name { get; set; }
        public String footprint { get; set; }
        public int rot { get; set; }
        public String nozzle { get; set; }
        public double height { get; set; }
        public double dimx { get; set; }
        public double dimy { get; set; }
        public double maxerror { get; set; }
        public int treshhold { get; set; }
        public int speed { get; set; }
        public String vision { get; set; }
        public bool pressure { get; set; }

        public void Save(StreamWriter sw)
        {
            sw.WriteLine(stackname + "§" + name + "§" + footprint + "§" + rot.ToString() + "§" + nozzle.ToString() + "§" + height.ToString() + "§" + vision + "§" + speed.ToString() + "§" + pressure.ToString() + "§" + dimx.ToString() + "§" + dimy.ToString() + "§" + maxerror.ToString()+ "§" + treshhold.ToString());
        }

        public bool Load(StreamReader sr)
        {
            String s = sr.ReadLine();
            if (s == null) return false;
            try
            {
                String[] sa = s.Split(new char[] { '§' });
                stackname = sa[0].ToUpper();
                name = sa[1];
                footprint = sa[2];
                rot = info.MyToInt32(sa[3]);
                nozzle = sa[4];
                height = info.MyToDouble(sa[5]);
                vision = sa[6];
                try
                {
                    speed = info.MyToInt32(sa[7]);
                }
                catch
                {
                    speed = 50;
                }
                try
                {
                    pressure = info.MyToBool(sa[8]);
                }
                catch
                {
                    pressure = true;
                }
                try
                {
                    dimx = info.MyToDouble(sa[9]);
                }
                catch
                {
                    dimx = 1.6; // 0603 size component as default value
                }
                try
                {
                    dimy = info.MyToDouble(sa[10]);
                }
                catch
                {
                    dimy = 0.8;
                }
                try
                {
                    maxerror = info.MyToDouble(sa[11]);
                }
                catch
                {
                    maxerror = 0.1; // 10%
                }
                try
                {
                    treshhold = info.MyToInt32(sa[12]);
                }
                catch
                {
                    treshhold = 60;
                }
            }
            catch
            {
                nozzle = "1";
                height = 0.5;
                vision = "None";
            }

            return true;
        }
    }
}
