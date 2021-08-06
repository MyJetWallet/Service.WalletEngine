using System.Threading;
using System.Threading.Tasks;
using MyServiceBus.TcpClient;
using WalletEngine.Messages;
using WalletEngine.Messages.Tools;

namespace Service.WalletEngine.Subscriber
{
    public class EventPublisher : IWalletTopicPublisher
    {
        private readonly MyServiceBusTcpClient _client;
        private readonly string _topic;
        private readonly bool _immediatelyPersist;

        private ulong _lastSequenceId = 0;

        public EventPublisher(MyServiceBusTcpClient client)
        {
            _topic = $"{Program.Settings.TopicId}-output";
            _client = client.CreateTopicIfNotExists(_topic);
            _immediatelyPersist = true;
        }

        public void SetLastSequenceId(ulong sequenceId)
        {
            _lastSequenceId = sequenceId;
        }

        public Task Publish(IWalletMessage message)
        {
            var seqId = Interlocked.Increment(ref _lastSequenceId);

            message.SequenceId = seqId;

            var data = WalletMessageSerializer.Serialise(message);
            return _client.PublishAsync(_topic, data, _immediatelyPersist);
        }
    }
}