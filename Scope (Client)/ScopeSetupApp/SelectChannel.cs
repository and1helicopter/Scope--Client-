using System.Collections.Generic;
using System.Windows.Forms;

namespace ScopeSetupApp
{
    public partial class SelectChannel : Form
    {
        public SelectChannel(List<string> nameChannel, List<int> numChannel, string str)
        {
           InitializeComponent();
           for (int i = 0; i < nameChannel.Count; i++)
           {
               comboBox1.Items.Add((numChannel[i] + 1) + ". " + nameChannel[i]);
           }
           comboBox1.SelectedIndex = 0;
           Format_label.Text = str;
        }

        public static int NumChannel;

        private void OKbutton_Click(object sender, System.EventArgs e)
        {
            NumChannel = comboBox1.SelectedIndex;
        }
    }
}
