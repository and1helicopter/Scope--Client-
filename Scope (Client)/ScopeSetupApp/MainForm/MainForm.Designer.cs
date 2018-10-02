namespace ScopeSetupApp.MainForm
{
	public sealed partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ButtonsTimer = new System.Windows.Forms.Timer(this.components);
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.Infopanel = new System.Windows.Forms.Panel();
			this.toolStrip3 = new System.Windows.Forms.ToolStrip();
			this.current_config = new System.Windows.Forms.ToolStripButton();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.nowStatusFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.connectBtn = new System.Windows.Forms.ToolStripButton();
			this.ConfigMCUButton = new System.Windows.Forms.ToolStripButton();
			this.OpenScope_Button = new System.Windows.Forms.ToolStripButton();
			this.ConfigScopeButton = new System.Windows.Forms.ToolStripButton();
			this.Setting_Button = new System.Windows.Forms.ToolStripButton();
			this.loadScopeToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.loadDataProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.stopDownloadStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.WaitLoadConfig_toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
			this.com_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.connect_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.format_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.config_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.freq_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.size_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer3 = new System.Windows.Forms.ToolStripContainer();
			this.toolStripContainer4 = new System.Windows.Forms.ToolStripContainer();
			this.checkStateRenderer1 = new BrightIdeasSoftware.CheckStateRenderer();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.Infopanel.SuspendLayout();
			this.toolStrip3.SuspendLayout();
			this.MainPanel.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.toolStripContainer2.ContentPanel.SuspendLayout();
			this.toolStripContainer2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer3.ContentPanel.SuspendLayout();
			this.toolStripContainer3.SuspendLayout();
			this.toolStripContainer4.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer4.ContentPanel.SuspendLayout();
			this.toolStripContainer4.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonsTimer
			// 
			this.ButtonsTimer.Interval = 250;
			this.ButtonsTimer.Tick += new System.EventHandler(this.ButtonsTimer_Tick);
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AllowDrop = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.Infopanel);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.MainPanel);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip1);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip2);
			this.toolStripContainer1.ContentPanel.ImeMode = System.Windows.Forms.ImeMode.On;
			this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1034, 489);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(1034, 489);
			this.toolStripContainer1.TabIndex = 14;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanelVisible = false;
			// 
			// Infopanel
			// 
			this.Infopanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Infopanel.Controls.Add(this.toolStrip3);
			this.Infopanel.Location = new System.Drawing.Point(0, 449);
			this.Infopanel.Margin = new System.Windows.Forms.Padding(0);
			this.Infopanel.Name = "Infopanel";
			this.Infopanel.Size = new System.Drawing.Size(244, 40);
			this.Infopanel.TabIndex = 5;
			// 
			// toolStrip3
			// 
			this.toolStrip3.AutoSize = false;
			this.toolStrip3.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.current_config});
			this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.toolStrip3.Location = new System.Drawing.Point(0, 0);
			this.toolStrip3.Margin = new System.Windows.Forms.Padding(3);
			this.toolStrip3.Name = "toolStrip3";
			this.toolStrip3.Padding = new System.Windows.Forms.Padding(3);
			this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip3.Size = new System.Drawing.Size(244, 40);
			this.toolStrip3.TabIndex = 0;
			this.toolStrip3.Text = "toolStrip3";
			// 
			// current_config
			// 
			this.current_config.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.current_config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.current_config.Image = ((System.Drawing.Image)(resources.GetObject("current_config.Image")));
			this.current_config.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.current_config.Margin = new System.Windows.Forms.Padding(3);
			this.current_config.Name = "current_config";
			this.current_config.Size = new System.Drawing.Size(231, 19);
			this.current_config.Text = "Текущая";
			this.current_config.ToolTipText = "Соединение не установлено";
			// 
			// MainPanel
			// 
			this.MainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.MainPanel.Controls.Add(this.tableLayoutPanel);
			this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainPanel.Location = new System.Drawing.Point(244, 0);
			this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(746, 489);
			this.MainPanel.TabIndex = 0;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.nowStatusFlowLayoutPanel, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(746, 489);
			this.tableLayoutPanel.TabIndex = 4;
			// 
			// nowStatusFlowLayoutPanel
			// 
			this.nowStatusFlowLayoutPanel.AutoScroll = true;
			this.nowStatusFlowLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.nowStatusFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nowStatusFlowLayoutPanel.Location = new System.Drawing.Point(373, 0);
			this.nowStatusFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.nowStatusFlowLayoutPanel.Name = "nowStatusFlowLayoutPanel";
			this.nowStatusFlowLayoutPanel.Size = new System.Drawing.Size(373, 489);
			this.nowStatusFlowLayoutPanel.TabIndex = 2;
			this.nowStatusFlowLayoutPanel.Tag = "";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(373, 489);
			this.panel1.TabIndex = 3;
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolStrip1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectBtn,
            this.ConfigMCUButton,
            this.OpenScope_Button,
            this.ConfigScopeButton,
            this.Setting_Button,
            this.loadScopeToolStripLabel,
            this.loadDataProgressBar,
            this.stopDownloadStripButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip1.Size = new System.Drawing.Size(244, 489);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Tag = "0";
			this.toolStrip1.Text = "Новая";
			// 
			// connectBtn
			// 
			this.connectBtn.AutoSize = false;
			this.connectBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.connectBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.connectBtn.Image = ((System.Drawing.Image)(resources.GetObject("connectBtn.Image")));
			this.connectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.connectBtn.Margin = new System.Windows.Forms.Padding(2);
			this.connectBtn.MergeIndex = 3;
			this.connectBtn.Name = "connectBtn";
			this.connectBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.connectBtn.Size = new System.Drawing.Size(230, 60);
			this.connectBtn.Tag = "";
			this.connectBtn.Text = "Настройка соединения ";
			this.connectBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.connectBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
			// 
			// ConfigMCUButton
			// 
			this.ConfigMCUButton.AutoSize = false;
			this.ConfigMCUButton.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.ConfigMCUButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.ConfigMCUButton.Image = ((System.Drawing.Image)(resources.GetObject("ConfigMCUButton.Image")));
			this.ConfigMCUButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ConfigMCUButton.Margin = new System.Windows.Forms.Padding(2);
			this.ConfigMCUButton.MergeIndex = 3;
			this.ConfigMCUButton.Name = "ConfigMCUButton";
			this.ConfigMCUButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.ConfigMCUButton.Size = new System.Drawing.Size(230, 60);
			this.ConfigMCUButton.Tag = "";
			this.ConfigMCUButton.Text = "Конфигурация осциллографа";
			this.ConfigMCUButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ConfigMCUButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.ConfigMCUButton.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// OpenScope_Button
			// 
			this.OpenScope_Button.AutoSize = false;
			this.OpenScope_Button.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.OpenScope_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.OpenScope_Button.Image = ((System.Drawing.Image)(resources.GetObject("OpenScope_Button.Image")));
			this.OpenScope_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenScope_Button.Margin = new System.Windows.Forms.Padding(2);
			this.OpenScope_Button.MergeIndex = 3;
			this.OpenScope_Button.Name = "OpenScope_Button";
			this.OpenScope_Button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.OpenScope_Button.Size = new System.Drawing.Size(230, 60);
			this.OpenScope_Button.Tag = "";
			this.OpenScope_Button.Text = "Открыть файл с осциллограммой";
			this.OpenScope_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.OpenScope_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.OpenScope_Button.Click += new System.EventHandler(this.toolStripButton2_Click_1);
			// 
			// ConfigScopeButton
			// 
			this.ConfigScopeButton.AutoSize = false;
			this.ConfigScopeButton.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.ConfigScopeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.ConfigScopeButton.Image = ((System.Drawing.Image)(resources.GetObject("ConfigScopeButton.Image")));
			this.ConfigScopeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ConfigScopeButton.Margin = new System.Windows.Forms.Padding(2);
			this.ConfigScopeButton.MergeIndex = 3;
			this.ConfigScopeButton.Name = "ConfigScopeButton";
			this.ConfigScopeButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.ConfigScopeButton.Size = new System.Drawing.Size(230, 60);
			this.ConfigScopeButton.Tag = "";
			this.ConfigScopeButton.Text = "Конфигурация системы";
			this.ConfigScopeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ConfigScopeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.ConfigScopeButton.Visible = false;
			this.ConfigScopeButton.Click += new System.EventHandler(this.toolStripButton3_Click);
			// 
			// Setting_Button
			// 
			this.Setting_Button.AutoSize = false;
			this.Setting_Button.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Setting_Button.Image = global::ScopeSetupApp.Properties.Resources.Support_50;
			this.Setting_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Setting_Button.Margin = new System.Windows.Forms.Padding(2);
			this.Setting_Button.Name = "Setting_Button";
			this.Setting_Button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Setting_Button.Size = new System.Drawing.Size(230, 60);
			this.Setting_Button.Text = "Параметры ";
			this.Setting_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Setting_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.Setting_Button.Visible = false;
			this.Setting_Button.Click += new System.EventHandler(this.Setting_Button_Click);
			// 
			// loadScopeToolStripLabel
			// 
			this.loadScopeToolStripLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.loadScopeToolStripLabel.Margin = new System.Windows.Forms.Padding(9);
			this.loadScopeToolStripLabel.Name = "loadScopeToolStripLabel";
			this.loadScopeToolStripLabel.Size = new System.Drawing.Size(224, 14);
			this.loadScopeToolStripLabel.Text = "toolStripLabel1";
			this.loadScopeToolStripLabel.Visible = false;
			// 
			// loadDataProgressBar
			// 
			this.loadDataProgressBar.AutoSize = false;
			this.loadDataProgressBar.Margin = new System.Windows.Forms.Padding(3);
			this.loadDataProgressBar.MarqueeAnimationSpeed = 1000;
			this.loadDataProgressBar.Maximum = 1000;
			this.loadDataProgressBar.MergeIndex = 0;
			this.loadDataProgressBar.Name = "loadDataProgressBar";
			this.loadDataProgressBar.Size = new System.Drawing.Size(150, 24);
			this.loadDataProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.loadDataProgressBar.Visible = false;
			// 
			// stopDownloadStripButton
			// 
			this.stopDownloadStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.stopDownloadStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopDownloadStripButton.Image")));
			this.stopDownloadStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopDownloadStripButton.Margin = new System.Windows.Forms.Padding(45, 0, 45, 0);
			this.stopDownloadStripButton.Name = "stopDownloadStripButton";
			this.stopDownloadStripButton.Size = new System.Drawing.Size(152, 36);
			this.stopDownloadStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.stopDownloadStripButton.ToolTipText = "Отмена";
			this.stopDownloadStripButton.Visible = false;
			this.stopDownloadStripButton.Click += new System.EventHandler(this.stopDownloadStripButton_Click);
			// 
			// toolStrip2
			// 
			this.toolStrip2.AutoSize = false;
			this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
			this.toolStrip2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.WaitLoadConfig_toolStripProgressBar});
			this.toolStrip2.Location = new System.Drawing.Point(990, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip2.Size = new System.Drawing.Size(44, 489);
			this.toolStrip2.TabIndex = 4;
			this.toolStrip2.Tag = "0";
			this.toolStrip2.Text = "toolStrip2";
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.AutoSize = false;
			this.toolStripButton2.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Margin = new System.Windows.Forms.Padding(2);
			this.toolStripButton2.MergeIndex = 3;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStripButton2.Size = new System.Drawing.Size(42, 42);
			this.toolStripButton2.Tag = "";
			this.toolStripButton2.Text = "Ручной запуск осциллографа";
			this.toolStripButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
			this.toolStripButton2.Click += new System.EventHandler(this.manStartBtn_Click);
			// 
			// WaitLoadConfig_toolStripProgressBar
			// 
			this.WaitLoadConfig_toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.WaitLoadConfig_toolStripProgressBar.MarqueeAnimationSpeed = 1;
			this.WaitLoadConfig_toolStripProgressBar.Maximum = 23;
			this.WaitLoadConfig_toolStripProgressBar.Name = "WaitLoadConfig_toolStripProgressBar";
			this.WaitLoadConfig_toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.WaitLoadConfig_toolStripProgressBar.Size = new System.Drawing.Size(40, 20);
			this.WaitLoadConfig_toolStripProgressBar.Step = 1;
			// 
			// toolStripContainer2
			// 
			this.toolStripContainer2.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer2.ContentPanel
			// 
			this.toolStripContainer2.ContentPanel.AutoScroll = true;
			this.toolStripContainer2.ContentPanel.Controls.Add(this.toolStripContainer1);
			this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(1034, 489);
			this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer2.LeftToolStripPanelVisible = false;
			this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer2.Name = "toolStripContainer2";
			this.toolStripContainer2.Size = new System.Drawing.Size(1034, 489);
			this.toolStripContainer2.TabIndex = 15;
			this.toolStripContainer2.Text = "toolStripContainer2";
			this.toolStripContainer2.TopToolStripPanelVisible = false;
			// 
			// com_toolStripStatusLabel
			// 
			this.com_toolStripStatusLabel.Name = "com_toolStripStatusLabel";
			this.com_toolStripStatusLabel.Size = new System.Drawing.Size(141, 17);
			this.com_toolStripStatusLabel.Text = "com_toolStripStatusLabel";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connect_toolStripStatusLabel,
            this.com_toolStripStatusLabel,
            this.format_toolStripStatusLabel,
            this.config_toolStripStatusLabel,
            this.freq_toolStripStatusLabel,
            this.size_toolStripStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1034, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// connect_toolStripStatusLabel
			// 
			this.connect_toolStripStatusLabel.Name = "connect_toolStripStatusLabel";
			this.connect_toolStripStatusLabel.Size = new System.Drawing.Size(141, 17);
			this.connect_toolStripStatusLabel.Text = "com_toolStripStatusLabel";
			// 
			// format_toolStripStatusLabel
			// 
			this.format_toolStripStatusLabel.Name = "format_toolStripStatusLabel";
			this.format_toolStripStatusLabel.Size = new System.Drawing.Size(153, 17);
			this.format_toolStripStatusLabel.Text = "format_toolStripStatusLabel";
			// 
			// config_toolStripStatusLabel
			// 
			this.config_toolStripStatusLabel.Name = "config_toolStripStatusLabel";
			this.config_toolStripStatusLabel.Size = new System.Drawing.Size(151, 17);
			this.config_toolStripStatusLabel.Text = "config_toolStripStatusLabel";
			// 
			// freq_toolStripStatusLabel
			// 
			this.freq_toolStripStatusLabel.Name = "freq_toolStripStatusLabel";
			this.freq_toolStripStatusLabel.Size = new System.Drawing.Size(138, 17);
			this.freq_toolStripStatusLabel.Text = "freq_toolStripStatusLabel";
			// 
			// size_toolStripStatusLabel
			// 
			this.size_toolStripStatusLabel.Name = "size_toolStripStatusLabel";
			this.size_toolStripStatusLabel.Size = new System.Drawing.Size(136, 17);
			this.size_toolStripStatusLabel.Text = "size_toolStripStatusLabel";
			// 
			// toolStripContainer3
			// 
			this.toolStripContainer3.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer3.ContentPanel
			// 
			this.toolStripContainer3.ContentPanel.AutoScroll = true;
			this.toolStripContainer3.ContentPanel.Controls.Add(this.toolStripContainer2);
			this.toolStripContainer3.ContentPanel.Size = new System.Drawing.Size(1034, 489);
			this.toolStripContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer3.LeftToolStripPanelVisible = false;
			this.toolStripContainer3.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer3.Name = "toolStripContainer3";
			// 
			// toolStripContainer3.RightToolStripPanel
			// 
			this.toolStripContainer3.RightToolStripPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.toolStripContainer3.Size = new System.Drawing.Size(1034, 489);
			this.toolStripContainer3.TabIndex = 16;
			this.toolStripContainer3.Text = "toolStripContainer3";
			this.toolStripContainer3.TopToolStripPanelVisible = false;
			// 
			// toolStripContainer4
			// 
			// 
			// toolStripContainer4.BottomToolStripPanel
			// 
			this.toolStripContainer4.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer4.ContentPanel
			// 
			this.toolStripContainer4.ContentPanel.AutoScroll = true;
			this.toolStripContainer4.ContentPanel.Controls.Add(this.toolStripContainer3);
			this.toolStripContainer4.ContentPanel.Size = new System.Drawing.Size(1034, 489);
			this.toolStripContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer4.LeftToolStripPanelVisible = false;
			this.toolStripContainer4.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer4.Name = "toolStripContainer4";
			this.toolStripContainer4.RightToolStripPanelVisible = false;
			this.toolStripContainer4.Size = new System.Drawing.Size(1034, 511);
			this.toolStripContainer4.TabIndex = 17;
			this.toolStripContainer4.Text = "toolStripContainer4";
			this.toolStripContainer4.TopToolStripPanelVisible = false;
			// 
			// MainForm
			// 
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1034, 511);
			this.Controls.Add(this.toolStripContainer4);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(1050, 550);
			this.Name = "MainForm";
			this.Text = "Загрузка осциллограмм";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
			this.SizeChanged += new System.EventHandler(this.MainForm_ResizeEnd);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.Infopanel.ResumeLayout(false);
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStripContainer2.ContentPanel.ResumeLayout(false);
			this.toolStripContainer2.ResumeLayout(false);
			this.toolStripContainer2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStripContainer3.ContentPanel.ResumeLayout(false);
			this.toolStripContainer3.ResumeLayout(false);
			this.toolStripContainer3.PerformLayout();
			this.toolStripContainer4.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer4.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer4.ContentPanel.ResumeLayout(false);
			this.toolStripContainer4.ResumeLayout(false);
			this.toolStripContainer4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer ButtonsTimer;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton connectBtn;
		private System.Windows.Forms.ToolStripButton ConfigScopeButton;
		private System.Windows.Forms.ToolStripButton ConfigMCUButton;
		private System.Windows.Forms.ToolStripButton OpenScope_Button;
		private System.Windows.Forms.ToolStripLabel loadScopeToolStripLabel;
		private System.Windows.Forms.ToolStripProgressBar loadDataProgressBar;
		private System.Windows.Forms.FlowLayoutPanel nowStatusFlowLayoutPanel;
		private System.Windows.Forms.ToolStripContainer toolStripContainer2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer3;
		private System.Windows.Forms.ToolStripContainer toolStripContainer4;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.ToolStripButton Setting_Button;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripStatusLabel com_toolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel format_toolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel config_toolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel connect_toolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel size_toolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel freq_toolStripStatusLabel;
		private System.Windows.Forms.Panel Infopanel;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton current_config;
        private System.Windows.Forms.ToolStripButton stopDownloadStripButton;
		private System.Windows.Forms.ToolStripProgressBar WaitLoadConfig_toolStripProgressBar;
		private BrightIdeasSoftware.CheckStateRenderer checkStateRenderer1;
	}
}

