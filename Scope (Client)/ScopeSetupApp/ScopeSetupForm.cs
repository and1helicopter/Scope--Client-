﻿using System;
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
       
        private List<Label> possibleLabels;
        private List<Label> currentLabels;
        private List<CheckBox> checkBoxs;

        public void InitPossiblePanel()
        {
            possibleLabels = new List<Label>();
            checkBoxs = new List<CheckBox>();
            currentLabels = new List<Label>();
           // oscilAllSize = ScopeSysType.OscilAllSize;

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

                enaScopeCheckBox.Checked = ScopeConfig.ScopeEnabled;
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
            if (checkBox1.Checked) 
            {
                for (writeNameStep = 0; writeNameStep < nowMaxChannelCount + 1; writeNameStep++)
                {
                    CalcNewOscillConfig(writeNameStep);
                    for (int i = 0; i < 48; i++) Console.WriteLine("[{0}] = {1}", i, newOscillConfig[i]);
                    Console.WriteLine("____________ \n");
                }
            }
            else
            {
                CalcNewOscillConfig( writeNameStep );
                for(int i = 0; i < 48; i++) Console.WriteLine("[{0}] = {1}",i , newOscillConfig[i]);
            }
            
            
        }
   
        //**************ИЗМЕНЕНИЕ КОНФИГУРАЦИИ ОСЦИЛЛОГРАФА *********************************************//
        //***********************************************************************************************//
        //***********************************************************************************************//
        ushort[] newOscillConfig = new ushort[48];
        int writeConfigStep = 0;
        ushort writeNameStep = 0;
 
        // Channel Addrs
        private List<ushort> ChannelAddrs()
        {
            List<ushort> l = new List<ushort>();
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (currentLabels[i].Visible) { l.Add(ScopeSysType.ChannelAddrs[i]); }
            }

            if (l.Count > nowMaxChannelCount) { l.Clear(); }

            return l;
        }

        // OscilSize
        private uint OscilSize(uint AllSize)
        {
            uint OscilMaxMultiplicity;
            
            OscilMaxMultiplicity = ScopeSysType.ChannelFormats.Max();

            if (OscilMaxMultiplicity == 16) OscilMaxMultiplicity = 2;
            if (OscilMaxMultiplicity == 32) OscilMaxMultiplicity = 4;
            if (OscilMaxMultiplicity == 64) OscilMaxMultiplicity = 8;
            

            uint OscS = (AllSize * 1024) / Convert.ToUInt32(nowScopeCount);
            while (OscS % OscilMaxMultiplicity != 0 || OscS % nowMaxChannelCount != 0)   //Проверка на кратность слова с учетом колличества каналов  
            { 		
                OscS--;
            }
            oscSize.Text = "Размер под осциллограмму: " + OscS.ToString() + " байт";
                        
            return (OscS);
        }
       //////////////////////////////////////////
        // Channel Formats 
        private List<string> ChannelFormats()
        {
            List<string> ChannelFormat = new List<string>();
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (currentLabels[i].Visible) 
                {
                    ChannelFormat.Add(ScopeSysType.ChannelFormatsName[i]);

                }
            }

            if (ChannelFormat.Count > nowMaxChannelCount) { ChannelFormat.Clear(); }

            return ChannelFormat;
        }

        //OscilEnable
        private ushort OscilEnable() {
            ushort status = 0; 
            if (!enaScopeCheckBox.Checked ) status = 0;                              //Осциллограффирование отключено 
            if (enaScopeCheckBox.Checked && !checkBox1.Checked) status = 1;          //Осциллограффирование включено но без сокранения на карту пмяти
            if (enaScopeCheckBox.Checked && checkBox1.Checked) status = 2;           //Осциллограффирование включено с сохранением на карту пмяти
            return status;
        }
      ///////////////////////////////
        // Channel Names
        private List<string> ChNames()
        {
            List<string> ChName = new List<string>();
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (currentLabels[i].Visible) { ChName.Add(ScopeSysType.ChannelNames[i]); }
            }

            if (ChName.Count > nowMaxChannelCount) { ChName.Clear(); }

            return ChName;
        }

        private void CalcNewOscillConfig(ushort NumFrame)  
        {
            newOscillConfig = new ushort[48];

            if (NumFrame == 0) 
            {            
                newOscillConfig[0] = 0x0001;                    //Флаг изменения конфигурации
                newOscillConfig[1] = NumFrame;                  //Номер кадра

                List<ushort> ChAddrs = ChannelAddrs();          //Адреса
            
                if (nowMaxChannelCount < ChAddrs.Count || nowMaxChannelCount > ChAddrs.Count)
                {
                    MessageBox.Show("Количество осциллографируемых и выбранных каналов не совпадает");
                    return;
                }

                for (int i = 0; i < 32; i++)
                {
                    if (i < ChAddrs.Count) { newOscillConfig[2 + i] = ChAddrs[i]; }
                    else { newOscillConfig[2 + i] = 0; }
                }

                newOscillConfig[34] = Convert.ToUInt16((OscilSize(ScopeSysType.OscilAllSize) << 16) >> 16); 
                newOscillConfig[35] = Convert.ToUInt16(OscilSize(ScopeSysType.OscilAllSize) >> 16);
                                        
                newOscillConfig[36] = nowScopeCount;            //Колличество формируемых осциллограмм
                newOscillConfig[37] = nowMaxChannelCount;       //Колличество каналов
                newOscillConfig[38] = nowHystory;               //Предыстория
                newOscillConfig[39] = nowOscFreq;               //Как часто нужно записывать данные 
                newOscillConfig[40] = OscilEnable();            //Включен или выключен осциллограф и нужно ли выполнять запись в память 
 
                
                newOscillConfig[47] = 0x0001;                   //Флаг изменения конфигурации
            }

            if (OscilEnable() == 2 && NumFrame != 0)
            {
                 newOscillConfig[0] = 0x0001;
                 newOscillConfig[1] = NumFrame;                  //Номер кадра (канала) 
                 //Запись названия канала
                 List<string> ChName = ChNames();                //Название каналов в Cp1251

                 string ChNameString = ChName[NumFrame - 1];
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
                    newOscillConfig[2 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(TempChNameStr[j - 1]) << 8);
                    newOscillConfig[2 + (j / 2)] += Convert.ToUInt16(TempChNameStr[j]);
                 }
                               
                 //
                 List<string> ChFormats = ChannelFormats();          //Идентификатор формата данных в каналах

                 string ChFormatString = ChFormats[NumFrame - 1];
                 byte[] ChFormatStr = new Byte[16];
                 byte[] TempChFormatStr = new Byte[16];
                 ChFormatStr = Encoding.Default.GetBytes(ChFormatString);

                 if (ScopeSysType.ChannelFormats[NumFrame - 1] == 16) { TempChFormatStr[0] = 0; TempChFormatStr[1] = 0; }
                 if (ScopeSysType.ChannelFormats[NumFrame - 1] == 32) { TempChFormatStr[0] = 0; TempChFormatStr[1] = 1; }
                 if (ScopeSysType.ChannelFormats[NumFrame - 1] == 64) { TempChFormatStr[0] = 1; TempChFormatStr[1] = 1; }

                 for (int l = 0; l < ChFormatStr.Length; l++)     
                 {
                    TempChFormatStr[l + 2] = ChFormatStr[l];
                 } 
                        
                 for (int l = 1; l < 8; l += 2)
                 {
                    newOscillConfig[18 + l / 2] = Convert.ToUInt16(Convert.ToUInt32(TempChFormatStr[l - 1]) << 8);
                    newOscillConfig[18 + l / 2] += Convert.ToUInt16(TempChFormatStr[l]);  
                 }
                       
                 newOscillConfig[47] = 0x0001;  
            }
        }

        private void WriteConfigToSystem()
        {
            writeConfigStep = 0;
            CalcNewOscillConfig( writeNameStep );
            WritePartConfigToSystem();
        }


        private void WritePartConfigToSystem()
        {
            ushort[] partParam = new ushort[8];
            for (int i = 0; i < 8; i++)
            {
                partParam[i] = newOscillConfig[i + writeConfigStep * 8];
            }
            ModBusUnits.ScopeSetupModbusUnit.SetData((ushort)( ScopeSysType.ParamLoadDataAddr + writeConfigStep * 8), 8, partParam);
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
                if (writeConfigStep < 6) { WritePartConfigToSystem(); }     //Отправляю новую конфигурацию 
                else 
                {
                    if (OscilEnable() == 2 && writeNameStep < nowMaxChannelCount ) { writeNameStep++; WriteConfigToSystem(); }
                    else
                    {
                        writeNameStep = 0;
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
        private void openButton2_Click(object sender, EventArgs e)
        {
           // List<string> ChannelNamesTemp = ChNames();
            List<string> ChannelNamesTemp = new List<string>();
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                ChannelNamesTemp.Add(ScopeSysType.ChannelNames[i]);
            }



            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".xml"; // Default file extension
            ofd.Filter = "XML|*.xml"; // Filter files by extension
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                ScopeSysType.xmlFileName = ofd.FileName;
                
                try
                {
                    ScopeSysType.InitScopeSysType();
                    for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++) Console.WriteLine(ScopeSysType.ChannelNames[i]);
                }
                catch
                {
                    MessageBox.Show("Ошибка загрузки данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (ChannelNamesTemp.SequenceEqual(ScopeSysType.ChannelNames)){;}
            else MessageBox.Show("Конфигурация не соответсвует");

            for (int i = possibleLabels.Count - 1; i >= 0; i--)
            {            
                possibleTableLayoutPanel.Controls.Remove(possibleLabels[i]); 
                possibleLabels.Remove(possibleLabels[i]);  
                checkBoxs.Remove(checkBoxs[i]);
                currentLabels.Remove(currentLabels[i]); 
            }
          
            if (ScopeSysType.OscilCount != 0) chCountRadioButton.Text = Convert.ToString(ScopeSysType.OscilCount);
            else chCountRadioButton.Clear();
            if (ScopeSysType.HystoryCount != 0) hystoryRadioButton.Text = Convert.ToString(ScopeSysType.HystoryCount);
            else hystoryRadioButton.Clear();
            if (ScopeSysType.FrequncyCount != 0) oscFreqRadioButton.Text = Convert.ToString(ScopeSysType.FrequncyCount);
            else oscFreqRadioButton.Clear();
            if (ScopeSysType.OscilCount != 0) chCountRadioButton.Text = Convert.ToString(ScopeSysType.OscilCount);
            else chCountRadioButton.Clear();
            radioButton.Clear();
            

            InitPossiblePanel() ;
 
            for (int i = 0; i < ScopeSysType.ChannelNames.Count; i++)
            {
                if (ScopeSysType.ChannelChecked[i] == true)
                {
                    checkBoxs[i].Checked = true;
                    currentLabels[i].Visible = true;
                    possibleLabels[i].BackColor = System.Drawing.Color.LightSteelBlue;
                    radioButton.Text = Convert.ToString(VisibleCount());

                }
            }

            
        }

        private void saveButton2_Click(object sender, EventArgs e)
        {
            if (nowMaxChannelCount != ChNames().Count)
            {
                MessageBox.Show("Количество осциллографируемых и выбранных каналов не совпадает");
                return;
            }
            
            List<string> paramAddrStrs = new List<string>();
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xml"; // Default file extension
            sfd.Filter = "XML|*.xml"; // Filter files by extension
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                xmlOut.Formatting = Formatting.Indented;
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("Setup");
                /////////////////////////////////////////////////////////////
                xmlOut.WriteStartElement("TimeStamp");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.TimeStampAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilStatus");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.OscilStatusAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ScopeCount");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.ScopeCountAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Hystory");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.HystoryAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ChannelCount");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.ChannelCountAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("DataStart");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.DataStartAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilFreq");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.OscilFreqAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("LoadOscilStart");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.LoadOscilStartAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ParamLoadConfig");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.ParamLoadConfigAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ParamLoadData");
                xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.ParamLoadDataAddr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("MeasureParams");
                xmlOut.WriteAttributeString("Count", Convert.ToString(possibleLabels.Count));
                xmlOut.WriteEndElement();
                             
                for (int i = 0; i < possibleLabels.Count; i++)
                {
                    xmlOut.WriteStartElement("MeasureParam" + (i + 1).ToString());

                    xmlOut.WriteAttributeString("Name", ScopeSysType.ChannelNames[i]);
                    xmlOut.WriteAttributeString("Addr", Convert.ToString(ScopeSysType.ChannelAddrs[i]));
                    xmlOut.WriteAttributeString("Color", Convert.ToString(ScopeSysType.ChannelColors[i].ToArgb()));
                    xmlOut.WriteAttributeString("Format", Convert.ToString(ScopeSysType.ChannelFormats[i]));
                    xmlOut.WriteAttributeString("FormatName", Convert.ToString(ScopeSysType.ChannelFormatsName[i]));
                    xmlOut.WriteAttributeString("StepLine", Convert.ToString(ScopeSysType.ChannelStepLines[i]));
                    xmlOut.WriteAttributeString("Checked", Convert.ToString(checkBoxs[i].Checked));
                    
                    xmlOut.WriteEndElement();
                }

                xmlOut.WriteStartElement("Oscil");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowScopeCount));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Channel");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowMaxChannelCount));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("History");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowHystory));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Frequency");
                xmlOut.WriteAttributeString("Count", Convert.ToString(nowOscFreq));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilAllSize");
                xmlOut.WriteAttributeString("Count", Convert.ToString(oscilAllSize));
                xmlOut.WriteEndElement();



                /////////////////////////////////////////////////////////////
                xmlOut.WriteEndElement();
                xmlOut.WriteEndDocument();
                xmlOut.Close();
                fs.Close();
            }
        }
    }
}
