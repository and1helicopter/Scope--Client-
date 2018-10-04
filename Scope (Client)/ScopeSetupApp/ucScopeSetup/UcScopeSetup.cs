using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Linq;
using System.Windows;
using BrightIdeasSoftware;
using ScopeSetupApp.Format;
using UniSerialPort;
using Color = System.Drawing.Color;
using FontStyle = System.Drawing.FontStyle;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;
using SystemColors = System.Drawing.SystemColors;
using UserControl = System.Windows.Forms.UserControl;

namespace ScopeSetupApp.ucScopeSetup
{
	public sealed partial class UcScopeSetup : UserControl
	{
		private ushort _nowHystory;             //Предыстория 
		private ushort _nowScopeCount = 1;          //Количество осциллограмм
		private ushort _nowMaxChannelCount;         //Количество каналов
		private ushort _nowOscFreq = 1;             //Делитель     
		private readonly uint _oscilAllSize = ScopeSysType.OscilAllSize;             //
		private readonly bool _admin;

		private static readonly object[] Format = FormatConverter.ActualFormat;

		private static readonly object[] SizeFormat =
		{
			"16",
			"32",
			"64",
		};

		#region

		private static readonly List<ItemGroup> ItemGroups = new List<ItemGroup>();

		private void InitTable(bool _admin)
		{
			ItemGroups.Clear();

			OLVColumn nameChanel = new OLVColumn
			{
				AspectName = "Name",
				Groupable = false,
				HeaderCheckBoxUpdatesRowCheckBoxes = false,
				IsEditable = false,
				MinimumWidth = 250,
				Text = @"Название канала",
				Width = 350
			};

			if (_admin)
			{
				OLVColumn addrChanel = new OLVColumn
				{
					AspectName = "Addr",
					Groupable = false,
					HeaderCheckBoxUpdatesRowCheckBoxes = false,
					MinimumWidth = 200,
					Text = @"Адрес",
					Width = 300
				};

				OLVColumn formatChanel = new OLVColumn
				{
					AspectName = "Format",
					Groupable = false,
					HeaderCheckBoxUpdatesRowCheckBoxes = false,
					MinimumWidth = 200,
					Text = @"Формат",
					Width = 300
				};

				treeListView.AllColumns.Add(nameChanel);
				treeListView.AllColumns.Add(addrChanel);
				treeListView.AllColumns.Add(formatChanel);

				treeListView.Columns.AddRange(new ColumnHeader[]
				{
					nameChanel,
					addrChanel,
					formatChanel,
				});
			}
			else
			{
				treeListView.CanExpandGetter = model => (model as Item)?.Group != null;

				treeListView.Columns.AddRange(new ColumnHeader[]
				{
					nameChanel,
				});
			}

			CommentRichTextBox.Text = ScopeSysType.OscilComment;

			foreach (var item in ScopeSysType.ScopeItem)
			{
				var item1 = new Item(item);
			}

			treeListView.CanExpandGetter = model =>
			{
				if (model.GetType() == typeof(ItemGroup))
					return ((ItemGroup)model).Children.Count > 0;
				else
					return false;
			};
;
			treeListView.ChildrenGetter = x => ((ItemGroup) x).Children;
			
			treeListView.SetObjects(ItemGroups);
			treeListView.UseCellFormatEvents = true;
			treeListView.FormatRow += TreeListViewOnFormatRow;
			treeListView.ExpandAll();
			AutosizeColumns();
		}

		private class ItemGroup
		{
			public string Name { get; set; }
			public string NameShort { get; set; }
			public string Addr { get; set; } = "";
			public string Format { get; set; } = "";
			public int countSelect { get; set; } = 0;
			public CheckState State { get; set; }

			public List<Item> Children { get; set; } = new List<Item>();
		}

		private class Item
		{
			public string Name { get; set; }
			public string Addr { get; set; }
			public string Format { get; set; }
			public string Group { get; set; }
			public ScopeChannelConfig Value { get; set; }

			public Item(ScopeChannelConfig item)
			{
				Value = item;
				Name = item.ChannelNames;
				Addr = "0x" + item.ChannelAddrs.ToString("x4");
				Format = Convert.ToString(SizeFormat[item.ChannelformatNumeric]) + "b   " + Convert.ToString(FormatConverter.ActualFormat[item.ChannelFormats]);

				Group = item.ChannelGroupNames != "" ? item.ChannelGroupNames : @"Несгруппированные параметры";

				if (ItemGroups.Any(x => x.Name == Group))
				{
					ItemGroups.First(x=>x.Name == Group).Children.Add(this);
				}
				else
				{
					ItemGroups.Add(new ItemGroup{Name = Group, NameShort = Group, Children = new List<Item>{this}});
				}
			}
		}

		private void TreeListViewOnFormatRow(object o, FormatRowEventArgs formatRowEventArgs)
		{
			if (treeListView.CheckedObjects.OfType<Item>().Count() <= 32)
			{
				formatRowEventArgs.Item.BackColor = formatRowEventArgs.Item.Checked ? Color.LightSteelBlue : SystemColors.ButtonHighlight;
			}
			else
			{
				treeListView.UncheckObject(formatRowEventArgs.Item.RowObject);
			}
		}

		private void treeListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			var xxx = e.Item.Index;
			var obj = treeListView.GetModelObject(xxx);

			if (obj.GetType() == typeof(ItemGroup))
			{
				var itemGroup = (ItemGroup)obj;

				var countAll = treeListView.CheckedObjects.OfType<Item>().Count();
				var countCurrent = itemGroup.Children.Count(x => treeListView.IsChecked(x));
				var countNew = countAll - countCurrent + itemGroup.Children.Count;

				if (!treeListView.IsExpanded(itemGroup))
				{
					if (countNew > 32)
					{
						treeListView.Expand(itemGroup);
					}
				}
				else
				{
					if (countAll == 32)
					{
						treeListView.UncheckObject(itemGroup);
					}
				}
			}
			
