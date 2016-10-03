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

        private ushort nowHystory = 1;             //Предыстория 
        private ushort nowScopeCount = 1;          //Количество осциллограмм
        private ushort nowMaxChannelCount = 1 ;    //Количество каналов
        private ushort nowOscFreq = 1;             //Делитель     
        private uint oscilAllSize = 1;

        //Динамическое заполнение формы 
        #region
        private List<Label> possibleLabels;
        private List<Label> currentLabels;
        private List<CheckBox> checkBoxs;

        public void InitPossiblePanel()
        {
            possibleLabels = new List<Label>();
            checkBoxs = new List<CheckBox>();
            currentLabels = new List<Label>();
            oscilAllSize = ScopeSysType.OscilAllSize;

            CommentRichTextBox.Text = ScopeSysType.OscilComment.ToString();

            int i;

            for (i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                currentLabels.Add(new Label());
                currentLabels[i].Visible = false;
            }
            
            this.possibleTableLayoutPanel.RowCount = ScopeSysType.ChannelNames.Count;

            this.possibleTableLayoutPanel.RowStyles[0] = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize);
            for (i = 0; i < (ScopeSysType.ChannelNames.Count - 1); i++)
            {
                this.possibleTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            }

            for (i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBoxs.Add(checkBox);
                possibleLabels.Add(new Label());
                possibleLabels[i].Dock = DockStyle.Fill;
                possibleLabels[i].BorderStyle = BorderStyle.FixedSingle;
                possibleLabels[i].AutoSize = true;
                possibleLabels[i].Margin = new System.Windows.Forms.Padding (1); 
                possibleLabels[i].Font = sampleNameLabel.Font;
                possibleLabels[i].TextAlign = ContentAlignment.MiddleLeft;
                possibleLabels[i].Text = (i + 1) + ". " + ScopeSysType.ChannelNames[i]; // +"; " + ScopeSysType.ChannelFormatsName[i];
                possibleLabels[i].Controls.Add(checkBox);
                checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                checkBox.AutoSize = true;
                checkBox.Dock = DockStyle.Right;
                checkBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                checkBox.Size = new System.Drawing.Size(15, 15);
                checkBox.Click += new System.EventHandler(this.checkBox_Click);
                possibleLabels[i].Controls.Contains(checkBox);
                possibleTableLayoutPanel.Controls.Add(possibleLabels[i]);
                possibleTableLayoutPanel.SetColumn(possibleLabels[i], 0);
                possibleTableLayoutPanel.SetRow(possibleLabels[i], i);
                possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
                possibleLabels[i].Tag = i;
            }
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (checkBoxs[i].Checked) 
                {
                    currentLabels[i].Visible = true; 
                    possibleLabels[i].BackColor = System.Drawing.Color.LightSteelBlue;
                    radioButton.Text = Convert.ToString(VisibleCount());
                
                }
                else
                {
                    currentLabels[i].Visible = false;
                    possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
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
                if (currentLabels[i].Visible == true) count++;
            }
            return count;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
                {
                    checkBoxs[i].Checked = true;
                    currentLabels[i].Visible = true;
                    if (VisibleCount() != 0) radioButton.Text = Convert.ToString(VisibleCount());
                    else radioButton.Clear();
                    possibleLabels[i].BackColor = System.Drawing.Color.LightSteelBlue;
                }
             }
            else
            {
                for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
                {
                    checkBoxs[i].Checked = false;
                    currentLabels[i].Visible = false;
                    if (VisibleCount() != 0) radioButton.Text = Convert.ToString(VisibleCount());
                    else radioButton.Clear();
                    possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
                }
            }
        }
        #endregion

        public ScopeSetupForm()
        {
            InitializeComponent();
            InitPossiblePanel();        

            ModBusUnits.ScopeSetupModbusUnit.RequestFinished += new EventHandler(EndRequest);
        }

        private ushort CalcCurrentParams()
                {
                    ushort u = 0;
                    for (int i1 = 0; i1 < ScopeSysType.ChannelNames.Count; i1++)
                    {
                        if (currentLabels[i1].Visible) { u++; }
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
                nowScopeCount = Convert.ToUInt16(chCountRadioButton.Text);
                if (nowScopeCount < 1 || nowScopeCount > 32)
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
                nowMaxChannelCount = Convert.ToUInt16(radioButton.Text);
                if (nowMaxChannelCount < 1 || nowMaxChannelCount > 32)
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
                nowHystory = Convert.ToUInt16(hystoryRadioButton.Text);
                if (nowHystory < 1 || nowHystory > 99)
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
                nowOscFreq = Convert.ToUInt16(oscFreqRadioButton.Text);
                if (nowOscFreq < 1 || nowOscFreq > 1000)
                {
                    MessageBox.Show("Ошибка в поле Предыстория");
                    oscFreqRadioButton.Clear();
                    return;
                }
            }
        }

        //Размер под осциллограммы:
        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)  // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

 
        private void reloadBtn_Click(object sender, EventArgs e)
        {
            List<ushort> ChFormats = ChannelFormats(); 
            List<ushort> ChannelAdd = ChannelAddrs();               
        }
        #endregion
   
        //**************ИЗМЕНЕНИЕ КОНФИГУРАЦИИ ОСЦИЛЛОГРАФА *********************************************//
        //***********************************************************************************************//
        //***********************************************************************************************//
        ushort[] newOscillConfig = new ushort[40];
        ushort[] OscillConfig = new ushort[1280];
        int[] ChannelSeries = new int[32];

        int writeConfigStep = 0;
        ushort writeStep = 0;

        //Конфигурирование осциллограммы 
        #region
        private List<ushort> ChannelFormats()
        {
            int j = 0;
            List<ushort> l = new List<ushort>();
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 3) { ChannelSeries[j++] = i; l.Add(ScopeSysType.ChannelFormats[i]); }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 2) { ChannelSeries[j++] = i; l.Add(ScopeSysType.ChannelFormats[i]); }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 1) { ChannelSeries[j++] = i; l.Add(ScopeSysType.ChannelFormats[i]); }
            }
            if (l.Count > nowMaxChannelCount) { l.Clear(); }

            return l;
        }
 
        // Channel Addrs
        private List<ushort> ChannelAddrs()
        {
            List<ushort> l = new List<ushort>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelAddrs[ChannelSeries[j++]]); }
            }
            if (l.Count > nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        // OscilSize
        public uint OscilSize(uint AllSize)
        {
            uint count64 = 0, count32 = 0, count16 = 0;
            uint SampleSize;
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 3) { count64++; }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 2) { count32++; }
            }
            for (int i = 0; i < ScopeSysType.ChannelFormats.Count; i++)
            {
                if (currentLabels[i].Visible && (ScopeSysType.ChannelFormats[i] >> 8) == 1) { count16++; }
            }

            SampleSize = count64 * 8 + count32 * 4 + count16 * 2;
            if ((count64 != 0 || count32 != 0) && count16 % 2 != 0) { SampleSize += 2; } // Выравнивание на 4 байта

            uint OscS = (AllSize * 1024) / Convert.ToUInt32(nowScopeCount);

            while (OscS % 64 != 0 || OscS % SampleSize != 0)   // 
            { 		
                OscS--;
            }
                    
            return (OscS);
        }

        //OscilEnable
        private ushort OscilEnable()
        {
            ushort status = 0;
            if (!enaScopeCheckBox.Checked) status = 0;                              //Осциллограффирование отключено 
            if (enaScopeCheckBox.Checked && !checkBox1.Checked) status = 1;          //Осциллограффирование включено но без сокранения на карту пмяти
            if (enaScopeCheckBox.Checked && checkBox1.Checked) status = 2;           //Осциллограффирование включено с сохранением на карту пмяти
            return status;
        }

        // Channel Names
        private List<string> ChNames()
        {
            List<string> ChName = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (currentLabels[i].Visible) { ChName.Add(ScopeSysType.ChannelNames[ChannelSeries[j++]]); }
            }
            if (ChName.Count > nowMaxChannelCount) { ChName.Clear(); }

            return ChName;
        }
       //////////////////////////////////////////
       //For Comtrade 
        private List<string> ChPhase()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelPhase.Count; i++)
            {
                if (currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelPhase[ChannelSeries[j++]]); }
            }
            if (l.Count > nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<string> ChCCBM()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelCCBM.Count; i++)
            {
                if (currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelCCBM[ChannelSeries[j++]]); }
            }
            if (l.Count > nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<string> ChDemension()
        {
            List<string> l = new List<string>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelDimension.Count; i++)
            {
                if (currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelDimension[ChannelSeries[j++]]); }
            }
            if (l.Count > nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        private List<ushort> ChTypeAD()
        {
            List<ushort> l = new List<ushort>();
            for (int i = 0, j = 0; i < ScopeSysType.ChannelTypeAD.Count; i++)
            {
                if (currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelTypeAD[ChannelSeries[j++]]); }
            }
            if (l.Count > nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        #endregion


        private void CalcNewOscillConfig(ushort writeStep)  
        {
            newOscillConfig = new ushort[40];

            newOscillConfig[0] = 1; 
            newOscillConfig[1] = writeStep;
            for (int i = 0; i < 32; i++)
            {           
                newOscillConfig[2 + i] = OscillConfig[i + 32*writeStep];
            }
            newOscillConfig[34] = 1; 
        }

        //Конфигурирование параметрв осциллограммы 
        private void CalcOscillConfig()  
        {
            OscillConfig = new ushort[1280];

            List<ushort> ChFormats = ChannelFormats();          //Идентификатор формата данных в каналах

            for (int i = 0; i < 32; i++)
            {
                if (i < ChFormats.Count) { OscillConfig[i] = ChFormats[i]; }
                else { OscillConfig[i] = 0; }
            }           
            
            List<ushort> ChAddrs = ChannelAddrs();          //Адреса
            
            for (int i = 0; i < 32; i++)
            {
                if (i < ChAddrs.Count) { OscillConfig[i + 32] = ChAddrs[i]; }
                else { OscillConfig[i] = 0; }
            }

            OscillConfig[64] = Convert.ToUInt16((OscilSize(ScopeSysType.OscilAllSize) << 16) >> 16);  //размер под осциллограмму 
            OscillConfig[65] = Convert.ToUInt16(OscilSize(ScopeSysType.OscilAllSize) >> 16);
                                        
            OscillConfig[66] = nowScopeCount;            //Колличество формируемых осциллограмм
            OscillConfig[67] = nowMaxChannelCount;       //Колличество каналов
            OscillConfig[68] = nowHystory;               //Предыстория
            OscillConfig[69] = nowOscFreq;               //Как часто нужно записывать данные 
            OscillConfig[70] = OscilEnable();            //Включен или выключен осциллограф и нужно ли выполнять запись в память 

            if (OscilEnable() == 2)
            {
                //Дополнительные параметры 
                //Запись названия канала
                List<string> ChName = ChNames();                //Название каналов в Cp1251

                for (int i = 0; i < ChName.Count; i++) 
                { 
                    string ChNameString = ChName[i];
                    byte[] ChNameStr = new Byte[32];
                    byte[] TempChNameStr = new Byte[32];
                    ChNameStr = Encoding.Default.GetBytes(ChNameString);

                    for (int j = 0; j < 32; j++)
                    {
                        if (j < ChNameString.Length) TempChNameStr[j] = ChNameStr[j];
                        else TempChNameStr[j] = 32;
                    }
                    for (int j = 1; j < 32; j += 2)
                    {
                        OscillConfig[71 + 16 * i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempChNameStr[j - 1]) << 8);
                        OscillConfig[71 + 16 * i + (j / 2)] += Convert.ToUInt16(TempChNameStr[j]);
                    }
                }

                //for Comtrade
                #region
                List<string> ChPhases = ChPhase();                //в Cp1251

                for (int i = 0; i < ChPhases.Count; i++)
                {
                    string ChPhaseString = ChPhases[i];
                    byte[] ChPhaseStr = new Byte[2];
                    byte[] TempChPhaseStr = new Byte[2];
                    ChPhaseStr = Encoding.Default.GetBytes(ChPhaseString);

                    for (int j = 0; j < 2; j++)
                    {
                        if (j < ChPhaseString.Length) TempChPhaseStr[j] = ChPhaseStr[j];
                        else TempChPhaseStr[j] = 32;
                    }
                    for (int j = 1; j < 2; j += 2)
                    {
                        OscillConfig[583 + i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempChPhaseStr[j - 1]) << 8);
                        OscillConfig[583 + i + (j / 2)] += Convert.ToUInt16(TempChPhaseStr[j]);
                    }
                }

                List<string> ChCCBMs = ChCCBM();                // в Cp1251

                for (int i = 0; i < ChCCBMs.Count; i++)
                {
                    string ChCCBMString = ChCCBMs[i];
                    byte[] ChCCBMStr = new Byte[16];
                    byte[] TempChCCBMStr = new Byte[16];
                    ChCCBMStr = Encoding.Default.GetBytes(ChCCBMString);

                    for (int j = 0; j < 16; j++)
                    {
                        if (j < ChCCBMString.Length) TempChCCBMStr[j] = ChCCBMStr[j];
                        else TempChCCBMStr[j] = 32;
                    }
                    for (int j = 1; j < 16; j += 2)
                    {
                        OscillConfig[615 + i * 8 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempChCCBMStr[j - 1]) << 8);
                        OscillConfig[615 + i * 8 + (j / 2)] += Convert.ToUInt16(TempChCCBMStr[j]);
                    }
                }
                List<string> ChDemensions = ChDemension();                //в Cp1251

                for (int i = 0; i < ChDemensions.Count; i++)
                {
                    string ChDemensionString = ChDemensions[i];
                    byte[] ChDemensionStr = new Byte[8];
                    byte[] TempChDemensionStr = new Byte[8];
                    ChDemensionStr = Encoding.Default.GetBytes(ChDemensionString);

                    for (int j = 0; j < 8; j++)
                    {
                        if (j < ChDemensionString.Length) TempChDemensionStr[j] = ChDemensionStr[j];
                        else TempChDemensionStr[j] = 32;
                    }
                    for (int j = 1; j < 8; j += 2)
                    {
                        OscillConfig[871 + i * 4 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempChDemensionStr[j - 1]) << 8);
                        OscillConfig[871 + i * 4 + (j / 2)] += Convert.ToUInt16(TempChDemensionStr[j]);
                    }
                }

                List<ushort> ChaTypeAD = ChTypeAD();          //

                for (int i = 0; i < ChaTypeAD.Count; i++)
                {
                    if (i < ChaTypeAD.Count) { OscillConfig[999 + i] = ChaTypeAD[i]; }
                    else { OscillConfig[999 + i] = 0; }
                }  

                String StationName = ScopeSysType.StationName;                //

                byte[] StationNameStr = new Byte[32];
                byte[] TempStationNameStr = new Byte[32];
                StationNameStr = Encoding.Default.GetBytes(StationName);

                for (int j = 0; j < 32; j++)
                {
                    if (j < StationName.Length) TempStationNameStr[j] = StationNameStr[j];
                    else TempStationNameStr[j] = 32;
                }
                for (int j = 1; j < 32; j += 2)
                {
                    OscillConfig[1031 +  (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempStationNameStr[j - 1]) << 8);
                    OscillConfig[1031 +  (j / 2)] += Convert.ToUInt16(TempStationNameStr[j]);
                }

                String RecordingID = ScopeSysType.RecordingDevice;            //

                byte[] RecordingIDStr = new Byte[16];
                byte[] TempRecordingIDStr = new Byte[16];
                RecordingIDStr = Encoding.Default.GetBytes(RecordingID);

                for (int j = 0; j < 16; j++)
                {
                    if (j < RecordingID.Length) TempRecordingIDStr[j] = RecordingIDStr[j];
                    else TempRecordingIDStr[j] = 32;
                }
                for (int j = 1; j < 16; j += 2)
                {
                    OscillConfig[1047 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempRecordingIDStr[j - 1]) << 8);
                    OscillConfig[1047 + (j / 2)] += Convert.ToUInt16(TempRecordingIDStr[j]);
                }

                String TimeCode = ScopeSysType.TimeCode;     //

                byte[] TimeCodeStr = new Byte[16];
                byte[] TempTimeCodeStr = new Byte[16];
                TimeCodeStr = Encoding.Default.GetBytes(TimeCode);

                for (int j = 0; j < 8; j++)
                {
                    if (j < TimeCode.Length) TempTimeCodeStr[j] = TimeCodeStr[j];
                    else TempTimeCodeStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    OscillConfig[1055 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempTimeCodeStr[j - 1]) << 8);
                    OscillConfig[1055 + (j / 2)] += Convert.ToUInt16(TempTimeCodeStr[j]);
                }

                String LocalCode = ScopeSysType.TimeCode;     //

                byte[] LocalCodeStr = new Byte[16];
                byte[] TempLocalCodeStr = new Byte[16];
                LocalCodeStr = Encoding.Default.GetBytes(LocalCode);

                for (int j = 0; j < 8; j++)
                {
                    if (j < LocalCode.Length) TempLocalCodeStr[j] = LocalCodeStr[j];
                    else TempLocalCodeStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    OscillConfig[1059 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempLocalCodeStr[j - 1]) << 8);
                    OscillConfig[1059 + (j / 2)] += Convert.ToUInt16(TempLocalCodeStr[j]);
                }

                String tmqCode = ScopeSysType.tmqCode;     //

                byte[] tmqCodeStr = new Byte[16];
                byte[] TemptmqCodeStr = new Byte[16];
                tmqCodeStr = Encoding.Default.GetBytes(tmqCode);

                for (int j = 0; j < 8; j++)
                {
                    if (j < tmqCode.Length) TemptmqCodeStr[j] = tmqCodeStr[j];
                    else TemptmqCodeStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    OscillConfig[1063 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TemptmqCodeStr[j - 1]) << 8);
                    OscillConfig[1063 + (j / 2)] += Convert.ToUInt16(TemptmqCodeStr[j]);
                }

                String leapsec = ScopeSysType.leapsec;     //

                byte[] leapsecStr = new Byte[16];
                byte[] TempleapsecStr = new Byte[16];
                leapsecStr = Encoding.Default.GetBytes(leapsec);

                for (int j = 0; j < 8; j++)
                {
                    if (j < leapsec.Length) TempleapsecStr[j] = leapsecStr[j];
                    else TempleapsecStr[j] = 32;
                }
                for (int j = 1; j < 8; j += 2)
                {
                    OscillConfig[1067 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempleapsecStr[j - 1]) << 8);
                    OscillConfig[1067 + (j / 2)] += Convert.ToUInt16(TempleapsecStr[j]);
                }
                #endregion
            }
        }

        private void WriteConfigToSystem()
        {
            CalcOscillConfig();
            writeConfigStep = 0;
            CalcNewOscillConfig(writeStep);
            WritePartConfigToSystem();
        }


        private void WritePartConfigToSystem()
        {
            ushort[] partParam = new ushort[8];
            for (int i = 0; i < 8; i++)
            {
                partParam[i] = newOscillConfig[i + writeConfigStep * 8];
            }
            ModBusUnits.ScopeSetupModbusUnit.SetData((ushort)( ScopeSysType.NewConfigAddr + writeConfigStep * 8), 8, partParam);
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
                writeConfigStep++;
                if (writeConfigStep < 5) { WritePartConfigToSystem(); }     //Отправляю новую конфигурацию 
                else 
                {
                    if (writeStep < 39)         { writeStep++; CalcNewOscillConfig(writeStep); writeConfigStep = 0; WritePartConfigToSystem(); }
                    else
                    {
                        writeStep = 0;
                        MessageBox.Show("Конфигурация осциллографа была изменена!", "Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ScopeConfig.ChangeScopeConfig = true;
                    }
                }       
            }
        }

        private void writeToSystemBtn_Click(object sender, EventArgs e)
        {
            if (!ModBusClient.ModBusOpened)
            { MessageBox.Show("Соединение с системой не установлено!", "Настройка осциллографа", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            if (MessageBox.Show("Изменить конфигурацию осциллографа?\n" +
                                "Все текущие осциллограммы будут удалены из памяти системы!",
                                "Настройка осциллографа", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes
                ) { return; }

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

        delegate void NoParamDelegate();
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
            List<string> OscilChannelNames = new List<string>();
            List<ushort> OscilChannelAddrs = new List<ushort>();
            List<ushort> OscilChannelFormats = new List<ushort>();
            OscilChannelNames = new List<string>();
            OscilChannelAddrs = new List<ushort>();
            OscilChannelFormats = new List<ushort>();
             
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                checkBoxs[i].Checked = false;
                currentLabels[i].Visible = false;
                possibleLabels[i].BackColor = System.Drawing.SystemColors.ButtonHighlight;
            }
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".ocxml"; // Default file extension
            ofd.Filter = "Oscil Configuration XML|*.ocxml|XML|*.xml|All files|*.*"; // Filter files by extension
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.xmlFileName = ofd.FileName;   
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

            for (int i = 0; i < ScopeSysType.OscilChannelNames.Count ; i++)
            {
                for(int j = 0; j < ScopeSysType.ChannelNames.Count; j++)
                {    
                    if (ScopeSysType.OscilChannelFormats[i] == ScopeSysType.ChannelFormats[j] && ScopeSysType.OscilChannelAddrs[i] == ScopeSysType.ChannelAddrs[j])
                    {
                        checkBoxs[j].Checked = true;
                        currentLabels[j].Visible = true;
                        possibleLabels[j].BackColor = System.Drawing.Color.LightSteelBlue;
                        radioButton.Text = Convert.ToString(VisibleCount());
                    }
                }
            }
        }
        #endregion

        //Сохранение осциллограммы в файл
        #region
        private void saveButton2_Click(object sender, EventArgs e)
        {
            if (nowMaxChannelCount != ChNames().Count)
            {
                MessageBox.Show("Количество осциллографируемых и выбранных каналов не совпадает");
                return;
            }
            
            List<string> paramAddrStrs = new List<string>();
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".ocxml"; // Default file extension
            sfd.Filter = "Oscil Configuration XML|*.ocxml|XML|*.xml"; // Filter files by extension
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.xmlFileName = sfd.FileName;

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                xmlOut.Formatting = Formatting.Indented;
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("Setup");
                /////////////////////////////////////////////////////////////

                xmlOut.WriteStartElement("Oscil");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowScopeCount));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Channel");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowMaxChannelCount));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Story");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowHystory));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Frequency");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowOscFreq));
                xmlOut.WriteEndElement();
                             
                for (int i = 0, j = 0; i < possibleLabels.Count; i++)
                {
                    if(currentLabels[i].Visible == true)
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

    }
}
