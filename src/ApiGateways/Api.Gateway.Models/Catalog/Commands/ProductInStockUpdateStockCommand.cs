namespace Catalog.Service.EventHandlers.Commands;

public class ProductInStockUpdateStockCommand
{
    public IEnumerable<ProductInStockUpdateStockCommandItem> Items { get; set; } = new List<ProductInStockUpdateStockCommandItem>();
}
