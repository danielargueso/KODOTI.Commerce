using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers;

[Route("/")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class DefaultController : ControllerBase
{
    private readonly ILogger<DefaultController> _logger;

    public DefaultController(ILogger<DefaultController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        return "Running...";
    }
}
