using System;
using Microsoft.Extensions.Hosting;
using Catalog.Service.Proxy;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Catalog.Listener.AzureServiceBus
{
	public class AzureServiceBusListenerHostedService : IHostedService
	{
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<AzureServiceBusListenerHostedService> _logger;
        private readonly IAzureServiceBusMessageHandler _azureServiceBusMessageHandler;

        private string _connectionString = "Endpoint=sb://argaradev-test.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cYvTe1i5g3OMns/GpDKVMuIy+S8TbXUm59S5dgLHXsI=";
        private string _queueName = "order-stock-update";
        private ServiceBusClient _client;
        private ServiceBusProcessor _processor;

        public AzureServiceBusListenerHostedService(
            IHostApplicationLifetime hostApplicationLifetime,
            IAzureServiceBusMessageHandler azureServiceBusMessageHandler,
            ILogger<AzureServiceBusListenerHostedService> logger
            )
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _azureServiceBusMessageHandler = azureServiceBusMessageHandler;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _hostApplicationLifetime.ApplicationStopped.Register(OnStopped);

            _logger.LogInformation("Registering Azure Service Bus Listener");
            _client = new ServiceBusClient(_connectionString);
            _processor = _client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
            _processor.ProcessMessageAsync += _azureServiceBusMessageHandler.MessageHandler;
            _processor.ProcessErrorAsync += _azureServiceBusMessageHandler.ErrorHandler;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        private async void OnStarted()
        {
            await _processor.StartProcessingAsync();
            _logger.LogInformation($"Started listening Azure Service Bus in queue '{_queueName}' sucessfully");
        }

        private async void OnStopping()
        {
            _logger.LogInformation("Stoping Azure Service Bus listener");
            await _processor.StopProcessingAsync();
            if (_processor != null && !_processor.IsClosed)
            {
                await _processor.DisposeAsync();
            }

            if (_client != null && !_processor.IsClosed)
            {
                await _client.DisposeAsync();
            }
        }

        private void OnStopped()
        {
            _logger.LogInformation("Stopped Azure Service Bus listener successfully");
        }

    }
}

