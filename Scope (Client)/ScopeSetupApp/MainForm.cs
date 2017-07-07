using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ModBusLibrary;
using ADSPLibrary;
using System.Xml;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ScopeSetupApp.Format;

namespace ScopeSetupApp
{
	[SuppressMessage("ReSharper", "LocalizableElement")]
	public partial class MainForm : Form
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

		private readonly ModBusUnit _modBusUnit;
		private bool _buttonsAlreadyCreated = true;
		private bool _configLoaded;
		private bool _lineBusy;
		private int _loadConfigStep;
		private int _requestStep;
		private int _loadTimeStampStep;

		private void LoadWindowSize(string comPortXmlName, out int newHeight, out int newWidth, out int newWinState)
		{
			var doc = new XmlDocument();
			try
			{
				doc.Load(comPortXmlName);
			}
			catch
			{
				MessageBox.Show(@"Файл с настройками не найден!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			  //  Application.Exit();
			}

			var xmls = doc.GetElementsByTagName(@"MainWindow");

			if (xmls.Count != 1)
			{
				//MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//Application.Exit();
			}
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
				//MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		public MainForm(string[] agrs)
		{
			_argsG = agrs;

			InitializeComponent();

			InitializeFormat();
			//FormatStrLabel();

			InitializeConfig();



			tableLayoutPanel.ColumnStyles[0].Width = 80;
			tableLayoutPanel.ColumnStyles[1].Width = 20;



			if (agrs.Length > 0)
			{
				if (agrs[0] == "a" || agrs[0] == "A")
				{
					ConfigScopeButton.Visible = true;
					Setting_Button.Visible = true;
				} 
			}

			_modBusUnit = new ModBusUnit();
			_modBusUnit.RequestFinished += EndRequest;

			ModBusUnits.ScopeSetupModbusUnit = new ModBusUnit();
			ModBusClient.InitModBusEvent();

			int height, width, winState;
			LoadWindowSize("prgSettings.xml", out height, out width, out winState);
			
			Size size = new Size(width, height);
			Size = size;

			WindowState = winState == 1 ? FormWindowState.Maximized : FormWindowState.Normal;
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
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public delegate void FormatLabel();
		public static readonly FormatLabel FormatStatusLabel = FormatStrLabel;

		private static void FormatStrLabel()
		{
			format_toolStripStatusLabel.Text = FormatConverter.OldFormat ? @"Формат данных: OLD" : @"Формат данных: NEW";
		}


		private delegate void SetStringDelegate(string parameter);

		private void SetTimeLabel(string statusConnect)
		{
			try
			{
				connect_toolStripStatusLabel.Text = statusConnect;
			}
			catch
			{
				// ignored
			}
		}


		//************************** ВЫЗОВЫ ДОЧЕРНИХ ОКОН ***************************************//
		//***************************************************************************************//
		//***************************************************************************************//

		private ScopeSetupForm _scopeSetupForm;

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (_scopeSetupForm != null)
			{
				try
				{
					_scopeSetupForm.Show();
					_scopeSetupForm.Activate();
				}
				catch (Exception)
				{
					_scopeSetupForm = new ScopeSetupForm(_argsG)
					{
						Size = Size,
						WindowState = WindowState
					};
					_scopeSetupForm.Show();
				}
			}
			else
			{
				_scopeSetupForm = new ScopeSetupForm(_argsG)
				{
					Size = Size,
					WindowState = WindowState
				};
				_scopeSetupForm.Show();
			}  
		}

		private ScopeConfigForm _scopeConfigForm;

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			if (_scopeConfigForm != null)
			{
				try
				{
					_scopeConfigForm.Show();
					_scopeConfigForm.Activate();
				}
				catch (Exception)
				{
					_scopeConfigForm = new ScopeConfigForm()
					{
						Size = Size,
						WindowState = WindowState
					};
					_scopeConfigForm.Show();
				}
			}
			else
			{
				_scopeConfigForm = new ScopeConfigForm()
				{
					Size = Size,
					WindowState = WindowState
				};
				_scopeConfigForm.Show();
			}


			_buttonsStatus = (byte) (_buttonsStatus == 0x00 ? 0x01 : 0x00);
			UpdateButtons();
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
			}
			else
			{
				toolStrip1.Size = new Size(74, 444);

				UpdateButtonsSets(connectBtn);
				UpdateButtonsSets(OpenScope_Button);

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
			}
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

		private void LoadComPortSettings(string comPortXmlName, out int newPar, out int newSpeed, out int newPortIndex, out int newAddr)
		{
			var doc = new XmlDocument();
			try { doc.Load(comPortXmlName); }
			catch
			{
				//MessageBox.Show(@"Не удалось открыть файл с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//Application.Exit();
			}

			var xmls = doc.GetElementsByTagName("ComPort");

			if (xmls.Count != 1)
			{
				//MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//Application.Exit();
			}
			var xmlNode = xmls[0];
			try
			{
				// ReSharper disable once PossibleNullReferenceException
				newPortIndex = Convert.ToInt32(xmlNode.Attributes["Name"].Value);
				newSpeed = Convert.ToInt32(xmlNode.Attributes["Speed"].Value);
				newPar = Convert.ToInt32(xmlNode.Attributes["Parity"].Value);
				newAddr = Convert.ToInt32(xmlNode.Attributes["Addr"].Value) - 1;
			}
			catch
			{
				newPortIndex = 1;
				newSpeed = 0;
				newPar = 0;
				newAddr = 1;
				//MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//Application.Exit();
			}
		}

		private void OpenPort()
		{
			if (ModBusClient.PortList.Count == 0)
			{
				MessageBox.Show(@"Нет ни одного доступного COM-порта!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int par; int portIndex; int sp; int addr;
			LoadComPortSettings(@"PrgSettings.xml", out par, out sp, out portIndex, out addr);

			if (ModBusClient.ModBusOpened)
			{
				ConnectForm connectForm = new ConnectForm(ModBusClient.ModBusOpened, ModBusClient.CurrentAddr, portIndex, ModBusClient.CurrentPortParity, ModBusClient.CurrentPortSpeed);
				connectForm.ShowDialog();
			}
			else
			{
				ConnectForm connectForm = new ConnectForm(false, addr, portIndex, par, sp);
				connectForm.ShowDialog();
			}
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			OpenPort();
		}


		//*****************************************************************************************//
		//*****************************************************************************************//
		//*****************************************************************************************//
		private readonly ushort[] _oscilsStatus = { 0, 0, 0, 0,   0, 0, 0, 0,  0, 0, 0, 0,     0, 0, 0, 0, 
								  0, 0, 0, 0,   0, 0, 0, 0,  0, 0, 0, 0,     0, 0, 0, 0,  0};

		private readonly string[] _oscilTitls = new string[32];
		private readonly DateTime [] _date = new DateTime[32];

		private void SendTimeStampRequest()
		{
			int i  = _loadTimeStampStep;

			if (i >= ScopeConfig.ScopeCount)
			{
				_requestStep = 0;
				_loadTimeStampStep = 0;
				_lineBusy = false;
				return;
			}

			while ((_oscilsStatus[i] < 4) && (i < ScopeConfig.ScopeCount))
			{
				i++;
			}

			if (i >= ScopeConfig.ScopeCount)
			{
				_requestStep = 0;
				_loadTimeStampStep = 0;
				_lineBusy = false;
			}
			else if (_requestStep == 3)
			{
				_loadTimeStampStep = i;
			   
				_modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 136 + i * 6), 6);
			}
		}
		private void EndTimeStampRequest()
		{
			if (!_modBusUnit.modBusData.RequestError && _requestStep == 3)
			{
				UpdateTimeStampInvoke();
				_loadTimeStampStep++;
			}
			else
			{
				MessageBox.Show(@"TimeStamp error");
			}			
		}

		int _indexChannel;

		private void LoadConfig()
		{
			if (_loadConfigStep == 0)                //Количество каналов 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 67), 1);
				return;
			}

			if (_loadConfigStep == 1)                //Количество осциллограмм 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 66), 1);
				return;
			}

