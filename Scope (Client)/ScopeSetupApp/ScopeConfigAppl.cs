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
        List<Panel> _layoutPanel = new List<Panel>();

        //Номер
        List<Label> _numLabels = new List<Label>();

        //Группа
        List<TextBox> _groupTextBoxs = new List<TextBox>();

        //Названия
        List<TextBox> _nameTextBoxs = new List<TextBox>();

        //Фаза
        List<TextBox> _phaseTextBoxs = new List<TextBox>();

        //ccbm
        List<TextBox> _ccbmTextBoxs = new List<TextBox>();

        //Размерность физической величины 
        List<ComboBox> _analogDigitalComboBox = new List<ComboBox>();

        //Адреса
        List<TextBox> _addrTextBoxs = new List<TextBox>();

        //форматы данных
        List<ComboBox> _formatComboBoxNumeric = new List<ComboBox>();
        List<ComboBox> _formatComboBox = new List<ComboBox>();

        //Размерность физической величины 
        List<ComboBox> _dimensionComboBox = new List<ComboBox>();

        List<TextBox> _minTextBoxs = new List<TextBox>();
        List<TextBox> _maxTextBoxs = new List<TextBox>();

        //Удаление 
        List<Button> _removeButtons = new List<Button>();

        //Отметка 
        List<CheckBox> _checkBoxs = new List<CheckBox>();

        //Line
        //List<> Line = new List<?>();

        object[] _format = new object[]{
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

        object[] _sizeFormat = new object[]{
            "16",
            "32",
            "64",
        };

        object[] _dimension = new object []{
            "NONE",
        };


        private void AddParamLine(string lineName, string linePhase, string lineCcbm, string lineDimension, string lineGroup, int lineAddr,  int formatData,  int lineTypeAd, int min, int max)
        {
            int i;

            _nameTextBoxs.Add(new TextBox());
            i = _nameTextBoxs.Count - 1;
            _nameTextBoxs[i].Dock = DockStyle.None;
            _nameTextBoxs[i].Font = new Font("Arial", 9);
            _nameTextBoxs[i].AutoSize = false;
            _nameTextBoxs[i].Left = 187;
            _nameTextBoxs[i].Top = 3;
            _nameTextBoxs[i].Width = 150;
            _nameTextBoxs[i].Height = 24;
            _nameTextBoxs[i].Text = lineName;

            _groupTextBoxs.Add(new TextBox());
            _groupTextBoxs[i].Dock = DockStyle.None;
            _groupTextBoxs[i].Font = new Font("Arial", 9);
            _groupTextBoxs[i].AutoSize = false;
            _groupTextBoxs[i].Left = 34;
            _groupTextBoxs[i].Top = 3;
            _groupTextBoxs[i].Width = 150;
            _groupTextBoxs[i].Height = 24;
            _groupTextBoxs[i].Text = lineGroup;

            _numLabels.Add(new Label());
            _numLabels[i].Text = Convert.ToString(i + 1) + ".";
            _numLabels[i].Left = 2;
            _numLabels[i].Top = 6;

            _analogDigitalComboBox.Add(new ComboBox());
            _analogDigitalComboBox[i].Tag = i;
            _analogDigitalComboBox[i].Dock = DockStyle.None;
            _analogDigitalComboBox[i].Font = new Font("Arial", 9);
            _analogDigitalComboBox[i].Left = 34;
            _analogDigitalComboBox[i].Width = 150;
            _analogDigitalComboBox[i].DropDownStyle = ComboBoxStyle.DropDownList;
            _analogDigitalComboBox[i].Items.Add("Analog");
            _analogDigitalComboBox[i].Items.Add("Digital");
            _analogDigitalComboBox[i].SelectedIndex = lineTypeAd;
            _analogDigitalComboBox[i].Top = 30;
            
            _phaseTextBoxs.Add(new TextBox());
            _phaseTextBoxs[i].Dock = DockStyle.None;
            _phaseTextBoxs[i].Font = new Font("Arial", 9);
            _phaseTextBoxs[i].AutoSize = false;
            _phaseTextBoxs[i].Left = 187;
            _phaseTextBoxs[i].Top = 30;
            _phaseTextBoxs[i].Width = 50;
            _phaseTextBoxs[i].Height = 24;
            _phaseTextBoxs[i].Text = linePhase;

            _ccbmTextBoxs.Add(new TextBox());
            _ccbmTextBoxs[i].Dock = DockStyle.None;
            _ccbmTextBoxs[i].Font = new Font("Arial", 9);
            _ccbmTextBoxs[i].AutoSize = false;
            _ccbmTextBoxs[i].Left = 240;
            _ccbmTextBoxs[i].Top = 30;
            _ccbmTextBoxs[i].Width = 97;
            _ccbmTextBoxs[i].Height = 24;
            _ccbmTextBoxs[i].Text = lineCcbm;
            
            _dimensionComboBox.Add(new ComboBox());
            _dimensionComboBox[i].Tag = i;
            _dimensionComboBox[i].Dock = DockStyle.None;
            _dimensionComboBox[i].Font = new Font("Arial", 9);
            _dimensionComboBox[i].Items.AddRange(_dimension);
            _dimensionComboBox[i].Left = 340;
            _dimensionComboBox[i].Top = 30;
            _dimensionComboBox[i].Width = 90;
            _dimensionComboBox[i].Text = lineDimension;
            _dimensionComboBox[i].DropDownStyle = ComboBoxStyle.DropDown;

            _addrTextBoxs.Add(new TextBox());
            _addrTextBoxs[i].Dock = DockStyle.None;
            _addrTextBoxs[i].Font = new Font("Arial", 9);
            _addrTextBoxs[i].AutoSize = false;
            _addrTextBoxs[i].Left = 340;
            _addrTextBoxs[i].Top = 3;
            _addrTextBoxs[i].Width = 90;
            _addrTextBoxs[i].Height = 24;
            _addrTextBoxs[i].Text = "0x" + lineAddr.ToString("X4");
            _addrTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            _formatComboBoxNumeric.Add(new ComboBox());
            _formatComboBoxNumeric[i].Tag = i;
            _formatComboBoxNumeric[i].Dock = DockStyle.None;
            _formatComboBoxNumeric[i].Font = new Font("Arial", 9);
            _formatComboBoxNumeric[i].Items.AddRange(_sizeFormat);
            _formatComboBoxNumeric[i].Width = 100;
            _formatComboBoxNumeric[i].Left = 437;
            _formatComboBoxNumeric[i].Top = 3;
            _formatComboBoxNumeric[i].SelectedIndex = (formatData >> 8) - 1;
            _formatComboBoxNumeric[i].DropDownStyle = ComboBoxStyle.DropDownList;
            
            _formatComboBox.Add(new ComboBox());
            _formatComboBox[i].Tag = i;
            _formatComboBox[i].Dock = DockStyle.None;
            _formatComboBox[i].Font = new Font("Arial", 9);
            _formatComboBox[i].DropDownStyle = ComboBoxStyle.DropDownList;
            _formatComboBox[i].Items.AddRange(_format);
            _formatComboBox[i].Width = 100;
            _formatComboBox[i].Left = 540;
            _formatComboBox[i].Top = 3;
            _formatComboBox[i].SelectedIndex = formatData & 0x00FF ;
            
            _minTextBoxs.Add(new TextBox());
            _minTextBoxs[i].Dock = DockStyle.None;
            _minTextBoxs[i].Font = new Font("Arial", 9);
            _minTextBoxs[i].AutoSize = false;
            _minTextBoxs[i].Left = 437;
            _minTextBoxs[i].Top = 30;
            _minTextBoxs[i].Width = 100;
            _minTextBoxs[i].Height = 24;
            _minTextBoxs[i].Text = min.ToString();
            _minTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            _maxTextBoxs.Add(new TextBox());
            _maxTextBoxs[i].Dock = DockStyle.None;
            _maxTextBoxs[i].Font = new Font("Arial", 9);
            _maxTextBoxs[i].AutoSize = false;
            _maxTextBoxs[i].Left = 540;
            _maxTextBoxs[i].Top = 30;
            _maxTextBoxs[i].Width = 100;
            _maxTextBoxs[i].Height = 24;
            _maxTextBoxs[i].Text = max.ToString();
            _maxTextBoxs[i].TextAlign = HorizontalAlignment.Right;

            _removeButtons.Add(new Button());
            _removeButtons[i].Tag = i;
            _removeButtons[i].Text = "Удалить";
    //        removeButtons[i].Location = new System.Drawing.Point(700, 8);
            _removeButtons[i].Margin = new Padding(0,0,0,0);
          //  removeButtons[i].Image = Properties.Resources.Minus_32;
           // removeButtons[i].ImageAlign = ContentAlignment.MiddleCenter;
            _removeButtons[i].Size = new System.Drawing.Size(40, 40);
            _removeButtons[i].Dock = DockStyle.Right;
        //removeButtons[i].Left = 770;
            //removeButtons[i].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            _removeButtons[i].AutoSize = true;
            //removeButtons[i].Top = 8;
            _removeButtons[i].Click += new EventHandler(deleteButton_Click);

            _checkBoxs.Add(new CheckBox());
            _checkBoxs[i].Tag = i;
            _checkBoxs[i].Dock = DockStyle.Right;
            _checkBoxs[i].AutoSize = false;
            _checkBoxs[i].Size = new System.Drawing.Size(16, 16);
            _checkBoxs[i].Top = 18;
            _checkBoxs[i].Click += new System.EventHandler(checkBox_Click);
            
            _layoutPanel.Add(new Panel());
          //  LayoutPanel[i].Visible = false;
            _layoutPanel[i].Dock = DockStyle.Top;
            _layoutPanel[i].BackColor = Color.WhiteSmoke;
            _layoutPanel[i].Left = 5;
            _layoutPanel[i].Top = 5 + 63 * i;
            _layoutPanel[i].Width = 800;
            _layoutPanel[i].Height = 58;
            _layoutPanel[i].BorderStyle = BorderStyle.FixedSingle;
            
            configPanel.Controls.Add(_layoutPanel[i]);
            _layoutPanel[i].Controls.Add(_nameTextBoxs[i]);
            _layoutPanel[i].Controls.Add(_groupTextBoxs[i]);
            _layoutPanel[i].Controls.Add(_phaseTextBoxs[i]);
            _layoutPanel[i].Controls.Add(_ccbmTextBoxs[i]);
            _layoutPanel[i].Controls.Add(_addrTextBoxs[i]);
            _layoutPanel[i].Controls.Add(_dimensionComboBox[i]);
            _layoutPanel[i].Controls.Add(_formatComboBoxNumeric[i]);
            _layoutPanel[i].Controls.Add(_formatComboBox[i]);
            _layoutPanel[i].Controls.Add(_analogDigitalComboBox[i]);
            _layoutPanel[i].Controls.Add(_minTextBoxs[i]);
            _layoutPanel[i].Controls.Add(_maxTextBoxs[i]);
            _layoutPanel[i].Controls.Add(_removeButtons[i]);
            _layoutPanel[i].Controls.Add(_numLabels[i]);
            _layoutPanel[i].Controls.Add(_checkBoxs[i]);

        }

        private void DeleteLine(int lineNum)
        {
            if (lineNum >= _nameTextBoxs.Count) { return; }
            int i = lineNum;
            for (i = lineNum; i < _nameTextBoxs.Count - 1; i++)
            {
                _nameTextBoxs[i].Text = _nameTextBoxs[i + 1].Text;
                _phaseTextBoxs[i].Text = _phaseTextBoxs[i + 1].Text;
                _ccbmTextBoxs[i].Text = _ccbmTextBoxs[i + 1].Text;
                _dimensionComboBox[i].Text = _dimensionComboBox[i + 1].Text;
                _addrTextBoxs[i].Text = _addrTextBoxs[i + 1].Text;
                _groupTextBoxs[i].Text = _groupTextBoxs[i + 1].Text;
                _formatComboBoxNumeric[i].Text = _formatComboBoxNumeric[i + 1].Text;
                _formatComboBox[i].Text = _formatComboBox[i + 1].Text;
                _analogDigitalComboBox[i].Text = _analogDigitalComboBox[i + 1].Text;
                _minTextBoxs[i].Text = _minTextBoxs[i + 1].Text;
                _maxTextBoxs[i].Text = _maxTextBoxs[i + 1].Text;
            }
            i = _nameTextBoxs.Count - 1;

            configPanel.Controls.Remove(_layoutPanel[i]);
            _layoutPanel[i].Controls.Remove(_nameTextBoxs[i]);
            _layoutPanel[i].Controls.Remove(_phaseTextBoxs[i]);
            _layoutPanel[i].Controls.Remove(_ccbmTextBoxs[i]);
            _layoutPanel[i].Controls.Remove(_dimensionComboBox[i]);
            _layoutPanel[i].Controls.Remove(_addrTextBoxs[i]);
            _layoutPanel[i].Controls.Remove(_removeButtons[i]);
            _layoutPanel[i].Controls.Remove(_formatComboBoxNumeric[i]);
            _layoutPanel[i].Controls.Remove(_formatComboBox[i]);
            _layoutPanel[i].Controls.Remove(_analogDigitalComboBox[i]);
            _layoutPanel[i].Controls.Remove(_minTextBoxs[i]);
            _layoutPanel[i].Controls.Remove(_maxTextBoxs[i]);
            _layoutPanel[i].Controls.Remove(_numLabels[i]);
            _layoutPanel[i].Controls.Remove(_checkBoxs[i]);
            _layoutPanel[i].Controls.Remove(_groupTextBoxs[i]);            

            _nameTextBoxs.Remove(_nameTextBoxs[i]);
            _phaseTextBoxs.Remove(_phaseTextBoxs[i]);
            _ccbmTextBoxs.Remove(_ccbmTextBoxs[i]);
            _dimensionComboBox.Remove(_dimensionComboBox[i]);
            _addrTextBoxs.Remove(_addrTextBoxs[i]);
            _removeButtons.Remove(_removeButtons[i]);
            _formatComboBoxNumeric.Remove(_formatComboBoxNumeric[i]);
            _formatComboBox.Remove(_formatComboBox[i]);
            _analogDigitalComboBox.Remove(_analogDigitalComboBox[i]);
            _minTextBoxs.Remove(_minTextBoxs[i]);
            _maxTextBoxs.Remove(_maxTextBoxs[i]);
            _numLabels.Remove(_numLabels[i]);
            _checkBoxs.Remove(_checkBoxs[i]);
            _groupTextBoxs.Remove(_groupTextBoxs[i]);
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
            AddParamLine("Параметр ", "", "", "NONE", "", _addrTextBoxs.Count, 16, 1, -1, 1);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int i = (int)((sender as Button).Tag);

            DeleteLine(i);
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _checkBoxs.Count; i++)
            {
                if (_checkBoxs[i].Checked) _layoutPanel[i].BackColor = Color.PaleGreen;
                else _layoutPanel[i].BackColor = Color.WhiteSmoke;
            }
        }

        List<String> _nameTextBoxsCopy = new List<String>();
        List<String> _phaseTextBoxsCopy = new List<String>();
        List<String> _ccbmTextBoxsCopy = new List<String>();
        List<String> _dimensionComboBoxCopy = new List<String>();
        List<String> _groupTextBoxsCopy = new List<String>();
        List<int> _analogDigitalComboBoxCopy = new List<int>();
        List<int> _addrTextBoxsCopy = new List<int>();
        List<int> _formatComboBoxCopy = new List<int>();
        List<int> _minTextBoxsCopy = new List<int>();
        List<int> _maxTextBoxsCopy = new List<int>();

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _nameTextBoxsCopy.Clear();
            _phaseTextBoxsCopy.Clear();
            _ccbmTextBoxsCopy.Clear();
            _analogDigitalComboBoxCopy.Clear();
            _groupTextBoxsCopy.Clear();
            _addrTextBoxsCopy.Clear();
            _formatComboBoxCopy.Clear();
            _dimensionComboBoxCopy.Clear();
            _minTextBoxsCopy.Clear();
            _maxTextBoxsCopy.Clear();

            for (int i = 0; i < _checkBoxs.Count; i++) 
            {
                if (_checkBoxs[i].Checked) 
                {
                    _nameTextBoxsCopy.Add(_nameTextBoxs[i].Text);
                    _phaseTextBoxsCopy.Add(_phaseTextBoxs[i].Text);
                    _ccbmTextBoxsCopy.Add(_ccbmTextBoxs[i].Text);
                    _analogDigitalComboBoxCopy.Add(Convert.ToInt32(_analogDigitalComboBox[i].SelectedIndex));
                    _groupTextBoxsCopy.Add(_groupTextBoxs[i].Text);
                    _addrTextBoxsCopy.Add(Convert.ToInt32(AdvanceConvert.StrToInt(_addrTextBoxs[i].Text)));
                    _formatComboBoxCopy.Add((Convert.ToInt32(_formatComboBoxNumeric[i].SelectedIndex + 1) << 8) + Convert.ToInt32(_formatComboBox[i].SelectedIndex));
                    _dimensionComboBoxCopy.Add(_dimensionComboBox[i].Text);
                    _minTextBoxsCopy.Add(Convert.ToInt32(_minTextBoxs[i].Text));
                    _maxTextBoxsCopy.Add(Convert.ToInt32(_maxTextBoxs[i].Text));
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < _nameTextBoxsCopy.Count; i++)
            {    
                 AddParamLine(_nameTextBoxsCopy[i],_phaseTextBoxsCopy[i],_ccbmTextBoxsCopy[i],_dimensionComboBoxCopy[i], _groupTextBoxsCopy[i], _addrTextBoxsCopy[i], _formatComboBoxCopy[i], _analogDigitalComboBoxCopy[i],_minTextBoxsCopy[i],_maxTextBoxsCopy[i]);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = _nameTextBoxs.Count - 1; i >= 0; i--) 
            {
                if (_checkBoxs[i].Checked) 
                {
                    _layoutPanel[i].BackColor = Color.WhiteSmoke;
                    _checkBoxs[i].Checked = false;
                    DeleteLine(i); 
                }
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                for (int i = 0; i < _checkBoxs.Count; i++)
                {
                    _checkBoxs[i].Checked  = true;
                    _layoutPanel[i].BackColor = Color.PaleGreen;     
                }
            }
            else
            {
                for (int i = 0; i < _checkBoxs.Count; i++)
                {
                    _checkBoxs[i].Checked = false;
                    _layoutPanel[i].BackColor = Color.WhiteSmoke;
                }
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
                ScopeSysType.XmlFileName = ofd.FileName;
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
                tmqCode_textBox.Text = ScopeSysType.TmqCode.ToString();
                leapsec_textBox.Text = ScopeSysType.Leapsec.ToString();
                
                for (int i = _nameTextBoxs.Count - 1; i >= 0 ; i--)
                {
                    configPanel.Controls.Remove(_layoutPanel[i]);
                    _layoutPanel[i].Controls.Remove(_nameTextBoxs[i]);
                    _layoutPanel[i].Controls.Remove(_phaseTextBoxs[i]);
                    _layoutPanel[i].Controls.Remove(_ccbmTextBoxs[i]);
                    _layoutPanel[i].Controls.Remove(_dimensionComboBox[i]);
                    _layoutPanel[i].Controls.Remove(_addrTextBoxs[i]);
                    _layoutPanel[i].Controls.Remove(_removeButtons[i]);
                    _layoutPanel[i].Controls.Remove(_formatComboBoxNumeric[i]);
                    _layoutPanel[i].Controls.Remove(_formatComboBox[i]);
                    _layoutPanel[i].Controls.Remove(_analogDigitalComboBox[i]);
                    _layoutPanel[i].Controls.Remove(_minTextBoxs[i]);
                    _layoutPanel[i].Controls.Remove(_maxTextBoxs[i]);
                    _layoutPanel[i].Controls.Remove(_groupTextBoxs[i]);

                    _nameTextBoxs.Remove(_nameTextBoxs[i]);
                    _phaseTextBoxs.Remove(_phaseTextBoxs[i]);
                    _ccbmTextBoxs.Remove(_ccbmTextBoxs[i]);
                    _dimensionComboBox.Remove(_dimensionComboBox[i]);
                    _addrTextBoxs.Remove(_addrTextBoxs[i]);
                    _removeButtons.Remove(_removeButtons[i]);
                    _formatComboBoxNumeric.Remove(_formatComboBoxNumeric[i]);
                    _formatComboBox.Remove(_formatComboBox[i]);
                    _analogDigitalComboBox.Remove(_analogDigitalComboBox[i]);
                    _minTextBoxs.Remove(_minTextBoxs[i]);
                    _maxTextBoxs.Remove(_maxTextBoxs[i]);
                    _groupTextBoxs.Remove(_groupTextBoxs[i]);
                }


                for (int i1 = 0; i1 < ScopeSysType.ChannelNames.Count; i1++)
                {

                    AddParamLine(
                                    ScopeSysType.ChannelNames[i1],
                                    ScopeSysType.ChannelPhase[i1],
                                    ScopeSysType.ChannelCcbm[i1],
                                    ScopeSysType.ChannelDimension[i1],
                                    ScopeSysType.GroupNames[i1],
                                    ScopeSysType.ChannelAddrs[i1],
                                    ScopeSysType.ChannelFormats[i1],
                                    ScopeSysType.ChannelTypeAd[i1],
                                    ScopeSysType.ChannelMin[i1],
                                    ScopeSysType.ChannelMax[i1]  
                                 );
                }

            /*    for (int i = 0; i <  ScopeSysType.ChannelNames.Count; i++)
                {
                    LayoutPanel[i].Visible = true;
                }*/
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

                for (int i = 0; i < _addrTextBoxs.Count; i++)
                {
                    if (!AdvanceConvert.StrToInt(_addrTextBoxs[i].Text))
                    {
                        MessageBox.Show("Ошибка в поле адреса параметра\n" + _nameTextBoxs[i].Text);
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

                ScopeSysType.XmlFileName = sfd.FileName;

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
                xmlOut.WriteAttributeString("Count", _nameTextBoxs.Count.ToString());
                xmlOut.WriteEndElement();

                for (int i = 0; i < paramAddrStrs.Count; i++)
                {
                    xmlOut.WriteStartElement("MeasureParam" + (i + 1).ToString());
                    xmlOut.WriteAttributeString("Name", _groupTextBoxs[i].Text +"/" + _nameTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Phase", _phaseTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("CCBM", _ccbmTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Dimension", _dimensionComboBox[i].Text);
                    xmlOut.WriteAttributeString("Addr", paramAddrStrs[i]);
                    xmlOut.WriteAttributeString("Format", ((Convert.ToInt32(_formatComboBoxNumeric[i].SelectedIndex + 1) << 8) + Convert.ToInt32(_formatComboBox[i].SelectedIndex)).ToString());
                    xmlOut.WriteAttributeString("TypeAD", _analogDigitalComboBox[i].SelectedIndex.ToString());
                    xmlOut.WriteAttributeString("Min", _minTextBoxs[i].Text);
                    xmlOut.WriteAttributeString("Max", _maxTextBoxs[i].Text);

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
            pathfile = Path.GetDirectoryName(ScopeSysType.XmlFileName);
            sfd.FileName = pathfile + "\\" + namefile;
            Save_To_file(sfd); 

            Update_Oscil();
        }

        private string convert_text(object obj, string del)
        {
            int i = 0;
            string str = "";
            str = Convert.ToString(obj);
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
            ScopeSysType.TmqCode = Convert.ToString(tmqCode_textBox.Text);
            ScopeSysType.Leapsec = Convert.ToString(leapsec_textBox.Text);
            
            ScopeSysType.ChannelNames.Clear();
            ScopeSysType.GroupNames.Clear();
            ScopeSysType.ChannelPhase.Clear();
            ScopeSysType.ChannelCcbm.Clear();
            ScopeSysType.ChannelDimension.Clear();
            ScopeSysType.ChannelAddrs.Clear();
            ScopeSysType.ChannelFormats.Clear();
            ScopeSysType.ChannelTypeAd.Clear();
            ScopeSysType.ChannelMin.Clear();
            ScopeSysType.ChannelMax.Clear();
            
            for (int i = 0; i < _addrTextBoxs.Count; i++)
            {
                ScopeSysType.GroupNames.Add(_groupTextBoxs[i].Text);
                ScopeSysType.ChannelNames.Add(_nameTextBoxs[i].Text);
                ScopeSysType.ChannelPhase.Add(_phaseTextBoxs[i].Text);
                ScopeSysType.ChannelCcbm.Add(_ccbmTextBoxs[i].Text);
                ScopeSysType.ChannelDimension.Add(_dimensionComboBox[i].Text);
                ScopeSysType.ChannelAddrs.Add(Convert.ToUInt16(convert_text(_addrTextBoxs[i].Text, "0x")));
                ScopeSysType.ChannelFormats.Add(Convert.ToUInt16(((Convert.ToUInt16(_formatComboBoxNumeric[i].SelectedIndex + 1) << 8) + Convert.ToInt32(_formatComboBox[i].SelectedIndex)).ToString()));
                ScopeSysType.ChannelTypeAd.Add(Convert.ToUInt16(_analogDigitalComboBox[i].SelectedIndex.ToString()));
                ScopeSysType.ChannelMin.Add(Convert.ToInt32(_minTextBoxs[i].Text));
                ScopeSysType.ChannelMax.Add(Convert.ToInt32(_maxTextBoxs[i].Text));
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

        bool _firstPage = true;
        int _paramNum = 0;

        private void SCPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "";
            int yPos = 30;
            int xPos1 = 25;
            int xPos2 = 200;
            int xPos3 = 350;
            int xPos4 = 520;

            if (_firstPage == true)
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
                e.Graphics.DrawString(ScopeSysType.TmqCode.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
                e.Graphics.DrawString("leapsec:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.Leapsec.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
            }
            
            e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 10));
            e.Graphics.DrawString("№", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos + 10));
            e.Graphics.DrawString("Channel Name", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 35, yPos + 10));
            e.Graphics.DrawString("Address", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 200, yPos + 10));
            e.Graphics.DrawString("Format", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 260, yPos + 10));
            e.Graphics.DrawString("Dimension", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 320, yPos + 10));
            e.Graphics.DrawString("Phase", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 470, yPos + 10));
            e.Graphics.DrawString("CCBM", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 520, yPos + 10));
            e.Graphics.DrawString("TypeAD", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 570, yPos + 10));
            e.Graphics.DrawString("Min", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 630, yPos + 10));
            e.Graphics.DrawString("Max", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 690, yPos + 10));
            e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 15));
            for (int i = _paramNum, j = 0; i < ScopeSysType.ChannelNames.Count;j++, i++)
            {
                e.Graphics.DrawString((i + 1).ToString() + ".", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
                e.Graphics.DrawString(ScopeSysType.ChannelNames[i], new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 35, yPos));
                e.Graphics.DrawString("0x" + ScopeSysType.ChannelAddrs[i].ToString("X4"), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 200, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelFormats[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 260, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelDimension[i], new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 320, yPos));

                e.Graphics.DrawString(ScopeSysType.ChannelPhase[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 470, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelCcbm[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 520, yPos));
                if (ScopeSysType.ChannelTypeAd[i] == 0) str = "Analog";
                else str = "Digital";
                e.Graphics.DrawString(str, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 570, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelMin[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 630, yPos));
                e.Graphics.DrawString(ScopeSysType.ChannelMax[i].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 690, yPos));
                if (_firstPage == true && j == 40) { _paramNum += (j + 1); e.HasMorePages = true; _firstPage = false; break; }
                if (_firstPage == false && j == 52) { _paramNum += (j + 1); e.HasMorePages = true; break; }
                if (i == ScopeSysType.ChannelNames.Count - 1) { _firstPage = true; _paramNum = 0; e.HasMorePages = false; }
            }
        }

        private void ConfigToSystem()
        {
            string str ="";
            str = Path.GetFileName(ScopeSysType.XmlFileName);
            ConfigToSystem_label.Text = "Actual configuration: " + str;
        }


    }
}

