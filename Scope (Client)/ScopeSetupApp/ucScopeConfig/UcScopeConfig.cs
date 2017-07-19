using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using ADSPLibrary;
using System.Drawing;
using ScopeSetupApp.Format;

namespace ScopeSetupApp.ucScopeConfig
{
	public partial class UcScopeConfig : UserControl
	{
		readonly object[] _typeChannel =
		{
			"Analog",
			"Digital"
		};

		private readonly object[] _format = FormatConverter.ActualFormat;

		readonly object[] _sizeFormat =
		{
			"16",
			"32",
			"64"
		};

		public UcScopeConfig()
		{
			InitializeComponent();

			Column_channelFormats.Items.Clear();
			Column_channelFormats.Items.AddRange(_format);

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

		private static readonly List<ScopeChannelConfig> ScopeItemCopy = new List<ScopeChannelConfig>();

		private void copyButton_Click(object sender, EventArgs e)
		{
			ScopeItemCopy.Clear();
			foreach (DataGridViewRow item in ChanneldataGridView.SelectedRows)
			{
				ScopeChannelConfig itemCopy = new ScopeChannelConfig()
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

				MainForm.ConfigStatusLabel.Invoke();
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

			for (int i = 0; i < ChanneldataGridView.Rows.Count; i++)
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

			xmlOut.WriteStartElement("TmqCode", tmqCode_textBox.Text);
			xmlOut.WriteEndElement();

			xmlOut.WriteStartElement("Leapsec", leapsec_textBox.Text);
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

			if (!AdvanceConvert.StrToInt(ConfigAddr_textBox.Text))
			{
				MessageBox.Show(@"Ошибка в поле Configuration Addr");
				return;
			}
			else
			{
				ScopeSysType.ConfigurationAddr = AdvanceConvert.uValue;
			}


			if (!AdvanceConvert.StrToInt(OscilCmndAddr_textBox.Text))
			{
				MessageBox.Show(@"Ошибка в поле Oscil Cmnd Addr");
				return;
			}
			else
			{
				ScopeSysType.OscilCmndAddr = AdvanceConvert.uValue;
			}

			ScopeSysType.StationName = Convert.ToString(stationName_textBox.Text);
			ScopeSysType.RecordingDevice = Convert.ToString(recordingDevice_textBox.Text);
			ScopeSysType.OscilNominalFrequency = Convert.ToUInt16(convert_text(nominalFrequency_textBox.Text, ""));
			ScopeSysType.TimeCode = Convert.ToString(timeCode_textBox.Text);
			ScopeSysType.LocalCode = Convert.ToString(localCode_textBox.Text);
			ScopeSysType.TmqCode = Convert.ToString(tmqCode_textBox.Text);
			ScopeSysType.Leapsec = Convert.ToString(leapsec_textBox.Text);

			ScopeSysType.ScopeItem.Clear();

			foreach (DataGridViewRow item in ChanneldataGridView.Rows)
			{
				// ReSharper disable once InconsistentNaming
				ScopeChannelConfig Item = new ScopeChannelConfig
				{
					ChannelNames = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[0].Value),
					ChannelGroupNames = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[1].Value),
					ChannelTypeAd = Convert.ToUInt16(Array.IndexOf(_typeChannel, ChanneldataGridView.Rows[item.Index].Cells[2].Value)),
					ChannelAddrs = Convert.ToUInt16(convert_text(ChanneldataGridView.Rows[item.Index].Cells[3].Value, "0x")),
					ChannelformatNumeric = Array.IndexOf(_sizeFormat, ChanneldataGridView.Rows[item.Index].Cells[4].Value),
					ChannelFormats = Array.IndexOf(_format, ChanneldataGridView.Rows[item.Index].Cells[5].Value),
					ChannelPhase = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[6].Value),
					ChannelCcbm = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[7].Value),
					ChannelDimension = Convert.ToString(ChanneldataGridView.Rows[item.Index].Cells[8].Value),
					ChannelMin = Convert.ToInt32(ChanneldataGridView.Rows[item.Index].Cells[9].Value),
					ChannelMax = Convert.ToInt32(ChanneldataGridView.Rows[item.Index].Cells[10].Value)
				};

				ScopeSysType.ScopeItem.Add(Item);
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
			int yPos = 30;
			int xPos1 = 25;
			int xPos2 = 200;
			int xPos3 = 350;
			int xPos4 = 520;

