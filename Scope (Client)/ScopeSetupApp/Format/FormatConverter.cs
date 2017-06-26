using System;
using System.Collections.Generic;
using System.Linq;

namespace ScopeSetupApp
{
	public static class FormatConverter
	{
		public static List<Format> formatList = new List<Format>()
		{
			new Format("HexToPercent", 0, "int16", 1/40.96, 0, 1, "double", 2),
			new Format("HexToUint16", 1, "uint16", 1, 0, 1, "double", 4),
			new Format("HexToInt16", 2, "int16", 1, 0, 1, "double", 4),
			new Format("HexToFreq", 3, "uint16", 1/8000.0, 0, -1, "double", 4),
			new Format("HexTo8_8", 4, "int16", 1/256.0, 0, 1, "double", 4),
			new Format("HexTo0_16", 5, "uint16", 1/65536.0, 0, 1, "double", 4),
			new Format("HexToSlide", 6, "uint16", 1/327.68, 0, 1, "double", 4),
			new Format("HexToDigits", 7, "uint16", 1, 0, 1, "binary", 0),
			new Format("OLOLOLO", 8, "uint16", 1/327.68, 0, 1, "double", 4),
			new Format("OLOLOLO", 9, "uint16", 1, 0, 1, "binary", 0),
			new Format("HexToInt10", 10, "int16", 1/10.0, 0, 1, "double", 4),
			new Format("HexToHex", 11, "uint16", 1, 0, 1, "hex", 0),
			new Format("HexToUf", 12, "int16", 0.135, 0, 1, "double", 4),
			new Format("HexToFreqNew", 13, "uint16", 1/500.0, 0, 1, "double", 4),
			new Format("HexToTT", 14, "int16", 1/2560.0, 0, -1, "double", 2),
			new Format("HexToTransAlarm", 15, "int16", 0.00172633491500621954199424893092, 0, 1, "double", 7 ),
			new Format("HexToInt8", 16, "int16", 1/8.0, 0, 1, "double", 2),
			new Format("HexToUint1000", 17, "uint16", 1/1000.0, 0, 1, "double", 2),
			new Format("HexToPercent4", 18, "int16", 1/10.24, 0, 1, "double", 2),
			new Format("HexToFreqNew2", 19, "uint16", 1/90000.0, 0, -1, "double", 2),
			new Format("HexToPercentUpp", 20, "int16", 1/20.48, 0, 1, "double", 2),
			new Format("HexToFreqUPTF", 21, "uint16", 16000, 0, 1, "double", 2)
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
		string
		double
		*/

		public static string GetValue(ulong value, byte indexFormat)
		{
			Format temp = formatList[indexFormat];
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
							str = Math.Pow((temp.A * (ulong)value + temp.B), temp.Z).ToString($"F{temp.Smaller}");
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
				case "string":
					break;
			}

			if (temp.ExaptionList.Any())
			{
				foreach (var itemExaption in temp.ExaptionList)
				{
					if (Math.Abs(itemExaption.Value - Convert.ToDouble(str)) < 0.00001)
					{
						str = itemExaption.Massage;
						break;
					}
				}
			}

			return str;
		}




		public class Format
		{
			public string Name { get;  }
			public int Index { get;  }
			public BitDepth BitDepth { get; }
			public double A { get;  }
			public double B { get;  }
			public double Z { get;  }
			public string OutFormat { get;  }
			public int Smaller { get; }
			public List<Exaption> ExaptionList = new List<Exaption>();

			public Format(string name, int index, string bitDepth, double a, double b, double z, string outFormat, int smaller)
			{
				Name = name;
				Index = index;
				BitDepth = new BitDepth(bitDepth);
				A = a;
				B = b;
				Z = z;
				OutFormat = outFormat;
				Smaller = smaller;
			}

			public class Exaption 
			{
				public double Value { get; }
				public string Massage { get; }

				public Exaption(double val, string mes)
				{
					Value = val;
					Massage = mes;
				}
			}
		}

		public class BitDepth
		{
			public ushort Bit { get; }		// 16 - 1, 32 - 2, 64 - 3
			public bool Sign { get; }		// Signed - true, Unsigned - false

			public BitDepth(string bitDepth)
			{
				switch (bitDepth)
				{
					case "uint16":
						Bit = 1;
						Sign = false;
						break;
					case "uint32":
						Bit = 2;
						Sign = false;
						break;
					case "uint64":
						Bit = 3;
						Sign = false;
						break;
					case "int16":
						Bit = 1;
						Sign = true;
						break;
					case "int32":
						Bit = 2;
						Sign = true;
						break;
					case "int64":
						Bit = 3;
						Sign = true;
						break;
				}
			}
		}
	}
}