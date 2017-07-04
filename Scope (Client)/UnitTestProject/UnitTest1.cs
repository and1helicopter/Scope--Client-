using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScopeSetupApp.Format;

namespace UnitTestProject
{
	[TestClass]
	public class HexToPercent
	{
		[TestMethod]
		public void HexToPercent1()
		{
			Assert.AreEqual((-86.328125).ToString("F2"), FormatConverter.GetValue(62000, 0));
		}

		[TestMethod]
		public void HexToPercent2()
		{
			Assert.AreEqual((2.44140625).ToString("F2"), FormatConverter.GetValue(100, 0));
		}
	}

	[TestClass]
	public class HexToUint16
	{
		[TestMethod]
		public void HexToUint16_1()
		{
			Assert.AreEqual((62000).ToString("F4"), FormatConverter.GetValue(62000, 1));
		}

		[TestMethod]
		public void HexToUint16_2()
		{
			Assert.AreEqual((100).ToString("F4"), FormatConverter.GetValue(100, 1));
		}
	}

	[TestClass]
	public class HexToInt16
	{
		[TestMethod]
		public void HexToInt16_1()
		{
			Assert.AreEqual((-3536).ToString("F4"), FormatConverter.GetValue(62000, 2));
		}

		[TestMethod]
		public void HexToInt16_2()
		{
			Assert.AreEqual((100).ToString("F4"), FormatConverter.GetValue(100, 2));
		}
	}

	[TestClass]
	public class HexToFreq
	{
		[TestMethod]
		public void HexToFreq_1()
		{
			Assert.AreEqual((0.5).ToString("F4"), FormatConverter.GetValue(16000, 3));
		}

		[TestMethod]
		public void HexToFreq_2()
		{
			Assert.AreEqual((1).ToString("F4"), FormatConverter.GetValue(8000, 3));
		}
	}

	[TestClass]
	public class HexTo8_8
	{
		[TestMethod]
		public void HexTo8_8_1()
		{
			Assert.AreEqual((1).ToString("F4"), FormatConverter.GetValue(256, 4));
		}

		[TestMethod]
		public void HexTo8_8_2()
		{
			Assert.AreEqual((-56).ToString("F4"), FormatConverter.GetValue(51200, 4));
		}
	}

	[TestClass]
	public class HexTo0_16
	{
		[TestMethod]
		public void HexTo0_16_1()
		{
			Assert.AreEqual((0).ToString("F4"), FormatConverter.GetValue(65536, 5));
		}

		[TestMethod]
		public void HexTo0_16_2()
		{
			Assert.AreEqual((1/65536).ToString("F4"), FormatConverter.GetValue(1, 5));
		}
	}

	[TestClass]
	public class HexToSlide
	{
		[TestMethod]
		public void HexToSlide_1()
		{
			Assert.AreEqual((0).ToString("F4"), FormatConverter.GetValue(0, 6));
		}

		[TestMethod]
		public void HexToSlide_2()
		{
			Assert.AreEqual((100).ToString("F4"), FormatConverter.GetValue(32768, 6));
		}

		[TestMethod]
		public void HexToSlide_3()
		{
			Assert.AreEqual((0.9765625).ToString("F4"), FormatConverter.GetValue(320, 6));
		}
	}

	[TestClass]
	public class HexToDigits
	{
		[TestMethod]
		public void HexToDigits_1()
		{
			Assert.AreEqual((1010).ToString("F0"), FormatConverter.GetValue(10, 7));
		}

		[TestMethod]
		public void HexToDigits_2()
		{
			Assert.AreEqual((1).ToString("F0"), FormatConverter.GetValue(1, 7));
		}

		[TestMethod]
		public void HexToDigits_3()
		{
			Assert.AreEqual( "1111110111101000", FormatConverter.GetValue(65000, 7));
		}
	}

	[TestClass]
	public class HexToInt10
	{
		[TestMethod]
		public void HexToInt10_1()
		{
			Assert.AreEqual((1).ToString("F4"), FormatConverter.GetValue(10, 10));
		}

		[TestMethod]
		public void HexToInt10_2()
		{
			Assert.AreEqual((-1053.6000).ToString("F4"), FormatConverter.GetValue(55000, 10));
		}
	}

	[TestClass]
	public class HexToHex
	{
		[TestMethod]
		public void HexToHex_1()
		{
			Assert.AreEqual("F", FormatConverter.GetValue(15, 11));
		}

		[TestMethod]
		public void HexToHex_2()
		{
			Assert.AreEqual("DDD8", FormatConverter.GetValue(56792, 11));
		}
	}

	[TestClass]
	public class HexToUf
	{
		[TestMethod]
		public void HexToUf_1()
		{
			Assert.AreEqual((135.0000).ToString("F4"), FormatConverter.GetValue(1000, 12));
		}

		[TestMethod]
		public void HexToUf_2()
		{
			Assert.AreEqual((1350.0000).ToString("F4"), FormatConverter.GetValue(10000, 12));
		}
	}

	[TestClass]
	public class HexToFreqNew
	{
		[TestMethod]
		public void HexToFreqNew_1()
		{
			Assert.AreEqual((20.0000).ToString("F4"), FormatConverter.GetValue(10000, 13));
		}

		[TestMethod]
		public void HexToFreqNew_2()
		{
			Assert.AreEqual((80.0000).ToString("F4"), FormatConverter.GetValue(40000, 13));
		}
	}

	[TestClass]
	public class HexToTT
	{
		[TestMethod]
		public void HexToTT_1()
		{
			Assert.AreEqual((1.00).ToString("F2"), FormatConverter.GetValue(2560, 14));
		}

		[TestMethod]
		public void HexToTT_2()
		{
			Assert.AreEqual((-0.17857142857142857142857142857143).ToString("F2"), FormatConverter.GetValue(51200, 14));
		}
	}

	[TestClass]
	public class HexToTransAlarm
	{
		[TestMethod]
		public void HexToTransAlarm_1()
		{
			Assert.AreEqual((4.4194174).ToString("F7"), FormatConverter.GetValue(2560, 15));
		}

		[TestMethod]
		public void HexToTransAlarm_2()
		{
			Assert.AreEqual((-24.7487373).ToString("F7"), FormatConverter.GetValue(51200, 15));
		}
	}

	[TestClass]
	public class HexRegulMode
	{
		[TestMethod]
		public void HexRegulMode_1()
		{
			Assert.AreEqual((0).ToString("D"), FormatConverter.GetValue(0, 8));
		}

		[TestMethod]
		public void HexRegulMode_2()
		{
			Assert.AreEqual((1).ToString("D"), FormatConverter.GetValue(1, 8));
		}

		[TestMethod]
		public void HexRegulMode_3()
		{
			Assert.AreEqual((11).ToString("D"), FormatConverter.GetValue(11, 8));
		}
	}

	//[TestClass]
	//public class TestDouble
	//{
	//	[TestMethod]
	//	public void TestDouble_1()
	//	{
	//		FormatConverter.FormatList.Add(new FormatConverter.Format("HexToPercent", "int16", "1 / 40.96", "0", "1", "double", 2));

	//		Assert.AreEqual((0.0244140625).ToString("F2"), (FormatConverter.FormatList[0].A).ToString("F2"));
	//	}

	//}

}
