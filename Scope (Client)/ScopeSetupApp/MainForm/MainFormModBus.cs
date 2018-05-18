using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows;
using UniSerialPort;
using MessageBox = System.Windows.MessageBox;

namespace ScopeSetupApp.MainForm
{
	public sealed partial class MainForm
	{
		public void SerialPortOpen()
		{
			SerialPort.Open();
            //Установлено соединение с COM портом
		    if (SerialPort.IsOpen)
		        CheackConnect();
		    else
		    {
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

	    private delegate void SetBoolDelegate(bool parameter);

        private void SetEnableOrDisableLoadConfigToSystem(bool connect)
	    {
	        _ucScopeSetup?.ButtonsVisibale(connect);
	    }

        private delegate void SetStringDelegate(string parameter);
		private void SetComLabel(string status)
		{
			com_toolStripStatusLabel.Text = status;
		}

	    private bool _cheackComError;

	    private void CheackCom()
	    {
	        if (SerialPort.portError && !_cheackComError)
	        {
                StopUpdate();
	            MessageBox.Show(@"Обрыв соединения", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
	            _cheackComError = true;
	        }
	        else if(!SerialPort.portError &&  _cheackComError)
	        {
                _cheackComError = false;
            }
        }

		private void UpdateStatusComPort()
		{
		    CheackCom();
            if (com_toolStripStatusLabel != null)
			{
				Invoke(new SetStringDelegate(SetComLabel), SerialPort.IsOpen && !SerialPort.portError ? @"COM: Открыт" : @"COM: Закрыт");
            }
        }

	    private bool _connectToSystem;

	    private void SetConnectLabel(string status)
	    {
	        connect_toolStripStatusLabel.Text = status;
	    }

	    private void UpdateStatusConnection()
	    {
	        if (connect_toolStripStatusLabel != null)
	        {
	            Invoke(new SetStringDelegate(SetConnectLabel), _connectToSystem ? @"CONNECT" : @"NO CONNECT");
	            Invoke(new SetBoolDelegate(SetEnableOrDisableLoadConfigToSystem), _connectToSystem);
            }
	    }

        private void UpdateTimeStamp()
		{
			if (SerialPort.IsOpen && _statusButtons?.Count != 0 && !ScopeConfig.ChangeScopeConfig)
			{
				var maxCount = ScopeConfig.ScopeCount < 32 ? ScopeConfig.ScopeCount : 32;
				for (int j = 0; j < maxCount; j++)
				{
					if (_oscilsStatus[j] >= 4)
					{
						SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 136 + j * 4), 4, UpdateTimeStamp, j);
					}
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
			    string str3 = ((paramRtu[3]  >> 6) & 0x3E7).ToString("D3"); 
				string strTextButton = @"№" + (index + 1) + "\n" + str2 + "\n" + str1;
				string strTitle = @"Осциллограмма №" + (index + 1) + "\n" + str1 + "\n" + str2;
			    string str = str1 + "," + str2  + @"."+ str3;
                try
				{
					var date = DateTime.Parse(str);
					if (_oscilsStatus[index] >= 4)
					{
						UpdateTimeInvoke(index, strTextButton, strTitle, date);
					}
				}
				catch
				{
					// ignored
				}
			}
		}

	    private void UpdateStatusСonfig()
	    {
	        if (_connectToSystem)
	        {
	            //OscilStatusLoad 378
	            SerialPort.GetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 378), 1, UpdateStatusСonfig, "OscilStatusLoad");
	            //OscilEnable 70
	            SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 70), 1, UpdateStatusСonfig, "OscilEnable");
            }
	        else
	        {
	            Invoke(new SetOscilStatusLoad(SetOscilStatusLoadFunc), @"Соединение не установлено", System.Drawing.SystemColors.ButtonFace);
	            Invoke(new SetOscilEnable(SetOscilEnableFunc), @"Соединение не установлено",  System.Drawing.SystemColors.ButtonFace);
            }
        }

        private delegate void SetOscilStatusLoad(string text, Color color);

	    private void SetOscilStatusLoadFunc(string text, Color color)
	    {
	        if (new_config.ToolTipText == text || new_config.BackColor == color)
            {
                return;
	        }
	        new_config.ToolTipText = text;
	        new_config.BackColor = color;
        }

	    private delegate void SetOscilEnable(string text, Color color);

	    private void SetOscilEnableFunc(string text, Color color)
	    {
	        if (ScopeConfig.ChannelCount == 0)
	        {
	            text = @"В системе отсутствует конфигурация";
	        }

            if (current_config.ToolTipText == text || current_config.BackColor == color)
	        {
	            return;
	        }
	        current_config.ToolTipText = text;
	        current_config.BackColor = color;
	    }

        private void UpdateStatusСonfig(bool dataOk, ushort[] paramRtu, object param)
        {
            switch (param.ToString())
            {
                case "OscilStatusLoad":
                    switch (paramRtu[0])
                    {
                        case 0:
                            Invoke(new SetOscilStatusLoad(SetOscilStatusLoadFunc), @"Конфигурация в системе остсутствует", System.Drawing.SystemColors.ButtonFace);
                            break;
                        case 1:
                            Invoke(new SetOscilStatusLoad(SetOscilStatusLoadFunc), @"Конфигурация успешно установлена", Color.LightGreen);
                            break;
                        case 2:
                            Invoke(new SetOscilStatusLoad(SetOscilStatusLoadFunc), @"Конфигурация незагружена", Color.LightCoral);
                            break;
                        case 3:
                            Invoke(new SetOscilStatusLoad(SetOscilStatusLoadFunc), @"Конфигурация загружается", Color.LightBlue);
                            break;
                    }
                    break;
                case "OscilEnable":
                    switch (paramRtu[0])
                    {
                        case 0:
                            Invoke(new SetOscilEnable(SetOscilEnableFunc), @"Осциллограффирование отключено", Color.LightCoral);
                            break;
                        case 1:
                            Invoke(new SetOscilEnable(SetOscilEnableFunc), @"Осциллограффирование включено, с перезаписью (без сохранения)", Color.LightGreen);
                            break;
                        case 2:
                            Invoke(new SetOscilEnable(SetOscilEnableFunc), @"Осциллограффирование включено, с перезаписью  (с сохранением)", Color.LightGreen);
                            break;
                        case 3:
                            Invoke(new SetOscilEnable(SetOscilEnableFunc), @"Осциллограффирование включено, без перезаписи (без сохранения)", Color.LightGreen);
                            break;
                        case 4:
                            Invoke(new SetOscilEnable(SetOscilEnableFunc), @"Осциллограффирование включено, без перезаписи (без сохранения)", Color.LightGreen);
                            break;
                    }
                    break;
            }
        }


        private void SetScopeStatus(int index)
		{
            switch (_oscilsStatus[index])
            {
                case 0x04:
                    {
                        if (_loadOscData)
                        {
                            MessageBox.Show("Уже производится загрузка осциллограммы.", @"Warring", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        ushort addr = (ushort)(ScopeSysType.OscilCmndAddr + 8 + index);
                        SerialPort.SetDataRTU(addr, null, RequestPriority.Normal, null, 0x05);
                        //Инициализируем скачивание осцллограммы
                        SerialPort.GetDataRTU(addr, 1, StartScopeLoad, index);
                        break;
                    }
                case 0x05:
                    {
                        MessageBoxResult dialogResult = MessageBox.Show("Осциллограмма может загружаться на другом устростве.\nСбросить статус загрузки?", @"Warring", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (dialogResult == MessageBoxResult.Yes)
                        {
                            UnsetScopeStatus(index);
                        }
                        break;
                    }

            }
        }

		private void StartScopeLoad(bool dataOk, ushort[] paramRtu, object param)
		{
			if (dataOk)
			{
				var index = Convert.ToInt32(param);
			    if (paramRtu[0] == 0x05)
			    {
			        //Начинаем загрузку
			        loadScopeToolStripLabel.Text = _oscilTitls[_loadOscNum];
			        loadDataProgressBar.Value = 0;
			        UpdateLoadDataProgressBarInvoke();
			        _downloadedData = new List<ushort[]>();
			        LoadOscDataRequest();
                }
				else
                {
					SetScopeStatus(index);
				}
			}
		}

		private void UnsetScopeStatus(int index)
		{
		    _loadOscilIndex = 0;
            _loadOscilTemp = 0;
            _loadOscData = false;
		    _loadOscDataStep = 0;
		    _countTemp = 0;
		    ushort addr = (ushort)(ScopeSysType.OscilCmndAddr + 8 + index);
		    SerialPort.SetDataRTU(addr, null, RequestPriority.Normal, null, 0x04);
        }

		private void ClearScopeStatus(int index)
		{
		    if (_oscilsStatus[index] != 0x05)
		    {
		        ushort addr = (ushort)(ScopeSysType.OscilCmndAddr + 8 + index);
		        SerialPort.SetDataRTU(addr, null, RequestPriority.Normal, null, 0x00);
            }
		    else
		    {
		        MessageBox.Show("Осциллограмма может загружаться на другом устростве.\nДля удаления осциилограммы сбросьте статус загрузки.", @"Warring", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

		//Ручной запуск
		private void ManStartRequest()
		{
			ushort[] uv = { 1 };

			SerialPort.SetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + 4), null, RequestPriority.Normal, null, uv);
		}


		private bool    _loadOscData;        //Флаг, что идет скачивание осцилограммы, все остальные запросы приостановлены
		private int     _loadOscDataStep;            //0 - загрузка loadOscilTemp
																			//1 - загрузка непосредственно тела

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
							_loadOscData = true;
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
									UpdateLoadDataProgressBarInvoke();
									UnsetScopeStatus(_loadOscNum);
								}
							}
						}
						else
						{
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

	    private void ConfigCheack()
		{
			//Отключить обновление информации об осциллограммах
			StopUpdate();
			_loadConfigStep = 0;
			SerialPort.UnsetPortBusy();
			LoadConfig();
		}

	    public void CheackConnect()
	    {
	        SerialPort.GetDataRTU((ushort) (ScopeSysType.OscilCmndAddr + 379), 1, CheackConnect, "CheackConnect");
	    }

	    private void CheackConnect(bool dataOk, ushort[] paramRtu, object param)
	    {
	        if (dataOk)
	        { 
	            ConfigCheack();
	            _connectToSystem = true;
	        }
	        else
	        {
	            MessageBox.Show("Соединение с системой не установлено", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
	            _connectToSystem = false;
	            SerialPort.Close();
            }
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
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 72 + 16 * _indexChannel), 16, LoadConfig, RequestPriority.Normal, 13);
					break;
				case 14:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 584 + _indexChannel), 1, LoadConfig, RequestPriority.Normal, 14);
					break;
				case 15:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 616 + 8 * _indexChannel), 8, LoadConfig, RequestPriority.Normal, 15);
					break;
				case 16:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 872 + 4 * _indexChannel), 4, LoadConfig, RequestPriority.Normal, 16);
					break;
				case 17:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1000 + _indexChannel), 1, LoadConfig, RequestPriority.Normal, 17);
					break;
				case 18:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1032), 16, LoadConfig, RequestPriority.Normal, 18);
					break;
				case 19:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1048), 8, LoadConfig, RequestPriority.Normal, 19);
					break;
				case 20:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1056), 4, LoadConfig, RequestPriority.Normal, 20);
					break;
				case 21:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1060), 4, LoadConfig, RequestPriority.Normal, 21);
					break;
				case 22:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1064), 4, LoadConfig, RequestPriority.Normal, 22);
					break;
				case 23:
					SerialPort.GetDataRTU((ushort)(ScopeSysType.ConfigurationAddr + 1068), 4, LoadConfig, RequestPriority.Normal, 23);
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
								//_loadConfigStep = 0;
								//_buttonsAlreadyCreated = false;

								EndLoadConfig(false);

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

							EndLoadConfig(true);

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

		private void EndLoadConfig(bool foolStatus)
		{

			_loadConfigStep = 0;

			ScopeConfig.ChangeScopeConfig = false;
			_updateTimer = true;

			if (ScopeConfig.SendNewConfig)
			{
				_ucScopeSetup?.StatusConfigToSystemStrLabel();
			}
			_ucSettings?.UpdateConfig();
			if (foolStatus)
			{
				CreateStatusButtons();
			}
		}
	}
}