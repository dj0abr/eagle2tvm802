namespace eagle2tvm
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_loadfiles = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_loadTVMfiles = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_loadeaglefiles = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.button_tdruck = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tdataGridView_devices = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.button_bdruck = new System.Windows.Forms.Button();
            this.button_bzuordnung = new System.Windows.Forms.Button();
            this.bdataGridView_devices = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.button_printstacklist = new System.Windows.Forms.Button();
            this.button_loadstacklist = new System.Windows.Forms.Button();
            this.button_savestack = new System.Windows.Forms.Button();
            this.dataGridView_stack = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.openEagleFiles = new System.Windows.Forms.OpenFileDialog();
            this.textBox_status = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openTVMfiles = new System.Windows.Forms.OpenFileDialog();
            this.textBox_info = new System.Windows.Forms.TextBox();
            this.saveFileDialog_tvm802 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog_stack = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_stack = new System.Windows.Forms.SaveFileDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDocument_bdruck = new System.Drawing.Printing.PrintDocument();
            this.button_en = new System.Windows.Forms.Button();
            this.button_de = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button_makeTVMfiles = new System.Windows.Forms.Button();
            this.label_usagetop = new System.Windows.Forms.Label();
            this.label_usage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage_loadfiles.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdataGridView_devices)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdataGridView_devices)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_stack)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_loadfiles);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(17, 16);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1221, 708);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_loadfiles
            // 
            this.tabPage_loadfiles.Controls.Add(this.label2);
            this.tabPage_loadfiles.Controls.Add(this.label_usage);
            this.tabPage_loadfiles.Controls.Add(this.label_usagetop);
            this.tabPage_loadfiles.Controls.Add(this.groupBox3);
            this.tabPage_loadfiles.Controls.Add(this.groupBox2);
            this.tabPage_loadfiles.Controls.Add(this.groupBox1);
            this.tabPage_loadfiles.Location = new System.Drawing.Point(4, 25);
            this.tabPage_loadfiles.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage_loadfiles.Name = "tabPage_loadfiles";
            this.tabPage_loadfiles.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage_loadfiles.Size = new System.Drawing.Size(1213, 679);
            this.tabPage_loadfiles.TabIndex = 0;
            this.tabPage_loadfiles.Text = "Laden/Soeichern PnP Dateien";
            this.tabPage_loadfiles.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_loadTVMfiles);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(21, 276);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(405, 138);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lade zuvor erstellte TVM802 Dateien";
            // 
            // button_loadTVMfiles
            // 
            this.button_loadTVMfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadTVMfiles.Location = new System.Drawing.Point(24, 57);
            this.button_loadTVMfiles.Margin = new System.Windows.Forms.Padding(4);
            this.button_loadTVMfiles.Name = "button_loadTVMfiles";
            this.button_loadTVMfiles.Size = new System.Drawing.Size(343, 42);
            this.button_loadTVMfiles.TabIndex = 1;
            this.button_loadTVMfiles.Text = "Lade TVM802 Dateien";
            this.button_loadTVMfiles.UseVisualStyleBackColor = true;
            this.button_loadTVMfiles.Click += new System.EventHandler(this.button_loadTVMfiles_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button_loadeaglefiles);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(21, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(405, 246);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lade EAGLE Originaldateien";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(20, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 72);
            this.label3.TabIndex = 2;
            this.label3.Text = "Zuordnungen zu Nozzle, Stack, Fiducials usw. \r\nwerden auf Defaultwerte gesetzt. E" +
    "vt. zuvor \r\neine TVM802 Datei laden um die Fiducial Werte \r\nzu übernehmen.";
            // 
            // button_loadeaglefiles
            // 
            this.button_loadeaglefiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadeaglefiles.Location = new System.Drawing.Point(24, 135);
            this.button_loadeaglefiles.Margin = new System.Windows.Forms.Padding(4);
            this.button_loadeaglefiles.Name = "button_loadeaglefiles";
            this.button_loadeaglefiles.Size = new System.Drawing.Size(343, 82);
            this.button_loadeaglefiles.TabIndex = 1;
            this.button_loadeaglefiles.Text = "Lade Eagle Dateien";
            this.button_loadeaglefiles.UseVisualStyleBackColor = true;
            this.button_loadeaglefiles.Click += new System.EventHandler(this.button_loadeaglefiles_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.button_tdruck);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.tdataGridView_devices);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1213, 679);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TOP Bauteile";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 651);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "zum Sortieren den Spaltenkopf anklicken !";
            // 
            // button_tdruck
            // 
            this.button_tdruck.Location = new System.Drawing.Point(794, 640);
            this.button_tdruck.Margin = new System.Windows.Forms.Padding(4);
            this.button_tdruck.Name = "button_tdruck";
            this.button_tdruck.Size = new System.Drawing.Size(180, 28);
            this.button_tdruck.TabIndex = 5;
            this.button_tdruck.Text = "Drucken";
            this.button_tdruck.UseVisualStyleBackColor = true;
            this.button_tdruck.Click += new System.EventHandler(this.button_tdruck_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1023, 640);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 28);
            this.button2.TabIndex = 4;
            this.button2.Text = "Auto-Stack";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tdataGridView_devices
            // 
            this.tdataGridView_devices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tdataGridView_devices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tdataGridView_devices.Location = new System.Drawing.Point(9, 9);
            this.tdataGridView_devices.Margin = new System.Windows.Forms.Padding(4);
            this.tdataGridView_devices.MultiSelect = false;
            this.tdataGridView_devices.Name = "tdataGridView_devices";
            this.tdataGridView_devices.Size = new System.Drawing.Size(1193, 624);
            this.tdataGridView_devices.TabIndex = 0;
            this.tdataGridView_devices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bdataGridView_devices_KeyDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.button_bdruck);
            this.tabPage3.Controls.Add(this.button_bzuordnung);
            this.tabPage3.Controls.Add(this.bdataGridView_devices);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1213, 679);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "BOTTOM Bauteile";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 651);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(273, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "zum Sortieren den Spaltenkopf anklicken !";
            // 
            // button_bdruck
            // 
            this.button_bdruck.Location = new System.Drawing.Point(782, 640);
            this.button_bdruck.Margin = new System.Windows.Forms.Padding(4);
            this.button_bdruck.Name = "button_bdruck";
            this.button_bdruck.Size = new System.Drawing.Size(180, 28);
            this.button_bdruck.TabIndex = 3;
            this.button_bdruck.Text = "Drucken";
            this.button_bdruck.UseVisualStyleBackColor = true;
            this.button_bdruck.Click += new System.EventHandler(this.button_bdruck_Click);
            // 
            // button_bzuordnung
            // 
            this.button_bzuordnung.Location = new System.Drawing.Point(1021, 640);
            this.button_bzuordnung.Margin = new System.Windows.Forms.Padding(4);
            this.button_bzuordnung.Name = "button_bzuordnung";
            this.button_bzuordnung.Size = new System.Drawing.Size(180, 28);
            this.button_bzuordnung.TabIndex = 2;
            this.button_bzuordnung.Text = "Auto-Stack";
            this.button_bzuordnung.UseVisualStyleBackColor = true;
            this.button_bzuordnung.Click += new System.EventHandler(this.button_bzuordnung_Click);
            // 
            // bdataGridView_devices
            // 
            this.bdataGridView_devices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.bdataGridView_devices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bdataGridView_devices.Location = new System.Drawing.Point(8, 7);
            this.bdataGridView_devices.Margin = new System.Windows.Forms.Padding(4);
            this.bdataGridView_devices.MultiSelect = false;
            this.bdataGridView_devices.Name = "bdataGridView_devices";
            this.bdataGridView_devices.Size = new System.Drawing.Size(1195, 625);
            this.bdataGridView_devices.TabIndex = 1;
            this.bdataGridView_devices.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.bdataGridView_devices_DataError);
            this.bdataGridView_devices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bdataGridView_devices_KeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button_printstacklist);
            this.tabPage1.Controls.Add(this.button_loadstacklist);
            this.tabPage1.Controls.Add(this.button_savestack);
            this.tabPage1.Controls.Add(this.dataGridView_stack);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1213, 679);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Stack/Tray";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 650);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(273, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "zum Sortieren den Spaltenkopf anklicken !";
            // 
            // button_printstacklist
            // 
            this.button_printstacklist.Location = new System.Drawing.Point(731, 643);
            this.button_printstacklist.Name = "button_printstacklist";
            this.button_printstacklist.Size = new System.Drawing.Size(120, 29);
            this.button_printstacklist.TabIndex = 3;
            this.button_printstacklist.Text = "Drucken";
            this.button_printstacklist.UseVisualStyleBackColor = true;
            this.button_printstacklist.Click += new System.EventHandler(this.button_printstacklist_Click);
            // 
            // button_loadstacklist
            // 
            this.button_loadstacklist.Location = new System.Drawing.Point(906, 643);
            this.button_loadstacklist.Name = "button_loadstacklist";
            this.button_loadstacklist.Size = new System.Drawing.Size(120, 29);
            this.button_loadstacklist.TabIndex = 2;
            this.button_loadstacklist.Text = "Laden";
            this.button_loadstacklist.UseVisualStyleBackColor = true;
            this.button_loadstacklist.Click += new System.EventHandler(this.button_loadstacklist_Click);
            // 
            // button_savestack
            // 
            this.button_savestack.Location = new System.Drawing.Point(1081, 643);
            this.button_savestack.Name = "button_savestack";
            this.button_savestack.Size = new System.Drawing.Size(120, 29);
            this.button_savestack.TabIndex = 1;
            this.button_savestack.Text = "Speichern";
            this.button_savestack.UseVisualStyleBackColor = true;
            this.button_savestack.Click += new System.EventHandler(this.button_savestack_Click);
            // 
            // dataGridView_stack
            // 
            this.dataGridView_stack.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_stack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_stack.Location = new System.Drawing.Point(9, 9);
            this.dataGridView_stack.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_stack.MultiSelect = false;
            this.dataGridView_stack.Name = "dataGridView_stack";
            this.dataGridView_stack.Size = new System.Drawing.Size(1193, 627);
            this.dataGridView_stack.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label6);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage5.Size = new System.Drawing.Size(1213, 679);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Info";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(929, 642);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(247, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Autor: www.dj0abr.de, www.helitron.de";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1065, 550);
            this.label4.TabIndex = 0;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // openEagleFiles
            // 
            this.openEagleFiles.Filter = "EagleMnt (*.mnt)|*.mnt|EagleMnb (*.mnb)|*.mnb|All Files (*.*)|*.*";
            // 
            // textBox_status
            // 
            this.textBox_status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_status.Location = new System.Drawing.Point(23, 732);
            this.textBox_status.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_status.Multiline = true;
            this.textBox_status.Name = "textBox_status";
            this.textBox_status.ReadOnly = true;
            this.textBox_status.Size = new System.Drawing.Size(671, 116);
            this.textBox_status.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openTVMfiles
            // 
            this.openTVMfiles.Filter = "TVM802 (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
            // 
            // textBox_info
            // 
            this.textBox_info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_info.Location = new System.Drawing.Point(703, 732);
            this.textBox_info.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_info.Multiline = true;
            this.textBox_info.Name = "textBox_info";
            this.textBox_info.ReadOnly = true;
            this.textBox_info.Size = new System.Drawing.Size(530, 116);
            this.textBox_info.TabIndex = 2;
            // 
            // saveFileDialog_tvm802
            // 
            this.saveFileDialog_tvm802.DefaultExt = "csv";
            this.saveFileDialog_tvm802.Filter = "TVM802 (*.csv)|*.csv|All Files (*.*)|*.*";
            this.saveFileDialog_tvm802.RestoreDirectory = true;
            // 
            // openFileDialog_stack
            // 
            this.openFileDialog_stack.FileName = "mystack.stk";
            this.openFileDialog_stack.Filter = "StackFile (*.stk)|*.stk|All Files (*.*)|*.*";
            // 
            // saveFileDialog_stack
            // 
            this.saveFileDialog_stack.FileName = "mystack.stk";
            this.saveFileDialog_stack.Filter = "StackFile (*.stk)|*.stk|All Files (*.*)|*.*";
            this.saveFileDialog_stack.RestoreDirectory = true;
            this.saveFileDialog_stack.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_stack_FileOk);
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDocument_bdruck
            // 
            this.printDocument_bdruck.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_bdruck_PrintPage);
            // 
            // button_en
            // 
            this.button_en.BackgroundImage = global::eagle2tvm.Properties.Resources.flagge_grossbritannien;
            this.button_en.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_en.Location = new System.Drawing.Point(1185, 6);
            this.button_en.Name = "button_en";
            this.button_en.Size = new System.Drawing.Size(44, 28);
            this.button_en.TabIndex = 3;
            this.button_en.UseVisualStyleBackColor = true;
            this.button_en.Click += new System.EventHandler(this.button_en_Click);
            // 
            // button_de
            // 
            this.button_de.BackgroundImage = global::eagle2tvm.Properties.Resources.flagge_deutschland;
            this.button_de.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_de.Location = new System.Drawing.Point(1135, 6);
            this.button_de.Name = "button_de";
            this.button_de.Size = new System.Drawing.Size(44, 28);
            this.button_de.TabIndex = 4;
            this.button_de.UseVisualStyleBackColor = true;
            this.button_de.Click += new System.EventHandler(this.button_de_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button_makeTVMfiles);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(21, 430);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 227);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Speichere TVM802 Dateien";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(24, 107);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(343, 85);
            this.button3.TabIndex = 6;
            this.button3.Text = "Erstelle TVM802 Dateien\r\nNUR Bauteile mit \r\nzugewiesenem Stack/Tray";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_makeTVMfiles
            // 
            this.button_makeTVMfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_makeTVMfiles.Location = new System.Drawing.Point(24, 35);
            this.button_makeTVMfiles.Margin = new System.Windows.Forms.Padding(4);
            this.button_makeTVMfiles.Name = "button_makeTVMfiles";
            this.button_makeTVMfiles.Size = new System.Drawing.Size(343, 47);
            this.button_makeTVMfiles.TabIndex = 5;
            this.button_makeTVMfiles.Text = "Erstelle TVM802 Dateien";
            this.button_makeTVMfiles.UseVisualStyleBackColor = true;
            this.button_makeTVMfiles.Click += new System.EventHandler(this.button_makeTVMfiles_Click);
            // 
            // label_usagetop
            // 
            this.label_usagetop.AutoSize = true;
            this.label_usagetop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_usagetop.Location = new System.Drawing.Point(459, 36);
            this.label_usagetop.Name = "label_usagetop";
            this.label_usagetop.Size = new System.Drawing.Size(136, 29);
            this.label_usagetop.TabIndex = 5;
            this.label_usagetop.Text = "Bedienung:";
            // 
            // label_usage
            // 
            this.label_usage.AutoSize = true;
            this.label_usage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_usage.Location = new System.Drawing.Point(464, 99);
            this.label_usage.Name = "label_usage";
            this.label_usage.Size = new System.Drawing.Size(627, 360);
            this.label_usage.TabIndex = 6;
            this.label_usage.Text = resources.GetString("label_usage.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(464, 557);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(609, 60);
            this.label2.TabIndex = 7;
            this.label2.Text = "Die Dateien bekommen zusätzlich zum eingegebenem Namen einen Zusatz von:\r\nTOP-Lay" +
    "er:  _tvm802_top.csv\r\nBOTTOM-Layer: _tvm802_bottom.csv\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 863);
            this.Controls.Add(this.button_de);
            this.Controls.Add(this.button_en);
            this.Controls.Add(this.textBox_info);
            this.Controls.Add(this.textBox_status);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Eagle mnt/mnb to TVM802 Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_loadfiles.ResumeLayout(false);
            this.tabPage_loadfiles.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdataGridView_devices)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdataGridView_devices)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_stack)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_loadfiles;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_loadeaglefiles;
        private System.Windows.Forms.OpenFileDialog openEagleFiles;
        private System.Windows.Forms.DataGridView tdataGridView_devices;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView_stack;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox_status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_loadTVMfiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openTVMfiles;
        private System.Windows.Forms.DataGridView bdataGridView_devices;
        private System.Windows.Forms.TextBox textBox_info;
        private System.Windows.Forms.Button button_bzuordnung;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_tvm802;
        private System.Windows.Forms.Button button_printstacklist;
        private System.Windows.Forms.Button button_loadstacklist;
        private System.Windows.Forms.Button button_savestack;
        private System.Windows.Forms.OpenFileDialog openFileDialog_stack;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_stack;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_bdruck;
        private System.Drawing.Printing.PrintDocument printDocument_bdruck;
        private System.Windows.Forms.Button button_tdruck;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_en;
        private System.Windows.Forms.Button button_de;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button_makeTVMfiles;
        private System.Windows.Forms.Label label_usagetop;
        private System.Windows.Forms.Label label_usage;
        private System.Windows.Forms.Label label2;
    }
}

