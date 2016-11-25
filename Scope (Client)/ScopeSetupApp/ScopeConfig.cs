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
        static ushort[] _loadParams = new ushort[32];
        public static ushort[] LoadParams 
        { 
            get { return _loadParams; } 
            set { }
        }

        public static void SetLoadParamsBlock(ushort[] newPartLoadParams, int startIndex, int paramCount)
        {
            int i;
            try
            {
                for (i = 0; i < paramCount; i++)
                {
                    _loadParams[startIndex + i] = newPartLoadParams[i];
                }
            }
            catch { }
        }

        static bool _connectMcu = false;
        public static bool ConnectMcu
        {
            get { return _connectMcu; }
            set { _connectMcu = value; }
        }

        //Частота выборки без делителя
        static ushort _sampl = 1;
        public static ushort SampleRate
        {
            get { return _sampl; }
            set { _sampl = value; }
        }

        //Размер всей памяти 
        static uint _oscilAllSize = 0;
        public static uint OscilAllSize
        {
            get { return _oscilAllSize; }
            set { _oscilAllSize = value; }
        }

        //Размер выборки
        static ushort _samplSize = 0;
        public static ushort SampleSize
        {
            get { return _samplSize; }
            set { _samplSize = value; }
        }

        //Количество выборок в предыстории 
        static uint _oscilHistCount = 0;
        public static uint OscilHistCount
        {
            get { return _oscilHistCount; }
            set { _oscilHistCount = value;  }
        }

        //Количество осциллограмм 
        static ushort _scopeCount = 1;
        public static ushort ScopeCount
        {
            get { return _scopeCount; }
            set { _scopeCount = value; }
        }

        //Количество каналов
        static ushort _channelCount = 1;
        public static ushort ChannelCount
        {
            get { return _channelCount; }
            set { _channelCount = value; }
        }
        //Предыстория 
        static ushort _historyCount = 0;
        public static ushort HistoryCount
        {
            get { return _historyCount; }
            set { _historyCount = value; }
        }
        //Делитель 
        static ushort _freqCount = 1;
        public static ushort FreqCount
        {
            get { return _freqCount; }
            set { _freqCount = value; }
        }

        //Режим работы 
        static ushort _oscilEnable = 0;
        public static ushort OscilEnable
        {
            get { return _oscilEnable; }
            set { _oscilEnable = value; }
        }

        //Размер осциллограммы 
        static uint _oscilSize = 0;
        public static uint OscilSize
        {
            get { return _oscilSize; }
            set { _oscilSize = value; }
        }

        //Адреса каналов 
        static List<ushort> _oscilAddr = new List<ushort>();
        public static List<ushort> OscilAddr
        {
            get { return _oscilAddr; }
            set{}
        }
        public static void InitOscilAddr(ushort[] loadParams)
        {
            int i;
            _oscilAddr.Clear();
            for (i = 0; i < _channelCount; i++)
            {
                _oscilAddr.Add(loadParams[i]);
            }
        }

        //Формат каналов 
        static List<ushort> _oscilFormat = new List<ushort>();
        public static List<ushort> OscilFormat
        {
            get { return _oscilFormat; }
            set { }
        }
        public static void InitOscilFormat(ushort[] loadParams)
        {
            int i;
            _oscilFormat.Clear();
            for (i = 0; i < _channelCount; i++)
            {
                _oscilFormat.Add(loadParams[i]);
            }
        }

        //Осциллографирумые параметры (получаем список параметров которые будем осциллогофировать)
        static List<int> _oscilParams = new List<int>();
        public static List<int> OscilParams
        {
            get { return _oscilParams; }
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
            _oscilParams.Clear();
            for (i = 0; i < _channelCount; i++)
            {
                _oscilParams.Add(FindParamIndex(_oscilAddr[i], oscilFormat[i]));
            }
        }

        //Осциллограф включен
        public static bool ScopeEnabled = true;
    }
}
