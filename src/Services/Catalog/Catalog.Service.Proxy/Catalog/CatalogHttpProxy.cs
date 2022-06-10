using System.Text;
using System.Text.Json;
using Catalog.Service.Proxy.Catalog.Commands;
using Catalog.Service.Proxy.Catalog.Contracts;
using Microsoft.Extensions.Options;

namespace Catalog.Service.Proxy.Catalog;

public class CatalogHttpProxy : ICatalogProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    public CatalogHttpProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
    {
        _apiUrls = apiUrls.Value;
        _httpClient = httpClient;
    }

    public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
    {
        //v1/stocks
        var content = new StringContent(
            JsonSerializer.Serialize(command),
            Encoding.UTF8,
            "application/json"
        );
        var endpointUrl = _apiUrls.CatalogUrl + ApiVersion.V1 + EndPoint.Stocks;
        var request = await _httpClient.PutAsync(endpointUrl, content);
        request.EnsureSuccessStatusCode();
    }
}
