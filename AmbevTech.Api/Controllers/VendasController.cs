using AmbevTech.Application.Interfaces;
using AmbevTech.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmbevTech.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly IVendaService _vendaService;

        public VendasController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpPost]
        public async Task<ActionResult<Venda>> CreateVenda(Venda venda)
        {
            var createdVenda = await _vendaService.CreateVendaAsync(venda);
            return CreatedAtAction(nameof(CreateVenda), new { id = createdVenda.NumeroVenda }, createdVenda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenda(int id, Venda venda)
        {
            if (id != venda.NumeroVenda)
            {
                return BadRequest("Número da venda não informado!");
            }

            await _vendaService.UpdateVendaAsync(venda);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelVenda(int id)
        {
            await _vendaService.CancelVendaAsync(id);
            return Ok();
        }

        [HttpPut("item/{id}")]
        public async Task<IActionResult> CancelItem(int id)
        {
            await _vendaService.CancelItemAsync(id);
            return Ok();
        }
    }
}
