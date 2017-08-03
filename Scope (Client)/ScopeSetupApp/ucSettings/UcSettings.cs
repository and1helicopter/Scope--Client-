using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ScopeSetupApp.Format;
using ScopeSetupApp.Properties;

namespace ScopeSetupApp.ucSettings
{
	public partial class UcSettings : UserControl
	{

		public UcSettings()
		{

			InitializeComponent();

			InitTable();
			InitConfig();

			OldFormatChange();
		}

		private void FormatsdataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			switch (FormatsdataGridView.Columns[e.ColumnIndex].Name)
			{
				case "smallerCol":
					ChangeSmallCol(e);
					break;
				case "bCol":
					ChangeBCol(e);
					break;
				case "aCol":
					ChangeACol(e);
					break;
				case "bitCol":
					ChangeBitCol(e);
					break;
				case "nameCol":
					ChangeNameCol(e);
					break;
			}

			FormatsdataGridView_RowEnter(sender, e);
		}

		private void ChangeSmallCol(DataGridViewCellEventArgs i)
		{
			try
			{
				var val = Convert.ToUInt32(FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value);
				FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value = val;
				FormatConverter.FormatList[i.RowIndex].Smaller = val;
			}
			catch
			{
				MessageBox.Show(@"Неверно введены данные", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value = i.RowIndex < FormatConverter.FormatList.Count ? FormatConverter.FormatList[i.RowIndex].Smaller : 0;
			}
		}

		private void ChangeBCol(DataGridViewCellEventArgs i)
		{
			var val = FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value.ToString();
			if (ValidateConvertToDouble(val))
			{
				FormatConverter.FormatList[i.RowIndex].BChange(val);
			}
			else
			{
				MessageBox.Show(@"Неверно введены данные", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value = FormatConverter.FormatList[i.RowIndex].BStr;
			}
		}

		private void ChangeACol(DataGridViewCellEventArgs i)
		{
			var val = FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value.ToString();
			if (ValidateConvertToDouble(val))
			{
				FormatConverter.FormatList[i.RowIndex].AChange(val);
			}
			else
			{
				MessageBox.Show(@"Неверно введены данные", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value = FormatConverter.FormatList[i.RowIndex].AStr;
			}
		}

		private void ChangeBitCol(DataGridViewCellEventArgs i)
		{
			var val = FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value.ToString();
			FormatConverter.FormatList[i.RowIndex].BitDepth = new FormatConverter.BitDepth(val);
		}

		private void ChangeNameCol(DataGridViewCellEventArgs i)
		{
			FormatConverter.FormatList[i.RowIndex].Name = FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value.ToString();
		}

		private bool ValidateConvertToDouble(string val)
		{
			// ReSharper disable once NotAccessedVariable
			double value;
			try
			{
				if (val.Split('/').Length == 2)
				{
					var valStr = val.Split('/');
					// ReSharper disable once RedundantAssignment
					value = Convert.ToDouble(valStr[0].Replace('.', ',')) / Convert.ToDouble(valStr[1].Replace('.', ','));
				}
				else
				{
					// ReSharper disable once RedundantAssignment
					value = Convert.ToDouble(val.Replace('.', ','));
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		private void InitTable()
		{
			CreateTable();
			UpdateTable();
		}

		private void CreateTable()
		{
			foreach (var f in FormatConverter.FormatList)
			{
				try
				{
					FormatsdataGridView.Rows.Add(
						f.Name,
						f.BitDepth.Name,
						f.AStr,
						f.BStr,
						f.Smaller);
				}
				catch
				{
					MessageBox.Show(@"Ошибка загрузки данных", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
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

		private void oldFormat_checkBox_Click(object sender, EventArgs e)
		{
			FormatConverter.OldFormat = !FormatConverter.OldFormat;
			FormatConverter.UpdateVisualFormat();
			Program.MainFormWin.FormatStrLabel();

			OldFormatChange();
		}

		private void OldFormatChange()
		{
			if (FormatConverter.OldFormat)
			{
				old_toolStripButton.Image = Resources.Checked;
				old_toolStripButton.Text = @"Старый формат";
				old_toolStripButton.ToolTipText = @"Старый формат";
			}
			else
			{
				old_toolStripButton.Image = Resources.Unchecked;
				old_toolStripButton.Text = @"Новый формат";
				old_toolStripButton.ToolTipText = @"Новый формат";
			}
		}

		private void addFormatButton_Click(object sender, EventArgs e)
		{
			int index = FormatsdataGridView.Rows.Count;
			FormatsdataGridView.Rows.Add($"Format{index}", "uint16", "1", "0", "0");
			FormatConverter.FormatList.Add(new FormatConverter.Format($"Format{index}", "uint16", "1", "0", 0));

			UpdateTable();
		}

		private void UpdateTable()
		{
			foreach (DataGridViewRow r in FormatsdataGridView.Rows)
			{
				FormatsdataGridView.Rows[r.Index].HeaderCell.Value = r.Index.ToString();
				r.DefaultCellStyle.BackColor = r.Cells["nameCol"].Value.ToString() == "BLOCKED" ? Color.LightSalmon : Color.White;
			}

			FormatConverter.UpdateVisualFormat();

			SetDoubleBuffered(FormatsdataGridView, true);
			SetDoubleBuffered(FormatsdataGridView, true);
			SetDoubleBuffered(FormatsdataGridView, true);
			SetDoubleBuffered(FormatsdataGridView, true);
			SetDoubleBuffered(FormatsdataGridView, true);
		}

		private static string _nameFileFormat = "Formats.xml";

		private void openFormatButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog
			{
				DefaultExt = @".xml",           // Default file extension
				Filter = @"XML|*.xml"           // Filter files by extension 
			};

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				_nameFileFormat = ofd.FileName;
				FormatConverter.ReadFormats(ofd.FileName);
			}
		}

		private void saveFormatButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog
			{
				DefaultExt = @".xml",           // Default file extension
				Filter = @"XML|*.xml",
				FileName = _nameFileFormat
			};

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				FormatConverter.SaveFormats(sfd.FileName);
			}
		}

		private void removeFormatButton_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow item in FormatsdataGridView.SelectedRows)
			{
				FormatConverter.FormatList.Remove((from x in FormatConverter.FormatList
					where x.Name == item.Cells["nameCol"].Value as string
					select x).First());

				FormatsdataGridView.Rows.RemoveAt(item.Index);
			}

			UpdateTable();
		}

		private void FormatsdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			var format = FormatConverter.GetEquationFormat(e.RowIndex);

			if (FormatsdataGridView.Rows[e.RowIndex].Cells["nameCol"].Value as string == "BLOCKED")
			{
				format = "BLOCKED";
			}
			var code = (FormatConverter.FormatList[e.RowIndex].BitDepth.Bit << 8) + e.RowIndex;

			info_format_label.Text = @"Код: " + code + @" Формат: " + format;
		}

		private string convert_text(object obj, string del)
		{
			int i = 0;
			string str = Convert.ToString(obj);
			try
			{
				if (str == "") str = "0";
				if (del == "0x") i = Convert.ToInt32(str, 16);
				if (del == "") i = Convert.ToInt32(str);
			}
			catch
			{
				MessageBox.Show(@"Неправильно введены данные", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			str = Convert.ToString(i);
			//if (del != "") str = str.Replace(del, "");
			return str;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ScopeSysType.ConfigurationAddr = Convert.ToUInt16(convert_text(ConfigAddr_textBox.Text, "0x"));
			ScopeSysType.OscilCmndAddr = Convert.ToUInt16(convert_text(OscilCmndAddr_textBox.Text, "0x"));

			Program.MainFormWin.ConfigCheack();
		}

		private void UpdateLabelScopeConfig (bool status)
		{
			label2.Text = status ? ScopeConfig.ScopeCount.ToString() : @"UNKNOWN";
			label3.Text = status ? ScopeConfig.ChannelCount.ToString() : @"UNKNOWN";
			label4.Text = status ? GetAddrFormateName() : @"UNKNOWN";
			label5.Text = status ? ScopeConfig.HistoryCount.ToString() : @"UNKNOWN";
		}

		private delegate void UpdateLabelConfig(bool status);


		private string GetAddrFormateName()
		{
			string str = "";
			for (int i = 0; i < ScopeConfig.ChannelCount; i++)
			{
				str += "0x" + ScopeConfig.OscilAddr[i].ToString("X4") + "  " + ScopeConfig.OscilFormat[i] + "  " + ScopeConfig.ChannelName[i] + "\n";
			}
			return str;
		}


		private void InitConfig()
		{
			ConfigAddr_textBox.Text = @"0x" + ScopeSysType.ConfigurationAddr.ToString("X4");
			OscilCmndAddr_textBox.Text = @"0x" + ScopeSysType.OscilCmndAddr.ToString("X4");

			//Если конфиграция успешно получена, то выводим конфигурацию
			UpdateLabelScopeConfig(ScopeConfig.StatusOscil == 0x0001);
			//Invoke(new UpdateLabelConfig(UpdateLabelScopeConfig), ScopeConfig.StatusOscil == 0x0001);
		}

		public void UpdateConfig()
		{
			ConfigAddr_textBox.Text = @"0x" + ScopeSysType.ConfigurationAddr.ToString("X4");
			OscilCmndAddr_textBox.Text = @"0x" + ScopeSysType.OscilCmndAddr.ToString("X4");

			//Если конфиграция успешно получена, то выводим конфигурацию
			Invoke(new UpdateLabelConfig(UpdateLabelScopeConfig), ScopeConfig.StatusOscil == 0x0001);
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			ScopeConfig.CodeDevice = Convert.ToByte(1 << Convert.ToInt32(numericUpDown1.Value));
		}
	}
}
