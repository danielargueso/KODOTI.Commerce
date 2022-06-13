using Order.Service.Queries.Contracts;
using Order.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Order.Api.Controllers.v1;


[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderDetailController
{
	private readonly ILogger<OrderDetailController> _logger;
	private readonly IOrderDetailQueryService _orderDetailQueryService;
    private readonly IMediator _mediator;

    public OrderDetailController(ILogger<OrderDetailController> logger, IOrderDetailQueryService orderDetailQueryService, IMediator mediator)
    {
        _logger = logger;
        _orderDetailQueryService = orderDetailQueryService;
        _mediator = mediator;
    }

    [HttpGet("v1/orders/{orderID}/orderdetails")]
    public async Task<DataCollection<OrderDetailDto>> GetAsync(int orderID, int page = 1, int take = 10, string? ids = null)
    {
        IEnumerable<int>? orderDetailsIds = null;

        if (string.IsNullOrEmpty(ids) == false)
        {
            orderDetailsIds = ids.Split(',').Select(x => Convert.ToInt32(x));
        }

        return await _orderDetailQueryService.GetByOrderIdAsync(orderID, page, take, orderDetailsIds);
    }
}

