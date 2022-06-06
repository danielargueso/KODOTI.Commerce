using Customer.Domain;
using Customer.Persistance.Database;
using Customer.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customer.Service.EventHandlers;

public class ClientCreateEventHandler : INotificationHandler<ClientCreateCommand>
{
    private readonly CustomerDbContext _context;
    private readonly ILogger<ClientCreateEventHandler> _logger;

    public ClientCreateEventHandler(CustomerDbContext context, ILogger<ClientCreateEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(ClientCreateCommand notification, CancellationToken cancellationToken)
    {
        await _context.AddAsync(new Client
        {
            Name = notification.Name
        });

        await _context.SaveChangesAsync();
    }
}

