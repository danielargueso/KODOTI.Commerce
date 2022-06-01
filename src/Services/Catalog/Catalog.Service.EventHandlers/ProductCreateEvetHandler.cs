using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using MediatR;

namespace Catalog.Service.EventHandlers
{
    public class ProductCreateEvetHandler
        : INotificationHandler<ProductCreateCommand>
	{
		private readonly ApplicationDbContext _context;

        public ProductCreateEvetHandler(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price
            });

            await _context.SaveChangesAsync();
        }
    }
}

