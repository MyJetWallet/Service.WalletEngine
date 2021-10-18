using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using Service.WalletEngine.Subscriber;
using WalletEngine.Messages.Tools;

namespace Service.WalletEngine.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(
                Program.ReloadedSettings(e => e.ServiceBusHostPort),
                Program.LogFactory);

            builder
                .RegisterMyServiceBusPublisher<byte[]>(serviceBusClient,
                    $"{Program.Settings.TopicId}-output", true);

            builder
                .RegisterType<EventPublisher>()
                .As<IWalletTopicPublisher>()
                .AsSelf()
                .SingleInstance()
                .AutoActivate();

            builder
                .RegisterType<IncomingSubscriber>()
                .SingleInstance()
                .AutoActivate();
        }
    }
}