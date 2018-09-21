using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Xml.Linq;
using UniSerialPort;

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
								new XAttribute("Name", portComboBox.Text),
								new XAttribute("Speed", speedComboBox.Text),
								new XAttribute("Parity", parityComboBox.Text),
								new XAttribute("StopBits", stopBitstComboBox.Text),
                                new XAttribute("Address", addrComboBox.Text)),
							new XElement("MainWindow",
								new XAttribute("Height", MainForm.MainForm.SizeMainWindow.Height.ToString()),
								new XAttribute("Width", MainForm.MainForm.SizeMainWindow.Width.ToString()),
								new XAttribute("WindowState", MainForm.MainForm.WindowStateMainWindow == FormWindowState.Maximized ? 1.ToString() : 0.ToString()))));

				xDocument.Save(fs);
				fs.Close();
			}
			catch
			{
				MessageBox.Show(@"Ошибка при создании файла!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private readonly MainForm.MainForm _mainForm;

		public ConnectForm(bool connected, string currentParity, string currentStopBits, string currentSpeed,  string currentCom, string currentAddress, MainForm.MainForm form)
		{
			_mainForm = form;

			InitializeComponent();
            addrComboBox.Items.Clear();
		    for (int i = 1; i < 256; i++)
		    {
		        addrComboBox.Items.Add(i.ToString());
		    }

			foreach (string st in SerialPort.GetPortNames())
			{
				portComboBox.Items.Add(st);
				if (portComboBox.Items.Count != 0) portComboBox.SelectedIndex = 0;
			}

			portComboBox.Text = GetComPortName(currentCom);
			speedComboBox.Text = GetBaudRate(currentSpeed).ToString();
			stopBitstComboBox.Text = GetSerialPortStopBits(currentStopBits).ToString();
			parityComboBox.Text = GetSerialPortParity(currentParity).ToString();
		    addrComboBox.Text = GetSerialPortAddress(currentAddress).ToString();

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

		private void SetButton_Click(object sender, EventArgs e)
		{
			try
			{
				MainForm.MainForm.SerialPort.SerialPortMode = SerialPortModes.RSMode;
				MainForm.MainForm.SerialPort.BaudRate = GetBaudRate(speedComboBox.Text);
				MainForm.MainForm.SerialPort.Parity = GetSerialPortParity(stopBitstComboBox.Text);
				MainForm.MainForm.SerialPort.StopBits = GetSerialPortStopBits(stopBitstComboBox.Text);
				MainForm.MainForm.SerialPort.PortName = GetComPortName(portComboBox.Text);
			    MainForm.MainForm.SerialPort.SlaveAddr = GetSerialPortAddress(addrComboBox.Text);

                _mainForm.SerialPortOpen();
			}
			catch
			{
				MessageBox.Show(@"Ошибка при открытии COM - порта!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			MainForm.MainForm.SerialPort.Close();
			_mainForm.StopUpdate();
            Close();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			SaveComPortSettings("PrgSettings.xml");
		}
		
		private int GetBaudRate(object baudRate)
		{
			switch (baudRate)
			{
				case @"9600":
					return 9600;
				case @"19200":
					return 19200;
				case @"38400":
					return 38400;
				case @"57600":
					return 57600;
				case @"115200":
					return 115200;
				case @"230400":
					return 230400;
                case @"460800":
                    return 460800;
                default:
					speedComboBox.SelectedIndex = 0;
					return 9600;
			}
		}
		
		private Parity GetSerialPortParity(object serialPortParity)
		{
			switch (serialPortParity)
			{
				case @"Odd":
					return Parity.Odd;
				case @"Even":
					return Parity.Even;
				case @"None":
					return Parity.None;
				default:
					parityComboBox.SelectedIndex = 0;
					return Parity.Odd;
			}
		}

		private StopBits GetSerialPortStopBits(object serialPortStopBits)
		{
			switch (serialPortStopBits)
			{
				case @"One":
					return StopBits.One;
				case @"Two":
					return StopBits.Two;
				default:
					stopBitstComboBox.SelectedIndex = 0;
					return StopBits.One;
			}
		}

	    private byte GetSerialPortAddress(object serialPortAddress)
	    {
	        return Convert.ToByte(serialPortAddress);
	    }


        private string GetComPortName(object comPort)
		{
			if (portComboBox.Items.Count != 0)
			{
				return portComboBox.Items.Contains(comPort) ? comPort.ToString() : portComboBox.Items[0].ToString();
			}
			return "";
		}

		private void portComboBox_Click(object sender, EventArgs e)
		{
			foreach (string st in SerialPort.GetPortNames())
			{
				if (!portComboBox.Items.Contains(st))
				{
					portComboBox.Items.Add(st);
				}

				if (portComboBox.Items.Count != 0) portComboBox.SelectedIndex = 0;
			}
		}
	}
}
