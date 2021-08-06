using System;

namespace Service.WalletEngine.Domain.Models
{
    public interface IHelloMessage
    {
        string Message { get; set; }
    }


    public class WalletMessage
    {
        public string MessageType { get; set; }

        public long MessageId { get; set; }

        public WalletMessageHeader Header { get; set; }

        public string Context { get; set; }
    }

    public class WalletMessageHeader
    {
        public string OperationId { get; set; }
        public string ClientId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
