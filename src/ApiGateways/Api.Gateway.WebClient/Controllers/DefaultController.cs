using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Gateway.WebClient.Controllers;

[Route("/")]
public class DefaultController : ControllerBase
{
    // GET: api/values
    [HttpGet]
    public string Get() => "Running...";
}

