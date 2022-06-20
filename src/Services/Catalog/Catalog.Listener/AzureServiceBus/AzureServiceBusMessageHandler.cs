using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Catalog.Service.Proxy.Catalog.Commands;
using Catalog.Service.Proxy.Catalog.Contracts;
using Microsoft.Extensions.Logging;

namespace Catalog.Listener.AzureServiceBus;

public class AzureServiceBusMessageHandler : IAzureServiceBusMessageHandler
{
    private readonly ILogger<AzureServiceBusMessageHandler> _logger;
    private readonly ICatalogProxy _catalogProxy;

    public AzureServiceBusMessageHandler(ILogger<AzureServiceBusMessageHandler> logger, ICatalogProxy catalogProxy)
    {
        _logger = logger;
        _catalogProxy = catalogProxy;
    }

    public async Task MessageHandler(ProcessMessageEventArgs args)
    {
        _logger.LogInformation("Received new message from Azure Service Bus");
        Console.WriteLine("Received new message from Azure Service Bus");
        string body = args.Message.Body.ToString();

        var entity = ConvertMessageToCommand(body);

        await _catalogProxy.UpdateStockAsync(entity);

        await args.CompleteMessageAsync(args.Message);
    }
    public Task ErrorHandler(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception.ToString());
        return Task.CompletedTask;
    }

    private ProductInStockUpdateStockCommand ConvertMessageToCommand(string message)
    {
        try
        {
            var entity = JsonSerializer.Deserialize<ProductInStockUpdateStockCommand>(message);

            return entity ?? throw new Exception();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error happen during the conversion of ASB message to Api command.");
            _logger.LogDebug(ex.ToString());

            return new ProductInStockUpdateStockCommand();
        }
    }
}