			if (treeListView.CheckedObjects.OfType<Item>().Count() > 32)
			{
				e.Item.Checked = false;
			}

			foreach (var itemGroup in ItemGroups)
			{
				itemGroup.countSelect = itemGroup.Children.Count(x => treeListView.IsChecked(x));
				itemGroup.Name = itemGroup.NameShort + $" (Выбрано {itemGroup.countSelect} из {itemGroup.Children.Count})";
			}

			_nowMaxChannelCount = (ushort)treeListView.CheckedObjects.OfType<Item>().Count();
			radioButton.Text = _nowMaxChannelCount.ToString();

			treeListView.RefreshObject(e.Item);
			//treeListView.UpdateObject(e.Item);
		}

		#endregion

		public UcScopeSetup(string[] agrs)
		{
			DoubleBuffered = true;

			if (agrs.Length > 0)
			{
				_admin = agrs[0] == "a" || agrs[0] == "A";
			}

			InitializeComponent();
		}

		private void UcScopeSetup_Load(object sender, EventArgs e)
		{
			InitTable(_admin);
			reloadButton_Click(null, null);
			DelayOscil();
			labelAllSize.Text = $@"Размер доступной памяти: {ScopeConfig.OscilAllSize / 1024} Кб";
			labelFreq.Text = $@"Частота выборки: {(ScopeConfig.SampleRate / _nowOscFreq):D} Гц";
			labelPrecents.Text = $@"{sizeOcsil_trackBar.Value} %";
		}

		//****************************************************************************//
		//****************************************************************************//
		//****************************************************************************//
		//Обработка полей формы
		#region  
		//Количество осциллограмм
		private void chCount_TextChanged(object sender, EventArgs e)
		{
			_nowScopeCount = (ushort)chCountNumericUpDown.Value;

			DelayOscil();
		}

		//Предыстория 
		private void hystory_TextChanged(object sender, EventArgs e)
		{
			_nowHystory = (ushort)hystoryNumericUpDown.Value;
		}

		//Делитель
		private void oscFreq_TextChanged(object sender, EventArgs e)
		{
			_nowOscFreq = (ushort)oscFreqNumericUpDown.Value;

			DelayOscil();

			labelFreq.Text = $@"Частота выборки: {ScopeConfig.SampleRate / _nowOscFreq:D} Гц";
		}

		//Длительность осциллограммы:
		private void DelayOscil()
		{
			if (_nowScopeCount != 0)
			{
				if (MainForm.MainForm.SerialPort.IsOpen && ScopeConfig.ConnectMcu)
				{
					double sampleCount = (double)OscilSize(_oscilAllSize, false) / OscilSize(_oscilAllSize, true);
					double freq = (double)ScopeConfig.SampleRate / _nowOscFreq;
					double timeSec = ((uint)sampleCount) / freq;
					if (Math.Abs(timeSec) < 0.001)
					{
						DelayOsc.Text = @"Длительность: " + @"-----";
					}
					else
					{
						DelayOsc.Text = @"Длительность: " + timeSec.ToString("0.000") + @" сек";
					}
					DelayOsc.Visible = true;
				}
				else
				{
					double sampleCount = (double)OscilSize(_oscilAllSize, false) / OscilSize(_oscilAllSize, true);
					double freq = (double)ScopeSysType.OscilSampleRate / _nowOscFreq;
					double timeSec = ((uint)sampleCount) / freq;
					if (Math.Abs(timeSec) < 0.001)
					{
						DelayOsc.Text = @"Длительность: " + @"-----";
					}
					else
					{
						DelayOsc.Text = @"Длительность: " + timeSec.ToString("0.000") + @" сек";
					}
					DelayOsc.Visible = true;
				}
			}
		}

		private void trackBar1_ValueChanged(object sender, EventArgs e)
		{
			DelayOscil();
			labelPrecents.Text = $@"{sizeOcsil_trackBar.Value} %";
		}
		#endregion

		//**************ИЗМЕНЕНИЕ КОНФИГУРАЦИИ ОСЦИЛЛОГРАФА *********************************************//
		//***********************************************************************************************//
		//***********************************************************************************************//
		private ushort[] _newOscillConfig = new ushort[40];
		private ushort[] _oscillConfig = new ushort[1280];
		private readonly List<ScopeChannelConfig> _channelSeries = new List<ScopeChannelConfig>();        //Где какой элемент лежит


		private int _writeConfigStep;
		private ushort _writeStep;

