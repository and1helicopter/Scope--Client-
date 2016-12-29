using System.Windows.Forms;

namespace ScopeSetupApp
{
    public partial class LoadOscQueryForm : Form
    {
        public LoadOscQueryForm(string titl, bool enaDownLoadBtn)
        {
            InitializeComponent();
            titlLabel.Text = titl;
            SaveOscil.Enabled = enaDownLoadBtn;
        }
    }
}
