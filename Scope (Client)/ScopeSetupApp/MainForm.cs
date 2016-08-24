﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ModBusLibrary;
using ADSPLibrary;
using System.Xml;
using System.Diagnostics;

namespace ScopeSetupApp
{
	public partial class MainForm : Form
	{
		//путь приложения
		string CalcApplPath()
		{
			string str = Application.ExecutablePath;
			string str2 = "";
			int i = str.Length;
			char ch = (char)0;

			do
			{
				ch = str[i - 1];
				if (ch != 0x5C) { i = i - 1; }
			} while ((ch != 0x5C) && (i > 0));

			str2 = str.Substring(0, i);
			return (str2);
		}

		//Статусные кнопки загрузки осциллограмм
		List<Button> statusButtons;
		bool buttonsAlreadyCreated = true;

		ModBusUnit modBusUnit;
		bool configLoaded = false;
		bool lineBusy = false;
		int  loadConfigStep = 0;
		int requestStep = 0;
		int loadTimeStampStep = 0;

		void LoadWindowSize(string comPortXMLName, out int newHeight, out int newWidth)
		{
			XmlNodeList xmls;
			XmlNode xmlNode;

			var doc = new XmlDocument();
			try { doc.Load(comPortXMLName); }
			catch
			{
				MessageBox.Show("Не удалось открыть файл с настройками!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			xmls = doc.GetElementsByTagName("MainWindow");

			if (xmls.Count != 1)
			{
				MessageBox.Show("Ошибки в файле с настройками!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
			xmlNode = xmls[0];
			try
			{
				newHeight = Convert.ToInt32(xmlNode.Attributes["Height"].Value);
				newWidth = Convert.ToInt32(xmlNode.Attributes["Width"].Value);
			}
			catch
			{
				newHeight = 600;
				newWidth = 800;
				MessageBox.Show("Ошибки в файле с настройками!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		void SaveWindowSize(string comPortXMLName)
		{
			try
			{
				var doc = new XmlDocument();
				doc.Load(comPortXMLName);

				XmlNodeList adds = doc.GetElementsByTagName("MainWindow");
				foreach (XmlNode add in adds)
				{
					add.Attributes["Height"].Value = this.Height.ToString();
					add.Attributes["Width"].Value = this.Width.ToString();

				}
				doc.Save(comPortXMLName);
			}
			catch
			{
			}
		}

		public MainForm()
		{
			InitializeComponent();
			try
			{
				ScopeSysType.InitScopeSysType();
			 //   MessageBox.Show(ScopeSysType.ParamLoadDataAddr.ToString("X4"));
			}

			catch(Exception e)
			{
				MessageBox.Show(e.Message.ToString());
			}

		  //  for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
		  //  {
		   //  MessageBox.Show(ScopeSysType.ChannelFormats[i].ToString());

			//}
			modBusUnit = new ModBusUnit();
			modBusUnit.RequestFinished += new EventHandler(EndRequest);

			ModBusUnits.ScopeSetupModbusUnit = new ModBusUnit();
			ModBusClient.InitModBusEvent();

			int i1,i2;
			LoadWindowSize("prgSettings.xml",out i1,out i2);
			Size size = new Size(i2, i1);
			this.Size = size;
		}

		delegate void SetStringDelegate(string parameter);
		void SetTimeLabel(string newTime)
		{
			try
			{
			   label1.Text = newTime;
			}
			catch { }
		}

		private void LoadOscillConfig()
		{


		}

		//************************** ВЫЗОВЫ ДОЧЕРНИХ ОКОН ***************************************//
		//***************************************************************************************//
		//***************************************************************************************//

		ScopeSetupForm scopeSetupForm;

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			scopeSetupForm = new ScopeSetupForm();
			scopeSetupForm.Show();
		}

		ScopeConfigForm scopeConfigForm;

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			scopeConfigForm = new ScopeConfigForm();
			scopeConfigForm.Show();
		}


		//***************************************************************************//
		//***************************************************************************//
		//***************************************************************************//
		private void InitComport()
		{
			ModBusClient.InitModBusEvent();
		}

		//***************************************************************************//
		//***************************************************************************//
		//***************************************************************************//
		private void connectBtn_Click(object sender, EventArgs e)
		{
			OpenPort();
		}

		void LoadComPortSettings(string comPortXMLName, out int newPar, out int newSpeed, out int newPortIndex, out int newAddr)
		{
			XmlNodeList xmls;
			XmlNode xmlNode;

			var doc = new XmlDocument();
			try { doc.Load(comPortXMLName); }
			catch
			{
				MessageBox.Show("Не удалось открыть файл с настройками!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			xmls = doc.GetElementsByTagName("ComPort");

			if (xmls.Count != 1)
			{
				MessageBox.Show("Ошибки в файле с настройками!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
			xmlNode = xmls[0];
			try
			{
				newPortIndex = Convert.ToInt32(xmlNode.Attributes["Name"].Value);
				newSpeed = Convert.ToInt32(xmlNode.Attributes["Speed"].Value);
				newPar = Convert.ToInt32(xmlNode.Attributes["Parity"].Value);
				newAddr = Convert.ToInt32(xmlNode.Attributes["Addr"].Value);
			}
			catch
			{
				newPortIndex = 1;
				newSpeed = 0;
				newPar = 0;
				newAddr = 1;
				MessageBox.Show("Ошибки в файле с настройками!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		private void OpenPort()
		{
			if (ModBusClient.PortList.Count == 0)
			{
				MessageBox.Show("Нет ни одного доступного COM-порта!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int par; int portIndex; int sp; int addr;
			LoadComPortSettings("PrgSettings.xml", out par, out sp, out portIndex, out addr);

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
		ushort[] oscilsStatus = { 0, 0, 0, 0,   0, 0, 0, 0,  0, 0, 0, 0,     0, 0, 0, 0, 
								  0, 0, 0, 0,   0, 0, 0, 0,  0, 0, 0, 0,     0, 0, 0, 0,  0};

		string[] oscilTitls = new string[32];
		string[] oscTimeDates = new string[32];

		private void SendTimeStampRequest()
		{
			int i1 = requestStep;
			int i  = loadTimeStampStep;


			if (i >= ScopeConfig.ScopeCount)
			{
				requestStep = 0;
				loadTimeStampStep = 0;
				lineBusy = false;
				return;
			}

			while ((oscilsStatus[i] < 4) && (i < ScopeConfig.ScopeCount))
			{
				i++;
			}

			if (i >= ScopeConfig.ScopeCount)
			{
				requestStep = 0;
				loadTimeStampStep = 0;
				lineBusy = false;
				return;
			}
			else
			{
				loadTimeStampStep = i;
			   
				modBusUnit.GetData((ushort)(ScopeSysType.TimeStampAddr + i * 8), 8);
			}
		}
		private void EndTimeStampRequest()
		{
			if (!modBusUnit.modBusData.RequestError)
			{
				UpdateTimeStampInvoke();
			}
			else
			{
				MessageBox.Show("TimeStamp error");
			}

			loadTimeStampStep++;
		}

		private void LoadConfig()
		{
			if (loadConfigStep == 0)
			{
				modBusUnit.GetData(ScopeSysType.ChannelCountAddr, 1);
				return;
			}

			if (loadConfigStep == 1)
			{
				modBusUnit.GetData(ScopeSysType.ScopeCountAddr, 1);
				return;
			}

			if (loadConfigStep == 2)
			{
				modBusUnit.GetData(ScopeSysType.OscilFreqAddr, 1);
				return;
			}

			if (loadConfigStep == 3)
			{
				modBusUnit.GetData(ScopeSysType.HystoryAddr, 8);
				return;
			}

			if (loadConfigStep == 4)
			{
				modBusUnit.GetData(ScopeSysType.DataStartAddr, 16);
				return;
			}

		}
		private void EndLoadConfig()
		{
			if (!modBusUnit.modBusData.RequestError)
			{
				switch (loadConfigStep)
				{
					case 0:
						{
							ScopeConfig.ChannelCount = modBusUnit.modBusData.ReadData[0];
							loadConfigStep = 1;
							LoadConfig();
						} break;

					case 1:
						{
							ScopeConfig.ScopeCount = modBusUnit.modBusData.ReadData[0];
							loadConfigStep = 2;
							LoadConfig();
						} break;

					case 2:
						{
							ScopeConfig.ScopeFreq = modBusUnit.modBusData.ReadData[0];
							loadConfigStep = 3;
							LoadConfig();
						} break;

					case 3:
						{
							ScopeConfig.Hystory = modBusUnit.modBusData.ReadData[0];
							if (modBusUnit.modBusData.ReadData[6] != 0)
							{
								ScopeConfig.ScopeEnabled = true;
							}
							else
							{
								ScopeConfig.ScopeEnabled = false;
							}

							loadConfigStep = 4;
							LoadConfig();
						} break;

					case 4:
						{
							ScopeConfig.InitOscillParams(modBusUnit.modBusData.ReadData);
							loadConfigStep = 0;
							configLoaded = true;
							buttonsAlreadyCreated = false;
						} break;


				}
				return;
			}
			else
			{
				loadConfigStep = 0;
			}
		}
		private void EndLoadTime()
		{
			if (!modBusUnit.modBusData.RequestError)
			{
				string str, str2;
				str = (modBusUnit.modBusData.ReadData[4] & 0x3F).ToString("X2") + "/" + (modBusUnit.modBusData.ReadData[5] & 0x1F).ToString("X2") + "/" + (modBusUnit.modBusData.ReadData[6] & 0xFF).ToString("X2") + "";
				str2 = (modBusUnit.modBusData.ReadData[2] & 0x3F).ToString("X2") + ":" + (modBusUnit.modBusData.ReadData[1] & 0x7F).ToString("X2") + ":" + (modBusUnit.modBusData.ReadData[0] & 0x7F).ToString("X2") + " ";
				try
				{
					Invoke(new SetStringDelegate(SetTimeLabel), str2 + str);
				}
				catch { }
			}
			else
			{
				try
				{
					Invoke(new SetStringDelegate(SetTimeLabel), "Нет связи");
				}
				catch { }
			}
		}
		private void EndLoadStatus(int blockNum)
		{
			int i;
			if (!modBusUnit.modBusData.RequestError)
			{
				for (i = 0; i < 16; i++)
				{
					oscilsStatus[i+16*blockNum] = modBusUnit.modBusData.ReadData[i];
				}
			}
			else
			{
				for (i = 0; i < 16; i++)
				{
					oscilsStatus[i] = 0;
				}
			}

			UpdateOscilsStatusInvoke();
		}

		//Очистка осциллограмм
		int clearOscNum = 0x7FFF;
		bool clearOscFlag = false;
		bool initClearOscFlag = false;
		private void ClearOscRequest()
		{
			if (clearOscNum >= ScopeConfig.ScopeCount)
			{
				MessageBox.Show("Error");
				clearOscFlag = false;
				return;
			}

			ushort u = (ushort)(ScopeSysType.OscilStatusAddr + clearOscNum);
			ushort[] uv = {0,0,0,0};
			modBusUnit.SetData(u,1,uv);
		}


		private void ClearOscResponce()
		{
			clearOscFlag = false;
		}

		//Ручной запуск
		bool manStartFlag = false;
		bool initManStartFlag = false;
		private void ManStartRequest()
		{
			ushort u = (ushort)(ScopeSysType.ParamLoadConfigAddr);//
			//MessageBox.Show(u.ToString("X4"));
			ushort[] uv = { 1, 1, 1, 1 };
			modBusUnit.SetData(u, 1, uv);
		}

		private void ManStartResponce()
		{
			manStartFlag = false;
		}

		private void EndRequest(object sender, EventArgs e)
		{
			if (modBusUnit.modBusData.RequestError)
			{
				ScopeConfig.ChangeScopeConfig = false;
				RemoveButtonsInvoke();
				HideProgressBarInvoke();
				loadOscDataStep = 0;            
				loadOscDataSubStep = 0;
				loadOscilIndex = 0;
				requestStep = 0;
				clearOscFlag = false;
				loadOscData = false;
				lineBusy = false;
				return;
			}

			if (loadOscData)
			{
				LoadOscDataResponce();
				lineBusy = false;
				return;
			}

			if (!configLoaded)
			{
				EndLoadConfig();
			}
			else if (clearOscFlag)
			{
				ClearOscResponce();
			}
			else if (manStartFlag)
			{
				ManStartResponce();
			}
			else if (requestStep == 0)
			{
				EndLoadTime();
				requestStep = 1;
			}
			else if (requestStep == 1)
			{
				EndLoadStatus(0);
				requestStep = 2;
			}
			else if (requestStep == 2)
			{
				EndLoadStatus(1);
				requestStep = 3;
			}

			else if (requestStep == 3)
			{
				EndTimeStampRequest();
			}

			if (ScopeConfig.ChangeScopeConfig)
			{
				ScopeConfig.ChangeScopeConfig = false;
				RemoveButtonsInvoke();
			}

			if (initLoadOscilFlag) { InitLoadOscillInvoke(); initLoadOscilFlag = false; requestStep = 0;  lineBusy = false; return; }
			if (initClearOscFlag) { clearOscFlag = true; initClearOscFlag = false; }
			if (initManStartFlag) { manStartFlag = true; initManStartFlag = false; }
			lineBusy = false;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Enabled = false;
			CreateStatusButtons();
			if (!ModBusClient.ModBusOpened) { timer1.Enabled = true; return; }

			if (loadOscData)    { timer1.Enabled = true; return; }
			if (lineBusy)       { timer1.Enabled = true; return; }

			lineBusy = true;

			if (!configLoaded)
			{
				LoadConfig();
				timer1.Enabled = true;
				return;
			}

			if (clearOscFlag)
			{
				ClearOscRequest();
				requestStep = 0;
				timer1.Enabled = true;
				return;
			}

			if (manStartFlag)
			{
				ManStartRequest();
				requestStep = 0;
				timer1.Enabled = true;
				return;
			}

			if (requestStep==0)
			{
				modBusUnit.GetData(0x202, 8);
				}
			else if (requestStep == 1)
				{modBusUnit.GetData(ScopeSysType.OscilStatusAddr, 16);}

			else if (requestStep == 2)
				{modBusUnit.GetData((ushort)(ScopeSysType.OscilStatusAddr+16), 16); }

			else if (requestStep == 3)
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
			if (buttonsAlreadyCreated) { return; }
			int i;
			statusButtons = new List<Button>();

			Font font = new Font("Open Sans", 9);
			Size size = new Size(120, 60);

			for (i = 0; i < ScopeConfig.ScopeCount; i++)
			{
				statusButtons.Add(new Button());
				statusButtons[i].Text = "№" + (i + 1).ToString() + "\n" + "Пусто";
				statusButtons[i].Size = size;
				statusButtons[i].Font = font;
                statusButtons[i].Margin = new System.Windows.Forms.Padding(2);
				statusButtons[i].Tag = i;
				statusButtons[i].Dock = DockStyle.None;
				statusButtons[i].Tag = i;
				statusButtons[i].Click += new EventHandler(loadOscBtnClick);
				nowStatusFlowLayoutPanel.Controls.Add(statusButtons[i]);
			}
			buttonsAlreadyCreated = true;

		}



		//*************** ОБНОВЛЕНИЕ КОНТРОЛОВ В АСИНХРОННОМ РЕЖИМЕ ***************************************//
		//*************************************************************************************************//
		//*************************************************************************************************//
		delegate void NoParamDelegate();

		private void UpdateOscilsStatus()
		{
			int i;
			
			for (i = 0; i < ScopeConfig.ScopeCount; i++)
			{
				if (oscilsStatus[i] == 0) 
				{
					statusButtons[i].FlatStyle = FlatStyle.Standard;
					statusButtons[i].BackColor = Color.White;
					statusButtons[i].Enabled = false;
					oscilTitls[i] = "Осциллограмма №" + (i + 1).ToString() + ".";
					statusButtons[i].Text = "№" + (i + 1).ToString() + "\n" + "Пусто";
				
				}
                else if (oscilsStatus[i] >= 4) { statusButtons[i].BackColor = Color.LightSteelBlue; ; statusButtons[i].Enabled = true; }
			
				else if (oscilsStatus[i] == 3)
				{
					statusButtons[i].FlatStyle = FlatStyle.Standard;
                    statusButtons[i].BackColor = Color.Lavender; 
					statusButtons[i].Enabled = true;
					oscilTitls[i] = "Осциллограмма №" + (i + 1).ToString() + ".";
					statusButtons[i].Text = "№" + (i + 1).ToString() + "\n" + "Идет запись";         
				}
				else if (oscilsStatus[i] == 1)
				{
                    statusButtons[i].BackColor = Color.GhostWhite;
					statusButtons[i].Enabled = true;
					oscilTitls[i] = "Осциллограмма №" + (i + 1).ToString() + ".";
					statusButtons[i].Text = "№" + (i + 1).ToString() + "\n" + "Запись предыстории";
				}
				else {
                    statusButtons[i].BackColor = Color.AliceBlue;
					statusButtons[i].Enabled = true;
					oscilTitls[i] = "Осциллограмма №" + (i + 1).ToString()+".";
					statusButtons[i].Text = "№" + (i + 1).ToString() + "\n" + "Готова к записи";
				}
			}
		}
		private void UpdateOscilsStatusInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateOscilsStatus), null);
			}
			catch { }
		}

		private void UpdateTimeStamp()
		{
			 string str, str2;
			 str = (modBusUnit.modBusData.ReadData[5] & 0x3F).ToString("X2") + "." + (modBusUnit.modBusData.ReadData[6] & 0x1F).ToString("X2") + ".20" + (modBusUnit.modBusData.ReadData[7] & 0xFF).ToString("X2");     //D.M.Y врямя 
			 str2 = (modBusUnit.modBusData.ReadData[3] & 0x3F).ToString("X2") + ":" + (modBusUnit.modBusData.ReadData[2] & 0x7F).ToString("X2") + ":" + (modBusUnit.modBusData.ReadData[1] & 0x7F).ToString("X2");      //S.M.H    
			 statusButtons[loadTimeStampStep].Text = "№" + (loadTimeStampStep + 1).ToString() + "\n" + str + "\n" + str2;
			 oscilTitls[loadTimeStampStep] = "Осциллограмма №" + (loadTimeStampStep + 1).ToString() + "\n" + str + "\n" + str2;
			 oscTimeDates[loadTimeStampStep] = str + " " + str2;
		}
		private void UpdateTimeStampInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateTimeStamp), null);
			}
			catch { }
		}

		private void UpdateLoadDataProgressBar()
		{
			loadDataProgressBar.Value = (int)loadOscilIndex;
		}
		private void UpdateLoadDataProgressBarInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(UpdateLoadDataProgressBar), null);
			}
			catch { }      
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
			catch { }     
		}

		private void RemoveButtons()
		{
			try
			{
				for (int i = 0; i < ScopeConfig.ScopeCount; i++)
				{
					nowStatusFlowLayoutPanel.Controls.Remove(statusButtons[i]);
				}
				statusButtons.Clear();
			}
			catch { }
			
			configLoaded = false;
		}
		private void RemoveButtonsInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(RemoveButtons), null);
			}
			catch { }     
		}


		//*************************** СКАЧИВАНИЕ ОСЦИЛЛОГРАММ В ФАЙЛ ************************************//
		//***********************************************************************************************//
		//***********************************************************************************************//
		private void loadOscBtnClick(object sender, EventArgs e)
		{
			bool b = false;
			if (oscilsStatus[(int)(sender as Button).Tag] >= 4)
			{
				b = true;
			}

			LoadOscQueryForm loadOscQueruForm = new LoadOscQueryForm(oscilTitls[(int)(sender as Button).Tag], b);
			DialogResult dlgr = loadOscQueruForm.ShowDialog();

			//СКАЧИВАНИЕ ОСЦИЛЛОГРАММ
			if (dlgr == DialogResult.OK)
			{
				loadOscNum = (uint)(sender as Button).Tag;
				initLoadOscilFlag = true;
				//InitParamsLines();
			}

			//СБРОС ОСЦИЛЛОГРАММ
			else if (dlgr == DialogResult.Abort)
			{
				clearOscNum = (int)(sender as Button).Tag;
				initClearOscFlag = true;
			}
		}

		//Загрузка осциллограмм
		List<ushort[]> downloadedData = new List<ushort[]>(); 

		void LoadOscDataRequest()
		{
			switch (loadOscDataStep)
			{
				//Загрузка стартового индекса
				case 0:
					{
						modBusUnit.GetData((ushort)(ScopeSysType.LoadOscilStartAddr + loadOscNum), 1);
					} break;

				//Загрузка данных
				case 1:
					{
						if (loadOscDataSubStep == 0)
						{
							ushort[] writeArr = new ushort[4];
						   
							writeArr[0] = 0x0001;
							writeArr[1] = (ushort)loadOscNum;
							writeArr[2] = Convert.ToUInt16(( CalcOscilLoadTemp(loadOscNum) << 16) >> 16);
							writeArr[3] = Convert.ToUInt16(CalcOscilLoadTemp(loadOscNum) >> 16); 
							modBusUnit.SetData((ushort)(ScopeSysType.ParamLoadConfigAddr + 1), 4, writeArr);
						}
						else
						{
							modBusUnit.GetData((ushort)(ScopeSysType.ParamLoadDataAddr + (loadOscDataSubStep - 1) * 8), 8);
						}

					} break;
			}
		}
		void LoadOscDataResponce()
		{
			switch (loadOscDataStep)
			{
				//Загрузка стартового адреса
				case 0:
					{
						if (!modBusUnit.modBusData.RequestError)
						{
							loadOscilStart = modBusUnit.modBusData.ReadData[0];
							loadOscDataStep = 1;
						}
						else
						{
							loadOscData = false;
							loadOscDataStep = 0;
							loadOscNum = 0;
						}

					} break;

				case 1:
					{
						if (!modBusUnit.modBusData.RequestError)
						{
							switch (loadOscDataSubStep)
							{
								case 0:
									{

									}break;
								case 1:
								case 2:
								case 3:
								case 4:
									{
										for (int i = 0; i < 8; i++)
										{
											loadParamPart[i + (loadOscDataSubStep - 1) * 8] =
												modBusUnit.modBusData.ReadData[i];
										}

									}break;
							}
							loadOscDataSubStep++;
							if (loadOscDataSubStep == 5) 
							{
								downloadedData.Add(new ushort[32]);
								for (int i = 0; i < 32; i++)
								{
									downloadedData[downloadedData.Count - 1][i] = loadParamPart[i];
								}

								loadOscDataSubStep = 0;
								UpdateLoadDataProgressBarInvoke();

								if (loadOscilIndex==0)
								{
									createFileNum = loadOscNum;
									createFileFlag = true;
									//CreateFileInvoke();
									loadOscData = false;
									loadOscDataStep = 0;
									loadOscNum = 0;
									UpdateLoadDataProgressBarInvoke();
									
								}

							}
						}
						else
						{
							loadOscData = false;
							loadOscDataStep = 0;
							loadOscNum = 0;
						}

					} break;
			}
			if (!loadOscData) 
			{
				HideProgressBarInvoke();
				return; 
			}
			if (!modBusUnit.modBusData.RequestError) { LoadOscDataRequest(); }

		}

		bool    loadOscData         = false;        //Флаг, что идет скачивание осцилограммы, все остальные запросы приостановлены
		int     loadOscDataStep     = 0;            //0 - загрузка LoadOscilStart
													//1 - загрузка непосредственно тела

		int     loadOscDataSubStep  = 0;            //0 - расчет MemoryAddr
													//1 - получение порции данных
													//2 - 
													//3 -
													//4 -
		ushort[] loadParamPart = new ushort[32];

		uint loadOscNum          = 0;
		uint loadOscilIndex = 0x0000;
		uint loadOscilStart = 0x0000;
		uint OscilStartTemp;
		uint OscilEndTemp;

		uint CalcOscilLoadTemp(uint nowLoadOscNum)
		{
			OscilStartTemp = nowLoadOscNum*(OscilSize(ScopeSysType.OscilAllSize) / 2);  	
			OscilEndTemp = (nowLoadOscNum + 1)*(OscilSize(ScopeSysType.OscilAllSize) / 2);
			loadOscilStart += 32;
			if (OscilEndTemp - loadOscilStart < 32) loadOscilStart = OscilStartTemp;
			return loadOscilStart;
		}

		private uint OscilSize(uint AllSize)
		{
			uint OscilMaxMultiplicity;

			OscilMaxMultiplicity = ScopeSysType.ChannelFormats.Max();

			if (OscilMaxMultiplicity == 16) OscilMaxMultiplicity = 2;
			if (OscilMaxMultiplicity == 32) OscilMaxMultiplicity = 4;
			if (OscilMaxMultiplicity == 64) OscilMaxMultiplicity = 8;


			uint OscS = (AllSize * 1024) / Convert.ToUInt32(ScopeConfig.ScopeCount);
			while (OscS % OscilMaxMultiplicity != 0 || OscS % ScopeConfig.ScopeCount != 0)   //Проверка на кратность слова с учетом колличества каналов  
			{
				OscS--;
			}

			return (OscS);
		}

		//СОЗДАНИЕ ФАЙЛА
		string HexToPercent(ushort value)
		{
			double f = (short)value / 40.96;
			return (f.ToString("F2"));
		}

		string FileHeaderLine()
		{
			string str = "";
			int i = 0;
			str = " " + "\t";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.OscillParams[i] < ScopeSysType.ChannelNames.Count)
				{
					str = str + ScopeSysType.ChannelNames[ScopeConfig.OscillParams[i]] + "\t";
				}
			}
			return str;
		}
		string FileColorLine()
		{
			string str = "";
			int i = 0;
			str = " " + "\t";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.OscillParams[i] < ScopeSysType.ChannelNames.Count)
				{
					str = str + ScopeSysType.ChannelColors[ScopeConfig.OscillParams[i]].ToArgb().ToString() + "\t";
				}
			}
			return str;
		}
		string FileOffsetLine()
		{
			string str = "";
			int i = 0;
			str = " " + "\t";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.OscillParams[i] < ScopeSysType.ChannelNames.Count)
				{
					str = str + "0,00000" + "\t";
				}
			}
			return str;
		}
		string FileScaleLine()
		{
			string str = "";
			int i = 0;
			str = " " + "\t";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.OscillParams[i] < ScopeSysType.ChannelNames.Count)
				{
					str = str + "1,00000" + "\t";
				}
			}
			return str;
		}

		string FileReserveLine()
		{
			string str = "";
			int i = 0;
			str = " " + "\t";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.OscillParams[i] < ScopeSysType.ChannelNames.Count)
				{
					str = str + "0" + "\t";
				}
			}
			return str;
		}
		string FileStepLine()
		{
			string str = "";
			int i = 0;
			str = " " + "\t";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.OscillParams[i] < ScopeSysType.ChannelNames.Count)
				{
					str = str + ScopeSysType.ChannelStepLines[ScopeConfig.OscillParams[i]].ToString()+ "\t";
				}
			}
			return str;
		}
		string FileParamLine(ushort[] paramLine, int lineNum)
		{
			string str = "";
			int i = 0;
			str = lineNum.ToString() + "\t";
			for (i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				//Если параметр в списке известных, то пишем его в файл
				if (ScopeConfig.OscillParams[i] < ScopeSysType.ChannelNames.Count)
				{
				  //  str = str + HexToPercent(paramLine[i]) + "\t";
				   // MessageBox.Show(ScopeSysType.ChannelNames[i]+" "+ScopeSysType.ChannelFormats[i].ToString());
					//MessageBox.Show(ScopeSysType.ChannelFormats[i].ToString());
					str = str + AdvanceConvert.HexToFormat(paramLine[i], (byte)ScopeSysType.ChannelFormats[ScopeConfig.OscillParams[i]]) + "\t";
				}
			}
			return str;
		}
		List<ushort[]> InitParamsLines()
		{
			ushort countInLine = (ushort)Math.Round(32.0 / ScopeConfig.ChannelCount);
			List<ushort[]> paramsLines = new List<ushort[]>();

			for (int i3 = 0; i3 < downloadedData.Count; i3++)
			{
				for (int i = 0; i < countInLine; i++)
				{
					paramsLines.Add(new ushort[ScopeConfig.ChannelCount]);
					for (int i2 = 0; i2 < ScopeConfig.ChannelCount; i2++)
					{
						paramsLines[paramsLines.Count-1][i2] = downloadedData[i3][i * ScopeConfig.ChannelCount + i2];
					}
				}
			}
			return paramsLines;
		}

		void CreateFile()
		{
		   // MessageBox.Show("SDF");
			SaveFileDialog sfd = new SaveFileDialog();
			//sfd.FileName = oscilTitls[createFileNum];
			sfd.DefaultExt = ".txt"; // Default file extension
			sfd.Filter = "Текстовый файл (.txt)|*.txt"; // Filter files by extension
			if (sfd.ShowDialog() != DialogResult.OK) { return; }

			StreamWriter sw;
			try
			{ 
				sw = File.CreateText(sfd.FileName); 
			}
			catch
			{
				MessageBox.Show("Ошибка при создании файла!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				sw.WriteLine(oscTimeDates[createFileNum]);
				sw.WriteLine(ScopeConfig.ScopeFreq.ToString());
				sw.WriteLine(FileHeaderLine());
				sw.WriteLine(FileColorLine());
				sw.WriteLine(FileOffsetLine());
				sw.WriteLine(FileScaleLine());
				sw.WriteLine(FileStepLine());
				
				sw.WriteLine(FileReserveLine());
				sw.WriteLine(FileReserveLine());
				sw.WriteLine(FileReserveLine());
				sw.WriteLine(FileReserveLine());

				List<ushort[]> lu = InitParamsLines();
				for (int i = 0; i < lu.Count; i++)
				{
					sw.WriteLine(FileParamLine(lu[i], i));
				}
			}
			catch
			{
				MessageBox.Show("Ошибка при записи в файл!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			sw.Close();

			if (MessageBox.Show("Открыть осциллограмму для просмотра",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ExecuteScopeView(sfd.FileName);
			}
		}

		bool initLoadOscilFlag = false;
		private void InitLoadOscill()
		{
			loadOscData = true;
			loadOscDataStep = 0;
			initLoadOscilFlag = false;
			loadOscDataSubStep = 0;
			loadOscilIndex = 0;

			loadScopeToolStripLabel.Text = oscilTitls[loadOscNum];
			loadDataProgressBar.Value = 0;

			loadDataProgressBar.Visible = true;
			loadScopeToolStripLabel.Visible = true;

			downloadedData = new List<ushort[]>();
		   // loadOscilCountRound = (UInt16)(ScopeConfig.ScopeCount * 8);
			LoadOscDataRequest();
		}
		private void InitLoadOscillInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(InitLoadOscill), null);
			}
			catch { }      
		}
		private void CreateFileInvoke()
		{
			try
			{
				Invoke(new NoParamDelegate(CreateFile), null);
			}
			catch { }  
		}
		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			CreateFile();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveWindowSize("prgSettings.xml");
		}


		//Запуск приложения для просмотра осциллограмм
		private void ExecuteScopeView(string fileName)
		{
			Process proc = new Process();
			proc.StartInfo.FileName = CalcApplPath() + "scopeviewapplication.exe";
			proc.StartInfo.Arguments = "\"" + fileName + "\"";
			proc.Start();
		}


		private void toolStripButton2_Click_1(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".txt"; // Default file extension
			ofd.Filter = "Текстовый файл (.txt)|*.txt"; // Filter files by extension
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				ExecuteScopeView(ofd.FileName);
			}
		}

		bool createFileFlag = false;
		uint createFileNum = 0;
		private void timer2_Tick(object sender, EventArgs e)
		{
			timer2.Enabled = false;
			if (createFileFlag)
			{
				createFileFlag = false;
				CreateFile();
			}
			timer2.Enabled = true;
		}


		//Ручной запуск осциллографа
		private void manStartBtn_Click(object sender, EventArgs e)
		{
			initManStartFlag = true;
		}

	}
}
