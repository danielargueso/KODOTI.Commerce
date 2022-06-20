using Api.Gateway.Models.Catalog.Common;

namespace Catalog.Service.EventHandlers.Commands;

public class ProductInStockUpdateStockCommandItem
{
    public int ProductId { get; set; } = 0;
    public int Stock { get; set; } = 0;
    public ProductInStockAction Action { get; set; } = ProductInStockAction.Substract;
}
