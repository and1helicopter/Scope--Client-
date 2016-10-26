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
        public static List<string> ChannelPhase = new List<string>();
        public static List<string> ChannelCCBM = new List<string>();
        public static List<string> ChannelDimension = new List<string>();
        public static List<ushort> ChannelAddrs = new List<ushort>();
        public static List<Color> ChannelColors = new List<Color>();
        public static List<ushort> ChannelFormats = new List<ushort>();
        public static List<int> ChannelStepLines = new List<int>();
        public static List<ushort> ChannelTypeAD = new List<ushort>();
        public static List<int> ChannelMin = new List<int>();
        public static List<int> ChannelMax = new List<int>();

        public static ushort ConfigurationAddr;
        public static ushort OscilCmndAddr; 

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
        public static string tmqCode;
        public static string leapsec;
        //For OscilConfigurator
        public static List<string> OscilChannelNames = new List<string>();
        public static List<ushort> OscilChannelAddrs = new List<ushort>();
        public static List<ushort> OscilChannelFormats = new List<ushort>();

        static void LoadFromXML(string paramName, string WrName, XmlDocument doc, out ushort addr)
        {
            XmlNodeList xmls;
            XmlNode xmlline;
            
            xmls = doc.GetElementsByTagName(paramName);

            xmlline = xmls[0];

            try
            {
                addr = Convert.ToUInt16(xmlline.Attributes[WrName].Value);
            }
            catch 
            {
                addr = 0;
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


            LoadFromXML("Configuration", "Addr", doc, out ConfigurationAddr);
            LoadFromXML("OscilCmnd", "Addr", doc, out OscilCmndAddr);

            ushort count = 0;
            LoadFromXML("OscilAllSize", "Count", doc, out OscilAllSize);
            LoadFromXML("MeasureParams", "Count", doc, out count);

            LoadFromXML("Oscil", "Count", doc, out OscilCount);
            LoadFromXML("Channel", "Count", doc, out ChannelCount);
            LoadFromXML("Story", "Count", doc, out HistoryCount);
            LoadFromXML("Frequency", "Count", doc, out FrequncyCount);
            LoadFromXML("OscilSampleRate", "Count", doc, out OscilSampleRate);
            LoadNameFromXML("Comment", doc, out OscilComment);
            //Cometrade format
            LoadNameFromXML("StationName", doc, out StationName);
            LoadNameFromXML("RecordingDevice", doc, out RecordingDevice);
            LoadFromXML("OscilNominalFrequency", "Count", doc, out OscilNominalFrequency);
            //For rev. 2013
            LoadNameFromXML("TimeCode", doc, out TimeCode);
            LoadNameFromXML("LocalCode", doc, out LocalCode);
            LoadNameFromXML("tmqCode", doc, out tmqCode);
            LoadNameFromXML("leapsec", doc, out leapsec);
            
            ChannelNames = new List<string>();
            ChannelPhase = new List<string>();
            ChannelCCBM = new List<string>();
            ChannelDimension = new List<string>();
            ChannelAddrs = new List<ushort>();
            ChannelColors = new List<Color>();
            ChannelFormats = new List<ushort>();
            ChannelStepLines = new List<int>();
            ChannelTypeAD = new List<ushort>();
            ChannelMin = new List<int>();
            ChannelMax = new List<int>();

            XmlNodeList xmls;
            XmlNode xmlline;

            for (int i = 1; i < (count + 1); i++)
            {
                xmls = doc.GetElementsByTagName("MeasureParam"+i.ToString());
                xmlline = xmls[0];

                try
                {
                    ChannelNames.Add(Convert.ToString(xmlline.Attributes["Name"].Value));
                    ChannelPhase.Add(Convert.ToString(xmlline.Attributes["Phase"].Value));
                    ChannelCCBM.Add(Convert.ToString(xmlline.Attributes["CCBM"].Value));
                    ChannelDimension.Add(Convert.ToString(xmlline.Attributes["Dimension"].Value));
                    ChannelAddrs.Add(Convert.ToUInt16(xmlline.Attributes["Addr"].Value));
                    int ic = Convert.ToInt32(xmlline.Attributes["Color"].Value);
                    ChannelColors.Add(Color.FromArgb(ic));
                    ChannelFormats.Add(Convert.ToUInt16(xmlline.Attributes["Format"].Value));
                    ChannelStepLines.Add(Convert.ToInt32(xmlline.Attributes["StepLine"].Value));
                    ChannelTypeAD.Add(Convert.ToUInt16(xmlline.Attributes["TypeAD"].Value));
                    ChannelMin.Add(Convert.ToInt32(xmlline.Attributes["Min"].Value));
                    ChannelMax.Add(Convert.ToInt32(xmlline.Attributes["Max"].Value));
                }
                catch
                {
                   throw new Exception("Ошибки в файле: " + xmlFileName + "!");
                }
            } 
        }

        public static void InitScopeOscilType()
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

            LoadFromXML("Oscil", "Count", doc, out OscilCount);
            LoadFromXML("Channel", "Count", doc, out ChannelCount);
            LoadFromXML("Story", "Count", doc, out HistoryCount);
            LoadFromXML("Frequency", "Count", doc, out FrequncyCount);
            LoadFromXML("OscilEnable", "Count", doc, out OscilEnable);

            OscilChannelNames = new List<string>();
            OscilChannelAddrs = new List<ushort>();
            OscilChannelFormats = new List<ushort>();

            XmlNodeList xmls;
            XmlNode xmlline;

            for (int i = 1; i < ChannelCount + 1; i++)
            {
                xmls = doc.GetElementsByTagName("MeasureParam" + i.ToString());
                xmlline = xmls[0];

                try
                {
                    OscilChannelNames.Add(Convert.ToString(xmlline.Attributes["Name"].Value));
                    OscilChannelAddrs.Add(Convert.ToUInt16(xmlline.Attributes["Addr"].Value));
                    OscilChannelFormats.Add(Convert.ToUInt16(xmlline.Attributes["Format"].Value));
                }
                catch
                {
                    throw new Exception("Ошибки в файле: " + xmlFileName + "!");
                }
            }
        }
    }
}
