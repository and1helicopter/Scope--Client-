using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using UniSerialPort;

namespace ScopeSetupApp.MainForm
{
	public sealed partial class MainForm
	{
		public void SerialPortOpen()
		{
			SerialPort.Open();

			ConfigCheack();
		}

		private Thread _updateThread;

		private void StartUpdateThread()
		{
			if (_updateThread == null)
			{
				_updateThread = new Thread(UpdateThread)
				{
					Name = @"UpdateThread"
				};
				_updateThread.Start();
			}
		}

		private void UpdateThread()
		{
			while (true)
			{
				UpdateStatusConnect();
				UpdateStatusConfigToSystemStrLabel();

				//UpdateStatusButtonsInvoke();
				//UpdateStatusButtons();
				//UpdateStatus();
			//	UpdateTimeStamp();


			//	UpdateOscilsStatus();


				//if (SerialPort.portError)
				//{
				//	ScopeConfig.ConnectMcu = false;
				//	ScopeConfig.ChangeScopeConfig = false;

				//	UpdateScopeSetupShowButtonsLoad();
				//	StatusConfigToSystemStrLabel();
				//	RemoveButtonsInvoke();
				//	HideProgressBarInvoke();
				//	_loadOscData = false;

				//	_loadOscDataStep = 0;
				//	_loadOscilIndex = 0;

				//	Thread.Sleep(500);
				//	continue;
				//}




				ScopeConfig.ConnectMcu = true;



				Thread.Sleep(50);
			}
		}

		private bool _updateStatus;

