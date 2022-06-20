using System.Text.Json;
using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.Proxies.Catalog.Contracts;
using Api.Gateway.Proxies.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Api.Gateway.Proxies;

public class CatalogProxy : ICatalogProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;
    public CatalogProxy(
        IOptions<ApiUrls> apiUrls,
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor
        )
    {
        httpClient.AddBearerToken(httpContextAccessor);

        _apiUrls = apiUrls.Value;
        _httpClient = httpClient;
    }

    public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int>? products = null)
    {
        var ids = string.Join(',', products ?? new List<int>());

        var requestUrl = string.Concat(
            _apiUrls.CatalogUrl,
            ApiVersion.V1,
            EndPoint.Products,
            ApiPagination.GetPagination(page, take, ids)
            );

        var request = await _httpClient.GetAsync(requestUrl);
        request.EnsureSuccessStatusCode();

        var responseContent = await request.Content.ReadAsStringAsync();

        if (responseContent == null)
        {
            return new();
        }

        var results = JsonSerializer.Deserialize<DataCollection<ProductDto>>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return results ?? new();
    }

    public async Task<ProductDto> GetAsync(int id)
    {
        var requestUrl = string.Concat(
            _apiUrls.CatalogUrl,
            ApiVersion.V1,
            EndPoint.Products,
            $"{id}"
            );
        var request = await _httpClient.GetAsync(requestUrl);
        request.EnsureSuccessStatusCode();

        var responseContent = await request.Content.ReadAsStringAsync();

        if (responseContent == null)
        {
            return new();
        }

        var result = JsonSerializer.Deserialize<ProductDto>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return result ?? new();
    }
}

