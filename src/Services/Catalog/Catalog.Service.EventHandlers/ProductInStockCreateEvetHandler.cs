using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using MediatR;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockCreateEvetHandler
        : INotificationHandler<ProductInStockCreateCommand>
    {
        private readonly ApplicationDbContext _context;

        public ProductInStockCreateEvetHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(ProductInStockCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new ProductInStock
            {
                ProductId = command.ProductId,
                Stock = command.Stock
            });

            await _context.SaveChangesAsync();
        }
    }
}
