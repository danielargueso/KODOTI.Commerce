using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTOs;

namespace Api.Gateway.Proxies.Order.Contracts;

public interface IOrderProxy
{ 
    Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);
    Task<OrderDto> GetAsync(int id);
    Task CreateAsync(OrderCreateCommand command);
}

