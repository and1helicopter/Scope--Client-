namespace ScopeSetupApp.Format
{
	partial class Settings
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.removeFormatButton = new System.Windows.Forms.Button();
			this.addFormatButton = new System.Windows.Forms.Button();
			this.oldFormat_checkBox = new System.Windows.Forms.CheckBox();
			this.FormatsdataGridView = new System.Windows.Forms.DataGridView();
			this.nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bitCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.aCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.smallerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.openFormatButton = new System.Windows.Forms.Button();
			this.saveFormatButton = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1001, 488);
			this.tabControl1.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.removeFormatButton);
			this.tabPage1.Controls.Add(this.addFormatButton);
			this.tabPage1.Controls.Add(this.oldFormat_checkBox);
			this.tabPage1.Controls.Add(this.FormatsdataGridView);
			this.tabPage1.Controls.Add(this.openFormatButton);
			this.tabPage1.Controls.Add(this.saveFormatButton);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(993, 462);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// removeFormatButton
			// 
			this.removeFormatButton.Location = new System.Drawing.Point(8, 418);
			this.removeFormatButton.Name = "removeFormatButton";
			this.removeFormatButton.Size = new System.Drawing.Size(75, 23);
			this.removeFormatButton.TabIndex = 5;
			this.removeFormatButton.Text = "rem";
			this.removeFormatButton.UseVisualStyleBackColor = true;
			// 
			// addFormatButton
			// 
			this.addFormatButton.Location = new System.Drawing.Point(8, 389);
			this.addFormatButton.Name = "addFormatButton";
			this.addFormatButton.Size = new System.Drawing.Size(75, 23);
			this.addFormatButton.TabIndex = 4;
			this.addFormatButton.Text = "add";
			this.addFormatButton.UseVisualStyleBackColor = true;
			// 
			// oldFormat_checkBox
			// 
			this.oldFormat_checkBox.AutoSize = true;
			this.oldFormat_checkBox.Location = new System.Drawing.Point(592, 395);
			this.oldFormat_checkBox.Name = "oldFormat_checkBox";
			this.oldFormat_checkBox.Size = new System.Drawing.Size(48, 17);
			this.oldFormat_checkBox.TabIndex = 3;
			this.oldFormat_checkBox.Text = "OLD";
			this.oldFormat_checkBox.UseVisualStyleBackColor = true;
			// 
			// FormatsdataGridView
			// 
			this.FormatsdataGridView.AllowUserToAddRows = false;
			this.FormatsdataGridView.AllowUserToDeleteRows = false;
			this.FormatsdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.FormatsdataGridView.BackgroundColor = System.Drawing.SystemColors.HighlightText;
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
			this.FormatsdataGridView.Dock = System.Windows.Forms.DockStyle.Top;
			this.FormatsdataGridView.Location = new System.Drawing.Point(3, 3);
			this.FormatsdataGridView.Name = "FormatsdataGridView";
			this.FormatsdataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.FormatsdataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.FormatsdataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.FormatsdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.FormatsdataGridView.Size = new System.Drawing.Size(987, 369);
			this.FormatsdataGridView.TabIndex = 2;
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
			// openFormatButton
			// 
			this.openFormatButton.Location = new System.Drawing.Point(170, 391);
			this.openFormatButton.Name = "openFormatButton";
			this.openFormatButton.Size = new System.Drawing.Size(75, 23);
			this.openFormatButton.TabIndex = 0;
			this.openFormatButton.Text = "open";
			this.openFormatButton.UseVisualStyleBackColor = true;
			// 
			// saveFormatButton
			// 
			this.saveFormatButton.Location = new System.Drawing.Point(170, 418);
			this.saveFormatButton.Name = "saveFormatButton";
			this.saveFormatButton.Size = new System.Drawing.Size(75, 23);
			this.saveFormatButton.TabIndex = 1;
			this.saveFormatButton.Text = "save";
			this.saveFormatButton.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(906, 452);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "Settings";
			this.Size = new System.Drawing.Size(1001, 488);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button removeFormatButton;
		private System.Windows.Forms.Button addFormatButton;
		private System.Windows.Forms.CheckBox oldFormat_checkBox;
		private System.Windows.Forms.DataGridView FormatsdataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridViewComboBoxColumn bitCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn aCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn bCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn smallerCol;
		private System.Windows.Forms.Button openFormatButton;
		private System.Windows.Forms.Button saveFormatButton;
		private System.Windows.Forms.TabPage tabPage2;
	}
}
