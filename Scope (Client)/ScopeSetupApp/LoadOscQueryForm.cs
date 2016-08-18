using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScopeSetupApp
{
    public partial class LoadOscQueryForm : Form
    {
        public LoadOscQueryForm(string titl, bool enaDownLoadBtn)
        {
            InitializeComponent();
            titlLabel.Text = titl;
            button1.Enabled = enaDownLoadBtn;
        }


    }
}
