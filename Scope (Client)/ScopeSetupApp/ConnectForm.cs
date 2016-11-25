using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModBusLibrary;
using System.Xml;

namespace ScopeSetupApp
{
    public partial class ConnectForm : Form
    {
        private void SaveComPortSettings(string comPortXmlName)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(comPortXmlName);

                XmlNodeList adds = doc.GetElementsByTagName("ComPort");
                foreach (XmlNode add in adds)
                {
                    add.Attributes["Name"].Value = portComboBox.SelectedIndex.ToString();
                    add.Attributes["Speed"].Value = speedComboBox.SelectedIndex.ToString();
                    add.Attributes["Parity"].Value = parityComboBox.SelectedIndex.ToString();
                    add.Attributes["Addr"].Value = addrComboBox.SelectedIndex.ToString();

                }
                doc.Save(comPortXmlName);
            }
            catch
            {
                MessageBox.Show(@"Ошибка при создании файла!", "Настройка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(@"Ошибка при создании файла!", "Настройка соединения", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if ((portComboBox.Items.Count - 1) < currentCom) 
                        { portComboBox.SelectedIndex = 0; } else { portComboBox.SelectedIndex = currentCom; }
            if ((addrComboBox.Items.Count - 1) < currentAddr) 
                        { addrComboBox.SelectedIndex = 0; } else { addrComboBox.SelectedIndex = currentAddr; }
            if ((speedComboBox.Items.Count - 1) < currentSpeed) 
                        { speedComboBox.SelectedIndex = 0; } else { speedComboBox.SelectedIndex = currentSpeed; }
            if ((parityComboBox.Items.Count - 1) < currentParity) 
                        { parityComboBox.SelectedIndex = 0; } else { parityComboBox.SelectedIndex = currentParity; }

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
                ModBusClient.OpenModBusPort((ushort)(addrComboBox.SelectedIndex + 1), 
                    CalcPortIndex(portComboBox.Items[portComboBox.SelectedIndex].ToString()), sp, par);
            }
            catch
            {
                MessageBox.Show(@"Ошибка при создании файла!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            ModBusClient.CloseModBusPort();
            this.Close();
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveComPortSettings("PrgSettings.xml");
        }
    }
}
