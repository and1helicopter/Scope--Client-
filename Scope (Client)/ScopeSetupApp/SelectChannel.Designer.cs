namespace ScopeApp
{
    partial class SelectChannel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectChannel));
			this.OKbutton = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Format_label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OKbutton
			// 
			this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKbutton.Location = new System.Drawing.Point(137, 86);
			this.OKbutton.Name = "OKbutton";
			this.OKbutton.Size = new System.Drawing.Size(75, 23);
			this.OKbutton.TabIndex = 0;
			this.OKbutton.Text = "OK";
			this.OKbutton.UseVisualStyleBackColor = true;
			this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(12, 63);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(324, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(258, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Несколько каналов имеют один адрес и формат:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(175, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Выберите какой канал оставить:";
			// 
			// Format_label
			// 
			this.Format_label.AutoSize = true;
			this.Format_label.Location = new System.Drawing.Point(12, 27);
			this.Format_label.Name = "Format_label";
			this.Format_label.Size = new System.Drawing.Size(64, 13);
			this.Format_label.TabIndex = 5;
			this.Format_label.Text = "Format Addr";
			// 
			// SelectChannel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(348, 110);
			this.ControlBox = false;
			this.Controls.Add(this.Format_label);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.OKbutton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SelectChannel";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Предупреждение";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Format_label;
    }
}