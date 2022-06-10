using Azure.Messaging.ServiceBus;

namespace Catalog.Listener.AzureServiceBus;

public interface IAzureServiceBusMessageHandler
{
    Task MessageHandler(ProcessMessageEventArgs args);
    Task ErrorHandler(ProcessErrorEventArgs args);
}

