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
        public static string XmlFileName = "ScopeSysType.xml";
        public static string XmlFileNameOscil = "ScopeSysType.xml";
        public static List<string> GroupNames = new List<string>();
        public static List<string> ChannelNames = new List<string>();
        public static List<string> ChannelPhase = new List<string>();
        public static List<string> ChannelCcbm = new List<string>();
        public static List<string> ChannelDimension = new List<string>();
        public static List<ushort> ChannelAddrs = new List<ushort>();
        public static List<ushort> ChannelFormats = new List<ushort>();
        public static List<ushort> ChannelTypeAd = new List<ushort>();
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
        public static string TmqCode;
        public static string Leapsec;
        //For OscilConfigurator
        public static List<string> OscilChannelNames = new List<string>();
        public static List<ushort> OscilChannelAddrs = new List<ushort>();
        public static List<ushort> OscilChannelFormats = new List<ushort>();

        static void LoadFromXml(string paramName, string wrName, XmlDocument doc, out ushort addr)
        {
            XmlNodeList xmls;
            XmlNode xmlline;
            
            xmls = doc.GetElementsByTagName(paramName);

            xmlline = xmls[0];

            try
            {
                addr = Convert.ToUInt16(xmlline.Attributes[wrName].Value);
            }
            catch 
            {
                addr = 0;
            }
        }

        static void LoadNameFromXml(string paramName, XmlDocument doc, out string str)
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
                doc.Load(XmlFileName);
            }
            catch
            {
                throw new Exception("Не удалось открыть файл: " + XmlFileName + "!");
            }


            LoadFromXml("Configuration", "Addr", doc, out ConfigurationAddr);
            LoadFromXml("OscilCmnd", "Addr", doc, out OscilCmndAddr);

            ushort count = 0;
            LoadFromXml("OscilAllSize", "Count", doc, out OscilAllSize);
            LoadFromXml("MeasureParams", "Count", doc, out count);

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
            LoadNameFromXml("tmqCode", doc, out TmqCode);
            LoadNameFromXml("leapsec", doc, out Leapsec);
            
            ChannelNames = new List<string>();
            GroupNames = new List<string>();
            ChannelPhase = new List<string>();
            ChannelCcbm = new List<string>();
            ChannelDimension = new List<string>();
            ChannelAddrs = new List<ushort>();
            ChannelFormats = new List<ushort>();
            ChannelTypeAd = new List<ushort>();
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

                    string[] str = (Convert.ToString(xmlline.Attributes["Name"].Value)).Split(new char[] { '/' });
                    string str1 = "", str2 = "";
                    if (str.Length > 1) { str1 = str[1]; str2 = str[0]; }
                    else str1 = str[0];

                    ChannelNames.Add(str1);
                    GroupNames.Add(str2);
                    ChannelPhase.Add(Convert.ToString(xmlline.Attributes["Phase"].Value));
                    ChannelCcbm.Add(Convert.ToString(xmlline.Attributes["CCBM"].Value));
                    ChannelDimension.Add(Convert.ToString(xmlline.Attributes["Dimension"].Value));
                    ChannelAddrs.Add(Convert.ToUInt16(xmlline.Attributes["Addr"].Value));
                    ChannelFormats.Add(Convert.ToUInt16(xmlline.Attributes["Format"].Value));
                    ChannelTypeAd.Add(Convert.ToUInt16(xmlline.Attributes["TypeAD"].Value));
                    ChannelMin.Add(Convert.ToInt32(xmlline.Attributes["Min"].Value));
                    ChannelMax.Add(Convert.ToInt32(xmlline.Attributes["Max"].Value));
                }
                catch
                {
                   throw new Exception("Ошибки в файле: " + XmlFileName + "!");
                }
            } 
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
                throw new Exception("Не удалось открыть файл: " + XmlFileNameOscil + "!");
            }

            LoadFromXml("Oscil", "Count", doc, out OscilCount);
            LoadFromXml("Channel", "Count", doc, out ChannelCount);
            LoadFromXml("Story", "Count", doc, out HistoryCount);
            LoadFromXml("Frequency", "Count", doc, out FrequncyCount);
            LoadFromXml("OscilEnable", "Count", doc, out OscilEnable);

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
                    throw new Exception("Ошибки в файле: " + XmlFileNameOscil + "!");
                }
            }
        }
    }
}
