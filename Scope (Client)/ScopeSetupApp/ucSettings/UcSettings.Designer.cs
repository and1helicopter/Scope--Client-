namespace ScopeSetupApp.ucSettings
{
	partial class UcSettings
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcSettings));
			this.formats_tabControl = new System.Windows.Forms.TabControl();
			this.formats_tab = new System.Windows.Forms.TabPage();
			this.FormatsdataGridView = new System.Windows.Forms.DataGridView();
			this.nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bitCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.aCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.smallerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.open_toolStripButton = new System.Windows.Forms.ToolStripButton();
			this.save_toolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.add_toolStripButton = new System.Windows.Forms.ToolStripButton();
			this.remove_toolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.old_toolStripButton = new System.Windows.Forms.ToolStripButton();
			this.info_format_label = new System.Windows.Forms.ToolStripLabel();
			this.Config_tab = new System.Windows.Forms.TabPage();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.configToSystem_label = new System.Windows.Forms.Label();
			this.ConfigAddr_textBox = new System.Windows.Forms.TextBox();
			this.ConfigAddr_label = new System.Windows.Forms.Label();
			this.OscilCmndAddr_textBox = new System.Windows.Forms.TextBox();
			this.OscilCmndAddr_label = new System.Windows.Forms.Label();
			this.updateConfig_button = new System.Windows.Forms.Button();
			this.formats_tabControl.SuspendLayout();
			this.formats_tab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.Config_tab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// formats_tabControl
			// 
			this.formats_tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.formats_tabControl.Controls.Add(this.formats_tab);
			this.formats_tabControl.Controls.Add(this.Config_tab);
			this.formats_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formats_tabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.formats_tabControl.Location = new System.Drawing.Point(1, 1);
			this.formats_tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.formats_tabControl.Name = "formats_tabControl";
			this.formats_tabControl.SelectedIndex = 0;
			this.formats_tabControl.Size = new System.Drawing.Size(661, 484);
			this.formats_tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.formats_tabControl.TabIndex = 3;
			// 
			// formats_tab
			// 
			this.formats_tab.Controls.Add(this.FormatsdataGridView);
			this.formats_tab.Controls.Add(this.toolStrip1);
			this.formats_tab.Location = new System.Drawing.Point(4, 4);
			this.formats_tab.Margin = new System.Windows.Forms.Padding(0);
			this.formats_tab.Name = "formats_tab";
			this.formats_tab.Padding = new System.Windows.Forms.Padding(3);
			this.formats_tab.Size = new System.Drawing.Size(653, 458);
			this.formats_tab.TabIndex = 0;
			this.formats_tab.Text = "Форматы";
			this.formats_tab.UseVisualStyleBackColor = true;
			// 
			// FormatsdataGridView
			// 
			this.FormatsdataGridView.AllowUserToAddRows = false;
			this.FormatsdataGridView.AllowUserToDeleteRows = false;
			this.FormatsdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.FormatsdataGridView.BackgroundColor = System.Drawing.SystemColors.HighlightText;
			this.FormatsdataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.FormatsdataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
			this.FormatsdataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.FormatsdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.FormatsdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameCol,
            this.bitCol,
            this.aCol,
            this.bCol,
            this.smallerCol});
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.FormatsdataGridView.DefaultCellStyle = dataGridViewCellStyle6;
			this.FormatsdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FormatsdataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
			this.FormatsdataGridView.Location = new System.Drawing.Point(3, 41);
			this.FormatsdataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.FormatsdataGridView.Name = "FormatsdataGridView";
			this.FormatsdataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.FormatsdataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.FormatsdataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.FormatsdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.FormatsdataGridView.Size = new System.Drawing.Size(647, 414);
			this.FormatsdataGridView.TabIndex = 8;
			this.FormatsdataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.FormatsdataGridView_CellValidated);
			this.FormatsdataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.FormatsdataGridView_RowEnter);
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Название";
			this.nameCol.Name = "nameCol";
			this.nameCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// bitCol
			// 
			this.bitCol.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.bitCol.HeaderText = "Формат входных данных";
			this.bitCol.Items.AddRange(new object[] {
            "int16",
            "uint16",
            "int32",
            "uint32",
            "int64",
            "uint64"});
			this.bitCol.Name = "bitCol";
			// 
			// aCol
			// 
			this.aCol.HeaderText = "A";
			this.aCol.Name = "aCol";
			this.aCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// bCol
			// 
			this.bCol.HeaderText = "B";
			this.bCol.Name = "bCol";
			this.bCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// smallerCol
			// 
			this.smallerCol.HeaderText = "Число знаков после запятой";
			this.smallerCol.Name = "smallerCol";
			this.smallerCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open_toolStripButton,
            this.save_toolStripButton,
            this.toolStripSeparator1,
            this.add_toolStripButton,
            this.remove_toolStripButton,
            this.toolStripSeparator2,
            this.old_toolStripButton,
            this.info_format_label});
			this.toolStrip1.Location = new System.Drawing.Point(3, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new System.Drawing.Size(647, 38);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// open_toolStripButton
			// 
			this.open_toolStripButton.AutoSize = false;
			this.open_toolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.open_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.open_toolStripButton.Image = global::ScopeSetupApp.Properties.Resources.Open_Folder_50;
			this.open_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.open_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
			this.open_toolStripButton.Name = "open_toolStripButton";
			this.open_toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.open_toolStripButton.Size = new System.Drawing.Size(32, 32);
			this.open_toolStripButton.Text = "Открыть файл с форматами";
			this.open_toolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
			this.open_toolStripButton.Click += new System.EventHandler(this.openFormatButton_Click);
			// 
			// save_toolStripButton
			// 
			this.save_toolStripButton.AutoSize = false;
			this.save_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.save_toolStripButton.Image = global::ScopeSetupApp.Properties.Resources.Save_as_50;
			this.save_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.save_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
			this.save_toolStripButton.Name = "save_toolStripButton";
			this.save_toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.save_toolStripButton.Size = new System.Drawing.Size(32, 32);
			this.save_toolStripButton.Text = "Сохранить форматы в файл";
			this.save_toolStripButton.Click += new System.EventHandler(this.saveFormatButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
			// 
			// add_toolStripButton
			// 
			this.add_toolStripButton.AutoSize = false;
			this.add_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.add_toolStripButton.Image = global::ScopeSetupApp.Properties.Resources.Plus_50;
			this.add_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.add_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
			this.add_toolStripButton.Name = "add_toolStripButton";
			this.add_toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.add_toolStripButton.Size = new System.Drawing.Size(32, 32);
			this.add_toolStripButton.Text = "Добавить формат";
			this.add_toolStripButton.Click += new System.EventHandler(this.addFormatButton_Click);
			// 
			// remove_toolStripButton
			// 
			this.remove_toolStripButton.AutoSize = false;
			this.remove_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.remove_toolStripButton.Image = global::ScopeSetupApp.Properties.Resources.Cancel_50;
			this.remove_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.remove_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
			this.remove_toolStripButton.Name = "remove_toolStripButton";
			this.remove_toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.remove_toolStripButton.Size = new System.Drawing.Size(32, 32);
			this.remove_toolStripButton.Text = "Удалить формат";
			this.remove_toolStripButton.Click += new System.EventHandler(this.removeFormatButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
			// 
			// old_toolStripButton
			// 
			this.old_toolStripButton.AutoSize = false;
			this.old_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.old_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("old_toolStripButton.Image")));
			this.old_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.old_toolStripButton.Margin = new System.Windows.Forms.Padding(3);
			this.old_toolStripButton.Name = "old_toolStripButton";
			this.old_toolStripButton.Size = new System.Drawing.Size(32, 32);
			this.old_toolStripButton.Text = "toolStripButton5";
			this.old_toolStripButton.Click += new System.EventHandler(this.oldFormat_checkBox_Click);
			// 
			// info_format_label
			// 
			this.info_format_label.Name = "info_format_label";
			this.info_format_label.Size = new System.Drawing.Size(99, 35);
			this.info_format_label.Text = "info_format_label";
			// 
			// Config_tab
			// 
			this.Config_tab.Controls.Add(this.groupBox1);
			this.Config_tab.Controls.Add(this.label8);
			this.Config_tab.Controls.Add(this.numericUpDown1);
			this.Config_tab.Location = new System.Drawing.Point(4, 4);
			this.Config_tab.Margin = new System.Windows.Forms.Padding(0);
			this.Config_tab.Name = "Config_tab";
			this.Config_tab.Padding = new System.Windows.Forms.Padding(3);
			this.Config_tab.Size = new System.Drawing.Size(653, 458);
			this.Config_tab.TabIndex = 1;
			this.Config_tab.Text = "Конфигурация";
			this.Config_tab.UseVisualStyleBackColor = true;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(385, 42);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown1.TabIndex = 47;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(382, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(168, 13);
			this.label8.TabIndex = 48;
			this.label8.Text = "Метка бита загрузки в статусе:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.configToSystem_label);
			this.groupBox1.Controls.Add(this.ConfigAddr_textBox);
			this.groupBox1.Controls.Add(this.ConfigAddr_label);
			this.groupBox1.Controls.Add(this.OscilCmndAddr_textBox);
			this.groupBox1.Controls.Add(this.OscilCmndAddr_label);
			this.groupBox1.Controls.Add(this.updateConfig_button);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(267, 452);
			this.groupBox1.TabIndex = 49;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Конфигурация в системе";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 148);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 59;
			this.label1.Text = "Предыстории:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 131);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 13);
			this.label6.TabIndex = 58;
			this.label6.Text = "Каналов:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 115);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(87, 13);
			this.label7.TabIndex = 57;
			this.label7.Text = "Осциллограмм:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(106, 148);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 13);
			this.label5.TabIndex = 56;
			this.label5.Text = "label5";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 166);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 55;
			this.label4.Text = "label4";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(106, 131);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 54;
			this.label3.Text = "label3";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(106, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 53;
			this.label2.Text = "label2";
			// 
			// configToSystem_label
			// 
			this.configToSystem_label.AutoSize = true;
			this.configToSystem_label.Location = new System.Drawing.Point(13, 98);
			this.configToSystem_label.Name = "configToSystem_label";
			this.configToSystem_label.Size = new System.Drawing.Size(138, 13);
			this.configToSystem_label.TabIndex = 52;
			this.configToSystem_label.Text = "Конфигурация в системе:";
			// 
			// ConfigAddr_textBox
			// 
			this.ConfigAddr_textBox.Location = new System.Drawing.Point(138, 20);
			this.ConfigAddr_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ConfigAddr_textBox.Name = "ConfigAddr_textBox";
			this.ConfigAddr_textBox.Size = new System.Drawing.Size(116, 20);
			this.ConfigAddr_textBox.TabIndex = 51;
			this.ConfigAddr_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ConfigAddr_label
			// 
			this.ConfigAddr_label.Location = new System.Drawing.Point(6, 16);
			this.ConfigAddr_label.Name = "ConfigAddr_label";
			this.ConfigAddr_label.Size = new System.Drawing.Size(115, 26);
			this.ConfigAddr_label.TabIndex = 50;
			this.ConfigAddr_label.Text = "Configuration Address";
			this.ConfigAddr_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// OscilCmndAddr_textBox
			// 
			this.OscilCmndAddr_textBox.Location = new System.Drawing.Point(138, 43);
			this.OscilCmndAddr_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.OscilCmndAddr_textBox.Name = "OscilCmndAddr_textBox";
			this.OscilCmndAddr_textBox.Size = new System.Drawing.Size(116, 20);
			this.OscilCmndAddr_textBox.TabIndex = 49;
			this.OscilCmndAddr_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// OscilCmndAddr_label
			// 
			this.OscilCmndAddr_label.Location = new System.Drawing.Point(10, 40);
			this.OscilCmndAddr_label.Name = "OscilCmndAddr_label";
			this.OscilCmndAddr_label.Size = new System.Drawing.Size(111, 26);
			this.OscilCmndAddr_label.TabIndex = 48;
			this.OscilCmndAddr_label.Text = "Oscil Cmnd Address";
			this.OscilCmndAddr_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// updateConfig_button
			// 
			this.updateConfig_button.Location = new System.Drawing.Point(152, 70);
			this.updateConfig_button.Name = "updateConfig_button";
			this.updateConfig_button.Size = new System.Drawing.Size(88, 25);
			this.updateConfig_button.TabIndex = 47;
			this.updateConfig_button.Text = "Обновить";
			this.updateConfig_button.UseVisualStyleBackColor = true;
			// 
			// UcSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.Controls.Add(this.formats_tabControl);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UcSettings";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(663, 486);
			this.formats_tabControl.ResumeLayout(false);
			this.formats_tab.ResumeLayout(false);
			this.formats_tab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.Config_tab.ResumeLayout(false);
			this.Config_tab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl formats_tabControl;
		private System.Windows.Forms.TabPage Config_tab;
		private System.Windows.Forms.DataGridView FormatsdataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridViewComboBoxColumn bitCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn aCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn bCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn smallerCol;
		private System.Windows.Forms.TabPage formats_tab;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton open_toolStripButton;
		private System.Windows.Forms.ToolStripButton save_toolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton add_toolStripButton;
		private System.Windows.Forms.ToolStripButton remove_toolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton old_toolStripButton;
		private System.Windows.Forms.ToolStripLabel info_format_label;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label configToSystem_label;
		private System.Windows.Forms.TextBox ConfigAddr_textBox;
		private System.Windows.Forms.Label ConfigAddr_label;
		private System.Windows.Forms.TextBox OscilCmndAddr_textBox;
		private System.Windows.Forms.Label OscilCmndAddr_label;
		private System.Windows.Forms.Button updateConfig_button;
	}
}
