﻿using System.Windows.Forms;

namespace ScopeApp
{
    public partial class LoadOscQueryForm : Form
    {
        public LoadOscQueryForm(string titl, bool enaDownLoadBtn, string textButton)
        {
            InitializeComponent();
            titlLabel.Text = titl;
            SaveOscil.Enabled = enaDownLoadBtn;
            SaveOscil.Text = textButton;
        }
    }
}
