using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyServiceBus.TcpClient;
using Service.WalletEngine.Subscriber;
using WalletEngine.Messages.Tools;

namespace Service.WalletEngine
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly MyServiceBusTcpClient _client;
        private readonly EventPublisher _publisher;

        public ApplicationLifetimeManager(IHostApplicationLifetime appLifetime, ILogger<ApplicationLifetimeManager> logger, MyServiceBusTcpClient client,
            EventPublisher publisher)
            : base(appLifetime)
        {
            _logger = logger;
            _client = client;
            _publisher = publisher;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");

            _publisher.SetLastSequenceId(0);

            _client.Start();
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
            
            _client.Stop();
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}
