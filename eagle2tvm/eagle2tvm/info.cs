using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace eagle2tvm
{
    public static class info
    {
        public static String LastDir = "";
        public static String LastFile = "";
        static String cfgfile = "eagle2pnp.cfg";
        public static String stackfile = "eagle2pnp.stk";
        public static String tvmDir = "";
        public static String ttvmfile = "";
        public static String btvmfile = "";
        public static SortableBindingList<stackitem> stacklist = new SortableBindingList<stackitem>();
        public static String error = "";
        public static String rightmostdevice = "";
        public static int ostype = 0;   // 1=Linux
        public static String cwd = "";
        public static int lang = 1; //0=en 1=de
        public static BindingList<fiducialitem> tfiducialslist = new BindingList<fiducialitem>();
        public static BindingList<fiducialitem> bfiducialslist = new BindingList<fiducialitem>();

        public static void Save()
        {
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(myFilePath(cfgfile)))
                {
                    sw.WriteLine(LastDir);
                    sw.WriteLine(LastFile);
                    sw.WriteLine(tvmDir);
                    sw.WriteLine(lang.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Load()
        {
            StreamReader sr = null;
            try
            {
                String fn = myFilePath(cfgfile);
                using (sr = new StreamReader(fn))
                {
                    LastDir = ReadString(sr);
                    LastFile = ReadString(sr);
                    tvmDir = ReadString(sr);
                    lang = Convert.ToInt32(ReadString(sr));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static String ReadString(StreamReader sr)
        {
            String s = sr.ReadLine();
            if (s != null)
            {
                return s;
            }
            return " ";
        }

        public static string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }

        public static string ByteArrayToString(byte[] arr, int offset, int len)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr, offset, len);
        }

        public static byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }

        public static void InsertStringToByteArray(Byte[] arr, int pos, int len, String s)
        {
            s = s.Trim().PadRight(len).Substring(0, len);
            int i = 0;
            foreach (char c in s)
            {
                if ((pos + i) >= arr.Length) return;
                arr[pos + i] = (Byte)c;
                i++;
            }
        }

        public static String myFilePath(String file)
        {
            if (info.ostype == 0)
            {
                // Windows
                return Application.UserAppDataPath + "\\" + file;
            }
            else
            {
                // Linux
                return cwd + "/" + file;
            }
        }

        // Culture invariant conversion
        public static double MyToDouble(String s)
        {
            double r = 0;

            try
            {
                s = s.Replace(',', '.');
                r = Convert.ToDouble(s, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
            }
            return r;
        }

        
        public static bool MyToBool(String s)
        {
            bool r = false;

            try
            {
                s = s.Replace(',', '.');
                r = Convert.ToBoolean(s);
            }
            catch
            {
            }
            return r;
        }

        public static int MyToInt32(String s)
        {
            int r = 0;

            try
            {
                r = Convert.ToInt32(s);
            }
            catch
            {
            }
            return r;
        }

        public static String sDefaultTail =
@"
Other
1.60

True
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False

True
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False
False

Mark
2
0
True
6
";
    }
}
