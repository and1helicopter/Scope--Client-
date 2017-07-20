
namespace ScopeSetupApp
{
    public class ScopeChannelConfig
    {
        public string ChannelNames { get; set; }
        public string ChannelGroupNames { get; set; }
        public ushort ChannelTypeAd { get; set; }   
        public ushort ChannelAddrs { get; set; }
        public int ChannelformatNumeric { get; set; }  
        public int ChannelFormats { get; set; }
        public string ChannelPhase { get; set; }
        public string ChannelCcbm { get; set; } 
        public string ChannelDimension { get; set; }
        public double ChannelMin { get; set; }   
        public double ChannelMax { get; set; }
    }
}
