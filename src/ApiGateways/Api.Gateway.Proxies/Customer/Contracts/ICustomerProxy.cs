using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTOs;

namespace Api.Gateway.Proxies.Customer.Contracts;

public interface ICustomerProxy
{
    Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int>? ids = null);
    Task<ClientDto> GetAsync(int id);
}

