namespace ScopeSetupApp
{
    partial class LoadOscQueryForm
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
            this.titlLabel = new System.Windows.Forms.Label();
            this.SaveOscil = new System.Windows.Forms.Button();
            this.ClearOscil = new System.Windows.Forms.Button();
            this.CancelLoadOscQueryForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titlLabel
            // 
            this.titlLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.titlLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlLabel.Location = new System.Drawing.Point(0, 0);
            this.titlLabel.Name = "titlLabel";
            this.titlLabel.Size = new System.Drawing.Size(396, 28);
            this.titlLabel.TabIndex = 0;
            this.titlLabel.Text = "label1";
            this.titlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaveOscil
            // 
            this.SaveOscil.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SaveOscil.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveOscil.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveOscil.Location = new System.Drawing.Point(12, 31);
            this.SaveOscil.Name = "SaveOscil";
            this.SaveOscil.Size = new System.Drawing.Size(121, 47);
            this.SaveOscil.TabIndex = 1;
            this.SaveOscil.Text = "Скачать";
            this.SaveOscil.UseVisualStyleBackColor = false;
            // 
            // ClearOscil
            // 
            this.ClearOscil.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClearOscil.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.ClearOscil.Location = new System.Drawing.Point(139, 31);
            this.ClearOscil.Name = "ClearOscil";
            this.ClearOscil.Size = new System.Drawing.Size(121, 47);
            this.ClearOscil.TabIndex = 2;
            this.ClearOscil.Text = "Очистить";
            this.ClearOscil.UseVisualStyleBackColor = false;
            // 
            // CancelLoadOscQueryForm
            // 
            this.CancelLoadOscQueryForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelLoadOscQueryForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelLoadOscQueryForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CancelLoadOscQueryForm.Location = new System.Drawing.Point(266, 31);
            this.CancelLoadOscQueryForm.Name = "CancelLoadOscQueryForm";
            this.CancelLoadOscQueryForm.Size = new System.Drawing.Size(121, 47);
            this.CancelLoadOscQueryForm.TabIndex = 3;
            this.CancelLoadOscQueryForm.Text = "Отмена";
            this.CancelLoadOscQueryForm.UseVisualStyleBackColor = false;
            // 
            // LoadOscQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(396, 87);
            this.Controls.Add(this.CancelLoadOscQueryForm);
            this.Controls.Add(this.ClearOscil);
            this.Controls.Add(this.SaveOscil);
            this.Controls.Add(this.titlLabel);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadOscQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор действия";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titlLabel;
        private System.Windows.Forms.Button SaveOscil;
        private System.Windows.Forms.Button ClearOscil;
        private System.Windows.Forms.Button CancelLoadOscQueryForm;
    }
}