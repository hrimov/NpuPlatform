using Microsoft.AspNetCore.Mvc;

namespace NpuBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("healthcheck")]
        public IActionResult GetHealthCheck()
        {
            return Ok(new { message = "ok" });
        }
        
    }
}