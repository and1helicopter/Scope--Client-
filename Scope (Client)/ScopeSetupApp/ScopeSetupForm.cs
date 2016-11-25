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
using System.IO;

namespace ScopeSetupApp
{
    public partial class ScopeSetupForm : Form
    {

        private ushort _nowHystory = 1;             //Предыстория 
        private ushort _nowScopeCount = 1;          //Количество осциллограмм
        private ushort _nowMaxChannelCount = 1 ;    //Количество каналов
        private ushort _nowOscFreq = 1;             //Делитель     
        private uint _oscilAllSize = 1;

        //Динамическое заполнение формы 
        #region
        private List<Label> _possibleLabels;
        private List<Label> _currentLabels;
        private List<CheckBox> _checkBoxs;
        private List<string> _nameGroup;
        private List<List<int>> _countInGroup;
        private List<Label> _groupLabels;

        public void InitPossiblePanel()
        {
            _possibleLabels = new List<Label>();
            _checkBoxs = new List<CheckBox>();
            _currentLabels = new List<Label>();
            _nameGroup = new List<string>();
            _countInGroup = new List<List<int>>();
            _groupLabels = new List<Label>();

            _oscilAllSize = ScopeSysType.OscilAllSize;
            CommentRichTextBox.Text = ScopeSysType.OscilComment;

            int i;

            for (i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                _currentLabels.Add(new Label());
                _currentLabels[i].Visible = false;
            }
            
            possibleTableLayoutPanel.RowCount = ScopeSysType.ChannelNames.Count;

            possibleTableLayoutPanel.RowStyles[0] = new RowStyle(SizeType.AutoSize);
            for (i = 0; i < (ScopeSysType.ChannelNames.Count - 1); i++)
            {
                possibleTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            _nameGroup.Clear();
            _countInGroup.Clear();
            _nameGroup.Add(ScopeSysType.GroupNames[0]);
            _countInGroup.Add(new List<int>());

            for (i = 0; i < ScopeSysType.GroupNames.Count; i++)
            {
                for (int j = 0; j < _nameGroup.Count; j++)
                {
                    if (ScopeSysType.GroupNames[i] == _nameGroup[j])
                    {
                        _countInGroup[j].Add(i);
                        break;
                    }
                    if (j == _nameGroup.Count - 1)
                    {
                        _countInGroup.Add(new List<int>());
                        _nameGroup.Add(ScopeSysType.GroupNames[i]);
                        _countInGroup[j + 1].Add(i);
                        break;
                    }
                }
            }

            for (int j = 0; j < _nameGroup.Count; j++)
            {
                _groupLabels.Add(new Label());
                _groupLabels[j].Dock = DockStyle.Fill;
                if (_nameGroup[j] == "") _groupLabels[j].Text = @"Несгруппированные параметры";
                else _groupLabels[j].Text = _nameGroup[j];
                _groupLabels[j].BorderStyle = BorderStyle.FixedSingle;
                _groupLabels[j].Margin = new Padding(1);
                _groupLabels[j].AutoSize = true;
                _groupLabels[j].Font = sampleNameLabel.Font;
                _groupLabels[j].BackColor = SystemColors.ButtonHighlight;
                // groupLabels[j].Click += new System.EventHandler(group_Click);

                possibleTableLayoutPanel.Controls.Add(_groupLabels[j]);

                for (int k = 0; k < _countInGroup[j].Count; k++)
                {
                    CheckBox checkBox = new CheckBox();
                    _checkBoxs.Add(checkBox);
                    _possibleLabels.Add(new Label());
                    i = _possibleLabels.Count - 1;
                    _possibleLabels[i].Dock = DockStyle.Fill;
                    _possibleLabels[i].BorderStyle = BorderStyle.FixedSingle;
                    _possibleLabels[i].AutoSize = true;
                    _possibleLabels[i].Margin = new Padding(15, 1, 1, 1);
                    _possibleLabels[i].Font = sampleNameLabel.Font;
                    _possibleLabels[i].TextAlign = ContentAlignment.MiddleLeft;
                    _possibleLabels[i].Text = (i + 1) + @". " + ScopeSysType.ChannelNames[i];
                     _possibleLabels[i].Controls.Add(checkBox);
                    checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    checkBox.AutoSize = true;
                    checkBox.Dock = DockStyle.Right;
                    checkBox.RightToLeft = RightToLeft.Yes;
                    checkBox.Size = new Size(15, 15);
                    checkBox.Click += new EventHandler(checkBox_Click);
                    _possibleLabels[i].Controls.Contains(checkBox);
                    _possibleLabels[i].BackColor = SystemColors.ButtonHighlight;
                    _possibleLabels[i].Tag = i;
                    
                    possibleTableLayoutPanel.Controls.Add(_possibleLabels[i]);
                }

            }
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (_checkBoxs[i].Checked) 
                {
                    _currentLabels[i].Visible = true; 
                    _possibleLabels[i].BackColor = System.Drawing.Color.LightSteelBlue;
                    radioButton.Text = Convert.ToString(VisibleCount());
                }
                else
                {
                    _currentLabels[i].Visible = false;
                    _possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
                    if (VisibleCount() != 0) radioButton.Text = Convert.ToString(VisibleCount());
                    else radioButton.Clear();
                    checkBox2.Checked = false;
                }
            }
        }



