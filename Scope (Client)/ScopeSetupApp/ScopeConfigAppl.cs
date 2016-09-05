using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using ADSPLibrary;

namespace ScopeSetupApp
{
    public partial class ScopeConfigForm : Form
    {
        //Динамически создаваемые контролы
        //Названия
        List<TextBox> nameTextBoxs = new List<TextBox>();

        //Адреса
        List<TextBox> addrTextBoxs = new List<TextBox>();

        //Цвета
        List<Label> colorLabels = new List<Label>();

        //форматы данных
        List<ComboBox> formatComboBoxNumeric = new List<ComboBox>();
        List<ComboBox> formatComboBox = new List<ComboBox>();

        //Размерность физической величины 
        List<ComboBox> dimensionComboBox = new List<ComboBox>();

        //Размерность физической величины 
        List<ComboBox> AnalogDigitalComboBox = new List<ComboBox>();

        List<TextBox> minTextBoxs = new List<TextBox>();
        List<TextBox> maxTextBoxs = new List<TextBox>();

        object[] format = new object[]{
            "Percent",
            "uint16",
            "int16",
            "Freq standart",
            "8.8",
            "0.16",
            "Slide",
            "Digits",
            "RegulMode",
            "AVR type",
            "Int/10",
            "Hex",
            "*0.135 (Uf)",
            "FreqNew",
            "Current trans",
            "trans alarm",
            "int/8",
            "uint/1000",
            "percent/4",
            "FreqNew2",
            "Percent upp",
            "Freq UPTF",
        };

        object[] sizeFormat = new object[]{
            "16",
            "32",
            "64",
        };

        object[] dimension = new object []{
            "NONE",
        };
        //Сглаживание 
        List<ComboBox> stepLineCheckBoxs = new List<ComboBox>();

        //Удаление 
        List<Button> removeButtons = new List<Button>();

