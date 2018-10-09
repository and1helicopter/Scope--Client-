namespace ScopeApp.ucScopeSet
{
	sealed partial class UcScopeSetup
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcScopeSetup));
			this.label3 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.radioButton = new System.Windows.Forms.TextBox();
			this.sizeOcsil_trackBar = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.openButton2 = new System.Windows.Forms.ToolStripButton();
			this.saveButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripView = new System.Windows.Forms.ToolStripButton();
			this.toolStripPrint = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.writeToSystemBtn = new System.Windows.Forms.ToolStripButton();
			this.reloadButton = new System.Windows.Forms.ToolStripButton();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.CommentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.DelayOsc = new System.Windows.Forms.Label();
			this.enaScopeCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelPrecents = new System.Windows.Forms.Label();
			this.oscFreqNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.hystoryNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.chCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.labelFreq = new System.Windows.Forms.Label();
			this.labelAllSize = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.possibleParamPanel = new System.Windows.Forms.Panel();
			this.treeListView = new BrightIdeasSoftware.TreeListView();
			this.label2 = new System.Windows.Forms.Label();
			this.SCPrintDialog = new System.Windows.Forms.PrintDialog();
			this.SCPrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
			this.SCPrintDocument = new System.Drawing.Printing.PrintDocument();
			((System.ComponentModel.ISupportInitialize)(this.sizeOcsil_trackBar)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.oscFreqNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hystoryNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chCountNumericUpDown)).BeginInit();
			this.possibleParamPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(287, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "От 1 до 32";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 9);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(114, 13);
			this.label9.TabIndex = 16;
			this.label9.Text = "Количество каналов:";
			// 
			// radioButton
			// 
			this.radioButton.Enabled = false;
			this.radioButton.Location = new System.Drawing.Point(151, 6);
			this.radioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.radioButton.Name = "radioButton";
			this.radioButton.Size = new System.Drawing.Size(130, 20);
			this.radioButton.TabIndex = 15;
			this.radioButton.Text = "0";
			this.radioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.radioButton.TextChanged += new System.EventHandler(this.ChannelCount_TextChanged);
			// 
			// sizeOcsil_trackBar
			// 
			this.sizeOcsil_trackBar.Location = new System.Drawing.Point(151, 99);
			this.sizeOcsil_trackBar.Maximum = 100;
			this.sizeOcsil_trackBar.Minimum = 1;
			this.sizeOcsil_trackBar.Name = "sizeOcsil_trackBar";
			this.sizeOcsil_trackBar.Size = new System.Drawing.Size(130, 45);
			this.sizeOcsil_trackBar.TabIndex = 13;
			this.sizeOcsil_trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.sizeOcsil_trackBar.Value = 100;
			this.sizeOcsil_trackBar.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 54);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(79, 13);
			this.label8.TabIndex = 3;
			this.label8.Text = "Предыстория:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(287, 77);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "От 1 до 32";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(287, 32);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(59, 13);
			this.label10.TabIndex = 1;
			this.label10.Text = "От 1 до 32";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.Location = new System.Drawing.Point(287, 54);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "От 0 до 100%";
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
            this.toolStripSeparator2,
            this.toolStripView,
            this.toolStripPrint,
            this.toolStripSeparator1,
            this.writeToSystemBtn,
            this.reloadButton});
			this.toolStrip1.Location = new System.Drawing.Point(1, 1);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(790, 38);
			this.toolStrip1.TabIndex = 38;
			this.toolStrip1.Text = "Просмотр";
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
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
			// 
			// toolStripView
			// 
			this.toolStripView.AutoSize = false;
			this.toolStripView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripView.Image = global::ScopeApp.Properties.Resources.Fine_Print_50;
			this.toolStripView.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripView.Margin = new System.Windows.Forms.Padding(3);
			this.toolStripView.Name = "toolStripView";
			this.toolStripView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStripView.Size = new System.Drawing.Size(32, 32);
			this.toolStripView.Text = "Просмотр";
			this.toolStripView.Click += new System.EventHandler(this.View_toolStripButton_Click);
			// 
			// toolStripPrint
			// 
			this.toolStripPrint.AutoSize = false;
			this.toolStripPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripPrint.Image = global::ScopeApp.Properties.Resources.Print_50;
			this.toolStripPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripPrint.Margin = new System.Windows.Forms.Padding(3);
			this.toolStripPrint.Name = "toolStripPrint";
			this.toolStripPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStripPrint.Size = new System.Drawing.Size(32, 32);
			this.toolStripPrint.Text = "Печать";
			this.toolStripPrint.ToolTipText = "Печать";
			this.toolStripPrint.Click += new System.EventHandler(this.Print_toolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
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
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(495, 56);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(222, 17);
			this.checkBox3.TabIndex = 12;
			this.checkBox3.Text = "Отключить перезапись осциллограмм";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// CommentRichTextBox
			// 
			this.CommentRichTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.CommentRichTextBox.Location = new System.Drawing.Point(495, 9);
			this.CommentRichTextBox.Name = "CommentRichTextBox";
			this.CommentRichTextBox.ReadOnly = true;
			this.CommentRichTextBox.Size = new System.Drawing.Size(236, 45);
			this.CommentRichTextBox.TabIndex = 11;
			this.CommentRichTextBox.Text = "";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 77);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Делитель:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 32);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(147, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "Количество осциллограмм:";
			// 
			// DelayOsc
			// 
			this.DelayOsc.AutoSize = true;
			this.DelayOsc.Location = new System.Drawing.Point(6, 100);
			this.DelayOsc.Name = "DelayOsc";
			this.DelayOsc.Size = new System.Drawing.Size(86, 13);
			this.DelayOsc.TabIndex = 8;
			this.DelayOsc.Text = "Длительность: ";
			this.DelayOsc.Visible = false;
			// 
			// enaScopeCheckBox
			// 
			this.enaScopeCheckBox.AutoSize = true;
			this.enaScopeCheckBox.Location = new System.Drawing.Point(495, 98);
			this.enaScopeCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.enaScopeCheckBox.Name = "enaScopeCheckBox";
			this.enaScopeCheckBox.Size = new System.Drawing.Size(195, 17);
			this.enaScopeCheckBox.TabIndex = 5;
			this.enaScopeCheckBox.Text = "Осциллографирование включено";
			this.enaScopeCheckBox.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.Controls.Add(this.labelPrecents);
			this.panel1.Controls.Add(this.oscFreqNumericUpDown);
			this.panel1.Controls.Add(this.hystoryNumericUpDown);
			this.panel1.Controls.Add(this.chCountNumericUpDown);
			this.panel1.Controls.Add(this.labelFreq);
			this.panel1.Controls.Add(this.labelAllSize);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.radioButton);
			this.panel1.Controls.Add(this.sizeOcsil_trackBar);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.checkBox3);
			this.panel1.Controls.Add(this.CommentRichTextBox);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.DelayOsc);
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.enaScopeCheckBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(1, 39);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(790, 144);
			this.panel1.TabIndex = 36;
			// 
			// labelPrecents
			// 
			this.labelPrecents.AutoSize = true;
			this.labelPrecents.Location = new System.Drawing.Point(209, 122);
			this.labelPrecents.Name = "labelPrecents";
			this.labelPrecents.Size = new System.Drawing.Size(15, 13);
			this.labelPrecents.TabIndex = 25;
			this.labelPrecents.Text = "%";
			// 
			// oscFreqNumericUpDown
			// 
			this.oscFreqNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.oscFreqNumericUpDown.Location = new System.Drawing.Point(151, 77);
			this.oscFreqNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.oscFreqNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.oscFreqNumericUpDown.Name = "oscFreqNumericUpDown";
			this.oscFreqNumericUpDown.Size = new System.Drawing.Size(130, 20);
			this.oscFreqNumericUpDown.TabIndex = 24;
			this.oscFreqNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.oscFreqNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.oscFreqNumericUpDown.ValueChanged += new System.EventHandler(this.oscFreq_TextChanged);
			// 
			// hystoryNumericUpDown
			// 
			this.hystoryNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.hystoryNumericUpDown.Location = new System.Drawing.Point(151, 54);
			this.hystoryNumericUpDown.Name = "hystoryNumericUpDown";
			this.hystoryNumericUpDown.Size = new System.Drawing.Size(130, 20);
			this.hystoryNumericUpDown.TabIndex = 23;
			this.hystoryNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.hystoryNumericUpDown.ValueChanged += new System.EventHandler(this.hystory_TextChanged);
			// 
			// chCountNumericUpDown
			// 
			this.chCountNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.chCountNumericUpDown.Location = new System.Drawing.Point(151, 30);
			this.chCountNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.chCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.chCountNumericUpDown.Name = "chCountNumericUpDown";
			this.chCountNumericUpDown.Size = new System.Drawing.Size(130, 20);
			this.chCountNumericUpDown.TabIndex = 22;
			this.chCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.chCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.chCountNumericUpDown.ValueChanged += new System.EventHandler(this.chCount_TextChanged);
			// 
			// labelFreq
			// 
			this.labelFreq.AutoSize = true;
			this.labelFreq.Location = new System.Drawing.Point(287, 100);
			this.labelFreq.Name = "labelFreq";
			this.labelFreq.Size = new System.Drawing.Size(99, 13);
			this.labelFreq.TabIndex = 21;
			this.labelFreq.Text = "Частота выборки:";
			// 
			// labelAllSize
			// 
			this.labelAllSize.AutoSize = true;
			this.labelAllSize.Location = new System.Drawing.Point(287, 122);
			this.labelAllSize.Name = "labelAllSize";
			this.labelAllSize.Size = new System.Drawing.Size(144, 13);
			this.labelAllSize.TabIndex = 20;
			this.labelAllSize.Text = "Размер доступной памяти:";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(495, 77);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(236, 17);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "Сохранение осциллограммы на SD карту";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// possibleParamPanel
			// 
			this.possibleParamPanel.AutoScroll = true;
			this.possibleParamPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.possibleParamPanel.Controls.Add(this.treeListView);
			this.possibleParamPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.possibleParamPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.possibleParamPanel.Location = new System.Drawing.Point(1, 226);
			this.possibleParamPanel.Margin = new System.Windows.Forms.Padding(0);
			this.possibleParamPanel.Name = "possibleParamPanel";
			this.possibleParamPanel.Size = new System.Drawing.Size(790, 381);
			this.possibleParamPanel.TabIndex = 17;
			// 
			// treeListView
			// 
			this.treeListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeListView.CausesValidation = false;
			this.treeListView.CellEditUseWholeCell = false;
			this.treeListView.CheckBoxes = true;
			this.treeListView.CopySelectionOnControlC = false;
			this.treeListView.CopySelectionOnControlCUsesDragSource = false;
			this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView.FullRowSelect = true;
			this.treeListView.GridLines = true;
			this.treeListView.HideSelection = false;
			this.treeListView.HierarchicalCheckboxes = true;
			this.treeListView.IsSearchOnSortColumn = false;
			this.treeListView.LabelWrap = false;
			this.treeListView.Location = new System.Drawing.Point(0, 0);
			this.treeListView.Name = "treeListView";
			this.treeListView.RevealAfterExpand = false;
			this.treeListView.SelectAllOnControlA = false;
			this.treeListView.SelectColumnsMenuStaysOpen = false;
			this.treeListView.SelectColumnsOnRightClick = false;
			this.treeListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
			this.treeListView.ShowFilterMenuOnRightClick = false;
			this.treeListView.ShowGroups = false;
			this.treeListView.ShowHeaderInAllViews = false;
			this.treeListView.ShowSortIndicators = false;
			this.treeListView.Size = new System.Drawing.Size(790, 381);
			this.treeListView.TabIndex = 10;
			this.treeListView.TriggerCellOverEventsWhenOverHeader = false;
			this.treeListView.UpdateSpaceFillingColumnsWhenDraggingColumnDivider = false;
			this.treeListView.UseCompatibleStateImageBehavior = false;
			this.treeListView.UseHotControls = false;
			this.treeListView.UseOverlays = false;
			this.treeListView.UseWaitCursorWhenExpanding = false;
			this.treeListView.View = System.Windows.Forms.View.Details;
			this.treeListView.VirtualMode = true;
			this.treeListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.TreeListViewOnFormatRow);
			this.treeListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.treeListView_ItemChecked);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.Control;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.Location = new System.Drawing.Point(1, 183);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(790, 43);
			this.label2.TabIndex = 39;
			this.label2.Text = "Возможные для осциллографирования параметры";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SCPrintDialog
			// 
			this.SCPrintDialog.UseEXDialog = true;
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
			// SCPrintDocument
			// 
			this.SCPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.SCPrintDocument_PrintPage);
			// 
			// UcScopeSetup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.Controls.Add(this.possibleParamPanel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolStrip1);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UcScopeSetup";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(792, 608);
			this.Load += new System.EventHandler(this.UcScopeSetup_Load);
			((System.ComponentModel.ISupportInitialize)(this.sizeOcsil_trackBar)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.oscFreqNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hystoryNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chCountNumericUpDown)).EndInit();
			this.possibleParamPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox radioButton;
		private System.Windows.Forms.TrackBar sizeOcsil_trackBar;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton writeToSystemBtn;
		private System.Windows.Forms.ToolStripButton reloadButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton openButton2;
		private System.Windows.Forms.ToolStripButton saveButton2;
		private System.Windows.Forms.CheckBox checkBox3;
		internal System.Windows.Forms.RichTextBox CommentRichTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label DelayOsc;
		private System.Windows.Forms.CheckBox enaScopeCheckBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Panel possibleParamPanel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelFreq;
		private System.Windows.Forms.Label labelAllSize;
		private System.Windows.Forms.NumericUpDown chCountNumericUpDown;
		private System.Windows.Forms.NumericUpDown oscFreqNumericUpDown;
		private System.Windows.Forms.NumericUpDown hystoryNumericUpDown;
		private System.Windows.Forms.PrintDialog SCPrintDialog;
		private System.Windows.Forms.PrintPreviewDialog SCPrintPreviewDialog;
		private System.Drawing.Printing.PrintDocument SCPrintDocument;
		private System.Windows.Forms.ToolStripButton toolStripView;
		private System.Windows.Forms.ToolStripButton toolStripPrint;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private BrightIdeasSoftware.TreeListView treeListView;
		private System.Windows.Forms.Label labelPrecents;
	}
}
