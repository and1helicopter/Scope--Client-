namespace ScopeSetupApp
{
    partial class ScopeConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopeConfigForm));
            this.mailToolStrip = new System.Windows.Forms.ToolStrip();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.View_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Print_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Update_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SetDefault_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.nominalFrequency_label = new System.Windows.Forms.Label();
            this.nominalFrequency_textBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ConfigAddr_textBox = new System.Windows.Forms.TextBox();
            this.ConfigAddr_label = new System.Windows.Forms.Label();
            this.OscilCmndAddr_textBox = new System.Windows.Forms.TextBox();
            this.OscilCmndAddr_label = new System.Windows.Forms.Label();
            this.CommentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sampleRate_textBox = new System.Windows.Forms.TextBox();
            this.sampleRate_label = new System.Windows.Forms.Label();
            this.OscilSizeData_TextBox = new System.Windows.Forms.TextBox();
            this.OscilSizeData_label = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.recordingDevice_textBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.stationName_textBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.leapsec_textBox = new System.Windows.Forms.TextBox();
            this.timeCode_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tmqCode_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.localCode_textBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.configPanel = new System.Windows.Forms.Panel();
            this.SCPrintDialog = new System.Windows.Forms.PrintDialog();
            this.SCPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.SCPrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.ConfigToSystem_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mailToolStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mailToolStrip
            // 
            this.mailToolStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mailToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mailToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mailToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.saveButton,
            this.toolStripSeparator2,
            this.View_toolStripButton,
            this.Print_toolStripButton,
            this.toolStripSeparator1,
            this.Update_toolStripButton,
            this.SetDefault_toolStripButton});
            this.mailToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mailToolStrip.Name = "mailToolStrip";
            this.mailToolStrip.Size = new System.Drawing.Size(885, 38);
            this.mailToolStrip.TabIndex = 0;
            this.mailToolStrip.Text = "toolStrip1";
            // 
            // openButton
            // 
            this.openButton.AutoSize = false;
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Margin = new System.Windows.Forms.Padding(3);
            this.openButton.Name = "openButton";
            this.openButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.openButton.Size = new System.Drawing.Size(32, 32);
            this.openButton.Text = "Открыть файл";
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = false;
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Margin = new System.Windows.Forms.Padding(3);
            this.saveButton.MergeIndex = 0;
            this.saveButton.Name = "saveButton";
            this.saveButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.saveButton.Size = new System.Drawing.Size(32, 32);
            this.saveButton.Text = "Сохранить файл";
            this.saveButton.ToolTipText = "Сохранить в файл";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // View_toolStripButton
            // 
            this.View_toolStripButton.AutoSize = false;
            this.View_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.View_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("View_toolStripButton.Image")));
            this.View_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.View_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
            this.View_toolStripButton.Name = "View_toolStripButton";
            this.View_toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.View_toolStripButton.Size = new System.Drawing.Size(32, 32);
            this.View_toolStripButton.Text = "Просмотр";
            this.View_toolStripButton.Click += new System.EventHandler(this.View_toolStripButton_Click);
            // 
            // Print_toolStripButton
            // 
            this.Print_toolStripButton.AutoSize = false;
            this.Print_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Print_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Print_toolStripButton.Image")));
            this.Print_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Print_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
            this.Print_toolStripButton.Name = "Print_toolStripButton";
            this.Print_toolStripButton.Size = new System.Drawing.Size(32, 32);
            this.Print_toolStripButton.Text = "Печать";
            this.Print_toolStripButton.Click += new System.EventHandler(this.Print_toolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // Update_toolStripButton
            // 
            this.Update_toolStripButton.AutoSize = false;
            this.Update_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Update_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Update_toolStripButton.Image")));
            this.Update_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Update_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
            this.Update_toolStripButton.Name = "Update_toolStripButton";
            this.Update_toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Update_toolStripButton.Size = new System.Drawing.Size(32, 32);
            this.Update_toolStripButton.Text = "Применить конфигурацию";
            this.Update_toolStripButton.Click += new System.EventHandler(this.Update_toolStripButton_Click);
            // 
            // SetDefault_toolStripButton
            // 
            this.SetDefault_toolStripButton.AutoSize = false;
            this.SetDefault_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SetDefault_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SetDefault_toolStripButton.Image")));
            this.SetDefault_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SetDefault_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
            this.SetDefault_toolStripButton.Name = "SetDefault_toolStripButton";
            this.SetDefault_toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SetDefault_toolStripButton.Size = new System.Drawing.Size(32, 32);
            this.SetDefault_toolStripButton.Text = "Применить конфигурацию по умолчанию ";
            this.SetDefault_toolStripButton.Click += new System.EventHandler(this.SetDefault_toolStripButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(885, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // nominalFrequency_label
            // 
            this.nominalFrequency_label.AutoSize = true;
            this.nominalFrequency_label.Location = new System.Drawing.Point(10, 67);
            this.nominalFrequency_label.Name = "nominalFrequency_label";
            this.nominalFrequency_label.Size = new System.Drawing.Size(136, 15);
            this.nominalFrequency_label.TabIndex = 23;
            this.nominalFrequency_label.Text = "Nominal frequency (Hz)";
            // 
            // nominalFrequency_textBox
            // 
            this.nominalFrequency_textBox.Location = new System.Drawing.Point(156, 64);
            this.nominalFrequency_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nominalFrequency_textBox.Name = "nominalFrequency_textBox";
            this.nominalFrequency_textBox.Size = new System.Drawing.Size(116, 21);
            this.nominalFrequency_textBox.TabIndex = 24;
            this.nominalFrequency_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.ItemSize = new System.Drawing.Size(130, 20);
            this.tabControl1.Location = new System.Drawing.Point(0, 38);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(885, 123);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 23;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(877, 95);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Oscil Config";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.ConfigAddr_textBox);
            this.panel5.Controls.Add(this.ConfigAddr_label);
            this.panel5.Controls.Add(this.OscilCmndAddr_textBox);
            this.panel5.Controls.Add(this.OscilCmndAddr_label);
            this.panel5.Controls.Add(this.CommentRichTextBox);
            this.panel5.Controls.Add(this.sampleRate_textBox);
            this.panel5.Controls.Add(this.sampleRate_label);
            this.panel5.Controls.Add(this.OscilSizeData_TextBox);
            this.panel5.Controls.Add(this.OscilSizeData_label);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(877, 95);
            this.panel5.TabIndex = 3;
            // 
            // ConfigAddr_textBox
            // 
            this.ConfigAddr_textBox.Location = new System.Drawing.Point(426, 10);
            this.ConfigAddr_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConfigAddr_textBox.Name = "ConfigAddr_textBox";
            this.ConfigAddr_textBox.Size = new System.Drawing.Size(116, 21);
            this.ConfigAddr_textBox.TabIndex = 33;
            this.ConfigAddr_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ConfigAddr_label
            // 
            this.ConfigAddr_label.Location = new System.Drawing.Point(276, 10);
            this.ConfigAddr_label.Name = "ConfigAddr_label";
            this.ConfigAddr_label.Size = new System.Drawing.Size(147, 26);
            this.ConfigAddr_label.TabIndex = 32;
            this.ConfigAddr_label.Text = "Configuration Address";
            this.ConfigAddr_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OscilCmndAddr_textBox
            // 
            this.OscilCmndAddr_textBox.Location = new System.Drawing.Point(426, 37);
            this.OscilCmndAddr_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OscilCmndAddr_textBox.Name = "OscilCmndAddr_textBox";
            this.OscilCmndAddr_textBox.Size = new System.Drawing.Size(116, 21);
            this.OscilCmndAddr_textBox.TabIndex = 31;
            this.OscilCmndAddr_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OscilCmndAddr_label
            // 
            this.OscilCmndAddr_label.Location = new System.Drawing.Point(276, 35);
            this.OscilCmndAddr_label.Name = "OscilCmndAddr_label";
            this.OscilCmndAddr_label.Size = new System.Drawing.Size(147, 26);
            this.OscilCmndAddr_label.TabIndex = 30;
            this.OscilCmndAddr_label.Text = "Oscil Cmnd Address";
            this.OscilCmndAddr_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CommentRichTextBox
            // 
            this.CommentRichTextBox.Location = new System.Drawing.Point(8, 8);
            this.CommentRichTextBox.Name = "CommentRichTextBox";
            this.CommentRichTextBox.Size = new System.Drawing.Size(263, 50);
            this.CommentRichTextBox.TabIndex = 29;
            this.CommentRichTextBox.Text = "";
            // 
            // sampleRate_textBox
            // 
            this.sampleRate_textBox.Location = new System.Drawing.Point(725, 37);
            this.sampleRate_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sampleRate_textBox.Name = "sampleRate_textBox";
            this.sampleRate_textBox.Size = new System.Drawing.Size(116, 21);
            this.sampleRate_textBox.TabIndex = 28;
            this.sampleRate_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sampleRate_label
            // 
            this.sampleRate_label.AutoSize = true;
            this.sampleRate_label.Location = new System.Drawing.Point(617, 40);
            this.sampleRate_label.Name = "sampleRate_label";
            this.sampleRate_label.Size = new System.Drawing.Size(100, 15);
            this.sampleRate_label.TabIndex = 27;
            this.sampleRate_label.Text = "Sample rate (Hz)";
            // 
            // OscilSizeData_TextBox
            // 
            this.OscilSizeData_TextBox.Location = new System.Drawing.Point(725, 10);
            this.OscilSizeData_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OscilSizeData_TextBox.Name = "OscilSizeData_TextBox";
            this.OscilSizeData_TextBox.Size = new System.Drawing.Size(116, 21);
            this.OscilSizeData_TextBox.TabIndex = 21;
            this.OscilSizeData_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OscilSizeData_label
            // 
            this.OscilSizeData_label.Location = new System.Drawing.Point(577, 8);
            this.OscilSizeData_label.Name = "OscilSizeData_label";
            this.OscilSizeData_label.Size = new System.Drawing.Size(142, 26);
            this.OscilSizeData_label.TabIndex = 22;
            this.OscilSizeData_label.Text = "Oscill Size Data (Kbyte)";
            this.OscilSizeData_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.recordingDevice_textBox);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.stationName_textBox);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.nominalFrequency_textBox);
            this.tabPage3.Controls.Add(this.nominalFrequency_label);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(877, 95);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = " COMETRADE Config";
            // 
            // recordingDevice_textBox
            // 
            this.recordingDevice_textBox.Location = new System.Drawing.Point(156, 37);
            this.recordingDevice_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.recordingDevice_textBox.Name = "recordingDevice_textBox";
            this.recordingDevice_textBox.Size = new System.Drawing.Size(116, 21);
            this.recordingDevice_textBox.TabIndex = 39;
            this.recordingDevice_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(48, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 15);
            this.label9.TabIndex = 38;
            this.label9.Text = "Recording device";
            // 
            // stationName_textBox
            // 
            this.stationName_textBox.Location = new System.Drawing.Point(156, 10);
            this.stationName_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stationName_textBox.Name = "stationName_textBox";
            this.stationName_textBox.Size = new System.Drawing.Size(116, 21);
            this.stationName_textBox.TabIndex = 37;
            this.stationName_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(68, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 15);
            this.label14.TabIndex = 36;
            this.label14.Text = "Station name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.leapsec_textBox);
            this.groupBox1.Controls.Add(this.timeCode_textBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tmqCode_textBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.localCode_textBox);
            this.groupBox1.Location = new System.Drawing.Point(278, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(591, 88);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "For rev. 2013";
            // 
            // leapsec_textBox
            // 
            this.leapsec_textBox.Location = new System.Drawing.Point(447, 37);
            this.leapsec_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.leapsec_textBox.Name = "leapsec_textBox";
            this.leapsec_textBox.Size = new System.Drawing.Size(116, 21);
            this.leapsec_textBox.TabIndex = 34;
            this.leapsec_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // timeCode_textBox
            // 
            this.timeCode_textBox.Location = new System.Drawing.Point(148, 10);
            this.timeCode_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeCode_textBox.Name = "timeCode_textBox";
            this.timeCode_textBox.Size = new System.Drawing.Size(116, 21);
            this.timeCode_textBox.TabIndex = 28;
            this.timeCode_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(390, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "leapsec";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "Time Code";
            // 
            // tmqCode_textBox
            // 
            this.tmqCode_textBox.Location = new System.Drawing.Point(447, 10);
            this.tmqCode_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tmqCode_textBox.Name = "tmqCode_textBox";
            this.tmqCode_textBox.Size = new System.Drawing.Size(116, 21);
            this.tmqCode_textBox.TabIndex = 32;
            this.tmqCode_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Local Code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(379, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 15);
            this.label8.TabIndex = 31;
            this.label8.Text = "tmq Code";
            // 
            // localCode_textBox
            // 
            this.localCode_textBox.Location = new System.Drawing.Point(148, 37);
            this.localCode_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.localCode_textBox.Name = "localCode_textBox";
            this.localCode_textBox.Size = new System.Drawing.Size(116, 21);
            this.localCode_textBox.TabIndex = 30;
            this.localCode_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 161);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(885, 65);
            this.panel2.TabIndex = 24;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Location = new System.Drawing.Point(716, 43);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox2.Size = new System.Drawing.Size(147, 19);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Выбрать все каналы";
            this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripSeparator3,
            this.toolStripButton3,
            this.toolStripButton2,
            this.toolStripSeparator4,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(885, 38);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.AutoSize = false;
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripButton5.Size = new System.Drawing.Size(32, 32);
            this.toolStripButton5.Text = "Добавить канал";
            this.toolStripButton5.Click += new System.EventHandler(this.addLineButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.AutoSize = false;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripButton3.Size = new System.Drawing.Size(32, 32);
            this.toolStripButton3.Text = "Вставить каналы";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripButton2.Size = new System.Drawing.Size(32, 32);
            this.toolStripButton2.Text = "Копировать выделенные каналы";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripButton1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripButton1.Size = new System.Drawing.Size(32, 32);
            this.toolStripButton1.Text = "Удалить выделенные каналы";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "№";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(576, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 15);
            this.label13.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(446, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 15);
            this.label12.TabIndex = 2;
            this.label12.Text = "Формат(Мин./Макс.)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(337, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 15);
            this.label11.TabIndex = 1;
            this.label11.Text = "Адрес(Размерн.)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(186, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Параметр (фаза, линия)";
            // 
            // configPanel
            // 
            this.configPanel.AutoScroll = true;
            this.configPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.configPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.configPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configPanel.Location = new System.Drawing.Point(0, 226);
            this.configPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(885, 229);
            this.configPanel.TabIndex = 25;
            // 
            // SCPrintDialog
            // 
            this.SCPrintDialog.UseEXDialog = true;
            // 
            // SCPrintDocument
            // 
            this.SCPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.SCPrintDocument_PrintPage);
            // 
            // SCPrintPreviewDialog
            // 
            this.SCPrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.SCPrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.SCPrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.SCPrintPreviewDialog.Enabled = true;
            this.SCPrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("SCPrintPreviewDialog.Icon")));
            this.SCPrintPreviewDialog.Name = "SCPrintPreviewDialog";
            this.SCPrintPreviewDialog.Visible = false;
            // 
            // ConfigToSystem_label
            // 
            this.ConfigToSystem_label.AutoSize = true;
            this.ConfigToSystem_label.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ConfigToSystem_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConfigToSystem_label.Location = new System.Drawing.Point(427, 9);
            this.ConfigToSystem_label.Name = "ConfigToSystem_label";
            this.ConfigToSystem_label.Size = new System.Drawing.Size(127, 16);
            this.ConfigToSystem_label.TabIndex = 34;
            this.ConfigToSystem_label.Text = "Actual configuration:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(35, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Группа(Тип)";
            // 
            // ScopeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(885, 477);
            this.Controls.Add(this.ConfigToSystem_label);
            this.Controls.Add(this.configPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mailToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScopeConfigForm";
            this.Text = "Конфигурация системы";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mailToolStrip.ResumeLayout(false);
            this.mailToolStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mailToolStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.TextBox nominalFrequency_textBox;
        private System.Windows.Forms.Label nominalFrequency_label;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox OscilSizeData_TextBox;
        private System.Windows.Forms.Label OscilSizeData_label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox leapsec_textBox;
        private System.Windows.Forms.TextBox timeCode_textBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tmqCode_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox localCode_textBox;
        private System.Windows.Forms.TextBox recordingDevice_textBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox stationName_textBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.TextBox sampleRate_textBox;
        private System.Windows.Forms.Label sampleRate_label;
        private System.Windows.Forms.PrintDialog SCPrintDialog;
        private System.Drawing.Printing.PrintDocument SCPrintDocument;
        private System.Windows.Forms.PrintPreviewDialog SCPrintPreviewDialog;
        private System.Windows.Forms.RichTextBox CommentRichTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton View_toolStripButton;
        private System.Windows.Forms.ToolStripButton Print_toolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton Update_toolStripButton;
        private System.Windows.Forms.ToolStripButton SetDefault_toolStripButton;
        private System.Windows.Forms.TextBox ConfigAddr_textBox;
        private System.Windows.Forms.Label ConfigAddr_label;
        private System.Windows.Forms.TextBox OscilCmndAddr_textBox;
        private System.Windows.Forms.Label OscilCmndAddr_label;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label ConfigToSystem_label;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
    }
}

