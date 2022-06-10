using System;
using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Order.Service.Proxy.Catalog.Commands;
using Order.Service.Proxy.Catalog.Contracts;

namespace Order.Service.Proxy.Catalog
{
	public class CatalogQueueProxy : ICatalogProxy
	{
        // Code adaption of official documentation: https://docs.microsoft.com/es-es/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues#add-code-to-send-messages-to-the-queue

        private const string QUEUE_NAME = "order-stock-update";

        private readonly string _connectionString;
        private ServiceBusClient? _serviceBusClient;
        private ServiceBusSender? _serviceBusSender;

        public CatalogQueueProxy(
            IOptions<AzureServiceBus> azureServiceBusOptions
            )
		{
            _connectionString = azureServiceBusOptions.Value.ConnectionString;
        }

        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {

            if (_serviceBusClient == null || _serviceBusClient.IsClosed)
            {
                _serviceBusClient = new(_connectionString);
            }

            if (_serviceBusSender == null)
            {
                _serviceBusSender = _serviceBusClient.CreateSender(QUEUE_NAME);
            }

            string commandBody = JsonSerializer.Serialize(command);
            var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(commandBody));
             
            await _serviceBusSender.SendMessageAsync(serviceBusMessage);

            await _serviceBusSender.CloseAsync();
            await _serviceBusSender.DisposeAsync();
            await _serviceBusClient.DisposeAsync();

        }
    }
}