		private void UpdateStatus()
		{
			if (!_updateStatus && SerialPort.IsOpen && !ScopeConfig.ChangeScopeConfig)
			{
				_updateStatus = true;
				SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 8), 16, UpdateStatus, RequestPriority.Normal, "part1");
				SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 24), 16, UpdateStatus, RequestPriority.Normal, "part2");
			}
		}

		private void UpdateStatus(bool dataOk, ushort[] paramRtu, object param)
		{
			if (dataOk)
			{
				switch (param.ToString())
				{
					case "part1":
						for (int j = 0; j < paramRtu.Length; j++)
						{
							_oscilsStatus[j] = paramRtu[j];
						}
						break;
					case "part2":
						for (int j = 0; j < paramRtu.Length; j++)
						{
							_oscilsStatus[j + 16] = paramRtu[j];
						}
						_updateStatus = false;
						break;
				}
			}

			UpdateOscilsStatusInvoke();
		}

		private delegate void SetStringDelegate(string parameter);

		private bool _connectToSysem;

		private void SetTimeLabel(string statusConnect)
		{
			connect_toolStripStatusLabel.Text = statusConnect;
			_connectToSysem = statusConnect == @"CONNECT";
			SetEnableOrDisableLoadConfigToSystem(_connectToSysem);
		}

		private void SetEnableOrDisableLoadConfigToSystem(bool connect)
		{
			_ucScopeSetup?.ButtonsVisibale(connect);
		}

		private void UpdateStatusConnect()
		{
			Invoke(new SetStringDelegate(SetTimeLabel), SerialPort.IsOpen && !SerialPort.portError ? @"CONNECT" : @"NO CONNECT");
		}

		private delegate void StatusConfigToSystem(string status);

		private void UpdateStatusConfigToSystemStrLabel()
		{
			Invoke(new StatusConfigToSystem(SetStatusConfigToSystemLabel), StatusConfigToSystemStrLabel());
		}

		private void SetStatusConfigToSystemLabel(string status)
		{
			systemConfig_toolStripStatusLabel.Text = status;
		}

		private string StatusConfigToSystemStrLabel()
		{
			if ((ScopeConfig.StatusOscil & 0x0001) == 0x0000)
			{
				return @"Статус загрузки конфигурации: Конфигурация отсутствует";
			}
			if ((ScopeConfig.StatusOscil & 0x0001) == 0x0001)
			{
				return @"Статус загрузки конфигурации: " + @"Конфигурация успешно загружена";
			}
			if ((ScopeConfig.StatusOscil & 0x0002) == 0x0002)
			{
				return @"Статус загрузки конфигурации:" + "\n" + @"Конфигурация загружена, но не прошла проверку";
			}
			if ((ScopeConfig.StatusOscil & 0x0004) == 0x0004)
			{
				return @"Статус загрузки конфигурации:" + "\n" + @"При загрузке нарушена целостность данных";
			}
			return "";
		}






		private bool _updateTimeStamp;

		private void UpdateTimeStamp()
		{
			if (!_updateTimeStamp && SerialPort.IsOpen && _statusButtons?.Count != 0 && !ScopeConfig.ChangeScopeConfig)
			{
				if (ScopeConfig.ScopeCount != 0)
				{
					_updateTimeStamp = true;
				}
				for (int j = 0; j < ScopeConfig.ScopeCount; j++)
				{
					SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 136 + j * 6), 6, UpdateTimeStamp, j);
				}
			}
		}

		private void UpdateTimeStamp(bool dataOk, ushort[] paramRtu, object param)
		{
			if (dataOk)
			{
				var index = Convert.ToInt32(param);

				string str1 = (paramRtu[0] & 0x3F).ToString("X2") + "/" + ((paramRtu[0] >> 8) & 0x1F).ToString("X2") + @"/20" + (paramRtu[1] & 0xFF).ToString("X2");
				string str2 = (paramRtu[3] & 0x3F).ToString("X2") + ":" + ((paramRtu[2] >> 8) & 0x7F).ToString("X2") + @":" + (paramRtu[2] & 0x7F).ToString("X2");
				string str3 = ((paramRtu[4] *1000) >> 8).ToString("D3") + @"000";
				string strTextButton = @"№" + (index + 1) + "\n" + str1 + "\n" + str2;
				string strTitle = @"Осциллограмма №" + (index + 1) + "\n" + str1 + "\n" + str2;
				string str = str1 + "," + str2 + @"." + str3;
				DateTime date = DateTime.Parse(str);

				UpdateTimeInvoke(index, strTextButton, strTitle, date);

				if (index == ScopeConfig.ScopeCount - 1)
				{
					_updateTimeStamp = false;
				}
			}
		}

		private byte CodeDevice = 0x00;

		private void SetScopeStatus(int index)
		{
			var statusGet = _oscilsStatus[index];
			if ((byte)statusGet == 0x04)
			{
				if ((byte)((statusGet >> 8) & (ushort)(1 << CodeDevice)) != (1 << CodeDevice))
				{
					ushort [] statusSet =
					{
						Convert.ToUInt16(statusGet | (1 << 8 + CodeDevice))
					};

					ushort addr = (ushort)(ScopeSysType.OscilCmndAddr + 8 + index);

					SerialPort.SetDataRTU(addr, null, RequestPriority.Normal, statusSet);
				}
			}
		}

		private void UnsetScopeStatus(int index)
		{
			var statusGet = _oscilsStatus[index];
			if ((byte)statusGet == 0x04)
			{
				if ((byte)((statusGet >> 8) & (ushort)(1 << CodeDevice)) == (1 << CodeDevice))
				{
					ushort [] statusSet =
					{
						Convert.ToUInt16(statusGet ^ (1 << 8 + CodeDevice))
					};

					ushort addr = (ushort)(ScopeSysType.OscilCmndAddr + 8 + index);

					SerialPort.SetDataRTU(addr, null, RequestPriority.Normal, statusSet);
				}
			}
		}
		
		private void ClearOscRequest()
		{
			if (_clearOscNum >= ScopeConfig.ScopeCount)
			{
				MessageBox.Show(@"Error");
				return;
			}

			UnsetScopeStatus(_clearOscNum);
		}

		//Ручной запуск
		private void ManStartRequest()
		{
			ushort[] uv = { 1, 1, 1, 1 };

			SerialPort.SetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 4), null, RequestPriority.Normal, uv);
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
					SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 72 + _loadOscNum * 2),2, LoadOscDataResponce, 0);
				}
					break;

				//Загрузка данных
				case 1:
				{
					{
						uint oscilLoadTemp = (CalcOscilLoadTemp()) >> 5;
						SerialPort.GetDataRTU04((ushort)(oscilLoadTemp), 32, LoadOscDataResponce, 1);
					}
				}
					break;
			}
		}

		private void LoadOscDataResponce(bool dataOk, ushort[] paramRtu, object param)
		{
			if (dataOk)
			{
				switch (Convert.ToInt32(param))
				{
					//Загрузка стартового адреса
					case 0:
					{
						if (!SerialPort.portError)
						{
							_startLoadSample = (uint) (paramRtu[1] << 16);
							_startLoadSample += paramRtu[0];
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
					}
						break;

					case 1:
					{
						if (!SerialPort.portError)
						{
							for (int i = 0; i < 32; i++)
							{
								_loadParamPart[i] = paramRtu[i];
							}
							{
								_downloadedData.Add(new ushort[32]);
								for (int i = 0; i < 32; i++)
								{
									_downloadedData[_downloadedData.Count - 1][i] = _loadParamPart[i];
								}

								_loadOscilIndex = (2 * _countTemp * 1000) / ScopeConfig.OscilSize;

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
							UnsetScopeStatus(_loadOscNum);
						}

					}
						break;
				}
				if (!_loadOscData)
				{
					HideProgressBarInvoke();
					return;
				}
				if (!SerialPort.portError)
				{
					LoadOscDataRequest();
				}
			}
			else
			{
				HideProgressBarInvoke();
			}
		}

		private int _indexChannel;

		public void ConfigCheack()
		{
			LoadConfig();
		}

		private void LoadConfig()
		{
			switch (_loadConfigStep)
			{
				case 0:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 67), 1, LoadConfig, RequestPriority.Normal, 0);
					break;
				case 1:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 66), 1, LoadConfig, RequestPriority.Normal, 1);
					break;
				case 2:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 68), 1, LoadConfig, RequestPriority.Normal, 2);
					break;
				case 3:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 69), 1, LoadConfig, RequestPriority.Normal, 3);
					break;
				case 4:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 70), 1, LoadConfig, RequestPriority.Normal, 4);
					break;
				case 5:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 64), 2, LoadConfig, RequestPriority.Normal, 5);
					break;
				case 6:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 2), 1, LoadConfig, RequestPriority.Normal, 6);
					break;
				case 7:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 376), 2, LoadConfig, RequestPriority.Normal, 7);
					break;
				case 8:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 3), 1, LoadConfig, RequestPriority.Normal, 8);
					break;
				case 9:
					SerialPort.GetDataRTU(ScopeSysType.OscilCmndAddr, 2, LoadConfig, RequestPriority.Normal, 9);
					break;
				case 10:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 378), 2, LoadConfig, RequestPriority.Normal, 10);
					break;
				case 11:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 32), 32, LoadConfig, RequestPriority.Normal, 11);
					break;
				case 12:
					SerialPort.GetDataRTU(ScopeSysType.ConfigurationAddr, 32, LoadConfig, RequestPriority.Normal, 12);
					break;
				case 13:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 71 + 16 * _indexChannel), 16, LoadConfig, RequestPriority.Normal, 13);
					break;
				case 14:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 583 + _indexChannel), 1, LoadConfig, RequestPriority.Normal, 14);
					break;
				case 15:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 615 + 8 * _indexChannel), 8, LoadConfig, RequestPriority.Normal, 15);
					break;
				case 16:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 871 + 4 * _indexChannel), 4, LoadConfig, RequestPriority.Normal, 16);
					break;
				case 17:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 999 + _indexChannel), 1, LoadConfig, RequestPriority.Normal, 17);
					break;
				case 18:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1031), 16, LoadConfig, RequestPriority.Normal, 18);
					break;
				case 19:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1047), 8, LoadConfig, RequestPriority.Normal, 19);
					break;
				case 20:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1055), 4, LoadConfig, RequestPriority.Normal, 20);
					break;
				case 21:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1059), 4, LoadConfig, RequestPriority.Normal, 21);
					break;
				case 22:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1063), 4, LoadConfig, RequestPriority.Normal, 22);
					break;
				case 23:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1067), 4, LoadConfig, RequestPriority.Normal, 23);
					break;
			}
		}

		private void LoadConfig(bool dataOk, ushort[] paramRtu, object param)
		{
			if (dataOk)
			{
				if (!SerialPort.portError)
				{
					switch (_loadConfigStep)
					{
						case 0: //Количество каналов 
						{
							ScopeConfig.ChannelCount = paramRtu[0];
							_loadConfigStep = 1;
							LoadConfig();
						}
							break;

						case 1: //Количество осциллограмм
						{
							ScopeConfig.ScopeCount = paramRtu[0];
							_loadConfigStep = 2;
							LoadConfig();
						}
							break;

						case 2: //Предыстория 
						{
							ScopeConfig.HistoryCount = paramRtu[0];
							_loadConfigStep = 3;
							LoadConfig();
						}
							break;

						case 3: //Делитель
						{
							ScopeConfig.FreqCount = paramRtu[0];
							_loadConfigStep = 4;
							LoadConfig();
						}
							break;

						case 4: //Режим работы
						{
							ScopeConfig.OscilEnable = paramRtu[0];
							_loadConfigStep = 5;
							LoadConfig();
						}
							break;

						case 5: //Размер осциллограммы 
						{
							ScopeConfig.OscilSize = (uint) (paramRtu[1] << 16);
							ScopeConfig.OscilSize += paramRtu[0];
							_loadConfigStep = 6;
							LoadConfig();
						}
							break;

						case 6: //Частота выборки
						{
							ScopeConfig.SampleRate = paramRtu[0];
							ScopeConfig.ScopeEnabled = true;
							_loadConfigStep = 7;
							LoadConfig();
						}
							break;

						case 7: //Размер осциллограммы 
						{
							ScopeConfig.OscilAllSize = (uint) (paramRtu[1] << 16);
							ScopeConfig.OscilAllSize += (paramRtu[0]);
							_loadConfigStep = 8;
							LoadConfig();
						}
							break;

						case 8: //Размер одной выборки
						{
							ScopeConfig.SampleSize = paramRtu[0];
							_loadConfigStep = 9;
							LoadConfig();
						}
							break;
						case 9: //Размер всей памяти 
						{
							ScopeConfig.OscilHistCount = (uint) (paramRtu[1] << 16);
							ScopeConfig.OscilHistCount += paramRtu[0];
							_loadConfigStep = 10;
							LoadConfig();

						}
							break;
						case 10: //Статус осциллогрофа
						{
							ScopeConfig.StatusOscil = paramRtu[0];
							_loadConfigStep = 11;
							LoadConfig();
						}
							break;
						case 11: //Адреса каналов 
						{
							ScopeConfig.InitOscilAddr(paramRtu);
							_loadConfigStep = 12;
							LoadConfig();
						}
							break;
						case 12: //Формат каналов 
						{
							ScopeConfig.InitOscilFormat(paramRtu);
							ScopeConfig.InitOscilParams(ScopeConfig.OscilAddr, ScopeConfig.OscilFormat);
							_loadConfigStep = 13;
							LoadConfig();
						}
							break;
						case 13: //Названия каналов 
						{
							if (ScopeConfig.ChannelCount == 0) //Если в системе нет конфигурации
							{
								_loadConfigStep = 0;
								_buttonsAlreadyCreated = false;
								break;
							}
							if (_indexChannel == 0) ScopeConfig.ChannelName.Clear();
							ScopeConfig.InitChannelName(paramRtu);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 14;
							}
							else
							{
								_indexChannel++;
							}
							LoadConfig();
						}
							break;
						case 14: //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelPhase.Clear();
							ScopeConfig.InitChannelPhase(paramRtu);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 15;
							}
							else
							{
								_indexChannel++;
							}
							LoadConfig();
						}
							break;

						case 15: //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelCcbm.Clear();
							ScopeConfig.InitChannelCcbm(paramRtu);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 16;
							}
							else
							{
								_indexChannel++;
							}
							LoadConfig();
						}
							break;

						case 16: //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelDemension.Clear();
							ScopeConfig.InitChannelDemension(paramRtu);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 17;
							}
							else
							{
								_indexChannel++;
							}
							LoadConfig();
						}
							break;

						case 17: //Названия каналов 
						{
							if (_indexChannel == 0) ScopeConfig.ChannelType.Clear();
							ScopeConfig.InitChannelType(paramRtu);
							if (_indexChannel == ScopeConfig.ChannelCount - 1)
							{
								_indexChannel = 0;
								_loadConfigStep = 18;
							}
							else
							{
								_indexChannel++;
							}
							LoadConfig();
						}
							break;

						case 18: //
						{
							ScopeConfig.InitStationName(paramRtu);
							_loadConfigStep = 19;
							LoadConfig();
						}
							break;

						case 19: //Названия каналов 
						{
							ScopeConfig.InitRecordingId(paramRtu);
							_loadConfigStep = 20;
							LoadConfig();

						}
							break;

						case 20: //Названия каналов 
						{
							ScopeConfig.InitTimeCode(paramRtu);
							_loadConfigStep = 21;
							LoadConfig();
						}
							break;

						case 21: //Названия каналов 
						{
							ScopeConfig.InitLocalCode(paramRtu);

							_loadConfigStep = 22;
							LoadConfig();
						}
							break;

						case 22: //Названия каналов 
						{
							ScopeConfig.InitTmqCode(paramRtu);

							_loadConfigStep = 23;
							LoadConfig();
						}
							break;

						case 23: //Названия каналов 
						{
							ScopeConfig.InitLeapsec(paramRtu);

							_loadConfigStep = 0;
							_ucScopeSetup?.StatusConfigToSystemStrLabel();
							CreateStatusButtons();
							ScopeConfig.ChangeScopeConfig = false;
						}
							break;
					}
				}
				else
				{
					_loadConfigStep = 0;
				}
			}
		}
	}
}