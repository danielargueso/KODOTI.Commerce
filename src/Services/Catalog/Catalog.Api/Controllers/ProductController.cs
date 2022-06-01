﻿using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries.Contracts;
using Catalog.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Catalog.Api.Controllers
{
    [Route("/products")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ILogger<DefaultController> _logger;
		private readonly IProductQueryService _productQueryService;
		private readonly IMediator _mediator;


        public ProductController(ILogger<DefaultController> logger, IProductQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _productQueryService = productQueryService;
            _mediator = mediator;
        }

        [HttpGet]
		public async Task<DataCollection<ProductDto>> GetAllAsync(int page = 1, int take = 10, string? ids = null)
		{
			IEnumerable<int>? products = null;

			if(string.IsNullOrEmpty(ids) == false)
            {
				products = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

			return await _productQueryService.GetAllAsync(page, take, products);
		}

        [HttpGet("{id}")]
		public async Task<ProductDto> GetAsync(int id)
        {
			return await _productQueryService.GetAsync(id);
        }

        [HttpPost]
		public async Task<IActionResult> Create(ProductCreateCommand command)
        {
			await _mediator.Publish(command);
			return Ok();
        }
	}
}

