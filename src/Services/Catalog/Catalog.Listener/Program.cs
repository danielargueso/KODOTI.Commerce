using Catalog.Listener.AzureServiceBus;
using Catalog.Service.Proxy;
using Catalog.Service.Proxy.Catalog;
using Catalog.Service.Proxy.Catalog.Contracts;
using Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

//using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddProvider(
            new SyslogLoggerProvider(
                config.GetValue<string>("Papertrail:Host"),
                config.GetValue<int>("Papertrail:Port"),
                null
            )
        );
        
    })
    .ConfigureHostConfiguration(hostConfig =>
    {
        hostConfig.AddJsonFile("appsettings.json");
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<AzureServiceBusListenerHostedService>();
        services.Configure<ApiUrls>(
            opts => config.GetSection(ApiUrls.SectionName).Bind(opts)
            );

        services.AddHttpClient<ICatalogProxy, CatalogHttpProxy>();

        services.AddTransient<IAzureServiceBusMessageHandler, AzureServiceBusMessageHandler>();

    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();


/*
class Program
{
    //Code From: https://docs.microsoft.com/es-es/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues#add-the-code-to-receive-messages-from-the-queue

    // connection string to your Service Bus namespace
    static string connectionString = "Endpoint=sb://argaradev-test.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cYvTe1i5g3OMns/GpDKVMuIy+S8TbXUm59S5dgLHXsI=";

    // name of your Service Bus queue
    static string queueName = "order-stock-update";


    // the client that owns the connection and can be used to create senders and receivers
    static ServiceBusClient client;

    // the processor that reads and processes messages from the queue
    static ServiceBusProcessor processor;

    static async Task Main()
    {
        // The Service Bus client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when messages are being published or read
        // regularly.
        //

        // Create the client object that will be used to create sender and receiver objects
        client = new ServiceBusClient(connectionString); 

        // create a processor that we can use to process the messages
        processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        try
        {
            // add handler to process messages
            processor.ProcessMessageAsync += AzureServiceBusMessageHandler.MessageHandler;

            // add handler to process any errors
            processor.ProcessErrorAsync += AzureServiceBusMessageHandler.ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();

            Console.WriteLine("Wait for a minute and then press any key to end the processing");
            Console.ReadKey();

            // stop processing 
            Console.WriteLine("\nStopping the receiver...");
            await processor.StopProcessingAsync();
            Console.WriteLine("Stopped receiving messages");
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}
*/