        private void AddParamLine(string lineName, string lineDimension, int lineAddr, Color clr, int formatData, string formatName, int stepLine, int lineTypeAD, int min, int max)
        {
            int i;

            nameTextBoxs.Add(new TextBox());
            i = nameTextBoxs.Count - 1;
            nameTextBoxs[i].Dock = DockStyle.None;
            nameTextBoxs[i].Font = new Font("Arial", 9);
            nameTextBoxs[i].AutoSize = false;
            nameTextBoxs[i].Left = 10;
            nameTextBoxs[i].Top = 3 + 26 * i;
            nameTextBoxs[i].Width = 140;
            nameTextBoxs[i].Height = 24;
            nameTextBoxs[i].Text = lineName;

            dimensionComboBox.Add(new ComboBox());
            dimensionComboBox[i].Tag = i;
            dimensionComboBox[i].Dock = DockStyle.None;
            dimensionComboBox[i].Font = new Font("Arial", 9);
            dimensionComboBox[i].Items.AddRange(dimension);
            dimensionComboBox[i].Left = 153;
            dimensionComboBox[i].Top = 3 + 26 * i;
            dimensionComboBox[i].Width = 55;
            dimensionComboBox[i].Text = lineDimension;
            dimensionComboBox[i].DropDownStyle = ComboBoxStyle.DropDown;

            addrTextBoxs.Add(new TextBox());
            addrTextBoxs[i].Dock = DockStyle.None;
            addrTextBoxs[i].Font = new Font("Arial", 9);
            addrTextBoxs[i].AutoSize = false;
            addrTextBoxs[i].Left = 211;
            addrTextBoxs[i].Top = 3 + 26 * i;
            addrTextBoxs[i].Width = 55;
            addrTextBoxs[i].Height = 24;
            addrTextBoxs[i].Text = "0x" + lineAddr.ToString("X4");
            addrTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            colorLabels.Add(new Label());
            colorLabels[i].Tag = i;
            colorLabels[i].Dock = DockStyle.None;
            colorLabels[i].BorderStyle = BorderStyle.FixedSingle;
            colorLabels[i].Width = colorLabels[i].Height = addrTextBoxs[i].Height;
            colorLabels[i].Left = 269;
            colorLabels[i].Top = 3 + 26 * i;
            colorLabels[i].BackColor = clr;
            colorLabels[i].Click += new EventHandler(colorLabel_Click);

            formatComboBoxNumeric.Add(new ComboBox());
            formatComboBoxNumeric[i].Tag = i;
            formatComboBoxNumeric[i].Dock = DockStyle.None;
            formatComboBoxNumeric[i].Font = new Font("Arial", 9);
            formatComboBoxNumeric[i].Items.AddRange(sizeFormat);
            formatComboBoxNumeric[i].Width = 40;
            formatComboBoxNumeric[i].Left = 297;
            formatComboBoxNumeric[i].Top = 3 + 26 * i;
            formatComboBoxNumeric[i].Text = Convert.ToString(formatData);
            formatComboBoxNumeric[i].DropDownStyle = ComboBoxStyle.DropDownList;
            
            formatComboBox.Add(new ComboBox());
            formatComboBox[i].Tag = i;
            formatComboBox[i].Dock = DockStyle.None;
            formatComboBox[i].Font = new Font("Arial", 9);
            formatComboBox[i].DropDownStyle = ComboBoxStyle.DropDown;
            formatComboBox[i].Items.AddRange(format);
            formatComboBox[i].Width = 90;
            formatComboBox[i].Left = 340;
            formatComboBox[i].Top = 3 + 26 * i;
            formatComboBox[i].Text = formatName;
            
            
            stepLineCheckBoxs.Add(new ComboBox());
            stepLineCheckBoxs[i].Tag = i;
            stepLineCheckBoxs[i].Dock = DockStyle.None;
            stepLineCheckBoxs[i].Font = new Font("Arial", 9);
            stepLineCheckBoxs[i].Left = 433;
            stepLineCheckBoxs[i].Width = 60;
            stepLineCheckBoxs[i].DropDownStyle = ComboBoxStyle.DropDownList;
            stepLineCheckBoxs[i].Items.Add("Smooth");
            stepLineCheckBoxs[i].Items.Add("Step");
            stepLineCheckBoxs[i].SelectedIndex = stepLine;
            stepLineCheckBoxs[i].Top = 3 + 26 * i;

            AnalogDigitalComboBox.Add(new ComboBox());
            AnalogDigitalComboBox[i].Tag = i;
            AnalogDigitalComboBox[i].Dock = DockStyle.None;
            AnalogDigitalComboBox[i].Font = new Font("Arial", 9);
            AnalogDigitalComboBox[i].Left = 496;
            AnalogDigitalComboBox[i].Width = 60;
            AnalogDigitalComboBox[i].DropDownStyle = ComboBoxStyle.DropDownList;
            AnalogDigitalComboBox[i].Items.Add("Analog");
            AnalogDigitalComboBox[i].Items.Add("Digital");
            AnalogDigitalComboBox[i].SelectedIndex = lineTypeAD;
            AnalogDigitalComboBox[i].Top = 3 + 26 * i;

            minTextBoxs.Add(new TextBox());
            minTextBoxs[i].Dock = DockStyle.None;
            minTextBoxs[i].Font = new Font("Arial", 9);
            minTextBoxs[i].AutoSize = false;
            minTextBoxs[i].Left = 559;
            minTextBoxs[i].Top = 3 + 26 * i;
            minTextBoxs[i].Width = 95;
            minTextBoxs[i].Height = 24;
            minTextBoxs[i].Text = min.ToString();
            minTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            maxTextBoxs.Add(new TextBox());
            maxTextBoxs[i].Dock = DockStyle.None;
            maxTextBoxs[i].Font = new Font("Arial", 9);
            maxTextBoxs[i].AutoSize = false;
            maxTextBoxs[i].Left = 657;
            maxTextBoxs[i].Top = 3 + 26 * i;
            maxTextBoxs[i].Width = 95;
            maxTextBoxs[i].Height = 24;
            maxTextBoxs[i].Text = max.ToString();
            maxTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            removeButtons.Add(new Button());
            removeButtons[i].Text = "Удалить";
            removeButtons[i].Tag = i;
            removeButtons[i].Dock = DockStyle.None;
            removeButtons[i].Left = 753;
            removeButtons[i].Top = 3 + 26 * i;
            removeButtons[i].Click += new EventHandler(deleteButton_Click);

                        
            configPanel.Controls.Add(addrTextBoxs[i]);
            configPanel.Controls.Add(dimensionComboBox[i]);
            configPanel.Controls.Add(nameTextBoxs[i]);
            configPanel.Controls.Add(colorLabels[i]);
            configPanel.Controls.Add(formatComboBoxNumeric[i]);
            configPanel.Controls.Add(formatComboBox[i]);
            configPanel.Controls.Add(stepLineCheckBoxs[i]);
            configPanel.Controls.Add(AnalogDigitalComboBox[i]);
            configPanel.Controls.Add(minTextBoxs[i]);
            configPanel.Controls.Add(maxTextBoxs[i]);
            configPanel.Controls.Add(removeButtons[i]);
        }

