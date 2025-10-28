using Microsoft.AspNetCore.Mvc;
using SharpTheory.Models;

namespace SharpTheory.Controllers;

[ApiController]
public class HealthController
{

    [HttpGet("/health")]
    public HealthModel HealthCheck()
    {
        return new HealthModel("healthy", 200);
    }
}