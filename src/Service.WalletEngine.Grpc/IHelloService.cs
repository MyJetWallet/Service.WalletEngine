using System.ServiceModel;
using System.Threading.Tasks;
using Service.WalletEngine.Grpc.Models;

namespace Service.WalletEngine.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}