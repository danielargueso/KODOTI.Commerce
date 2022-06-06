using Customer.Domain;
using Customer.Persistance.Database;
using Customer.Service.EventHandlers.Commands;
using MediatR;

namespace Customer.Service.EventHandlers;

public class ClientCreateEventHandler : INotificationHandler<ClientCreateCommand>
{
    private readonly CustomerDbContext _context;

    public ClientCreateEventHandler(CustomerDbContext context)
    {
        _context = context;
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