        private void DeleteLine(int lineNum)
        {
            if (lineNum >= nameTextBoxs.Count) { return; }
            int i = lineNum;
            for (i = lineNum; i < nameTextBoxs.Count - 1; i++)
            {
                nameTextBoxs[i].Text = nameTextBoxs[i + 1].Text;
                dimensionComboBox[i].Text = dimensionComboBox[i + 1].Text;
                addrTextBoxs[i].Text = addrTextBoxs[i + 1].Text;
                colorLabels[i].BackColor = colorLabels[i + 1].BackColor;
                formatComboBoxNumeric[i].Text = formatComboBoxNumeric[i + 1].Text;
                formatComboBox[i].Text = formatComboBox[i + 1].Text;
                stepLineCheckBoxs[i].SelectedIndex = stepLineCheckBoxs[i + 1].SelectedIndex;
                AnalogDigitalComboBox[i].Text = AnalogDigitalComboBox[i + 1].Text;
                minTextBoxs[i].Text = minTextBoxs[i + 1].Text;
                maxTextBoxs[i].Text = maxTextBoxs[i + 1].Text;
            }
            i = nameTextBoxs.Count - 1;
            configPanel.Controls.Remove(nameTextBoxs[i]);
            configPanel.Controls.Remove(dimensionComboBox[i]);
            configPanel.Controls.Remove(addrTextBoxs[i]);
            configPanel.Controls.Remove(removeButtons[i]);
            configPanel.Controls.Remove(colorLabels[i]);
            configPanel.Controls.Remove(formatComboBoxNumeric[i]);
            configPanel.Controls.Remove(formatComboBox[i]);
            configPanel.Controls.Remove(stepLineCheckBoxs[i]);
            configPanel.Controls.Remove(AnalogDigitalComboBox[i]);
            configPanel.Controls.Remove(minTextBoxs[i]);
            configPanel.Controls.Remove(maxTextBoxs[i]);

            nameTextBoxs.Remove(nameTextBoxs[i]);
            dimensionComboBox.Remove(dimensionComboBox[i]);
            addrTextBoxs.Remove(addrTextBoxs[i]);
            removeButtons.Remove(removeButtons[i]);
            colorLabels.Remove(colorLabels[i]);
            formatComboBoxNumeric.Remove(formatComboBoxNumeric[i]);
            formatComboBox.Remove(formatComboBox[i]);
            stepLineCheckBoxs.Remove(stepLineCheckBoxs[i]);
            AnalogDigitalComboBox.Remove(AnalogDigitalComboBox[i]);
            minTextBoxs.Remove(minTextBoxs[i]);
            maxTextBoxs.Remove(maxTextBoxs[i]);
        }

        public ScopeConfigForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void addLineButton_Click(object sender, EventArgs e)
        {
            AddParamLine("Параметр ", "NONE", addrTextBoxs.Count, Color.Black, 16, "Custom", 1, 0, -1, 1);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int i = (int)((sender as Button).Tag);

            DeleteLine(i);
        }

        private void colorLabel_Click(object sender, EventArgs e)
        {
            ColorDialog cldg = new ColorDialog();
            if (cldg.ShowDialog() == DialogResult.OK)
            {
                colorLabels[(int)((sender as Label).Tag)].BackColor = cldg.Color;
            }
        }

