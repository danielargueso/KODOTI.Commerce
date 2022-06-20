using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;

namespace Api.Gateway.Proxies.Catalog.Contracts;

public interface ICatalogProxy
{
    Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int>? products = null);
    Task<ProductDto> GetAsync(int id);
}

