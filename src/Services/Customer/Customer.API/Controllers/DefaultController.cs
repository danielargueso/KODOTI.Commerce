using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer.API.Controllers;

[Route("/")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class DefaultController : Controller
{
    private readonly ILogger<DefaultController> _logger;

    public DefaultController(ILogger<DefaultController> logger)
    {
        _logger = logger;
    }

    // GET: api/values
    [HttpGet]
    public string Get()
    {
        _logger.LogDebug($"Called default controller");
        return "Running...";
    }
}