        private int VisibleCount()
        {
            int count = 0;
            for(int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (_currentLabels[i].Visible == true) count++;
            }
            return count;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
                {
                    _checkBoxs[i].Checked = true;
                    _currentLabels[i].Visible = true;
                    if (VisibleCount() != 0) radioButton.Text = Convert.ToString(VisibleCount());
                    else radioButton.Clear();
                    _possibleLabels[i].BackColor = System.Drawing.Color.LightSteelBlue;
                }
             }
            else
            {
                for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
                {
                    _checkBoxs[i].Checked = false;
                    _currentLabels[i].Visible = false;
                    if (VisibleCount() != 0) radioButton.Text = Convert.ToString(VisibleCount());
                    else radioButton.Clear();
                    _possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
                }
            }
        }
        #endregion

        public ScopeSetupForm()
        {
            InitializeComponent();

            ConfigToSystem();
            InitPossiblePanel();

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

            timer1.Enabled = true;
        }

        private ushort CalcCurrentParams()
        {
            ushort u = 0;
            for (int i1 = 0; i1 < ScopeSysType.ChannelNames.Count; i1++)
            {
                if (_currentLabels[i1].Visible) { u++; }
            }
            return u;
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
            if (chCountRadioButton.Text != "" && chCountRadioButton.Text != "-")
            {
                _nowScopeCount = Convert.ToUInt16(chCountRadioButton.Text);
                if (_nowScopeCount < 1 || _nowScopeCount > 32)
                {
                    MessageBox.Show("Ошибка в поле Количество осциллограмм");
                    chCountRadioButton.Clear();
                    return;
                }
            }
        }

        //Колличество  каналов

