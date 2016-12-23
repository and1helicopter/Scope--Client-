using System.Collections.Generic;
using System.Linq;

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
            set { _loadParams = value; }
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
            catch
            {
                // ignored
            }
        }

        public static bool ConnectMcu { get; set; }

        //Частота выборки без делителя
        public static ushort SampleRate { get; set; }

        //Размер всей памяти 
        public static uint OscilAllSize { get; set; }

        //Размер выборки
        public static ushort SampleSize { get; set; }

        //Количество выборок в предыстории 
        public static uint OscilHistCount { get; set; }

        //Количество осциллограмм 
        public static ushort ScopeCount { get; set; }

        //Количество каналов
        public static ushort ChannelCount { get; set; }

        //Предыстория 
        public static ushort HistoryCount { get; set; }

        //Делитель 
        public static ushort FreqCount { get; set; }

        //Режим работы 
        public static ushort OscilEnable { get; set; }

        //Размер осциллограммы 
        public static uint OscilSize { get; set; }

        //Статус осциллогрофа
        public static ushort StatusOscil { get; set; }

        public static bool Coincides { get; set; }

        //Адреса каналов 
        static List<ushort> _oscilAddr = new List<ushort>();
        public static List<ushort> OscilAddr
        {
            get { return _oscilAddr; }
            set { _oscilAddr = value; }
        }
        public static void InitOscilAddr(ushort[] loadParams)
        {
            int i;
            _oscilAddr.Clear();
            for (i = 0; i < ChannelCount; i++)
            {
                _oscilAddr.Add(loadParams[i]);
            }
        }

        //Формат каналов 
        static List<ushort> _oscilFormat = new List<ushort>();
        public static List<ushort> OscilFormat
        {
            get { return _oscilFormat; }
            set { _oscilAddr = value; }
        }
        public static void InitOscilFormat(ushort[] loadParams)
        {
            int i;
            _oscilFormat.Clear();
            for (i = 0; i < ChannelCount; i++)
            {
                _oscilFormat.Add(loadParams[i]);
            }
        }

        //Осциллографирумые параметры (получаем список параметров которые будем осциллогофировать)
        static List<int> _oscilParams = new List<int>();
        public static List<int> OscilParams
        {
            get { return _oscilParams; }
            set { _oscilParams = value; }
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
        public static void InitOscilParams(List<ushort> oscilAddr, List<ushort> oscilFormat)
        {
            int i;
            _oscilParams.Clear();
            for (i = 0; i < ChannelCount; i++)
            {
                _oscilParams.Add(FindParamIndex(oscilAddr[i], oscilFormat[i]));
            }
            Coincides = OscilParams.Distinct().Count() == ChannelCount;
        }

        //Осциллограф включен
        public static bool ScopeEnabled = true;

    }
}
