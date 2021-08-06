using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.WalletEngine.Settings
{
    public class SettingsModel
    {
        [YamlProperty("WalletEngine.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("WalletEngine.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("WalletEngine.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }


        [YamlProperty("WalletEngine.ServiceBusHostPort")]
        public string ServiceBusHostPort { get; set; }

        [YamlProperty("WalletEngine.TopicId")]
        public string TopicId { get; set; }
        
    }
}
