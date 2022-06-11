using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers.v1
{
    [Route("v1/identity")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		private readonly ILogger<IdentityController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediator;

        public IdentityController(ILogger<IdentityController> logger, SignInManager<ApplicationUser> signInManager, IMediator mediator)
        {
            _logger = logger;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        [HttpPost("createuser")]
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

        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication(UserLoginCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Invalid login attempt using for user name: '{mail}'", command.Email);
                    return BadRequest("Access denied");
                }

                _logger.LogInformation("Succesfully login attempt with user name: '{mail}'", command.Email);
                return Ok(result);
            }

            return BadRequest();
        }
    }
}

