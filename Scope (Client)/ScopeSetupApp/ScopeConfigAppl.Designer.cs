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
            this.panel5 = new System.Windows.Forms.Panel();
            this.FlagNeed_ConfigTextBox = new System.Windows.Forms.TextBox();
            this.OscilSizeData_TextBox = new System.Windows.Forms.TextBox();
            this.FlagNeed_label = new System.Windows.Forms.Label();
            this.OscilSizeData_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
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
            this.configPanel = new System.Windows.Forms.Panel();
            this.mailToolStrip.SuspendLayout();
            this.panel5.SuspendLayout();
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
            this.mailToolStrip.Size = new System.Drawing.Size(852, 38);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 398);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.FlagNeed_ConfigTextBox);
            this.panel5.Controls.Add(this.OscilSizeData_TextBox);
            this.panel5.Controls.Add(this.FlagNeed_label);
            this.panel5.Controls.Add(this.OscilSizeData_label);
            this.panel5.Controls.Add(this.panel2);
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
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 38);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(852, 155);
            this.panel5.TabIndex = 2;
            // 
            // FlagNeed_ConfigTextBox
            // 
            this.FlagNeed_ConfigTextBox.Location = new System.Drawing.Point(695, 10);
            this.FlagNeed_ConfigTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FlagNeed_ConfigTextBox.Name = "FlagNeed_ConfigTextBox";
            this.FlagNeed_ConfigTextBox.Size = new System.Drawing.Size(116, 24);
            this.FlagNeed_ConfigTextBox.TabIndex = 13;
            // 
            // OscilSizeData_TextBox
            // 
            this.OscilSizeData_TextBox.Location = new System.Drawing.Point(695, 64);
            this.OscilSizeData_TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OscilSizeData_TextBox.Name = "OscilSizeData_TextBox";
            this.OscilSizeData_TextBox.Size = new System.Drawing.Size(116, 24);
            this.OscilSizeData_TextBox.TabIndex = 21;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 122);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 33);
            this.panel2.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Open Sans", 10F);
            this.label13.Location = new System.Drawing.Point(499, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 19);
            this.label13.TabIndex = 3;
            this.label13.Text = "Сглаживание";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Open Sans", 10F);
            this.label12.Location = new System.Drawing.Point(330, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 19);
            this.label12.TabIndex = 2;
            this.label12.Text = "Формат";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Open Sans", 10F);
            this.label11.Location = new System.Drawing.Point(234, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 19);
            this.label11.TabIndex = 1;
            this.label11.Text = "Адрес";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Open Sans", 10F);
            this.label10.Location = new System.Drawing.Point(14, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 19);
            this.label10.TabIndex = 0;
            this.label10.Text = "Параметр";
            // 
            // timeStampTextBox
            // 
            this.timeStampTextBox.Location = new System.Drawing.Point(695, 37);
            this.timeStampTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeStampTextBox.Name = "timeStampTextBox";
            this.timeStampTextBox.Size = new System.Drawing.Size(116, 24);
            this.timeStampTextBox.TabIndex = 1;
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
            // configPanel
            // 
            this.configPanel.AutoScroll = true;
            this.configPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.configPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configPanel.Location = new System.Drawing.Point(0, 193);
            this.configPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(852, 205);
            this.configPanel.TabIndex = 4;
            // 
            // ScopeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 420);
            this.Controls.Add(this.configPanel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mailToolStrip);
            this.Font = new System.Drawing.Font("Open Sans", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScopeConfigForm";
            this.Text = "Конфигурация системы";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mailToolStrip.ResumeLayout(false);
            this.mailToolStrip.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mailToolStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.TextBox OscilFreq_TextBox;
        private System.Windows.Forms.Label OscilFreq_label;
        private System.Windows.Forms.TextBox History_TextBox;
        private System.Windows.Forms.Label History_label;
        private System.Windows.Forms.TextBox NewConfig_TextBox;
        private System.Windows.Forms.Label NewConfig_label;
        private System.Windows.Forms.TextBox FlagNeed_ConfigTextBox;
        private System.Windows.Forms.Label FlagNeed_label;
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
        private System.Windows.Forms.TextBox timeStampTextBox;
        private System.Windows.Forms.Label TimeStamp_label;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton addLineButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label OscilSizeData_label;
        private System.Windows.Forms.TextBox OscilSizeData_TextBox;
    }
}

