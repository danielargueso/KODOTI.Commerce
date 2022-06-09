using Order.Service.Proxy.Catalog.Commands;

namespace Order.Service.Proxy.Catalog.Contracts
{
    public interface ICatalogProxy
	{
		Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
	}
}

