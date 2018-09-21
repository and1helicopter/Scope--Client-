using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using BrightIdeasSoftware;
using ScopeSetupApp.Format;
using UniSerialPort;
using Color = System.Drawing.Color;
using UserControl = System.Windows.Forms.UserControl;

namespace ScopeSetupApp.ucScopeSetup
{
	public partial class UcScopeSetup : UserControl
	{
		private ushort _nowHystory = 1;             //Предыстория 
		private ushort _nowScopeCount = 1;          //Количество осциллограмм
		private ushort _nowMaxChannelCount;         //Количество каналов
		private ushort _nowOscFreq = 1;             //Делитель     
		private readonly uint _oscilAllSize = ScopeSysType.OscilAllSize;             //

		private static readonly object[] Format = FormatConverter.ActualFormat;

		private static readonly object[] SizeFormat =
		{
			"16",
			"32",
			"64",
		};

		#region

	    private readonly List<Item> _channelItems = new List<Item>();

		private void InitTable(string[] agrs)
		{
		    OLVColumn groupChanel = new OLVColumn
		    {
		        AspectName = "Group",
		        IsVisible = false,
		        MaximumWidth = 20,
		        MinimumWidth = 20,
		        ShowTextInHeader = false,
		        Text = @"Группа",
		        Width = 20
		    };

		    OLVColumn nameChanel = new OLVColumn
		    {
		        AspectName = "Name",
		        Groupable = false,
		        HeaderCheckBoxUpdatesRowCheckBoxes = false,
		        IsEditable = false,
		        MinimumWidth = 200,
		        Text = @"Название канала",
		        Width = 200
		    };

		    OLVColumn colorChanel = new OLVColumn
		    {
		        AspectName = "Color",
		        ButtonSizing = OLVColumn.ButtonSizingMode.CellBounds,
		        Groupable = false,
		        HeaderCheckBoxUpdatesRowCheckBoxes = false,
		        MinimumWidth = 100,
		        Text = @"Цвет",
		        Width = 100,
		        WordWrap = true
		    };

		    OLVColumn colorsChanel = new OLVColumn
		    {
		        AspectName = "Colors",
		        MinimumWidth = 100,
		        ShowTextInHeader = false,
		        Text = @"Color",
		        Width = 100
		    };
            if (agrs.Length > 0)
			{


                if (agrs[0] == "a" || agrs[0] == "A")
				{
				    OLVColumn addrChanel = new OLVColumn
				    {
				        AspectName = "Addr",
				        Groupable = false,
				        HeaderCheckBoxUpdatesRowCheckBoxes = false,
				        MinimumWidth = 160,
				        Text = @"Адрес",
				        Width = 160
				    };

				    OLVColumn formatChanel = new OLVColumn
				    {
				        AspectName = "Format",
				        Groupable = false,
				        HeaderCheckBoxUpdatesRowCheckBoxes = false,
				        MinimumWidth = 160,
				        Text = @"Формат",
				        Width = 160
				    };

				    dataListView.AllColumns.Add(groupChanel);
				    dataListView.AllColumns.Add(nameChanel);
				    dataListView.AllColumns.Add(addrChanel);
				    dataListView.AllColumns.Add(formatChanel);
				    dataListView.AllColumns.Add(colorChanel);
				    dataListView.AllColumns.Add(colorsChanel);

				    dataListView.Columns.AddRange(new ColumnHeader[]
				    {
				        groupChanel,
				        nameChanel,
				        addrChanel,
				        formatChanel,
                        colorChanel,
				        colorsChanel
				    });
                }
            }
			else
            {
                colorChanel.MinimumWidth = 200;
                colorsChanel.MinimumWidth = 200;

                dataListView.AllColumns.Add(groupChanel);
			    dataListView.AllColumns.Add(nameChanel);
			    dataListView.AllColumns.Add(colorChanel);
			    dataListView.AllColumns.Add(colorsChanel);

			    dataListView.Columns.AddRange(new ColumnHeader[]
			    {
			        groupChanel,
			        nameChanel,
			        colorChanel,
			        colorsChanel
                });
			}

            CommentRichTextBox.Text = ScopeSysType.OscilComment;

            colorChanel.ButtonSizing = OLVColumn.ButtonSizingMode.CellBounds;
		    colorChanel.ButtonSize = new Size(48, 16);
            colorChanel.IsButton = true;
		    colorChanel.TextAlign = HorizontalAlignment.Center;
		    
            foreach (var item in ScopeSysType.ScopeItem)
            {
                var value = new Item(item);
                _channelItems.Add(value);
            }

		    dataListView.SetObjects(_channelItems);
            dataListView.UseCellFormatEvents = true;
            dataListView.FormatCell += DataListViewOnFormatCell;
            dataListView.FormatRow += DataListViewOnFormatRow;


            dataListView.ButtonClick += delegate(object sender, CellClickEventArgs e)
		    {
		        ColorDialog cd = new ColorDialog();
		        if (cd.ShowDialog() == DialogResult.OK)
		        {
                    e.SubItem.BackColor = cd.Color;
		            e.SubItem.Text = cd.Color.R.ToString("x2") + cd.Color.G.ToString("x2") + cd.Color.B.ToString("x2");

                    ((Item)e.Model).Color = cd.Color.R.ToString("x2") + cd.Color.G.ToString("x2") + cd.Color.B.ToString("x2");
                    dataListView.RefreshObject(e.Model);
                }
            };
		}

