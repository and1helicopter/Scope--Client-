using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ScopeSetupApp.Format
{
	public partial class Settings : UserControl
	{
		public Settings()
		{
			InitializeComponent();

			InitTable();

			FormatsdataGridView.CellValidated += FormatsdataGridView_CellValidated;
		}

		private void FormatsdataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			if (FormatsdataGridView.Columns[e.ColumnIndex].Name == "smallerCol")
			{
				ChangeSmallCol(e);
			}
			else if (FormatsdataGridView.Columns[e.ColumnIndex].Name == "bCol")
			{
				ChangeBCol(e);
			}
			else if (FormatsdataGridView.Columns[e.ColumnIndex].Name == "aCol")
			{
				ChangeACol(e);
			}
			else if (FormatsdataGridView.Columns[e.ColumnIndex].Name == "bitCol")
			{
				ChangeBitCol(e);
			}
			else if (FormatsdataGridView.Columns[e.ColumnIndex].Name == "nameCol")
			{
				ChangeNameCol(e);
			}
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
				MessageBox.Show(@"Неверно введены данные", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
				MessageBox.Show(@"Неверно введены данные", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
				MessageBox.Show(@"Неверно введены данные", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
					MessageBox.Show(@"Ошибка загрузки данных", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			MainForm.FormatStatusLabel.Invoke();
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

				var sdfvds = (from x in FormatConverter.FormatList
					where x.Name == r.Cells["nameCol"].Value as string
					select x).First();

				int index = FormatConverter.FormatList.IndexOf(sdfvds);

				FormatsdataGridView.Rows[r.Index].HeaderCell.Value = (index + 1).ToString();
			}

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
	}
}
