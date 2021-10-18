using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using Service.WalletEngine.Subscriber;

namespace Service.WalletEngine
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly ServiceBusLifeTime _serviceBusLifeTime;
        private readonly EventPublisher _publisher;

        public ApplicationLifetimeManager(
            IHostApplicationLifetime appLifetime,
            ILogger<ApplicationLifetimeManager> logger,
            ServiceBusLifeTime serviceBusLifeTime,
            EventPublisher publisher)
            : base(appLifetime)
        {
            _logger = logger;
            _serviceBusLifeTime = serviceBusLifeTime;
            _publisher = publisher;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called");
            _publisher.SetLastSequenceId(0);
            _serviceBusLifeTime.Start();
            _logger.LogInformation("ServiceBusLifeTime is started");
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called");
            _serviceBusLifeTime.Stop();
            _logger.LogInformation("ServiceBusLifeTime is stop");
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called");
        }
    }
}