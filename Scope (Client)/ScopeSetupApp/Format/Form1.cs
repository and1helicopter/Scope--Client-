using System;
using System.Reflection;
using System.Windows.Forms;

namespace ScopeSetupApp.Format
{
	public partial class Form1 : Form
	{
		public Form1()
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
			else if (FormatsdataGridView.Columns[e.ColumnIndex].Name == "zCol")
			{
				ChangeZCol(e);
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

		private void button1_Click(object sender, EventArgs e)
		{
			//FormatConverter.ReadFormats();
			//	smallerCol.

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

		private void ChangeZCol(DataGridViewCellEventArgs i)
		{
			var val = FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value.ToString();
			if (ValidateConvertToDouble(val))
			{
				FormatConverter.FormatList[i.RowIndex].ZChange(val);
			}
			else
			{
				MessageBox.Show(@"Неверно введены данные", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				FormatsdataGridView.Rows[i.RowIndex].Cells[i.ColumnIndex].Value = FormatConverter.FormatList[i.RowIndex].ZStr;
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
					value = Convert.ToDouble(valStr[0].Replace('.', ','))/Convert.ToDouble(valStr[1].Replace('.', ','));
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

		//private void addLineButton_Click(object sender, EventArgs e)
		//{
		//	ChanneldataGridView.Rows.Add("Параметр", "", _typeChannel[0], "0x" + (ChanneldataGridView.Rows.Count - 1).ToString("X4"), _sizeFormat[0], _format[0], "", "", "NONE", -1, 1);
		//	UpdateTable();
		//}


		//private void deleteButton_Click(object sender, EventArgs e)
		//{
		//	foreach (DataGridViewRow item in ChanneldataGridView.SelectedRows)
		//	{
		//		ChanneldataGridView.Rows.RemoveAt(item.Index);
		//	}
		//	UpdateTable();
		//}

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
						f.ZStr,
						f.Smaller
					);
				}
				catch
				{
					MessageBox.Show(@"Ошибка загрузки данных", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
		}

		private void UpdateTable()
		{
			foreach (DataGridViewRow r in FormatsdataGridView.Rows)
			{
				FormatsdataGridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
			}

			SetDoubleBuffered(FormatsdataGridView, true);
		}

		void SetDoubleBuffered(Control c, bool value)
		{
			PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
			if (pi != null)
			{
				pi.SetValue(c, value, null);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			FormatConverter.SaveFormats();
		}
	}
}
