using Microsoft.AspNetCore.Mvc;

namespace QuaverEd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetTest()
        {
            return Ok(new { 
                message = "Test endpoint working!",
                timestamp = DateTime.UtcNow,
                databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") != null ? "Configured" : "Not configured",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"
            });
        }

        [HttpGet("health")]
        public ActionResult GetHealth()
        {
            return Ok(new { 
                status = "healthy",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
