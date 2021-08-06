using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MyJetWallet.Sdk.Service;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;

namespace WalletEngine.Messages.Tools
{
    public abstract class WalletTopicSubscriberSinge
    {
        private MessageType[] _filterList = new MessageType[0];

        public WalletTopicSubscriberSinge(MyServiceBusTcpClient client, string topic, string queueName, TopicQueueType type)
        {
            client.Subscribe(topic, queueName, type, ReadMessage);
        }

        protected void SetFilterMessage(params MessageType[] types)
        {
            _filterList = types;
        }

        private ValueTask ReadMessage(IMyServiceBusMessage message)
        {
            var msg = WalletMessageSerializer.Deserialize(message.Data.ToArray());

            if (_filterList.Any() && !_filterList.Contains(msg.MessageType))
                return new ValueTask();

            using var activity = MyTelemetry.Source.StartActivity($"message {msg.MessageType}", ActivityKind.Consumer, msg.ActivityId);

            activity?.AddTag("message-no", message.Id);
            activity?.AddTag("message-attemptNo", message.AttemptNo);
            activity?.AddTag("message-OperationId", msg.OperationId);
            activity?.AddTag("message-MessageType", msg.MessageType);
            activity?.AddTag("message-Source", msg.Source);
            activity?.AddTag("message-SequenceId", msg.SequenceId);

            switch (msg.MessageType)
            {
                case MessageType.CashInOutRequest: return HandleCashInOutRequest((CashInOutRequestMessage)msg);
                case MessageType.CashInOutConformation: return HandleCashInOutConformation((CashInOutConformationMessage)msg);
            }

            throw new Exception($"Cannot handle message type {msg.MessageType}");
        }

        protected abstract ValueTask HandleCashInOutConformation(CashInOutConformationMessage message);
        
        protected abstract ValueTask HandleCashInOutRequest(CashInOutRequestMessage message);
    }
}