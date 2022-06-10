using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Order.Service.Proxy.Catalog.Commands;
using Order.Service.Proxy.Catalog.Contracts;

namespace Order.Service.Proxy.Catalog;

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

        var request = await _httpClient.PutAsync(_apiUrls.CatalogUrl + "v1/stocks", content);
        request.EnsureSuccessStatusCode();
    }
}