        //Загрузка из файла
        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".xml"; // Default file extension
            ofd.Filter = "XML|*.xml"; // Filter files by extension
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.xmlFileName = ofd.FileName;
                try
                {
                    ScopeSysType.InitScopeSysType();
                }
                catch
                {
                    MessageBox.Show("Ошибка загрузки данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                timeStampTextBox.Text = "0x" + ScopeSysType.TimeStampAddr.ToString("X4");
                OscilStatus_TextBox.Text = "0x" + ScopeSysType.OscilStatusAddr.ToString("X4");
                History_TextBox.Text = "0x" + ScopeSysType.HistoryAddr.ToString("X4");
                ChannelCount_TextBox.Text = "0x" + ScopeSysType.ChannelCountAddr.ToString("X4");
                StartTemp_TextBox.Text = "0x" + ScopeSysType.StartTemptAddr.ToString("X4");
                OscilFreq_TextBox.Text = "0x" + ScopeSysType.OscilFreqAddr.ToString("X4");
                ScopeCount_TextBox.Text = "0x" + ScopeSysType.ScopeCountAddr.ToString("X4");
                OscilLoad_TextBox.Text = "0x" + ScopeSysType.OscilLoadAddr.ToString("X4");
                FlagNeed_ConfigTextBox.Text = "0x" + ScopeSysType.FlagNeedAddr.ToString("X4");
                NewConfig_TextBox.Text = "0x" + ScopeSysType.NewConfigAddr.ToString("X4");
                OscilSizeData_TextBox.Text = "0x" + ScopeSysType.OscilAllSize.ToString("X4");
                //For COMETRADE
                stationName_textBox.Text = ScopeSysType.StationName.ToString();
                recordingDevice_textBox.Text = ScopeSysType.RecordingDevice.ToString();
                nominalFrequency_textBox.Text = ScopeSysType.OscilNominalFrequency.ToString("###");
                sampleRate_textBox.Text = ScopeSysType.OscilSampleRate.ToString("###");
                timeCode_textBox.Text = ScopeSysType.TimeCode.ToString();
                localCode_textBox.Text = ScopeSysType.LocalCode.ToString();
                tmqCode_textBox.Text = ScopeSysType.tmqCode.ToString();
                leapsec_textBox.Text = ScopeSysType.leapsec.ToString();
                
                for (int i = nameTextBoxs.Count - 1; i >= 0 ; i--)
                {
                    configPanel.Controls.Remove(nameTextBoxs[i]);
                    configPanel.Controls.Remove(dimensionComboBox[i]);
                    configPanel.Controls.Remove(addrTextBoxs[i]);
                    configPanel.Controls.Remove(removeButtons[i]);
                    configPanel.Controls.Remove(colorLabels[i]);
                    configPanel.Controls.Remove(formatComboBoxNumeric[i]);
                    configPanel.Controls.Remove(formatComboBox[i]);
                    configPanel.Controls.Remove(stepLineCheckBoxs[i]);
                    configPanel.Controls.Remove(AnalogDigitalComboBox[i]);
                    configPanel.Controls.Remove(minTextBoxs[i]);
                    configPanel.Controls.Remove(maxTextBoxs[i]);

                    nameTextBoxs.Remove(nameTextBoxs[i]);
                    dimensionComboBox.Remove(dimensionComboBox[i]);
                    addrTextBoxs.Remove(addrTextBoxs[i]);
                    removeButtons.Remove(removeButtons[i]);
                    colorLabels.Remove(colorLabels[i]);
                    formatComboBoxNumeric.Remove(formatComboBoxNumeric[i]);
                    formatComboBox.Remove(formatComboBox[i]);
                    stepLineCheckBoxs.Remove(stepLineCheckBoxs[i]);
                    AnalogDigitalComboBox.Remove(AnalogDigitalComboBox[i]);
                    minTextBoxs.Remove(minTextBoxs[i]);
                    maxTextBoxs.Remove(maxTextBoxs[i]);
                }


                for (int i1 = 0; i1 < ScopeSysType.ChannelNames.Count; i1++)
                {
                    AddParamLine(
                                    ScopeSysType.ChannelNames[i1],
                                    ScopeSysType.ChannelDimension[i1],
                                    ScopeSysType.ChannelAddrs[i1],
                                    ScopeSysType.ChannelColors[i1],
                                    ScopeSysType.ChannelFormats[i1],
                                    ScopeSysType.ChannelFormatsName[i1],
                                    ScopeSysType.ChannelStepLines[i1], 
                                    ScopeSysType.ChannelTypeAD[i1],
                                    ScopeSysType.ChannelMin[i1],
                                    ScopeSysType.ChannelMax[i1]  
                                 );
                }
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string timeStampStr, oscilStatusStr, scopeCountStr,
                   historyStr, channelCountStr, dataStartStr,
                   oscilFreqStr, loadOscilStartStr, paramLoadConfigStr,
                   paramLoadDataStr, oscillSizeDataStr;
            List<string> paramAddrStrs = new List<string>();

            if (!AdvanceConvert.StrToInt(timeStampTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле TimeStamp");
                return;
            }
            else
            {
                timeStampStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(OscilStatus_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле oscillStatus");
                return;
            }
            else
            {
                oscilStatusStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(ScopeCount_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле scopeCount");
                return;
            }
            else
            {
                scopeCountStr = AdvanceConvert.uValue.ToString();
            }

            if (!AdvanceConvert.StrToInt(History_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле hystoryText");
                return;
            }
            else
            {
                historyStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(ChannelCount_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле channelCount");
                return;
            }
            else
            {
                channelCountStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(StartTemp_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле dataStart");
                return;
            }
            else
            {
                dataStartStr = AdvanceConvert.uValue.ToString();
            }

            if (!AdvanceConvert.StrToInt(OscilFreq_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле oscilFreq");
                return;
            }
            else
            {
                oscilFreqStr = AdvanceConvert.uValue.ToString();
            }

            if (!AdvanceConvert.StrToInt(OscilLoad_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле loadOscilStart");
                return;
            }
            else
            {
                loadOscilStartStr = AdvanceConvert.uValue.ToString();
            }

            if (!AdvanceConvert.StrToInt(FlagNeed_ConfigTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле paramLoadConfig");
                return;
            }
            else
            {
                paramLoadConfigStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(NewConfig_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле paramLoadData");
                return;
            }
            else
            {
                paramLoadDataStr = AdvanceConvert.uValue.ToString();
            }


            for (int i = 0; i < addrTextBoxs.Count; i++)
            {
                if (!AdvanceConvert.StrToInt(addrTextBoxs[i].Text))
                {
                    MessageBox.Show("Ошибка в поле адреса параметра\n" + nameTextBoxs[i].Text);
                    return;
                }
                else
                {
                    paramAddrStrs.Add(AdvanceConvert.uValue.ToString());
                }

            }

            if (!AdvanceConvert.StrToInt(OscilSizeData_TextBox.Text))
            {
                MessageBox.Show("Ошибка в поле Oscill Size Data");
                return;
            }
            else
            {
                oscillSizeDataStr = AdvanceConvert.uValue.ToString();
            }

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

                xmlOut.WriteStartElement("OscilConfig");

                xmlOut.WriteStartElement("ScopeCount");
                xmlOut.WriteAttributeString("Addr", scopeCountStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ChannelCount");
                xmlOut.WriteAttributeString("Addr", channelCountStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("History");
                xmlOut.WriteAttributeString("Addr", historyStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilFreq");
                xmlOut.WriteAttributeString("Addr", oscilFreqStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilStatus");
                xmlOut.WriteAttributeString("Addr", oscilStatusStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("StartTemp");
                xmlOut.WriteAttributeString("Addr", dataStartStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("LoadOscilStart");
                xmlOut.WriteAttributeString("Addr", loadOscilStartStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("NewConfig");
                xmlOut.WriteAttributeString("Addr", paramLoadDataStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("FlagNeed");
                xmlOut.WriteAttributeString("Addr", paramLoadConfigStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("TimeStamp");
                xmlOut.WriteAttributeString("Addr", timeStampStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilAllSize");
                xmlOut.WriteAttributeString("Count", Convert.ToString(oscillSizeDataStr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("MeasureParams");
                xmlOut.WriteAttributeString("Count", nameTextBoxs.Count.ToString());
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Oscil");
                xmlOut.WriteAttributeString("Count", Convert.ToString("0"));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Channel");
                xmlOut.WriteAttributeString("Count", Convert.ToString("0"));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Story");
                xmlOut.WriteAttributeString("Count", Convert.ToString("1"));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Frequency");
                xmlOut.WriteAttributeString("Count", Convert.ToString("1"));
                xmlOut.WriteEndElement();

                for (int i = 0; i < paramAddrStrs.Count; i++)
                {
                    xmlOut.WriteStartElement("MeasureParam" + (i + 1).ToString());

                    xmlOut.WriteAttributeString("Name", nameTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Dimension", dimensionComboBox[i].Text);
                    xmlOut.WriteAttributeString("Addr", paramAddrStrs[i]);
                    xmlOut.WriteAttributeString("Color", colorLabels[i].BackColor.ToArgb().ToString());
                    xmlOut.WriteAttributeString("Format", formatComboBoxNumeric[i].Text);
                    xmlOut.WriteAttributeString("FormatName", formatComboBox[i].Text);
                    xmlOut.WriteAttributeString("StepLine", stepLineCheckBoxs[i].SelectedIndex.ToString());
                    xmlOut.WriteAttributeString("TypeAD", AnalogDigitalComboBox[i].SelectedIndex.ToString());
                    xmlOut.WriteAttributeString("Min", minTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Max", maxTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Checked", Convert.ToString("False"));
                    
                    xmlOut.WriteEndElement();
                }

                xmlOut.WriteEndElement();
                
                /////////////////////////////////////////////////////////////
                xmlOut.WriteStartElement("COMETRADEConfig");

                xmlOut.WriteStartElement("StationName", stationName_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("RecordingDevice", recordingDevice_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilNominalFrequency");
                xmlOut.WriteAttributeString("Count", nominalFrequency_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilSampleRate");
                xmlOut.WriteAttributeString("Count", sampleRate_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("TimeCode", timeCode_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("LocalCode", localCode_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("tmqCode", tmqCode_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("leapsec", leapsec_textBox.Text);
                xmlOut.WriteEndElement();

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
