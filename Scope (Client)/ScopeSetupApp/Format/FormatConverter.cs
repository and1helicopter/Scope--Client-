using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ScopeSetupApp.Format
{
	public static class FormatConverter
	{
		public static List<Format> FormatList = new List<Format>()
		{
			new Format("HexToPercent",  "int16", "1/40.96", "0", "1", "double", 2),
			new Format("HexToUint16", "uint16", "1", "0", "1", "double", 4),
			new Format("HexToInt16", "int16", "1", "0", "1", "double", 4),
			new Format("HexToFreq", "uint16", "1/8000.0", "0", "-1", "double", 4),
			new Format("HexTo8_8", "int16", "1/256.0", "0", "1", "double", 4),
			new Format("HexTo0_16", "uint16", "1/65536.0", "0", "1", "double", 4),
			new Format("HexToSlide", "uint16", "1/327.68", "0", "1", "double", 4),
			new Format("HexToDigits", "uint16", "1", "0", "1", "binary", 0),
			new Format("HexRegulMode", "uint16", "1", "0", "1", "double", 0),
			new Format("HexToAVRType", "uint16", "1", "0", "1", "double", 0),
			new Format("HexToInt10", "int16", "1/10.0", "0", "1", "double", 4),
			new Format("HexToHex", "uint16", "1", "0", "1", "hex", 0),
			new Format("HexToUf", "int16", "0.135", "0", "1", "double", 4),
			new Format("HexToFreqNew", "uint16", "1/500.0", "0", "1", "double", 4),
			new Format("HexToTT", "int16", "1/2560.0", "0", "-1", "double", 2),
			new Format("HexToTransAlarm", "int16", "0.00172633491500621954199424893092", "0", "1", "double", 7 ),
			new Format("HexToInt8", "int16", "1/8.0", "0", "1", "double", 2),
			new Format("HexToUint1000", "uint16", "1/1000.0", "0", "1", "double", 2),
			new Format("HexToPercent4", "int16", "1/10.24", "0", "1", "double", 2),
			new Format("HexToFreqNew2", "uint16", "1/90000.0", "0", "-1", "double", 2),
			new Format("HexToPercentUpp", "int16", "1/20.48", "0", "1", "double", 2),
			new Format("HexToFreqUPTF", "uint16", "16000", "0", "1", "double", 2)
		};
		
		/*  ____Discription____ 
		(A*value + B)^Z
								
		Bit depth:		 
						
		uint16  int16	16bit	1
		uint32  int32	32bit	2
		uint64  int64	64bit	3	
		false	true

		OutFormat:

		binary
		hex
		double
		*/

		public static string GetValue(ulong value, byte indexFormat)
		{
			Format temp = FormatList[indexFormat];
			string str = "";
		
			switch (temp.BitDepth.Sign)
			{
				case true:
					switch (temp.BitDepth.Bit)
					{
						case 1:
							str = Math.Pow((temp.A * (short)value + temp.B), temp.Z).ToString($"F{temp.Smaller}");
							break;
						case 2:
							str = Math.Pow((temp.A * (int)value + temp.B), temp.Z).ToString($"F{temp.Smaller}");
							break;
						case 3:
							str = Math.Pow((temp.A * (long)value + temp.B), temp.Z).ToString($"F{temp.Smaller}");
							break;
					}
					break;
				case false:
					switch (temp.BitDepth.Bit)
					{
						case 1:
							str = Math.Pow((temp.A * (ushort)value + temp.B), temp.Z).ToString($"F{temp.Smaller}");
							break;
						case 2:
							str = Math.Pow((temp.A * (uint)value + temp.B), temp.Z).ToString($"F{temp.Smaller}");
							break;
						case 3:
							str = Math.Pow((temp.A * value + temp.B), temp.Z).ToString($"F{temp.Smaller}");
							break;
					}
					break;
			}

			switch (temp.OutFormat)
			{
				case "binary":
					str = Convert.ToString(Convert.ToInt64(str), 2).ToUpper();
					break;
				case "hex":
					str = Convert.ToString(Convert.ToInt64(str), 16).ToUpper();
					break;
			}

			return str;
		}

		public class Format
		{
			public string Name { get; set; }
			public BitDepth BitDepth { get; set; }
			public string AStr { get; private set; }
			public double A { get; private set; }
			public string BStr { get; private set; }
			public double B { get; private set; }
			public string ZStr{ get; private set; }
			public double Z { get; private set; }
			public string OutFormat { get; set; }
			public uint Smaller { get; set; }

			public void AChange(string a)
			{
				AStr = a;
				A = ConvertToDouble(AStr);
			}

			public void BChange(string b)
			{
				BStr = b;
				B = ConvertToDouble(BStr);
			}

			public void ZChange(string z)
			{
				ZStr = z;
				Z = ConvertToDouble(ZStr);
			}

			public Format(string name, string bitDepth, string a, string b, string z, string outFormat, uint smaller)
			{
				Name = name;
				BitDepth = new BitDepth(bitDepth);
				AStr = a;
				A = ConvertToDouble(AStr);
				BStr = b;
				B = ConvertToDouble(BStr);
				ZStr = z;
				Z = ConvertToDouble(ZStr);
				OutFormat = outFormat;
				Smaller = smaller;
			}

			private double ConvertToDouble(string val)
			{
				double valOut;

				if (val.Split('/').Length == 2)
				{
					var valStr = val.Split('/');
					valOut = Convert.ToDouble(valStr[0].Length != 0 ? valStr[0].Replace('.', ','):"0") 
						/ Convert.ToDouble(valStr[1].Length != 0 ? valStr[1].Replace('.',','):"1");
				}
				//else if (val.Split('*').Length == 2)
				//{
				//	var valStr = val.Split('*');
				//	valOut = Convert.ToDouble(valStr[0].Length != 0 ? valStr[0].Replace('.', ',') : "0")
				//	         * Convert.ToDouble(valStr[1].Length != 0 ? valStr[1].Replace('.', ',') : "0");
				//}
				//else if (val.Split('+').Length == 2)
				//{
				//	var valStr = val.Split('+');
				//	valOut = Convert.ToDouble(valStr[0].Length != 0 ? valStr[0].Replace('.', ','):"0") 
				//		+ Convert.ToDouble(valStr[1].Length != 0 ? valStr[1].Replace('.', ',') : "0");
				//}
				//else if (val.Split('-').Length == 2)
				//{
				//	var valStr = val.Split('-');
				//	valOut = Convert.ToDouble(valStr[0].Length != 0 ? valStr[0].Replace('.', ',') : "0") 
				//		- Convert.ToDouble(valStr[1].Length != 0? valStr[1].Replace('.', ','):"0");
				//}
				else
				{
					valOut = Convert.ToDouble(val.Replace('.', ','));
				}

				return valOut;
			}
		}

		public class BitDepth
		{
			public string Name { get; }
			public ushort Bit { get; }		// 16 - 1, 32 - 2, 64 - 3
			public bool Sign { get; }		// Signed - true, Unsigned - false

			public BitDepth(string bitDepth)
			{
				switch (bitDepth)
				{
					case "uint16":
						Bit = 1;
						Sign = false;
						Name = "uint16";
						break;
					case "uint32":
						Bit = 2;
						Sign = false;
						Name = "uint32";
						break;
					case "uint64":
						Bit = 3;
						Sign = false;
						Name = "uint64";
						break;
					case "int16":
						Bit = 1;
						Sign = true;
						Name = "int16";
						break;
					case "int32":
						Bit = 2;
						Sign = true;
						Name = "int32";
						break;
					case "int64":
						Bit = 3;
						Sign = true;
						Name = "int64";
						break;
				}
			}
		}

		public static void ReadFormats()
		{
			//задаем путь к нашему рабочему файлу XML
			string filePath = @"Formats.xml";

			XDocument doc;
			try
			{
				doc = XDocument.Load(filePath);
			}
			catch
			{
				return;
			}

			//читаем данные из файла
			var xElement = doc.Element("Formats");
			if (xElement != null)
			{
				var formatList = xElement.Elements("Format").ToList();

				foreach (var itemFormat in formatList)
				{
					if (itemFormat.Attribute("name") != null)
					{
						if (!(from x in FormatList
							 where x.Name == itemFormat.Attribute("name")?.Value
							 select x).ToList().Any())
						{
							FormatList.Add(new Format(
								itemFormat.Attribute("name")?.Value,
								itemFormat.Attribute("bitDepth")?.Value,
								itemFormat.Attribute("A")?.Value,
								itemFormat.Attribute("B")?.Value, 
								itemFormat.Attribute("Z")?.Value,
								itemFormat.Attribute("OutFormat")?.Value,
								Convert.ToUInt32(itemFormat.Attribute("Smaller")?.Value)));
						}
					}
				}
			}
		}

		public static void SaveFormats()
		{
			string savePath = "Formats.xml";

			FileStream fs = new FileStream(savePath, FileMode.Create);

			XDocument xDocument =new XDocument(new XElement("Formats"));

			foreach (var itemFormat in FormatList)
			{
				xDocument.Element("Formats")?.Add(new XElement("Format",
					new XAttribute("name", Convert.ToString(itemFormat.Name)),
					new XAttribute("bitDepth", BitDepthToString(itemFormat.BitDepth)),
					new XAttribute("OutFormat", Convert.ToString(itemFormat.OutFormat)),
					new XAttribute("Smaller", Convert.ToString(itemFormat.Smaller)),
					new XAttribute("A", itemFormat.AStr),
					new XAttribute("B", itemFormat.BStr),
					new XAttribute("Z", itemFormat.ZStr)));
			}

			xDocument.Save(fs);
			fs.Close();
		}

		private static string BitDepthToString(BitDepth item)
		{
			//public ushort Bit { get; }      // 16 - 1, 32 - 2, 64 - 3
			//public bool Sign { get; }		// Signed - true, Unsigned - false

			string val;

			if (item.Sign)
			{
				switch (item.Bit)
				{
					case 3:
						val = "int64";
						break;
					case 2:
						val = "int32";
						break;
					default:
						val = "int16";
						break;
				}
			}
			else
			{
				switch (item.Bit)
				{
					case 3:
						val = "uint64";
						break;
					case 2:
						val = "uint32";
						break;
					default:
						val = "uint16";
						break;
				}
			}

			return val;
		}

	}
}