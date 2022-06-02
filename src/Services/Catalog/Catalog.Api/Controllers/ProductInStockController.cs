using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries.Contracts;
using Catalog.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Catalog.Api.Controllers
{
    [Route("/stocks")]
    [ApiController]
    public class ProductInStockController : ControllerBase
    {
        private readonly ILogger<ProductInStockController> _logger;
        private readonly IProductInStockQueryService _productInStockQueryService;
        private readonly IMediator _mediator;

        public ProductInStockController(ILogger<ProductInStockController> logger, IProductInStockQueryService productInStockQueryService, IMediator mediator)
        {
            _logger = logger;
            _productInStockQueryService = productInStockQueryService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<ProductInStockDto>> GetAllAsync(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int>? stocks = null;

            if(string.IsNullOrEmpty(ids) == false)
            {
                stocks = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            return await _productInStockQueryService.GetAllAsync(page, take, stocks);
        }

        [HttpGet("{id}")]
        public async Task<ProductInStockDto> GetAsync(int id)
        {
            return await _productInStockQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductInStockCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateStockCommand command)
        {
            await _mediator.Publish(command);
            return NoContent();
        }
    }
}