			if (_loadConfigStep == 2)                //Предыстория 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 68), 1);
				return;
			}

			if (_loadConfigStep == 3)                //Делитель
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 69), 1);
				return;
			}

			if (_loadConfigStep == 4)                //Режим работы
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 70), 1);
				return;
			}

			if (_loadConfigStep == 5)                //Размер осциллограммы 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 64), 2);
				return;
			}

			if (_loadConfigStep == 6)                //Частота выборки
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 2), 1);
				return;
			}

			if (_loadConfigStep == 7)                //Весь размер под осциллограммы 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 376), 2);
				return;
			}

			if (_loadConfigStep == 8)                //Размер одной выборки
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 3), 1);
				return;
			}
			if (_loadConfigStep == 9)                //Количество выборок на предысторию 
			{
				_modBusUnit.GetData(ScopeSysType.OscilCmndAddr, 2);
				return;
			}

			if (_loadConfigStep == 10)                //Статус осциллогрофа
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 378), 2);
				return;
			}

			if (_loadConfigStep == 11)                //Адреса каналов 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 32), 32);
				return;
			}

			if (_loadConfigStep == 12)                //Формат каналов 
			{
				_modBusUnit.GetData((ScopeSysType.ConfigurationAddr), 32);
				return;
			}

			if (_loadConfigStep == 13)                //Название каналов 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 71 + 16*_indexChannel), 16);
				return;
			}

			if (_loadConfigStep == 14)                //Фаза каналов 
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 583 + _indexChannel), 1);
				return;
			}

			if (_loadConfigStep == 15)                //CCBM каналов
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 615 + 8 *_indexChannel), 8);
				return;
			}

			if (_loadConfigStep == 16)                //Измеерение каналов
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 871 + 4 * _indexChannel), 4);
				return;
			}

			if (_loadConfigStep == 17)                //Type каналов
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 999 + _indexChannel), 1);
				return;
			}

			if (_loadConfigStep == 18)                //
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 1031), 16);
				return;
			}

			if (_loadConfigStep == 19)                //
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 1047), 8);
				return;
			}

			if (_loadConfigStep == 20)                //
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 1055), 4);
				return;
			}

			if (_loadConfigStep == 21)                //
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 1059), 4);
				return;
			}

			if (_loadConfigStep == 22)                //
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 1063), 4);
				return;
			}

			if (_loadConfigStep == 23)                //
			{
				_modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr + 1067), 4);
			}
		}
		private void EndLoadConfig()
		{
			if (!_modBusUnit.modBusData.RequestError)
			{
				switch (_loadConfigStep)
				{
					case 0:                     //Количество каналов 
						{
							ScopeConfig.ChannelCount = _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 1;
							LoadConfig();
						} break;

					case 1:                     //Количество осциллограмм
						{
							ScopeConfig.ScopeCount = _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 2;
							LoadConfig();
						} break;

					case 2:                     //Предыстория 
						{
							ScopeConfig.HistoryCount = _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 3;
							LoadConfig();
						} break;

					case 3:                     //Делитель
						{
							ScopeConfig.FreqCount = _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 4;
							LoadConfig();
						} break;

					case 4:                     //Режим работы
						{
							ScopeConfig.OscilEnable = _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 5;
							LoadConfig();
						} break;

					case 5:                     //Размер осциллограммы 
						{
							ScopeConfig.OscilSize = (uint)(_modBusUnit.modBusData.ReadData[1] << 16);
							ScopeConfig.OscilSize += _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 6;
							LoadConfig();
						} break;

					case 6:                     //Частота выборки
						{
							ScopeConfig.SampleRate = _modBusUnit.modBusData.ReadData[0];
							ScopeConfig.ScopeEnabled = true;
							_loadConfigStep = 7;
							LoadConfig();
						} break;

					case 7:                     //Размер осциллограммы 
						{
							ScopeConfig.OscilAllSize = (uint)(_modBusUnit.modBusData.ReadData[1] << 16);
							ScopeConfig.OscilAllSize += (_modBusUnit.modBusData.ReadData[0]);
							_loadConfigStep = 8;
							LoadConfig();
						} break;

					case 8:                     //Размер одной выборки
						{
							ScopeConfig.SampleSize = _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 9;
							LoadConfig();
						} break;
					case 9:                     //Размер всей памяти 
						{
							ScopeConfig.OscilHistCount = (uint)(_modBusUnit.modBusData.ReadData[1] << 16);
							ScopeConfig.OscilHistCount += _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 10;
							LoadConfig();

						} break;
					case 10:                     //Статус осциллогрофа
						{
							ScopeConfig.StatusOscil = _modBusUnit.modBusData.ReadData[0];
							_loadConfigStep = 11;
							LoadConfig();
						} break;
					case 11:                     //Адреса каналов 
						{
							ScopeConfig.InitOscilAddr(_modBusUnit.modBusData.ReadData);
							_loadConfigStep = 12;
							LoadConfig();
						} break;
					case 12:                     //Формат каналов 
						{
							ScopeConfig.InitOscilFormat(_modBusUnit.modBusData.ReadData);
							ScopeConfig.InitOscilParams(ScopeConfig.OscilAddr,ScopeConfig.OscilFormat);
							_loadConfigStep = 13;
							LoadConfig();
						} break;
					case 13:                     //Названия каналов 
						{
							if (ScopeConfig.ChannelCount == 0)   //Если в системе нет конфигурации
							{
								_loadConfigStep = 0;
								_configLoaded = true;
								_buttonsAlreadyCreated = false;
								break;
							}
							if(_indexChannel == 0) ScopeConfig.ChannelName.Clear();
							ScopeConfig.InitChannelName(_modBusUnit.modBusData.ReadData);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 14;
							}
							else
							{
								_indexChannel++;
								LoadConfig();
							}
						} break;
					case 14:                     //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelPhase.Clear();
							ScopeConfig.InitChannelPhase(_modBusUnit.modBusData.ReadData);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 15;
							}
							else
							{
								_indexChannel++;
								LoadConfig();
							}
						} break;

					case 15:                     //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelCcbm.Clear();
							ScopeConfig.InitChannelCcbm(_modBusUnit.modBusData.ReadData);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 16;
							}
							else
							{
								_indexChannel++;
								LoadConfig();
							}
						} break;

					case 16:                     //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelDemension.Clear();
							ScopeConfig.InitChannelDemension(_modBusUnit.modBusData.ReadData);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 17;
							}
							else
							{
								_indexChannel++;
								LoadConfig();
							}
						} break;

					case 17:                     //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelType.Clear();
							ScopeConfig.InitChannelType(_modBusUnit.modBusData.ReadData);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 18;
							}
							else
							{
								_indexChannel++;
								LoadConfig();
							}
						} break;

					case 18:                     //
						{
							ScopeConfig.InitStationName(_modBusUnit.modBusData.ReadData);
							_loadConfigStep = 19;
							LoadConfig();
						} break;

					case 19:                     //Названия каналов 
						{
							ScopeConfig.InitRecordingId(_modBusUnit.modBusData.ReadData);
							_loadConfigStep = 20;
							LoadConfig();

						} break;

					case 20:                     //Названия каналов 
						{
							ScopeConfig.InitTimeCode(_modBusUnit.modBusData.ReadData);
							_loadConfigStep = 21;
							LoadConfig();
						} break;

					case 21:                     //Названия каналов 
						{
							ScopeConfig.InitLocalCode(_modBusUnit.modBusData.ReadData);

							_loadConfigStep = 22;
							LoadConfig();
						} break;

					case 22:                     //Названия каналов 
						{
							ScopeConfig.InitTmqCode(_modBusUnit.modBusData.ReadData);

							_loadConfigStep = 23;
							LoadConfig();
						} break;

				   case 23:                     //Названия каналов 
						{
							ScopeConfig.InitLeapsec(_modBusUnit.modBusData.ReadData);

							_loadConfigStep = 0;
							_configLoaded = true;
							_buttonsAlreadyCreated = false;
						} break;
				}
			}
			else
			{
				_loadConfigStep = 0;
			}
		}


		private void EndLoadTime()
		{
			if (!_modBusUnit.modBusData.RequestError)
			{
				try
				{
					Invoke(new SetStringDelegate(SetTimeLabel), @"CONNECT");
				}
				catch
				{
					// ignored
				}
			}
			else
			{
				try
				{
					Invoke(new SetStringDelegate(SetTimeLabel), @"NO CONNECT");
				}
				catch
				{
					// ignored
				}
			}
		}


		private void EndLoadStatus(int blockNum)
		{
			int i;
			if (!_modBusUnit.modBusData.RequestError)
			{
				for (i = 0; i < 16; i++)
				{
					_oscilsStatus[i+16*blockNum] = _modBusUnit.modBusData.ReadData[i];
				}
			}
			else
			{
				for (i = 0; i < 16; i++)
				{
					_oscilsStatus[i] = 0;
				}
			}

			UpdateOscilsStatusInvoke();
		}

		//Очистка осциллограмм
		private int _clearOscNum = 0x7FFF;
		private bool _clearOscFlag;
		private bool _initClearOscFlag;
		private void ClearOscRequest()
		{
			if (_clearOscNum >= ScopeConfig.ScopeCount)
			{
				MessageBox.Show(@"Error");
				_clearOscFlag = false;
				return;
			}

			ushort u = (ushort)(ScopeSysType.OscilCmndAddr + 8 + _clearOscNum);
			ushort[] uv = {0,0,0,0};
			_modBusUnit.SetData(u,1,uv);
		}


		private void ClearOscResponce()
		{
			_clearOscFlag = false;
		}

		//Ручной запуск
		private bool _manStartFlag;
		private bool _initManStartFlag;
		private void ManStartRequest()
		{
			ushort u = (ushort)(ScopeSysType.OscilCmndAddr + 4);//
			//MessageBox.Show(u.ToString("X4"));
			ushort[] uv = { 1, 1, 1, 1 };
			_modBusUnit.SetData(u, 1, uv);
		}

		private void ManStartResponce()
		{
			_manStartFlag = false;
		}

		private void EndRequest(object sender, EventArgs e)
		{
			if (_modBusUnit.modBusData.RequestError)
			{
				ScopeConfig.ConnectMcu = false;
				ScopeConfig.ChangeScopeConfig = false;
				RemoveButtonsInvoke();
				HideProgressBarInvoke();
				_loadOscDataStep = 0;            
				_loadOscilIndex = 0;
				_requestStep = 0;
				_clearOscFlag = false;
				_loadOscData = false;
				_lineBusy = false;
				EndLoadTime();
				return;
			}

			ScopeConfig.ConnectMcu = true;

			if (_loadOscData)
			{
				LoadOscDataResponce();
				_lineBusy = false;
				return;
			}

			if (!_configLoaded)
			{
				EndLoadConfig();
			}
			else if (_clearOscFlag)
			{
				ClearOscResponce();
			}
			else if (_manStartFlag)
			{
				ManStartResponce();
			}
			else if (_requestStep == 0)
			{
				EndLoadTime();
				_requestStep = 1;
			}
			else if (_requestStep == 1)
			{
				EndLoadStatus(0);
				_requestStep = 2;
			}
			else if (_requestStep == 2)
			{
				EndLoadStatus(1);
				_requestStep = 3;
			}

			else if (_requestStep == 3 || _requestStep == 4)
			{
				EndTimeStampRequest();
			}

			if (ScopeConfig.ChangeScopeConfig)
			{
				ScopeConfig.ChangeScopeConfig = false;
				RemoveButtonsInvoke();
			}

			if (_initLoadOscilFlag) { InitLoadOscillInvoke(); _initLoadOscilFlag = false; _requestStep = 0;  _lineBusy = false; return; }
			if (_initClearOscFlag) { _clearOscFlag = true; _initClearOscFlag = false; }
			if (_initManStartFlag) { _manStartFlag = true; _initManStartFlag = false; }
			_lineBusy = false;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Enabled = false;
			CreateStatusButtons();
			if (!ModBusClient.ModBusOpened) { timer1.Enabled = true; return; }

			if (_loadOscData)
			{
				timer1.Enabled = true;
				return;
			}
			if (_lineBusy)       { timer1.Enabled = true; return; }

			_lineBusy = true;

			if (!_configLoaded)
			{
				LoadConfig();
				timer1.Enabled = true;
				return;
			}

			if (_clearOscFlag)
			{
				ClearOscRequest();
				_requestStep = 0;
				timer1.Enabled = true;
				return;
			}

			if (_manStartFlag)
			{
				ManStartRequest();
				_requestStep = 0;
				timer1.Enabled = true;
				return;
			}

			if (_requestStep==0)
			{
				_modBusUnit.GetData(0x202, 8);
			}
			else if (_requestStep == 1)
			{ _modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 8), 16); }

			else if (_requestStep == 2)
			{ _modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 24), 16); }

			else if (_requestStep == 3)
			{
				 SendTimeStampRequest(); 
			}

			else if (_requestStep == 4)
			{
				SendTimeStampRequest();
			}

			timer1.Enabled = true;
		}
		private void MainForm_Load(object sender, EventArgs e)
		{

		}


		//**************** ДИНАМИЧЕСКОЕ СОЗДАНИЕ КОНТРОЛОВ***************************************//
		//***************************************************************************************//
		//***************************************************************************************//
		private void CreateStatusButtons()
		{
			if (_buttonsAlreadyCreated) { return; }
			int i;
			_statusButtons = new List<Button>();

			Font font = new Font(@"Open Sans", 9);
			Size size = new Size(120, 60);

			for (i = 0; i < ScopeConfig.ScopeCount; i++)
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
				nowStatusFlowLayoutPanel.Controls.Add(_statusButtons[i]);
			}
			_buttonsAlreadyCreated = true;

		}



		//*************** ОБНОВЛЕНИЕ КОНТРОЛОВ В АСИНХРОННОМ РЕЖИМЕ ***************************************//
		//*************************************************************************************************//
		//*************************************************************************************************//
		private delegate void NoParamDelegate();

		private void UpdateOscilsStatus()
		{
			int i;
			
			for (i = 0; i < ScopeConfig.ScopeCount; i++)
			{
				if (_oscilsStatus[i] == 0) 
				{
					_statusButtons[i].FlatStyle = FlatStyle.Standard;
					_statusButtons[i].BackColor = Color.White;
					_statusButtons[i].Enabled = false;
					_oscilTitls[i] = @"Осциллограмма №" + (i + 1)+ @".";
					_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Пусто";
				
				}
				else if (_oscilsStatus[i] >= 4) 
				{ 
					_statusButtons[i].BackColor = Color.LightSteelBlue; 
					_statusButtons[i].Enabled = true; 
				}
			
				else if (_oscilsStatus[i] == 3)
				{
					_statusButtons[i].FlatStyle = FlatStyle.Standard;
					_statusButtons[i].BackColor = Color.Lavender; 
					_statusButtons[i].Enabled = true;
					_oscilTitls[i] = @"Осциллограмма №" + (i + 1) + @".";
					_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Идет запись";         
				}
				else if (_oscilsStatus[i] == 1)
				{
					_statusButtons[i].BackColor = Color.GhostWhite;
					_statusButtons[i].Enabled = true;
					_oscilTitls[i] = @"Осциллограмма №" + (i + 1) + @".";
					_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Запись предыстории";
				}
				else {
					_statusButtons[i].BackColor = Color.AliceBlue;
					_statusButtons[i].Enabled = true;
					_oscilTitls[i] = @"Осциллограмма №" + (i + 1) + @".";
					_statusButtons[i].Text = @"№" + (i + 1) + "\n" + @"Готова к записи";
				}
			}
		}
		private void UpdateOscilsStatusInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateOscilsStatus), null);
			}
			catch
			{
				// ignored
			}
		}

		private void UpdateTimeStamp()
		{
			string str1 = (_modBusUnit.modBusData.ReadData[0] & 0x3F).ToString("X2") + "/" + ((_modBusUnit.modBusData.ReadData[0] >> 8) & 0x1F).ToString("X2") + @"/20" + (_modBusUnit.modBusData.ReadData[1] & 0xFF).ToString("X2");
			string str2 = (_modBusUnit.modBusData.ReadData[3] & 0x3F).ToString("X2") + ":" + ((_modBusUnit.modBusData.ReadData[2] >> 8) & 0x7F).ToString("X2") + @":" + (_modBusUnit.modBusData.ReadData[2] & 0x7F).ToString("X2");
			string str3 = ((_modBusUnit.modBusData.ReadData[4] *1000) >> 8).ToString("D3") + @"000";
			_statusButtons[_loadTimeStampStep].Text = @"№" + (_loadTimeStampStep + 1) + "\n" + str1 + "\n" + str2;
			_oscilTitls[_loadTimeStampStep] = @"Осциллограмма №" + (_loadTimeStampStep + 1) + "\n" + str1 + "\n" + str2;
			string str = str1 + "," + str2 + @"." + str3;
			try
			{
				_date[_loadTimeStampStep] = DateTime.Parse(str);
			}
			catch
			{
				// ignored
			}
		}
		private void UpdateTimeStampInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateTimeStamp), null);
			}
			catch
			{
				// ignored
			}
		}

		private void UpdateLoadDataProgressBar()
		{
			loadDataProgressBar.Value = (int)_loadOscilIndex;
		}
		private void UpdateLoadDataProgressBarInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateLoadDataProgressBar), null);
			}
			catch
			{
				// ignored
			}
		}

		private void HideProgressBar()
		{
			loadDataProgressBar.Visible = false;
			loadScopeToolStripLabel.Visible = false;
		}
		private void HideProgressBarInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(HideProgressBar), null);
			}
			catch
			{
				// ignored
			}
		}

		private void RemoveButtons()
		{
			try
			{
				for (int i = 0; i < ScopeConfig.ScopeCount; i++)
				{
					nowStatusFlowLayoutPanel.Controls.Remove(_statusButtons[i]);
				}
				_statusButtons.Clear();
			}
			catch
			{
				// ignored
			}

			_configLoaded = false;
		}
		private void RemoveButtonsInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(RemoveButtons), null);
			}
			catch
			{
				// ignored
			}
		}


		//*************************** СКАЧИВАНИЕ ОСЦИЛЛОГРАММ В ФАЙЛ ************************************//
		//***********************************************************************************************//
		//***********************************************************************************************//
		#region

		private void LoadOscBtnClick(object sender, EventArgs e)
		{
			ScopeConfig.InitOscilParams(ScopeConfig.OscilAddr, ScopeConfig.OscilFormat);
			bool b = _oscilsStatus[(int)((Button) sender).Tag] >= 4;

			LoadOscQueryForm loadOscQueruForm = new LoadOscQueryForm(_oscilTitls[(int)((Button) sender).Tag], b);
			DialogResult dlgr = loadOscQueruForm.ShowDialog();

			//СКАЧИВАНИЕ ОСЦИЛЛОГРАММ
			if (dlgr == DialogResult.OK)
			{
				_loadOscNum = (int)((Button)sender).Tag;
				_initLoadOscilFlag = true;
				_oscilStartTemp = ((uint)_loadOscNum * (ScopeConfig.OscilSize >> 1)); //Начало осциллограммы 
				_oscilEndTemp = (((uint)_loadOscNum + 1) * (ScopeConfig.OscilSize >> 1)); //Конец осциллограммы 
			}

			//СБРОС ОСЦИЛЛОГРАММ
			else if (dlgr == DialogResult.Abort)
			{
				_clearOscNum = (int)((Button) sender).Tag;
				_initClearOscFlag = true;
			}
		}

		//Загрузка осциллограмм
		private List<ushort[]> _downloadedData = new List<ushort[]>();

		private void LoadOscDataRequest()
		{
			switch (_loadOscDataStep)
			{
				//Загрузка номера выборки на котором заканчивается осциллограмма 
				case 0:
					{
						_modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 72 + _loadOscNum * 2), 2);
					}   break;

				//Загрузка данных
				case 1:
				{
					{
							uint oscilLoadTemp = (CalcOscilLoadTemp()) >> 5;
						_modBusUnit.GetData04((ushort)(oscilLoadTemp), 32);
					}

					} break;
			} 
		}

		private void LoadOscDataResponce()
		{
			switch (_loadOscDataStep)
			{
				//Загрузка стартового адреса
				case 0:
					{
						if (!_modBusUnit.modBusData.RequestError)
						{
							_startLoadSample = (uint)(_modBusUnit.modBusData.ReadData[1] << 16);
							_startLoadSample += _modBusUnit.modBusData.ReadData[0];
							_loadOscilIndex = 0;
							_loadOscDataStep = 1;
						}
						else
						{
							_loadOscData = false;
							_loadOscDataStep = 0;
							_loadOscNum = 0;
							_countTemp = 0;
						}
					} break;

				case 1:
					{
						if (!_modBusUnit.modBusData.RequestError)
						{
										for (int i = 0; i < 32; i++)
										{
											_loadParamPart[i] = _modBusUnit.modBusData.ReadData[i];
										}
							{
								_downloadedData.Add(new ushort[32]);
								for (int i = 0; i < 32; i++)
								{
									_downloadedData[_downloadedData.Count - 1][i] = _loadParamPart[i];
								}

								_loadOscilIndex = (2*_countTemp*1000)/ScopeConfig.OscilSize;
								
								UpdateLoadDataProgressBarInvoke();

								if (_countTemp >= (ScopeConfig.OscilSize >> 1))
								{

									_loadOscilIndex = 0;
									_createFileNum = _loadOscNum;
									_createFileFlag = true;
									_loadOscData = false;
									_loadOscDataStep = 0;
									UpdateLoadDataProgressBarInvoke();
									_loadOscilTemp = 0;
									_countTemp = 0;
								}
							}
						}
						else
						{
							_loadOscData = false;
							_loadOscDataStep = 0;
							_countTemp = 0;
						}

					} break;
			}
			if (!_loadOscData) 
			{
				HideProgressBarInvoke();
				return; 
			}
			if (!_modBusUnit.modBusData.RequestError) { LoadOscDataRequest(); }


		}

		private bool    _loadOscData;        //Флаг, что идет скачивание осцилограммы, все остальные запросы приостановлены
		private int     _loadOscDataStep;            //0 - загрузка loadOscilTemp
													//1 - загрузка непосредственно тела


		private readonly ushort[] _loadParamPart = new ushort[32];

		private int _loadOscNum;
		private uint _loadOscilIndex;
		private uint _loadOscilTemp;
		private uint _oscilStartTemp;
		// ReSharper disable once NotAccessedField.Local
		private uint _oscilEndTemp;
		private uint _countTemp;
		private uint _startLoadSample;

		private uint CalcOscilLoadTemp()
		{
			if (_countTemp < (ScopeConfig.OscilSize >> 1))                               //Проход по осциллограмме 
			{
				_loadOscilTemp += 32;                                                    //Какую часть осциллограммы грузим 
				_countTemp += 32;
			}
			return (_loadOscilTemp - 32 + _oscilStartTemp);                               //+Положение относительно начала массива
		}
		#endregion

		//СОЗДАНИЕ ФАЙЛА
		// Save to .txt
		#region

		private string FileHeaderLine()
		{
			string str = " " + "\t";
			for (int i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				str = str + ScopeConfig.ChannelName[i] + "\t";
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
				str = str + AdvanceConvert.HexToFormat(ulTemp, (byte)ScopeConfig.OscilFormat[i]) + "\t";
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
					str1 = AdvanceConvert.HexToFormat(ulTemp, (byte)ScopeConfig.OscilFormat[i]);
					str1 = str1.Replace(",", ".");
					str = str + "," + str1;
				}
			}
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{

				if (ScopeConfig.ChannelType[i] == 1)
				{
					ulTemp = ParseArr(i, paramLine);
					str1 = AdvanceConvert.HexToFormat(ulTemp, (byte)ScopeConfig.OscilFormat[i]);
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
			string chId = ScopeConfig.ChannelName[num];
			string ph = ScopeConfig.ChannelPhase[num];
			string ccbm = ScopeConfig.ChannelCcbm[num];
			string uu = ScopeConfig.ChannelDemension[num];
			string a = "1";
			string b = "0";
			int skew = 0;
			int min;
			int max;
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
				str = "60";
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

		private void CreateFile()
		{
			SaveFileDialog sfd = new SaveFileDialog
			{
				DefaultExt = @".txt",
				Filter = @"Text Files (*.txt)|*.txt|COMTRADE rev. 1999 (*.cfg)|*.cfg|COMTRADE rev. 2013 (*.cfg)|*.cfg"
			};

			if (sfd.ShowDialog() != DialogResult.OK) { return; }

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
					MessageBox.Show(@"Ошибка при создании файла!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				try
				{
					DateTime dateTemp = _date[_createFileNum];
					sw.WriteLine(dateTemp.ToString("dd'/'MM'/'yyyy HH:mm:ss.fff000"));                  //Штамп времени
					sw.WriteLine(Convert.ToString(ScopeConfig.SampleRate / ScopeConfig.FreqCount));     //Частота выборки (частота запуска осциллогрофа/ делитель)
					sw.WriteLine(ScopeConfig.OscilHistCount);                                           //Предыстория 
					sw.WriteLine(FileHeaderLine());                                                     //Формирование заголовка (подписи названия каналов)

					List<ushort[]> lu = InitParamsLines();                                              //Формирование строк всех загруженных данных (отсортированых с предысторией)
					for (int i = 0; i < lu.Count; i++)
					{
						sw.WriteLine(FileParamLine(lu[i], i));
					}
				}
				catch
				{
					MessageBox.Show(@"Ошибка при записи в файл!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				sw.Close();
			}
			#endregion

			// Save to COMETRADE
			#region
			if (sfd.FilterIndex != 1) 
			{
				StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.GetEncoding("Windows-1251"));

				string namefile;
				string pathfile;
				try
				 {
					 namefile = Path.GetFileNameWithoutExtension(sfd.FileName);
					 pathfile = Path.GetDirectoryName(sfd.FileName);
				 }
				 catch
				 {
					 MessageBox.Show(@"Ошибка при создании файла!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					 MessageBox.Show(@"Ошибка при записи в файл!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					 MessageBox.Show(@"Ошибка при создании файла!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					 MessageBox.Show(@"Ошибка при записи в файл!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					 return;
				 }
				 sw.Close();
			}
			#endregion

			_loadOscNum = 0;
			DialogResult dialogResult = MessageBox.Show(@"Открыть осциллограмму?", @"ScopeViewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				ExecuteScopeView(sfd.FileName);
			}           
		}

		private bool _initLoadOscilFlag;
		private void InitLoadOscill()
		{
			_loadOscData = true;
			_loadOscDataStep = 0;
			_initLoadOscilFlag = false;
			_loadOscilIndex = 0;

			loadScopeToolStripLabel.Text = _oscilTitls[_loadOscNum];
			loadDataProgressBar.Value = 0;

			loadDataProgressBar.Visible = true;
			loadScopeToolStripLabel.Visible = true;

			_downloadedData = new List<ushort[]>();
		   // loadOscilCountRound = (UInt16)(ScopeConfig.ScopeCount * 8);
			LoadOscDataRequest();
		}
		private void InitLoadOscillInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(InitLoadOscill), null);
			}
			// ReSharper disable once EmptyGeneralCatchClause
			catch { }      
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveWindowSize("prgSettings.xml");
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
				MessageBox.Show(@"Программа для просмотра осциллограмм не найдена!", @"ScopeViewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private bool _createFileFlag;
		private int _createFileNum;

		private Form1 _formTest;

		private Settings _settings;

		private void Setting_Button_Click(object sender, EventArgs e)
		{
			_settings = new Settings()
			{
				Dock = DockStyle.Fill,
			};
			flowLayoutPanel1.Container?.Add(_settings);
			_settings.Show();


			_formTest = new Form1();
			_formTest.Show();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			timer2.Enabled = false;
			if (_createFileFlag)
			{
				_createFileFlag = false;
				CreateFile();
			}
			timer2.Enabled = true; 
		}

		//Ручной запуск осциллографа
		private void manStartBtn_Click(object sender, EventArgs e)
		{
			_initManStartFlag = true;
		}


	}
}
