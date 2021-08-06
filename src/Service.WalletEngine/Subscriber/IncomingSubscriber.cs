using System;
using System.Threading.Tasks;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Newtonsoft.Json;
using WalletEngine.Messages;
using WalletEngine.Messages.Tools;

namespace Service.WalletEngine.Subscriber
{
    public class IncomingSubscriber: WalletTopicSubscriberSinge
    {
        private readonly IWalletTopicPublisher _publisher;

        public IncomingSubscriber(MyServiceBusTcpClient client, IWalletTopicPublisher publisher) 
            : base(client, $"{Program.Settings.TopicId}-input", "WalletEngine", TopicQueueType.PermanentWithSingleConnection)
        {
            client.CreateTopicIfNotExists($"{Program.Settings.TopicId}-input");
            _publisher = publisher;
        }

        protected override ValueTask HandleCashInOutConformation(CashInOutConformationMessage message)
        {
            return ValueTask.CompletedTask;
        }

        protected override async ValueTask HandleCashInOutRequest(CashInOutRequestMessage message)
        {
            Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));


            var wallet = new WalletIdentity()
            {
                WalletId = message.WalletId
            };

            var confirm = new CashInOutConformationMessage(message)
            {
                Balances =
                {
                    new WalletBalance()
                    {
                        Asset = "BTC",
                        Balance = "10",
                        Reserve = "0",
                        WalletId = wallet
                    },

                    new WalletBalance()
                    {
                        Asset = "USD",
                        Balance = "10000",
                        Reserve = "10",
                        WalletId = wallet
                    }
                },

                WalletId = wallet,
                Amount = message.Amount,
                AssetId = message.AssetId,
                Fee = message.Fee
            };

            await _publisher.Publish(confirm);
        }
    }
}