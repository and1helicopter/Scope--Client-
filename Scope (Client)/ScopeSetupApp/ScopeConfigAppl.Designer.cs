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
            this.addLineButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.nominalFrequency_label = new System.Windows.Forms.Label();
            this.nominalFrequency_textBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.FlagNeed_ConfigTextBox = new System.Windows.Forms.TextBox();
            this.OscilSizeData_TextBox = new System.Windows.Forms.TextBox();
            this.FlagNeed_label = new System.Windows.Forms.Label();
            this.OscilSizeData_label = new System.Windows.Forms.Label();
            this.timeStampTextBox = new System.Windows.Forms.TextBox();
            this.TimeStamp_label = new System.Windows.Forms.Label();
            this.OscilFreq_TextBox = new System.Windows.Forms.TextBox();
            this.OscilFreq_label = new System.Windows.Forms.Label();
            this.History_TextBox = new System.Windows.Forms.TextBox();
            this.History_label = new System.Windows.Forms.Label();
            this.NewConfig_TextBox = new System.Windows.Forms.TextBox();
            this.NewConfig_label = new System.Windows.Forms.Label();
            this.OscilLoad_TextBox = new System.Windows.Forms.TextBox();
            this.OscilLoad_label = new System.Windows.Forms.Label();
            this.ScopeCount_TextBox = new System.Windows.Forms.TextBox();
            this.ScopeCount_label = new System.Windows.Forms.Label();
            this.StartTemp_TextBox = new System.Windows.Forms.TextBox();
            this.StartTemp_label = new System.Windows.Forms.Label();
            this.ChannelCount_TextBox = new System.Windows.Forms.TextBox();
            this.ChannelCount_label = new System.Windows.Forms.Label();
            this.OscilStatus_TextBox = new System.Windows.Forms.TextBox();
            this.OscilStatus_label = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.sampleRate_textBox = new System.Windows.Forms.TextBox();
            this.sampleRate_label = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.configPanel = new System.Windows.Forms.Panel();
            this.mailToolStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.addLineButton});
            this.mailToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mailToolStrip.Name = "mailToolStrip";
            this.mailToolStrip.Size = new System.Drawing.Size(826, 38);
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
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(32, 32);
            this.saveButton.Text = "Сохранить файл";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // addLineButton
            // 
            this.addLineButton.AutoSize = false;
            this.addLineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addLineButton.Image = ((System.Drawing.Image)(resources.GetObject("addLineButton.Image")));
            this.addLineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addLineButton.Margin = new System.Windows.Forms.Padding(3);
            this.addLineButton.Name = "addLineButton";
            this.addLineButton.Size = new System.Drawing.Size(32, 32);
            this.addLineButton.Text = "Добавить строку";
            this.addLineButton.Click += new System.EventHandler(this.addLineButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(826, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // nominalFrequency_label
            // 
            this.nominalFrequency_label.AutoSize = true;
            this.nominalFrequency_label.Location = new System.Drawing.Point(10, 67);
            this.nominalFrequency_label.Name = "nominalFrequency_label";
            this.nominalFrequency_label.Size = new System.Drawing.Size(140, 17);
            this.nominalFrequency_label.TabIndex = 23;
            this.nominalFrequency_label.Text = "Nominal frequency (Hz)";
            // 
            // nominalFrequency_textBox
            // 
            this.nominalFrequency_textBox.Location = new System.Drawing.Point(156, 64);
            this.nominalFrequency_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nominalFrequency_textBox.Name = "nominalFrequency_textBox";
            this.nominalFrequency_textBox.Size = new System.Drawing.Size(116, 24);
            this.nominalFrequency_textBox.TabIndex = 24;
            this.nominalFrequency_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.ItemSize = new System.Drawing.Size(130, 20);
            this.tabControl1.Location = new System.Drawing.Point(0, 38);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(826, 152);
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
            this.tabPage1.Size = new System.Drawing.Size(818, 124);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Oscil Config";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.FlagNeed_ConfigTextBox);
            this.panel5.Controls.Add(this.OscilSizeData_TextBox);
            this.panel5.Controls.Add(this.FlagNeed_label);
            this.panel5.Controls.Add(this.OscilSizeData_label);
            this.panel5.Controls.Add(this.timeStampTextBox);
            this.panel5.Controls.Add(this.TimeStamp_label);
            this.panel5.Controls.Add(this.OscilFreq_TextBox);
            this.panel5.Controls.Add(this.OscilFreq_label);
            this.panel5.Controls.Add(this.History_TextBox);
            this.panel5.Controls.Add(this.History_label);
            this.panel5.Controls.Add(this.NewConfig_TextBox);
            this.panel5.Controls.Add(this.NewConfig_label);
            this.panel5.Controls.Add(this.OscilLoad_TextBox);
            this.panel5.Controls.Add(this.OscilLoad_label);
            this.panel5.Controls.Add(this.ScopeCount_TextBox);
            this.panel5.Controls.Add(this.ScopeCount_label);
            this.panel5.Controls.Add(this.StartTemp_TextBox);
            this.panel5.Controls.Add(this.StartTemp_label);
            this.panel5.Controls.Add(this.ChannelCount_TextBox);
            this.panel5.Controls.Add(this.ChannelCount_label);
            this.panel5.Controls.Add(this.OscilStatus_TextBox);
            this.panel5.Controls.Add(this.OscilStatus_label);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(818, 124);
            this.panel5.TabIndex = 3;
            // 
            // FlagNeed_ConfigTextBox
            // 
            this.FlagNeed_ConfigTextBox.Location = new System.Drawing.Point(695, 10);
            this.FlagNeed_ConfigTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FlagNeed_ConfigTextBox.Name = "FlagNeed_ConfigTextBox";
            this.FlagNeed_ConfigTextBox.Size = new System.Drawing.Size(116, 24);
            this.FlagNeed_ConfigTextBox.TabIndex = 13;
            this.FlagNeed_ConfigTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OscilSizeData_TextBox
            // 
            this.OscilSizeData_TextBox.Location = new System.Drawing.Point(695, 64);
            this.OscilSizeData_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OscilSizeData_TextBox.Name = "OscilSizeData_TextBox";
            this.OscilSizeData_TextBox.Size = new System.Drawing.Size(116, 24);
            this.OscilSizeData_TextBox.TabIndex = 21;
            this.OscilSizeData_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FlagNeed_label
            // 
            this.FlagNeed_label.Location = new System.Drawing.Point(552, 10);
            this.FlagNeed_label.Name = "FlagNeed_label";
            this.FlagNeed_label.Size = new System.Drawing.Size(137, 26);
            this.FlagNeed_label.TabIndex = 12;
            this.FlagNeed_label.Text = "Flag Need Address";
            this.FlagNeed_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OscilSizeData_label
            // 
            this.OscilSizeData_label.Location = new System.Drawing.Point(547, 62);
            this.OscilSizeData_label.Name = "OscilSizeData_label";
            this.OscilSizeData_label.Size = new System.Drawing.Size(142, 26);
            this.OscilSizeData_label.TabIndex = 22;
            this.OscilSizeData_label.Text = "Oscill Size Data (Kbyte)";
            this.OscilSizeData_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timeStampTextBox
            // 
            this.timeStampTextBox.Location = new System.Drawing.Point(695, 37);
            this.timeStampTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeStampTextBox.Name = "timeStampTextBox";
            this.timeStampTextBox.Size = new System.Drawing.Size(116, 24);
            this.timeStampTextBox.TabIndex = 1;
            this.timeStampTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TimeStamp_label
            // 
            this.TimeStamp_label.Location = new System.Drawing.Point(552, 35);
            this.TimeStamp_label.Name = "TimeStamp_label";
            this.TimeStamp_label.Size = new System.Drawing.Size(134, 26);
            this.TimeStamp_label.TabIndex = 0;
            this.TimeStamp_label.Text = "Time Stamp Address";
            this.TimeStamp_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OscilFreq_TextBox
            // 
            this.OscilFreq_TextBox.Location = new System.Drawing.Point(156, 91);
            this.OscilFreq_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OscilFreq_TextBox.Name = "OscilFreq_TextBox";
            this.OscilFreq_TextBox.Size = new System.Drawing.Size(116, 24);
            this.OscilFreq_TextBox.TabIndex = 19;
            this.OscilFreq_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OscilFreq_label
            // 
            this.OscilFreq_label.Location = new System.Drawing.Point(9, 89);
            this.OscilFreq_label.Name = "OscilFreq_label";
            this.OscilFreq_label.Size = new System.Drawing.Size(144, 26);
            this.OscilFreq_label.TabIndex = 18;
            this.OscilFreq_label.Text = "Oscill Freq Address";
            this.OscilFreq_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // History_TextBox
            // 
            this.History_TextBox.Location = new System.Drawing.Point(156, 64);
            this.History_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.History_TextBox.Name = "History_TextBox";
            this.History_TextBox.Size = new System.Drawing.Size(116, 24);
            this.History_TextBox.TabIndex = 17;
            this.History_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // History_label
            // 
            this.History_label.Location = new System.Drawing.Point(6, 62);
            this.History_label.Name = "History_label";
            this.History_label.Size = new System.Drawing.Size(147, 26);
            this.History_label.TabIndex = 16;
            this.History_label.Text = "History Address";
            this.History_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NewConfig_TextBox
            // 
            this.NewConfig_TextBox.Location = new System.Drawing.Point(424, 91);
            this.NewConfig_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewConfig_TextBox.Name = "NewConfig_TextBox";
            this.NewConfig_TextBox.Size = new System.Drawing.Size(116, 24);
            this.NewConfig_TextBox.TabIndex = 15;
            this.NewConfig_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NewConfig_label
            // 
            this.NewConfig_label.Location = new System.Drawing.Point(281, 88);
            this.NewConfig_label.Name = "NewConfig_label";
            this.NewConfig_label.Size = new System.Drawing.Size(137, 26);
            this.NewConfig_label.TabIndex = 14;
            this.NewConfig_label.Text = "New Config Address";
            this.NewConfig_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OscilLoad_TextBox
            // 
            this.OscilLoad_TextBox.Location = new System.Drawing.Point(424, 64);
            this.OscilLoad_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OscilLoad_TextBox.Name = "OscilLoad_TextBox";
            this.OscilLoad_TextBox.Size = new System.Drawing.Size(116, 24);
            this.OscilLoad_TextBox.TabIndex = 11;
            this.OscilLoad_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OscilLoad_label
            // 
            this.OscilLoad_label.Location = new System.Drawing.Point(276, 62);
            this.OscilLoad_label.Name = "OscilLoad_label";
            this.OscilLoad_label.Size = new System.Drawing.Size(142, 26);
            this.OscilLoad_label.TabIndex = 10;
            this.OscilLoad_label.Text = "Oscil Load Address";
            this.OscilLoad_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScopeCount_TextBox
            // 
            this.ScopeCount_TextBox.Location = new System.Drawing.Point(156, 10);
            this.ScopeCount_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ScopeCount_TextBox.Name = "ScopeCount_TextBox";
            this.ScopeCount_TextBox.Size = new System.Drawing.Size(116, 24);
            this.ScopeCount_TextBox.TabIndex = 9;
            this.ScopeCount_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ScopeCount_label
            // 
            this.ScopeCount_label.Location = new System.Drawing.Point(6, 10);
            this.ScopeCount_label.Name = "ScopeCount_label";
            this.ScopeCount_label.Size = new System.Drawing.Size(147, 26);
            this.ScopeCount_label.TabIndex = 8;
            this.ScopeCount_label.Text = "Scope Count Address";
            this.ScopeCount_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StartTemp_TextBox
            // 
            this.StartTemp_TextBox.Location = new System.Drawing.Point(424, 37);
            this.StartTemp_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.StartTemp_TextBox.Name = "StartTemp_TextBox";
            this.StartTemp_TextBox.Size = new System.Drawing.Size(116, 24);
            this.StartTemp_TextBox.TabIndex = 7;
            this.StartTemp_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // StartTemp_label
            // 
            this.StartTemp_label.Location = new System.Drawing.Point(281, 35);
            this.StartTemp_label.Name = "StartTemp_label";
            this.StartTemp_label.Size = new System.Drawing.Size(137, 26);
            this.StartTemp_label.TabIndex = 6;
            this.StartTemp_label.Text = "Start Temp Address";
            this.StartTemp_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ChannelCount_TextBox
            // 
            this.ChannelCount_TextBox.Location = new System.Drawing.Point(156, 37);
            this.ChannelCount_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChannelCount_TextBox.Name = "ChannelCount_TextBox";
            this.ChannelCount_TextBox.Size = new System.Drawing.Size(116, 24);
            this.ChannelCount_TextBox.TabIndex = 5;
            this.ChannelCount_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ChannelCount_label
            // 
            this.ChannelCount_label.Location = new System.Drawing.Point(6, 35);
            this.ChannelCount_label.Name = "ChannelCount_label";
            this.ChannelCount_label.Size = new System.Drawing.Size(147, 26);
            this.ChannelCount_label.TabIndex = 4;
            this.ChannelCount_label.Text = "Channel Count Address";
            this.ChannelCount_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OscilStatus_TextBox
            // 
            this.OscilStatus_TextBox.Location = new System.Drawing.Point(424, 10);
            this.OscilStatus_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OscilStatus_TextBox.Name = "OscilStatus_TextBox";
            this.OscilStatus_TextBox.Size = new System.Drawing.Size(116, 24);
            this.OscilStatus_TextBox.TabIndex = 3;
            this.OscilStatus_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OscilStatus_label
            // 
            this.OscilStatus_label.Location = new System.Drawing.Point(278, 10);
            this.OscilStatus_label.Name = "OscilStatus_label";
            this.OscilStatus_label.Size = new System.Drawing.Size(140, 26);
            this.OscilStatus_label.TabIndex = 2;
            this.OscilStatus_label.Text = "Oscill Status Address";
            this.OscilStatus_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.recordingDevice_textBox);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.stationName_textBox);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.sampleRate_textBox);
            this.tabPage2.Controls.Add(this.sampleRate_label);
            this.tabPage2.Controls.Add(this.nominalFrequency_textBox);
            this.tabPage2.Controls.Add(this.nominalFrequency_label);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(818, 124);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = " COMETRADE Config";
            // 
            // recordingDevice_textBox
            // 
            this.recordingDevice_textBox.Location = new System.Drawing.Point(156, 37);
            this.recordingDevice_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.recordingDevice_textBox.Name = "recordingDevice_textBox";
            this.recordingDevice_textBox.Size = new System.Drawing.Size(116, 24);
            this.recordingDevice_textBox.TabIndex = 39;
            this.recordingDevice_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(48, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 17);
            this.label9.TabIndex = 38;
            this.label9.Text = "Recording device";
            // 
            // stationName_textBox
            // 
            this.stationName_textBox.Location = new System.Drawing.Point(156, 10);
            this.stationName_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stationName_textBox.Name = "stationName_textBox";
            this.stationName_textBox.Size = new System.Drawing.Size(116, 24);
            this.stationName_textBox.TabIndex = 37;
            this.stationName_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(68, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 17);
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
            this.groupBox1.Size = new System.Drawing.Size(268, 118);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "For rev. 2013";
            // 
            // leapsec_textBox
            // 
            this.leapsec_textBox.Location = new System.Drawing.Point(146, 91);
            this.leapsec_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.leapsec_textBox.Name = "leapsec_textBox";
            this.leapsec_textBox.Size = new System.Drawing.Size(116, 24);
            this.leapsec_textBox.TabIndex = 34;
            this.leapsec_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // timeCode_textBox
            // 
            this.timeCode_textBox.Location = new System.Drawing.Point(146, 10);
            this.timeCode_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeCode_textBox.Name = "timeCode_textBox";
            this.timeCode_textBox.Size = new System.Drawing.Size(116, 24);
            this.timeCode_textBox.TabIndex = 28;
            this.timeCode_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(89, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 17);
            this.label7.TabIndex = 33;
            this.label7.Text = "leapsec";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 27;
            this.label6.Text = "Time Code";
            // 
            // tmqCode_textBox
            // 
            this.tmqCode_textBox.Location = new System.Drawing.Point(146, 64);
            this.tmqCode_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tmqCode_textBox.Name = "tmqCode_textBox";
            this.tmqCode_textBox.Size = new System.Drawing.Size(116, 24);
            this.tmqCode_textBox.TabIndex = 32;
            this.tmqCode_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "Local Code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(78, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 17);
            this.label8.TabIndex = 31;
            this.label8.Text = "tmq Code";
            // 
            // localCode_textBox
            // 
            this.localCode_textBox.Location = new System.Drawing.Point(146, 37);
            this.localCode_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.localCode_textBox.Name = "localCode_textBox";
            this.localCode_textBox.Size = new System.Drawing.Size(116, 24);
            this.localCode_textBox.TabIndex = 30;
            this.localCode_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sampleRate_textBox
            // 
            this.sampleRate_textBox.Location = new System.Drawing.Point(156, 91);
            this.sampleRate_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sampleRate_textBox.Name = "sampleRate_textBox";
            this.sampleRate_textBox.Size = new System.Drawing.Size(116, 24);
            this.sampleRate_textBox.TabIndex = 26;
            this.sampleRate_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sampleRate_label
            // 
            this.sampleRate_label.AutoSize = true;
            this.sampleRate_label.Location = new System.Drawing.Point(48, 94);
            this.sampleRate_label.Name = "sampleRate_label";
            this.sampleRate_label.Size = new System.Drawing.Size(102, 17);
            this.sampleRate_label.TabIndex = 25;
            this.sampleRate_label.Text = "Sample rate (Hz)";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(818, 124);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Save to Memory";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 190);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 24);
            this.panel2.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(582, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Мин./Макс.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Тип";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(234, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Размерн.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(492, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 17);
            this.label13.TabIndex = 3;
            this.label13.Text = "Сглаж.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(387, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Формат";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(300, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 17);
            this.label11.TabIndex = 1;
            this.label11.Text = "Адрес";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(82, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(151, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Параметр (фаза, линия)";
            // 
            // configPanel
            // 
            this.configPanel.AutoScroll = true;
            this.configPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.configPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.configPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configPanel.Location = new System.Drawing.Point(0, 214);
            this.configPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(826, 226);
            this.configPanel.TabIndex = 25;
            // 
            // ScopeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(826, 462);
            this.Controls.Add(this.configPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mailToolStrip);
            this.Font = new System.Drawing.Font("Open Sans", 9F);
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mailToolStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton addLineButton;
        private System.Windows.Forms.TextBox nominalFrequency_textBox;
        private System.Windows.Forms.Label nominalFrequency_label;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox FlagNeed_ConfigTextBox;
        private System.Windows.Forms.TextBox OscilSizeData_TextBox;
        private System.Windows.Forms.Label FlagNeed_label;
        private System.Windows.Forms.Label OscilSizeData_label;
        private System.Windows.Forms.TextBox timeStampTextBox;
        private System.Windows.Forms.Label TimeStamp_label;
        private System.Windows.Forms.TextBox OscilFreq_TextBox;
        private System.Windows.Forms.Label OscilFreq_label;
        private System.Windows.Forms.TextBox History_TextBox;
        private System.Windows.Forms.Label History_label;
        private System.Windows.Forms.TextBox NewConfig_TextBox;
        private System.Windows.Forms.Label NewConfig_label;
        private System.Windows.Forms.TextBox OscilLoad_TextBox;
        private System.Windows.Forms.Label OscilLoad_label;
        private System.Windows.Forms.TextBox ScopeCount_TextBox;
        private System.Windows.Forms.Label ScopeCount_label;
        private System.Windows.Forms.TextBox StartTemp_TextBox;
        private System.Windows.Forms.Label StartTemp_label;
        private System.Windows.Forms.TextBox ChannelCount_TextBox;
        private System.Windows.Forms.Label ChannelCount_label;
        private System.Windows.Forms.TextBox OscilStatus_TextBox;
        private System.Windows.Forms.Label OscilStatus_label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.TextBox sampleRate_textBox;
        private System.Windows.Forms.Label sampleRate_label;
        private System.Windows.Forms.TabPage tabPage3;
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
    }
}

