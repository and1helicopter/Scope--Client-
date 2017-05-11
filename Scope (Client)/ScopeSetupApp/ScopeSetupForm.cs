using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using ModBusLibrary;
using System.Xml;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ScopeSetupApp
{
    public partial class ScopeSetupForm : Form
    {

        private ushort _nowHystory = 1;             //Предыстория 
        private ushort _nowScopeCount = 1;          //Количество осциллограмм
        private ushort _nowMaxChannelCount;    //Количество каналов
        private ushort _nowOscFreq = 1;             //Делитель     
        private uint _oscilAllSize = 1;             //
        private bool _changeConfig;

        private readonly object[] _format = 
        {
            "0 - Percent",
            "1 - uint16",
            "2 - int16",
            "3 - Freq standart",
            "4 - 8.8",
            "5 - 0.16",
            "6 - Slide",
            "7 - Digits",
            "8 - RegulMode",
            "9 - AVR type",
            "10 - Int/10",
            "11 - Hex",
            "12 - *0.135 (Uf)",
            "13 - FreqNew",
            "14 - Current trans",
            "15 - trans alarm",
            "16 - int/8",
            "17 - uint/1000",
            "18 - percent/4",
            "19 - FreqNew2",
            "20 - Percent upp",
            "21 - Freq UPTF",
            "22 - 16.16",
            "23 - 32.32"
        };

        private readonly object[] _sizeFormat =
        {
            "16",
            "32",
            "64",
        };

        #region

        private readonly List<ListViewItem> _channelnameListViewItems = new List<ListViewItem>();
        private readonly List<ListViewGroup> _groupListViewGroup = new List<ListViewGroup>();
        private readonly List<String> _groupString = new List<String>();

        private void InitTable(string[] agrs)
        {
            if (agrs.Length > 0)
            {
                if (agrs[0] == "a" || agrs[0] == "A")
                {
                    ColumnHeader nameColumnHeader = new ColumnHeader
                    {
                        Name = "name_columnHeader",
                        Text = @"Название канала",
                        Width = 270,
                        TextAlign = HorizontalAlignment.Left,
                        DisplayIndex = 0,
                        Tag = 1
                    };

                    ColumnHeader addrColumnHeader = new ColumnHeader
                    {
                        Name = "name_columnHeader",
                        Text = @"Адрес",
                        Width = 270,
                        TextAlign = HorizontalAlignment.Left,
                        DisplayIndex = 0,
                        Tag = 2
                    };

                    ColumnHeader formatColumnHeader = new ColumnHeader
                    {
                        Name = "name_columnHeader",
                        Text = @"Формат канала",
                        Width = 270,
                        TextAlign = HorizontalAlignment.Left,
                        DisplayIndex = 0,
                        Tag = 3
                    };

                    listView1.Columns.Add(nameColumnHeader);
                    listView1.Columns.Add(addrColumnHeader);
                    listView1.Columns.Add(formatColumnHeader);
                }
            }
            else
            {
                ColumnHeader nameColumnHeader = new ColumnHeader
                {
                    Name = "name_columnHeader",
                    Text = @"Название канала",
                    Width = 270,
                    TextAlign = HorizontalAlignment.Left,
                    DisplayIndex = 0,
                    Tag = 1
                };

                listView1.Columns.Add(nameColumnHeader);
            }

            CommentRichTextBox.Text = ScopeSysType.OscilComment;
            foreach (var item in ScopeSysType.ScopeItem)
            {
                if (!_groupString.Contains(item.ChannelGroupNames))
                {
                    _groupString.Add(item.ChannelGroupNames);
                    _groupListViewGroup.Add(new ListViewGroup(item.ChannelGroupNames, HorizontalAlignment.Center));
                    listView1.Groups.Add(_groupListViewGroup[_groupListViewGroup.Count - 1]);
                }
            }

            // ReSharper disable once UnusedVariable
            foreach (var item in ScopeSysType.ScopeItem)
            {
                _channelnameListViewItems.Add(new ListViewItem());
                int i = _channelnameListViewItems.Count - 1;
                _channelnameListViewItems[i].Text = item.ChannelNames;
                _channelnameListViewItems[i].SubItems.Add("0x" + item.ChannelAddrs.ToString("X4"));
                _channelnameListViewItems[i].SubItems.Add(
                    Convert.ToString(_sizeFormat[item.ChannelformatNumeric]) + "b   " +
                    Convert.ToString(_format[item.ChannelFormats]));
                _channelnameListViewItems[i].Checked = false;
                foreach (var itemGroup in _groupListViewGroup)
                {
                    if (item.ChannelGroupNames == itemGroup.Header)
                    {
                        _channelnameListViewItems[i].Group = itemGroup;
                        break;
                    }
                }

                listView1.Items.Add(_channelnameListViewItems[i]);
            }

            foreach (var item in _groupListViewGroup)
            {
                if (item.Header == "")
                {
                    item.Header = @"Несгруппированные параметры";
                    break;
                }
            }
        }

        void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }

        private bool _resizing;

        private void ListView_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!_resizing)
            {
                // Set the resizing flag
                _resizing = true;

                ListView listView = sender as ListView;
                if (listView != null)
                {
                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (int i = 0; i < listView.Columns.Count; i++)
                    {
                        listView.Columns[i].Width = (int)((double)1 / listView.Columns.Count * listView.ClientRectangle.Width);
                    }
                }
            }

            // Clear the resizing flag
            _resizing = false;

            SetDoubleBuffered(listView1, true);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.BackColor = e.Item.Checked ? Color.LightSteelBlue : SystemColors.ButtonHighlight;

            _nowMaxChannelCount = (ushort)_channelnameListViewItems.Count(item => item.BackColor == Color.LightSteelBlue);
            radioButton.Text = _nowMaxChannelCount.ToString();
        }

        #endregion

        public ScopeSetupForm(string[] agrs)
        {
            InitializeComponent();
            
            ConfigToSystem();
            InitTable(agrs);

            // ReSharper disable once RedundantDelegateCreation
            ModBusUnits.ScopeSetupModbusUnit.RequestFinished += new EventHandler(EndRequest);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (ModBusClient.ModBusOpened == false || ScopeConfig.ConnectMcu == false)
            {
                reloadButton.Enabled = false;
                writeToSystemBtn.Enabled = false;
            }
            else
            {
                reloadButton.Enabled = true;
                writeToSystemBtn.Enabled = true;
            }

            DelayOscil();

            StatusDownloadConfig.Visible = ScopeConfig.ConnectMcu;
            StatusDownloadConfigToSystem();

            if (_changeConfig)
            {
                MessageAnswer();
                _changeConfig = false;
            }

            timer1.Enabled = true;
        }

        //****************************************************************************//
        //****************************************************************************//
        //****************************************************************************//
        //Обработка полей формы
        #region  
        //Количество осциллограмм
        private void chCountRadioButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)  // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void chCountRadioButton_TextChanged(object sender, EventArgs e)
        {
            if (chCountRadioButton.Text != "" && chCountRadioButton.Text != @"-")
            {
                _nowScopeCount = Convert.ToUInt16(chCountRadioButton.Text);
                if (_nowScopeCount < 1 || _nowScopeCount > 32)
                {
                    MessageBox.Show(@"Ошибка в поле Количество осциллограмм");
                    chCountRadioButton.Clear();
                }
            }
        }

        //Предыстория 

        private void hystoryRadioButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)  // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }        
        }

        private void hystoryRadioButton_TextChanged(object sender, EventArgs e)
        {
            if (hystoryRadioButton.Text != "" && hystoryRadioButton.Text != @"-")
            {
                _nowHystory = Convert.ToUInt16(hystoryRadioButton.Text);
                if (_nowHystory < 1 || _nowHystory > 99)
                {
                    MessageBox.Show(@"Ошибка в поле Предыстория");
                    hystoryRadioButton.Clear();
                }
            }
        }

        //Делитель
        private void oscFreqRadioButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)  // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void oscFreqRadioButton_TextChanged(object sender, EventArgs e)
        {
            if (oscFreqRadioButton.Text != "" && oscFreqRadioButton.Text != @"-")
            {
                _nowOscFreq = Convert.ToUInt16(oscFreqRadioButton.Text);
                if (_nowOscFreq < 1 || _nowOscFreq > 1000)
                {
                    MessageBox.Show(@"Ошибка в поле Предыстория");
                    oscFreqRadioButton.Clear();
                }
            }
        }

        //Длительность осциллограммы:
        private void DelayOscil()
        {
            if (_nowScopeCount != 0)
            {
                if (ModBusClient.ModBusOpened && ScopeConfig.ConnectMcu)
                {
                    double sampleCount = (double)OscilSize(_oscilAllSize, false) / OscilSize(_oscilAllSize, true);
                    double freq = (double)ScopeConfig.SampleRate / _nowOscFreq;
                    double timeSec = ((uint)sampleCount) / freq;
                    if (Math.Abs(timeSec) < 0.001)
                    {
                        DelayOsc.Text = @"Длительность: " + @"-----";
                    }
                    else
                    {
                        DelayOsc.Text = @"Длительность: " + timeSec.ToString("0.000") + @" сек";
                    }
                    DelayOsc.Visible = true;
                }
                else
                {
                    double sampleCount = (double) OscilSize(_oscilAllSize, false) / OscilSize(_oscilAllSize, true);
                    double freq = (double)ScopeSysType.OscilSampleRate / _nowOscFreq;
                    double timeSec = ((uint)sampleCount) / freq;
                    if (Math.Abs(timeSec) < 0.001)
                    {
                        DelayOsc.Text = @"Длительность: " + @"-----";
                    }
                    else
                    {
                        DelayOsc.Text = @"Длительность: " + timeSec.ToString("0.000") + @" сек";
                    }
                    DelayOsc.Visible = true;
                }
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            DelayOscil();
        }
        #endregion
   
        //**************ИЗМЕНЕНИЕ КОНФИГУРАЦИИ ОСЦИЛЛОГРАФА *********************************************//
        //***********************************************************************************************//
        //***********************************************************************************************//
        private ushort[] _newOscillConfig = new ushort[40];
        private ushort[] _oscillConfig = new ushort[1280];
        private readonly int[] _channelSeries = new int[32];

        private int _writeConfigStep;
        private ushort _writeStep;

        //Конфигурирование осциллограммы 
        private List<ushort> ChannelFormats()
        {
            int j = 0;
            List<ushort> l = new List<ushort>();
            for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked && ScopeSysType.ScopeItem[i].ChannelformatNumeric == 2)
                {
                    _channelSeries[j++] = i;
                    l.Add(Convert.ToUInt16((Convert.ToInt32(ScopeSysType.ScopeItem[i].ChannelformatNumeric + 1) << 8) + ScopeSysType.ScopeItem[i].ChannelFormats));
                }
            }
            for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked && ScopeSysType.ScopeItem[i].ChannelformatNumeric == 1)
                {
                    _channelSeries[j++] = i;
                    l.Add(Convert.ToUInt16(Convert.ToInt32((ScopeSysType.ScopeItem[i].ChannelformatNumeric + 1) << 8) + ScopeSysType.ScopeItem[i].ChannelFormats));
                }
            }
            for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked && ScopeSysType.ScopeItem[i].ChannelformatNumeric == 0)
                {
                    _channelSeries[j++] = i;
                    l.Add(Convert.ToUInt16(Convert.ToInt32((ScopeSysType.ScopeItem[i].ChannelformatNumeric + 1) << 8 )+ ScopeSysType.ScopeItem[i].ChannelFormats));
                }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }
 
        // Channel Addrs
        private List<ushort> ChannelAddrs()
        {
            List<ushort> l = new List<ushort>();
            for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked) { l.Add(ScopeSysType.ScopeItem[_channelSeries[j++]].ChannelAddrs); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        // OscilSize
        private uint OscilSize(uint allSize, bool wr)
        {
            uint count64 = 0, count32 = 0, count16 = 0;
            for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].BackColor == Color.LightSteelBlue && ScopeSysType.ScopeItem[i].ChannelformatNumeric == 2) { count64++; }
            }
            for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].BackColor == Color.LightSteelBlue && ScopeSysType.ScopeItem[i].ChannelformatNumeric == 1) { count32++; }
            }
            for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].BackColor == Color.LightSteelBlue && ScopeSysType.ScopeItem[i].ChannelformatNumeric == 0) { count16++; }
            }

            uint sampleSize = count64 * 8 + count32 * 4 + count16 * 2;
            if ((count64 != 0 || count32 != 0) && count16 % 2 != 0) { sampleSize += 2; } // Выравнивание на 4 байта
            if (sampleSize == 0)
            {
                return 0;
            }

            uint oscS = ScopeConfig.ConnectMcu ? Convert.ToUInt32((double)ScopeConfig.OscilAllSize / Convert.ToUInt32(_nowScopeCount) * ((double)sizeOcsil_trackBar.Value / 100)) : Convert.ToUInt32((double)allSize *1024 / Convert.ToUInt32(_nowScopeCount) * ((double)sizeOcsil_trackBar.Value / 100));

            while (oscS % 64 != 0 || oscS % sampleSize != 0)   // 
            { 		
                oscS--;
            }

            switch (wr)
            {
                case false:
                    return oscS;
                case true:
                    return sampleSize;
            }
            return 0;
        }

        //OscilEnable
        private ushort OscilEnable()
        {
            ushort status = 0;
            if (!enaScopeCheckBox.Checked) status = 0;                               //Осциллограффирование отключено 
            if (enaScopeCheckBox.Checked && !checkBox1.Checked) status = 1;          //Осциллограффирование включено но без сокранения на карту пмяти
            if (enaScopeCheckBox.Checked && checkBox1.Checked) status = 2;           //Осциллограффирование включено с сохранением на карту пмяти
            if (enaScopeCheckBox.Checked && !checkBox1.Checked && checkBox3.Checked) status = 3;          //Осциллограффирование включено но без сокранения на карту пмяти, перезаписью старых осциллограмм
            if (enaScopeCheckBox.Checked && checkBox1.Checked && checkBox3.Checked) status = 4;           //Осциллограффирование включено с сохранением на карту пмяти, перезаписью старых осциллограмм
            return status;
        }

        // Channel Names
        private List<string> ChNames()
        {
            List<string> chName = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked) { chName.Add(ScopeSysType.ScopeItem[_channelSeries[j++]].ChannelNames); }
            }

            if (chName.Count > _nowMaxChannelCount) { chName.Clear(); }

            return chName;
        }
       //////////////////////////////////////////
       //For Comtrade 
        private List<string> ChPhase()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked) { l.Add(ScopeSysType.ScopeItem[_channelSeries[j++]].ChannelPhase); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<string> ChCcbm()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked) { l.Add(ScopeSysType.ScopeItem[_channelSeries[j++]].ChannelCcbm); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<string> ChDemension()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked) { l.Add(ScopeSysType.ScopeItem[_channelSeries[j++]].ChannelDimension); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<ushort> ChTypeAd()
        {
            List<ushort> l = new List<ushort>();
            for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
            {
                if (_channelnameListViewItems[i].Checked) { l.Add(ScopeSysType.ScopeItem[_channelSeries[j++]].ChannelTypeAd); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }


        private void CalcNewOscillConfig(ushort writeStep)  
        {
            _newOscillConfig = new ushort[40];

            _newOscillConfig[0] = 1; 
            _newOscillConfig[1] = writeStep;
            for (int i = 0; i < 32; i++)
            {           
                _newOscillConfig[2 + i] = _oscillConfig[i + 32*writeStep];
            }
            _newOscillConfig[34] = 1; 
        }

        //Конфигурирование параметрв осциллограммы 
        private void CalcOscillConfig()  
        {
            _oscillConfig = new ushort[1280];

            List<ushort> chFormats = ChannelFormats();          //Идентификатор формата данных в каналах

            for (int i = 0; i < 32; i++)
            {
                if (i < chFormats.Count) { _oscillConfig[i] = chFormats[i]; }
                else { _oscillConfig[i] = 0; }
            }           
            
            List<ushort> chAddrs = ChannelAddrs();          //Адреса
            
            for (int i = 0; i < 32; i++)
            {
                if (i < chAddrs.Count) { _oscillConfig[i + 32] = chAddrs[i]; }
                else { _oscillConfig[i] = 0; }
            }

            _oscillConfig[64] = Convert.ToUInt16((OscilSize(ScopeSysType.OscilAllSize, false) << 16) >> 16);  //размер под осциллограмму 
            _oscillConfig[65] = Convert.ToUInt16(OscilSize(ScopeSysType.OscilAllSize, false) >> 16);
                                        
            _oscillConfig[66] = _nowScopeCount;            //Колличество формируемых осциллограмм
            _oscillConfig[67] = _nowMaxChannelCount;       //Колличество каналов
            _oscillConfig[68] = _nowHystory;               //Предыстория
            _oscillConfig[69] = _nowOscFreq;               //Как часто нужно записывать данные 
            _oscillConfig[70] = OscilEnable();            //Включен или выключен осциллограф и нужно ли выполнять запись в память 

            //Дополнительные параметры 
            //Запись названия канала
            List<string> chName = ChNames();                //Название каналов в Cp1251

            for (int i = 0; i < chName.Count; i++)
            {
                string chNameString = chName[i];
                byte[] tempChNameStr = new Byte[32];
                byte[] chNameStr = Encoding.Default.GetBytes(chNameString);

                for (int j = 0; j < 32; j++)
                {
                    if (j < chNameString.Length) tempChNameStr[j] = chNameStr[j];
                    else tempChNameStr[j] = 32;
                }
                for (int j = 1; j < 32; j += 2)
                {
                    _oscillConfig[71 + 16 * i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChNameStr[j]) << 8);
                    _oscillConfig[71 + 16 * i + (j / 2)] += Convert.ToUInt16(tempChNameStr[j - 1]);
                }
            }

            //for Comtrade
            #region
            List<string> chPhases = ChPhase();                //в Cp1251

            for (int i = 0; i < chPhases.Count; i++)
            {
                string chPhaseString = chPhases[i];
                byte[] tempChPhaseStr = new Byte[2];
                byte[] chPhaseStr = Encoding.Default.GetBytes(chPhaseString);

                for (int j = 0; j < 2; j++)
                {
                    if (j < chPhaseString.Length) tempChPhaseStr[j] = chPhaseStr[j];
                    else tempChPhaseStr[j] = 32;
                }
                for (int j = 1; j < 2; j += 2)
                {
                    _oscillConfig[583 + i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChPhaseStr[j]) << 8);
                    _oscillConfig[583 + i + (j / 2)] += Convert.ToUInt16(tempChPhaseStr[j - 1]);

                }
            }

            List<string> chCcbMs = ChCcbm();                // в Cp1251

            for (int i = 0; i < chCcbMs.Count; i++)
            {
                string chCcbmString = chCcbMs[i];
                byte[] tempChCcbmStr = new Byte[16];
                byte[] chCcbmStr = Encoding.Default.GetBytes(chCcbmString);

                for (int j = 0; j < 16; j++)
                {
                    if (j < chCcbmString.Length) tempChCcbmStr[j] = chCcbmStr[j];
                    else tempChCcbmStr[j] = 32;
                }
                for (int j = 1; j < 16; j += 2)
                {
                    _oscillConfig[615 + i * 8 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChCcbmStr[j]) << 8);
                    _oscillConfig[615 + i * 8 + (j / 2)] += Convert.ToUInt16(tempChCcbmStr[j - 1]);

                }
            }
            List<string> chDemensions = ChDemension();                //в Cp1251

            for (int i = 0; i < chDemensions.Count; i++)
            {
                string chDemensionString = chDemensions[i];
                byte[] tempChDemensionStr = new Byte[8];
                byte[] chDemensionStr = Encoding.Default.GetBytes(chDemensionString);

                for (int j = 0; j < 8; j++)
                {
                    if (j < chDemensionString.Length) tempChDemensionStr[j] = chDemensionStr[j];
                    else tempChDemensionStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    _oscillConfig[871 + i * 4 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChDemensionStr[j]) << 8);
                    _oscillConfig[871 + i * 4 + (j / 2)] += Convert.ToUInt16(tempChDemensionStr[j - 1]);
                }
            }

            List<ushort> chaTypeAd = ChTypeAd();          //

            for (int i = 0; i < chaTypeAd.Count; i++)
            {
                if (i < chaTypeAd.Count) { _oscillConfig[999 + i] = chaTypeAd[i]; }
                else { _oscillConfig[999 + i] = 0; }
            }

            String stationName = ScopeSysType.StationName;                //

            byte[] tempStationNameStr = new Byte[32];
            byte[] stationNameStr = Encoding.Default.GetBytes(stationName);

            for (int j = 0; j < 32; j++)
            {
                if (j < stationName.Length) tempStationNameStr[j] = stationNameStr[j];
                else tempStationNameStr[j] = 32;
            }
            for (int j = 1; j < 32; j += 2)
            {
                _oscillConfig[1031 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempStationNameStr[j]) << 8);
                _oscillConfig[1031 + (j / 2)] += Convert.ToUInt16(tempStationNameStr[j - 1]);
            }

            String recordingId = ScopeSysType.RecordingDevice;            //

            byte[] tempRecordingIdStr = new Byte[16];
            byte[] recordingIdStr = Encoding.Default.GetBytes(recordingId);

            for (int j = 0; j < 16; j++)
            {
                if (j < recordingId.Length) tempRecordingIdStr[j] = recordingIdStr[j];
                else tempRecordingIdStr[j] = 32;
            }
            for (int j = 1; j < 16; j += 2)
            {
                _oscillConfig[1047 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempRecordingIdStr[j]) << 8);
                _oscillConfig[1047 + (j / 2)] += Convert.ToUInt16(tempRecordingIdStr[j - 1]);
            }

            String timeCode = ScopeSysType.TimeCode;     //

            byte[] tempTimeCodeStr = new Byte[8];
            byte[] timeCodeStr = Encoding.Default.GetBytes(timeCode);

            for (int j = 0; j < 8; j++)
            {
                if (j < timeCode.Length) tempTimeCodeStr[j] = timeCodeStr[j];
                else tempTimeCodeStr[j] = 32;
            }
            for (int j = 1; j < 8; j += 2)
            {
                _oscillConfig[1055 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempTimeCodeStr[j]) << 8);
                _oscillConfig[1055 + (j / 2)] += Convert.ToUInt16(tempTimeCodeStr[j - 1]);
            }

            String localCode = ScopeSysType.TimeCode;     //

            byte[] tempLocalCodeStr = new Byte[8];
            byte[] localCodeStr = Encoding.Default.GetBytes(localCode);

            for (int j = 0; j < 8; j++)
            {
                if (j < localCode.Length) tempLocalCodeStr[j] = localCodeStr[j];
                else tempLocalCodeStr[j] = 32;
            }
            for (int j = 1; j < 8; j += 2)
            {
                _oscillConfig[1059 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempLocalCodeStr[j]) << 8);
                _oscillConfig[1059 + (j / 2)] += Convert.ToUInt16(tempLocalCodeStr[j - 1]);
            }

            String tmqCode = ScopeSysType.TmqCode;     //

            byte[] temptmqCodeStr = new Byte[8];
            byte[] tmqCodeStr = Encoding.Default.GetBytes(tmqCode);

            for (int j = 0; j < 8; j++)
            {
                if (j < tmqCode.Length) temptmqCodeStr[j] = tmqCodeStr[j];
                else temptmqCodeStr[j] = 32;
            }

            for (int j = 1; j < 8; j += 2)
            {
                _oscillConfig[1063 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(temptmqCodeStr[j]) << 8);
                _oscillConfig[1063 + (j / 2)] += Convert.ToUInt16(temptmqCodeStr[j - 1]);
            }

            String leapsec = ScopeSysType.Leapsec;     //

            byte[] templeapsecStr = new Byte[8];
            byte[] leapsecStr = Encoding.Default.GetBytes(leapsec);

            for (int j = 0; j < 8; j++)
            {
                if (j < leapsec.Length) templeapsecStr[j] = leapsecStr[j];
                else templeapsecStr[j] = 32;
            }
            for (int j = 1; j < 8; j += 2)
            {
                _oscillConfig[1067 + (j / 2)] += Convert.ToUInt16(Convert.ToUInt32(templeapsecStr[j]) << 8);
                _oscillConfig[1067 + (j / 2)] += Convert.ToUInt16(templeapsecStr[j - 1]);
            }
            #endregion   
        }

        private void WriteConfigToSystem()
        {
            CalcOscillConfig();
            _writeConfigStep = 0;
            CalcNewOscillConfig(_writeStep);
            WritePartConfigToSystem();
        }

        private void WritePartConfigToSystem()
        {
            ushort[] partParam = new ushort[8];
            for (int i = 0; i < 8; i++)
            {
                partParam[i] = _newOscillConfig[i + _writeConfigStep * 8];
            }
            ModBusUnits.ScopeSetupModbusUnit.SetData((ushort)( ScopeSysType.OscilCmndAddr + 328 + _writeConfigStep * 8), 8, partParam);
            // MessageBox.Show(ModBusUnits.ScopeSetupModbusUnit.modBusData.StartAddr.ToString("X4"));0x20 +
        }

        private void EndRequest(object sender, EventArgs e)
        {
            if (ModBusUnits.ScopeSetupModbusUnit.modBusData.RequestError)
            {
                if (Visible)
                {
                    MessageBox.Show(@"Ошибка связи!", @"Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LinkErrorInvoke();
            }
            else
            {   
                _writeConfigStep++;
                if (_writeConfigStep < 5) { WritePartConfigToSystem(); }     //Отправляю новую конфигурацию 
                else 
                {
                    if (_writeStep < 39)         { _writeStep++; CalcNewOscillConfig(_writeStep); _writeConfigStep = 0; WritePartConfigToSystem(); }
                    else
                    {
                        _writeStep = 0;
                        ScopeConfig.ChangeScopeConfig = true;
                        _changeConfig = true;
                    }
                }       
            }
        }

        private void StatusDownloadConfigToSystem()
        {
            if ((ScopeConfig.StatusOscil & 0x0001) == 0x0000)
            {
                StatusDownloadConfig.Image = Properties.Resources.Circle_Thin_64_2_;
                // ReSharper disable once LocalizableElement
                StatusDownloadConfig.ToolTipText = @"Статус загрузки конфигурации:" + "\n" + @"Конфигурация отсутствует.";
            }
            if ((ScopeConfig.StatusOscil & 0x0001) == 0x0001)
            {
                StatusDownloadConfig.Image = Properties.Resources.Circle_Thin_64_1_;
                // ReSharper disable once LocalizableElement
                StatusDownloadConfig.ToolTipText = @"Статус загрузки конфигурации:" + "\n" + @"Конфигурация успешно загружена и принята.";
            }
            if ((ScopeConfig.StatusOscil & 0x0002) == 0x0002)
            {
                StatusDownloadConfig.Image = Properties.Resources.Circle_Thin_64;
                // ReSharper disable once LocalizableElement
                StatusDownloadConfig.ToolTipText = @"Статус загрузки конфигурации:" + "\n" + @"Конфигурация загружена, но не прошла проверку.";
            }
            if ((ScopeConfig.StatusOscil & 0x0004) == 0x0004)
            {
                StatusDownloadConfig.Image = Properties.Resources.Circle_Thin_64;
                // ReSharper disable once LocalizableElement
                StatusDownloadConfig.ToolTipText = @"Статус загрузки конфигурации:" + "\n" + @"При загрузке нарушена целостность данных.";
            }
        }

        private void MessageAnswer()
        {
            if (ScopeConfig.StatusOscil == 0x0001)
            {
                // ReSharper disable once LocalizableElement
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"Конфигурация загружена и принята", @"Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (ScopeConfig.StatusOscil == 0x0002)
            {
                // ReSharper disable once LocalizableElement
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"Конфигурация загружена, но не принята", @"Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ScopeConfig.StatusOscil  == 0x0004)
            {
                TopMost = true;
                // ReSharper disable once LocalizableElement
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"Нарушена целостность данных", @"Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void writeToSystemBtn_Click(object sender, EventArgs e)
        {
            if (_nowMaxChannelCount < 1 || _nowMaxChannelCount > 32)
            {
                MessageBox.Show(@"Выбрано неверное число каналов", @"Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            if (!ModBusClient.ModBusOpened)
            {
                MessageBox.Show(@"Соединение с системой не установлено!", @"Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            // ReSharper disable once LocalizableElement
            if (MessageBox.Show("Изменить конфигурацию осциллографа?\n" +  
                                @"Все текущие осциллограммы будут удалены из памяти системы!",
                                @"Настройка осциллографа", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            WriteConfigToSystem();
        }

        //*************ЗАКРЫТИЕ ФОРМЫ, ОСВОБОЖДЕНИЕ РЕСУРСОВ *************************************************//
        //****************************************************************************************************//
        //****************************************************************************************************//
        private void ScopeSetupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ModBusUnits.ScopeSetupModbusUnit.RequestFinished -= EndRequest;
            }
            catch
            {
                // ignored
            }
        }

        //***************************Invok и****************************************************//

        private delegate void NoParamDelegate();
        private void LinkError()
        {
            Close();
        }
        private void LinkErrorInvoke()
        {
            try
            {
                Invoke(new NoParamDelegate(LinkError), null);
            }
            catch
            {
                // ignored
            }
        }

        //Загрузка из файла
        #region
        private void openButton2_Click(object sender, EventArgs e)
        {
            bool [] channelInList = new bool [32];
            bool channelInLists = false;
            string str = "Канала нет в списке: \n";
            for (int i = 0; i < 32; i++) { channelInList[i] = false; }

            foreach (var item in _channelnameListViewItems)
            {
                item.Checked = false;
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                DefaultExt = @".xoc",                                                                  // Default file extension
                Filter = @"XML Oscil Configuration |*.xoc|XML|*.xml|All files|*.*"                     // Filter files by extension  
            };
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.XmlFileNameOscil = ofd.FileName;   
                try
                {
                    ScopeSysType.InitScopeOscilType();
                }
                catch
                {
                    MessageBox.Show(@"Ошибка загрузки данных", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (ScopeSysType.OscilCount != 0) chCountRadioButton.Text = Convert.ToString(ScopeSysType.OscilCount);
            else chCountRadioButton.Clear();
            if (ScopeSysType.HistoryCount != 0) hystoryRadioButton.Text = Convert.ToString(ScopeSysType.HistoryCount);
            else hystoryRadioButton.Clear();
            if (ScopeSysType.FrequncyCount != 0) oscFreqRadioButton.Text = Convert.ToString(ScopeSysType.FrequncyCount);
            else oscFreqRadioButton.Clear();
            if (ScopeSysType.SizeValue > 0 && ScopeSysType.SizeValue <= 100) sizeOcsil_trackBar.Value = ScopeSysType.SizeValue;
            else sizeOcsil_trackBar.Value = 100;
            radioButton.Clear();

            if (ScopeSysType.OscilEnable == 0) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false;}
            if (ScopeSysType.OscilEnable == 1) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false;}
            if (ScopeSysType.OscilEnable == 2) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = false; }
            if (ScopeSysType.OscilEnable == 3) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = true; }
            if (ScopeSysType.OscilEnable == 4) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = true; }

            for (int i = 0; i < ScopeSysType.OscilChannelNames.Count ; i++)
            {
                for(int j = 0; j < ScopeSysType.ScopeItem.Count; j++)
                {
                    if (ScopeSysType.OscilChannelFormats[i] == ((ScopeSysType.ScopeItem[j].ChannelformatNumeric + 1) << 8) + ScopeSysType.ScopeItem[j].ChannelFormats && ScopeSysType.OscilChannelAddrs[i] == ScopeSysType.ScopeItem[j].ChannelAddrs)
                    {
                        channelInList[i] = true;
                        _channelnameListViewItems[j].Checked = true;
                        break;
                    }
                }
            }

            for (int i = 0; i < ScopeSysType.OscilChannelNames.Count; i++) 
            {
                if (channelInList[i] == false) 
                {
                    str += ScopeSysType.OscilChannelNames[i] + @" Адрес: 0x" + ScopeSysType.OscilChannelAddrs[i].ToString("X4") + @" Формат: " + ScopeSysType.OscilChannelFormats[i] + "\n";
                    channelInLists =true;
                }
            }
            if (channelInLists) MessageBox.Show(str);
        }

        //Сохранение осциллограммы в файл
        private void saveButton2_Click(object sender, EventArgs e)
        {
            if (_nowMaxChannelCount != ChNames().Count)
            {
                MessageBox.Show(@"Количество осциллографируемых и выбранных каналов не совпадает");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = @".xoc",                                       // Default file extension
                Filter = @"XML Oscil Configuration|*.xoc|XML|*.xml"         // Filter files by extension
            };

            int j = 0;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.XmlFileName = sfd.FileName;

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode)
                {
                    Formatting = Formatting.Indented
                };
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("Setup");
                /////////////////////////////////////////////////////////////

                xmlOut.WriteStartElement("Oscil");
                xmlOut.WriteAttributeString("Count", Convert.ToString(_nowScopeCount));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Channel");
                xmlOut.WriteAttributeString("Count", Convert.ToString(_nowMaxChannelCount));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Story");
                xmlOut.WriteAttributeString("Count", Convert.ToString(_nowHystory));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Frequency");
                xmlOut.WriteAttributeString("Count", Convert.ToString(_nowOscFreq));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Size");
                xmlOut.WriteAttributeString("Count", Convert.ToString(sizeOcsil_trackBar.Value));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilEnable");
                xmlOut.WriteAttributeString("Count", Convert.ToString(OscilEnable()));
                xmlOut.WriteEndElement();

                foreach (var item in _channelnameListViewItems)
                {
                    if (item.Checked)
                    {
                        xmlOut.WriteStartElement("MeasureParam" + (++j));

                        xmlOut.WriteAttributeString("Name", ScopeSysType.ScopeItem[item.Index].ChannelNames);
                        xmlOut.WriteAttributeString("Addr", ScopeSysType.ScopeItem[item.Index].ChannelAddrs.ToString());
                        xmlOut.WriteAttributeString("Format", ((((ScopeSysType.ScopeItem[item.Index].ChannelformatNumeric) + 1) << 8) + ScopeSysType.ScopeItem[item.Index].ChannelFormats).ToString());

                        xmlOut.WriteEndElement();
                    }
                }
                             
                /////////////////////////////////////////////////////////////
                xmlOut.WriteEndElement();
                xmlOut.WriteEndDocument();
                xmlOut.Close();
                fs.Close();
            }
        }
        #endregion

        private void reloadButton_Click(object sender, EventArgs e)
        {
            if (ScopeConfig.ChannelCount == 0)
            {
                MessageBox.Show(@"В системе отсутствует конфигурация.");
                return;
            }

            string str = "Следующих каналов из системы нет в списке:\n";
            bool channelInLists = false;
            bool[] channelInList = new bool[32];
            for (int i = 0; i < 32; i++) { channelInList[i] = false; }

            foreach (var item in _channelnameListViewItems)
            {
                item.Checked = false;
            }

            if (ScopeConfig.ScopeCount != 0) chCountRadioButton.Text = Convert.ToString(ScopeConfig.ScopeCount);
            else chCountRadioButton.Clear();
            if (ScopeConfig.HistoryCount != 0) hystoryRadioButton.Text = Convert.ToString(ScopeConfig.HistoryCount);
            else hystoryRadioButton.Clear();
            if (ScopeConfig.ChannelCount != 0) radioButton.Text = Convert.ToString(ScopeConfig.ChannelCount);
            else oscFreqRadioButton.Clear();
            if (ScopeConfig.FreqCount != 0) oscFreqRadioButton.Text = Convert.ToString(ScopeConfig.FreqCount);
            else hystoryRadioButton.Clear();

            if (ScopeConfig.OscilEnable == 0) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false; }
            if (ScopeConfig.OscilEnable == 1) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false; }
            if (ScopeConfig.OscilEnable == 2) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = false; }
            if (ScopeConfig.OscilEnable == 3) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = true; }
            if (ScopeConfig.OscilEnable == 4) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = true; }
            
            for (int i = 0; i < ScopeConfig.OscilFormat.Count; i++)
            {
                for (int j = 0; j < ScopeSysType.ScopeItem.Count; j++)
                {

                    if (ScopeConfig.OscilFormat[i] == ((ScopeSysType.ScopeItem[j].ChannelformatNumeric + 1) << 8)+ ScopeSysType.ScopeItem[j].ChannelFormats && ScopeConfig.OscilAddr[i] == ScopeSysType.ScopeItem[j].ChannelAddrs)
                    {
                        channelInList[i] = true;
                        _channelnameListViewItems[j].Checked = true;
                        
                        break;
                    }
                }
            }

            for (int i = 0; i < ScopeConfig.OscilAddr.Count; i++)
            {
                if (channelInList[i] == false)
                {
                    str += @"Адрес: 0x" + ScopeConfig.OscilAddr[i].ToString("X4") + @" Формат: " + ScopeConfig.OscilFormat[i].ToString() + "\n";
                    channelInLists = true;
                }
            }
            if (channelInLists) MessageBox.Show(str);

            SizeTrackBar();
        }

        private void SizeTrackBar()
        {
            if (ScopeConfig.ScopeCount != 0)
            {
                try
                {
                    sizeOcsil_trackBar.Value = ScopeConfig.OscilSize * 100 * ScopeConfig.ScopeCount % ScopeConfig.OscilAllSize == 0
                        ? (int) (ScopeConfig.OscilSize * 100 * ScopeConfig.ScopeCount / ScopeConfig.OscilAllSize)
                        : (int) (ScopeConfig.OscilSize * 100 * ScopeConfig.ScopeCount / ScopeConfig.OscilAllSize) + 1;
                }
                catch
                {
                    MessageBox.Show(@"Ошибка при чтении конфигурации с платы", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigToSystem()
        {
            string str = Path.GetFileName(ScopeSysType.XmlFileName);
            ConfigToSystem_label.Text = @"Actual configuration: " + str;
        }
    }
}