	    private class Item
	    {
	        public string Name { get; set; }
	        public string Addr { get; set; }
	        public string Format { get; set; }
	        public string Group { get; set; }
	        public string Color { get; set; }
            public ScopeChannelConfig Value { get; set; }

            public Item(ScopeChannelConfig item)
            {
                Value = item;
                Name = item.ChannelNames;
                Addr = "0x" + item.ChannelAddrs.ToString("x4");
                Format = Convert.ToString(SizeFormat[item.ChannelformatNumeric]) + "b   " + Convert.ToString(UcScopeSetup.Format[item.ChannelFormats]);
                Color = "ffffff";
                Group = item.ChannelGroupNames != "" ? item.ChannelGroupNames : @"Несгруппированные параметры";
	        }
	    }

        private void DataListViewOnFormatRow(object o, FormatRowEventArgs formatRowEventArgs)
	    {
	        formatRowEventArgs.Item.BackColor = formatRowEventArgs.Item.Checked ? Color.LightSteelBlue : SystemColors.ButtonHighlight;
	    }

        private void DataListViewOnFormatCell(object o, FormatCellEventArgs formatCellEventArgs)
	    {
	        for (int i = 0; i < dataListView.Items.Count; i++)
	        {
	            var colorIndex = dataListView.Columns.Count == 4 ? 2 : 4;
	            var colorsIndex = dataListView.Columns.Count == 4 ? 3 : 5;

                var str = dataListView.Items[i].SubItems[colorIndex].Text;
	            var red = int.Parse(str.Substring(0, 2), NumberStyles.AllowHexSpecifier);
	            var green = int.Parse(str.Substring(2, 2), NumberStyles.AllowHexSpecifier);
	            var blue = int.Parse(str.Substring(4, 2), NumberStyles.AllowHexSpecifier);

                dataListView.Items[i].SubItems[colorsIndex].BackColor = Color.FromArgb(red, green, blue);
	        }
        }

	    private void dataListView_ItemChecked(object sender, ItemCheckedEventArgs e)
	    {
            e.Item.BackColor = e.Item.Checked ? Color.LightSteelBlue : SystemColors.ButtonHighlight;

	        if (e.Item.Checked)
	        {
	            var item = (Item)dataListView.GetModelObject(e.Item.Index);
	            if (item.Color == "ffffff")
	            {
	                var random = new Random();
	                var color = $"{random.Next(0xffffff):X6}";
	                item.Color = color;
	                dataListView.RefreshObject(color);
                }
	        }

	        _nowMaxChannelCount = (ushort) dataListView.CheckedItems.Count;
	        radioButton.Text = _nowMaxChannelCount.ToString();
        }

        #endregion

        public UcScopeSetup(string[] agrs)
		{
			InitializeComponent();

			InitTable(agrs);
		    reloadButton_Click(null, null);
			labelAllSize.Text = $@"Размер доступной памяти: {ScopeConfig.OscilAllSize/1024} Кб";
			labelFreq.Text = $@"Частота выборки: {(ScopeConfig.SampleRate / _nowOscFreq):D} Гц";
		}

		//****************************************************************************//
		//****************************************************************************//
		//****************************************************************************//
		//Обработка полей формы
		#region  
		//Количество осциллограмм
		private void chCount_TextChanged(object sender, EventArgs e)
		{
			_nowScopeCount = (ushort) chCountNumericUpDown.Value;

			DelayOscil();
		}

