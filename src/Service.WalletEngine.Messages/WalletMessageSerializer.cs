using System;
using System.Diagnostics;
using Google.Protobuf;
using MyJetWallet.Sdk.Service;

namespace WalletEngine.Messages
{
    public static class WalletMessageSerializer
    {
        public static byte[] Serialise(IWalletMessage message)
        {
            message.Source = ApplicationEnvironment.HostName;

            var activity = Activity.Current;
            message.ActivityId = activity?.Id ?? "";


            if (message is CashInOutRequestMessage) message.MessageType = MessageType.CashInOutRequest;
            else if (message is CashInOutConformationMessage) message.MessageType = MessageType.CashInOutConformation;

            else
            {
                Console.WriteLine($"Cannot serialize message type {message.GetType().FullName}");
                throw new Exception($"Cannot serialize message type {message.GetType().FullName}");
            }

            return message.ToByteArray();
        }

        public static IWalletMessage Deserialize(byte[] bytes)
        {
            var type = OnlyMessageTypeMessage.Parser.ParseFrom(bytes);

            switch (type.MessageType)
            {
                case MessageType.CashInOutRequest: return CashInOutRequestMessage.Parser.ParseFrom(bytes);
                case MessageType.CashInOutConformation: return CashInOutConformationMessage.Parser.ParseFrom(bytes);
            }

            Console.WriteLine($"Cannot parse message type: {type.MessageType}");
            throw new Exception($"Cannot parse message type: {type.MessageType}");
        }
    }
}