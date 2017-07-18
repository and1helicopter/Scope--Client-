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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcSettings));
			this.formats_tabControl = new System.Windows.Forms.TabControl();
			this.formats_tab = new System.Windows.Forms.TabPage();
			this.info_format_label = new System.Windows.Forms.Label();
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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.formats_tabControl.SuspendLayout();
			this.formats_tab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// formats_tabControl
			// 
			this.formats_tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.formats_tabControl.Controls.Add(this.formats_tab);
			this.formats_tabControl.Controls.Add(this.tabPage2);
			this.formats_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formats_tabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.formats_tabControl.Location = new System.Drawing.Point(0, 0);
			this.formats_tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.formats_tabControl.Name = "formats_tabControl";
			this.formats_tabControl.SelectedIndex = 0;
			this.formats_tabControl.Size = new System.Drawing.Size(687, 486);
			this.formats_tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.formats_tabControl.TabIndex = 3;
			// 
			// formats_tab
			// 
			this.formats_tab.Controls.Add(this.info_format_label);
			this.formats_tab.Controls.Add(this.FormatsdataGridView);
			this.formats_tab.Controls.Add(this.toolStrip1);
			this.formats_tab.Location = new System.Drawing.Point(4, 4);
			this.formats_tab.Margin = new System.Windows.Forms.Padding(0);
			this.formats_tab.Name = "formats_tab";
			this.formats_tab.Padding = new System.Windows.Forms.Padding(3);
			this.formats_tab.Size = new System.Drawing.Size(679, 460);
			this.formats_tab.TabIndex = 0;
			this.formats_tab.Text = "Форматы";
			this.formats_tab.UseVisualStyleBackColor = true;
			// 
			// info_format_label
			// 
			this.info_format_label.AutoSize = true;
			this.info_format_label.BackColor = System.Drawing.SystemColors.ControlLight;
			this.info_format_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.info_format_label.Location = new System.Drawing.Point(283, 16);
			this.info_format_label.Name = "info_format_label";
			this.info_format_label.Size = new System.Drawing.Size(103, 15);
			this.info_format_label.TabIndex = 9;
			this.info_format_label.Text = "info_format_label";
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
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.FormatsdataGridView.DefaultCellStyle = dataGridViewCellStyle1;
			this.FormatsdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FormatsdataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
			this.FormatsdataGridView.Location = new System.Drawing.Point(3, 41);
			this.FormatsdataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.FormatsdataGridView.Name = "FormatsdataGridView";
			this.FormatsdataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.FormatsdataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.FormatsdataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.FormatsdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.FormatsdataGridView.Size = new System.Drawing.Size(673, 416);
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
            this.old_toolStripButton});
			this.toolStrip1.Location = new System.Drawing.Point(3, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new System.Drawing.Size(673, 38);
			this.toolStrip1.TabIndex = 7;
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
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(679, 460);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// UcSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.Controls.Add(this.formats_tabControl);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UcSettings";
			this.Size = new System.Drawing.Size(687, 486);
			this.formats_tabControl.ResumeLayout(false);
			this.formats_tab.ResumeLayout(false);
			this.formats_tab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl formats_tabControl;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView FormatsdataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridViewComboBoxColumn bitCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn aCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn bCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn smallerCol;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton add_toolStripButton;
		private System.Windows.Forms.ToolStripButton remove_toolStripButton;
		private System.Windows.Forms.ToolStripButton open_toolStripButton;
		private System.Windows.Forms.ToolStripButton save_toolStripButton;
		private System.Windows.Forms.ToolStripButton old_toolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.TabPage formats_tab;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Label info_format_label;
	}
}
