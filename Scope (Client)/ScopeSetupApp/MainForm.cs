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

namespace ScopeSetupApp
{
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
        

        private ModBusUnit _modBusUnit;
        private bool _buttonsAlreadyCreated = true;
        private bool _configLoaded;
        private bool _lineBusy;
        private int _loadConfigStep;
        private int _requestStep;
        private int _loadTimeStampStep;

        private void LoadWindowSize(string comPortXmlName, out int newHeight, out int newWidth)
        {
            XmlNodeList xmls;
            XmlNode xmlNode;

            var doc = new XmlDocument();
            try
            {
                doc.Load(comPortXmlName);
            }
            catch
            {
                MessageBox.Show(@"Неизв. формат удалось открыть файл с настройками!", @"Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }

            xmls = doc.GetElementsByTagName(@"MainWindow");

            if (xmls.Count != 1)
            {
                MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    add.Attributes["Height"].Value = this.Height.ToString();
                    add.Attributes["Width"].Value = this.Width.ToString();
                }
                doc.Save(comPortXmlName);
            }
            catch
            {
            }
        }

        public MainForm(string[] agrs)
        {
            InitializeComponent();
            try
            {
                ScopeSysType.InitScopeSysType();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            if (agrs.Length > 0)
            {
                if (agrs[0] == "a" || agrs[0] == "A") ConfigScopeButton.Visible = true; 
            }

            _modBusUnit = new ModBusUnit();
            _modBusUnit.RequestFinished += EndRequest;

            ModBusUnits.ScopeSetupModbusUnit = new ModBusUnit();
            ModBusClient.InitModBusEvent();

            int i1, i2;
            LoadWindowSize("prgSettings.xml", out i1, out i2);
            Size size = new Size(i2, i1);
            Size = size;
        }

        private delegate void SetStringDelegate(string parameter);

        private void SetTimeLabel(string newTime)
        {
            try
            {
                label1.Text = newTime;
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
                    _scopeSetupForm = new ScopeSetupForm();
                    _scopeSetupForm.Show();
                }
            }
            else
            {
                _scopeSetupForm = new ScopeSetupForm();
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
                _scopeConfigForm = new ScopeConfigForm();
                _scopeConfigForm.Show();
                }

            }
            else
            {
                _scopeConfigForm = new ScopeConfigForm();
                _scopeConfigForm.Show();
            };
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

        private void LoadComPortSettings(string comPortXmlName, out int newPar, out int newSpeed, out int newPortIndex, out int newAddr)
        {
            XmlNodeList xmls;
            XmlNode xmlNode;

            var doc = new XmlDocument();
            try { doc.Load(comPortXmlName); }
            catch
            {
                MessageBox.Show(@"Не удалось открыть файл с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            xmls = doc.GetElementsByTagName("ComPort");

            if (xmls.Count != 1)
            {
                MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(@"Ошибки в файле с настройками!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
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
                _modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr), 2);
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
                _modBusUnit.GetData((ushort)(ScopeSysType.ConfigurationAddr), 32);
                return;
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
                            ScopeConfig.OscilSize = (uint)((int)_modBusUnit.modBusData.ReadData[1] << 16);
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
                            ScopeConfig.OscilAllSize = (uint)((int)_modBusUnit.modBusData.ReadData[1] << 16);
                            ScopeConfig.OscilAllSize += (uint)((int)_modBusUnit.modBusData.ReadData[0]);
                            _loadConfigStep = 8;
                            LoadConfig();
                        } break;

                    case 8:                     //Размер одной выборки
                        {
       ;                     ScopeConfig.SampleSize = _modBusUnit.modBusData.ReadData[0];
                            _loadConfigStep = 9;
                            LoadConfig();
                        } break;
                    case 9:                     //Размер всей памяти 
                        {
                            ScopeConfig.OscilHistCount = (uint)((int)_modBusUnit.modBusData.ReadData[1] << 16);
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
                            _loadConfigStep = 0;
                            _configLoaded = true;
                            _buttonsAlreadyCreated = false;
                        } break;
                }
                return;
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
                _loadOscDataSubStep = 0;
                _loadOscilIndex = 0;
                _requestStep = 0;
                _clearOscFlag = false;
                _loadOscData = false;
                _lineBusy = false;
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
                timer3.Enabled = true;
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
                _statusButtons[i].Click += new EventHandler(LoadOscBtnClick);
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
                    _oscilTitls[i] = @"Осциллограмма №" + (i + 1).ToString() + @".";
                    _statusButtons[i].Text = @"№" + (i + 1).ToString() + "\n" + @"Пусто";
                
                }
                else if (_oscilsStatus[i] >= 4) { _statusButtons[i].BackColor = Color.LightSteelBlue; ; _statusButtons[i].Enabled = true; }
            
                else if (_oscilsStatus[i] == 3)
                {
                    _statusButtons[i].FlatStyle = FlatStyle.Standard;
                    _statusButtons[i].BackColor = Color.Lavender; 
                    _statusButtons[i].Enabled = true;
                    _oscilTitls[i] = @"Осциллограмма №" + (i + 1).ToString() + @".";
                    _statusButtons[i].Text = @"№" + (i + 1).ToString() + "\n" + @"Идет запись";         
                }
                else if (_oscilsStatus[i] == 1)
                {
                    _statusButtons[i].BackColor = Color.GhostWhite;
                    _statusButtons[i].Enabled = true;
                    _oscilTitls[i] = @"Осциллограмма №" + (i + 1).ToString() + @".";
                    _statusButtons[i].Text = @"№" + (i + 1).ToString() + "\n" + @"Запись предыстории";
                }
                else {
                    _statusButtons[i].BackColor = Color.AliceBlue;
                    _statusButtons[i].Enabled = true;
                    _oscilTitls[i] = @"Осциллограмма №" + (i + 1).ToString()+@".";
                    _statusButtons[i].Text = @"№" + (i + 1).ToString() + "\n" + @"Готова к записи";
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
            _statusButtons[_loadTimeStampStep].Text = @"№" + (_loadTimeStampStep + 1).ToString() + "\n" + str1 + "\n" + str2;
            _oscilTitls[_loadTimeStampStep] = @"Осциллограмма №" + (_loadTimeStampStep + 1).ToString() + "\n" + str1 + "\n" + str2;
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
                if (ScopeConfig.Coincides)
                {
                    _loadOscNum = (int) ((Button) sender).Tag;
                    _initLoadOscilFlag = true;
                    _oscilStartTemp = ((uint) _loadOscNum*(ScopeConfig.OscilSize >> 1)); //Начало осциллограммы 
                    _oscilEndTemp = (((uint) _loadOscNum + 1)*(ScopeConfig.OscilSize >> 1)); //Конец осциллограммы 
                }
                else MessageBox.Show(@"Конфигурация в системе не совпадает", @"Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private ushort[] _writeArr = new ushort[4];

        private void LoadOscDataRequest()
        {
            timer3.Enabled = false;
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
                        if (_loadOscDataSubStep == 0)
                        {
                            
                            uint oscilLoadTemp = (CalcOscilLoadTemp(_loadOscNum));
                           
                            _writeArr[0] = 0x0001;
                            _writeArr[1] = Convert.ToUInt16((oscilLoadTemp << 16) >> 16); 
                            _writeArr[2] = Convert.ToUInt16(oscilLoadTemp >> 16);

                            _modBusUnit.SetData((ushort)(ScopeSysType.OscilCmndAddr + 5), 3, _writeArr);
                        }
                        else
                        {
                            _modBusUnit.GetData((ushort)(ScopeSysType.OscilCmndAddr + 40 + (_loadOscDataSubStep - 1) * 8), 8);
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
                            _startLoadSample = (uint)((int)_modBusUnit.modBusData.ReadData[1] << 16);
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
                            switch (_loadOscDataSubStep)
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
                                            _loadParamPart[i + (_loadOscDataSubStep - 1) * 8] = _modBusUnit.modBusData.ReadData[i];
                                        }
                                    }break;
                            }
                            _loadOscDataSubStep++;
                            if (_loadOscDataSubStep == 5) 
                            {
                                _downloadedData.Add(new ushort[32]);
                                for (int i = 0; i < 32; i++)
                                {
                                    _downloadedData[_downloadedData.Count - 1][i] = _loadParamPart[i];
                                }

                                _loadOscDataSubStep = 0;
                                _loadOscilIndex = (2*_countTemp*1000)/ScopeConfig.OscilSize;
                                
                                UpdateLoadDataProgressBarInvoke();

                                if (_countTemp >= (ScopeConfig.OscilSize >> 1))
                                {
                                    timer3.Enabled = false;     

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

        private bool    _loadOscData         = false;        //Флаг, что идет скачивание осцилограммы, все остальные запросы приостановлены
        private int     _loadOscDataStep     = 0;            //0 - загрузка loadOscilTemp
                                                    //1 - загрузка непосредственно тела

        private int     _loadOscDataSubStep  = 0;            //0 - расчет MemoryAddr
                                                    //1 - получение порции данных
                                                    //2 - 
                                                    //3 -
                                                    //4 -
        private ushort[] _loadParamPart = new ushort[32];

        private int _loadOscNum;
        private uint _loadOscilIndex;
        private uint _loadOscilTemp;
        private uint _oscilStartTemp;
        private uint _oscilEndTemp;
        private uint _countTemp;
        private uint _startLoadSample;

        private uint CalcOscilLoadTemp(int nowLoadOscNum)
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
            string str = "";
            int i = 0;
            str = " " + "\t";
            for (i = 0; i < ScopeConfig.ChannelCount; i++)
            {
                //Если параметр в списке известных, то пишем его в файл
                if (ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count)
                {
                    str = str + ScopeSysType.ChannelNames[ScopeConfig.OscilParams[i]] + "\t";
                }
            }
            return str;
        }

        private string FileReserveLine()
        {
            string str = "";
            int i = 0;
            str = " " + "\t";
            for (i = 0; i < ScopeConfig.ChannelCount; i++)
            {
                //Если параметр в списке известных, то пишем его в файл
                if (ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count)
                {
                    str = str + "0" + "\t";
                }
            }
            return str;
        }


        private int _count64, _count32 , _count16;

        private string FileParamLine(ushort[] paramLine, int lineNum)
        {
            string str = "";
            ulong ulTemp = 0;
            int i;
           // ChFormats();
            str = lineNum.ToString() + "\t";
            for (i = 0, _count64 = 0, _count32 = 0, _count16 = 0; i < ScopeConfig.ChannelCount; i++)
            {
                //Если параметр в списке известных, то пишем его в файл
                if (ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count)
                {
                    ulTemp = ParseArr(i, paramLine);
                    str = str + AdvanceConvert.HexToFormat(ulTemp, (byte)ScopeSysType.ChannelFormats[ScopeConfig.OscilParams[i]]) + "\t";
                }
            }
            return str;
        }
        #endregion

        private ulong ParseArr(int i, ushort[] paramLine)
        {
            ulong ulTemp = 0;
            if ((ScopeSysType.ChannelFormats[ScopeConfig.OscilParams[i]] >> 8) == 3)
            {
               ulTemp = 0;
               ulTemp += (ulong)(paramLine[_count64 + 0]) << 8 * 2;
               ulTemp += (ulong)(paramLine[_count64 + 1]) << 8 * 3;
               ulTemp += (ulong)(paramLine[_count64 + 2]) << 8 * 0;
               ulTemp += (ulong)(paramLine[_count64 + 3]) << 8 * 1;
               _count64 += 4;
            }
            if ((ScopeSysType.ChannelFormats[ScopeConfig.OscilParams[i]] >> 8) == 2)
            {
                ulTemp = 0;
                ulTemp += (ulong)(paramLine[_count64 + _count32 + 0]) << 8 * 0;
                ulTemp += (ulong)(paramLine[_count64 + _count32 + 1]) << 8 * 1;
                _count32 += 2; 
            }
            if ((ScopeSysType.ChannelFormats[ScopeConfig.OscilParams[i]] >> 8) == 1)
            {
                ulTemp = (ulong)(paramLine[_count64 + _count32 + _count16]);
                _count16 += 1;
            }
            return ulTemp;
        }

        private List<ushort[]> InitParamsLines()
        {
            List<ushort[]> paramsLines = new List<ushort[]>();
            List<ushort[]> paramsSortLines = new List<ushort[]>();
            int k = 0;
            int j = 0;
            int l = 0;
            for (int i = 0; i < _downloadedData.Count; i++)
            {
                while (j < 32)
                {
                    if (k == 0) paramsLines.Add(new ushort[ScopeConfig.SampleSize >> 1]);
                    while (k < (ScopeConfig.SampleSize >> 1) && j < 32)
                    { 
                        paramsLines[paramsLines.Count-1][k] = _downloadedData[i][j];
                        k++;
                        j++;
                    }
                    if (k == (ScopeConfig.SampleSize >> 1)) k = 0;
                }
                j = 0;
            }
            paramsLines.RemoveAt(paramsLines.Count-1);
            //Формирую список начиная с предыстории 
            for(int i = 0; i < paramsLines.Count; i++)
            {
                if ((i + (int)_startLoadSample + 1) >= paramsLines.Count)
                {
                    k = 0;
                    paramsSortLines.Add(new ushort[ScopeConfig.SampleSize >> 1]);
                    paramsSortLines[i] = paramsLines[l];
                    l++;
                }
                else
                {
                    k = 0;
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
            string str = (lineNum + 1).ToString() + ",";
            for (i = 0; i < ScopeConfig.ChannelCount; i++)
            {
                //Если параметр в списке известных, то пишем его в файл
                if (ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count)
                {
                    if (ScopeSysType.ChannelTypeAd[i] == 0)
                    {
                        ulTemp = ParseArr(i, paramLine);
                        str1 = AdvanceConvert.HexToFormat(ulTemp, (byte)ScopeSysType.ChannelFormats[ScopeConfig.OscilParams[i]]);
                        str1 = str1.Replace(",", ".");
                        str = str + "," + str1;
                    }


                }
            }
            for (i = 0; i < ScopeConfig.ChannelCount; i++)
            {
                //Если параметр в списке известных, то пишем его в файл
                if (ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count)
                {
                    if (ScopeSysType.ChannelTypeAd[i] == 1)
                    {
                        ulTemp = ParseArr(i, paramLine);
                        str1 = AdvanceConvert.HexToFormat(ulTemp, (byte)ScopeSysType.ChannelFormats[ScopeConfig.OscilParams[i]]);
                        str1 = str1.Replace(",", ".");
                        str = str + "," + str1;
                    }
                }
            }
            return str;
        }

        private float CalculA(int num, int resolution)
        {
            float a = (float)(ScopeSysType.ChannelMax[ScopeConfig.OscilParams[num]] - ScopeSysType.ChannelMin[ScopeConfig.OscilParams[num]]) / (float) resolution;
            return a;
        }

        private float CalculB(int num, int resolution)
        {
            int j, all = ScopeSysType.ChannelMax[ScopeConfig.OscilParams[num]] - ScopeSysType.ChannelMin[ScopeConfig.OscilParams[num]];
            for (j = 1; all / (int)Math.Pow(10, j) != 0; j++) ;
            float a = (float)(all) / (float)resolution;
            float ax = (float)Math.Pow(10, j) * a;
            float b = (0 - ax);
           
            return b;
        }

        private string Line1( int filterIndex)
        {
            string str = "";
            string stationName = ScopeSysType.StationName;
            string recDevId = ScopeSysType.RecordingDevice;
            string revYear = "";
            if (filterIndex == 2) revYear = "1999";
            if (filterIndex == 3) revYear = "2013";
            str = stationName + "," + recDevId + "," + revYear;
            return str;
        }

        private string Line2()
        {
            string str = "";
            int nA = 0 , nD = 0;
            for (int i = 0; i < ScopeConfig.ChannelCount; i++)
            {
                //Если параметр в списке известных, то пишем его в файл
                if (ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count)
                {
                    if (ScopeSysType.ChannelTypeAd[i] == 0) nA += 1;
                    if (ScopeSysType.ChannelTypeAd[i] == 1) nD += 1;
                }
            }
            int tt = nA + nD;
            str = tt + "," + nA + "A," + nD + "D";
            return str;
        }

        private string Line3(int num ,int nA)
        {
            string chId = ScopeSysType.ChannelNames[ScopeConfig.OscilParams[num]];
            string ph = ScopeSysType.ChannelPhase[ScopeConfig.OscilParams[num]];
            string ccbm = ScopeSysType.ChannelCcbm[ScopeConfig.OscilParams[num]];
            string uu = ScopeSysType.ChannelDimension[ScopeConfig.OscilParams[num]];
            //string a = Convert.ToString(CalculA(Num, 4096));
            //a = a.Replace(",", ".");
            //string b = Convert.ToString(CalculB(Num, 4096));
            //b = b.Replace(",", ".");
            string a = "1";
            string b = "0";
            int skew = 0;
            int min = ScopeSysType.ChannelMin[ScopeConfig.OscilParams[num]];
            int max = ScopeSysType.ChannelMax[ScopeConfig.OscilParams[num]];
            int primary = 1; 
            int secondary = 1;
            string ps = "P";

            string str = nA + "," + chId + "," + ph + "," + ccbm + "," + uu + "," + a + "," + b + "," + skew + "," +
                         min + "," + max + "," + primary + "," + secondary + "," + ps;

            return str;
        }

        private string Line4(int num, int nD)
        {
            string str = "";

            string chId = ScopeSysType.ChannelNames[ScopeConfig.OscilParams[num]];
            string ph = ScopeSysType.ChannelPhase[ScopeConfig.OscilParams[num]];
            string ccbm = ScopeSysType.ChannelCcbm[ScopeConfig.OscilParams[num]];
            int y = 0;
            
            str = nD + "," + chId + "," + ph + "," + ccbm + "," + y ;

            return str;
        }

        private string Line5()
        {
            return ScopeSysType.OscilNominalFrequency.ToString();
        }

        private string Line6()
        {
            string nrates = "1";
            return nrates;
        }

        private string Line7()
        {
            string str = "";
            string samp = Convert.ToString(ScopeConfig.SampleRate / ScopeConfig.FreqCount);
            samp = samp.Replace(",", ".");
            string endsamp = InitParamsLines().Count.ToString();
            str = samp + "," + endsamp;
            return str;
        }

        private string Line8(int numOsc)
        {
           // string str;
            double milsec = 1000*(double)ScopeConfig.OscilHistCount/ScopeConfig.SampleRate;
            DateTime dateTemp = _date[numOsc].AddMilliseconds(-milsec);
            return dateTemp.ToString("dd'/'MM'/'yyyy,HH:mm:ss.fff000");
        }

        private string Line9(int numOsc)
        {
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
            string timecode = ScopeSysType.TimeCode;
            string localcode = ScopeSysType.LocalCode;
            return timecode + "," + localcode;
        }

        private string Line13()
        {
            string  tmqCode = ScopeSysType.TmqCode;
            string  leapsec = ScopeSysType.Leapsec;
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
                    MessageBox.Show(@"Ошибка при создании файла!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    DateTime dateTemp = _date[_createFileNum];
                    sw.WriteLine(dateTemp.ToString("dd'/'MM'/'yyyy HH:mm:ss.fff000"));
                    sw.WriteLine(Convert.ToString(ScopeConfig.SampleRate / ScopeConfig.FreqCount));
                    sw.WriteLine(ScopeConfig.OscilHistCount);
                    sw.WriteLine(FileHeaderLine());

                    List<ushort[]> lu = InitParamsLines();
                    for (int i = 0; i < lu.Count; i++)
                    {
                        sw.WriteLine(FileParamLine(lu[i], i));
                    }
                }
                catch
                {
                    MessageBox.Show(@"Ошибка при записи в файл!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if(ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count) 
                        {
                            if (ScopeSysType.ChannelTypeAd[i] == 0) {sw.WriteLine(Line3(i, j+1)); j++; }
                        }
                     }
                     for (int i = 0, j = 0; i < ScopeConfig.ChannelCount; i++)
                     {
                         if (ScopeConfig.OscilParams[i] < ScopeSysType.ChannelNames.Count)
                         {
                             if (ScopeSysType.ChannelTypeAd[i] == 1) { sw.WriteLine(Line4(i, j + 1)); j++; }
                         }
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
                     MessageBox.Show(@"Ошибка при записи в файл!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _loadOscDataSubStep = 0;
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
        private void CreateFileInvoke()
        {
            try
            {
                Invoke(new NoParamDelegate(CreateFile), null);
            }
            // ReSharper disable once EmptyGeneralCatchClause
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

        private void timer3_Tick(object sender, EventArgs e)
        {
           timer3.Enabled = false;
          //  timer3.Enabled = true;
           _loadOscDataSubStep = 0;
           LoadOscDataResponce();
         //  modBusUnit.SetData((ushort)(ScopeSysType.OscilCmndAddr + 5), 3, writeArr);
        }

    }
}
