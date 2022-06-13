using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.Queries.Contracts;
using Order.Service.Queries.DTOs;
using Order.Services.EventHandlers.Commands;
using Service.Common.Collection;

namespace Order.Api.Controllers.v1;

[Route("v1/orders")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : ControllerBase
{
	private readonly ILogger<OrderController> _logger;
	private readonly IOrderQueryService _orderQueryService;
	private readonly IMediator _mediator;

    public OrderController(ILogger<OrderController> logger, IOrderQueryService orderQueryService, IMediator mediator)
    {
        _logger = logger;
        _orderQueryService = orderQueryService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<DataCollection<OrderDto>> GetAllAsync(int page = 1, int take = 10, string? ids = null)
    {
        IEnumerable<int>? orders = null;

        if (string.IsNullOrEmpty(ids) == false)
        {
            orders = ids.Split(',').Select(x => Convert.ToInt32(x));
        }

        return await _orderQueryService.GetAllAsync(page, take, orders);
    }

    [HttpGet("{id}")]
    public async Task<OrderDto> GetAsync(int id)
    {
        return await _orderQueryService.GetAsync(id);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateCommand command)
    {
        await _mediator.Publish(command);
        return Ok();
    }
}

