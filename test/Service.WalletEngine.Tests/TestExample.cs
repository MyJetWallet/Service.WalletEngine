using System;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using NUnit.Framework;
using WalletEngine.Messages;

namespace Service.WalletEngine.Tests
{
    public class TestExample
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var message = new CashInOutRequestMessage()
            {
                AssetId = "BTC",
                Amount = "111.11",

                MessageType = MessageType.CashInOutRequest,
                OperationId = "11",
                SequenceId = 0,
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            };

            Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));
            Console.WriteLine();
            Console.WriteLine();

            var bytes = WalletMessageSerializer.Serialise(message);

            var baseMessage = WalletMessageSerializer.Deserialize(bytes);

            message = baseMessage as CashInOutRequestMessage;

            Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));

            Assert.Pass();
        }
    }
}