        private void radioButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)  // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void radioButton_TextChanged(object sender, EventArgs e)
        {
            if (radioButton.Text != "" && radioButton.Text != "-")
            {
                _nowMaxChannelCount = Convert.ToUInt16(radioButton.Text);
                if (_nowMaxChannelCount < 1 || _nowMaxChannelCount > 32)
                {
                    MessageBox.Show("Ошибка в поле Колличество каналов");
                    radioButton.Clear();
                    return;
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
            if (hystoryRadioButton.Text != "" && hystoryRadioButton.Text != "-")
            {
                _nowHystory = Convert.ToUInt16(hystoryRadioButton.Text);
                if (_nowHystory < 1 || _nowHystory > 99)
                {
                    MessageBox.Show("Ошибка в поле Предыстория");
                    hystoryRadioButton.Clear();
                    return;
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
            if (oscFreqRadioButton.Text != "" && oscFreqRadioButton.Text != "-")
            {
                _nowOscFreq = Convert.ToUInt16(oscFreqRadioButton.Text);
                if (_nowOscFreq < 1 || _nowOscFreq > 1000)
                {
                    MessageBox.Show("Ошибка в поле Предыстория");
                    oscFreqRadioButton.Clear();
                    return;
                }
            }
        }

        private int _divideOscilSize = 1;

        //Длительность осциллограммы:
        private void DelayOscil()
        {
            if (_nowScopeCount != 0)
            {
                if (ModBusClient.ModBusOpened == true && ScopeConfig.ConnectMcu == true)
                {
                    double sampleCount = (double)OscilSize(_oscilAllSize, false) / OscilSize(_oscilAllSize, true);
                    double freq = (double)ScopeConfig.SampleRate / _nowOscFreq;
                    double timeSec = (double)sampleCount / freq;
                    DelayOsc.Text = "Длительность: " + timeSec.ToString("0.000") + " сек";
                    DelayOsc.Visible = true;
                }
                else
                {
                    double sampleCount = (double)OscilSize(_oscilAllSize, false) / OscilSize(_oscilAllSize, true);
                    double freq = (double)ScopeSysType.OscilSampleRate / _nowOscFreq;
                    double timeSec = (double)sampleCount / freq;
                    DelayOsc.Text = "Длительность: " + timeSec.ToString("0.000") + " сек";
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
        private int[] _channelSeries = new int[32];

        private int _writeConfigStep = 0;
        private ushort _writeStep = 0;

        //Конфигурирование осциллограммы 
        #region
        private List<ushort> ChannelFormats()
        {
            int j = 0;
            List<ushort> l = new List<ushort>();
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (_currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 3) { _channelSeries[j++] = i; l.Add(ScopeSysType.ChannelFormats[i]); }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (_currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 2) { _channelSeries[j++] = i; l.Add(ScopeSysType.ChannelFormats[i]); }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (_currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 1) { _channelSeries[j++] = i; l.Add(ScopeSysType.ChannelFormats[i]); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }
 
        // Channel Addrs
        private List<ushort> ChannelAddrs()
        {
            List<ushort> l = new List<ushort>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (_currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelAddrs[_channelSeries[j++]]); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        // OscilSize
        public uint OscilSize(uint allSize, bool wr)
        {
            uint count64 = 0, count32 = 0, count16 = 0;
            uint sampleSize;
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (_currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 3) { count64++; }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (_currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 2) { count32++; }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (_currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 1) { count16++; }
            }

            sampleSize = count64 * 8 + count32 * 4 + count16 * 2;
            if ((count64 != 0 || count32 != 0) && count16 % 2 != 0) { sampleSize += 2; } // Выравнивание на 4 байта
            if (sampleSize == 0)
            {
                //MessageBox.Show("Не выбрано ни одного канала для осциллографирования");
                return 0;
            }

            uint oscS;
            if (ScopeConfig.ConnectMcu == true) oscS = Convert.ToUInt32((ScopeConfig.OscilAllSize / Convert.ToUInt32(_nowScopeCount)) * ((double)trackBar1.Value / 100));
            else oscS = Convert.ToUInt32(((allSize * 1024) / Convert.ToUInt32(_nowScopeCount)) * ((double)trackBar1.Value / 100));

            while (oscS % 64 != 0 || oscS % sampleSize != 0)   // 
            { 		
                oscS--;
            }

            if (wr == false) return oscS;
            if (wr == true) return sampleSize;
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
            for (int i = 0, j = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (_currentLabels[i].Visible) { chName.Add(ScopeSysType.ChannelNames[_channelSeries[j++]]); }
            }
            if (chName.Count > _nowMaxChannelCount) { chName.Clear(); }

            return chName;
        }
       //////////////////////////////////////////
       //For Comtrade 
        private List<string> ChPhase()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelPhase.Count; i++)
            {
                if (_currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelPhase[_channelSeries[j++]]); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<string> ChCcbm()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelCcbm.Count; i++)
            {
                if (_currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelCcbm[_channelSeries[j++]]); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<string> ChDemension()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelDimension.Count; i++)
            {
                if (_currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelDimension[_channelSeries[j++]]); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<ushort> ChTypeAd()
        {
            List<ushort> l = new List<ushort>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelTypeAd.Count; i++)
            {
                if (_currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelTypeAd[_channelSeries[j++]]); }
            }
            if (l.Count > _nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        #endregion


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

            if (OscilEnable() == 2)
            {
                //Дополнительные параметры 
                //Запись названия канала
                List<string> chName = ChNames();                //Название каналов в Cp1251

                for (int i = 0; i < chName.Count; i++) 
                { 
                    string chNameString = chName[i];
                    byte[] chNameStr = new Byte[32];
                    byte[] tempChNameStr = new Byte[32];
                    chNameStr = Encoding.Default.GetBytes(chNameString);

                    for (int j = 0; j < 32; j++)
                    {
                        if (j < chNameString.Length) tempChNameStr[j] = chNameStr[j];
                        else tempChNameStr[j] = 32;
                    }
                    for (int j = 1; j < 32; j += 2)
                    {
                        _oscillConfig[71 + 16 * i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChNameStr[j - 1]) << 8);
                        _oscillConfig[71 + 16 * i + (j / 2)] += Convert.ToUInt16(tempChNameStr[j]);
                    }
                }

                //for Comtrade
                #region
                List<string> chPhases = ChPhase();                //в Cp1251

                for (int i = 0; i < chPhases.Count; i++)
                {
                    string chPhaseString = chPhases[i];
                    byte[] chPhaseStr = new Byte[2];
                    byte[] tempChPhaseStr = new Byte[2];
                    chPhaseStr = Encoding.Default.GetBytes(chPhaseString);

                    for (int j = 0; j < 2; j++)
                    {
                        if (j < chPhaseString.Length) tempChPhaseStr[j] = chPhaseStr[j];
                        else tempChPhaseStr[j] = 32;
                    }
                    for (int j = 1; j < 2; j += 2)
                    {
                        _oscillConfig[583 + i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChPhaseStr[j - 1]) << 8);
                        _oscillConfig[583 + i + (j / 2)] += Convert.ToUInt16(tempChPhaseStr[j]);
                    }
                }

                List<string> chCcbMs = ChCcbm();                // в Cp1251

                for (int i = 0; i < chCcbMs.Count; i++)
                {
                    string chCcbmString = chCcbMs[i];
                    byte[] chCcbmStr = new Byte[16];
                    byte[] tempChCcbmStr = new Byte[16];
                    chCcbmStr = Encoding.Default.GetBytes(chCcbmString);

                    for (int j = 0; j < 16; j++)
                    {
                        if (j < chCcbmString.Length) tempChCcbmStr[j] = chCcbmStr[j];
                        else tempChCcbmStr[j] = 32;
                    }
                    for (int j = 1; j < 16; j += 2)
                    {
                        _oscillConfig[615 + i * 8 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChCcbmStr[j - 1]) << 8);
                        _oscillConfig[615 + i * 8 + (j / 2)] += Convert.ToUInt16(tempChCcbmStr[j]);
                    }
                }
                List<string> chDemensions = ChDemension();                //в Cp1251

                for (int i = 0; i < chDemensions.Count; i++)
                {
                    string chDemensionString = chDemensions[i];
                    byte[] chDemensionStr = new Byte[8];
                    byte[] tempChDemensionStr = new Byte[8];
                    chDemensionStr = Encoding.Default.GetBytes(chDemensionString);

                    for (int j = 0; j < 8; j++)
                    {
                        if (j < chDemensionString.Length) tempChDemensionStr[j] = chDemensionStr[j];
                        else tempChDemensionStr[j] = 32;
                    }
                    for (int j = 1; j < 8; j += 2)
                    {
                        _oscillConfig[871 + i * 4 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChDemensionStr[j - 1]) << 8);
                        _oscillConfig[871 + i * 4 + (j / 2)] += Convert.ToUInt16(tempChDemensionStr[j]);
                    }
                }

                List<ushort> chaTypeAd = ChTypeAd();          //

                for (int i = 0; i < chaTypeAd.Count; i++)
                {
                    if (i < chaTypeAd.Count) { _oscillConfig[999 + i] = chaTypeAd[i]; }
                    else { _oscillConfig[999 + i] = 0; }
                }  

                String stationName = ScopeSysType.StationName;                //

                byte[] stationNameStr = new Byte[32];
                byte[] tempStationNameStr = new Byte[32];
                stationNameStr = Encoding.Default.GetBytes(stationName);

                for (int j = 0; j < 32; j++)
                {
                    if (j < stationName.Length) tempStationNameStr[j] = stationNameStr[j];
                    else tempStationNameStr[j] = 32;
                }
                for (int j = 1; j < 32; j += 2)
                {
                    _oscillConfig[1031 +  (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempStationNameStr[j - 1]) << 8);
                    _oscillConfig[1031 +  (j / 2)] += Convert.ToUInt16(tempStationNameStr[j]);
                }

                String recordingId = ScopeSysType.RecordingDevice;            //

                byte[] recordingIdStr = new Byte[16];
                byte[] tempRecordingIdStr = new Byte[16];
                recordingIdStr = Encoding.Default.GetBytes(recordingId);

                for (int j = 0; j < 16; j++)
                {
                    if (j < recordingId.Length) tempRecordingIdStr[j] = recordingIdStr[j];
                    else tempRecordingIdStr[j] = 32;
                }
                for (int j = 1; j < 16; j += 2)
                {
                    _oscillConfig[1047 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempRecordingIdStr[j - 1]) << 8);
                    _oscillConfig[1047 + (j / 2)] += Convert.ToUInt16(tempRecordingIdStr[j]);
                }

                String timeCode = ScopeSysType.TimeCode;     //

                byte[] timeCodeStr = new Byte[16];
                byte[] tempTimeCodeStr = new Byte[16];
                timeCodeStr = Encoding.Default.GetBytes(timeCode);

                for (int j = 0; j < 8; j++)
                {
                    if (j < timeCode.Length) tempTimeCodeStr[j] = timeCodeStr[j];
                    else tempTimeCodeStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    _oscillConfig[1055 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempTimeCodeStr[j - 1]) << 8);
                    _oscillConfig[1055 + (j / 2)] += Convert.ToUInt16(tempTimeCodeStr[j]);
                }

                String localCode = ScopeSysType.TimeCode;     //

                byte[] localCodeStr = new Byte[16];
                byte[] tempLocalCodeStr = new Byte[16];
                localCodeStr = Encoding.Default.GetBytes(localCode);

                for (int j = 0; j < 8; j++)
                {
                    if (j < localCode.Length) tempLocalCodeStr[j] = localCodeStr[j];
                    else tempLocalCodeStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    _oscillConfig[1059 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempLocalCodeStr[j - 1]) << 8);
                    _oscillConfig[1059 + (j / 2)] += Convert.ToUInt16(tempLocalCodeStr[j]);
                }

                String tmqCode = ScopeSysType.TmqCode;     //

                byte[] tmqCodeStr = new Byte[16];
                byte[] temptmqCodeStr = new Byte[16];
                tmqCodeStr = Encoding.Default.GetBytes(tmqCode);

                for (int j = 0; j < 8; j++)
                {
                    if (j < tmqCode.Length) temptmqCodeStr[j] = tmqCodeStr[j];
                    else temptmqCodeStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    _oscillConfig[1063 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(temptmqCodeStr[j - 1]) << 8);
                    _oscillConfig[1063 + (j / 2)] += Convert.ToUInt16(temptmqCodeStr[j]);
                }

                String leapsec = ScopeSysType.Leapsec;     //

                byte[] leapsecStr = new Byte[16];
                byte[] templeapsecStr = new Byte[16];
                leapsecStr = Encoding.Default.GetBytes(leapsec);

                for (int j = 0; j < 8; j++)
                {
                    if (j < leapsec.Length) templeapsecStr[j] = leapsecStr[j];
                    else templeapsecStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    _oscillConfig[1067 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(templeapsecStr[j - 1]) << 8);
                    _oscillConfig[1067 + (j / 2)] += Convert.ToUInt16(templeapsecStr[j]);
                }
                #endregion
            }
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

        public void EndRequest(object sender, EventArgs e)
        {
            if (ModBusUnits.ScopeSetupModbusUnit.modBusData.RequestError)
            {
                if (this.Visible)
                {
                    MessageBox.Show("Ошибка связи!", "Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Конфигурация осциллографа была изменена!", "Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ScopeConfig.ChangeScopeConfig = true;
                    }
                }       
            }
        }

         
        private void writeToSystemBtn_Click(object sender, EventArgs e)
        {
            //TopMost = true;
            if (!ModBusClient.ModBusOpened)
            {
                MessageBox.Show("Соединение с системой не установлено!", "Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error);
             //   TopMost = false;
                return; 
            }

            if (MessageBox.Show("Изменить конфигурацию осциллографа?\n" +
                                "Все текущие осциллограммы будут удалены из памяти системы!",
                                "Настройка осциллографа", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            {
             //   TopMost = false; 
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
            catch { }

        }



        //***************************Invok и****************************************************//

        private delegate void NoParamDelegate();
        private void LinkError()
        {
            this.Close();
        }
        private void LinkErrorInvoke()
        {
            try
            {
                Invoke(new NoParamDelegate(LinkError), null);
            }
            catch { }
        }



        //Загрузка из файла
        #region
        private void openButton2_Click(object sender, EventArgs e)
        {
            bool [] channelInList = new bool [32];
            bool channelInLists = false;
            string str = "Канала нет в списке: \n";
            for (int i = 0; i < 32; i++) { channelInList[i] = false; }
             
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                _checkBoxs[i].Checked = false;
                _currentLabels[i].Visible = false;
                _possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
            }
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".xoc"; // Default file extension
            ofd.Filter = "XML Oscil Configuration |*.xoc|XML|*.xml|All files|*.*"; // Filter files by extension
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.XmlFileNameOscil = ofd.FileName;   
                try
                {
                    ScopeSysType.InitScopeOscilType();
                }
                catch
                {
                    MessageBox.Show("Ошибка загрузки данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (ScopeSysType.OscilCount != 0) chCountRadioButton.Text = Convert.ToString(ScopeSysType.OscilCount);
            else chCountRadioButton.Clear();
            if (ScopeSysType.HistoryCount != 0) hystoryRadioButton.Text = Convert.ToString(ScopeSysType.HistoryCount);
            else hystoryRadioButton.Clear();
            if (ScopeSysType.FrequncyCount != 0) oscFreqRadioButton.Text = Convert.ToString(ScopeSysType.FrequncyCount);
            else oscFreqRadioButton.Clear();
            radioButton.Clear();

            if (ScopeSysType.OscilEnable == 0) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false;}
            if (ScopeSysType.OscilEnable == 1) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false;}
            if (ScopeSysType.OscilEnable == 2) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = false; }
            if (ScopeSysType.OscilEnable == 3) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = true; }
            if (ScopeSysType.OscilEnable == 4) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = true; }

            for (int i = 0; i < ScopeSysType.OscilChannelNames.Count ; i++)
            {
                for(int j = 0; j < ScopeSysType.ChannelNames.Count; j++)
                {    
                    if (ScopeSysType.OscilChannelFormats[i] == ScopeSysType.ChannelFormats[j] && ScopeSysType.OscilChannelAddrs[i] == ScopeSysType.ChannelAddrs[j])
                    {
                        channelInList[i] = true;
                        _checkBoxs[j].Checked = true;
                        _currentLabels[j].Visible = true;
                        _possibleLabels[j].BackColor = System.Drawing.Color.LightSteelBlue;
                        radioButton.Text = Convert.ToString(VisibleCount());
                        break;
                    }
                }
            }

            for (int i = 0; i < ScopeSysType.OscilChannelNames.Count; i++) 
            {
                if (channelInList[i] == false) 
                {
                    str += ScopeSysType.OscilChannelNames[i].ToString() + " Адрес: 0x" + ScopeSysType.OscilChannelAddrs[i].ToString("X4") + " Формат: " + ScopeSysType.OscilChannelFormats[i].ToString() + "\n";
                    channelInLists =true;
                }
            }
            if (channelInLists == true) MessageBox.Show(str);
        }
        #endregion

        //Сохранение осциллограммы в файл
        #region
        private void saveButton2_Click(object sender, EventArgs e)
        {
            if (_nowMaxChannelCount != ChNames().Count)
            {
                MessageBox.Show("Количество осциллографируемых и выбранных каналов не совпадает");
                return;
            }
            
            List<string> paramAddrStrs = new List<string>();
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xoc"; // Default file extension
            sfd.Filter = "XML Oscil Configuration|*.xoc|XML|*.xml"; // Filter files by extension
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.XmlFileName = sfd.FileName;

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                xmlOut.Formatting = Formatting.Indented;
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

                xmlOut.WriteStartElement("OscilEnable");
                xmlOut.WriteAttributeString("Count", Convert.ToString(OscilEnable()));
                xmlOut.WriteEndElement();
                
                             
                for (int i = 0, j = 0; i < _possibleLabels.Count; i++)
                {
                    if(_currentLabels[i].Visible == true)
                    {                    
                        xmlOut.WriteStartElement("MeasureParam" + (++j).ToString());

                        xmlOut.WriteAttributeString("Name", ScopeSysType.ChannelNames[i]);
                        xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.ChannelAddrs[i]));
                        xmlOut.WriteAttributeString("Format", Convert.ToString(ScopeSysType.ChannelFormats[i]));
                    
                        xmlOut.WriteEndElement();
                    }

                }

                /////////////////////////////////////////////////////////////
                xmlOut.WriteEndElement();
                xmlOut.WriteEndDocument();
                xmlOut.Close();
                fs.Close();

                //ScopeSysType.InitScopeSysType();
            }
        }
        #endregion

        private void reloadButton_Click(object sender, EventArgs e)
        {
            string str = "Следующих каналов из системы нет в списке:\n";
            bool channelInLists = false;
            bool[] channelInList = new bool[32];
            for (int i = 0; i < 32; i++) { channelInList[i] = false; }

            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                _checkBoxs[i].Checked = false;
                _currentLabels[i].Visible = false;
                _possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
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
                for (int j = 0; j < ScopeSysType.ChannelNames.Count; j++)
                {
                    if (ScopeConfig.OscilFormat[i] == ScopeSysType.ChannelFormats[j] && ScopeConfig.OscilAddr[i] == ScopeSysType.ChannelAddrs[j])
                    {
                        channelInList[i] = true;
                        _checkBoxs[j].Checked = true;
                        _currentLabels[j].Visible = true;
                        _possibleLabels[j].BackColor = System.Drawing.Color.LightSteelBlue;
                        radioButton.Text = Convert.ToString(VisibleCount());
                        break;
                    }
                }
            }

            for (int i = 0; i < ScopeConfig.OscilAddr.Count; i++)
            {
                if (channelInList[i] == false)
                {
                    str += "Адрес: 0x" + ScopeConfig.OscilAddr[i].ToString("X4") + " Формат: " + ScopeConfig.OscilFormat[i].ToString() + "\n";
                    channelInLists = true;
                }
            }
            if (channelInLists == true) MessageBox.Show(str);
        }

        private void ConfigToSystem()
        {
            string str = "";
            str = Path.GetFileName(ScopeSysType.XmlFileName);
            ConfigToSystem_label.Text = "Actual configuration: " + str;
        }

    }
}
