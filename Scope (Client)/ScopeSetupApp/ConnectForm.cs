using System;
using System.IO;
using System.Windows.Forms;
using ModBusLibrary;
using System.Xml.Linq;
namespace ScopeSetupApp
{
    public partial class ConnectForm : Form
    {
        private void SaveComPortSettings(string comPortXmlName)
        {
            try
            {
                FileStream fs = new FileStream(comPortXmlName, FileMode.Create);

                XDocument xDocument =
                    new XDocument(
                        new XDeclaration("1.0", "utf-16", null),
                        new XElement("UcSettings",
                            new XElement("ComPort",
                                new XAttribute("Name", portComboBox.SelectedIndex.ToString()),
                                new XAttribute("Speed", speedComboBox.SelectedIndex.ToString()),
                                new XAttribute("Parity", parityComboBox.SelectedIndex.ToString()),
                                new XAttribute("Addr", addrComboBox.SelectedIndex.ToString())),
                            new XElement("MainWindow",
                                new XAttribute("Height", MainForm.SizeMainWindow.Height.ToString()),
                                new XAttribute("Width", MainForm.SizeMainWindow.Width.ToString()),
                                new XAttribute("WindowState", MainForm.WindowStateMainWindow == FormWindowState.Maximized ? 1.ToString() : 0.ToString()))));

                xDocument.Save(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show(@"Ошибка при создании файла!", @"Настройка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        public ConnectForm(bool connected, int currentAddr, int currentCom, int currentParity, int currentSpeed)
        {
            int i;

            InitializeComponent();

            foreach (string st in ModBusClient.PortList)
            {
                portComboBox.Items.Add(st);
                if (portComboBox.Items.Count != 0) portComboBox.SelectedIndex = 0;
            }

            for (i = 1; i < 257; i++)
            {
                addrComboBox.Items.Add(i.ToString());
            }

            portComboBox.SelectedIndex = (portComboBox.Items.Count - 1) < currentCom ? 0 : currentCom;
            addrComboBox.SelectedIndex = (addrComboBox.Items.Count - 1) < currentAddr ? 0 : currentAddr;
            speedComboBox.SelectedIndex = (speedComboBox.Items.Count - 1) < currentSpeed ? 0 : currentSpeed;
            parityComboBox.SelectedIndex = (parityComboBox.Items.Count - 1) < currentParity ? 0 : currentParity;

            if (connected)
            {
                comsetPanel.Enabled = false;
                connectBtn.Enabled = false;
                disconnectBtn.Enabled = true;
            }
            else
            {
                comsetPanel.Enabled = true;
                connectBtn.Enabled = true;
                disconnectBtn.Enabled = false;
            }


        }

        private byte CalcPortIndex(string portName)
        {
            byte i;
            string str = portName;
            if (str.Length == 5) { str = str[str.Length - 2].ToString() + str[str.Length - 1].ToString(); }
            else { str = str[str.Length - 1].ToString(); }

            try
            {
                i = Convert.ToByte(str);
            }
            catch
            {

                i = 0;
            }
            return i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte sp = (byte)speedComboBox.SelectedIndex;
            byte par = (byte)parityComboBox.SelectedIndex;
            try
            {
                ModBusClient.OpenModBusPort((ushort)(addrComboBox.SelectedIndex), 
                    CalcPortIndex(portComboBox.Items[portComboBox.SelectedIndex].ToString()), sp, par);
            }
            catch
            {
                MessageBox.Show(@"Ошибка при открытии COM - порта!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            ModBusClient.CloseModBusPort();
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveComPortSettings("PrgSettings.xml");
        }
    }
}