		//Конфигурирование осциллограммы 
		private List<ushort> ChannelFormats()
		{
			_channelSeries.Clear();

			List<ushort> l = new List<ushort>();

			foreach (Item item in treeListView.CheckedObjects.OfType<Item>().ToList())
			{
				if (treeListView.IsChecked(item) && item.Value.ChannelformatNumeric == 2)
				{
					if (item.Value.ChannelAddrs % 2 != 0)
					{
						_nowMaxChannelCount = 0;
						Program.MainFormWin.MessagesBox(
							// ReSharper disable once LocalizableElement
							@"Ошибка при формирование осциллограмы" + "\nCODE 0x1007",
							@"Error", MessageBoxButton.OK, MessageBoxImage.Error);
						l.Clear();
						return l;
					}
					
					_channelSeries.Add(item.Value);
					l.Add(Convert.ToUInt16((Convert.ToInt32(item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats));
				}
			}

			foreach (Item item in treeListView.CheckedObjects.OfType<Item>().ToList())
			{
				if (treeListView.IsChecked(item) && item.Value.ChannelformatNumeric == 1)
				{
					if (item.Value.ChannelAddrs % 2 != 0)
					{
						_nowMaxChannelCount = 0;
						Program.MainFormWin.MessagesBox(
							// ReSharper disable once LocalizableElement
							@"Ошибка при формирование осциллограмы" + "\nCODE 0x1007",
							@"Error", MessageBoxButton.OK, MessageBoxImage.Error);
						l.Clear();
						return l;
					}

					_channelSeries.Add(item.Value);
					l.Add(Convert.ToUInt16((Convert.ToInt32(item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats));
				}
			}

			foreach (Item item in treeListView.CheckedObjects.OfType<Item>().ToList())
			{
				if (treeListView.IsChecked(item) && item.Value.ChannelformatNumeric == 0)
				{
					_channelSeries.Add(item.Value);
					l.Add(Convert.ToUInt16((Convert.ToInt32(item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats));
				}
			}

			if (l.Count != _nowMaxChannelCount)
			{
				_nowMaxChannelCount = 0;
				Program.MainFormWin.MessagesBox(
					// ReSharper disable once LocalizableElement
					@"Ошибка при формирование осциллограмы" + "\nCODE 0x1006",
					@"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			if (l.Count > _nowMaxChannelCount) { l.Clear(); }
			return l;
		}

		// Channel Addrs
		private List<ushort> ChannelAddrs()
		{
			List<ushort> l = new List<ushort>();

			foreach (var item in _channelSeries)
			{
				l.Add(item.ChannelAddrs);
			}

			if (l.Count > _nowMaxChannelCount) { l.Clear(); }
			return l;
		}

		// OscilSize
		private uint OscilSize(uint allSize, bool wr)
		{
			uint count64 = 0, count32 = 0, count16 = 0;

			foreach (var checkedObject in treeListView.CheckedObjects)
			{
				if (checkedObject.GetType() == typeof(Item))
				{
					if (((Item)checkedObject).Value.ChannelformatNumeric == 2) { count64++; }
					if (((Item)checkedObject).Value.ChannelformatNumeric == 1) { count32++; }
					if (((Item)checkedObject).Value.ChannelformatNumeric == 0) { count16++; }
				}
			}

			uint sampleSize = count64 * 8 + count32 * 4 + count16 * 2;
			if ((count64 != 0 || count32 != 0) && count16 % 2 != 0) { sampleSize += 2; } // Выравнивание на 4 байта
			if (sampleSize == 0)
			{
				return 0;
			}

			uint oscS = ScopeConfig.ConnectMcu ? Convert.ToUInt32((double)ScopeConfig.OscilAllSize / Convert.ToUInt32(_nowScopeCount) * ((double)sizeOcsil_trackBar.Value / 100)) : Convert.ToUInt32((double)allSize * 1024 / Convert.ToUInt32(_nowScopeCount) * ((double)sizeOcsil_trackBar.Value / 100));

			while (oscS % 64 != 0 || oscS % sampleSize != 0)   // 
			{
				oscS--;
			}

			switch (wr)
			{
				case false:
					return oscS;
				case true:
					return sampleSize;
			}
			return 0;
		}

		//OscilEnable
		private ushort OscilEnable()
		{
			ushort status = 0;
			if (!enaScopeCheckBox.Checked) status = 0;                               //Осциллограффирование отключено 
			if (enaScopeCheckBox.Checked && !checkBox1.Checked) status = 1;          //Осциллограффирование включено но без сокранения на карту пмяти
			if (enaScopeCheckBox.Checked && checkBox1.Checked) status = 2;           //Осциллограффирование включено с сохранением на карту пмяти
			if (enaScopeCheckBox.Checked && !checkBox1.Checked && checkBox3.Checked) status = 3;          //Осциллограффирование включено но без сокранения на карту пмяти, перезаписью старых осциллограмм
			if (enaScopeCheckBox.Checked && checkBox1.Checked && checkBox3.Checked) status = 4;           //Осциллограффирование включено с сохранением на карту пмяти, перезаписью старых осциллограмм
			return status;
		}

		// Channel Names
		private List<string> ChNames()
		{
			List<string> chName = new List<string>();

			foreach (var item in _channelSeries)
			{
				var str = new char[32];
				var color = item.ChannelColor.ToLowerInvariant() == "Auto".ToLowerInvariant() ? "ffffff" : item.ChannelColor;

				for (int index = 0; index < item.ChannelNames.Length; index++)
					str[index] = item.ChannelNames[index];

				str[29] = (char)byte.Parse(color.Substring(0, 2), NumberStyles.AllowHexSpecifier);
				str[30] = (char)byte.Parse(color.Substring(2, 2), NumberStyles.AllowHexSpecifier);
				str[31] = (char)byte.Parse(color.Substring(4, 2), NumberStyles.AllowHexSpecifier);

				var valName = new string(str);
				chName.Add(valName);
			}

			if (chName.Count > _nowMaxChannelCount) { chName.Clear(); }
			return chName;
		}
		//////////////////////////////////////////
		//For Comtrade 
		private List<string> ChPhase()
		{
			List<string> l = new List<string>();

			foreach (var item in _channelSeries)
			{
				l.Add(item.ChannelPhase);
			}

			if (l.Count > _nowMaxChannelCount) { l.Clear(); }
			return l;
		}

		private List<string> ChCcbm()
		{
			List<string> l = new List<string>();

			foreach (var item in _channelSeries)
			{
				l.Add(item.ChannelCcbm);
			}

			if (l.Count > _nowMaxChannelCount) { l.Clear(); }

			return l;
		}

		private List<string> ChDemension()
		{
			List<string> l = new List<string>();
			foreach (var item in _channelSeries)
			{
				l.Add(item.ChannelDimension);

			}

			if (l.Count > _nowMaxChannelCount) { l.Clear(); }

			return l;
		}

		private List<ushort> ChTypeAd()
		{
			List<ushort> l = new List<ushort>();
			foreach (var item in _channelSeries)
			{
				l.Add(item.ChannelTypeAd);
			}

			if (l.Count > _nowMaxChannelCount) { l.Clear(); }

			return l;
		}


		private void CalcNewOscillConfig(ushort writeStep)
		{
			_newOscillConfig = new ushort[40];

			_newOscillConfig[0] = 1;
			_newOscillConfig[1] = writeStep;
			for (int i = 0; i < 32; i++)
			{
				_newOscillConfig[2 + i] = _oscillConfig[i + 32 * writeStep];
			}
			_newOscillConfig[34] = 1;
		}

		//Конфигурирование параметрв осциллограммы 
		private void CalcOscillConfig()
		{
			_oscillConfig = new ushort[1280];

			List<ushort> chFormats = ChannelFormats();          //Идентификатор формата данных в каналах

			for (int i = 0; i < 32; i++)
			{
				if (i < chFormats.Count) { _oscillConfig[i + StructAddr.OscilTypeData] = chFormats[i]; }
				else { _oscillConfig[i + StructAddr.OscilTypeData] = 0; }
			}

			List<ushort> chAddrs = ChannelAddrs();          //Адреса

			for (int i = 0; i < 32; i++)
			{
				if (i < chAddrs.Count) { _oscillConfig[i + StructAddr.OscilAddr] = chAddrs[i]; }
				else { _oscillConfig[i + StructAddr.OscilAddr] = 0; }
			}

			_oscillConfig[StructAddr.OscilSize] = Convert.ToUInt16((OscilSize(ScopeSysType.OscilAllSize, false) << 16) >> 16);  //размер под осциллограмму 
			_oscillConfig[StructAddr.OscilSize + 1] = Convert.ToUInt16(OscilSize(ScopeSysType.OscilAllSize, false) >> 16);

			_oscillConfig[StructAddr.OscilQuantity] = _nowScopeCount;            //Колличество формируемых осциллограмм
			_oscillConfig[StructAddr.OscilChNum] = _nowMaxChannelCount;       //Колличество каналов
			_oscillConfig[StructAddr.OscilHistoryPercent] = _nowHystory;               //Предыстория
			_oscillConfig[StructAddr.OscilFreqDiv] = _nowOscFreq;               //Как часто нужно записывать данные 
			_oscillConfig[StructAddr.OscilEnable] = OscilEnable();            //Включен или выключен осциллограф и нужно ли выполнять запись в память 

			//Дополнительные параметры 
			//Запись названия канала
			List<string> chName = ChNames();                //Название каналов в Cp1251

			for (int i = 0; i < chName.Count; i++)
			{
				string chNameString = chName[i];
				char[] chNameChar = chNameString.ToCharArray();
				byte[] tempChNameStr = new Byte[32];
				byte[] chNameStr = Encoding.Default.GetBytes(chNameString);

				chNameStr[29] = BitConverter.GetBytes(chNameChar[29])[0];
				chNameStr[30] = BitConverter.GetBytes(chNameChar[30])[0];
				chNameStr[31] = BitConverter.GetBytes(chNameChar[31])[0];

				for (int j = 0; j < 32; j++)
				{
					if (j < chNameString.Length) tempChNameStr[j] = chNameStr[j];
					else tempChNameStr[j] = 32;
				}
				for (int j = 1; j < 32; j += 2)
				{
					_oscillConfig[StructAddr.OscilChNumName + 16 * i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChNameStr[j]) << 8);
					_oscillConfig[StructAddr.OscilChNumName + 16 * i + (j / 2)] += Convert.ToUInt16(tempChNameStr[j - 1]);
				}
			}

			//for Comtrade
			#region
			List<string> chPhases = ChPhase();                //в Cp1251

			for (int i = 0; i < chPhases.Count; i++)
			{
				string chPhaseString = chPhases[i];
				byte[] tempChPhaseStr = new Byte[2];
				byte[] chPhaseStr = Encoding.Default.GetBytes(chPhaseString);

				for (int j = 0; j < 2; j++)
				{
					if (j < chPhaseString.Length) tempChPhaseStr[j] = chPhaseStr[j];
					else tempChPhaseStr[j] = 32;
				}
				for (int j = 1; j < 2; j += 2)
				{
					_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Phase + i + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChPhaseStr[j]) << 8);
					_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Phase + i + (j / 2)] += Convert.ToUInt16(tempChPhaseStr[j - 1]);

				}
			}

			List<string> chCcbMs = ChCcbm();                // в Cp1251

			for (int i = 0; i < chCcbMs.Count; i++)
			{
				string chCcbmString = chCcbMs[i];
				byte[] tempChCcbmStr = new Byte[16];
				byte[] chCcbmStr = Encoding.Default.GetBytes(chCcbmString);

				for (int j = 0; j < 16; j++)
				{
					if (j < chCcbmString.Length) tempChCcbmStr[j] = chCcbmStr[j];
					else tempChCcbmStr[j] = 32;
				}
				for (int j = 1; j < 16; j += 2)
				{
					_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Ccbm + i * 8 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChCcbmStr[j]) << 8);
					_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Ccbm + i * 8 + (j / 2)] += Convert.ToUInt16(tempChCcbmStr[j - 1]);

				}
			}
			List<string> chDemensions = ChDemension();                //в Cp1251

			for (int i = 0; i < chDemensions.Count; i++)
			{
				string chDemensionString = chDemensions[i];
				byte[] tempChDemensionStr = new Byte[8];
				byte[] chDemensionStr = Encoding.Default.GetBytes(chDemensionString);

				for (int j = 0; j < 8; j++)
				{
					if (j < chDemensionString.Length) tempChDemensionStr[j] = chDemensionStr[j];
					else tempChDemensionStr[j] = 32;
				}
				for (int j = 1; j < 8; j += 2)
				{
					_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Demension + i * 4 + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempChDemensionStr[j]) << 8);
					_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Demension + i * 4 + (j / 2)] += Convert.ToUInt16(tempChDemensionStr[j - 1]);
				}
			}

			List<ushort> chaTypeAd = ChTypeAd();          //

			for (int i = 0; i < chaTypeAd.Count; i++)
			{
				if (i < chaTypeAd.Count) { _oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Type + i] = chaTypeAd[i]; }
				else { _oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Type + i] = 0; }
			}

			String stationName = ScopeSysType.StationName;                //

			byte[] tempStationNameStr = new Byte[32];
			byte[] stationNameStr = Encoding.Default.GetBytes(stationName);

			for (int j = 0; j < 32; j++)
			{
				if (j < stationName.Length) tempStationNameStr[j] = stationNameStr[j];
				else tempStationNameStr[j] = 32;
			}
			for (int j = 1; j < 32; j += 2)
			{
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.StationName + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempStationNameStr[j]) << 8);
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.StationName + (j / 2)] += Convert.ToUInt16(tempStationNameStr[j - 1]);
			}

			String recordingId = ScopeSysType.RecordingDevice;            //

			byte[] tempRecordingIdStr = new Byte[16];
			byte[] recordingIdStr = Encoding.Default.GetBytes(recordingId);

			for (int j = 0; j < 16; j++)
			{
				if (j < recordingId.Length) tempRecordingIdStr[j] = recordingIdStr[j];
				else tempRecordingIdStr[j] = 32;
			}
			for (int j = 1; j < 16; j += 2)
			{
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.RecordingId + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempRecordingIdStr[j]) << 8);
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.RecordingId + (j / 2)] += Convert.ToUInt16(tempRecordingIdStr[j - 1]);
			}

			String timeCode = ScopeSysType.TimeCode;     //

			byte[] tempTimeCodeStr = new Byte[8];
			byte[] timeCodeStr = Encoding.Default.GetBytes(timeCode);

			for (int j = 0; j < 8; j++)
			{
				if (j < timeCode.Length) tempTimeCodeStr[j] = timeCodeStr[j];
				else tempTimeCodeStr[j] = 32;
			}
			for (int j = 1; j < 8; j += 2)
			{
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.TimeCode + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempTimeCodeStr[j]) << 8);
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.TimeCode + (j / 2)] += Convert.ToUInt16(tempTimeCodeStr[j - 1]);
			}

			String localCode = ScopeSysType.LocalCode;     //

			byte[] tempLocalCodeStr = new Byte[8];
			byte[] localCodeStr = Encoding.Default.GetBytes(localCode);

			for (int j = 0; j < 8; j++)
			{
				if (j < localCode.Length) tempLocalCodeStr[j] = localCodeStr[j];
				else tempLocalCodeStr[j] = 32;
			}
			for (int j = 1; j < 8; j += 2)
			{
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.LocalCode + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(tempLocalCodeStr[j]) << 8);
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.LocalCode + (j / 2)] += Convert.ToUInt16(tempLocalCodeStr[j - 1]);
			}

			String tmqCode = ScopeSysType.TmqCode;     //

			byte[] temptmqCodeStr = new Byte[8];
			byte[] tmqCodeStr = Encoding.Default.GetBytes(tmqCode);

			for (int j = 0; j < 8; j++)
			{
				if (j < tmqCode.Length) temptmqCodeStr[j] = tmqCodeStr[j];
				else temptmqCodeStr[j] = 32;
			}

			for (int j = 1; j < 8; j += 2)
			{
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.TmqCode + (j / 2)] = Convert.ToUInt16(Convert.ToUInt32(temptmqCodeStr[j]) << 8);
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.TmqCode + (j / 2)] += Convert.ToUInt16(temptmqCodeStr[j - 1]);
			}

			String leapsec = ScopeSysType.Leapsec;     //

			byte[] templeapsecStr = new Byte[8];
			byte[] leapsecStr = Encoding.Default.GetBytes(leapsec);

			for (int j = 0; j < 8; j++)
			{
				if (j < leapsec.Length) templeapsecStr[j] = leapsecStr[j];
				else templeapsecStr[j] = 32;
			}
			for (int j = 1; j < 8; j += 2)
			{
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Leapsec + (j / 2)] += Convert.ToUInt16(Convert.ToUInt32(templeapsecStr[j]) << 8);
				_oscillConfig[StructAddr.OscilComtradeConfig + StructAddr.Leapsec + (j / 2)] += Convert.ToUInt16(templeapsecStr[j - 1]);
			}
			#endregion
		}

		private void WriteConfigToSystem()
		{
			ScopeConfig.ChangeScopeConfig = true;
			ScopeConfig.SendNewConfig = true;
			//_mainForm.
			CalcOscillConfig();
			_writeConfigStep = 0;
			CalcNewOscillConfig(_writeStep);
			WritePartConfigToSystem();
		}

		private void WritePartConfigToSystem()
		{
			ushort[] partParam = new ushort[8];
			for (int i = 0; i < 8; i++)
			{
				partParam[i] = _newOscillConfig[i + _writeConfigStep * 8];
			}

			MainForm.MainForm.SerialPort.SetDataRTU((ushort)(ScopeSysType.OscilCmndAddr + StructAddr.OscilNewConfig + _writeConfigStep * 8), EndRequest, RequestPriority.Normal, null, partParam);
		}

		private void EndRequest(bool dataOk, ushort[] paramRtu, object param)
		{
			if (MainForm.MainForm.SerialPort.portError)
			{
				if (Visible)
				{
					// ReSharper disable once LocalizableElement
					Program.MainFormWin.MessagesBox(@"Ошибка связи!" + "\nCODE 0x1103", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				_writeConfigStep++;
				if (_writeConfigStep < 5)
				{
					WritePartConfigToSystem();
				}     //Отправляю новую конфигурацию 
				else
				{
					if (_writeStep < 39)
					{
						_writeStep++;
						CalcNewOscillConfig(_writeStep);
						_writeConfigStep = 0;
						WritePartConfigToSystem();
					}
					else
					{
						_writeStep = 0;
						Program.MainFormWin.CheackConnect();
					}
				}
			}
		}

		public void StatusConfigToSystemStrLabel()
		{
			// ReSharper disable LocalizableElement
			if ((ScopeConfig.StatusOscil & 0x0000) == 0x0000)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"Конфигурация загружена и принята!" +
				                                "\n" + @"CODE: 0x0000", @"Information", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else if ((ScopeConfig.StatusOscil & 0x0001) == 0x0001)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"Конфигурация загружена не полностью!" +
								"\n" + @"CODE: 0x0001", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0002) == 0x0002)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимое количество осциллограмм!" +
								"\n" + @"CODE: 0x0002", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0003) == 0x0003)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимое количество каналов!" +
								"\n" + @"CODE: 0x0003", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0004) == 0x0004)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимое значение предыстории!" +
								"\n" + @"CODE: 0x0004", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0005) == 0x0005)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не выровняны 32b и 64b данные!" +
								"\n" + @"CODE: 0x0005", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0006) == 0x0006)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимые 8b данные!" +
								"\n" + @"CODE: 0x0006", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0007) == 0x0007)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации нарушен порядок каналов!" +
								"\n" + @"CODE: 0x0007", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0008) == 0x0008)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации размер памяти не кратен 64b!" +
								"\n" + @"CODE: 0x0008", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0009) == 0x0009)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"Недостаточно памяти для конфигурации!" +
								"\n" + @"CODE: 0x0009", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x000A) == 0x000A)
			{
				Program.MainFormWin.MessagesBox(@"Конфигурация осциллографа была передана!" + "\n" + @"Память под осциллограммы содержит не расчитано на целое число записей!" +
								"\n" + @"CODE: 0x000A", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			ScopeConfig.SendNewConfig = false;
		}

		private void writeToSystemBtn_Click(object sender, EventArgs e)
		{
			if (_nowMaxChannelCount < 1 || _nowMaxChannelCount > 32)
			{
				Program.MainFormWin.MessagesBox(@"Выбрано неверное число каналов" + "\nCODE 0x1001", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			if (!MainForm.MainForm.SerialPort.IsOpen)
			{
				Program.MainFormWin.MessagesBox(@"Соединение с системой не установлено!" + "\nCODE 0x1103", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// ReSharper disable once LocalizableElement
			if (MessageBox.Show(@"Изменить конфигурацию осциллографа?" + "\n" + @"Все текущие осциллограммы будут удалены из памяти системы!", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}

			WriteConfigToSystem();
		}

		//*************ЗАКРЫТИЕ ФОРМЫ, ОСВОБОЖДЕНИЕ РЕСУРСОВ *************************************************//
		//****************************************************************************************************//
		//****************************************************************************************************//

		//***************************Invok и****************************************************//

		//Загрузка из файла
		#region
		private void openButton2_Click(object sender, EventArgs e)
		{
			bool[] channelInList = new bool[32];
			bool channelInLists = false;
			string str = "Канала нет в списке: \n";
			for (int i = 0; i < 32; i++) { channelInList[i] = false; }

			OpenFileDialog ofd = new OpenFileDialog
			{
				DefaultExt = @".xoc",                                                                  // Default file extension
				Filter = @"XML Oscil Configuration |*.xoc|XML|*.xml|All files|*.*"                     // Filter files by extension  
			};

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				treeListView.UncheckAll();

				ScopeSysType.XmlFileNameOscil = ofd.FileName;
				try
				{
					ScopeSysType.InitScopeOscilType();

					chCountNumericUpDown.Value = ScopeSysType.OscilCount != 0 ? ScopeSysType.OscilCount : 1;
					hystoryNumericUpDown.Value = ScopeSysType.HistoryCount != 0 ? ScopeSysType.HistoryCount : 0;
					oscFreqNumericUpDown.Value = ScopeSysType.FrequncyCount != 0 ? ScopeSysType.FrequncyCount : 1;
					if (ScopeSysType.SizeValue > 0 && ScopeSysType.SizeValue <= 100) sizeOcsil_trackBar.Value = ScopeSysType.SizeValue;
					else sizeOcsil_trackBar.Value = 100;
					radioButton.Clear();

					if (ScopeSysType.OscilEnable == 0) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false; }
					if (ScopeSysType.OscilEnable == 1) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false; }
					if (ScopeSysType.OscilEnable == 2) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = false; }
					if (ScopeSysType.OscilEnable == 3) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = true; }
					if (ScopeSysType.OscilEnable == 4) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = true; }

					var listItemGroupe = treeListView.Objects.OfType<ItemGroup>().ToList();

					foreach (var itemGroup in listItemGroupe)
					{
						var count = 0;
						if (itemGroup.GetType() == typeof(ItemGroup))
						{
							foreach (var item in itemGroup.Children)
							{
								for (int i = 0; i < ScopeSysType.OscilChannelNames.Count; i++)
								{
									if (ScopeSysType.OscilChannelFormats[i] == item.Format && ScopeSysType.OscilChannelAddrs[i] == item.Addr)
									{
										channelInList[i] = true;
										treeListView.CheckObject(item);
										treeListView.RefreshObject(item);
										count++;
										break;
									}
								}
							}

							itemGroup.countSelect = count;
							itemGroup.Name = itemGroup.NameShort + $" (Выбрано {itemGroup.countSelect} из {itemGroup.Children.Count})";

							if (count == itemGroup.Children.Count)
							{
								treeListView.CheckObject(itemGroup);
							}
							else if (count > 0 && count < itemGroup.Children.Count)
							{
								treeListView.CheckIndeterminateObject(itemGroup);
							}
							else if (count == 0)
							{
								treeListView.UncheckObject(itemGroup);
							}
						}
					}

					for (int i = 0; i < ScopeSysType.OscilChannelNames.Count; i++)
					{
						if (channelInList[i] == false)
						{
							str += ScopeSysType.OscilChannelNames[i] + @" Адрес: 0x" + ScopeSysType.OscilChannelAddrs[i] + @" Формат: " + ScopeSysType.OscilChannelFormats[i] + "\n";
							channelInLists = true;
						}
					}
					if (channelInLists) MessageBox.Show(str, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

				}
				catch
				{
					Program.MainFormWin.MessagesBox(@"Ошибка загрузки данных" + "\nCODE 0x1232", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
			}
		}

		//Сохранение осциллограммы в файл
		private void saveButton2_Click(object sender, EventArgs e)
		{
			if (_nowMaxChannelCount != treeListView.CheckedObjects.OfType<Item>().Count())
			{
				Program.MainFormWin.MessagesBox(@"Количество осциллографируемых и выбранных каналов не совпадает" + "\nCODE 0x1233", @"Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			SaveFileDialog sfd = new SaveFileDialog
			{
				DefaultExt = @".xoc",                                       // Default file extension
				Filter = @"XML Oscil Configuration|*.xoc|XML|*.xml"         // Filter files by extension
			};

			int j = 0;
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				ScopeSysType.XmlFileName = sfd.FileName;

				FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
				XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode)
				{
					Formatting = Formatting.Indented
				};
				xmlOut.WriteStartDocument();

				xmlOut.WriteStartElement("Setup");
				/////////////////////////////////////////////////////////////

				xmlOut.WriteStartElement("Version", "1.0");
				xmlOut.WriteEndElement();

				xmlOut.WriteStartElement("Oscil");
				xmlOut.WriteAttributeString("Count", Convert.ToString(_nowScopeCount));
				xmlOut.WriteEndElement();

				xmlOut.WriteStartElement("Channel");
				xmlOut.WriteAttributeString("Count", Convert.ToString(_nowMaxChannelCount));
				xmlOut.WriteEndElement();

				xmlOut.WriteStartElement("Story");
				xmlOut.WriteAttributeString("Count", Convert.ToString(_nowHystory));
				xmlOut.WriteEndElement();

				xmlOut.WriteStartElement("Frequency");
				xmlOut.WriteAttributeString("Count", Convert.ToString(_nowOscFreq));
				xmlOut.WriteEndElement();

				xmlOut.WriteStartElement("Size");
				xmlOut.WriteAttributeString("Count", Convert.ToString(sizeOcsil_trackBar.Value));
				xmlOut.WriteEndElement();

				xmlOut.WriteStartElement("OscilEnable");
				xmlOut.WriteAttributeString("Count", Convert.ToString(OscilEnable()));
				xmlOut.WriteEndElement();

				foreach (var checkedObject in treeListView.CheckedObjects)
				{
					if (checkedObject.GetType() == typeof(Item))
					{
						xmlOut.WriteStartElement("MeasureParam" + (++j));

						xmlOut.WriteAttributeString("Name", ((Item)checkedObject).Name);
						xmlOut.WriteAttributeString("Addr", ((Item)checkedObject).Addr);
						xmlOut.WriteAttributeString("Format", ((Item)checkedObject).Format);

						xmlOut.WriteEndElement();
					}
				}

				/////////////////////////////////////////////////////////////
				xmlOut.WriteEndElement();
				xmlOut.WriteEndDocument();
				xmlOut.Close();
				fs.Close();
			}
		}
		#endregion

		private void reloadButton_Click(object sender, EventArgs e)
		{
			if (ScopeConfig.ChannelCount == 0  && MainForm.MainForm.SerialPort.IsOpen && !MainForm.MainForm.SerialPort.portError)
			{
				Program.MainFormWin.MessagesBox(@"В системе отсутствует конфигурация" + "\nCODE 0x1002", @"Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			string str = "Следующих каналов из системы нет в списке:\n";
			bool channelInLists = false;
			bool[] channelInList = new bool[32];
			for (int i = 0; i < 32; i++) { channelInList[i] = false; }

			treeListView.UncheckAll();

			chCountNumericUpDown.Value = ScopeConfig.ScopeCount != 0 ? ScopeConfig.ScopeCount : 1;
			hystoryNumericUpDown.Value = ScopeConfig.HistoryCount != 0 ? ScopeConfig.HistoryCount : 0;
			radioButton.Text = ScopeConfig.ChannelCount != 0 ? Convert.ToString(ScopeConfig.ChannelCount) : "0";
			oscFreqNumericUpDown.Value = ScopeConfig.FreqCount != 0 ? ScopeConfig.FreqCount : 1;

			if (ScopeConfig.OscilEnable == 0) { enaScopeCheckBox.Checked = false; checkBox1.Checked = false; checkBox3.Checked = false; }
			if (ScopeConfig.OscilEnable == 1) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false; }
			if (ScopeConfig.OscilEnable == 2) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = false; }
			if (ScopeConfig.OscilEnable == 3) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = true; }
			if (ScopeConfig.OscilEnable == 4) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = true; }

			var listItemGroupe = treeListView.Objects.OfType<ItemGroup>().ToList();

			foreach (var itemGroup in listItemGroupe)
			{
				var count = 0;
				if (itemGroup.GetType() == typeof(ItemGroup))
				{
					foreach (var item in itemGroup.Children)
					{
						for (int i = 0; i < ScopeConfig.OscilFormat.Count; i++)
						{
							if (ScopeConfig.OscilFormat[i] == ((item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats &&
							    ScopeConfig.OscilAddr[i] == item.Value.ChannelAddrs)
							{
								channelInList[i] = true;
								treeListView.CheckObject(item);
								treeListView.RefreshObject(item);
								count++;
								break;
							}
						}
					}

					itemGroup.countSelect = count;
					itemGroup.Name = itemGroup.NameShort + $" (Выбрано {itemGroup.countSelect} из {itemGroup.Children.Count})";

					if (count == itemGroup.Children.Count)
					{
						treeListView.CheckObject(itemGroup);
					}
					else if (count > 0 && count < itemGroup.Children.Count)
					{
						treeListView.CheckIndeterminateObject(itemGroup);
					}
					else if (count == 0)
					{
						treeListView.UncheckObject(itemGroup);
					}
				}
			}

			for (int i = 0; i < ScopeConfig.OscilAddr.Count; i++)
			{
				if (channelInList[i] == false)
				{
					str += @"Адрес: 0x" + ScopeConfig.OscilAddr[i].ToString("X4") + @" Формат: " + ScopeConfig.OscilFormat[i] + "\n";
					channelInLists = true;
				}
			}
			if (channelInLists) Program.MainFormWin.MessagesBox(str, @"Information", MessageBoxButton.OK, MessageBoxImage.Information);

			SizeTrackBar();
		}

		private void SizeTrackBar()
		{
			if (ScopeConfig.ScopeCount != 0)
			{
				try
				{
					sizeOcsil_trackBar.Value = ScopeConfig.OscilSize * 100 * ScopeConfig.ScopeCount % ScopeConfig.OscilAllSize == 0
						? (int)(ScopeConfig.OscilSize * 100 * ScopeConfig.ScopeCount / ScopeConfig.OscilAllSize)
						: (int)(ScopeConfig.OscilSize * 100 * ScopeConfig.ScopeCount / ScopeConfig.OscilAllSize) + 1;
				}
				catch
				{
					Program.MainFormWin.MessagesBox(@"Ошибка при чтении конфигурации с платы" + "\nCODE 0x1003", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		public void ButtonsVisibale(bool connect)
		{
			if (connect)
			{
				reloadButton.Enabled = true;
				writeToSystemBtn.Enabled = true;
				labelAllSize.Text = $@"Размер доступной памяти: {ScopeConfig.OscilAllSize / 1024} Кб";
				labelFreq.Text = $@"Частота выборки: {(ScopeConfig.SampleRate / _nowOscFreq):D} Гц";
			}
			else
			{
				reloadButton.Enabled = false;
				writeToSystemBtn.Enabled = false;
				labelAllSize.Text = $@"Размер доступной памяти: {ScopeSysType.OscilAllSize } Кб";
				labelFreq.Text = $@"Частота выборки: {(ScopeSysType.OscilSampleRate / _nowOscFreq):D} Гц";
			}
		}

		private void ChannelCount_TextChanged(object sender, EventArgs e)
		{
			DelayOscil();
		}

		#region PrintAndShow
		private void View_toolStripButton_Click(object sender, EventArgs e)
		{
			SCPrintPreviewDialog.Document = SCPrintDocument;
			SCPrintPreviewDialog.ShowDialog();
		}

		private void Print_toolStripButton_Click(object sender, EventArgs e)
		{
			SCPrintDialog.Document = SCPrintDocument;
			if (SCPrintDialog.ShowDialog() == DialogResult.OK)
			{
				SCPrintDocument.Print();
			}
		}

		bool _firstPage = true;

		private void SCPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			int yPos = 200;
			int xPos1 = 25;
			int number = 0;

			if (_firstPage)
			{
				e.Graphics.DrawString($"Date: {DateTime.Now.ToShortDateString()}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 40));
				e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 55));
				e.Graphics.DrawString($"Описание: {CommentRichTextBox.Text}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 70));
				e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 85));
				e.Graphics.DrawString($"Количество каналов: {radioButton.Text}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 100));
				e.Graphics.DrawString($"Перезапись осциллограмм: {checkBox3.Checked}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 450, 100));
				e.Graphics.DrawString($"Количество осциллограмм: {chCountNumericUpDown.Value}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 120));
				e.Graphics.DrawString($"Сохранение на SD карту: {checkBox1.Checked}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 450, 120));
				e.Graphics.DrawString($"Предыстория: {hystoryNumericUpDown.Value}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 140));
				e.Graphics.DrawString($"Осциллографирование включено: {enaScopeCheckBox.Checked}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 450, 140));
				e.Graphics.DrawString($"Делитель: {oscFreqNumericUpDown.Value}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 160));
				e.Graphics.DrawString($"{labelFreq.Text}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 250, 160));
				e.Graphics.DrawString($"{labelAllSize.Text}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, 180));
				e.Graphics.DrawString($"{DelayOsc.Text}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 250, 180));
				e.Graphics.DrawString($"Использование пмяти: {sizeOcsil_trackBar.Value}%", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 450, 180));
				e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, 205));
				if (_admin)
				{
					e.Graphics.DrawString("№", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
					e.Graphics.DrawString("Канал", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 100, yPos));
					e.Graphics.DrawString("Адрес", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 350, yPos));
					e.Graphics.DrawString("Формат", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 600, yPos));
				}
				else
				{
					e.Graphics.DrawString("№", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
					e.Graphics.DrawString("Канал", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 100, yPos));
				}
				e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos + 10));
				foreach (var itemGroup in treeListView.Objects.OfType<ItemGroup>().ToList())
				{
					if (treeListView.IsChecked(itemGroup) || treeListView.IsCheckedIndeterminate(itemGroup))
					{
						e.Graphics.DrawString($"{itemGroup.NameShort}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 20, yPos += 25));
						foreach (var item in treeListView.CheckedObjects.OfType<Item>().ToList())
						{
							if (itemGroup.Children.Contains(item))
							{
								if (_admin)
								{
									e.Graphics.DrawString($"№{number += 1}.", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
									e.Graphics.DrawString($"{item.Name}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 100, yPos));
									e.Graphics.DrawString($"{item.Addr}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 350, yPos));
									e.Graphics.DrawString($"{item.Format}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 600, yPos));
								}
								else
								{
									e.Graphics.DrawString($"№{number += 1}.", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1, yPos += 20));
									e.Graphics.DrawString($"{item.Name}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(xPos1 + 100, yPos));
								}
							}
						}
					}
				}
			}
		}
		#endregion

		private void AutosizeColumns()
		{
			foreach (ColumnHeader col in treeListView.Columns)
			{
				//auto resize column width

				int colWidthBeforeAutoResize = col.Width - 32;
				col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
				int colWidthAfterAutoResizeByHeader = col.Width - 16;
				col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
				int colWidthAfterAutoResizeByContent = col.Width - 32;

				if (colWidthAfterAutoResizeByHeader > colWidthAfterAutoResizeByContent)
					col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

				//first column
				if (col.Index == 0)
					//we have to manually take care of tree structure, checkbox and image
					col.Width += treeListView.SmallImageSize.Width;
				//last column
				else if (col.Index == treeListView.Columns.Count - 1)
					//avoid "fill free space"
					col.Width = colWidthBeforeAutoResize > colWidthAfterAutoResizeByContent ? colWidthBeforeAutoResize : colWidthAfterAutoResizeByContent;
			}
		}
	}
}
