using Identity.Service.EventHandlers.Commands;
using Identity.Service.Queries.Contracts;
using Identity.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Identity.Api.Controllers.v1;

[Route("v1/users")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ApplicationUserController : ControllerBase
{
	private readonly ILogger<ApplicationUserController> _logger;
	private readonly IMediator _mediator;
	private readonly IApplicationUserQueryService _applicationUserQueryService;
    private readonly IApplicationUserRoleQueryService _applicationUserRoleQueryService;

    public ApplicationUserController(ILogger<ApplicationUserController> logger, IMediator mediator, IApplicationUserQueryService applicationUserQueryService, IApplicationUserRoleQueryService applicationUserRoleQueryService)
    {
        _logger = logger;
        _mediator = mediator;
        _applicationUserQueryService = applicationUserQueryService;
        _applicationUserRoleQueryService = applicationUserRoleQueryService;
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateUser(UserCreateCommand command)
    {
        if (ModelState.IsValid)
        {
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                _logger.LogError("Error during user creation process for user name: '{mail}'", command.Email);
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

        return BadRequest();
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<DataCollection<ApplicationUserDto>> GetUsersAsync(int page = 1, int take = 10, string? ids = null)
    {
        IEnumerable<string>? usersIds = null;

        if (string.IsNullOrEmpty(ids) == false)
        {
            usersIds = ids.Split(',').ToList();
        }
        return await _applicationUserQueryService.GetAllAsync(page, take, usersIds);
    }

    [HttpGet("{userId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApplicationUserDto> GetUserAsync(string userId)
    {
        if (userId == null)
        {
            BadRequest();
        }
        return await _applicationUserQueryService.GetAsync(userId);
    }

    [HttpGet("{userId}/roles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<DataCollection<ApplicationRoleDto>> GetUserRolesAsync(string userId, int page = 1, int take = 10)
    {
        if (userId == null)
        {
            BadRequest();
        }
        return await _applicationUserRoleQueryService.GetAllByUserIdAsync(userId, page, take);
    }
}

