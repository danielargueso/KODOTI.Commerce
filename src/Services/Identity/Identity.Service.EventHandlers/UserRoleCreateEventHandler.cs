using Identity.Domain;
using Identity.Persistence.Database;
using Identity.Service.EventHandlers.Commands;
using MediatR;

namespace Identity.Service.EventHandlers;

public class UserRoleCreateEventHandler
	: INotificationHandler<UserRoleCreateCommand>
{
    private readonly IdentityServiceDbContext _context;

    public UserRoleCreateEventHandler(IdentityServiceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UserRoleCreateCommand notification, CancellationToken cancellationToken)
    {
        _ = await _context.AddAsync(new ApplicationUserRole
        {
            RoleId = notification.RoleId,
            UserId = notification.UserId
        }, cancellationToken);

        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}

