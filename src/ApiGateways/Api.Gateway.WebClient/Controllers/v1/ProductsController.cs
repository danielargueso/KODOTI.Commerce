using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.Proxies.Catalog.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Gateway.WebClient.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICatalogProxy _catalogProxy;

    public ProductsController(ICatalogProxy catalogProxy)
    {
        _catalogProxy = catalogProxy;
    }

    // GET: api/values
    [HttpGet]
    public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take)
    {
        return await _catalogProxy.GetAllAsync(page, take);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ProductDto> GetAsync(int id)
    {
        return await _catalogProxy.GetAsync(id);
    }
}

