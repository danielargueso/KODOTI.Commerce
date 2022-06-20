using System.Text;
using System.Text.Json;
using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.Proxies.Common.Extensions;
using Api.Gateway.Proxies.Order.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Api.Gateway.Proxies.Order;

public class OrderProxy : IOrderProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    public OrderProxy(
        HttpClient httpClient,
        IOptions<ApiUrls> apiUrls,
        IHttpContextAccessor httpContextAccessor)
    {
        httpClient.AddBearerToken(httpContextAccessor);

        _httpClient = httpClient;
        _apiUrls = apiUrls.Value;
    }

    public async Task CreateAsync(OrderCreateCommand command)
    {
        var requestUrl = string.Concat(
            _apiUrls.OrderUrl,
            ApiVersion.V1,
            EndPoint.Orders
            );

        var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );

        var request = await _httpClient.PostAsync(requestUrl, content);
        request.EnsureSuccessStatusCode();
    }

    public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
    {
        var requestUrl = string.Concat(
            _apiUrls.OrderUrl,
            ApiVersion.V1,
            EndPoint.Orders,
            ApiPagination.GetPagination(page, take)
            );

        var request = await _httpClient.GetAsync(requestUrl);
        request.EnsureSuccessStatusCode();

        var responseContent = await request.Content.ReadAsStringAsync();

        if (responseContent == null)
        {
            return new();
        }
        var results = JsonSerializer.Deserialize<DataCollection<OrderDto>>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return results ?? new();
    }

    public async Task<OrderDto> GetAsync(int id)
    {
        var requestUrl = string.Concat(
            _apiUrls.OrderUrl,
            ApiVersion.V1,
            EndPoint.Orders,
            $"{id}"
            );
        var request = await _httpClient.GetAsync(requestUrl);
        request.EnsureSuccessStatusCode();

        var responseContent = await request.Content.ReadAsStringAsync();

        if (responseContent == null)
        {
            return new();
        }

        var result = JsonSerializer.Deserialize<OrderDto>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return result ?? new();
    }
}

