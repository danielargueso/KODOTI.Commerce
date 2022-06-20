using System.Text.Json;
using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTOs;
using Api.Gateway.Proxies.Common.Extensions;
using Api.Gateway.Proxies.Customer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Api.Gateway.Proxies.Customer;

public class CustomerProxy : ICustomerProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    public CustomerProxy(
        HttpClient httpClient,
        IOptions<ApiUrls> apiUrls,
        IHttpContextAccessor httpContextAccessor)
    {
        httpClient.AddBearerToken(httpContextAccessor);

        _httpClient = httpClient;
        _apiUrls = apiUrls.Value;
    }

    public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int>? clients = null)
    {
        var ids = string.Join(',', clients ?? new List<int>());

        var requestUrl = string.Concat(
            _apiUrls.CustomerUrl,
            ApiVersion.V1,
            EndPoint.Clients,
            ApiPagination.GetPagination(page, take, ids)
            );

        var request = await _httpClient.GetAsync(requestUrl);
        request.EnsureSuccessStatusCode();

        var responseContent = await request.Content.ReadAsStringAsync();

        if (responseContent == null)
        {
            return new();
        }
        var results = JsonSerializer.Deserialize<DataCollection<ClientDto>>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return results ?? new();
    }

    public async Task<ClientDto> GetAsync(int id)
    {
        var requestUrl = string.Concat(
            _apiUrls.CustomerUrl,
            ApiVersion.V1,
            EndPoint.Clients,
            $"{id}"
            );
        var request = await _httpClient.GetAsync(requestUrl);
        request.EnsureSuccessStatusCode();

        var responseContent = await request.Content.ReadAsStringAsync();

        if (responseContent == null)
        {
            return new();
        }

        var result = JsonSerializer.Deserialize<ClientDto>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return result ?? new();
    }
}

