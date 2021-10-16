﻿using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data;

namespace eagle2tvm
{
    public partial class Form1 : Form
    {
        eagle egl;
        universal_importer altium;
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
            altium = new universal_importer();
            stk = new stack();
            tvm802 = new tvm();
            info.Load();
            stk.LoadStack(info.myFilePath(info.stackfile));

            tdataGridView_devices.DataSource = egl.tdevlist;
            bdataGridView_devices.DataSource = egl.bdevlist;
            dataGridView_stack.DataSource = info.stacklist;
            dataGridView_fiducials.DataSource = info.tfiducialslist;
            dataGridView_bfiducials.DataSource = info.bfiducialslist;

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
            setStackComboboxes();
            setTopComboboxes();
            setBottomComboboxes();

            // mit 0 vorbelegen
            fiducialitem fi = new fiducialitem();
            info.tfiducialslist.Add(fi);
            info.bfiducialslist.Add(fi);

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

        int splashtime = 2;
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
                tb_platinendicke.Text = info.platinendicke.ToString();
                tb_exposure.Text = info.exposure_top.ToString();
                tb_exposure_bottom.Text = info.exposure_bottom.ToString();
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
                    bool found = false;
                    foreach (stackitem si in info.stacklist)
                    {
                        if (dev.name.ToUpper() == si.name.ToUpper() && dev.footprint.ToUpper().Contains(si.footprint.ToUpper())) 
                        {
                            dev.stackname = si.stackname.ToUpper();
                            dev.nozzle = si.nozzle;
                            dev.height = si.height;
                            dev.vision = si.vision;
                            dev.speed = si.speed;
                            dev.pressure = si.pressure;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        // dieses Bauteil befindet sich nicht in der Stackliste
                        // akzeptiere I, wenn es jedoch L oder B ist setze es auf ???
                        if (dev.stackname.ToUpper().Substring(0, 1) != "I")
                        {
                            dev.stackname = "L???";
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
                    bool found = false;
                    foreach (stackitem si in info.stacklist)
                    {
                        if (dev.name.ToUpper() == si.name.ToUpper() && dev.footprint.ToUpper().Contains(si.footprint.ToUpper()))
                        {
                            // Bauteil in Stackliste gefunden
                            dev.stackname = si.stackname.ToUpper();
                            dev.nozzle = si.nozzle;
                            dev.height = si.height;
                            dev.vision = si.vision;
                            dev.speed = si.speed;
                            dev.pressure = si.pressure;
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        // dieses Bauteil befindet sich nicht in der Stackliste
                        // akzeptiere I, wenn es jedoch L oder B ist setze es auf ???
                        if(dev.stackname.ToUpper().Substring(0,1) != "I")
                        {
                            dev.stackname = "L???";
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
        bool printused = false;

        private void button_printstacklist_Click(object sender, EventArgs e)
        {
            printlistidx = 0;
            printused = false;
            printDocument.Print();
        }

        private void button_printusedstacks_Click(object sender, EventArgs e)
        {
            printlistidx = 0;
            printused = true;
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
                    if(printused)
                    {
                        printlistidx = getNextUsedStack(printlistidx);
                        if(printlistidx == -1)
                        {
                            line = null;
                            break;
                        }
                    }
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

        int getNextUsedStack(int idx)
        {
            while (idx < info.stacklist.Count)
            {
                foreach (device dev in egl.tdevlist)
                {
                    if (info.stacklist[idx].stackname == dev.stackname)
                        return idx;
                }

                foreach (device dev in egl.bdevlist)
                {
                    if (info.stacklist[idx].stackname == dev.stackname)
                        return idx;
                }
                idx++;
            }

            return -1;
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

        private void button_pl_Click(object sender, EventArgs e)
        {
            info.lang = 2;
            setGUI();
        }

        DataGridViewComboBoxColumn makeFilledStringDataSource(String [] sa, int anz, String name, String type)
        {
            DataTable dt = new DataTable();
            DataColumn dcol;
            DataRow row;

            dcol = new DataColumn();
            dcol.DataType = Type.GetType(type);
            dcol.ColumnName = name;
            dcol.ReadOnly = true;
            dcol.Unique = true;
            dt.Columns.Add(dcol);

            for (int i = 0; i < anz; i++)
            {
                row = dt.NewRow();
                row[0] = sa[i];
                dt.Rows.Add(row);
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = dt;

            // und baue die Combobox
            DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
            cb.DataPropertyName = name;
            cb.HeaderText = name;
            cb.Width = 80;
            cb.DataSource = bs;
            cb.ValueMember = name;
            cb.DisplayMember = name;
            cb.SortMode = DataGridViewColumnSortMode.Automatic;
            return cb;
        }

        // Comboboxen für die Stackliste erzeugen
        void setStackComboboxes()
        {
            // Stackname Combobox
            String[] sa = new String[78];
            int idx = 0;
            for (int i = 1; i <= 24; i++) sa[idx++] = "L" + i.ToString();
            for (int i = 1; i <= 24; i++) sa[idx++] = "B" + i.ToString();
            for (int i = 1; i <= 30; i++) sa[idx++] = "I" + i.ToString();
            DataGridViewComboBoxColumn cb3 = makeFilledStringDataSource(sa, 78, "stackname", "System.String");
            dataGridView_stack.Columns.Insert(0,cb3);

            // Nozzle Combobox
            DataGridViewComboBoxColumn cb2 = makeFilledStringDataSource(new String[] { "1", "2" , "1/2"}, 3, "nozzle", "System.String");
            dataGridView_stack.Columns.Insert(5,cb2);

            // Speed Combobox
            DataGridViewComboBoxColumn cb4 = makeFilledStringDataSource(new String[] { "5", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", }, 11, "speed", "System.Int32");
            dataGridView_stack.Columns.Insert(7, cb4);

            // Vision Combobox
            DataGridViewComboBoxColumn cb1 = makeFilledStringDataSource(new String[] { "None", "Quick", "Accurate" }, 3, "vision", "System.String");
            dataGridView_stack.Columns.Insert(8,cb1);

            dataGridView_stack.Columns[1].Visible = false;  // und blende die originalen Spalten aus
            dataGridView_stack.Columns[6].Visible = false;
            dataGridView_stack.Columns[10].Visible = false;
            dataGridView_stack.Columns[11].Visible = false;
        }

        // Comboboxen für die TOPliste erzeugen
        void setTopComboboxes()
        {
            // Stackname Combobox
            String[] sa = new String[79];
            int idx = 0;
            for (int i = 1; i <= 24; i++) sa[idx++] = "L" + i.ToString();
            for (int i = 1; i <= 24; i++) sa[idx++] = "B" + i.ToString();
            for (int i = 1; i <= 30; i++) sa[idx++] = "I" + i.ToString();
            sa[78] = "L???";
            DataGridViewComboBoxColumn cb3 = makeFilledStringDataSource(sa, 79, "stackname", "System.String");
            tdataGridView_devices.Columns.Insert(4, cb3);

            // Nozzle Combobox
            DataGridViewComboBoxColumn cb2 = makeFilledStringDataSource(new String[] { "1", "2", "1/2" }, 3, "nozzle", "System.String");
            tdataGridView_devices.Columns.Insert(3, cb2);

            // Speed Combobox
            DataGridViewComboBoxColumn cb4 = makeFilledStringDataSource(new String[] { "5", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", }, 11, "speed", "System.Int32");
            tdataGridView_devices.Columns.Insert(7, cb4);

            // Vision Combobox
            DataGridViewComboBoxColumn cb1 = makeFilledStringDataSource(new String[] { "None", "Quick", "Accurate" }, 3, "vision", "System.String");
            tdataGridView_devices.Columns.Insert(8, cb1);

            tdataGridView_devices.Columns[4].Visible = false;  // und blende die originalen Spalten aus
            tdataGridView_devices.Columns[6].Visible = false;
            tdataGridView_devices.Columns[13].Visible = false;
            tdataGridView_devices.Columns[14].Visible = false;
        }

        // Comboboxen für die BOTTOMliste erzeugen
        void setBottomComboboxes()
        {
            // Stackname Combobox
            String[] sa = new String[79];
            int idx = 0;
            for (int i = 1; i <= 24; i++) sa[idx++] = "L" + i.ToString();
            for (int i = 1; i <= 24; i++) sa[idx++] = "B" + i.ToString();
            for (int i = 1; i <= 30; i++) sa[idx++] = "I" + i.ToString();
            sa[78] = "L???";
            DataGridViewComboBoxColumn cb3 = makeFilledStringDataSource(sa, 79, "stackname", "System.String");
            bdataGridView_devices.Columns.Insert(4, cb3);

            // Nozzle Combobox
            DataGridViewComboBoxColumn cb2 = makeFilledStringDataSource(new String[] { "1", "2", "1/2" }, 3, "nozzle", "System.String");
            bdataGridView_devices.Columns.Insert(3, cb2);

            // Speed Combobox
            DataGridViewComboBoxColumn cb4 = makeFilledStringDataSource(new String[] { "5", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", }, 11, "speed", "System.Int32");
            bdataGridView_devices.Columns.Insert(7, cb4);

            // Vision Combobox
            DataGridViewComboBoxColumn cb1 = makeFilledStringDataSource(new String[] { "None", "Quick", "Accurate" }, 3, "vision", "System.String");
            bdataGridView_devices.Columns.Insert(8, cb1);

            bdataGridView_devices.Columns[4].Visible = false;  // und blende die originalen Spalten aus
            bdataGridView_devices.Columns[6].Visible = false;
            bdataGridView_devices.Columns[13].Visible = false;
            bdataGridView_devices.Columns[14].Visible = false;
        }

        void setGUI()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));

            if (info.lang == 0)
            {
                button1.Text = "unused Stacks";
                tabPage_loadfiles.Text = "Load/Save PnP Files";
                groupBox2.Text = "Load previously generated TVM802 files";
                button_loadTVMfiles.Text = "Load TVM802 Files";
                groupBox1.Text = "Load original EAGLE files";
                label3.Text = "set assignments to Nozzle, Stack, Fiducials etc.\r\nto default values. Load a TVM802 file\r\nfirst to get actual fiducial coordinates.";
                button_loadeaglefiles.Text = "Load Eagle Files";
                button_loadaltiumfiles.Text = "Load Altium/OrCad/KiCad Files";
                tabPage2.Text = "TOP devices";
                label7.Text = label8.Text = label9.Text = "Click on the header to sort a column !";
                button_printstacklist.Text = button_bdruck.Text = button_tdruck.Text = "Print";
                button_printusedstacks.Text = "Print used Stacks";
                tabPage3.Text = "BOTTOM devices";
                button_loadstacklist.Text = "Load";
                button_savestack.Text = "Save";
                button_makeTVMfiles.Text = "Generate TVM802 files";
                button3.Text = "Generate TVM802 files\r\nONLY for devices with\r\na Stack/Tray assigned";
                label2.Text = "The filename is automatically extended with:\r\nTOP-Layer:  _tvm802_top.csv\r\nBOTTOM-Layer: _tvm802_bottom.csv\r\n";
                groupBox3.Text = "save TVM802 files";
                label_usagetop.Text = "Usage:";
                label10.Text = "Define in the board two SMD pads with the Device-Names FID1 and FID2 (for the TOP side)\r\nand FID3 and FID4 (for the bottom side)\r\nthese pads are imported as fiducials";
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

3. click ""load Eagle files"" and choose an mnt or mnb file
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
            else if (info.lang==2) {
                button1.Text = "Nieużywane podajniki";
                tabPage_loadfiles.Text = "Odczyt/Zapis plików PnP";
                groupBox2.Text = "Wczytaj poprzednio wygenerowane pliki TVM802";
                button_loadTVMfiles.Text = "Wczytaj pliki TVM802";
                groupBox1.Text = "Wczytaj źródłowe pliki EAGLE";
                label3.Text = "Zresetuj przydział dla głowic, podajników,\r\n markerów itp do wartości domyślnych.\r\n Wczytaj najpierw plik TVM802\r\n aby ustalić fizyczne współrzędne markerów.";
                button_loadeaglefiles.Text = "Wczytaj pliki Eagle";
                button_loadaltiumfiles.Text = "Wczytaj pliki Altium/OrCad/KiCad";
                tabPage2.Text = "Elementy strony TOP";
                label7.Text = label8.Text = label9.Text = "Kliknij na nagłówku kolumny aby posortować !";
                button_printstacklist.Text = button_bdruck.Text = button_tdruck.Text = "Drukuj";
                button_printusedstacks.Text = "Drukuj używane podajniki";
                tabPage3.Text = "Elementy strony BOTTOM";
                button_loadstacklist.Text = "Wczytaj";
                button_savestack.Text = "Zapis";
                button_makeTVMfiles.Text = "Wygeneruj pliki TVM802";
                button3.Text = "Wygeneruj pliki TVM802\r\nTYLKO dla elementów\r\nobsadzonych na podajnikach/tackach";
                label2.Text = "Nazwa pliku jest automatycznie uzupełniona przez:\r\nTOP-Layer:  _tvm802_top.csv\r\nBOTTOM-Layer: _tvm802_bottom.csv\r\n";
                groupBox3.Text = "zapisz pliki TVM802";
                label_usagetop.Text = "Instrukcja:";
                label10.Text = "Zdefiniuj na PCB dwa elementy SMD o nazwach FID1, FID2 (dla strony TOP)\r\n oraz FID3, FID4 (dla strony BOTTOM)\r\n posłużą one jako markery";
                label4.Text =
@"
Listy podajników/tacek są zapisane osobno i mogą być użyte ponownie we wszystkich projektach.

Orientacja elementów na podajnikach:
----------------------------------------------
Taśmy z rezystorami i kondensatorami jest dostosowana do formatu TVM802 automatycznie
Taśmy zawierająca elementy w SOT-23 i podobnych: wprowadź orientacje -90 stopni dla danej pozycji listy
Taśmy z diodami są dostosowywane do formatu TVM802 automatycznie

Orientacja elementów na definiowanych tackach:
----------------------------------------------
Elementy z dwoma rzędami wyprowadzeń (SO-8 itp): pin #1 powinien być w lewym dolnym rogu układu
Elementy z czterema rzędami wyprowadzeń (QFN, TQFP): pin #1 powinien być w lewym górnym rogu układu
Ogólnie Elementy powinny umieszczone być tak, aby ich oznaczenia były czytelne dla operatora

Przy zachowaniu tych zasad wszystkie korekcje orientacji będą przeliczone automatycznie zarówno dla strony TOP jak i BOTTOM

Strona BOTTOM: koordynaty elementów są przeliczone automatycznie. Skrajnie lewy element jest używany jako punkt referencyjny 
dla operacji korekcji i otrzymuje współrzędna X=0 po korekcji. Okno statusu pokazuje, który element został wybrany jako referencja 
dla tej operacji. Podczas ustawiania markerów przesunięcie to musi zostać dodane do ich pozycji.
Wszystkie pozostałe współrzędne i obroty są przeliczane automatycznie dla obu stron PCB.
";
                label_usage.Text =
@"
1. wprowadź wymagane dane do list podajników/tacek zgodnie z tym, co masz 
   założone na TVM802 (automat użyje pól ""nazwa"" oraz ""obudowa"")

2. w programie Eagle: uruchom skrypt ULP ""mountsmd"" - to wygeneruje potrzebne pliki
   mnt (TOP) oraz mnb (BOTTOM).

3. Kliknij ""Wczytaj pliki Eagle"" i wybierz jeden z dwóch plików
   (oba pliki zostaną wczytane w jednej operacji)

4. przejdź do okna list elementów dla strony TOP oraz BOTTOM i wprowadź 
   swoje ustawienia, lub skożystaj z przycisku AUTO
   (przycisk będzie działał, jeśli pola ""nazwa"" oraz ""obudowa"" 
    będą zawierały te same informacje co te we wczytanych plikach)

5. ""wygeneruj pliki TVM802""

Pozostało wczytać uzyskany plik do oprogramowania TVM802 i ustawić 
 fizyczne (we współrzednych maszyny) lokacje markerów.

Przy ponownym wczytaniu pliku zmodyfikowanego przez oprogramowanie TVM802 
 pola te pozostaną nietknięte.
";

            }
            else
            {
                button1.Text = "unbenuten Stacks";
                tabPage_loadfiles.Text = "Laden/Speichern PnP Dateien";
                groupBox2.Text = "Lade zuvor erstellte TVM802 Dateien";
                button_loadTVMfiles.Text = "Lade TVM802 Dateien";
                groupBox1.Text = "Lade EAGLE Originaldateien";
                label3.Text = "Zuordnungen zu Nozzle, Stack, Fiducials usw. \r\nwerden auf Defaultwerte gesetzt. Evt. zuvor \r\neine TVM802 Datei laden um die Fiducial Werte \r\nzu übernehmen.";
                button_loadeaglefiles.Text = "Lade Eagle Dateien";
                button_loadaltiumfiles.Text = "Lade Altium/OrCad/KiCad Dateien";
                tabPage2.Text = "TOP Bauteile";
                label7.Text = label8.Text = label9.Text = "zum Sortieren den Spaltenkopf anklicken !";
                button_printstacklist.Text = button_bdruck.Text = button_tdruck.Text = "Drucken";
                button_printusedstacks.Text = "Drucke benutzte Stacks";
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
                this.label10.Text = "Definiere im Board zwei SMD Pads mit den Namen: FID1 und FID2 (für die TOP Seite)\r\nund FID3 und FID4 (für die BOTTOM Seite).\r\nDiese werden auto" +
    "matisch als Fiducials importiert.";
            }
            Invalidate();
        }

        private void tdataGridView_devices_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        private void dataGridView_stack_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void tb_platinendicke_TextChanged(object sender, EventArgs e)
        {
            info.platinendicke = info.MyToDouble(tb_platinendicke.Text);
            if (info.platinendicke == 0) info.platinendicke = 1.6;
        }

        private void tb_exposure_TextChanged(object sender, EventArgs e)
        {
            info.exposure_top = info.MyToInt32(tb_exposure.Text);
            if (info.exposure_top == 0) info.exposure_top = 7;
        }

        private void tb_exposure_bottom_TextChanged(object sender, EventArgs e)
        {
            info.exposure_bottom = info.MyToInt32(tb_exposure.Text);
            if (info.exposure_bottom == 0) info.exposure_bottom = 7;
        }

        private void bt_checkstackB_Click(object sender, EventArgs e)
        {
            bool allok = true;
            String s = "";
            foreach (device dev in egl.bdevlist)
            {
                bool found = false;
                foreach (stackitem si in info.stacklist)
                {
                    if (dev.name.ToUpper() == si.name.ToUpper() && dev.footprint.ToUpper().Contains(si.footprint.ToUpper()))
                    {
                        // Bauteil in Stackliste gefunden
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    if (dev.stackname.ToUpper().Substring(0, 1) != "I")
                    {
                        // dieses Bauteil befindet sich nicht in der Stackliste
                        s += dev.name + " " + dev.footprint + " Stack: ???\r\n";
                        allok = false;
                    }
                }
            }
            if (!allok)
            {
                MessageBox.Show(s,"Stack Check");

            }
        }

        private void bt_checkstackT_Click(object sender, EventArgs e)
        {
            bool allok = true;
            String s = "";
            foreach (device dev in egl.tdevlist)
            {
                bool found = false;
                foreach (stackitem si in info.stacklist)
                {
                    if (dev.name.ToUpper() == si.name.ToUpper() && dev.footprint.ToUpper().Contains(si.footprint.ToUpper()))
                    {
                        // Bauteil in Stackliste gefunden
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    if (dev.stackname.ToUpper().Substring(0, 1) != "I")
                    {
                        // dieses Bauteil befindet sich nicht in der Stackliste
                        s += dev.name + " " + dev.footprint + " Stack: ???\r\n";
                        allok = false;
                    }
                }
            }
            if(!allok)
            {
                MessageBox.Show(s, "Stack Check");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s = "";

            for(int i=0; i<info.stacklist.Count; i++)
            {
                int ret = -1;
                foreach (device dev in egl.tdevlist)
                {
                    if (info.stacklist[i].name == dev.name)
                        ret = 0;
                }
                foreach (device dev in egl.bdevlist)
                {
                    if (info.stacklist[i].name == dev.name)
                        ret = 0;
                }

                if (ret == -1)
                {
                    s += info.stacklist[i].stackname + " " + info.stacklist[i].name + "\r\n";
                }
            }
            if(s.Length > 1)
            {
                MessageBox.Show(s);
            }
        }

        private void button_loadaltiumfiles_Click(object sender, EventArgs e)
        {
            button_ConverUnits.Enabled = true;
            button_ConvertUnitsT.Enabled = true;
            openUniversalFiles.InitialDirectory = info.LastDir;
            openUniversalFiles.FileName = info.LastFile;
            DialogResult dr = openUniversalFiles.ShowDialog();
            if (dr == DialogResult.OK)
            {
                info.LastFile = Path.GetFileName(openUniversalFiles.FileName);
                info.LastDir = Path.GetDirectoryName(openUniversalFiles.FileName);
                int ret = altium.Load();
                if (altium.units == 1)
                {
                    MessageBox.Show("TVM802 Software understands only METRIC units\n\r Imperial units were detected!\n\r Please convert them before saving output file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_ConverUnits.Enabled = true;
                    button_ConvertUnitsT.Enabled = true;
                }
                else
                {
                    button_ConverUnits.Enabled = false;
                    button_ConvertUnitsT.Enabled = false;
                }

                if (ret == 1)
                    MessageBox.Show(language.str(0) + info.error, language.str(1), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (ret == 2)
                    MessageBox.Show(language.str(0) + info.error, language.str(2), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                egl.bdevlist.Clear();
                egl.tdevlist.Clear();
                foreach (device dev in altium.bdevlist)
                {
                    egl.bdevlist.Add(dev);
                }
                foreach (device dev in altium.tdevlist)
                {
                    egl.tdevlist.Add(dev);
                }

            }

        }

        private void button_ConverUnits_Click(object sender, EventArgs e)
        {
            foreach(device dev in egl.bdevlist)
            {
                dev.x = Math.Round(dev.x * 0.0254,3);
                dev.y = Math.Round(dev.y * 0.0254,3);
            }
            foreach (device dev in egl.tdevlist)
            {
                dev.x = Math.Round(dev.x * 0.0254,3);
                dev.y = Math.Round(dev.y * 0.0254,3);
            }
            button_ConverUnits.Enabled = false;
            button_ConvertUnitsT.Enabled = false;
            bdataGridView_devices.Refresh();

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
