using System;
using Google.Protobuf.WellKnownTypes;

namespace WalletEngine.Messages
{
    public sealed partial class CashInOutConformationMessage : IWalletMessage
    {
        public CashInOutConformationMessage(IWalletMessage baseMessage)
        {
            Timestamp = Timestamp.FromDateTime(DateTime.UtcNow);
            this.MergeFromBase(baseMessage);
        }
    }
}