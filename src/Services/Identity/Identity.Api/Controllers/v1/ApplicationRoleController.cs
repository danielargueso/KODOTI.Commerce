using Identity.Service.EventHandlers.Commands;
using Identity.Service.Queries.Contracts;
using Identity.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Identity.Api.Controllers.v1;

[Route("v1/roles")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ApplicationRoleController : ControllerBase
{
	private readonly ILogger<ApplicationRoleController> _logger;
	private readonly IApplicationUserRoleQueryService _applicationUserRoleQueryService;
    private readonly IMediator _mediator;

    public ApplicationRoleController(ILogger<ApplicationRoleController> logger, IApplicationUserRoleQueryService applicationUserRoleQueryService, IMediator mediator)
    {
        _logger = logger;
        _applicationUserRoleQueryService = applicationUserRoleQueryService;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<DataCollection<ApplicationRoleDto>> GetUsersAsync(int page = 1, int take = 10, string? ids = null)
    {
        IEnumerable<string>? usersIds = null;

        if (string.IsNullOrEmpty(ids) == false)
        {
            usersIds = ids.Split(',').ToList();
        }
        return await _applicationUserRoleQueryService.GetAllAsync(page, take, usersIds);
    }

    [HttpGet("{roleId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApplicationRoleDto> GetUserAsync(string roleId)
    {
        if (roleId == null)
        {
            BadRequest();
        }
        return await _applicationUserRoleQueryService.GetAsync(roleId);
    }

    [HttpGet("assign/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateUserRolesAsync([FromBody] UserRoleCreateCommand command)
    {
        if (command == null ||
            string.IsNullOrWhiteSpace(command.UserId) || !Guid.TryParse(command.UserId, out _) ||
            string.IsNullOrWhiteSpace(command.RoleId) || !Guid.TryParse(command.RoleId, out _)
            )
        {
            BadRequest();
        }

        await _mediator.Publish(command);

        return Ok();
    }

}

