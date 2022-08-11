using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ping : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("pong!");
    }
}