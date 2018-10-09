using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;


namespace ScopeApp
{
	public static class ScopeSysType
	{
		public static readonly List<ScopeChannelConfig> ScopeItem = new List<ScopeChannelConfig>();

		public static string XmlFileName = "ScopeSysType.xml";
		public static string XmlFileNameOscil = "ScopeSysType.xml";
		public static string XmlPathName;
		public static bool HasViewer;

		public static ushort ConfigurationAddr = 512;
		public static ushort OscilCmndAddr = 4096; 

		public static ushort OscilAllSize;
		  
		public static ushort OscilCount;
		public static ushort ChannelCount;
		public static ushort HistoryCount;
		public static ushort FrequncyCount;

		public static ushort OscilSampleRate;
		public static string OscilComment;

		public static ushort OscilEnable;

		//Cometrade format
		public static string StationName;
		public static string RecordingDevice;
		public static ushort OscilNominalFrequency;
		//For rev. 2013
		public static string TimeCode;
		public static string LocalCode;
		public static string TmqCode;
		public static string Leapsec;
		//For OscilConfigurator
		public static ushort SizeValue;
		public static List<string> OscilChannelNames = new List<string>();
		public static List<string> OscilChannelAddrs = new List<string>();
		public static List<string> OscilChannelFormats = new List<string>();
	    public static List<string> OscilChannelColors = new List<string>();

        static void LoadFromXml(string paramName, string wrName, XmlDocument doc, out ushort addr)
		{
			var xmls = doc.GetElementsByTagName(paramName);
			var xmlline = xmls[0];

			try
			{
				addr = xmlline.Attributes != null ? Convert.ToUInt16(xmlline.Attributes[wrName].Value) : (ushort) 0;
			}
			catch 
			{
				addr = 0;
			}
		}

		static void LoadNameFromXml(string paramName, XmlDocument doc, out string str)
		{
			var xmls = doc.GetElementsByTagName(paramName);
			var xmlline = xmls[0];
			try
			{
				str = xmlline.Attributes != null ? Convert.ToString(xmlline.Attributes["xmlns"].Value) : "";
			}
			catch 
			{
				str = "";
			}
		}
	   
