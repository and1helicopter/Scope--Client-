namespace ScopeSetupApp
{
	partial class ConnectForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
			this.comsetPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.addrComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.parityComboBox = new System.Windows.Forms.ComboBox();
			this.stopBitstComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.speedComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.portComboBox = new System.Windows.Forms.ComboBox();
			this.connectBtn = new System.Windows.Forms.Button();
			this.disconnectBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.comsetPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// comsetPanel
			// 
			this.comsetPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.comsetPanel.Controls.Add(this.label1);
			this.comsetPanel.Controls.Add(this.addrComboBox);
			this.comsetPanel.Controls.Add(this.label5);
			this.comsetPanel.Controls.Add(this.label4);
			this.comsetPanel.Controls.Add(this.parityComboBox);
			this.comsetPanel.Controls.Add(this.stopBitstComboBox);
			this.comsetPanel.Controls.Add(this.label3);
			this.comsetPanel.Controls.Add(this.speedComboBox);
			this.comsetPanel.Controls.Add(this.label2);
			this.comsetPanel.Controls.Add(this.portComboBox);
			this.comsetPanel.Location = new System.Drawing.Point(2, 3);
			this.comsetPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.comsetPanel.Name = "comsetPanel";
			this.comsetPanel.Size = new System.Drawing.Size(187, 130);
			this.comsetPanel.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 24);
			this.label1.TabIndex = 11;
			this.label1.Text = "Адрес:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// addrComboBox
			// 
			this.addrComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.addrComboBox.FormattingEnabled = true;
			this.addrComboBox.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400"});
			this.addrComboBox.Location = new System.Drawing.Point(76, 28);
			this.addrComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.addrComboBox.Name = "addrComboBox";
			this.addrComboBox.Size = new System.Drawing.Size(106, 23);
			this.addrComboBox.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3, 101);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 24);
			this.label5.TabIndex = 9;
			this.label5.Text = "Четность:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 24);
			this.label4.TabIndex = 7;
			this.label4.Text = "Стоп биты:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// parityComboBox
			// 
			this.parityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.parityComboBox.FormattingEnabled = true;
			this.parityComboBox.Items.AddRange(new object[] {
            "Odd",
            "Even",
            "None"});
			this.parityComboBox.Location = new System.Drawing.Point(76, 103);
			this.parityComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.parityComboBox.Name = "parityComboBox";
			this.parityComboBox.Size = new System.Drawing.Size(106, 23);
			this.parityComboBox.TabIndex = 8;
			// 
			// stopBitstComboBox
			// 
			this.stopBitstComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stopBitstComboBox.FormattingEnabled = true;
			this.stopBitstComboBox.Items.AddRange(new object[] {
            "One",
            "Two"});
			this.stopBitstComboBox.Location = new System.Drawing.Point(76, 78);
			this.stopBitstComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.stopBitstComboBox.Name = "stopBitstComboBox";
			this.stopBitstComboBox.Size = new System.Drawing.Size(106, 23);
			this.stopBitstComboBox.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 24);
			this.label3.TabIndex = 5;
			this.label3.Text = "Скорость:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// speedComboBox
			// 
			this.speedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.speedComboBox.FormattingEnabled = true;
			this.speedComboBox.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800"});
			this.speedComboBox.Location = new System.Drawing.Point(76, 53);
			this.speedComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.speedComboBox.Name = "speedComboBox";
			this.speedComboBox.Size = new System.Drawing.Size(106, 23);
			this.speedComboBox.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "COM-порт:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// portComboBox
			// 
			this.portComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.portComboBox.FormattingEnabled = true;
			this.portComboBox.Location = new System.Drawing.Point(76, 3);
			this.portComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.portComboBox.Name = "portComboBox";
			this.portComboBox.Size = new System.Drawing.Size(106, 23);
			this.portComboBox.TabIndex = 2;
			this.portComboBox.Click += new System.EventHandler(this.portComboBox_Click);
			// 
			// connectBtn
			// 
			this.connectBtn.Location = new System.Drawing.Point(195, 6);
			this.connectBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.connectBtn.Name = "connectBtn";
			this.connectBtn.Size = new System.Drawing.Size(133, 24);
			this.connectBtn.TabIndex = 2;
			this.connectBtn.Text = "Установить соединение";
			this.connectBtn.UseVisualStyleBackColor = true;
			this.connectBtn.Click += new System.EventHandler(this.SetButton_Click);
			// 
			// disconnectBtn
			// 
			this.disconnectBtn.Location = new System.Drawing.Point(195, 31);
			this.disconnectBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.disconnectBtn.Name = "disconnectBtn";
			this.disconnectBtn.Size = new System.Drawing.Size(133, 24);
			this.disconnectBtn.TabIndex = 3;
			this.disconnectBtn.Text = "Разорвать";
			this.disconnectBtn.UseVisualStyleBackColor = true;
			this.disconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Location = new System.Drawing.Point(195, 105);
			this.cancelBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(133, 24);
			this.cancelBtn.TabIndex = 4;
			this.cancelBtn.Text = "Отмена";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(195, 81);
			this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(133, 24);
			this.button1.TabIndex = 5;
			this.button1.Text = "Сохранить настройки";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// ConnectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(333, 137);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.disconnectBtn);
			this.Controls.Add(this.connectBtn);
			this.Controls.Add(this.comsetPanel);
			this.Font = new System.Drawing.Font("Open Sans", 8F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Установка соединения";
			this.comsetPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel comsetPanel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox stopBitstComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox speedComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox portComboBox;
		private System.Windows.Forms.Button connectBtn;
		private System.Windows.Forms.Button disconnectBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox parityComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox addrComboBox;
    }
}