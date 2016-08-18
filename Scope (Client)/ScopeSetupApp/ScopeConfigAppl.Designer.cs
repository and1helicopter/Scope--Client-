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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.oscillFreqTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.hystoryTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Label();
            this.paramLoadDataTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.paramLoadConfigTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.loadOscillStartTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.scopeCountTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataStartTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.channelCountTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.oscillStatusTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timeStampTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.configPanel = new System.Windows.Forms.Panel();
            this.oscillSizeDataTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
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
            this.mailToolStrip.Size = new System.Drawing.Size(928, 38);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 360);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(928, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.oscillSizeDataTextBox);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.oscillFreqTextBox);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.hystoryTextBox);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Controls.Add(this.paramLoadDataTextBox);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.paramLoadConfigTextBox);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.loadOscillStartTextBox);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.scopeCountTextBox);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.dataStartTextBox);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.channelCountTextBox);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.oscillStatusTextBox);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.timeStampTextBox);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 38);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(928, 137);
            this.panel5.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(928, 26);
            this.panel2.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(463, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 18);
            this.label13.TabIndex = 3;
            this.label13.Text = "Сглаживание";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(316, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 18);
            this.label12.TabIndex = 2;
            this.label12.Text = "Формат";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(219, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 18);
            this.label11.TabIndex = 1;
            this.label11.Text = "Адрес";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(12, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "Параметр";
            // 
            // oscillFreqTextBox
            // 
            this.oscillFreqTextBox.Location = new System.Drawing.Point(692, 32);
            this.oscillFreqTextBox.Name = "oscillFreqTextBox";
            this.oscillFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.oscillFreqTextBox.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(537, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Oscill Freq Address";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hystoryTextBox
            // 
            this.hystoryTextBox.Location = new System.Drawing.Point(692, 6);
            this.hystoryTextBox.Name = "hystoryTextBox";
            this.hystoryTextBox.Size = new System.Drawing.Size(100, 20);
            this.hystoryTextBox.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(537, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(149, 20);
            this.panel1.TabIndex = 16;
            this.panel1.Text = "Hystory Address";
            this.panel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // paramLoadDataTextBox
            // 
            this.paramLoadDataTextBox.Location = new System.Drawing.Point(428, 84);
            this.paramLoadDataTextBox.Name = "paramLoadDataTextBox";
            this.paramLoadDataTextBox.Size = new System.Drawing.Size(100, 20);
            this.paramLoadDataTextBox.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(273, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "ParamLoadData Address";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // paramLoadConfigTextBox
            // 
            this.paramLoadConfigTextBox.Location = new System.Drawing.Point(167, 84);
            this.paramLoadConfigTextBox.Name = "paramLoadConfigTextBox";
            this.paramLoadConfigTextBox.Size = new System.Drawing.Size(100, 20);
            this.paramLoadConfigTextBox.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(12, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "ParamLoadConfig Address";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // loadOscillStartTextBox
            // 
            this.loadOscillStartTextBox.Location = new System.Drawing.Point(428, 58);
            this.loadOscillStartTextBox.Name = "loadOscillStartTextBox";
            this.loadOscillStartTextBox.Size = new System.Drawing.Size(100, 20);
            this.loadOscillStartTextBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(273, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "LoadOscillStart Address";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scopeCountTextBox
            // 
            this.scopeCountTextBox.Location = new System.Drawing.Point(167, 58);
            this.scopeCountTextBox.Name = "scopeCountTextBox";
            this.scopeCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.scopeCountTextBox.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "ScopeCount Address";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataStartTextBox
            // 
            this.dataStartTextBox.Location = new System.Drawing.Point(428, 32);
            this.dataStartTextBox.Name = "dataStartTextBox";
            this.dataStartTextBox.Size = new System.Drawing.Size(100, 20);
            this.dataStartTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(273, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "DataStart Address";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // channelCountTextBox
            // 
            this.channelCountTextBox.Location = new System.Drawing.Point(167, 32);
            this.channelCountTextBox.Name = "channelCountTextBox";
            this.channelCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.channelCountTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "ChannelCount Address";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oscillStatusTextBox
            // 
            this.oscillStatusTextBox.Location = new System.Drawing.Point(428, 6);
            this.oscillStatusTextBox.Name = "oscillStatusTextBox";
            this.oscillStatusTextBox.Size = new System.Drawing.Size(100, 20);
            this.oscillStatusTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(273, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "OscillStatus Address";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timeStampTextBox
            // 
            this.timeStampTextBox.Location = new System.Drawing.Point(167, 6);
            this.timeStampTextBox.Name = "timeStampTextBox";
            this.timeStampTextBox.Size = new System.Drawing.Size(100, 20);
            this.timeStampTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "TimeStamp Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // configPanel
            // 
            this.configPanel.AutoScroll = true;
            this.configPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.configPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configPanel.Location = new System.Drawing.Point(0, 175);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(928, 185);
            this.configPanel.TabIndex = 4;
            // 
            // oscillSizeDataTextBox
            // 
            this.oscillSizeDataTextBox.Location = new System.Drawing.Point(692, 58);
            this.oscillSizeDataTextBox.Name = "oscillSizeDataTextBox";
            this.oscillSizeDataTextBox.Size = new System.Drawing.Size(100, 20);
            this.oscillSizeDataTextBox.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(537, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(149, 20);
            this.label14.TabIndex = 22;
            this.label14.Text = "Oscill Size Data (Kbyte)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScopeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 382);
            this.Controls.Add(this.configPanel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mailToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.TextBox oscillFreqTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox hystoryTextBox;
        private System.Windows.Forms.Label panel1;
        private System.Windows.Forms.TextBox paramLoadDataTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox paramLoadConfigTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox loadOscillStartTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox scopeCountTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dataStartTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox channelCountTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox oscillStatusTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox timeStampTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton addLineButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox oscillSizeDataTextBox;
    }
}

