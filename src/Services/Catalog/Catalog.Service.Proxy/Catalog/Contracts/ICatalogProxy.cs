using Catalog.Service.Proxy.Catalog.Commands;

namespace Catalog.Service.Proxy.Catalog.Contracts;

public interface ICatalogProxy
{
    Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
}

