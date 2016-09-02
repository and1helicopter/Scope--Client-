﻿using System;
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
        
        
                    
        public static ushort TimeStampAddr;
        public static ushort OscilStatusAddr;
        public static ushort ScopeCountAddr;
        public static ushort HistoryAddr;
        public static ushort ChannelCountAddr;
        public static ushort StartTemptAddr;
        public static ushort OscilFreqAddr;
        public static ushort OscilLoadAddr;
        public static ushort FlagNeedAddr;
        public static ushort NewConfigAddr;

        public static ushort OscilCount;
        public static ushort ChannelCount;
        public static ushort HystoryCount; 
        public static ushort FrequncyCount;
        public static ushort OscilAllSize;
        public static ushort OscilNominalFrequency;
        public static ushort OscilSampleRate;

        static void LoadFromXML(string paramName, string WrName, XmlDocument doc, out ushort addr)
        {
            XmlNodeList xmls;
            XmlNode xmlline;

            
            xmls = doc.GetElementsByTagName(paramName);
            if (xmls.Count != 1)
            {
                addr = 0;
                throw new Exception("Ошибки в файле: "+xmlFileName+"!");
            }
            xmlline = xmls[0];

            try
            {
                addr = Convert.ToUInt16(xmlline.Attributes[WrName].Value);
            }
            catch 
            {
                addr = 0;
                throw new Exception("Ошибки в файле: "+xmlFileName+"!");
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

            LoadFromXML("TimeStamp", "Addr", doc, out TimeStampAddr);
            LoadFromXML("OscilStatus", "Addr", doc, out OscilStatusAddr);
            LoadFromXML("ScopeCount", "Addr", doc, out ScopeCountAddr);
            LoadFromXML("Hystory", "Addr", doc, out HistoryAddr);
            LoadFromXML("ChannelCount", "Addr", doc, out ChannelCountAddr);
            LoadFromXML("DataStart", "Addr", doc, out StartTemptAddr);
            LoadFromXML("OscilFreq", "Addr", doc, out OscilFreqAddr);
            LoadFromXML("LoadOscilStart", "Addr", doc, out OscilLoadAddr);
            LoadFromXML("ParamLoadConfig", "Addr", doc, out FlagNeedAddr);
            LoadFromXML("ParamLoadData", "Addr", doc, out NewConfigAddr);
           
            ushort count = 0;
            LoadFromXML("MeasureParams", "Count", doc, out count);
            LoadFromXML("Oscil", "Count", doc, out OscilCount);
            LoadFromXML("Channel", "Count", doc, out ChannelCount);
            LoadFromXML("History", "Count", doc, out HystoryCount);
            LoadFromXML("Frequency", "Count", doc, out FrequncyCount);
            LoadFromXML("OscilAllSize", "Count", doc, out OscilAllSize);
            LoadFromXML("OscilNominalFrequency", "Count", doc, out OscilNominalFrequency);
            LoadFromXML("OscilSampleRate", "Count", doc, out OscilSampleRate);
            
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
