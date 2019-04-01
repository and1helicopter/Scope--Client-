using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using ScopeApp.Format;
using ScopeApp.ucScopeConfig;
using ScopeApp.ucScopeSet;
using ScopeApp.ucSettings;
using UniSerialPort;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace ScopeApp.MainForm
{
	[SuppressMessage("ReSharper", "LocalizableElement")]
	public sealed partial class MainForm : Form
	{
		//путь приложения
		private string CalcApplPath()
		{
			string str = Application.ExecutablePath;
			int i = str.Length;
			char ch;

			do
			{
				ch = str[i - 1];
				if (ch != 0x5C)
				{
					i = i - 1;
				}
			} while ((ch != 0x5C) && (i > 0));

			var str2 = str.Substring(0, i);
			return (str2);
		}

		//Статусные кнопки загрузки осциллограмм
		private List<Button> _statusButtons;

		public static AsynchSerialPort SerialPort;

		private int _loadConfigStep;

		private void LoadWindowSize(string comPortXmlName, out int newHeight, out int newWidth, out int newWinState)
		{
			var doc = new XmlDocument();
			try
			{
				doc.Load(comPortXmlName);
			}
			catch
			{
				MessageBox.Show(@"Файл с настройками не найден!" + "\nCODE 0x1210", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			var xmls = doc.GetElementsByTagName(@"MainWindow");

			var xmlNode = xmls[0];
			try
			{
				// ReSharper disable once PossibleNullReferenceException
				newHeight = Convert.ToInt32(xmlNode.Attributes["Height"].Value);
				newWidth = Convert.ToInt32(xmlNode.Attributes["Width"].Value);
				newWinState = Convert.ToInt32(xmlNode.Attributes["WindowState"].Value);}
			catch
			{
				newHeight = 600;
				newWidth = 800;
				newWinState = 0;
			}
		}

		public static Size SizeMainWindow;
		public static FormWindowState WindowStateMainWindow;

		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
			SizeMainWindow = Size;
			WindowStateMainWindow = WindowState;
		}

		private void SaveWindowSize(string comPortXmlName)
		{
			try
			{
				var doc = new XmlDocument();
				doc.Load(comPortXmlName);

				XmlNodeList adds = doc.GetElementsByTagName("MainWindow");
				foreach (XmlNode add in adds)
				{
					// ReSharper disable once PossibleNullReferenceException
					add.Attributes["Height"].Value = Height.ToString();
					add.Attributes["Width"].Value = Width.ToString();
					add.Attributes["WindowState"].Value = WindowState == FormWindowState.Maximized ? 1.ToString() : 0.ToString();
				}
				doc.Save(comPortXmlName);
			}
			catch
			{
				// ignored
			}
		}

		private readonly string[] _argsG;
		private byte _buttonsStatus;

		private static void Get45PlusFromRegistry()
		{
			const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

			try
			{
				using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
				{
					if (ndpKey?.GetValue("Release") != null)
					{
						var version = (int)ndpKey.GetValue("Release");
						//MessageBox.Show($"version NET.Framework {CheckFor45PlusVersion(version)}", @"Information", MessageBoxButtons.OK,
						//	MessageBoxIcon.Information);
						if (version < 379893)
						{
							MessageBox.Show($"На устройстве установленна версия NET.Framework {CheckFor45PlusVersion(version)}\n" +
							                "Для стабильной работа приложения требуется версия NET.Framework 4.5.2",
								@"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					else
					{
						MessageBox.Show("На устройстве установленна версия NET.Framework ниже 4.5\n" +
						                "Для стабильной работа приложения требуется версия NET.Framework 4.5.2", 
							@"Error", MessageBoxButtons.OK,
							MessageBoxIcon.Error);
					}
				}
			}
			catch
			{
				MessageBox.Show("На устройстве установленна версия NET.Framework ниже 4.5\n" +
								"Для стабильной работа приложения требуется версия NET.Framework 4.5.2",
					@"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static string CheckFor45PlusVersion(int releaseKey)
		{
			if (releaseKey >= 461808)
				return "4.7.2 or later";
			if (releaseKey >= 461308)
				return "4.7.1";
			if (releaseKey >= 460798)
				return "4.7";
			if (releaseKey >= 394802)
				return "4.6.2";
			if (releaseKey >= 394254)
				return "4.6.1";
			if (releaseKey >= 393295)
				return "4.6";
			if (releaseKey >= 379893)
				return "4.5.2";
			if (releaseKey >= 378675)
				return "4.5.1";
			if (releaseKey >= 378389)
				return "4.5";
			// This code should never execute. A non-null release key should mean
			// that 4.5 or later is installed.
			return "No 4.5 or later version detected";
		}

		public MainForm(string[] agrs)
		{
			Get45PlusFromRegistry();

			_argsG = agrs;

			InitializeComponent();

			InitializeFormat();
			FormatStrLabel();
			
			InitializeConfig();
			ConfigStrLabel();

			DoubleBuffered = true;

			if (!ScopeSysType.HasViewer)
			{
				OpenScope_Button.Visible = false;
			}

			if (agrs.Length > 0)
			{
				if (agrs[0] == "a" || agrs[0] == "A")
				{
					ConfigScopeButton.Visible = true;
					Setting_Button.Visible = true;
				} 
			}

			SerialPort = new AsynchSerialPort();

			UpdateGrid();

			LoadWindowSize("prgSettings.xml", out var height, out var width, out var winState);
			
			Size size = new Size(width, height);
			Size = size;

			WindowState = winState == 1 ? FormWindowState.Maximized : FormWindowState.Normal;
			ButtonsTimer.Enabled = true;
		}

		private static void InitializeFormat()
		{
			FormatConverter.ReadFormats(null);
			FormatConverter.UpdateVisualFormat();
		}

		private void InitializeConfig()
		{
			try
			{
				ScopeSysType.InitScopeSysType();
				config_toolStripStatusLabel.Text = "Конфигурация: " + ScopeSysType.XmlFileName;
			}
			catch
			{
				// ignored
			}
		}

		private delegate void ConfigLabel();
		private static ConfigLabel _configStatusLabel;
		
		public void ConfigStrLabel()
		{
			if (config_toolStripStatusLabel != null)
			{
				_configStatusLabel = UpdateConfigStrLabel;
				_configStatusLabel.Invoke();
			}
		}

		private void UpdateConfigStrLabel()
		{
			config_toolStripStatusLabel.Text = "Конфигурация: " + ScopeSysType.XmlFileName.Split('\\').Last();
		}

		private delegate void FormatLabel();
		private static FormatLabel _formatStatusLabel;

		public void FormatStrLabel()
		{
			if (format_toolStripStatusLabel != null)
			{
				_formatStatusLabel = UpdateFormatStrLabel;
				_formatStatusLabel.Invoke();
			}
		}

		private void UpdateFormatStrLabel()
		{
			format_toolStripStatusLabel.Text = FormatConverter.OldFormat ? @"Формат данных: OLD" : @"Формат данных: NEW";
		}

		//************************** ВЫЗОВЫ ДОЧЕРНИХ ОКОН ***************************************//
		//***************************************************************************************//
		//***************************************************************************************//

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}

		private UcScopeSetup _ucScopeSetup;

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			try
			{
				var oldStatus = _buttonsStatus;
				_buttonsStatus = (byte)(_buttonsStatus == 0x02 ? 0x00 : 0x02);

				var draw = _buttonsStatus == 0x00 || (oldStatus == 0x00 && _buttonsStatus == 0x02);

				if (draw)
				{
					nowStatusFlowLayoutPanel.Visible = false;
				}

				VarificationUc();
				UpdateButtons();

				if (_ucScopeSetup == null)
				{
					_ucScopeSetup = new UcScopeSetup(_argsG)
					{
						Dock = DockStyle.Fill
					};
				}

				panel1.SuspendLayout();
				panel1.Controls.Add(_ucScopeSetup);
				panel1.ResumeLayout();

				if (draw)
				{
					nowStatusFlowLayoutPanel.Visible = true;
				}
			}
			catch 
			{
				//ignore
			}
		}

		private UcScopeConfig _ucScopeConfig;

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			try
			{
				var oldStatus = _buttonsStatus;
				_buttonsStatus = (byte)(_buttonsStatus == 0x01 ? 0x00 : 0x01);

				var draw = _buttonsStatus == 0x00 || (oldStatus == 0x00 && _buttonsStatus == 0x01);

				if (draw)
				{
					nowStatusFlowLayoutPanel.Visible = false;
				}

				VarificationUc();
				UpdateButtons();

				if (_ucScopeConfig == null)
				{
					_ucScopeConfig = new UcScopeConfig()
					{
						Dock = DockStyle.Fill
					};
				}

				panel1.SuspendLayout();
				panel1.Controls.Add(_ucScopeConfig);
				panel1.ResumeLayout();

				if (draw)
				{
					nowStatusFlowLayoutPanel.Visible = true;
				}
			}
			catch 
			{
				//ignore
			}
		}

		private UcSettings _ucSettings;

		private void Setting_Button_Click(object sender, EventArgs e)
		{
			try
			{
				var oldStatus = _buttonsStatus;
				_buttonsStatus = (byte)(_buttonsStatus == 0x03 ? 0x00 : 0x03);

				var draw = _buttonsStatus == 0x00 || (oldStatus == 0x00 && _buttonsStatus == 0x03);

				if (draw)
				{
					nowStatusFlowLayoutPanel.Visible = false;
				}

				VarificationUc();
				UpdateButtons();

				if (_ucSettings == null)
				{
					_ucSettings = new UcSettings()
					{
						Dock = DockStyle.Fill
					};
				}

				panel1.SuspendLayout();
				panel1.Controls.Add(_ucSettings);
				panel1.ResumeLayout();

				if (draw)
				{
					nowStatusFlowLayoutPanel.Visible = true;
				}
			}
			catch 
			{
				//ignore
			}
		}

		delegate void Varification();

		private Varification _varification;

		private void VarificationUc()
		{
			if(_ucScopeConfig != null)
			{
				_varification = _ucScopeConfig.Varification;
				_varification.Invoke();
			}
		}

		private void UpdateButtons()
		{
			if (_buttonsStatus == 0x00)
			{
				toolStrip1.Size = new Size(244, 444);
				UpdateButtonsReset(connectBtn);
				UpdateButtonsReset(ConfigScopeButton);
				UpdateButtonsReset(ConfigMCUButton);
				UpdateButtonsReset(OpenScope_Button);
				UpdateButtonsReset(Setting_Button);
			    Infopanel.Size = new Size(244, 68);

                panel1.Controls.Remove(_ucSettings);
				panel1.Controls.Remove(_ucScopeConfig);
				panel1.Controls.Remove(_ucScopeSetup);

				panel1.Controls.Clear();

				_ucSettings = null;
				_ucScopeConfig = null;
				_ucScopeSetup = null;

				UpdateGrid();
			}
			else
			{
				toolStrip1.Size = new Size(74, 444);

				UpdateButtonsSets(connectBtn);
				UpdateButtonsSets(OpenScope_Button);
				Infopanel.Size = new Size(74, 68);

				switch (_buttonsStatus)
				{
					case 0x01:
						UpdateButtonsSet(ConfigScopeButton);
						UpdateButtonsSets(ConfigMCUButton);
						UpdateButtonsSets(Setting_Button);
						break;
					case 0x02:
						UpdateButtonsSets(ConfigScopeButton);
						UpdateButtonsSet(ConfigMCUButton);
						UpdateButtonsSets(Setting_Button);
						break;
					case 0x03:
						UpdateButtonsSets(ConfigScopeButton);
						UpdateButtonsSets(ConfigMCUButton);
						UpdateButtonsSet(Setting_Button);
						break;
				}

				UpdateGrid();
				panel1.Controls.Clear();
			}

			UpdateStatusLoad();
			Refresh();
			UpdateStatusButtons();
		}

		private void UpdateButtonsSet(ToolStripButton button)
		{
			button.Size = new Size(60, 60);
			button.BackColor = Color.CornflowerBlue;
			button.DisplayStyle = ToolStripItemDisplayStyle.Image;
		}

		private void UpdateButtonsSets(ToolStripButton button)
		{
			button.Size = new Size(60, 60);
			button.BackColor = SystemColors.ButtonFace;
			button.DisplayStyle = ToolStripItemDisplayStyle.Image;
		}

		private void UpdateButtonsReset(ToolStripButton button)
		{		
			button.Size = new Size(230, 60);
			button.BackColor = SystemColors.ButtonFace;
			button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
		}

		//***************************************************************************//
        //***************************************************************************//
        //***************************************************************************//

        //***************************************************************************//
        //***************************************************************************//
        //***************************************************************************//
        private void connectBtn_Click(object sender, EventArgs e)
		{
			OpenPort();
		}

		private void LoadComPortSettings(string comPortXmlName, out string newParity, out string newSpeed, out string newPort, out string newStopBits, out string newAddress)
		{
			var doc = new XmlDocument();
			try
			{
				doc.Load(comPortXmlName); 
			}
			catch
			{
				// ignored
			}

			var xmls = doc.GetElementsByTagName("ComPort");

			var xmlNode = xmls[0];
			try
			{
				newPort = xmlNode.Attributes?["Name"].Value;
				newSpeed = xmlNode.Attributes?["Speed"].Value;
				newParity = xmlNode.Attributes?["Parity"].Value;
				newStopBits = xmlNode.Attributes?["StopBits"].Value;
                newAddress = xmlNode.Attributes?["Address"].Value;
            }
			catch
			{
				newPort = "";
				newSpeed = "9600";
				newParity = "Odd";
				newStopBits = "One";
			    newAddress = "1";
                MessageBox.Show(@"Ошибки в файле с настройками!" + "\nCODE 0x1211", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void OpenPort()
		{
			if (SerialPort.PortName.Length == 0)
			{
				MessageBox.Show(@"Нет ни одного доступного COM-порта!" + "\nCODE 0x1100", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

		    LoadComPortSettings(@"PrgSettings.xml", out var parity, out var speed, out var port, out var stopBits, out var addr);

			ConnectForm connectForm = new ConnectForm(SerialPort.IsOpen, parity, stopBits, speed, port, addr,this);
			connectForm.ShowDialog();
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			OpenPort();
		}


		//*****************************************************************************************//
		//*****************************************************************************************//
		//*****************************************************************************************//
		private readonly ushort[] _oscilsStatus = { 0, 0, 0, 0,   0, 0, 0, 0,  0, 0, 0, 0,     0, 0, 0, 0, 
								  0, 0, 0, 0,   0, 0, 0, 0,  0, 0, 0, 0,     0, 0, 0, 0};

		private readonly string[] _oscilTitls = new string[32];
		private readonly DateTime [] _date = new DateTime[32];

		public void StopUpdate()
		{
		    _loadOscilIndex = 0;
		    _loadOscilTemp = 0;
		    _loadOscData = false;
		    _loadOscDataStep = 0;
		    _countTemp = 0;

            _updateTimer = false;
			_updateStatus = false;
		    _connectToSystem = false;
			SerialPort.Requests.Clear();
			RemoveStatusButtons();

            ScopeConfig.ChangeScopeConfig = true;
			ScopeConfig.ScopeCount = 0;
			ScopeConfig.StatusOscil = 0x0000;
		}

		//Очистка осциллограмм
		private int _clearOscNum = 0x7FFF;

		private bool _updateTimer;
		private bool _createFileFlag;
		private SaveFileDialog _saveFileDialog;

		private void ButtonsTimer_Tick(object sender, EventArgs e)
		{
			ButtonsTimer.Enabled = false;

			if (SerialPort != null)
			{
				UpdateStatusComPort();
			    UpdateStatusConnection();
			    UpdateStatusСonfig();                       //Update statusСonfig in system
                UpdateGrid();
				UpdateSize();
				UpdateFreq();

				if (_createFileFlag)
				{
					_createFileFlag = false;
					if(_saveFileDialog != null)
						CreateFile(_saveFileDialog);
				}

				if (_updateTimer)
				{
					UpdateStatusButtonsInvoke();
					UpdateOscilButtonsStatusInvoke();
					UpdateStatus();
					UpdateTimeStamp();
					ValidetionConnect();

					ScopeConfig.ConnectMcu = true;
				}
			}

			ButtonsTimer.Enabled = true;
		}
		
		delegate void UpdateOscilButtonsStatusDelegate();

		private void UpdateOscilButtonsStatusInvoke()
		{
			Invoke(new UpdateOscilButtonsStatusDelegate(UpdateOscilButtonsStatus));
		}

		private void UpdateOscilButtonsStatus()
		{
			if (_statusLoad)
			{
				_statusButtons?.ForEach(x =>
				{
					if ((int)x.Tag != _loadOscNum) x.Enabled = false;
				});
			}
			else
			{
				_statusButtons?.ForEach(x =>
				{
					if(_oscilsStatus[(int)x.Tag] != 0x00) x.Enabled = true;
				});
			}
		}

		//**************** ДИНАМИЧЕСКОЕ СОЗДАНИЕ КОНТРОЛОВ***************************************//
		//***************************************************************************************//
		//***************************************************************************************//

		private bool _buttonsAlreadyCreated;

		private void CreateStatusButtons()
		{
			RemoveStatusButtons();
			AddStutusButtons();
		}

		private delegate void AddButtonDelegate(Button button);

		private void AddStutusButtons()
		{
			if (_buttonsAlreadyCreated)
			{
				return;
			}
			_statusButtons = new List<Button>();

			Font font = new Font(@"Open Sans", 9);
			Size size = new Size(120, 60);

			if (_buttonsStatus != 0x00)
			{
				font = new Font(@"Open Sans", 8);
				size = new Size(75, 45);
			}

			for (int i = 0; i < ScopeConfig.ScopeCount; i++)
			{
				_statusButtons.Add(new Button());
				_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Пусто";
				_statusButtons[i].Size = size;
				_statusButtons[i].Font = font;
				_statusButtons[i].Margin = new Padding(2);
				_statusButtons[i].Tag = i;
				_statusButtons[i].Dock = DockStyle.None;
				_statusButtons[i].Tag = i;
				_statusButtons[i].Click += LoadOscBtnClick;

				Invoke(new AddButtonDelegate(AddButton), _statusButtons[i]);
			}

			_buttonsAlreadyCreated = true;
		}

		private void AddButton(Button button)
		{
			nowStatusFlowLayoutPanel.Controls.Add(button);
		}

		private delegate void RemoveButtonDelegate();

		private void RemoveStatusButtons()
		{
			try
			{
				if (_statusButtons != null)
				{
					if (_statusButtons?.Count != 0)
					{
						Invoke(new RemoveButtonDelegate(RemoveButton));
						_statusButtons.Clear();
					}
				}
				_buttonsAlreadyCreated = false;
			}
			catch 
			{
				//ignore
			}
		}

		private void RemoveButton()
		{
			nowStatusFlowLayoutPanel.Controls.Clear();
		}

		//*************** ОБНОВЛЕНИЕ КОНТРОЛОВ В АСИНХРОННОМ РЕЖИМЕ ***************************************//
		//*************************************************************************************************//
		//*************************************************************************************************//
		private delegate void NoParamDelegate();

		private void UpdateStatusButtonsInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateStatusButtons));
			}
			catch 
			{
				//ignore
			}
		}

		private void UpdateStatusButtons()
		{
			if (_statusButtons != null)
			{
				if (_buttonsStatus == 0x00)
				{
					if (_statusButtons.Count != 0)
					{
						if (_statusButtons[_statusButtons.Count - 1].Size != new Size(120, 60))
						{
							foreach (var button in _statusButtons)
							{
								button.Size = new Size(120, 60);
								button.Font = new Font(@"Open Sans", 9);
							}
						}
					}
				}
				else
				{
					if (_statusButtons.Count != 0)
					{
						if (_statusButtons[_statusButtons.Count - 1].Size != new Size(75, 45))
						{
							foreach (var button in _statusButtons)
							{
								button.Size = new Size(75, 45);
								button.Font = new Font(@"Open Sans", 8);
							}
						}
					}
				}
			}
		}

		private void UpdateOscilsStatusInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateOscilsStatus));
			}
			catch
			{
				//ignore
			}
		}

		private void UpdateOscilsStatus()
		{
			if (_statusButtons != null)
			{
				for (int i = 0; i < ScopeConfig.ScopeCount; i++)
				{
					if (_oscilsStatus[i] == 0)
					{
						_statusButtons[i].FlatStyle = FlatStyle.Standard;
						_statusButtons[i].BackColor = Color.White;
						_statusButtons[i].Enabled = false;
						_oscilTitls[i] = @"Осциллограмма №" + (i + 1) + @".";
						_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Пусто";

					}
					else if (_oscilsStatus[i] > 4)
					{
						_statusButtons[i].BackColor = Color.CornflowerBlue;
						if(!_statusLoad) _statusButtons[i].Enabled = true;
					}
					else if (_oscilsStatus[i] == 4)
					{
						_statusButtons[i].BackColor = Color.LightSteelBlue;
						if (!_statusLoad) _statusButtons[i].Enabled = true;
					}

					else if (_oscilsStatus[i] == 3)
					{
						_statusButtons[i].FlatStyle = FlatStyle.Standard;
						_statusButtons[i].BackColor = Color.Lavender;

						if (!_statusLoad) _statusButtons[i].Enabled = true;
						_oscilTitls[i] = @"Осциллограмма №" + (i + 1) + @".";
						_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Записывается осциллограмма";
					}
					else if (_oscilsStatus[i] == 1)
					{
						_statusButtons[i].BackColor = Color.GhostWhite;
						if (!_statusLoad) _statusButtons[i].Enabled = true;
						_oscilTitls[i] = @"Осциллограмма №" + (i + 1) + @".";
						_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Предыстория записывается";
					}
					else
					{
						_statusButtons[i].BackColor = Color.AliceBlue;
						if (!_statusLoad) _statusButtons[i].Enabled = true;
						_oscilTitls[i] = @"Осциллограмма №" + (i + 1) + @".";
						_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Ожидание события записи";
					}
				}
			}
		}

		private delegate void UpdateTimeDelegate(int index, string strStatusButton, string strTitle, DateTime date); 

		private void UpdateTimeInvoke(int index, string strStatusButton, string strTitle, DateTime date)
		{
			Invoke(new UpdateTimeDelegate(UpdateTimeStatus), index, strStatusButton, strTitle, date);
		}

		private void UpdateTimeStatus(int index, string strStatusButton,  string strTitle, DateTime date )
		{
			if (_statusButtons?.Count != 0)
			{
				// ReSharper disable once PossibleNullReferenceException
				_statusButtons[index].Text = strStatusButton;
				_oscilTitls[index] = strTitle;
				_date[index] = date;
			}
		}

		private void UpdateLoadDataProgressBar()
		{
			if (!loadDataProgressBar.Visible)
			{
			    stopDownloadStripButton.Visible = true;
                loadDataProgressBar.Visible = true;
				loadScopeToolStripLabel.Visible = true;
			}
			UpdateStatusLoad();
			loadDataProgressBar.Value = (int)_loadOscilIndex;
		}
		private void UpdateLoadDataProgressBarInvoke()
		{
			Invoke(new NoParamDelegate(UpdateLoadDataProgressBar));
		}

		private void HideProgressBar()
		{
		    stopDownloadStripButton.Visible = false;
            loadDataProgressBar.Visible = false;
			loadScopeToolStripLabel.Visible = false;
			loadDataProgressBar.Value = 0;
		}

		private void HideProgressBarInvoke()
		{
			Invoke(new NoParamDelegate(HideProgressBar), null);
		}

		private void UpdateStatusLoad()
		{
			if (loadDataProgressBar.Visible)
			{
				if (_buttonsStatus != 0x00)
				{
				    stopDownloadStripButton.Visible = true;
				    stopDownloadStripButton.Margin = new Padding(10, 0, 10, 0);
                    loadScopeToolStripLabel.Visible = false;
					loadDataProgressBar.Size = new Size(50, 24);
				}
				else
				{
				    stopDownloadStripButton.Visible = true;
				    stopDownloadStripButton.Margin = new Padding(45, 0, 45, 0);
                    loadScopeToolStripLabel.Visible = true;
					loadDataProgressBar.Size = new Size(150, 24);
				}
			}
			else
			{
			    stopDownloadStripButton.Visible = false;
                loadDataProgressBar.Visible = false;
				loadScopeToolStripLabel.Visible = false;
			}
		}

		//*************************** СКАЧИВАНИЕ ОСЦИЛЛОГРАММ В ФАЙЛ ************************************//
		//***********************************************************************************************//
		//***********************************************************************************************//
		#region

		private bool _statusLoad;

		private void LoadOscBtnClick(object sender, EventArgs e)
		{
			ScopeConfig.InitOscilParams(ScopeConfig.OscilAddr, ScopeConfig.OscilFormat);
			bool b = _oscilsStatus[(int)((Button) sender).Tag] >= 4;
		    string textButton = _oscilsStatus[(int)((Button)sender).Tag] <= 4 ? @"Скачать" : @"Сброс";

			LoadOscQueryForm loadOscQueruForm = new LoadOscQueryForm(_oscilTitls[(int)((Button) sender).Tag], b, textButton);
			DialogResult dlgr = loadOscQueruForm.ShowDialog();

			//СКАЧИВАНИЕ ОСЦИЛЛОГРАММ
			if (dlgr == DialogResult.OK)
			{
				_loadOscNum = (int)((Button)sender).Tag;

				if (_oscilsStatus[_loadOscNum] == 0x04)
				{
					_saveFileDialog = new SaveFileDialog
					{
						DefaultExt = @".txt",
						Filter = @"Text Files (*.txt)|*.txt|COMTRADE rev. 1999 (*.cfg)|*.cfg|COMTRADE rev. 2013 (*.cfg)|*.cfg"
					};
					if (_saveFileDialog.ShowDialog() != DialogResult.OK)
					{
						return;
					}

					_statusLoad = true;
				}
				else
				{
					_statusLoad = false;
				}

				_oscilStartTemp = ((uint)_loadOscNum * (ScopeConfig.OscilSize >> 1)); //Начало осциллограммы 
				_oscilEndTemp = (((uint)_loadOscNum + 1) * (ScopeConfig.OscilSize >> 1)); //Конец осциллограммы 
				SetScopeStatus(_loadOscNum);
			}

			//СБРОС ОСЦИЛЛОГРАММ
			else if (dlgr == DialogResult.Abort)
			{
				_clearOscNum = (int)((Button) sender).Tag;
				ClearScopeStatus(_clearOscNum);
			}
		}
		
		private readonly ushort[] _loadParamPart = new ushort[32];
		#endregion

		//СОЗДАНИЕ ФАЙЛА
		// Save to .txt
		#region

		private string FileHeaderLine()
		{
			string str = " " + "\t";
			for (int i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				str = str + ScopeConfig.ChannelName[i].Substring(0,29).Replace("\0", String.Empty) + "\t";
			}
			return str;
		}

	    private string FileHeaderLineColor()
	    {
	        string str = " " + "\t";
	        for (int i = 0; i < ScopeConfig.ChannelCount; i++)
	        {
	            var name = Encoding.Default.GetBytes(ScopeConfig.ChannelName[i].Substring(29, 3));
                str = str + BitConverter.ToString(name).Replace("-", string.Empty).ToLower() + "\t";
	        }
	        return str;
	    }

        private int _count64, _count32 , _count16;

		/*
		 ushort Format 
		 Contain:
		 (byte)							|(byte)
		 Bit depth index		|Format index   			 
		*/

		private string FileParamLine(ushort[] paramLine, int lineNum)
		{
			int i;
		   // ChFormats();
			string str = lineNum + "\t";
			for (i = 0, _count64 = 0, _count32 = 0, _count16 = 0; i < ScopeConfig.ChannelCount; i++)
			{
				var ulTemp = ParseArr(i, paramLine);
				str = str + FormatConverter.GetValue(ulTemp, (byte) ScopeConfig.OscilFormat[i]) + "\t";
			}
			return str;
		}
		#endregion

		private ulong ParseArr(int i, ushort[] paramLine)
		{
			ulong ulTemp = 0;
			if (ScopeConfig.OscilFormat[i] >> 8 == 3)
			{
			   ulTemp = 0;
			   ulTemp += (ulong)(paramLine[_count64 + 0]) << 8 * 2;
			   ulTemp += (ulong)(paramLine[_count64 + 1]) << 8 * 3;
			   ulTemp += (ulong)(paramLine[_count64 + 2]) << 8 * 0;
			   ulTemp += (ulong)(paramLine[_count64 + 3]) << 8 * 1;
			   _count64 += 4;
			}
			if (ScopeConfig.OscilFormat[i] >> 8 == 2)
			{
				ulTemp = 0;
				ulTemp += (ulong)(paramLine[_count64 + _count32 + 0]) << 8 * 0;
				ulTemp += (ulong)(paramLine[_count64 + _count32 + 1]) << 8 * 1;
				_count32 += 2; 
			}
			if (ScopeConfig.OscilFormat[i] >> 8 == 1)
			{
				ulTemp = paramLine[_count64 + _count32 + _count16];
				_count16 += 1;
			}
			return ulTemp;
		}

		//Формирование строк всех загруженных данных
		private List<ushort[]> InitParamsLines()
		{
			List<ushort[]> paramsLines = new List<ushort[]>();
			List<ushort[]> paramsSortLines = new List<ushort[]>();
			int k = 0;
			int j = 0;
			int l = 0;
			foreach (ushort[] t in _downloadedData)
			{
				while (j < 32)
				{
					if (k == 0) paramsLines.Add(new ushort[ScopeConfig.SampleSize >> 1]);
					while (k < (ScopeConfig.SampleSize >> 1) && j < 32)
					{
						paramsLines[paramsLines.Count - 1][k] = t[j];
						k++;
						j++;
					}
					if (k == (ScopeConfig.SampleSize >> 1)) k = 0;
				}
				j = 0;
			}
		   // paramsLines.RemoveAt(paramsLines.Count-1);
			//Формирую список начиная с предыстории 
			for(int i = 0; i < paramsLines.Count; i++)
			{
				if ((i + (int)_startLoadSample + 1) >= paramsLines.Count)
				{
					paramsSortLines.Add(new ushort[ScopeConfig.SampleSize >> 1]);
					paramsSortLines[i] = paramsLines[l];
					l++;
				}
				else
				{
					paramsSortLines.Add(new ushort[ScopeConfig.SampleSize >> 1]);
					paramsSortLines[i] = paramsLines[i + (int)_startLoadSample + 1];
				}
			}
			return paramsSortLines;
		}
  
		//Save to cometrade
		#region

		private string FileParamLineData(ushort[] paramLine, int lineNum)
		{
			string str1;
			int i;
			ulong ulTemp;
			_count64 = 0; 
			_count32 = 0; 
			_count16 = 0;
			string str = (lineNum + 1) + ",";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				if (ScopeConfig.ChannelType[i] == 0)
				{
					ulTemp = ParseArr(i, paramLine);
					str1 = FormatConverter.GetValue(ulTemp, (byte)ScopeConfig.OscilFormat[i]);
					str1 = str1.Replace(",", ".");
					str = str + "," + str1;
				}
			}
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{

				if (ScopeConfig.ChannelType[i] == 1)
				{
					ulTemp = ParseArr(i, paramLine);
					str1 = FormatConverter.GetValue(ulTemp, (byte)ScopeConfig.OscilFormat[i]);
					str1 = str1.Replace(",", ".");
					str = str + "," + str1;
				}
			}
			return str;
		}

		private string Line1( int filterIndex)
		{
			string stationName = ScopeConfig.StationName;
			string recDevId = ScopeConfig.RecordingId;
			string revYear = "";
			if (filterIndex == 2) revYear = "1999";
			if (filterIndex == 3) revYear = "2013";
			string str = stationName + "," + recDevId + "," + revYear;
			return str;
		}

		private string Line2()
		{
			int nA = 0 , nD = 0;
			for (int i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.ChannelType[i] == 0) nA += 1;
				if (ScopeConfig.ChannelType[i] == 1) nD += 1;
			}
			int tt = nA + nD;
			string str = tt + "," + nA + "A," + nD + "D";
			return str;
		}

		private string Line3(int num ,int nA)
		{
			string chId = ScopeConfig.ChannelName[num].Substring(0, 29).Replace("\0", String.Empty);
			string ph = ScopeConfig.ChannelPhase[num];
			string ccbm = ScopeConfig.ChannelCcbm[num];
			string uu = ScopeConfig.ChannelDemension[num];
			string a = "1";
			string b = "0";
			int skew = 0;
			double min;
			double max;
			try
			{
				min = ScopeSysType.ScopeItem[ScopeConfig.OscilParams[num]].ChannelMin;
				max = ScopeSysType.ScopeItem[ScopeConfig.OscilParams[num]].ChannelMax;
			}
			catch
			{
				min = 0;
				max = 0;
			}

			int primary = 1; 
			int secondary = 1;
			string ps = "P";

			string str = nA + "," + chId + "," + ph + "," + ccbm + "," + uu + "," + a + "," + b + "," + skew + "," +
						 min + "," + max + "," + primary + "," + secondary + "," + ps;

			return str;
		}

		private string Line4(int num, int nD)
		{
			string chId = ScopeConfig.ChannelName[num];
			string ph = ScopeConfig.ChannelPhase[num];
			string ccbm = ScopeConfig.ChannelCcbm[num];
			int y = 0;

			string str = nD + "," + chId + "," + ph + "," + ccbm + "," + y;

			return str;
		}

		private string Line5()
		{
			string str;
			try
			{
				str = ScopeSysType.OscilNominalFrequency.ToString();
			}
			catch
			{
				str = "50";
			}

			return str;
		}

		private string Line6()
		{
			string nrates = "1";
			return nrates;
		}

		private string Line7()
		{
			string samp = Convert.ToString(ScopeConfig.SampleRate / ScopeConfig.FreqCount);
			samp = samp.Replace(",", ".");
			string endsamp = InitParamsLines().Count.ToString();
			string str = samp + "," + endsamp;
			return str;
		}

		private string Line8(int numOsc)
		{
		   //Время начала осциллограммы 
			double milsec = 1000*(double)ScopeConfig.OscilHistCount/((double)ScopeConfig.SampleRate / ScopeConfig.FreqCount);

			DateTime dateTemp = _date[numOsc].AddMilliseconds(-milsec);
			return dateTemp.ToString("dd'/'MM'/'yyyy,HH:mm:ss.fff000");
		}

		private string Line9(int numOsc)
		{
			//Время срабатывания триггера
			DateTime dateTemp = _date[numOsc];
			return dateTemp.ToString("dd'/'MM'/'yyyy,HH:mm:ss.fff000");
		}

		private string Line10()
		{
			string ft = "ASCII";
			return ft;
		}

		private string Line11()
		{
			string timemult = "1";
			return timemult;
		}

		private string Line12()
		{
			string timecode = ScopeConfig.TimeCode;
			string localcode = ScopeConfig.LocalCode;
			return timecode + "," + localcode;
		}

		private string Line13()
		{
			string tmqCode = ScopeConfig.TmqCode;
			string leapsec = ScopeConfig.Leapsec;
			return tmqCode + "," + leapsec; 
		}
		#endregion//Save to cometrade

		private void CreateFile(SaveFileDialog sfd)
		{
			// Save to .txt
			#region 
			
			if (sfd.FilterIndex == 1) 
			{
				StreamWriter sw;

				try
				{
					sw = File.CreateText(sfd.FileName);
				}
				catch
				{
					MessageBox.Show(@"Ошибка при создании файла!" + "\nCODE 0x1240", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				try
				{
					DateTime dateTemp = _date[_createFileNum];
					sw.WriteLine(dateTemp.ToString("dd'/'MM'/'yyyy HH:mm:ss.fff000"));                  //Штамп времени
					sw.WriteLine(Convert.ToString(ScopeConfig.SampleRate / ScopeConfig.FreqCount));     //Частота выборки (частота запуска осциллогрофа/ делитель)
					sw.WriteLine(ScopeConfig.OscilHistCount);                                           //Предыстория 
					sw.WriteLine(FileHeaderLine());                                                     //Формирование заголовка (подписи названия каналов)
                    sw.WriteLine(FileHeaderLineColor());          

                    List<ushort[]> lu = InitParamsLines();                                              //Формирование строк всех загруженных данных (отсортированых с предысторией)
					for (int i = 0; i < lu.Count; i++)
					{
						sw.WriteLine(FileParamLine(lu[i], i));
					}
				}
				catch
				{
					MessageBox.Show(@"Ошибка при записи в файл!" + "\nCODE 0x1241", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				sw.Close();
			}
			#endregion

			// Save to COMETRADE
			#region
			if (sfd.FilterIndex != 1) 
			{
				StreamWriter sw = new StreamWriter(sfd.FileName, false);   //, Encoding.GetEncoding("Windows-1251")

				string namefile;
				string pathfile;
				try
				 {
					 namefile = Path.GetFileNameWithoutExtension(sfd.FileName);
					 pathfile = Path.GetDirectoryName(sfd.FileName);
				 }
				 catch
				 {
					 MessageBox.Show(@"Ошибка при создании файла!" + "\nCODE 0x1240", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					 return;
				 }

				 try
				 {
					 sw.WriteLine(Line1(sfd.FilterIndex));
					 sw.WriteLine(Line2());

					 for (int i = 0, j = 0; i < ScopeConfig.ChannelCount; i++) 
					 {
						 if (ScopeConfig.ChannelType[i] == 0) { sw.WriteLine(Line3(i, j + 1)); j++; }
					 }
					 for (int i = 0, j = 0; i < ScopeConfig.ChannelCount; i++)
					 {
						 if (ScopeConfig.ChannelType[i] == 1) { sw.WriteLine(Line4(i, j + 1)); j++; }
					 }

					 sw.WriteLine(Line5());
					 sw.WriteLine(Line6());
					 sw.WriteLine(Line7());
					 sw.WriteLine(Line8(_createFileNum));
					 sw.WriteLine(Line9(_createFileNum));
					 sw.WriteLine(Line10());
					 sw.WriteLine(Line11());
					 if (sfd.FilterIndex == 3)
					 {
						 sw.WriteLine(Line12());
						 sw.WriteLine(Line13());
					 }
				 }
				 catch
				 {
					 MessageBox.Show(@"Ошибка при записи в файл!" + "\nCODE 0x1241", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					 return;
				 }
				 sw.Close();

				 string pathDateFile = pathfile + "\\" + namefile + @".dat";
				 try
				 {
					 sw = File.CreateText(pathDateFile);
				 }
				 catch
				 {
					 MessageBox.Show(@"Ошибка при создании файла!" + "\nCODE 0x1240", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					 return;
				 }
				 try
				 {
					 List<ushort[]> lud = InitParamsLines();
					 for (int i = 0; i < lud.Count; i++)
					 {
						 sw.WriteLine(FileParamLineData(lud[i], i));
					 }
				 }
				 catch
				 {
					 MessageBox.Show(@"Ошибка при записи в файл!" + "\nCODE 0x1241", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					 return;
				 }
				 sw.Close();
			}
			#endregion

			_loadOscNum = 0;

			MessageBox.Show(@"Осциллограмма загружена!", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			_statusLoad = false;

			if (ScopeSysType.HasViewer)
			{
				DialogResult dialogResult = MessageBox.Show(@"Открыть осциллограмму?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					ExecuteScopeView(sfd.FileName);
				}
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				SaveWindowSize("prgSettings.xml");
				DelateThread();
			}
			catch 
			{
				//ignored
			}
		}

		private void DelateThread()
		{
			//_updateThread.Abort();
			if(SerialPort.IsOpen && !SerialPort.PortError)
				SerialPort.Close();
		}

		//Запуск приложения для просмотра осциллограмм
		private void ExecuteScopeView(string fileName)
		{
			try
			{
				Process proc = new Process
				{
					StartInfo =
					{
						FileName = CalcApplPath() + "ScopeViewer.exe",
						Arguments = "\"" + fileName + "\""
					}
				};
				proc.Start();
			}
			catch
			{
				MessageBox.Show(@"Программа для просмотра осциллограмм не найдена!" + "\nCODE 0x1300", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void toolStripButton2_Click_1(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog
			{
				DefaultExt = @".txt",
				Filter = @"Текстовый файл (.txt)|*.txt"
			};
			if (ofd.ShowDialog() == DialogResult.OK)
			{
			   ExecuteScopeView(ofd.FileName);
			}
		}

		private int _createFileNum;

        private void stopDownloadStripButton_Click(object sender, EventArgs e)
        {
            SetScopeStatus(_loadOscNum);
        }

        //Ручной запуск осциллографа
        private void manStartBtn_Click(object sender, EventArgs e)
		{
			ManStartRequest();
		}

		private void UpdateGrid()
		{
			if (SerialPort.IsOpen && ScopeConfig.ScopeCount != 0)
			{
				if (_buttonsStatus != 0x00)
				{
					if (Math.Abs(tableLayoutPanel.ColumnStyles[0].Width - 80) > 0.5)
					{
						tableLayoutPanel.ColumnStyles[0].Width = 85;
						tableLayoutPanel.ColumnStyles[1].Width = 15;
					}
				}
				else
				{
					tableLayoutPanel.ColumnStyles[0].Width = 0;
					tableLayoutPanel.ColumnStyles[1].Width = 100;
				}
			}
			else
			{
				if (_buttonsStatus != 0x00)
				{
					if (Math.Abs(tableLayoutPanel.ColumnStyles[0].Width - 100) > 0.5)
					{
						tableLayoutPanel.ColumnStyles[0].Width = 100;
						tableLayoutPanel.ColumnStyles[1].Width = 0;
					}
				}
				else
				{
					tableLayoutPanel.ColumnStyles[0].Width = 0;
					tableLayoutPanel.ColumnStyles[1].Width = 100;
				}
			}
		}
	}
}
