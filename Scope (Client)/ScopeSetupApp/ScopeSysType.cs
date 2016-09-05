using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;

namespace ScopeSetupApp
{
    public static class ScopeSysType
    {
        public static string xmlFileName = "ScopeSysType.xml";
        public static List<string> ChannelNames = new List<string>();
        public static List<string> ChannelDimension = new List<string>();
        public static List<ushort> ChannelAddrs = new List<ushort>();
        public static List<Color> ChannelColors = new List<Color>();
        public static List<ushort> ChannelFormats = new List<ushort>();
        public static List<string> ChannelFormatsName = new List<string>();
        public static List<int> ChannelStepLines = new List<int>();
        public static List<int> ChannelTypeAD = new List<int>();
        public static List<int> ChannelMin = new List<int>();
        public static List<int> ChannelMax = new List<int>();
        public static List<bool> ChannelChecked = new List<bool>();

        public static ushort ScopeCountAddr;
        public static ushort ChannelCountAddr;
        public static ushort HistoryAddr;
        public static ushort OscilFreqAddr;
        public static ushort OscilStatusAddr;
        public static ushort StartTemptAddr;
        public static ushort OscilLoadAddr;
        public static ushort NewConfigAddr;
        public static ushort FlagNeedAddr;
        public static ushort TimeStampAddr;
        public static ushort OscilAllSize;
          
        public static ushort OscilCount;
        public static ushort ChannelCount;
        public static ushort HistoryCount;
        public static ushort FrequncyCount;
        //Cometrade format
        public static string StationName;
        public static string RecordingDevice;
        public static ushort OscilNominalFrequency;
        public static ushort OscilSampleRate;
        //For rev. 2013
        public static string TimeCode;
        public static string LocalCode;
        public static string tmqCode;
        public static string leapsec;

        static void LoadFromXML(string paramName, string WrName, XmlDocument doc, out ushort addr)
        {
            XmlNodeList xmls;
            XmlNode xmlline;

            
            xmls = doc.GetElementsByTagName(paramName);
            /*if (xmls.Count != 1)
            {
                addr = 0;
                throw new Exception("Ошибки в файле: "+xmlFileName+"!");
            }*/
            xmlline = xmls[0];

            try
            {
                addr = Convert.ToUInt16(xmlline.Attributes[WrName].Value);
            }
            catch 
            {
                addr = 0;
               // throw new Exception("Ошибки в файле: "+xmlFileName+"!");
            }
        }

        static void LoadNameFromXML(string paramName, XmlDocument doc, out string str)
        {
            XmlNodeList xmls;
            XmlNode xmlline;

            xmls = doc.GetElementsByTagName(paramName);
            xmlline = xmls[0];
            try
            {
                str = Convert.ToString(xmlline.Attributes["xmlns"].Value);
            }
            catch 
            {
                str = "";
            }
            
        }
       
        public static void InitScopeSysType()
        {
            var doc = new XmlDocument();
            try
            {
                doc.Load(xmlFileName);
            }
            catch
            {
                throw new Exception("Не удалось открыть файл: " + xmlFileName + "!");
            }

            LoadFromXML("ScopeCount", "Addr", doc, out ScopeCountAddr);
            LoadFromXML("ChannelCount", "Addr", doc, out ChannelCountAddr);
            LoadFromXML("History", "Addr", doc, out HistoryAddr);
            LoadFromXML("OscilFreq", "Addr", doc, out OscilFreqAddr);
            LoadFromXML("OscilStatus", "Addr", doc, out OscilStatusAddr);
            LoadFromXML("StartTemp", "Addr", doc, out StartTemptAddr);
            LoadFromXML("LoadOscilStart", "Addr", doc, out OscilLoadAddr);
            LoadFromXML("NewConfig", "Addr", doc, out NewConfigAddr);
            LoadFromXML("FlagNeed", "Addr", doc, out FlagNeedAddr);
            LoadFromXML("TimeStamp", "Addr", doc, out TimeStampAddr);
            ushort count = 0;
            LoadFromXML("OscilAllSize", "Count", doc, out OscilAllSize);
            LoadFromXML("MeasureParams", "Count", doc, out count);

            LoadFromXML("Oscil", "Count", doc, out OscilCount);
            LoadFromXML("Channel", "Count", doc, out ChannelCount);
            LoadFromXML("Story", "Count", doc, out HistoryCount);
            LoadFromXML("Frequency", "Count", doc, out FrequncyCount);
            //Cometrade format
            LoadNameFromXML("StationName", doc, out StationName);
            LoadNameFromXML("RecordingDevice", doc, out RecordingDevice);
            LoadFromXML("OscilNominalFrequency", "Count", doc, out OscilNominalFrequency);
            LoadFromXML("OscilSampleRate", "Count", doc, out OscilSampleRate);
            //For rev. 2013
            LoadNameFromXML("TimeCode", doc, out TimeCode);
            LoadNameFromXML("LocalCode", doc, out LocalCode);
            LoadNameFromXML("tmqCode", doc, out tmqCode);
            LoadNameFromXML("leapsec", doc, out leapsec);
            
            ChannelNames = new List<string>();
            ChannelDimension = new List<string>();
            ChannelAddrs = new List<ushort>();
            ChannelColors = new List<Color>();
            ChannelFormats = new List<ushort>();
            ChannelFormatsName = new List<string>();
            ChannelStepLines = new List<int>();
            ChannelTypeAD = new List<int>();
            ChannelMin = new List<int>();
            ChannelMax = new List<int>();
            ChannelChecked = new List<bool>();

            XmlNodeList xmls;
            XmlNode xmlline;

            for (int i = 1; i < (count + 1); i++)
            {
                xmls = doc.GetElementsByTagName("MeasureParam"+i.ToString());
                if (xmls.Count != 1)
                {
                    throw new Exception("Ошибки в файле: " + xmlFileName + "!");
                }
                xmlline = xmls[0];

                try
                {
                    ChannelNames.Add(Convert.ToString(xmlline.Attributes["Name"].Value));
                    ChannelDimension.Add(Convert.ToString(xmlline.Attributes["Dimension"].Value));
                    ChannelAddrs.Add(Convert.ToUInt16(xmlline.Attributes["Addr"].Value));
                    int ic = Convert.ToInt32(xmlline.Attributes["Color"].Value);
                    ChannelColors.Add(Color.FromArgb(ic));
                    ChannelFormats.Add(Convert.ToUInt16(xmlline.Attributes["Format"].Value));
                    ChannelFormatsName.Add(Convert.ToString(xmlline.Attributes["FormatName"].Value));
                    ChannelStepLines.Add(Convert.ToInt32(xmlline.Attributes["StepLine"].Value));
                    ChannelTypeAD.Add(Convert.ToInt32(xmlline.Attributes["TypeAD"].Value));
                    ChannelMin.Add(Convert.ToInt32(xmlline.Attributes["Min"].Value));
                    ChannelMax.Add(Convert.ToInt32(xmlline.Attributes["Max"].Value));
                    ChannelChecked.Add(Convert.ToBoolean(xmlline.Attributes["Checked"].Value));
                }
                catch
                {
                    throw new Exception("Ошибки в файле: " + xmlFileName + "!");
                }
            }

            
            

            
        }
    }
}
