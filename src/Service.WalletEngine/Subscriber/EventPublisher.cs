using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.Sdk.ServiceBus;
using WalletEngine.Messages;
using WalletEngine.Messages.Tools;

namespace Service.WalletEngine.Subscriber
{
    public class EventPublisher : IWalletTopicPublisher
    {
        private readonly IServiceBusPublisher<byte[]> _publisher;

        private ulong _lastSequenceId = 0;

        public EventPublisher(IServiceBusPublisher<byte[]> publisher)
        {
            _publisher = publisher;
        }

        public void SetLastSequenceId(ulong sequenceId)
        {
            _lastSequenceId = sequenceId;
        }

        public async Task Publish(IWalletMessage message)
        {
            var seqId = Interlocked.Increment(ref _lastSequenceId);

            message.SequenceId = seqId;

            var data = WalletMessageSerializer.Serialise(message);
            await _publisher.PublishAsync(data);
        }
    }
}