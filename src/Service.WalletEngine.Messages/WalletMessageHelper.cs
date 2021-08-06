namespace WalletEngine.Messages
{
    public static class WalletMessageHelper
    {
        public static void MergeFromBase(this IWalletMessage message, IWalletMessage baseMessage)
        {
            message.ActivityId = baseMessage.ActivityId;
            message.OperationId = baseMessage.OperationId;
            message.Context = baseMessage.Context;
        }
    }
}