using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTOs;
using Api.Gateway.Proxies.Customer.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Gateway.WebClient.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("v1/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ICustomerProxy _customerProxy;

    public ClientsController(ICustomerProxy customerProxy)
    {
        _customerProxy = customerProxy;
    }

    // GET: api/values
    [HttpGet]
    public async Task<DataCollection<ClientDto>> GetAllAsync(int page = 1, int take = 10)
    {
        return await _customerProxy.GetAllAsync(page, take);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ClientDto> Get(int id)
    {
        return await _customerProxy.GetAsync(id);
    }
}

