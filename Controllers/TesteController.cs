using CursoInfoeste.Models;
using CursoInfoeste.Services;
using Microsoft.AspNetCore.Mvc;

namespace CursoInfoeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController(TokenService service) : ControllerBase
    {


        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<Tenant>> Create()
        {
            return Ok(new Tenant { Id = 1, Name = "Tenant 1" });
        }

        [HttpPost]
        public IActionResult Login()
        {
            return Ok(service.GenerateToken());
        }
    }
}
