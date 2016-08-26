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

        //Текущая предыстория
        static ushort history = 1;
        public static ushort History
        {
            get { return history; }
            set
            {
                history = value;
            }

        }

        //Количество осциллограмм 
        static ushort scopeCount = 1;
        public static ushort ScopeCount
        {
            get { return scopeCount; }
            set
            {
                scopeCount = value;
            }
        }

        //Количество каналов
        static ushort channelCount = 1;
        public static ushort ChannelCount
        {
            get { return channelCount; }
            set
            {
                channelCount = value;
            }
        }

        //Размер осциллограммы 
        static uint oscilSize = 1;
        public static uint OscilSize
        {
            get { return oscilSize; }
            set
            {
                oscilSize = value;
            }
        }

        //Дискретность осциллографа
        static ushort scopeFreq = 1;
        public static ushort ScopeFreq
        {
            get { return scopeFreq; }
            set
            {
                scopeFreq = value;
            }

        }

        //Осциллограф включен
        public static bool ScopeEnabled = true;

        //Осциллографирумые параметры
        static List<int> oscillParams = new List<int>();
        public static List<int> OscillParams
        {
            get { return oscillParams; }
            set
            {

            }
        }

        public static int FindParamIndex(ushort paramAddr)
        {
            int i = 0;
            while ((i < ScopeSysType.ChannelAddrs.Count) && (paramAddr != ScopeSysType.ChannelAddrs[i]))
            {
                i++;
            }


            return i;
        }
        public static void InitOscillParams(ushort[] loadParams)
        {
            int i;
            oscillParams.Clear();
            for (i = 0; i < channelCount; i++)
            {
                oscillParams.Add(FindParamIndex(loadParams[i]));
            }
        }


    }
}
