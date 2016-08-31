namespace ScopeSetupApp
{
    partial class MainForm
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.nowStatusFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.connectBtn = new System.Windows.Forms.ToolStripButton();
            this.ConfigScopeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.OpenScope_Button = new System.Windows.Forms.ToolStripButton();
            this.manStartBtn = new System.Windows.Forms.ToolStripButton();
            this.loadScopeToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.loadDataProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.label1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(725, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // nowStatusFlowLayoutPanel
            // 
            this.nowStatusFlowLayoutPanel.AutoScroll = true;
            this.nowStatusFlowLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nowStatusFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nowStatusFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.nowStatusFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nowStatusFlowLayoutPanel.Name = "nowStatusFlowLayoutPanel";
            this.nowStatusFlowLayoutPanel.Size = new System.Drawing.Size(481, 444);
            this.nowStatusFlowLayoutPanel.TabIndex = 2;
            this.nowStatusFlowLayoutPanel.Tag = "";
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
            this.ConfigScopeButton,
            this.toolStripButton,
            this.OpenScope_Button,
            this.manStartBtn,
            this.loadScopeToolStripLabel,
            this.loadDataProgressBar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(244, 444);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Tag = "0";
            this.toolStrip1.Text = "toolStrip1";
            // 
            // connectBtn
            // 
            this.connectBtn.AutoSize = false;
            this.connectBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.connectBtn.Font = new System.Drawing.Font("Open Sans", 9F);
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
            // ConfigScopeButton
            // 
            this.ConfigScopeButton.AutoSize = false;
            this.ConfigScopeButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ConfigScopeButton.Font = new System.Drawing.Font("Open Sans", 9F);
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
            this.ConfigScopeButton.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton
            // 
            this.toolStripButton.AutoSize = false;
            this.toolStripButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripButton.Font = new System.Drawing.Font("Open Sans", 9F);
            this.toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton.Image")));
            this.toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripButton.MergeIndex = 3;
            this.toolStripButton.Name = "toolStripButton";
            this.toolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripButton.Size = new System.Drawing.Size(230, 60);
            this.toolStripButton.Tag = "";
            this.toolStripButton.Text = "Конфигурация осциллографа";
            this.toolStripButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // OpenScope_Button
            // 
            this.OpenScope_Button.AutoSize = false;
            this.OpenScope_Button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.OpenScope_Button.Font = new System.Drawing.Font("Open Sans", 9F);
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
            // manStartBtn
            // 
            this.manStartBtn.AutoSize = false;
            this.manStartBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.manStartBtn.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.manStartBtn.Image = ((System.Drawing.Image)(resources.GetObject("manStartBtn.Image")));
            this.manStartBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.manStartBtn.Margin = new System.Windows.Forms.Padding(2);
            this.manStartBtn.MergeIndex = 3;
            this.manStartBtn.Name = "manStartBtn";
            this.manStartBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.manStartBtn.Size = new System.Drawing.Size(230, 60);
            this.manStartBtn.Tag = "";
            this.manStartBtn.Text = "Ручной запуск осциллографа";
            this.manStartBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.manStartBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.manStartBtn.Click += new System.EventHandler(this.manStartBtn_Click);
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
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AllowDrop = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.ContentPanel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(725, 444);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(725, 444);
            this.toolStripContainer1.TabIndex = 14;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nowStatusFlowLayoutPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(244, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 444);
            this.panel1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 466);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Open Sans", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Загрузка осциллограмм";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel label1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.FlowLayoutPanel nowStatusFlowLayoutPanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton connectBtn;
        private System.Windows.Forms.ToolStripButton ConfigScopeButton;
        private System.Windows.Forms.ToolStripButton toolStripButton;
        private System.Windows.Forms.ToolStripButton OpenScope_Button;
        private System.Windows.Forms.ToolStripButton manStartBtn;
        private System.Windows.Forms.ToolStripLabel loadScopeToolStripLabel;
        private System.Windows.Forms.ToolStripProgressBar loadDataProgressBar;
        private System.Windows.Forms.Panel panel1;
    }
}

