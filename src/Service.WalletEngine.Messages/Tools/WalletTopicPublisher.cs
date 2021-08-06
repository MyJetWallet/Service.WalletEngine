using System.Threading;
using System.Threading.Tasks;
using MyServiceBus.TcpClient;

namespace WalletEngine.Messages.Tools
{
    public class WalletTopicPublisher: IWalletTopicPublisher
    {
        private readonly MyServiceBusTcpClient _client;
        private readonly string _topic;

        public WalletTopicPublisher(MyServiceBusTcpClient client, string topicId)
        {
            _topic = $"{topicId}-output";
            _client = client;
        }

        public Task Publish(IWalletMessage message)
        {
            message.SequenceId = 0;

            var data = WalletMessageSerializer.Serialise(message);
            return _client.PublishAsync(_topic, data, false);
        }
    }
}