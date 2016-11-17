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
        //Панель 
        List<Panel> LayoutPanel = new List<Panel>();

        //Номер
        List<Label> NumLabels = new List<Label>();

        //Названия
        List<TextBox> nameTextBoxs = new List<TextBox>();

        //Фаза
        List<TextBox> phaseTextBoxs = new List<TextBox>();

        //ccbm
        List<TextBox> ccbmTextBoxs = new List<TextBox>();

        //Размерность физической величины 
        List<ComboBox> AnalogDigitalComboBox = new List<ComboBox>();

        //Адреса
        List<TextBox> addrTextBoxs = new List<TextBox>();

        //Цвета
        List<Label> colorLabels = new List<Label>();

        //форматы данных
        List<ComboBox> formatComboBoxNumeric = new List<ComboBox>();
        List<ComboBox> formatComboBox = new List<ComboBox>();

        //Размерность физической величины 
        List<ComboBox> dimensionComboBox = new List<ComboBox>();

        List<TextBox> minTextBoxs = new List<TextBox>();
        List<TextBox> maxTextBoxs = new List<TextBox>();

        //Сглаживание 
        List<ComboBox> stepLineCheckBoxs = new List<ComboBox>();

        //Удаление 
        List<Button> removeButtons = new List<Button>();

        //Отметка 
        List<CheckBox> checkBoxs = new List<CheckBox>();

        //Line
        //List<> Line = new List<?>();

        object[] format = new object[]{
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
            "21 - Freq UPTF"
        };

        object[] sizeFormat = new object[]{
            "16",
            "32",
            "64",
        };

        object[] dimension = new object []{
            "NONE",
        };


        private void AddParamLine(string lineName, string linePhase, string lineCCBM, string lineDimension, int lineAddr, Color clr, int formatData,  int stepLine, int lineTypeAD, int min, int max)
        {
            int i;

            nameTextBoxs.Add(new TextBox());
            i = nameTextBoxs.Count - 1;
            nameTextBoxs[i].Dock = DockStyle.None;
            nameTextBoxs[i].Font = new Font("Arial", 9);
            nameTextBoxs[i].AutoSize = false;
            nameTextBoxs[i].Left = 34;
            nameTextBoxs[i].Top = 3;
            nameTextBoxs[i].Width = 150;
            nameTextBoxs[i].Height = 24;
            nameTextBoxs[i].Text = lineName;

            NumLabels.Add(new Label());
            NumLabels[i].Text = Convert.ToString(i + 1) + ".";
            NumLabels[i].Left = 2;
            NumLabels[i].Top = 6;

            AnalogDigitalComboBox.Add(new ComboBox());
            AnalogDigitalComboBox[i].Tag = i;
            AnalogDigitalComboBox[i].Dock = DockStyle.None;
            AnalogDigitalComboBox[i].Font = new Font("Arial", 9);
            AnalogDigitalComboBox[i].Left = 483;
            AnalogDigitalComboBox[i].Width = 90;
            AnalogDigitalComboBox[i].DropDownStyle = ComboBoxStyle.DropDownList;
            AnalogDigitalComboBox[i].Items.Add("Analog");
            AnalogDigitalComboBox[i].Items.Add("Digital");
            AnalogDigitalComboBox[i].SelectedIndex = lineTypeAD;
            AnalogDigitalComboBox[i].Top = 30;
            
            phaseTextBoxs.Add(new TextBox());
            phaseTextBoxs[i].Dock = DockStyle.None;
            phaseTextBoxs[i].Font = new Font("Arial", 9);
            phaseTextBoxs[i].AutoSize = false;
            phaseTextBoxs[i].Left = 34;
            phaseTextBoxs[i].Top = 30;
            phaseTextBoxs[i].Width = 50;
            phaseTextBoxs[i].Height = 24;
            phaseTextBoxs[i].Text = linePhase;

            ccbmTextBoxs.Add(new TextBox());
            ccbmTextBoxs[i].Dock = DockStyle.None;
            ccbmTextBoxs[i].Font = new Font("Arial", 9);
            ccbmTextBoxs[i].AutoSize = false;
            ccbmTextBoxs[i].Left = 87;
            ccbmTextBoxs[i].Top = 30;
            ccbmTextBoxs[i].Width = 97;
            ccbmTextBoxs[i].Height = 24;
            ccbmTextBoxs[i].Text = lineCCBM;

            dimensionComboBox.Add(new ComboBox());
            dimensionComboBox[i].Tag = i;
            dimensionComboBox[i].Dock = DockStyle.None;
            dimensionComboBox[i].Font = new Font("Arial", 9);
            dimensionComboBox[i].Items.AddRange(dimension);
            dimensionComboBox[i].Left = 187;
            dimensionComboBox[i].Top = 30;
            dimensionComboBox[i].Width = 60;
            dimensionComboBox[i].Text = lineDimension;
            dimensionComboBox[i].DropDownStyle = ComboBoxStyle.DropDown;

            addrTextBoxs.Add(new TextBox());
            addrTextBoxs[i].Dock = DockStyle.None;
            addrTextBoxs[i].Font = new Font("Arial", 9);
            addrTextBoxs[i].AutoSize = false;
            addrTextBoxs[i].Left = 187;
            addrTextBoxs[i].Top = 3;
            addrTextBoxs[i].Width = 60;
            addrTextBoxs[i].Height = 24;
            addrTextBoxs[i].Text = "0x" + lineAddr.ToString("X4");
            addrTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            colorLabels.Add(new Label());
            colorLabels[i].Tag = i;
            colorLabels[i].Dock = DockStyle.None;
            colorLabels[i].BorderStyle = BorderStyle.FixedSingle;
            colorLabels[i].Width = colorLabels[i].Height = addrTextBoxs[i].Height;
            colorLabels[i].Left = 250;
            colorLabels[i].Top = 3;
            colorLabels[i].BackColor = clr;
            colorLabels[i].Click += new EventHandler(colorLabel_Click);

            formatComboBoxNumeric.Add(new ComboBox());
            formatComboBoxNumeric[i].Tag = i;
            formatComboBoxNumeric[i].Dock = DockStyle.None;
            formatComboBoxNumeric[i].Font = new Font("Arial", 9);
            formatComboBoxNumeric[i].Items.AddRange(sizeFormat);
            formatComboBoxNumeric[i].Width = 100;
            formatComboBoxNumeric[i].Left = 277;
            formatComboBoxNumeric[i].Top = 3;
            formatComboBoxNumeric[i].SelectedIndex = (formatData >> 8) - 1;
            formatComboBoxNumeric[i].DropDownStyle = ComboBoxStyle.DropDownList;
            
            formatComboBox.Add(new ComboBox());
            formatComboBox[i].Tag = i;
            formatComboBox[i].Dock = DockStyle.None;
            formatComboBox[i].Font = new Font("Arial", 9);
            formatComboBox[i].DropDownStyle = ComboBoxStyle.DropDownList;
            formatComboBox[i].Items.AddRange(format);
            formatComboBox[i].Width = 100;
            formatComboBox[i].Left = 380;
            formatComboBox[i].Top = 3;
            formatComboBox[i].SelectedIndex = formatData & 0x00FF ;
            
            stepLineCheckBoxs.Add(new ComboBox());
            stepLineCheckBoxs[i].Tag = i;
            stepLineCheckBoxs[i].Dock = DockStyle.None;
            stepLineCheckBoxs[i].Font = new Font("Arial", 9);
            stepLineCheckBoxs[i].Left = 483;
            stepLineCheckBoxs[i].Width = 90;
            stepLineCheckBoxs[i].DropDownStyle = ComboBoxStyle.DropDownList;
            stepLineCheckBoxs[i].Items.Add("Smooth");
            stepLineCheckBoxs[i].Items.Add("Step");
            stepLineCheckBoxs[i].SelectedIndex = stepLine;
            stepLineCheckBoxs[i].Top = 3;

            minTextBoxs.Add(new TextBox());
            minTextBoxs[i].Dock = DockStyle.None;
            minTextBoxs[i].Font = new Font("Arial", 9);
            minTextBoxs[i].AutoSize = false;
            minTextBoxs[i].Left = 277;
            minTextBoxs[i].Top = 30;
            minTextBoxs[i].Width = 100;
            minTextBoxs[i].Height = 24;
            minTextBoxs[i].Text = min.ToString();
            minTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            maxTextBoxs.Add(new TextBox());
            maxTextBoxs[i].Dock = DockStyle.None;
            maxTextBoxs[i].Font = new Font("Arial", 9);
            maxTextBoxs[i].AutoSize = false;
            maxTextBoxs[i].Left = 380;
            maxTextBoxs[i].Top = 30;
            maxTextBoxs[i].Width = 100;
            maxTextBoxs[i].Height = 24;
            maxTextBoxs[i].Text = max.ToString();
            maxTextBoxs[i].TextAlign = HorizontalAlignment.Right;


            //this.checkBox2.Location = new System.Drawing.Point(715, 43);
            //this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            //this.checkBox2.Size = new System.Drawing.Size(148, 21);
  

            removeButtons.Add(new Button());
            removeButtons[i].Tag = i;
            //removeButtons[i].Text = "Удалить";
            removeButtons[i].Location = new System.Drawing.Point(700, 8);
            removeButtons[i].Image = Properties.Resources.Minus_32;
           // removeButtons[i].ImageAlign = ContentAlignment.MiddleCenter;
            removeButtons[i].Size = new System.Drawing.Size(40, 40);
            removeButtons[i].Dock = DockStyle.Right;
            //removeButtons[i].Left = 770;
            //removeButtons[i].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            removeButtons[i].AutoSize = false;
            //removeButtons[i].Top = 8;
            removeButtons[i].Click += new EventHandler(deleteButton_Click);

            checkBoxs.Add(new CheckBox());
            checkBoxs[i].Tag = i;
            checkBoxs[i].Dock = DockStyle.Right;
          //  checkBoxs[i].Left = 830;
            //            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;

            checkBoxs[i].AutoSize = false;
           // checkBoxs[i].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxs[i].Size = new System.Drawing.Size(40, 40);
            checkBoxs[i].Top = 18;
            checkBoxs[i].Click += new System.EventHandler(checkBox_Click);

            LayoutPanel.Add(new Panel());
            LayoutPanel[i].Dock = DockStyle.Top;
            LayoutPanel[i].BackColor = Color.WhiteSmoke;
            LayoutPanel[i].Left = 5;
            LayoutPanel[i].Top = 5 + 63 * i;
            LayoutPanel[i].Width = 800;
            LayoutPanel[i].Height = 58;
            LayoutPanel[i].BorderStyle = BorderStyle.FixedSingle;
            
            configPanel.Controls.Add(LayoutPanel[i]);
            LayoutPanel[i].Controls.Add(nameTextBoxs[i]);
            LayoutPanel[i].Controls.Add(phaseTextBoxs[i]);
            LayoutPanel[i].Controls.Add(ccbmTextBoxs[i]);
            LayoutPanel[i].Controls.Add(addrTextBoxs[i]);
            LayoutPanel[i].Controls.Add(dimensionComboBox[i]);
            LayoutPanel[i].Controls.Add(colorLabels[i]);
            LayoutPanel[i].Controls.Add(formatComboBoxNumeric[i]);
            LayoutPanel[i].Controls.Add(formatComboBox[i]);
            LayoutPanel[i].Controls.Add(stepLineCheckBoxs[i]);
            LayoutPanel[i].Controls.Add(AnalogDigitalComboBox[i]);
            LayoutPanel[i].Controls.Add(minTextBoxs[i]);
            LayoutPanel[i].Controls.Add(maxTextBoxs[i]);
            LayoutPanel[i].Controls.Add(removeButtons[i]);
            LayoutPanel[i].Controls.Add(NumLabels[i]);
            LayoutPanel[i].Controls.Add(checkBoxs[i]);
        }

        private void DeleteLine(int lineNum)
        {
            if (lineNum >= nameTextBoxs.Count) { return; }
            int i = lineNum;
            for (i = lineNum; i < nameTextBoxs.Count - 1; i++)
            {
                nameTextBoxs[i].Text = nameTextBoxs[i + 1].Text;
                phaseTextBoxs[i].Text = phaseTextBoxs[i + 1].Text;
                ccbmTextBoxs[i].Text = ccbmTextBoxs[i + 1].Text;
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

            configPanel.Controls.Remove(LayoutPanel[i]);
            LayoutPanel[i].Controls.Remove(nameTextBoxs[i]);
            LayoutPanel[i].Controls.Remove(phaseTextBoxs[i]);
            LayoutPanel[i].Controls.Remove(ccbmTextBoxs[i]);
            LayoutPanel[i].Controls.Remove(dimensionComboBox[i]);
            LayoutPanel[i].Controls.Remove(addrTextBoxs[i]);
            LayoutPanel[i].Controls.Remove(removeButtons[i]);
            LayoutPanel[i].Controls.Remove(colorLabels[i]);
            LayoutPanel[i].Controls.Remove(formatComboBoxNumeric[i]);
            LayoutPanel[i].Controls.Remove(formatComboBox[i]);
            LayoutPanel[i].Controls.Remove(stepLineCheckBoxs[i]);
            LayoutPanel[i].Controls.Remove(AnalogDigitalComboBox[i]);
            LayoutPanel[i].Controls.Remove(minTextBoxs[i]);
            LayoutPanel[i].Controls.Remove(maxTextBoxs[i]);
            LayoutPanel[i].Controls.Remove(NumLabels[i]);
            LayoutPanel[i].Controls.Remove(checkBoxs[i]);

            nameTextBoxs.Remove(nameTextBoxs[i]);
            phaseTextBoxs.Remove(phaseTextBoxs[i]);
            ccbmTextBoxs.Remove(ccbmTextBoxs[i]);
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
            NumLabels.Remove(NumLabels[i]);
            checkBoxs.Remove(checkBoxs[i]);
        }

        public ScopeConfigForm()
        {
            InitializeComponent();
            ConfigToSystem();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void addLineButton_Click(object sender, EventArgs e)
        {
            AddParamLine("Параметр ", "", "","NONE", addrTextBoxs.Count, Color.Black, 16, 1, 0, -1, 1);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int i = (int)((sender as Button).Tag);

            DeleteLine(i);
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkBoxs.Count; i++)
            {
                if (checkBoxs[i].Checked) LayoutPanel[i].BackColor = Color.PaleGreen;
                else LayoutPanel[i].BackColor = Color.WhiteSmoke;
            }
        }

        List<String> nameTextBoxsCopy = new List<String>();
        List<String> phaseTextBoxsCopy = new List<String>();
        List<String> ccbmTextBoxsCopy = new List<String>();
        List<String> dimensionComboBoxCopy = new List<String>();
        List<int> AnalogDigitalComboBoxCopy = new List<int>();
        List<int> addrTextBoxsCopy = new List<int>();
        List<Color> colorLabelsCopy = new List<Color>();
        List<int> formatComboBoxCopy = new List<int>();
        List<int> minTextBoxsCopy = new List<int>();
        List<int> maxTextBoxsCopy = new List<int>();
        List<int> stepLineCheckBoxsCopy = new List<int>();

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            nameTextBoxsCopy.Clear();
            phaseTextBoxsCopy.Clear();
            ccbmTextBoxsCopy.Clear();
            AnalogDigitalComboBoxCopy.Clear();
            addrTextBoxsCopy.Clear();
            colorLabelsCopy.Clear();
            formatComboBoxCopy.Clear();
            dimensionComboBoxCopy.Clear();
            minTextBoxsCopy.Clear();
            maxTextBoxsCopy.Clear();
            stepLineCheckBoxsCopy.Clear();

            for (int i = 0; i < checkBoxs.Count; i++) 
            {
                if (checkBoxs[i].Checked) 
                {
                    nameTextBoxsCopy.Add(nameTextBoxs[i].Text);
                    phaseTextBoxsCopy.Add(phaseTextBoxs[i].Text);
                    ccbmTextBoxsCopy.Add(ccbmTextBoxs[i].Text);
                    AnalogDigitalComboBoxCopy.Add(Convert.ToInt32(AnalogDigitalComboBox[i].SelectedIndex));
                    addrTextBoxsCopy.Add(Convert.ToInt32(AdvanceConvert.StrToInt(addrTextBoxs[i].Text)));
                    colorLabelsCopy.Add(colorLabels[i].BackColor);
                    formatComboBoxCopy.Add((Convert.ToInt32(formatComboBoxNumeric[i].SelectedIndex + 1) << 8) + Convert.ToInt32(formatComboBox[i].SelectedIndex));
                    dimensionComboBoxCopy.Add(dimensionComboBox[i].Text);
                    minTextBoxsCopy.Add(Convert.ToInt32(minTextBoxs[i].Text));
                    maxTextBoxsCopy.Add(Convert.ToInt32(maxTextBoxs[i].Text));
                    stepLineCheckBoxsCopy.Add(Convert.ToInt32(stepLineCheckBoxs[i].SelectedIndex));
                }
            }          
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < nameTextBoxsCopy.Count; i++)
            {    
                 AddParamLine(nameTextBoxsCopy[i],phaseTextBoxsCopy[i],ccbmTextBoxsCopy[i],dimensionComboBoxCopy[i],addrTextBoxsCopy[i],colorLabelsCopy[i],formatComboBoxCopy[i],stepLineCheckBoxsCopy[i],AnalogDigitalComboBoxCopy[i],minTextBoxsCopy[i],maxTextBoxsCopy[i]);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = nameTextBoxs.Count - 1; i >= 0; i--) 
            {
                if (checkBoxs[i].Checked) 
                {
                    LayoutPanel[i].BackColor = Color.WhiteSmoke;
                    checkBoxs[i].Checked = false;
                    DeleteLine(i); 
                }
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                for (int i = 0; i < checkBoxs.Count; i++)
                {
                    checkBoxs[i].Checked  = true;
                    LayoutPanel[i].BackColor = Color.PaleGreen;     
                }
            }
            else
            {
                for (int i = 0; i < checkBoxs.Count; i++)
                {
                    checkBoxs[i].Checked = false;
                    LayoutPanel[i].BackColor = Color.WhiteSmoke;
                }
            }

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
            ofd.DefaultExt = ".xsc"; // Default file extension
            ofd.Filter = "XML System Configuration|*.xsc|XML|*.xml|All files (*.*)|*.*"; // Filter files by extension
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
                ConfigAddr_textBox.Text = "0x" + ScopeSysType.ConfigurationAddr.ToString("X4");
                OscilCmndAddr_textBox.Text = "0x" + ScopeSysType.OscilCmndAddr.ToString("X4");
                OscilSizeData_TextBox.Text = "0x" + ScopeSysType.OscilAllSize.ToString("X4");
                CommentRichTextBox.Text = ScopeSysType.OscilComment.ToString();
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
                    configPanel.Controls.Remove(LayoutPanel[i]);
                    LayoutPanel[i].Controls.Remove(nameTextBoxs[i]);
                    LayoutPanel[i].Controls.Remove(phaseTextBoxs[i]);
                    LayoutPanel[i].Controls.Remove(ccbmTextBoxs[i]);
                    LayoutPanel[i].Controls.Remove(dimensionComboBox[i]);
                    LayoutPanel[i].Controls.Remove(addrTextBoxs[i]);
                    LayoutPanel[i].Controls.Remove(removeButtons[i]);
                    LayoutPanel[i].Controls.Remove(colorLabels[i]);
                    LayoutPanel[i].Controls.Remove(formatComboBoxNumeric[i]);
                    LayoutPanel[i].Controls.Remove(formatComboBox[i]);
                    LayoutPanel[i].Controls.Remove(stepLineCheckBoxs[i]);
                    LayoutPanel[i].Controls.Remove(AnalogDigitalComboBox[i]);
                    LayoutPanel[i].Controls.Remove(minTextBoxs[i]);
                    LayoutPanel[i].Controls.Remove(maxTextBoxs[i]);

                    nameTextBoxs.Remove(nameTextBoxs[i]);
                    phaseTextBoxs.Remove(phaseTextBoxs[i]);
                    ccbmTextBoxs.Remove(ccbmTextBoxs[i]);
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
                                    ScopeSysType.ChannelPhase[i1],
                                    ScopeSysType.ChannelCCBM[i1],
                                    ScopeSysType.ChannelDimension[i1],
                                    ScopeSysType.ChannelAddrs[i1],
                                    ScopeSysType.ChannelColors[i1],
                                    ScopeSysType.ChannelFormats[i1],
                                    ScopeSysType.ChannelStepLines[i1], 
                                    ScopeSysType.ChannelTypeAD[i1],
                                    ScopeSysType.ChannelMin[i1],
                                    ScopeSysType.ChannelMax[i1]  
                                 );
                }
            }
        }

        private void Save_To_file(SaveFileDialog sfd)
        {
                string oscillSizeDataStr, oscilCmndStr, configStr;
                List<string> paramAddrStrs = new List<string>();

                if (!AdvanceConvert.StrToInt(ConfigAddr_textBox.Text))
                {
                    MessageBox.Show("Ошибка в поле Configuration Addr");
                    return;
                }
                else
                {
                    configStr = AdvanceConvert.uValue.ToString();
                }


                if (!AdvanceConvert.StrToInt(OscilCmndAddr_textBox.Text))
                {
                    MessageBox.Show("Ошибка в поле Oscil Cmnd Addr");
                    return;
                }
                else
                {
                    oscilCmndStr = AdvanceConvert.uValue.ToString();
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

                ScopeSysType.xmlFileName = sfd.FileName;

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                xmlOut.Formatting = Formatting.Indented;
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("Setup");
                /////////////////////////////////////////////////////////////

                xmlOut.WriteStartElement("OscilConfig");

                xmlOut.WriteStartElement("Configuration");
                xmlOut.WriteAttributeString("Addr", configStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilCmnd");
                xmlOut.WriteAttributeString("Addr", oscilCmndStr);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilAllSize");
                xmlOut.WriteAttributeString("Count", Convert.ToString(oscillSizeDataStr));
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilSampleRate");
                xmlOut.WriteAttributeString("Count", sampleRate_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("MeasureParams");
                xmlOut.WriteAttributeString("Count", nameTextBoxs.Count.ToString());
                xmlOut.WriteEndElement();

                for (int i = 0; i < paramAddrStrs.Count; i++)
                {
                    xmlOut.WriteStartElement("MeasureParam" + (i + 1).ToString());

                    xmlOut.WriteAttributeString("Name", nameTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Phase", phaseTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("CCBM", ccbmTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Dimension", dimensionComboBox[i].Text);
                    xmlOut.WriteAttributeString("Addr", paramAddrStrs[i]);
                    xmlOut.WriteAttributeString("Color", colorLabels[i].BackColor.ToArgb().ToString());
                    xmlOut.WriteAttributeString("Format", ((Convert.ToInt32(formatComboBoxNumeric[i].SelectedIndex + 1) << 8) + Convert.ToInt32(formatComboBox[i].SelectedIndex)).ToString());
                    xmlOut.WriteAttributeString("StepLine", stepLineCheckBoxs[i].SelectedIndex.ToString());
                    xmlOut.WriteAttributeString("TypeAD", AnalogDigitalComboBox[i].SelectedIndex.ToString());
                    xmlOut.WriteAttributeString("Min", minTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Max", maxTextBoxs[i].Text);

                    xmlOut.WriteEndElement();
                }

                xmlOut.WriteStartElement("Comment", CommentRichTextBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteEndElement();
                /////////////////////////////////////////////////////////////
                xmlOut.WriteStartElement("COMTRADEConfig");

                xmlOut.WriteStartElement("StationName", stationName_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("RecordingDevice", recordingDevice_textBox.Text);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("OscilNominalFrequency");
                xmlOut.WriteAttributeString("Count", nominalFrequency_textBox.Text);
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

                ScopeSysType.InitScopeSysType(); 
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xsc"; // Default file extension
            sfd.Filter = "XML System Configuration|*.xsc|XML|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK) Save_To_file(sfd); 
        }


        private void SetDefault_toolStripButton_Click(object sender, EventArgs e)
        {
            string namefile, pathfile;
            SaveFileDialog sfd = new SaveFileDialog();
            namefile = "ScopeSysType.xml";
            pathfile = Path.GetDirectoryName(ScopeSysType.xmlFileName);
            sfd.FileName = pathfile + "\\" + namefile;
            Save_To_file(sfd); 

            Update_Oscil();
        }

        private string convert_text(object Obj, string del)
        {
            int i = 0;
            string str = "";
            str = Convert.ToString(Obj);
            if (str == "") str = "0";
            if (del == "0x") i = Convert.ToInt32(str, 16);
            if (del == "") i = Convert.ToInt32(str);
            str = Convert.ToString(i);
            //if (del != "") str = str.Replace(del, "");
            return str;
        }

        private void Update_Oscil()
        {
            ScopeSysType.OscilAllSize = Convert.ToUInt16(convert_text(OscilSizeData_TextBox.Text, "0x"));
            ScopeSysType.OscilSampleRate = Convert.ToUInt16(convert_text(sampleRate_textBox.Text, ""));
            ScopeSysType.OscilComment = Convert.ToString(CommentRichTextBox.Text);

            ScopeSysType.StationName = Convert.ToString(stationName_textBox.Text);
            ScopeSysType.RecordingDevice = Convert.ToString(recordingDevice_textBox.Text);
            ScopeSysType.OscilNominalFrequency = Convert.ToUInt16(convert_text(nominalFrequency_textBox.Text, ""));
            ScopeSysType.TimeCode = Convert.ToString(timeCode_textBox.Text);
            ScopeSysType.LocalCode = Convert.ToString(localCode_textBox.Text);
            ScopeSysType.tmqCode = Convert.ToString(tmqCode_textBox.Text);
            ScopeSysType.leapsec = Convert.ToString(leapsec_textBox.Text);
            
            ScopeSysType.ChannelNames.Clear();
            ScopeSysType.ChannelPhase.Clear();
            ScopeSysType.ChannelCCBM.Clear();
            ScopeSysType.ChannelDimension.Clear();
            ScopeSysType.ChannelAddrs.Clear();
            ScopeSysType.ChannelColors.Clear();
            ScopeSysType.ChannelFormats.Clear();
            ScopeSysType.ChannelStepLines.Clear();
            ScopeSysType.ChannelTypeAD.Clear();
            ScopeSysType.ChannelMin.Clear();
            ScopeSysType.ChannelMax.Clear();
            
            for (int i = 0; i < addrTextBoxs.Count; i++)
            {
                ScopeSysType.ChannelNames.Add(nameTextBoxs[i].Text);
                ScopeSysType.ChannelPhase.Add(phaseTextBoxs[i].Text);
                ScopeSysType.ChannelCCBM.Add(ccbmTextBoxs[i].Text);
                ScopeSysType.ChannelDimension.Add(dimensionComboBox[i].Text);
                ScopeSysType.ChannelAddrs.Add(Convert.ToUInt16(convert_text(addrTextBoxs[i].Text, "0x")));
                ScopeSysType.ChannelColors.Add(Color.FromArgb(colorLabels[i].BackColor.ToArgb()));
                ScopeSysType.ChannelFormats.Add(Convert.ToUInt16(((Convert.ToUInt16(formatComboBoxNumeric[i].SelectedIndex + 1) << 8) + Convert.ToInt32(formatComboBox[i].SelectedIndex)).ToString()));
                ScopeSysType.ChannelStepLines.Add(Convert.ToInt32(stepLineCheckBoxs[i].SelectedIndex.ToString()));
                ScopeSysType.ChannelTypeAD.Add(Convert.ToUInt16(AnalogDigitalComboBox[i].SelectedIndex.ToString()));
                ScopeSysType.ChannelMin.Add(Convert.ToInt32(minTextBoxs[i].Text));
                ScopeSysType.ChannelMax.Add(Convert.ToInt32(maxTextBoxs[i].Text));
            }  
            
            ConfigToSystem(); 
        }

        private void View_toolStripButton_Click(object sender, EventArgs e)
        {
            Update_Oscil();

             SCPrintPreviewDialog.Document = SCPrintDocument;
             SCPrintPreviewDialog.ShowDialog();
        }

        private void Print_toolStripButton_Click(object sender, EventArgs e)
        {
            Update_Oscil();

            SCPrintDialog.Document = SCPrintDocument;
            if (SCPrintDialog.ShowDialog() == DialogResult.OK)
            {
                SCPrintDocument.Print();
            }
        }

        private void Update_toolStripButton_Click(object sender, EventArgs e)
        {
            Update_Oscil();
        }

        bool FirstPage = true;
        int paramNum = 0;

        private void SCPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "";
            int yPos = 30;
            int xPos1 = 25;
            int xPos2 = 200;
            int xPos3 = 350;
            int xPos4 = 520;

            if (FirstPage == true)
            {
                yPos = 150;
                e.Graphics.DrawString("Title", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 40));
                e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 60));
                e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 70));
                e.Graphics.DrawString("Comment:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 80));
                e.Graphics.DrawString(ScopeSysType.OscilComment.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 100));
                e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 140));
                e.Graphics.DrawString("Oscil Config", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(xPos1, 150));
                e.Graphics.DrawString("COMTRADE Config", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(xPos3, 150));
                e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 160));
                e.Graphics.DrawString("Configuration Addr:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
                e.Graphics.DrawString("0x" + ScopeSysType.ConfigurationAddr.ToString("X4"), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(200, yPos));
                e.Graphics.DrawString("StationName:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
                e.Graphics.DrawString(ScopeSysType.StationName.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
                e.Graphics.DrawString("Oscil Cmnd Addr:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
                e.Graphics.DrawString("0x" + ScopeSysType.OscilCmndAddr.ToString("X4"), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(200, yPos));
                e.Graphics.DrawString("RecordingDevice:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
                e.Graphics.DrawString(ScopeSysType.RecordingDevice.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
                e.Graphics.DrawString("OscilAllSize (KB):", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.OscilAllSize.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos2, yPos));
                e.Graphics.DrawString("NominalFrequency:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
                e.Graphics.DrawString(ScopeSysType.OscilNominalFrequency.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
                e.Graphics.DrawString("OscilSampleRate:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.OscilSampleRate.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos2, yPos));
                e.Graphics.DrawString("TimeCode:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
                e.Graphics.DrawString(ScopeSysType.TimeCode.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
                e.Graphics.DrawString("MeasureParams:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.ChannelNames.Count.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos2, yPos));
                e.Graphics.DrawString("LocalCode:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
                e.Graphics.DrawString(ScopeSysType.LocalCode.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
                e.Graphics.DrawString("tmqCode:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.tmqCode.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
                e.Graphics.DrawString("leapsec:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.leapsec.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
            }
            
            e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 10));
            e.Graphics.DrawString("№", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos + 10));
            e.Graphics.DrawString("Channel Name", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 35, yPos + 10));
            e.Graphics.DrawString("Address", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 200, yPos + 10));
            e.Graphics.DrawString("Format", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 260, yPos + 10));
            e.Graphics.DrawString("Dimension", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 320, yPos + 10));
            e.Graphics.DrawString("TypeLine", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 400, yPos + 10));
            e.Graphics.DrawString("Phase", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 470, yPos + 10));
            e.Graphics.DrawString("CCBM", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 520, yPos + 10));
            e.Graphics.DrawString("TypeAD", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 570, yPos + 10));
            e.Graphics.DrawString("Min", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 630, yPos + 10));
            e.Graphics.DrawString("Max", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 690, yPos + 10));
            e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 15));
            for (int i = paramNum, j = 0; i < ScopeSysType.ChannelNames.Count;j++, i++)
            {
                e.Graphics.DrawString((i + 1).ToString() + ".", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.ChannelNames[i], new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 35, yPos));
                e.Graphics.DrawString("0x" + ScopeSysType.ChannelAddrs[i].ToString("X4"), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 200, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelFormats[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 260, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelDimension[i], new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 320, yPos));
                if (ScopeSysType.ChannelStepLines[i] == 0) str = "Smooth";
                else str = "Step";
                e.Graphics.DrawString(str, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 400, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelPhase[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 470, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelCCBM[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 520, yPos));
                if (ScopeSysType.ChannelTypeAD[i] == 0) str = "Analog";
                else str = "Digital";
                e.Graphics.DrawString(str, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 570, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelMin[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 630, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelMax[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 690, yPos));
                if (FirstPage == true && j == 40) { paramNum += (j + 1); e.HasMorePages = true; FirstPage = false; break; }
                if (FirstPage == false && j == 52) { paramNum += (j + 1); e.HasMorePages = true; break; }
                if (i == ScopeSysType.ChannelNames.Count - 1) { FirstPage = true; paramNum = 0; e.HasMorePages = false; }
            }
        }

        private void ConfigToSystem()
        {
            string str ="";
            str = Path.GetFileName(ScopeSysType.xmlFileName);
            ConfigToSystem_label.Text = "Actual configuration: " + str;
        }


    }
}

