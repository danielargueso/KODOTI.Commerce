namespace Catalog.Service.EventHandlers.Commands;

public class ProductInStockCreateCommand
{
    public int ProductId { get; set; } = 0;
    public int Stock { get; set; } = 0;
}
