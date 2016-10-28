using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScopeSetupApp
{
    public static class ScopeConfig
    {
        //Было сделано изменение конфигурации
        static public bool ChangeScopeConfig = false;

        //Скаченные параметры
        static ushort[] loadParams = new ushort[32];
        public static ushort[] LoadParams 
        { 
            get { return loadParams; } 
            set { }
        }

        public static void SetLoadParamsBlock(ushort[] newPartLoadParams, int startIndex, int paramCount)
        {
            int i;
            try
            {
                for (i = 0; i < paramCount; i++)
                {
                    loadParams[startIndex + i] = newPartLoadParams[i];
                }
            }
            catch { }
        }

        static bool connectMCU = false;
        public static bool ConnectMCU
        {
            get { return connectMCU; }
            set { connectMCU = value; }
        }

        //Частота выборки без делителя
        static ushort sampl = 1;
        public static ushort SampleRate
        {
            get { return sampl; }
            set { sampl = value; }
        }

        //Размер всей памяти 
        static uint oscilAllSize = 0;
        public static uint OscilAllSize
        {
            get { return oscilAllSize; }
            set { oscilAllSize = value; }
        }

        //Размер выборки
        static ushort samplSize = 0;
        public static ushort SampleSize
        {
            get { return samplSize; }
            set { samplSize = value; }
        }

        //Количество выборок в предыстории 
        static uint oscilHistCount = 0;
        public static uint OscilHistCount
        {
            get { return oscilHistCount; }
            set { oscilHistCount = value;  }
        }

        //Количество осциллограмм 
        static ushort scopeCount = 1;
        public static ushort ScopeCount
        {
            get { return scopeCount; }
            set { scopeCount = value; }
        }

        //Количество каналов
        static ushort channelCount = 1;
        public static ushort ChannelCount
        {
            get { return channelCount; }
            set { channelCount = value; }
        }
        //Предыстория 
        static ushort historyCount = 0;
        public static ushort HistoryCount
        {
            get { return historyCount; }
            set { historyCount = value; }
        }
        //Делитель 
        static ushort freqCount = 1;
        public static ushort FreqCount
        {
            get { return freqCount; }
            set { freqCount = value; }
        }

        //Режим работы 
        static ushort oscilEnable = 0;
        public static ushort OscilEnable
        {
            get { return oscilEnable; }
            set { oscilEnable = value; }
        }

        //Размер осциллограммы 
        static uint oscilSize = 0;
        public static uint OscilSize
        {
            get { return oscilSize; }
            set { oscilSize = value; }
        }

        //Адреса каналов 
        static List<ushort> oscilAddr = new List<ushort>();
        public static List<ushort> OscilAddr
        {
            get { return oscilAddr; }
            set{}
        }
        public static void InitOscilAddr(ushort[] loadParams)
        {
            int i;
            oscilAddr.Clear();
            for (i = 0; i < channelCount; i++)
            {
                oscilAddr.Add(loadParams[i]);
            }
        }

        //Формат каналов 
        static List<ushort> oscilFormat = new List<ushort>();
        public static List<ushort> OscilFormat
        {
            get { return oscilFormat; }
            set { }
        }
        public static void InitOscilFormat(ushort[] loadParams)
        {
            int i;
            oscilFormat.Clear();
            for (i = 0; i < channelCount; i++)
            {
                oscilFormat.Add(loadParams[i]);
            }
        }

        //Осциллографирумые параметры (получаем список параметров которые будем осциллогофировать)
        static List<int> oscilParams = new List<int>();
        public static List<int> OscilParams
        {
            get { return oscilParams; }
            set {}
        }
        //Проверка по адресу и формату 
        public static int FindParamIndex(ushort paramAddr, ushort paramFormat) 
        {
            int i = 0;
            while ((i < ScopeSysType.ChannelAddrs.Count) && ((paramAddr != ScopeSysType.ChannelAddrs[i]) || (paramFormat != ScopeSysType.ChannelFormats[i])))
            {
                i++;
            }

            return i;
        }
        public static void InitOscilParams(List<ushort> OscilAddr, List<ushort> oscilFormat)
        {
            int i;
            oscilParams.Clear();
            for (i = 0; i < channelCount; i++)
            {
                oscilParams.Add(FindParamIndex(oscilAddr[i], oscilFormat[i]));
            }
        }

        //Осциллограф включен
        public static bool ScopeEnabled = true;
    }
}
