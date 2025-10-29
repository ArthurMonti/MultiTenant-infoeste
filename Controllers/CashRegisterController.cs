using CursoInfoeste.Abstractions.Services;
using CursoInfoeste.Controllers.Base;
using CursoInfoeste.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CursoInfoeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CashRegisterController : BaseController
    {
        private readonly ICashRegisterService _service;

        public CashRegisterController(ICashRegisterService service)
        {
            _service = service;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> GetByNumber(int number, CancellationToken cancellationToken)
        {
            var cashRegister = await _service.GetByNumber( number, cancellationToken);
            if (cashRegister == null)
            {
                return NotFound();
            }
            return Ok(cashRegister);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCashRegisterRequest request, CancellationToken cancellationToken)
        {
            var cashRegister = await _service.Create( request, cancellationToken);
            return Ok(cashRegister);
        }

        [HttpPost("{number}/open")]
        public async Task<IActionResult> Open(int number, CancellationToken cancellationToken)
        {
            var opened = await _service.Open(number, cancellationToken);
            if (!opened)
            {
                return NoContent();
            }
            return Ok();
        }

        [HttpPost("{number}/close")]
        public async Task<IActionResult> Close(int number, CancellationToken cancellationToken)
        {
            var closed = await _service.Close(number, cancellationToken);
            if (!closed)
            {
                return NoContent();
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
    }
}
