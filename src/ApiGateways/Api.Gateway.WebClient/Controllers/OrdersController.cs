﻿using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.Proxies.Catalog.Contracts;
using Api.Gateway.Proxies.Customer.Contracts;
using Api.Gateway.Proxies.Order.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Gateway.WebClient.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderProxy _orderProxy;
    private readonly ICatalogProxy _catalogProxy;
    private readonly ICustomerProxy _customerProxy;

    public OrdersController(IOrderProxy orderProxy, ICatalogProxy catalogProxy, ICustomerProxy customerProxy)
    {
        _orderProxy = orderProxy;
        _catalogProxy = catalogProxy;
        _customerProxy = customerProxy;
    }

    // GET: api/values
    [HttpGet]
    public async Task<DataCollection<OrderDto>> GetAllAsync(int page = 1, int take = 10)
    {
        var result = await _orderProxy.GetAllAsync(page, take);

        if (result.HasItems)
        {
            // Retrieve client ids
            var clientIds = result.Items
                .Select(x => x.ClientId)
                .GroupBy(g => g)
                .Select(x => x.Key).ToList();

            var clients = await _customerProxy.GetAllAsync(1, clientIds.Count, clientIds);

            foreach (var order in result.Items)
            {
                order.ClientId = clients.Items.Single(x => x.ClientId == order.ClientId).ClientId;
            }
        }

        return result;
    }

    [HttpGet("{id}")]
    public async Task<OrderDto> Get(int id)
    {
        var result = await _orderProxy.GetAsync(id);

        // Retrieve client
        result.ClientId = (await _customerProxy.GetAsync(result.ClientId)).ClientId;

        // Retrieve product ids
        var productIds = result.Items
            .Select(x => x.ProductId)
            .GroupBy(g => g)
            .Select(x => x.Key).ToList();

        var products = await _catalogProxy.GetAllAsync(1, productIds.Count(), productIds);

        foreach (var item in result.Items)
        {
            item.ProductId = products.Items.Single(x => x.ProductId == item.ProductId).ProductId;
        }

        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateCommand command)
    {
        await _orderProxy.CreateAsync(command);
        return Ok();
    }
}

