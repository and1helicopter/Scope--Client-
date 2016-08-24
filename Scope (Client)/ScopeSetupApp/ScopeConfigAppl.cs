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

        //Сглаживание 
        List<ComboBox> stepLineCheckBoxs = new List<ComboBox>();

        //Удаление 
        List<Button> removeButtons = new List<Button>();

        private void AddParamLine(string lineName, int lineAddr, Color clr, int formatData, string formatName, int stepLine)
        {
            int i;

            nameTextBoxs.Add(new TextBox());
            i = nameTextBoxs.Count - 1;
            nameTextBoxs[i].Dock = DockStyle.None;
            nameTextBoxs[i].Font = new Font("Arial", 10);
            nameTextBoxs[i].Left = 10;
            nameTextBoxs[i].Top = 10 + 25 * i;
            nameTextBoxs[i].Width = 200;
            nameTextBoxs[i].Text = lineName;

            addrTextBoxs.Add(new TextBox());
            addrTextBoxs[i].Dock = DockStyle.None;
            addrTextBoxs[i].Font = new Font("Arial", 10);
            addrTextBoxs[i].Left = 215;
            addrTextBoxs[i].Top = 10 + 25 * i;
            addrTextBoxs[i].Width = 60;
            addrTextBoxs[i].Text = "0x" + lineAddr.ToString("X4");
            addrTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            colorLabels.Add(new Label());
            colorLabels[i].Tag = i;
            colorLabels[i].Dock = DockStyle.None;
            colorLabels[i].BorderStyle = BorderStyle.FixedSingle;
            colorLabels[i].Width = colorLabels[i].Height = addrTextBoxs[i].Height;
            colorLabels[i].Left = 285;
            colorLabels[i].Top = 10 + 25 * i;
            colorLabels[i].BackColor = clr;
            colorLabels[i].Click += new EventHandler(colorLabel_Click);

            formatComboBoxNumeric.Add(new ComboBox());
            formatComboBoxNumeric[i].Tag = i;
            formatComboBoxNumeric[i].Dock = DockStyle.None;
            formatComboBoxNumeric[i].Items.AddRange(sizeFormat);
            formatComboBoxNumeric[i].Width = 40;
            formatComboBoxNumeric[i].Left = 314;
            formatComboBoxNumeric[i].Top = 10 + 25 * i;
            formatComboBoxNumeric[i].Text = Convert.ToString(formatData);
            formatComboBoxNumeric[i].DropDownStyle = ComboBoxStyle.DropDownList;
            
            formatComboBox.Add(new ComboBox());
            formatComboBox[i].Tag = i;
            formatComboBox[i].Dock = DockStyle.None;
            formatComboBox[i].DropDownStyle = ComboBoxStyle.DropDown;
            formatComboBox[i].Items.AddRange(format);
            formatComboBox[i].Width = 100;
            formatComboBox[i].Left = 359;
            formatComboBox[i].Top = 10 + 25 * i;
            formatComboBox[i].Text = formatName;
            
            
            stepLineCheckBoxs.Add(new ComboBox());
            stepLineCheckBoxs[i].Tag = i;
            stepLineCheckBoxs[i].Dock = DockStyle.None;
            stepLineCheckBoxs[i].Left = 464;
            stepLineCheckBoxs[i].Width = 100;
            stepLineCheckBoxs[i].DropDownStyle = ComboBoxStyle.DropDownList;

            stepLineCheckBoxs[i].Items.Add("Сглаженная");
            stepLineCheckBoxs[i].Items.Add("Ступенчатая");

            stepLineCheckBoxs[i].SelectedIndex = stepLine;
            stepLineCheckBoxs[i].Top = 10 + 25 * i;


            removeButtons.Add(new Button());
            removeButtons[i].Text = "Удалить";
            removeButtons[i].Tag = i;
            removeButtons[i].Dock = DockStyle.None;
            removeButtons[i].Left = 569;
            removeButtons[i].Top = 10 + 25 * i;
            removeButtons[i].Click += new EventHandler(deleteButton_Click);

                        
            configPanel.Controls.Add(addrTextBoxs[i]);
            configPanel.Controls.Add(nameTextBoxs[i]);
            configPanel.Controls.Add(colorLabels[i]);
            configPanel.Controls.Add(formatComboBoxNumeric[i]);
            configPanel.Controls.Add(formatComboBox[i]);
            configPanel.Controls.Add(stepLineCheckBoxs[i]);
            configPanel.Controls.Add(removeButtons[i]);
        }

        private void DeleteLine(int lineNum)
        {
            if (lineNum >= nameTextBoxs.Count) { return; }
            int i = lineNum;
            for (i = lineNum; i < nameTextBoxs.Count - 1; i++)
            {
                nameTextBoxs[i].Text = nameTextBoxs[i + 1].Text;
                addrTextBoxs[i].Text = addrTextBoxs[i + 1].Text;
                colorLabels[i].BackColor = colorLabels[i + 1].BackColor;
                formatComboBoxNumeric[i].Text = formatComboBoxNumeric[i + 1].Text;
                formatComboBox[i].Text = formatComboBox[i + 1].Text;
                stepLineCheckBoxs[i].SelectedIndex = stepLineCheckBoxs[i].SelectedIndex;

            }
            i = nameTextBoxs.Count - 1;
            configPanel.Controls.Remove(nameTextBoxs[i]);
            configPanel.Controls.Remove(addrTextBoxs[i]);
            configPanel.Controls.Remove(removeButtons[i]);
            configPanel.Controls.Remove(colorLabels[i]);
            configPanel.Controls.Remove(formatComboBoxNumeric[i]);
            configPanel.Controls.Remove(formatComboBox[i]);
            configPanel.Controls.Remove(stepLineCheckBoxs[i]);

            nameTextBoxs.Remove(nameTextBoxs[i]);
            addrTextBoxs.Remove(addrTextBoxs[i]);
            removeButtons.Remove(removeButtons[i]);
            colorLabels.Remove(colorLabels[i]);
            formatComboBoxNumeric.Remove(formatComboBoxNumeric[i]);
            formatComboBox.Remove(formatComboBox[i]);
            stepLineCheckBoxs.Remove(stepLineCheckBoxs[i]);
        }

        public ScopeConfigForm()
        {
            InitializeComponent();
           /* for (int i1 = 0; i1 < ScopeSysType.ChannelNames.Count; i1++)
            {
                AddParamLine(
                    ScopeSysType.ChannelNames[i1],
                    ScopeSysType.ChannelAddrs[i1],
                    ScopeSysType.ChannelColors[i1],
                    ScopeSysType.ChannelFormats[i1],
                    ScopeSysType.ChannelStepLines[i1]);
            } */
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void addLineButton_Click(object sender, EventArgs e)
        {
            AddParamLine("Параметр ", addrTextBoxs.Count, Color.Black, 16, "Custom", 1);
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
                oscillStatusTextBox.Text = "0x" + ScopeSysType.OscilStatusAddr.ToString("X4");
                hystoryTextBox.Text = "0x" + ScopeSysType.HystoryAddr.ToString("X4");
                channelCountTextBox.Text = "0x" + ScopeSysType.ChannelCountAddr.ToString("X4");
                dataStartTextBox.Text = "0x" + ScopeSysType.DataStartAddr.ToString("X4");
                oscillFreqTextBox.Text = "0x" + ScopeSysType.OscilFreqAddr.ToString("X4");
                scopeCountTextBox.Text = "0x" + ScopeSysType.ScopeCountAddr.ToString("X4");
                loadOscillStartTextBox.Text = "0x" + ScopeSysType.LoadOscilStartAddr.ToString("X4");
                paramLoadConfigTextBox.Text = "0x" + ScopeSysType.ParamLoadConfigAddr.ToString("X4");
                paramLoadDataTextBox.Text = "0x" + ScopeSysType.ParamLoadDataAddr.ToString("X4");
                oscillSizeDataTextBox.Text = "0x" + ScopeSysType.OscilAllSize.ToString("X4");

                
                for (int i = nameTextBoxs.Count - 1; i >= 0 ; i--)
                {

                    configPanel.Controls.Remove(nameTextBoxs[i]);
                    configPanel.Controls.Remove(addrTextBoxs[i]);
                    configPanel.Controls.Remove(removeButtons[i]);
                    configPanel.Controls.Remove(colorLabels[i]);
                    configPanel.Controls.Remove(formatComboBoxNumeric[i]);
                    configPanel.Controls.Remove(formatComboBox[i]);
                    configPanel.Controls.Remove(stepLineCheckBoxs[i]);

                    nameTextBoxs.Remove(nameTextBoxs[i]);
                    addrTextBoxs.Remove(addrTextBoxs[i]);
                    removeButtons.Remove(removeButtons[i]);
                    colorLabels.Remove(colorLabels[i]);
                    formatComboBoxNumeric.Remove(formatComboBoxNumeric[i]);
                    formatComboBox.Remove(formatComboBox[i]);
                    stepLineCheckBoxs.Remove(stepLineCheckBoxs[i]);
                }


                for (int i1 = 0; i1 < ScopeSysType.ChannelNames.Count; i1++)
                {
                    AddParamLine(
                                    ScopeSysType.ChannelNames[i1],
                                    ScopeSysType.ChannelAddrs[i1],
                                    ScopeSysType.ChannelColors[i1],
                                    ScopeSysType.ChannelFormats[i1],
                                    ScopeSysType.ChannelFormatsName[i1],
                                    ScopeSysType.ChannelStepLines[i1]                                     
                                 );
                }
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string timeStampStr, oscilStatusStr, scopeCountStr,
                   hystoryStr, channelCountStr, dataStartStr,
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


            if (!AdvanceConvert.StrToInt(oscillStatusTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле oscillStatus");
                return;
            }
            else
            {
                oscilStatusStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(scopeCountTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле scopeCount");
                return;
            }
            else
            {
                scopeCountStr = AdvanceConvert.uValue.ToString();
            }

            if (!AdvanceConvert.StrToInt(hystoryTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле hystoryText");
                return;
            }
            else
            {
                hystoryStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(channelCountTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле channelCount");
                return;
            }
            else
            {
                channelCountStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(dataStartTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле dataStart");
                return;
            }
            else
            {
                dataStartStr = AdvanceConvert.uValue.ToString();
            }

            if (!AdvanceConvert.StrToInt(oscillFreqTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле oscilFreq");
                return;
            }
            else
            {
                oscilFreqStr = AdvanceConvert.uValue.ToString();
            }



            if (!AdvanceConvert.StrToInt(loadOscillStartTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле loadOscilStart");
                return;
            }
            else
            {
                loadOscilStartStr = AdvanceConvert.uValue.ToString();
            }

            if (!AdvanceConvert.StrToInt(paramLoadConfigTextBox.Text))
            {
                MessageBox.Show("Ошибка в поле paramLoadConfig");
                return;
            }
            else
            {
                paramLoadConfigStr = AdvanceConvert.uValue.ToString();
            }


            if (!AdvanceConvert.StrToInt(paramLoadDataTextBox.Text))
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

            if (!AdvanceConvert.StrToInt(oscillSizeDataTextBox.Text))
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
                xmlOut.WriteStartElement("TimeStamp");
                xmlOut.WriteAttributeString("Addr", timeStampStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilStatus");
                xmlOut.WriteAttributeString("Addr", oscilStatusStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ScopeCount");
                xmlOut.WriteAttributeString("Addr", scopeCountStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Hystory");
                xmlOut.WriteAttributeString("Addr", hystoryStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ChannelCount");
                xmlOut.WriteAttributeString("Addr", channelCountStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("DataStart");
                xmlOut.WriteAttributeString("Addr", dataStartStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilFreq");
                xmlOut.WriteAttributeString("Addr", oscilFreqStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("LoadOscilStart");
                xmlOut.WriteAttributeString("Addr", loadOscilStartStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ParamLoadConfig");
                xmlOut.WriteAttributeString("Addr", paramLoadConfigStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("ParamLoadData");
                xmlOut.WriteAttributeString("Addr", paramLoadDataStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("MeasureParams");
                xmlOut.WriteAttributeString("Count", nameTextBoxs.Count.ToString());
                xmlOut.WriteEndElement();

                for (int i = 0; i < paramAddrStrs.Count; i++)
                {
                    xmlOut.WriteStartElement("MeasureParam" + (i + 1).ToString());

                    xmlOut.WriteAttributeString("Name", nameTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Addr", paramAddrStrs[i]);
                    xmlOut.WriteAttributeString("Color", colorLabels[i].BackColor.ToArgb().ToString());
                    xmlOut.WriteAttributeString("Format", formatComboBoxNumeric[i].Text);
                    xmlOut.WriteAttributeString("FormatName", formatComboBox[i].Text);
                    xmlOut.WriteAttributeString("StepLine", stepLineCheckBoxs[i].SelectedIndex.ToString());
                    xmlOut.WriteAttributeString("Checked", Convert.ToString("False"));
                    
                    xmlOut.WriteEndElement();
                }

                xmlOut.WriteStartElement("Oscil");
                xmlOut.WriteAttributeString("Count", Convert.ToString("0"));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Channel");
                xmlOut.WriteAttributeString("Count", Convert.ToString("0"));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("History");
                xmlOut.WriteAttributeString("Count", Convert.ToString("0"));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("Frequency");
                xmlOut.WriteAttributeString("Count", Convert.ToString("0"));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilAllSize");
                xmlOut.WriteAttributeString("Count", Convert.ToString(oscillSizeDataStr));
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
