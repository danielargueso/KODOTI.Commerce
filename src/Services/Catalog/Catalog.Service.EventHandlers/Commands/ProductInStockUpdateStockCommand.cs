using MediatR;

namespace Catalog.Service.EventHandlers.Commands
{
    public class ProductInStockUpdateStockCommand : INotification
    {
        public IEnumerable<ProductInStockUpdateStockCommandItem> Items { get; set; } = new List<ProductInStockUpdateStockCommandItem>();
    }
}
