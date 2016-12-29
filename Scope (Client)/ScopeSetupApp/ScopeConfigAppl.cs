using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using ADSPLibrary;
using System.Drawing;

namespace ScopeSetupApp
{
    public partial class ScopeConfigForm : Form
    {
        readonly object[] _typeChannel = 
        {
            "Analog",
            "Digital"
        };

        readonly object[] _format = 
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
            "21 - Freq UPTF"
        };

        readonly object[] _sizeFormat =
        {
            "16",
            "32",
            "64",
        };

        public ScopeConfigForm()
        {
            InitializeComponent();
            ConfigToSystem();

            InitTable();
        }

        void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }

        private void addLineButton_Click(object sender, EventArgs e)
        {
            ChanneldataGridView.Rows.Add("Параметр", "", _typeChannel[0], "0x" + (ChanneldataGridView.Rows.Count - 1).ToString("X4"), _sizeFormat[0], _format[0], "", "", "NONE", -1, 1);
            UpdateTable();
        }

        private static readonly List<ScopeTempConfig> ScopeItemCopy = new List<ScopeTempConfig>();
        

        private void copyButton_Click(object sender, EventArgs e)
        {
            ScopeItemCopy.Clear();
            foreach (DataGridViewRow item in ChanneldataGridView.SelectedRows)
            {
                ScopeTempConfig itemCopy = new ScopeTempConfig()
                {
                    ChannelNames = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[0].Value),
                    ChannelGroupNames = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[1].Value),
                    ChannelTypeAd = Convert.ToUInt16(Array.IndexOf(_typeChannel, ChanneldataGridView.Rows[item.Index].Cells[2].Value)),
                    ChannelAddrs = Convert.ToUInt16(convert_text(ChanneldataGridView.Rows[item.Index].Cells[3].Value, "0x")),
                    ChannelformatNumeric = Convert.ToInt32(Array.IndexOf(_sizeFormat, ChanneldataGridView.Rows[item.Index].Cells[4].Value)),
                    ChannelFormats = Convert.ToInt32(Array.IndexOf(_format, ChanneldataGridView.Rows[item.Index].Cells[5].Value)),
                    ChannelPhase = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[6].Value),
                    ChannelCcbm = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[7].Value),
                    ChannelDimension = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[8].Value),
                    ChannelMin = Convert.ToInt32(ChanneldataGridView.Rows[item.Index].Cells[9].Value),
                    ChannelMax = Convert.ToInt32(ChanneldataGridView.Rows[item.Index].Cells[10].Value)
                };
                
                ScopeItemCopy.Add(itemCopy);
            }
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            foreach (var t in ScopeItemCopy)
            {
                ChanneldataGridView.Rows.Add(
                        t.ChannelNames,
                        t.ChannelGroupNames,
                        _typeChannel[(t.ChannelTypeAd)],
                        "0x" + t.ChannelAddrs.ToString("X4"),
                        _sizeFormat[(t.ChannelformatNumeric)],
                        _format[(t.ChannelFormats)],
                        t.ChannelPhase,
                        t.ChannelCcbm,
                        t.ChannelDimension,
                        t.ChannelMin,
                        t.ChannelMax
                        );
            }
            UpdateTable();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in ChanneldataGridView.SelectedRows)
            {
                ChanneldataGridView.Rows.RemoveAt(item.Index);
            }
            UpdateTable();
        }

        private string convert_text(object obj, string del)
        {
            int i = 0;
            string str = Convert.ToString(obj);
            if (str == "") str = "0";
            if (del == "0x") i = Convert.ToInt32(str, 16);
            if (del == "") i = Convert.ToInt32(str);
            str = Convert.ToString(i);
            //if (del != "") str = str.Replace(del, "");
            return str;
        }

        private void UpdateTable()
        {
            foreach (DataGridViewRow r in ChanneldataGridView.Rows)
            {
                ChanneldataGridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
            }

            SetDoubleBuffered(ChanneldataGridView, true);
        }

        private void CreateTable()
        {
            foreach (var t in ScopeSysType.ScopeItem)
            {
                try
                {
                    ChanneldataGridView.Rows.Add(
                        t.ChannelNames,
                        t.ChannelGroupNames,
                        _typeChannel[(t.ChannelTypeAd)],
                        "0x" + t.ChannelAddrs.ToString("X4"),
                        _sizeFormat[(t.ChannelformatNumeric)],
                        _format[(t.ChannelFormats)],
                        t.ChannelPhase,
                        t.ChannelCcbm,
                        t.ChannelDimension,
                        t.ChannelMin,
                        t.ChannelMax
                        );
                }
                catch
                {
                    MessageBox.Show(@"Ошибка загрузки данных", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        //Загрузка из файла
        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                DefaultExt = @".xsc",                                                           // Default file extension
                Filter = @"XML System Configuration|*.xsc|XML|*.xml|All files (*.*)|*.*"         // Filter files by extension 
            };
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ScopeSysType.XmlFileName = ofd.FileName;
                try
                {
                    ChanneldataGridView.Rows.Clear();
                    ScopeSysType.ScopeItem.Clear();
                    ScopeSysType.InitScopeSysType();
                }
                catch
                {
                    MessageBox.Show(@"Ошибка загрузки данных", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                InitTable();
            }
        }

        private void InitTable()
        {
            ConfigAddr_textBox.Text = @"0x" + ScopeSysType.ConfigurationAddr.ToString("X4");
            OscilCmndAddr_textBox.Text = @"0x" + ScopeSysType.OscilCmndAddr.ToString("X4");
            OscilSizeData_TextBox.Text = @"0x" + ScopeSysType.OscilAllSize.ToString("X4");
            CommentRichTextBox.Text = ScopeSysType.OscilComment;
            //For COMETRADE
            stationName_textBox.Text = ScopeSysType.StationName;
            recordingDevice_textBox.Text = ScopeSysType.RecordingDevice;
            nominalFrequency_textBox.Text = ScopeSysType.OscilNominalFrequency.ToString("###");
            sampleRate_textBox.Text = ScopeSysType.OscilSampleRate.ToString("###");
            timeCode_textBox.Text = ScopeSysType.TimeCode;
            localCode_textBox.Text = ScopeSysType.LocalCode;
            tmqCode_textBox.Text = ScopeSysType.TmqCode;
            leapsec_textBox.Text = ScopeSysType.Leapsec;

            CreateTable();
            UpdateTable();
        }

        private void Save_To_file(SaveFileDialog sfd)
        {
                string oscillSizeDataStr, oscilCmndStr, configStr;
                List<string> paramAddrStrs = new List<string>();

                if (!AdvanceConvert.StrToInt(ConfigAddr_textBox.Text))
                {
                    MessageBox.Show(@"Ошибка в поле Configuration Addr");
                    return;
                }
                else
                {
                    configStr = AdvanceConvert.uValue.ToString();
                }


                if (!AdvanceConvert.StrToInt(OscilCmndAddr_textBox.Text))
                {
                    MessageBox.Show(@"Ошибка в поле Oscil Cmnd Addr");
                    return;
                }
                else
                {
                    oscilCmndStr = AdvanceConvert.uValue.ToString();
                }

                for (int i = 0; i <  ChanneldataGridView.Rows.Count; i++)
                {
                    if (!AdvanceConvert.StrToInt(Convert.ToString(ChanneldataGridView.Rows[i].Cells[3].Value)))
                    {
                        MessageBox.Show(@"Ошибка в поле адреса параметра\n" + Convert.ToString(ChanneldataGridView.Rows[i].Cells[3].Value));
                        return;
                    }
                    else
                    {
                        paramAddrStrs.Add(AdvanceConvert.uValue.ToString());
                    }

                }

                if (!AdvanceConvert.StrToInt(OscilSizeData_TextBox.Text))
                {
                    MessageBox.Show(@"Ошибка в поле Oscill Size Data");
                    return;
                }
                else
                {
                    oscillSizeDataStr = AdvanceConvert.uValue.ToString();
                }

                ScopeSysType.XmlFileName = sfd.FileName;

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode)
                {
                    Formatting = Formatting.Indented
                };
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
                xmlOut.WriteAttributeString("Count", ChanneldataGridView.Rows.Count.ToString());
                xmlOut.WriteEndElement();

                for (int i = 0; i < ChanneldataGridView.Rows.Count; i++)
                {
                    xmlOut.WriteStartElement("MeasureParam" + (i + 1));
                    xmlOut.WriteAttributeString("Name", Convert.ToString(ChanneldataGridView.Rows[i].Cells[1].Value) + "/" + Convert.ToString(ChanneldataGridView.Rows[i].Cells[0].Value));
                    xmlOut.WriteAttributeString("Phase", Convert.ToString(ChanneldataGridView.Rows[i].Cells[6].Value));
                    xmlOut.WriteAttributeString("CCBM", Convert.ToString(ChanneldataGridView.Rows[i].Cells[7].Value));
                    xmlOut.WriteAttributeString("Dimension", Convert.ToString(ChanneldataGridView.Rows[i].Cells[8].Value));
                    xmlOut.WriteAttributeString("Addr", paramAddrStrs[i]);
                    xmlOut.WriteAttributeString("Format", Convert.ToString(((Array.IndexOf(_sizeFormat, ChanneldataGridView.Rows[i].Cells[4].Value) + 1) << 8) + Array.IndexOf(_format, ChanneldataGridView.Rows[i].Cells[5].Value)));
                    xmlOut.WriteAttributeString("TypeAD", Convert.ToString(Array.IndexOf(_typeChannel, ChanneldataGridView.Rows[i].Cells[2].Value)));
                    xmlOut.WriteAttributeString("Min", Convert.ToString(ChanneldataGridView.Rows[i].Cells[9].Value));
                    xmlOut.WriteAttributeString("Max", Convert.ToString(ChanneldataGridView.Rows[i].Cells[10].Value));

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
        }

        private void VerificationChannel()
        {
            List<string> nameChannelRepeat = new List<string>();
            List<int> numChannelRepeat = new List<int>();

            for (int i = 0; i < ChanneldataGridView.RowCount; i++)
            {
                nameChannelRepeat.Clear();
                numChannelRepeat.Clear();
                string str = "";

                for (int j = 0; j < ChanneldataGridView.RowCount; j++)
                {
                    //Адреса, разрядность, формат
                    if (Convert.ToString(ChanneldataGridView.Rows[i].Cells[3].Value) == Convert.ToString(ChanneldataGridView.Rows[j].Cells[3].Value) &&
                        ChanneldataGridView.Rows[i].Cells[4].Value == ChanneldataGridView.Rows[j].Cells[4].Value &&
                        ChanneldataGridView.Rows[i].Cells[5].Value == ChanneldataGridView.Rows[j].Cells[5].Value)
                    {
                        str = "Адрес: " + ChanneldataGridView.Rows[i].Cells[3].Value + 
                            " Разряднсоть: " + ChanneldataGridView.Rows[i].Cells[4].Value +
                            " Формат: " + ChanneldataGridView.Rows[i].Cells[5].Value;
                        nameChannelRepeat.Add(Convert.ToString(ChanneldataGridView.Rows[j].Cells[0].Value));
                        numChannelRepeat.Add(j);
                    }
                }
                if (numChannelRepeat.Count > 1)
                {
                    SelectChannel loadOscQueruForm = new SelectChannel(nameChannelRepeat, numChannelRepeat, str);
                    DialogResult dlgr = loadOscQueruForm.ShowDialog();

                    if (dlgr == DialogResult.OK)
                    {
                        for (int k = numChannelRepeat.Count - 1; k >= 0; k--)
                        {
                            if (numChannelRepeat[k] == numChannelRepeat[SelectChannel.NumChannel]) continue;
                            ChanneldataGridView.Rows.RemoveAt(numChannelRepeat[k]);
                        }

                        UpdateTable();
                    }
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            VerificationChannel();

            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = @".xsc",
                Filter = @"XML System Configuration|*.xsc|XML|*.xml"
            };
            // Default file extension
            if (sfd.ShowDialog() == DialogResult.OK) Save_To_file(sfd); 
        }


        private void SetDefault_toolStripButton_Click(object sender, EventArgs e)
        {
            VerificationChannel();

            SaveFileDialog sfd = new SaveFileDialog();
            string namefile = "ScopeSysType.xml";
            string pathfile = Path.GetDirectoryName(Path.GetFullPath(ScopeSysType.XmlFileName));
            sfd.FileName = pathfile + "\\" + namefile;
            Save_To_file(sfd); 

            Update_Oscil();
        }

        private void Update_toolStripButton_Click(object sender, EventArgs e)
        {
            VerificationChannel();

            Update_Oscil();
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
            ScopeSysType.ChannelTypeAd.Clear();
            ScopeSysType.ChannelAddrs.Clear();
            ScopeSysType.ChannelFormats.Clear();
            ScopeSysType.ChannelPhase.Clear();
            ScopeSysType.ChannelCcbm.Clear();
            ScopeSysType.ChannelDimension.Clear();
            ScopeSysType.ChannelMin.Clear();
            ScopeSysType.ChannelMax.Clear();

            ScopeSysType.ScopeItem.Clear();

            foreach (DataGridViewRow item in ChanneldataGridView.Rows)
            {
                ScopeSysType.ChannelNames.Add(Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[0].Value));
                ScopeSysType.GroupNames.Add(Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[1].Value));
                ScopeSysType.ChannelTypeAd.Add(Convert.ToUInt16(Array.IndexOf(_typeChannel, ChanneldataGridView.Rows[item.Index].Cells[2].Value)));
                ScopeSysType.ChannelAddrs.Add(Convert.ToUInt16(convert_text(ChanneldataGridView.Rows[item.Index].Cells[3].Value, "0x")));
                ScopeSysType.ChannelFormats.Add(Convert.ToUInt16(((Array.IndexOf(_sizeFormat, ChanneldataGridView.Rows[item.Index].Cells[4].Value) + 1) << 8) + Array.IndexOf(_format, ChanneldataGridView.Rows[item.Index].Cells[5].Value)));
                ScopeSysType.ChannelPhase.Add(Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[6].Value));
                ScopeSysType.ChannelCcbm.Add(Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[7].Value));
                ScopeSysType.ChannelDimension.Add(Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[8].Value));
                ScopeSysType.ChannelMin.Add(Convert.ToInt32(ChanneldataGridView.Rows[item.Index].Cells[9].Value));
                ScopeSysType.ChannelMax.Add(Convert.ToInt32(ChanneldataGridView.Rows[item.Index].Cells[10].Value));

                // ReSharper disable once InconsistentNaming
                ScopeTempConfig Item = new ScopeTempConfig
                {
                    ChannelNames = ScopeSysType.ChannelNames[ScopeSysType.ChannelNames.Count - 1],
                    ChannelGroupNames = ScopeSysType.GroupNames[ScopeSysType.GroupNames.Count - 1],
                    ChannelTypeAd = ScopeSysType.ChannelTypeAd[ScopeSysType.ChannelTypeAd.Count - 1],
                    ChannelAddrs = ScopeSysType.ChannelAddrs[ScopeSysType.ChannelAddrs.Count - 1],
                    ChannelformatNumeric = (ScopeSysType.ChannelFormats[ScopeSysType.ChannelFormats.Count - 1] >> 8) - 1,
                    ChannelFormats = ScopeSysType.ChannelFormats[ScopeSysType.ChannelFormats.Count - 1] & 0x00FF,
                    ChannelPhase = ScopeSysType.ChannelPhase[ScopeSysType.ChannelPhase.Count - 1],
                    ChannelCcbm = ScopeSysType.ChannelCcbm[ScopeSysType.ChannelCcbm.Count - 1],
                    ChannelDimension = ScopeSysType.ChannelDimension[ScopeSysType.ChannelDimension.Count - 1],
                    ChannelMin = ScopeSysType.ChannelMin[ScopeSysType.ChannelMin.Count - 1],
                    ChannelMax = ScopeSysType.ChannelMax[ScopeSysType.ChannelMax.Count - 1]
                };

                ScopeSysType.ScopeItem.Add(Item);
            }

            ConfigToSystem();

        }

        private void ConfigToSystem()
        {
            string str = Path.GetFileName(ScopeSysType.XmlFileName);
            ConfigToSystem_label.Text = @"Actual configuration: " + str;
        }

        private void ChanneldataGridView_BindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in ChanneldataGridView.Rows)
            {
                ChanneldataGridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
            }
        }

        private void View_toolStripButton_Click(object sender, EventArgs e)
        {
            VerificationChannel();

             SCPrintPreviewDialog.Document = SCPrintDocument;
             SCPrintPreviewDialog.ShowDialog();
        }

        private void Print_toolStripButton_Click(object sender, EventArgs e)
        {
            VerificationChannel();

             SCPrintDialog.Document = SCPrintDocument;
            if (SCPrintDialog.ShowDialog() == DialogResult.OK)
            {
                SCPrintDocument.Print();
            }
        }

        bool _firstPage = true;
        int _paramNum;

       private void SCPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
       {
            string str;
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
        
    }
}