		public static void InitScopeSysType()
		{
			var pathDirectory = Path.GetDirectoryName(Path.GetFullPath("ScopeSetup.exe"));
			HasViewer = File.Exists($"{pathDirectory}\\ScopeViewer.exe");



			var doc = new XmlDocument();
			try
			{
				doc.Load(XmlFileName);
				XmlPathName = Path.GetDirectoryName(Path.GetFullPath(XmlFileName));
			}
			catch
			{
				// ReSharper disable once LocalizableElement
				Program.MainFormWin.MessagesBox($"Не удалось открыть файл: {XmlFileName}!\nCODE 0x1220", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			LoadFromXml("Configuration", "Addr", doc, out ConfigurationAddr);
			LoadFromXml("OscilCmnd", "Addr", doc, out OscilCmndAddr);

			LoadFromXml("OscilAllSize", "Count", doc, out OscilAllSize);
			LoadFromXml("MeasureParams", "Count", doc, out ushort count);

			LoadFromXml("Oscil", "Count", doc, out OscilCount);
			LoadFromXml("Channel", "Count", doc, out ChannelCount);
			LoadFromXml("Story", "Count", doc, out HistoryCount);
			LoadFromXml("Frequency", "Count", doc, out FrequncyCount);
			LoadFromXml("OscilSampleRate", "Count", doc, out OscilSampleRate);
			LoadNameFromXml("Comment", doc, out OscilComment);
			//Cometrade format
			LoadNameFromXml("StationName", doc, out StationName);
			LoadNameFromXml("RecordingDevice", doc, out RecordingDevice);
			LoadFromXml("OscilNominalFrequency", "Count", doc, out OscilNominalFrequency);
			//For rev. 2013
			LoadNameFromXml("TimeCode", doc, out TimeCode);
			LoadNameFromXml("LocalCode", doc, out LocalCode);
			LoadNameFromXml("TmqCode", doc, out TmqCode);
			LoadNameFromXml("Leapsec", doc, out Leapsec);
			
			for (int i = 1; i < (count + 1); i++)
			{
				var xmls = doc.GetElementsByTagName("MeasureParam"+i.ToString());
				var xmlline = xmls[0];

				try
				{
					if (xmlline.Attributes != null)
					{
						string[] str = (Convert.ToString(xmlline.Attributes["Name"].Value)).Split('/');
						string str1;
						string str2 = "";
						if (str.Length > 1) { str1 = str[1]; str2 = str[0]; }
						else str1 = str[0];

						ScopeChannelConfig item = new ScopeChannelConfig
						{
							ChannelNames = str1,
							ChannelColor = xmlline.Attributes["Color"] != null ? xmlline.Attributes["Color"].Value : "ffffff",
							ChannelGroupNames = str2,
							ChannelTypeAd = Convert.ToUInt16(xmlline.Attributes["TypeAD"] != null ? xmlline.Attributes["TypeAD"].Value : "0"),
							ChannelAddrs = Convert.ToUInt16(xmlline.Attributes["Addr"] != null ? xmlline.Attributes["Addr"].Value:$"0x{i:X4}"),
							ChannelformatNumeric = (Convert.ToUInt16(xmlline.Attributes["Format"] != null ? xmlline.Attributes["Format"].Value : "256") >> 8) - 1,
							ChannelFormats = Convert.ToUInt16(xmlline.Attributes["Format"] != null ? xmlline.Attributes["Format"].Value : "256") & 0x00FF,
							ChannelPhase = Convert.ToString(xmlline.Attributes["Phase"] != null ? xmlline.Attributes["Phase"].Value : ""),
							ChannelCcbm = Convert.ToString(xmlline.Attributes["CCBM"] != null ? xmlline.Attributes["CCBM"].Value : ""),
							ChannelDimension = Convert.ToString(xmlline.Attributes["Dimension"] != null ? xmlline.Attributes["Dimension"].Value : "NONE"),
							ChannelMin = Convert.ToDouble(ReturnDoubleString(xmlline.Attributes["Min"] != null ? xmlline.Attributes["Min"].Value : "0")),
							ChannelMax = Convert.ToDouble(ReturnDoubleString(xmlline.Attributes["Max"] != null ? xmlline.Attributes["Max"].Value : "1"))
						};

						ScopeItem.Add(item);
					}
				}
				catch
				{
					// ReSharper disable once LocalizableElement
					Program.MainFormWin.MessagesBox($"Ошибки в файле: {XmlFileName}!\nCODE 0x1221", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			} 
		}

		private static string ReturnDoubleString(string value)
		{
			value = value.Replace('.', ',');
			return value;
		}

		public static void InitScopeOscilType()
		{
			var doc = new XmlDocument();
			try
			{
				doc.Load(XmlFileNameOscil);
			}
			catch
			{
				// ReSharper disable once LocalizableElement
				Program.MainFormWin.MessagesBox($"Не удалось открыть файл: {XmlFileNameOscil}!\nCODE 0x1230", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			LoadFromXml("Oscil", "Count", doc, out OscilCount);
			LoadFromXml("Channel", "Count", doc, out ChannelCount);
			LoadFromXml("Story", "Count", doc, out HistoryCount);
			LoadFromXml("Frequency", "Count", doc, out FrequncyCount);
			LoadFromXml("Size", "Count", doc, out SizeValue);
			LoadFromXml("OscilEnable", "Count", doc, out OscilEnable);

			OscilChannelNames = new List<string>();
			OscilChannelAddrs = new List<string>();
			OscilChannelFormats = new List<string>();


            for (int i = 1; i < ChannelCount + 1; i++)
			{
				var xmls = doc.GetElementsByTagName("MeasureParam" + i.ToString());
				var xmlline = xmls[0];

				try
				{
					if (xmlline.Attributes != null)
					{
						OscilChannelNames.Add(Convert.ToString(xmlline.Attributes["Name"].Value));
						OscilChannelAddrs.Add(Convert.ToString(xmlline.Attributes["Addr"].Value));
						OscilChannelFormats.Add(Convert.ToString(xmlline.Attributes["Format"].Value));
                    }
				}
				catch
				{
					// ReSharper disable once LocalizableElement
					Program.MainFormWin.MessagesBox($"Ошибки в файле: {XmlFileNameOscil}!\nCODE 0x1231", @"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
	}
}