		//Предыстория 
		private void hystory_TextChanged(object sender, EventArgs e)
		{
			_nowHystory = (ushort) hystoryNumericUpDown.Value;
		}

		//Делитель
		private void oscFreq_TextChanged(object sender, EventArgs e)
		{
			_nowOscFreq = (ushort) oscFreqNumericUpDown.Value;

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
		}
		#endregion

		//**************ИЗМЕНЕНИЕ КОНФИГУРАЦИИ ОСЦИЛЛОГРАФА *********************************************//
		//***********************************************************************************************//
		//***********************************************************************************************//
		private ushort[] _newOscillConfig = new ushort[40];
		private ushort[] _oscillConfig = new ushort[1280];
		private readonly int[] _channelSeries = new int[32];        //Где какой элемент лежит

		private int _writeConfigStep;
		private ushort _writeStep;

		//Конфигурирование осциллограммы 
		private List<ushort> ChannelFormats()
		{
			int j = 0;
			List<ushort> l = new List<ushort>();
			for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    var item = (Item)dataListView.GetModelObject(i);
                if (dataListView.Items[i].Checked && item.Value.ChannelformatNumeric == 2)
                {
                    _channelSeries[j++] = i;
                    l.Add(Convert.ToUInt16((Convert.ToInt32(item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats));
                }
			}
			for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    var item = (Item)dataListView.GetModelObject(i);
                if (dataListView.Items[i].Checked && item.Value.ChannelformatNumeric == 1)
				{
					_channelSeries[j++] = i;
					l.Add(Convert.ToUInt16((Convert.ToInt32(item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats));
				}
			}
			for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    var item = (Item)dataListView.GetModelObject(i);
                if (dataListView.Items[i].Checked && item.Value.ChannelformatNumeric == 0)
				{
					_channelSeries[j++] = i;
					l.Add(Convert.ToUInt16((Convert.ToInt32(item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats));
				}
			}
			if (l.Count > _nowMaxChannelCount) { l.Clear(); }
			return l;
		}

		// Channel Addrs
		private List<ushort> ChannelAddrs()
		{
			List<ushort> l = new List<ushort>();
			for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        var item = (Item)dataListView.GetModelObject(_channelSeries[j++]);
                    l.Add(item.Value.ChannelAddrs);
			    }
			}
			if (l.Count > _nowMaxChannelCount) { l.Clear(); }

			return l;
		}

		// OscilSize
		private uint OscilSize(uint allSize, bool wr)
		{
			uint count64 = 0, count32 = 0, count16 = 0;
		    Item item;
            for (int i = 0; i < dataListView.Items.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        item = (Item)dataListView.GetModelObject(i);
                    if(item.Value.ChannelformatNumeric == 2) { count64++; }
                }
			}
			for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        item = (Item)dataListView.GetModelObject(i);
			        if (item.Value.ChannelformatNumeric == 1) { count32++; }
			    }
			}

