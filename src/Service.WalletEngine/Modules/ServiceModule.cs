using System.Reflection.Metadata;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using DotNetCoreDecorators;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.WalletEngine.Subscriber;
using WalletEngine.Messages.Tools;

namespace Service.WalletEngine.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.ServiceBusHostPort), ApplicationEnvironment.HostName, Program.LogFactory)
                .ThrowExceptionOnPublishIfNoConnection(true);

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