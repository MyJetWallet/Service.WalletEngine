using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MyJetWallet.Sdk.Service;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;

namespace WalletEngine.Messages.Tools
{
    public abstract class WalletTopicSubscriberBatch
    {
        private MessageType[] _filterList = new MessageType[0];

        public WalletTopicSubscriberBatch(MyServiceBusTcpClient client, string topic, string queueName, TopicQueueType type)
        {
            client.Subscribe(topic, queueName, type, ReadMessage);
        }

        private ValueTask ReadMessage(IConfirmationContext context, IReadOnlyList<IMyServiceBusMessage> messages)
        {
            var list = messages
                .Select(e => WalletMessageSerializer.Deserialize(e.Data.ToArray()))
                .Where(e => _filterList.Length == 0 || _filterList.Contains(e.MessageType))
                .ToList();

            if (list.Count == 0)
                return new ValueTask();

            using var activity = MyTelemetry.StartActivity("batch of wallet messages", ActivityKind.Consumer);
            activity?.AddTag("count", list.Count);

            activity?.AddTag("message-attemptNo", messages.First().AttemptNo);
            activity?.AddTag("message-no-min", messages.Min(e => e.Id));
            activity?.AddTag("message-no-max", messages.Max(e => e.Id));

            activity?.AddTag("message-SequenceId-min", list.Min(e => e.SequenceId));
            activity?.AddTag("message-SequenceId-min", list.Max(e => e.SequenceId));

            return HandleBatch(list);
        }

        protected abstract ValueTask HandleBatch(List<IWalletMessage> messages);

        protected void SetFilterMessage(params MessageType[] types)
        {
            _filterList = types;
        }

        protected abstract ValueTask HandleCashInOutConformation(CashInOutConformationMessage message);

        protected abstract ValueTask HandleCashInOutRequest(CashInOutRequestMessage message);
    }
}