			for (int i = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        item = (Item)dataListView.GetModelObject(i);
			        if (item.Value.ChannelformatNumeric == 0) { count16++; }
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
			for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        var item = (Item)dataListView.GetModelObject(_channelSeries[j++]);
			        var str = new char[32];
                    var color = item.Color;

                    for (int index = 0; index < item.Value.ChannelNames.Length; index++)
			            str[index] = item.Value.ChannelNames[index];

			        str[29] = (char) byte.Parse(color.Substring(0, 2), NumberStyles.AllowHexSpecifier);
			        str[30] = (char) byte.Parse(color.Substring(2, 2), NumberStyles.AllowHexSpecifier);
			        str[31] = (char) byte.Parse(color.Substring(4, 2), NumberStyles.AllowHexSpecifier);

                    var valName = new string(str);
                    chName.Add(valName);
			    }
			}
			if (chName.Count > _nowMaxChannelCount) { chName.Clear(); }
			return chName;
		}
		//////////////////////////////////////////
		//For Comtrade 
		private List<string> ChPhase()
		{
			List<string> l = new List<string>();
			for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        var item = (Item) dataListView.GetModelObject(_channelSeries[j++]);
			        l.Add(item.Value.ChannelPhase);
			    }
			}
			if (l.Count > _nowMaxChannelCount) { l.Clear(); }

			return l;
		}

		private List<string> ChCcbm()
		{
			List<string> l = new List<string>();
			for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        var item = (Item)dataListView.GetModelObject(_channelSeries[j++]);
			        l.Add(item.Value.ChannelCcbm);
			    }
			}
			if (l.Count > _nowMaxChannelCount) { l.Clear(); }

			return l;
		}

		private List<string> ChDemension()
		{
			List<string> l = new List<string>();
			for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        var item = (Item)dataListView.GetModelObject(_channelSeries[j++]);
			        l.Add(item.Value.ChannelDimension);
			    }
			}
			if (l.Count > _nowMaxChannelCount) { l.Clear(); }

			return l;
		}

		private List<ushort> ChTypeAd()
		{
			List<ushort> l = new List<ushort>();
			for (int i = 0, j = 0; i < ScopeSysType.ScopeItem.Count; i++)
			{
			    if (dataListView.Items[i].Checked)
			    {
			        var item = (Item)dataListView.GetModelObject(_channelSeries[j++]);
			        l.Add(item.Value.ChannelTypeAd);
			    }
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
					MessageBox.Show(@"Ошибка связи!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		public void  StatusConfigToSystemStrLabel()
		{
		    // ReSharper disable LocalizableElement
            if ((ScopeConfig.StatusOscil & 0x0000) == 0x0000)
		    {
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"Конфигурация загружена и принята!" + 
                                "\n" + @"CODE: 0x0000", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((ScopeConfig.StatusOscil & 0x0001) == 0x0001)
			{
				MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"Конфигурация загружена не полностью!" + 
				                "\n" + @"CODE: 0x0001", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if ((ScopeConfig.StatusOscil & 0x0002) == 0x0002)
			{
			    MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимое количество осциллограмм!" +
			                    "\n" + @"CODE: 0x0002", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		    else if ((ScopeConfig.StatusOscil & 0x0003) == 0x0003)
		    {
		        MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимое количество каналов!" +
		                        "\n" + @"CODE: 0x0003", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		    }
            else if ((ScopeConfig.StatusOscil & 0x0004) == 0x0004)
			{
			    MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимое значение предыстории!" +
			                    "\n" + @"CODE: 0x0004", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((ScopeConfig.StatusOscil & 0x0005) == 0x0005)
            {
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не выровняны 32b и 64b данные!" +
                                "\n" + @"CODE: 0x0005", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((ScopeConfig.StatusOscil & 0x0006) == 0x0006)
            {
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации не допустимые 8b данные!" +
                                "\n" + @"CODE: 0x0006", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((ScopeConfig.StatusOscil & 0x0007) == 0x0007)
            {
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации нарушен порядок каналов!" +
                                "\n" + @"CODE: 0x0007", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((ScopeConfig.StatusOscil & 0x0008) == 0x0008)
            {
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"В конфигурации размер памяти не кратен 64b!" +
                                "\n" + @"CODE: 0x0008", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((ScopeConfig.StatusOscil & 0x0009) == 0x0009)
            {
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"Недостаточно памяти для конфигурации!" +
                                "\n" + @"CODE: 0x0009", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((ScopeConfig.StatusOscil & 0x000A) == 0x000A)
            {
                MessageBox.Show(@"Конфигурация осциллографа была передана!" + "\n" + @"Память под осциллограммы содержит не расчитано на целое число записей!" +
                                "\n" + @"CODE: 0x000A", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ScopeConfig.SendNewConfig = false;
		}

		private void writeToSystemBtn_Click(object sender, EventArgs e)
		{
			if (_nowMaxChannelCount < 1 || _nowMaxChannelCount > 32)
			{
				MessageBox.Show(@"Выбрано неверное число каналов", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!MainForm.MainForm.SerialPort.IsOpen)
			{
				MessageBox.Show(@"Соединение с системой не установлено!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		    dataListView.UncheckAll();

			OpenFileDialog ofd = new OpenFileDialog
			{
				DefaultExt = @".xoc",                                                                  // Default file extension
				Filter = @"XML Oscil Configuration |*.xoc|XML|*.xml|All files|*.*"                     // Filter files by extension  
			};

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				ScopeSysType.XmlFileNameOscil = ofd.FileName;
				try
				{
					ScopeSysType.InitScopeOscilType();
				}
				catch
				{
					MessageBox.Show(@"Ошибка загрузки данных", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

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

			for (int i = 0; i < ScopeSysType.OscilChannelNames.Count; i++)
			{
			    for (int j = 0; j < dataListView.Items.Count; j++)
			    {
			        var item = (Item)dataListView.GetModelObject(j);
			        if (ScopeSysType.OscilChannelFormats[i] == item.Format && ScopeSysType.OscilChannelAddrs[i] == item.Addr)  //&& ScopeSysType.OscilChannelNames[i] == item.Name
                    {
			            channelInList[i] = true;
                        dataListView.CheckObject(item);
                        item.Color = ScopeSysType.OscilChannelColors[i];
                        dataListView.RefreshObject(item);
                        break;
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

		//Сохранение осциллограммы в файл
		private void saveButton2_Click(object sender, EventArgs e)
		{
			if (_nowMaxChannelCount != ChNames().Count)
			{
				MessageBox.Show(@"Количество осциллографируемых и выбранных каналов не совпадает", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

			    for (int i = 0; i < dataListView.Items.Count; i++)
			    {
			        if (dataListView.Items[i].Checked)
			        {
			            xmlOut.WriteStartElement("MeasureParam" + (++j));

			            xmlOut.WriteAttributeString("Name", dataListView.Items[i].SubItems[1].Text);
			            xmlOut.WriteAttributeString("Addr", dataListView.Items[i].SubItems[2].Text);
			            xmlOut.WriteAttributeString("Format", dataListView.Items[i].SubItems[3].Text);      //((((ScopeSysType.ScopeItem[item.Index].ChannelformatNumeric) + 1) << 8) + ScopeSysType.ScopeItem[item.Index].ChannelFormats).ToString()
                        xmlOut.WriteAttributeString("Color", dataListView.Items[i].SubItems[4].Text);

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
			if (ScopeConfig.ChannelCount == 0)
			{
				MessageBox.Show(@"В системе отсутствует конфигурация", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string str = "Следующих каналов из системы нет в списке:\n";
			bool channelInLists = false;
			bool[] channelInList = new bool[32];
			for (int i = 0; i < 32; i++) { channelInList[i] = false; }

		    dataListView.UncheckAll();

			chCountNumericUpDown.Value = ScopeConfig.ScopeCount != 0 ? ScopeConfig.ScopeCount : 1;
			hystoryNumericUpDown.Value = ScopeConfig.HistoryCount != 0 ? ScopeConfig.HistoryCount : 0;
			radioButton.Text = ScopeConfig.ChannelCount != 0 ? Convert.ToString(ScopeConfig.ChannelCount) : "0";
			oscFreqNumericUpDown.Value = ScopeConfig.FreqCount != 0 ? ScopeConfig.FreqCount : 1;

			if (ScopeConfig.OscilEnable == 0) { enaScopeCheckBox.Checked = false; checkBox1.Checked = false; checkBox3.Checked = false; }
			if (ScopeConfig.OscilEnable == 1) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = false; }
			if (ScopeConfig.OscilEnable == 2) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = false; }
			if (ScopeConfig.OscilEnable == 3) { enaScopeCheckBox.Checked = true; checkBox1.Checked = false; checkBox3.Checked = true; }
			if (ScopeConfig.OscilEnable == 4) { enaScopeCheckBox.Checked = true; checkBox1.Checked = true; checkBox3.Checked = true; }

			for (int i = 0; i < ScopeConfig.OscilFormat.Count; i++)
			{
			    for (int j = 0; j < dataListView.Items.Count; j++)
			    {
			        var item = (Item)dataListView.GetModelObject(j);

			        if (ScopeConfig.OscilFormat[i] == ((item.Value.ChannelformatNumeric + 1) << 8) + item.Value.ChannelFormats &&
			            ScopeConfig.OscilAddr[i] == item.Value.ChannelAddrs)
			        {
			            channelInList[i] = true;
			            //Изменяю цвет
                        var name = Encoding.Default.GetBytes(ScopeConfig.ChannelName[i].Substring(29, 3));
			            item.Color = BitConverter.ToString(name).Replace("-", string.Empty).ToLower();
                        dataListView.CheckObject(item);
                        dataListView.RefreshObject(item);
                        break;
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
			if (channelInLists) MessageBox.Show(str, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
					MessageBox.Show(@"Ошибка при чтении конфигурации с платы", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		public void ButtonsVisibale(bool connect)
		{
			if (connect)
			{
				reloadButton.Enabled = true;
				writeToSystemBtn.Enabled = true;
			}
			else
			{
				reloadButton.Enabled = false;
				writeToSystemBtn.Enabled = false;
			}
		}

		private void ChannelCount_TextChanged(object sender, EventArgs e)
		{
			DelayOscil();
		}
	}
}
