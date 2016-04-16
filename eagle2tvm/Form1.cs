using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace eagle2tvm
{
    public partial class Form1 : Form
    {
        eagle egl;
        stack stk;
        tvm tvm802;

        splash spf = new splash();

        public Form1()
        {
            InitializeComponent();

            spf.Show();

            // test OS type
            OperatingSystem osversion = System.Environment.OSVersion;
            if (osversion.VersionString.Contains("indow"))
                info.ostype = 0;
            else
                info.ostype = 1;

            info.cwd = Directory.GetCurrentDirectory();

            egl = new eagle();
            stk = new stack();
            tvm802 = new tvm();
            info.Load();
            stk.LoadStack(info.myFilePath(info.stackfile));
            //tvm802.makeFilenames();

            tdataGridView_devices.DataSource = egl.tdevlist;
            bdataGridView_devices.DataSource = egl.bdevlist;
            dataGridView_stack.DataSource = info.stacklist;

            tdataGridView_devices.Columns[3].DefaultCellStyle.Format = "N2";
            tdataGridView_devices.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tdataGridView_devices.Columns[4].DefaultCellStyle.Format = "N2";
            tdataGridView_devices.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tdataGridView_devices.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            bdataGridView_devices.Columns[3].DefaultCellStyle.Format = "N2";
            bdataGridView_devices.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            bdataGridView_devices.Columns[4].DefaultCellStyle.Format = "N2";
            bdataGridView_devices.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            bdataGridView_devices.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // die Combobox wird nicht gebunden, weil sonst keine freie Definition der Auswahlmöglichkeiten besteht

            setGUI();   // default Sprache
        }

        private void button_loadeaglefiles_Click(object sender, EventArgs e)
        {
            openEagleFiles.InitialDirectory = info.LastDir;
            openEagleFiles.FileName = info.LastFile;
            DialogResult dr = openEagleFiles.ShowDialog();
            if(dr == DialogResult.OK)
            {
                info.LastFile = Path.GetFileName(openEagleFiles.FileName);
                info.LastDir = Path.GetDirectoryName(openEagleFiles.FileName);
                int ret = egl.Load();
                if (ret == 1)
                    MessageBox.Show(language.str(0) + info.error, language.str(1), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (ret == 2)
                    MessageBox.Show(language.str(0) + info.error, language.str(2), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            info.Save();
            stk.SaveStack(info.myFilePath(info.stackfile));
        }

        private void bdataGridView_devices_KeyDown(object sender, KeyEventArgs e)
        {
            // DEL Taste gedrückt ?
            if(e.KeyCode == Keys.Delete)
            {
                try
                {
                    int i = ((DataGridView)sender).SelectedCells[0].RowIndex;   // Index
                    // object s = ((DataGridView)sender).Rows[i].DataBoundItem; // Inhalt
                    egl.DeleteRow(egl.bdevlist, i);
                    ((DataGridView)sender).Refresh();
                }
                catch { }
            }
        }

        int splashtime = 3;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (splashtime > 0)
            {
                splashtime--;
                if(splashtime == 0)
                    spf.Close();
            }

            try
            {
                String s =
                    "Eagle " + language.str(3) + ": " + info.LastDir + "\r\n" +
                    "Eagle " + language.str(4) + ": TOP = " + info.LastFile.Substring(0, info.LastFile.Length - 1) + "t,  BOTTOM =  " + info.LastFile.Substring(0, info.LastFile.Length - 1) + "b\r\n" +
                    "TVM802 " + language.str(3) + ": " + info.tvmDir + "\r\n" +
                    "TVM802 " + language.str(4) + ": TOP = " + info.ttvmfile + "  BOTTOM = " + info.btvmfile;
                textBox_status.Text = s;

                s = language.str(5) + " TOP: " + egl.getTopNum() + "  BOTTOM: " + egl.getBotNum() + "\r\n\r\n";
                if (egl.bdevlist.Count > 0)
                {
                    s += "Eagle-BOTTOM " + language.str(6) + "\r\n" + info.rightmostdevice;
                }
                textBox_info.Text = s;
            }
            catch { }
        }

        private void button_makeTVMfiles_Click(object sender, EventArgs e)
        {
            saveFileDialog_tvm802.FileName = info.ttvmfile;
            saveFileDialog_tvm802.InitialDirectory = info.tvmDir;

            DialogResult dr = saveFileDialog_tvm802.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String fn = saveFileDialog_tvm802.FileName;
                int idx = fn.LastIndexOf('\\');
                if(idx > 0)
                {
                    try
                    {
                        tvm802.generateTVMfilenames(fn);
                        info.tvmDir = fn.Substring(0, idx);
                        tvm802.WriteCSV(egl);
                    }
                    catch { }
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog_tvm802.FileName = info.ttvmfile;
            saveFileDialog_tvm802.InitialDirectory = info.tvmDir;

            DialogResult dr = saveFileDialog_tvm802.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String fn = saveFileDialog_tvm802.FileName;
                int idx = fn.LastIndexOf('\\');
                if (idx > 0)
                {
                    try
                    {
                        tvm802.generateTVMfilenames(fn);
                        info.tvmDir = fn.Substring(0, idx);
                        tvm802.WriteCSV(egl,false);
                    }
                    catch { }
                }
            }
        }

        private void button_loadTVMfiles_Click(object sender, EventArgs e)
        {
            openTVMfiles.InitialDirectory = info.tvmDir;
            openTVMfiles.FileName = info.btvmfile;
            DialogResult dr = openTVMfiles.ShowDialog();
            if (dr == DialogResult.OK)
            {
                tvm802.generateTVMfilenames(Path.GetFileName(openTVMfiles.FileName));
                info.tvmDir = Path.GetDirectoryName(openTVMfiles.FileName);
                tvm802.LoadCSV(egl);
            }
        }

        // Versuche die Zuordnung von Stacks zur Bauteilliste
        private void button_bzuordnung_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(language.str(7), language.str(8), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                foreach (device dev in egl.bdevlist)
                {
                    bool doauto = false;
                    foreach (stackitem si in info.stacklist)
                    {
                        if (dev.name.ToUpper() == si.name.ToUpper() && dev.footprint.ToUpper().Contains(si.footprint.ToUpper())) doauto = true;

                        if (doauto)
                        {
                            dev.stackname = si.stackname.ToUpper();
                            dev.nozzle = si.nozzle;
                            dev.height = si.height;
                            dev.vision = si.vision;
                            break;
                        }
                    }
                }
                bdataGridView_devices.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(language.str(7), language.str(8), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                foreach (device dev in egl.tdevlist)
                {
                    foreach (stackitem si in info.stacklist)
                    {
                        bool doauto = false;
                        if (dev.name.ToUpper() == si.name.ToUpper() && dev.footprint.ToUpper().Contains(si.footprint.ToUpper())) doauto = true;

                        if (doauto)
                        {
                            dev.stackname = si.stackname.ToUpper();
                            dev.nozzle = si.nozzle;
                            dev.height = si.height;
                            dev.vision = si.vision;
                            break;
                        }
                    }
                }
                tdataGridView_devices.Refresh();
            }
        }

        private void bdataGridView_devices_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        private void button_loadstacklist_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog_stack.ShowDialog();
            if(dr == DialogResult.OK)
            {
                dataGridView_stack.BeginEdit(true);
                stk.LoadStack(openFileDialog_stack.FileName);
                dataGridView_stack.EndEdit();
                dataGridView_stack.Refresh();
            }
        }

        private void button_savestack_Click(object sender, EventArgs e)
        {
            saveFileDialog_stack.ShowDialog();
        }

        private void saveFileDialog_stack_FileOk(object sender, CancelEventArgs e)
        {
            stk.SaveStack(saveFileDialog_stack.FileName);
        }

        Font printFont = new Font("Courier New", 10);
        int printlistidx = 0;

        private void button_printstacklist_Click(object sender, EventArgs e)
        {
            printlistidx = 0;
            printDocument.Print();
        }

        String lastStackletter = "";
        void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float yPos = 0f;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            while (count < linesPerPage)
            {
                if(printlistidx >= info.stacklist.Count)
                {
                    line = null;
                    break;
                }

                if (count == 0)
                {
                    line = language.str(9);
                }
                else if (count == 1)
                {
                    line = "================================";
                }
                else {
                    String fl = info.stacklist[printlistidx].stackname.Substring(0, 1);
                    if (lastStackletter != fl)
                    {
                        lastStackletter = fl;
                        String newstack = "Backstack";
                        if (fl == "L") newstack = "Left Stack";
                        else if (fl == "I") newstack = "Custom Tray";
                        else if (fl != "B") newstack = "!!! STACK ERROR !!!";
                        line = "----------------" + newstack + "----------------";
                    }
                    else {
                        line = info.stacklist[printlistidx].stackname.PadRight(5) +
                            info.stacklist[printlistidx].name.PadRight(20).Substring(0, 20) +
                            info.stacklist[printlistidx].footprint.PadRight(15).Substring(0, 15) +
                            info.stacklist[printlistidx].rot.ToString().PadRight(5) +
                            info.stacklist[printlistidx].nozzle.ToString().PadRight(5) +
                            info.stacklist[printlistidx].height.ToString().PadRight(5) +
                            info.stacklist[printlistidx].vision;
                        printlistidx++;
                    }
                }

                yPos = topMargin + count * printFont.GetHeight(e.Graphics);
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }
            if (line != null)
            {
                e.HasMorePages = true;
            }
        }

        bool topside = true;

        private void button_bdruck_Click(object sender, EventArgs e)
        {
            if (egl.bdevlist.Count == 0) return;
            printlistidx = 0;
            topside = false;
            printDocument_bdruck.DefaultPageSettings.Landscape = true;
            printDocument_bdruck.Print();
        }

        private void printDocument_bdruck_PrintPage(object sender, PrintPageEventArgs e)
        {
            String ueberschrift = "Pick&Place "+ language.str(10)+ " - BOTTOM " + language.str(11) + ":";
            float yPos = 0f;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            while (count < linesPerPage)
            {
                if (topside)
                {
                    ueberschrift = "Pick&Place " + language.str(10) + " - TOP " + language.str(11) + ":";
                    if (printlistidx >= egl.tdevlist.Count)
                    {
                        line = null;
                        break;
                    }
                }
                else
                {
                    if (printlistidx >= egl.bdevlist.Count)
                    {
                        line = null;
                        break;
                    }
                }

                if (count == 0)
                {
                    line = ueberschrift;
                }
                else if (count == 1)
                {
                    line = "===============================";
                }
                else {
                    String press = "True";

                    if (topside)
                    {
                        if (!egl.tdevlist[printlistidx].pressure) press = "False";

                        line = egl.tdevlist[printlistidx].location.PadRight(6) +
                            egl.tdevlist[printlistidx].nozzle.ToString().PadRight(3) +
                            egl.tdevlist[printlistidx].stackname.PadRight(5) +
                            egl.tdevlist[printlistidx].x.ToString("000.00").PadRight(8) +
                            egl.tdevlist[printlistidx].y.ToString("000.00").PadRight(8) +
                            egl.tdevlist[printlistidx].rot.ToString("000.00").PadRight(7) +
                            egl.tdevlist[printlistidx].height.ToString("0.00").PadRight(7) +
                            egl.tdevlist[printlistidx].speed.ToString().PadRight(4) +
                            egl.tdevlist[printlistidx].vision.PadRight(9) +
                            press + " " +
                            egl.tdevlist[printlistidx].name.PadRight(20) +
                            egl.tdevlist[printlistidx].footprint.PadRight(15);
                    }
                    else {
                        if (!egl.bdevlist[printlistidx].pressure) press = "False";

                        line = egl.bdevlist[printlistidx].location.PadRight(6) +
                            egl.bdevlist[printlistidx].nozzle.ToString().PadRight(3) +
                            egl.bdevlist[printlistidx].stackname.PadRight(5) +
                            egl.bdevlist[printlistidx].x.ToString("000.00").PadRight(8) +
                            egl.bdevlist[printlistidx].y.ToString("000.00").PadRight(8) +
                            egl.bdevlist[printlistidx].rot.ToString("000.00").PadRight(7) +
                            egl.bdevlist[printlistidx].height.ToString("0.00").PadRight(7) +
                            egl.bdevlist[printlistidx].speed.ToString().PadRight(4) +
                            egl.bdevlist[printlistidx].vision.PadRight(9) +
                            press + " " +
                            egl.bdevlist[printlistidx].name.PadRight(20) +
                            egl.bdevlist[printlistidx].footprint.PadRight(15);
                    }

                    printlistidx++;
                }

                yPos = topMargin + count * printFont.GetHeight(e.Graphics);
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }
            if (line != null)
            {
                e.HasMorePages = true;
            }
        }

        private void button_tdruck_Click(object sender, EventArgs e)
        {
            if (egl.tdevlist.Count == 0) return;
            printlistidx = 0;
            topside = true;
            printDocument_bdruck.DefaultPageSettings.Landscape = true;
            printDocument_bdruck.Print();
        }

        private void button_de_Click(object sender, EventArgs e)
        {
            info.lang = 1;
            setGUI();
        }

        private void button_en_Click(object sender, EventArgs e)
        {
            info.lang = 0;
            setGUI();
        }

        void setGUI()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));

            if (info.lang == 0)
            {
                tabPage_loadfiles.Text = "Load/Save PnP Files";
                groupBox2.Text = "Load previously generated TVM802 files";
                button_loadTVMfiles.Text = "Load TVM802 Files";
                groupBox1.Text = "Load original EAGLE files";
                label3.Text = "set assignments to Nozzle, Stack, Fiducials etc.\r\nto default values. Load a TVM802 file\r\nfirst to get actual fiducial coordinates.";
                button_loadeaglefiles.Text = "Load Eagle Files";
                tabPage2.Text = "TOP devices";
                label7.Text = label8.Text = label9.Text = "Click on the header to sort a coloumn !";
                button_printstacklist.Text = button_bdruck.Text = button_tdruck.Text = "Print";
                tabPage3.Text = "BOTTOM devices";
                button_loadstacklist.Text = "Load";
                button_savestack.Text = "Save";
                button_makeTVMfiles.Text = "Generate TVM802 files";
                button3.Text = "Generate TVM802 files\r\nONLY for devices with\r\na Stack/Tray assigned";
                label2.Text = "The filename is automatically extended with:\r\nTOP-Layer:  _tvm802_top.csv\r\nBOTTOM-Layer: _tvm802_bottom.csv\r\n";
                groupBox3.Text = "save TVM802 files";
                label_usagetop.Text = "Usage:";
                label4.Text =
@"
Stack/Tray lists are stored separately and can be used for all boards.

Component rotation in the Stacks:
----------------------------------------------
Belt with R or Cs: is translated into TVM802 format automatically
Belt with SOT-23 (and similar 3pin devices): enter a rotation of -90 in the stack list !
Belt with diodes: is translated into TVM802 format automatically

Component rotation in custom trays:
---------------------------------------------
IC with 2 rows of pads (i.e. SO-8): pin-1 must be in the front-left
IC with 4 rows (i.e. QFN usw): pin-1 must be in the back-left
in general: put ICs into the tray so that the label on the chip is readable.

If components are posisioned in this way then all rotations are calculated
automatically, for the TOP as well as for the BOTTOM side.

BOTTOM-Side: the coordinates are mirrored automatically. The most left located device is used as the X-rotation axis
and is located at X=0 after rotation. The status window shows which component was used for rotation.
When setting the fiducials the offset of this mirror-axis must be added.
All other coordinates and rotations are calculated automatically for top and
bottom side.
";
                label_usage.Text = 
@"
1. enter all data in the Stack/Tray list according the reels/trays in your TVM802
   (the automatic assigment uses ""name"" and ""footprint"")

2. in EAGLE: run the ULP mountsmd, this generates the mnt and mnb files.

3. click ""laod Eagle files"" and choose an mnt or mnb file
   (this always load both, the mnt and mnb, files)

4. go to the TOP and bottom devices window and enter all data, or
    click ""Auto-Stack"" for automatic assignment
    (this works if name and footprint are identically in the stacklist and the board)

5. click ""generate TVM802 files""

go to your TVM802 and set the fiducials and save it.
Now you can reload the TVM802 file here and the fiducial coordinates
are kept as they are.
";

            }
            else
            {
                tabPage_loadfiles.Text = "Laden/Speichern PnP Dateien";
                groupBox2.Text = "Lade zuvor erstellte TVM802 Dateien";
                button_loadTVMfiles.Text = "Lade TVM802 Dateien";
                groupBox1.Text = "Lade EAGLE Originaldateien";
                label3.Text = "Zuordnungen zu Nozzle, Stack, Fiducials usw. \r\nwerden auf Defaultwerte gesetzt. E" +
    "vt. zuvor \r\neine TVM802 Datei laden um die Fiducial Werte \r\nzu übernehmen.";
                button_loadeaglefiles.Text = "Lade Eagle Dateien";
                tabPage2.Text = "TOP Bauteile";
                label7.Text = label8.Text = label9.Text = "zum Sortieren den Spaltenkopf anklicken !";
                button_printstacklist.Text = button_bdruck.Text = button_tdruck.Text = "Drucken";
                tabPage3.Text = "BOTTOM Bauteile";
                button_loadstacklist.Text = "Laden";
                button_savestack.Text = "Speichern";
                button_makeTVMfiles.Text = "Erzeuge TVM802 Dateien";
                button3.Text = "Erstelle TVM802 Dateien\r\nNUR Bauteile mit \r\nzugewiesenem Stack/Tray";
                label2.Text = "Die Dateien bekommen zusätzlich zum eingegebenem Namen eine Zusatz von:\r\nTOP-Layer:  _tvm802_top.csv\r\nBOTTOM-Layer: _tvm802_bottom.csv\r\n";
                groupBox3.Text = "speichere TVM802 Dateien";
                label_usagetop.Text = "Bedienung:";
                label_usage.Text = resources.GetString("label_usage.Text");
                label4.Text = resources.GetString("label4.Text");
            }
            Invalidate();
        }
    }
}