			if (_firstPage)
			{
				yPos = 150;
				e.Graphics.DrawString("Title", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 40));
				e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 60));
				e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 70));
				e.Graphics.DrawString("Comment:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 80));
				e.Graphics.DrawString(CommentRichTextBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 100));
				e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 140));
				e.Graphics.DrawString("Oscil Config", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(xPos1, 150));
				e.Graphics.DrawString("COMTRADE Config", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(xPos3, 150));
				e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 160));
				e.Graphics.DrawString("Configuration Addr:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
				e.Graphics.DrawString(ConfigAddr_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(200, yPos));
				e.Graphics.DrawString("StationName:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
				e.Graphics.DrawString(stationName_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
				e.Graphics.DrawString("Oscil Cmnd Addr:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
				e.Graphics.DrawString(OscilCmndAddr_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(200, yPos));
				e.Graphics.DrawString("RecordingDevice:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
				e.Graphics.DrawString(recordingDevice_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
				e.Graphics.DrawString("OscilAllSize (KB):", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
				e.Graphics.DrawString(OscilSizeData_TextBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos2, yPos));
				e.Graphics.DrawString("NominalFrequency:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
				e.Graphics.DrawString(nominalFrequency_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
				e.Graphics.DrawString("OscilSampleRate:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
				e.Graphics.DrawString(sampleRate_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos2, yPos));
				e.Graphics.DrawString("TimeCode:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
				e.Graphics.DrawString(timeCode_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
				e.Graphics.DrawString("MeasureParams:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
				e.Graphics.DrawString(ChanneldataGridView.Rows.Count.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos2, yPos));
				e.Graphics.DrawString("LocalCode:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos));
				e.Graphics.DrawString(localCode_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
				e.Graphics.DrawString("TmqCode:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos += 20));
				e.Graphics.DrawString(tmqCode_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
				e.Graphics.DrawString("Leapsec:", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos3, yPos += 20));
				e.Graphics.DrawString(leapsec_textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos4, yPos));
			}

			e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 10));
			e.Graphics.DrawString("№", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos + 10));
			e.Graphics.DrawString("Channel Name", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 35, yPos + 10));
			e.Graphics.DrawString("Address", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 200, yPos + 10));
			e.Graphics.DrawString("Bit", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 260, yPos + 10));
			e.Graphics.DrawString("Format", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 290, yPos + 10));
			e.Graphics.DrawString("Dimension", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 395, yPos + 10));
			e.Graphics.DrawString("Phase", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 470, yPos + 10));
			e.Graphics.DrawString("CCBM", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 520, yPos + 10));
			e.Graphics.DrawString("TypeAD", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 570, yPos + 10));
			e.Graphics.DrawString("Min", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 630, yPos + 10));
			e.Graphics.DrawString("Max", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 690, yPos + 10));
			e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 15));
			for (int i = _paramNum, j = 0; i < ChanneldataGridView.Rows.Count; j++, i++)
			{
				e.Graphics.DrawString((i + 1) + ".", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[0].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 35, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 200, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[4].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 260, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[5].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 290, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[8].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 395, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[6].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 470, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[7].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 520, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 570, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[9].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 630, yPos));
				e.Graphics.DrawString(ChanneldataGridView.Rows[i].Cells[10].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 690, yPos));
				if (_firstPage && j == 40) { _paramNum += (j + 1); e.HasMorePages = true; _firstPage = false; break; }
				if (_firstPage == false && j == 52) { _paramNum += (j + 1); e.HasMorePages = true; break; }
				if (i == ChanneldataGridView.Rows.Count - 1) { _firstPage = true; _paramNum = 0; e.HasMorePages = false; }
			}
		}

		private void ChanneldataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var index = e.RowIndex;

			if (ChanneldataGridView.Rows[index].Cells["Column_channelFormats"].Value.ToString().Contains("BLOCKED"))
			{
				ChanneldataGridView.Rows[index].Cells["Column_channelFormats"].Value = _format[0];
			}
			
			ChanneldataGridView.Rows[index].Cells["Column_channelformatNumeric"].Value = _sizeFormat[FormatConverter.
				GetIndexSizeFormat(ChanneldataGridView.Rows[index].Cells["Column_channelFormats"].Value.ToString())];
		}

		private void stationName_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(stationName_label, "Название станции");
		}

		private void recordingDevice_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(recordingDevice_label, "Обозначение станции");
		}

		private void nominalFrequency_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(nominalFrequency_label, "Частота сети");
		}

		private void timeCode_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(timeCode_label, @"Временной код (смещение относительно UTC)");
		}

		private void localCode_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(localCode_label, @"Разница во времени между местным часовым поясом и мировым");
		}

		private void tmqCode_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(tmqCode_label, @"Код показаывающий качество записывающего устройства");
		}

		private void leapsec_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(leapsec_label, @"Скачок");
		}

		private void OscilCmndAddr_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(OscilCmndAddr_label, @"Адрес вспомогательных параметров");
		}

		private void OscilSizeData_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(OscilSizeData_label, @"Предполагаемый размер выделенный под осциллограммы");
		}

		private void sampleRate_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(sampleRate_label, @"Предполагаемая частота дискретизации");
		}

		private void ConfigAddr_MouseEnter(object sender, EventArgs e)
		{
			ToolTip tTip = new ToolTip();
			tTip.SetToolTip(ConfigAddr_label, @"Адрес конфигурации");
		}

		private void ChanneldataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			code_label.Text = @"Код: " + FormatConverter.GetCodeFormat(ChanneldataGridView.Rows[e.RowIndex].Cells["Column_channelFormats"].Value as string);
		}
	}
}

