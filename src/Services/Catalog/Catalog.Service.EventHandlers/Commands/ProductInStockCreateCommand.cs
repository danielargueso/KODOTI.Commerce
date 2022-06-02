using MediatR;

namespace Catalog.Service.EventHandlers.Commands
{
    public class ProductInStockCreateCommand : INotification
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
    }
}
