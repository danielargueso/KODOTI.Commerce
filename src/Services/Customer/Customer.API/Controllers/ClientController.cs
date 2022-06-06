using Customer.Service.EventHandlers.Commands;
using Customer.Service.Queries.Contracts;
using Customer.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IClientQueryService _clientQueryService;
    private readonly IMediator _mediator;

    public ClientController(ILogger<ClientController> logger, IClientQueryService clientQueryService, IMediator mediator)
    {
        _logger = logger;
        _clientQueryService = clientQueryService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<DataCollection<ClientDTO>> GetAllAsync(int page = 1, int take = 10, string? ids = null)
    {
        IEnumerable<int>? clients = null;

        if (string.IsNullOrEmpty(ids) == false)
        {
            clients = ids.Split(',').Select(x => Convert.ToInt32(x));
        }

        return await _clientQueryService.GetAllAsync(page, take, clients);
    }

    [HttpGet("{id}")]
    public async Task<ClientDTO> GetAsync(int id)
    {
        return await _clientQueryService.GetAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]ClientCreateCommand command)
    {
        await _mediator.Publish(command);
        return Ok();
    }
}

