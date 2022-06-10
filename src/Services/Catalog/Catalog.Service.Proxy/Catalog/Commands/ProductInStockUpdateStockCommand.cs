namespace Catalog.Service.Proxy.Catalog.Commands;

public class ProductInStockUpdateStockCommand
{
    public IEnumerable<ProductInStockUpdateStockCommandItem> Items { get; set; } = new List<ProductInStockUpdateStockCommandItem>();
}

