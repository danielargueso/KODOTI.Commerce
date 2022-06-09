using Order.Service.Proxy.Catalog.Enums;

namespace Order.Service.Proxy.Catalog.Commands
{
    public class ProductInStockUpdateStockCommandItem
	{
		public int ProductId { get; set; }
		public int Stock { get; set; }
		public ProductInStockAction Action { get; set; }
	}
}

