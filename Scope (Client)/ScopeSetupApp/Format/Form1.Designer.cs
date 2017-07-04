namespace ScopeSetupApp.Format
{
	partial class Form1
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.FormatsdataGridView = new System.Windows.Forms.DataGridView();
			this.nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bitCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.aCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.zCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.smallerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 389);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(8, 418);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(914, 478);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.FormatsdataGridView);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(906, 452);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// FormatsdataGridView
			// 
			this.FormatsdataGridView.AllowUserToAddRows = false;
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
			this.zCol,
			this.smallerCol});
			this.FormatsdataGridView.Dock = System.Windows.Forms.DockStyle.Top;
			this.FormatsdataGridView.Location = new System.Drawing.Point(3, 3);
			this.FormatsdataGridView.MultiSelect = false;
			this.FormatsdataGridView.Name = "FormatsdataGridView";
			this.FormatsdataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.FormatsdataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.FormatsdataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.FormatsdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.FormatsdataGridView.Size = new System.Drawing.Size(900, 369);
			this.FormatsdataGridView.TabIndex = 2;
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Название";
			this.nameCol.Name = "nameCol";
			// 
			// bitCol
			// 
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
			// 
			// bCol
			// 
			this.bCol.HeaderText = "B";
			this.bCol.Name = "bCol";
			// 
			// zCol
			// 
			this.zCol.HeaderText = "Z";
			this.zCol.Name = "zCol";
			// 
			// smallerCol
			// 
			this.smallerCol.HeaderText = "Число знаков после запятой";
			this.smallerCol.Name = "smallerCol";
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(914, 478);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.FormatsdataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView FormatsdataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridViewComboBoxColumn bitCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn aCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn bCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn zCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn smallerCol;
	}
}