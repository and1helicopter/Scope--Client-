namespace ScopeSetupApp
{
    partial class ScopeSetupForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopeSetupForm));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.writeToSystemBtn = new System.Windows.Forms.ToolStripButton();
			this.reloadButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.openButton2 = new System.Windows.Forms.ToolStripButton();
			this.saveButton2 = new System.Windows.Forms.ToolStripButton();
			this.StatusDownloadConfig = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.radioButton = new System.Windows.Forms.TextBox();
			this.sizeOcsil_trackBar = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.hystoryRadioButton = new System.Windows.Forms.TextBox();
			this.oscFreqRadioButton = new System.Windows.Forms.TextBox();
			this.CommentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.chCountRadioButton = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.DelayOsc = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.enaScopeCheckBox = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.possibleParamPanel = new System.Windows.Forms.Panel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.label2 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.ConfigToSystem_label = new System.Windows.Forms.Label();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sizeOcsil_trackBar)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.possibleParamPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStrip1.Font = new System.Drawing.Font("Consolas", 9F);
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton2,
            this.saveButton2,
            this.StatusDownloadConfig,
            this.toolStripSeparator1,
            this.writeToSystemBtn,
            this.reloadButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(884, 38);
			this.toolStrip1.TabIndex = 7;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// writeToSystemBtn
			// 
			this.writeToSystemBtn.AutoSize = false;
			this.writeToSystemBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.writeToSystemBtn.Enabled = false;
			this.writeToSystemBtn.Image = ((System.Drawing.Image)(resources.GetObject("writeToSystemBtn.Image")));
			this.writeToSystemBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.writeToSystemBtn.Margin = new System.Windows.Forms.Padding(3);
			this.writeToSystemBtn.Name = "writeToSystemBtn";
			this.writeToSystemBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.writeToSystemBtn.Size = new System.Drawing.Size(32, 32);
			this.writeToSystemBtn.Text = "Загрузиь конфигурацию в систему";
			this.writeToSystemBtn.Click += new System.EventHandler(this.writeToSystemBtn_Click);
			// 
			// reloadButton
			// 
			this.reloadButton.AutoSize = false;
			this.reloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.reloadButton.Enabled = false;
			this.reloadButton.Image = ((System.Drawing.Image)(resources.GetObject("reloadButton.Image")));
			this.reloadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.reloadButton.Margin = new System.Windows.Forms.Padding(3);
			this.reloadButton.Name = "reloadButton";
			this.reloadButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.reloadButton.Size = new System.Drawing.Size(32, 32);
			this.reloadButton.Text = "Считать конфигурацию  из системы";
			this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
			// 
			// openButton2
			// 
			this.openButton2.AutoSize = false;
			this.openButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openButton2.Image = ((System.Drawing.Image)(resources.GetObject("openButton2.Image")));
			this.openButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openButton2.Margin = new System.Windows.Forms.Padding(3);
			this.openButton2.Name = "openButton2";
			this.openButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.openButton2.Size = new System.Drawing.Size(32, 32);
			this.openButton2.Text = "Открыть файл";
			this.openButton2.Click += new System.EventHandler(this.openButton2_Click);
			// 
			// saveButton2
			// 
			this.saveButton2.AutoSize = false;
			this.saveButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveButton2.Image = ((System.Drawing.Image)(resources.GetObject("saveButton2.Image")));
			this.saveButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveButton2.Margin = new System.Windows.Forms.Padding(3);
			this.saveButton2.Name = "saveButton2";
			this.saveButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.saveButton2.Size = new System.Drawing.Size(32, 32);
			this.saveButton2.Text = "Сохранить файл";
			this.saveButton2.Click += new System.EventHandler(this.saveButton2_Click);
			// 
			// StatusDownloadConfig
			// 
			this.StatusDownloadConfig.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.StatusDownloadConfig.AutoSize = false;
			this.StatusDownloadConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.StatusDownloadConfig.Image = global::ScopeSetupApp.Properties.Resources.Circle_Thin_64_2_;
			this.StatusDownloadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.StatusDownloadConfig.Margin = new System.Windows.Forms.Padding(3);
			this.StatusDownloadConfig.Name = "StatusDownloadConfig";
			this.StatusDownloadConfig.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.StatusDownloadConfig.Size = new System.Drawing.Size(32, 32);
			this.StatusDownloadConfig.Text = "toolStripButton1";
			this.StatusDownloadConfig.ToolTipText = "Статус загрузки конфигурации";
			this.StatusDownloadConfig.Visible = false;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.radioButton);
			this.panel1.Controls.Add(this.sizeOcsil_trackBar);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.checkBox3);
			this.panel1.Controls.Add(this.hystoryRadioButton);
			this.panel1.Controls.Add(this.oscFreqRadioButton);
			this.panel1.Controls.Add(this.CommentRichTextBox);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.chCountRadioButton);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.DelayOsc);
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.enaScopeCheckBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 38);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(884, 147);
			this.panel1.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(312, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 15);
			this.label3.TabIndex = 14;
			this.label3.Text = "От 1 до 32";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 9);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(130, 15);
			this.label9.TabIndex = 16;
			this.label9.Text = "Количество каналов:";
			// 
			// radioButton
			// 
			this.radioButton.Enabled = false;
			this.radioButton.Location = new System.Drawing.Point(176, 6);
			this.radioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.radioButton.Name = "radioButton";
			this.radioButton.Size = new System.Drawing.Size(130, 21);
			this.radioButton.TabIndex = 15;
			this.radioButton.Text = "0";
			this.radioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sizeOcsil_trackBar
			// 
			this.sizeOcsil_trackBar.Location = new System.Drawing.Point(727, 8);
			this.sizeOcsil_trackBar.Maximum = 100;
			this.sizeOcsil_trackBar.Minimum = 1;
			this.sizeOcsil_trackBar.Name = "sizeOcsil_trackBar";
			this.sizeOcsil_trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sizeOcsil_trackBar.Size = new System.Drawing.Size(45, 125);
			this.sizeOcsil_trackBar.TabIndex = 13;
			this.sizeOcsil_trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.sizeOcsil_trackBar.Value = 100;
			this.sizeOcsil_trackBar.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 63);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(90, 15);
			this.label8.TabIndex = 3;
			this.label8.Text = "Предыстория:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(312, 90);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(33, 15);
			this.label5.TabIndex = 4;
			this.label5.Text = "От 1";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(312, 36);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(67, 15);
			this.label10.TabIndex = 1;
			this.label10.Text = "От 1 до 32";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label6.Location = new System.Drawing.Point(312, 63);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(81, 15);
			this.label6.TabIndex = 2;
			this.label6.Text = "От 1 до 99 %";
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(411, 62);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(250, 19);
			this.checkBox3.TabIndex = 12;
			this.checkBox3.Text = "Отключить перезапись осциллограмм";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// hystoryRadioButton
			// 
			this.hystoryRadioButton.Location = new System.Drawing.Point(176, 60);
			this.hystoryRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.hystoryRadioButton.Name = "hystoryRadioButton";
			this.hystoryRadioButton.Size = new System.Drawing.Size(130, 21);
			this.hystoryRadioButton.TabIndex = 3;
			this.hystoryRadioButton.Text = "1";
			this.hystoryRadioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.hystoryRadioButton.TextChanged += new System.EventHandler(this.hystoryRadioButton_TextChanged);
			this.hystoryRadioButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hystoryRadioButton_KeyPress);
			// 
			// oscFreqRadioButton
			// 
			this.oscFreqRadioButton.Location = new System.Drawing.Point(176, 87);
			this.oscFreqRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.oscFreqRadioButton.Name = "oscFreqRadioButton";
			this.oscFreqRadioButton.Size = new System.Drawing.Size(130, 21);
			this.oscFreqRadioButton.TabIndex = 4;
			this.oscFreqRadioButton.Text = "1";
			this.oscFreqRadioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.oscFreqRadioButton.TextChanged += new System.EventHandler(this.oscFreqRadioButton_TextChanged);
			this.oscFreqRadioButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.oscFreqRadioButton_KeyPress);
			// 
			// CommentRichTextBox
			// 
			this.CommentRichTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.CommentRichTextBox.Location = new System.Drawing.Point(411, 8);
			this.CommentRichTextBox.Name = "CommentRichTextBox";
			this.CommentRichTextBox.ReadOnly = true;
			this.CommentRichTextBox.Size = new System.Drawing.Size(265, 45);
			this.CommentRichTextBox.TabIndex = 11;
			this.CommentRichTextBox.Text = "";
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 92);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(68, 15);
			this.label7.TabIndex = 7;
			this.label7.Text = "Делитель:";
			// 
			// chCountRadioButton
			// 
			this.chCountRadioButton.Location = new System.Drawing.Point(176, 33);
			this.chCountRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chCountRadioButton.Name = "chCountRadioButton";
			this.chCountRadioButton.Size = new System.Drawing.Size(130, 21);
			this.chCountRadioButton.TabIndex = 1;
			this.chCountRadioButton.Text = "1";
			this.chCountRadioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.chCountRadioButton.TextChanged += new System.EventHandler(this.chCountRadioButton_TextChanged);
			this.chCountRadioButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chCountRadioButton_KeyPress);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 36);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(167, 15);
			this.label11.TabIndex = 3;
			this.label11.Text = "Количество осциллограмм:";
			// 
			// DelayOsc
			// 
			this.DelayOsc.AutoSize = true;
			this.DelayOsc.Location = new System.Drawing.Point(6, 118);
			this.DelayOsc.Name = "DelayOsc";
			this.DelayOsc.Size = new System.Drawing.Size(195, 15);
			this.DelayOsc.TabIndex = 8;
			this.DelayOsc.Text = "Длительность осциллограммы: ";
			this.DelayOsc.Visible = false;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(411, 89);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(265, 19);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "Сохронение осциллограммы на SD карту";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// enaScopeCheckBox
			// 
			this.enaScopeCheckBox.AutoSize = true;
			this.enaScopeCheckBox.Location = new System.Drawing.Point(411, 115);
			this.enaScopeCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.enaScopeCheckBox.Name = "enaScopeCheckBox";
			this.enaScopeCheckBox.Size = new System.Drawing.Size(221, 19);
			this.enaScopeCheckBox.TabIndex = 5;
			this.enaScopeCheckBox.Text = "Осциллографирование включено";
			this.enaScopeCheckBox.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 185);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 276);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.possibleParamPanel);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 4);
			this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(878, 268);
			this.panel2.TabIndex = 2;
			// 
			// possibleParamPanel
			// 
			this.possibleParamPanel.AutoScroll = true;
			this.possibleParamPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.possibleParamPanel.Controls.Add(this.listView1);
			this.possibleParamPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.possibleParamPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.possibleParamPanel.Location = new System.Drawing.Point(0, 35);
			this.possibleParamPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.possibleParamPanel.Name = "possibleParamPanel";
			this.possibleParamPanel.Size = new System.Drawing.Size(878, 233);
			this.possibleParamPanel.TabIndex = 6;
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView1.BackColor = System.Drawing.SystemColors.HighlightText;
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1.CheckBoxes = true;
			this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(878, 233);
			this.listView1.TabIndex = 8;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
			this.listView1.SizeChanged += new System.EventHandler(this.ListView_SizeChanged);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(878, 35);
			this.label2.TabIndex = 4;
			this.label2.Text = "Возможные для осциллографирования параметры";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 250;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
			// 
			// ConfigToSystem_label
			// 
			this.ConfigToSystem_label.AutoSize = true;
			this.ConfigToSystem_label.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ConfigToSystem_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ConfigToSystem_label.Location = new System.Drawing.Point(410, 9);
			this.ConfigToSystem_label.Name = "ConfigToSystem_label";
			this.ConfigToSystem_label.Size = new System.Drawing.Size(127, 16);
			this.ConfigToSystem_label.TabIndex = 35;
			this.ConfigToSystem_label.Text = "Actual configuration:";
			// 
			// ScopeSetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(884, 461);
			this.Controls.Add(this.ConfigToSystem_label);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(900, 500);
			this.Name = "ScopeSetupForm";
			this.Text = "Конфигурация осциллографа";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScopeSetupForm_FormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sizeOcsil_trackBar)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.possibleParamPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel possibleParamPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton writeToSystemBtn;
        private System.Windows.Forms.TextBox chCountRadioButton;
        private System.Windows.Forms.TextBox hystoryRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label DelayOsc;
        private System.Windows.Forms.ToolStripButton openButton2;
        private System.Windows.Forms.ToolStripButton saveButton2;
        protected internal System.Windows.Forms.RichTextBox CommentRichTextBox;
        private System.Windows.Forms.CheckBox enaScopeCheckBox;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ToolStripButton reloadButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox oscFreqRadioButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label ConfigToSystem_label;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TrackBar sizeOcsil_trackBar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox radioButton;
        private System.Windows.Forms.ToolStripButton StatusDownloadConfig;

        /*
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
         */

    }
}