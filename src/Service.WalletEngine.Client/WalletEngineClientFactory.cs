using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.WalletEngine.Grpc;

namespace Service.WalletEngine.Client
{
    [UsedImplicitly]
    public class WalletEngineClientFactory: MyGrpcClientFactory
    {
        public WalletEngineClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
