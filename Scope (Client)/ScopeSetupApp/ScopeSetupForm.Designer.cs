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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopeSetupForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.writeToSystemBtn = new System.Windows.Forms.ToolStripButton();
            this.reloadBtn = new System.Windows.Forms.ToolStripButton();
            this.openButton2 = new System.Windows.Forms.ToolStripButton();
            this.saveButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.oscSize = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.oscFreqRadioButton = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.chCountRadioButton = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.radioButton = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.hystoryRadioButton = new System.Windows.Forms.TextBox();
            this.enaScopeCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.possibleParamPanel = new System.Windows.Forms.Panel();
            this.possibleTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.sampleNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.possibleParamPanel.SuspendLayout();
            this.possibleTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Font = new System.Drawing.Font("Consolas", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.writeToSystemBtn,
            this.reloadBtn,
            this.openButton2,
            this.saveButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(956, 38);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // writeToSystemBtn
            // 
            this.writeToSystemBtn.AutoSize = false;
            this.writeToSystemBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.writeToSystemBtn.Image = ((System.Drawing.Image)(resources.GetObject("writeToSystemBtn.Image")));
            this.writeToSystemBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.writeToSystemBtn.Margin = new System.Windows.Forms.Padding(3);
            this.writeToSystemBtn.Name = "writeToSystemBtn";
            this.writeToSystemBtn.Size = new System.Drawing.Size(32, 32);
            this.writeToSystemBtn.Text = "Загрузиь конфигурацию в систему";
            this.writeToSystemBtn.Click += new System.EventHandler(this.writeToSystemBtn_Click);
            // 
            // reloadBtn
            // 
            this.reloadBtn.AutoSize = false;
            this.reloadBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reloadBtn.Image = ((System.Drawing.Image)(resources.GetObject("reloadBtn.Image")));
            this.reloadBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadBtn.Margin = new System.Windows.Forms.Padding(3);
            this.reloadBtn.Name = "reloadBtn";
            this.reloadBtn.Size = new System.Drawing.Size(32, 32);
            this.reloadBtn.Text = "Обновить";
            this.reloadBtn.Click += new System.EventHandler(this.reloadBtn_Click);
            // 
            // openButton2
            // 
            this.openButton2.AutoSize = false;
            this.openButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton2.Image = ((System.Drawing.Image)(resources.GetObject("openButton2.Image")));
            this.openButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton2.Margin = new System.Windows.Forms.Padding(3);
            this.openButton2.Name = "openButton2";
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
            this.saveButton2.Size = new System.Drawing.Size(32, 32);
            this.saveButton2.Text = "Сохранить файл";
            this.saveButton2.Click += new System.EventHandler(this.saveButton2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.oscSize);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.enaScopeCheckBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 147);
            this.panel1.TabIndex = 0;
            // 
            // oscSize
            // 
            this.oscSize.AutoSize = true;
            this.oscSize.Location = new System.Drawing.Point(410, 47);
            this.oscSize.Name = "oscSize";
            this.oscSize.Size = new System.Drawing.Size(174, 17);
            this.oscSize.TabIndex = 8;
            this.oscSize.Text = "Размер под осциллограмму:";
            this.oscSize.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.oscFreqRadioButton);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(5, 107);
            this.panel4.Margin = new System.Windows.Forms.Padding(5, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(399, 33);
            this.panel4.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "От 1";
            // 
            // oscFreqRadioButton
            // 
            this.oscFreqRadioButton.Location = new System.Drawing.Point(173, 5);
            this.oscFreqRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.oscFreqRadioButton.Name = "oscFreqRadioButton";
            this.oscFreqRadioButton.Size = new System.Drawing.Size(130, 24);
            this.oscFreqRadioButton.TabIndex = 4;
            this.oscFreqRadioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.oscFreqRadioButton.TextChanged += new System.EventHandler(this.oscFreqRadioButton_TextChanged);
            this.oscFreqRadioButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.oscFreqRadioButton_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Делитель:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(410, 114);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 21);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Запись в память";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.chCountRadioButton);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Location = new System.Drawing.Point(5, 5);
            this.panel7.Margin = new System.Windows.Forms.Padding(5, 4, 3, 4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(399, 33);
            this.panel7.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(309, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "От 1 до 32";
            // 
            // chCountRadioButton
            // 
            this.chCountRadioButton.Location = new System.Drawing.Point(173, 5);
            this.chCountRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chCountRadioButton.Name = "chCountRadioButton";
            this.chCountRadioButton.Size = new System.Drawing.Size(130, 24);
            this.chCountRadioButton.TabIndex = 1;
            this.chCountRadioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chCountRadioButton.TextChanged += new System.EventHandler(this.chCountRadioButton_TextChanged);
            this.chCountRadioButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chCountRadioButton_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(170, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "Количество осциллограмм:";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.radioButton);
            this.panel6.Location = new System.Drawing.Point(5, 39);
            this.panel6.Margin = new System.Windows.Forms.Padding(5, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(399, 33);
            this.panel6.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "От 1 до 32";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "Количество каналов:";
            // 
            // radioButton
            // 
            this.radioButton.Location = new System.Drawing.Point(173, 5);
            this.radioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton.Name = "radioButton";
            this.radioButton.Size = new System.Drawing.Size(130, 24);
            this.radioButton.TabIndex = 2;
            this.radioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.radioButton.TextChanged += new System.EventHandler(this.radioButton_TextChanged);
            this.radioButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.radioButton_KeyPress);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.hystoryRadioButton);
            this.panel5.Location = new System.Drawing.Point(5, 73);
            this.panel5.Margin = new System.Windows.Forms.Padding(5, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(399, 33);
            this.panel5.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Предыстория:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Open Sans", 9F);
            this.label6.Location = new System.Drawing.Point(309, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "От 1 до 99 %";
            // 
            // hystoryRadioButton
            // 
            this.hystoryRadioButton.Location = new System.Drawing.Point(173, 5);
            this.hystoryRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hystoryRadioButton.Name = "hystoryRadioButton";
            this.hystoryRadioButton.Size = new System.Drawing.Size(130, 24);
            this.hystoryRadioButton.TabIndex = 3;
            this.hystoryRadioButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hystoryRadioButton.TextChanged += new System.EventHandler(this.hystoryRadioButton_TextChanged);
            this.hystoryRadioButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hystoryRadioButton_KeyPress);
            // 
            // enaScopeCheckBox
            // 
            this.enaScopeCheckBox.AutoSize = true;
            this.enaScopeCheckBox.Location = new System.Drawing.Point(410, 80);
            this.enaScopeCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.enaScopeCheckBox.Name = "enaScopeCheckBox";
            this.enaScopeCheckBox.Size = new System.Drawing.Size(228, 21);
            this.enaScopeCheckBox.TabIndex = 5;
            this.enaScopeCheckBox.Text = "Осциллографирование включено";
            this.enaScopeCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(956, 342);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.possibleParamPanel);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(948, 332);
            this.panel2.TabIndex = 2;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox2.Location = new System.Drawing.Point(929, 7);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // possibleParamPanel
            // 
            this.possibleParamPanel.AutoScroll = true;
            this.possibleParamPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.possibleParamPanel.Controls.Add(this.possibleTableLayoutPanel);
            this.possibleParamPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.possibleParamPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.possibleParamPanel.Location = new System.Drawing.Point(0, 35);
            this.possibleParamPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.possibleParamPanel.Name = "possibleParamPanel";
            this.possibleParamPanel.Size = new System.Drawing.Size(948, 297);
            this.possibleParamPanel.TabIndex = 6;
            // 
            // possibleTableLayoutPanel
            // 
            this.possibleTableLayoutPanel.AutoSize = true;
            this.possibleTableLayoutPanel.ColumnCount = 1;
            this.possibleTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.possibleTableLayoutPanel.Controls.Add(this.sampleNameLabel, 0, 0);
            this.possibleTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.possibleTableLayoutPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.possibleTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.possibleTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.possibleTableLayoutPanel.Name = "possibleTableLayoutPanel";
            this.possibleTableLayoutPanel.RowCount = 1;
            this.possibleTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.possibleTableLayoutPanel.Size = new System.Drawing.Size(948, 39);
            this.possibleTableLayoutPanel.TabIndex = 7;
            // 
            // sampleNameLabel
            // 
            this.sampleNameLabel.AutoSize = true;
            this.sampleNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sampleNameLabel.Location = new System.Drawing.Point(0, 6);
            this.sampleNameLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.sampleNameLabel.Name = "sampleNameLabel";
            this.sampleNameLabel.Size = new System.Drawing.Size(46, 18);
            this.sampleNameLabel.TabIndex = 0;
            this.sampleNameLabel.Text = "label2";
            this.sampleNameLabel.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Open Sans", 10F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(948, 35);
            this.label2.TabIndex = 4;
            this.label2.Text = "Возможные для осциллографирования параметры";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScopeSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(956, 527);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Open Sans", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScopeSetupForm";
            this.Text = "Конфигурация осциллографа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScopeSetupForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.possibleParamPanel.ResumeLayout(false);
            this.possibleParamPanel.PerformLayout();
            this.possibleTableLayoutPanel.ResumeLayout(false);
            this.possibleTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel possibleParamPanel;
        private System.Windows.Forms.TableLayoutPanel possibleTableLayoutPanel;
        private System.Windows.Forms.Label sampleNameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton reloadBtn;
        private System.Windows.Forms.ToolStripButton writeToSystemBtn;
        private System.Windows.Forms.CheckBox enaScopeCheckBox;
        private System.Windows.Forms.TextBox radioButton;
        private System.Windows.Forms.TextBox chCountRadioButton;
        private System.Windows.Forms.TextBox hystoryRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox oscFreqRadioButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label oscSize;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ToolStripButton openButton2;
        private System.Windows.Forms.ToolStripButton saveButton2;

        /*
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
         */

    }
}