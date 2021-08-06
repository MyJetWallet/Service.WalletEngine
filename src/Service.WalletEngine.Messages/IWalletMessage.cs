using Google.Protobuf;

namespace WalletEngine.Messages
{
    public interface IWalletMessage: IMessage
    {
        public MessageType MessageType { get; set; }
        string OperationId { get; set; }
        Google.Protobuf.WellKnownTypes.Timestamp Timestamp { get; set; }
        ulong SequenceId { get; set; }
        string Context { get; set; }
        string Source { get; set; }
        string ActivityId { get; set; }
    }
}