using Autofac;
using Service.WalletEngine.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.WalletEngine.Client
{
    public static class AutofacHelper
    {
        public static void RegisterWalletEngineClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new WalletEngineClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
