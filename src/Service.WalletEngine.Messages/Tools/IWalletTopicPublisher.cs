using System.Threading.Tasks;

namespace WalletEngine.Messages.Tools
{
    public interface IWalletTopicPublisher
    {
        Task Publish(IWalletMessage message);
    }
}