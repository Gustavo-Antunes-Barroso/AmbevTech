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

        /// <summary>
        /// Realiza a criação de uma venda
        /// </summary>
        /// <param name="venda"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Venda>> CreateVenda(Venda venda)
        {
            var createdVenda = await _vendaService.CreateVendaAsync(venda);
            return CreatedAtAction(nameof(CreateVenda), new { id = createdVenda.NumeroVenda }, createdVenda);
        }

        /// <summary>
        /// Realiza o update de uma venda
        /// </summary>
        /// <param name="id"></param>
        /// <param name="venda"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Realiza o cancelamento de uma venda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelVenda(int id)
        {
            await _vendaService.CancelVendaAsync(id);
            return Ok();
        }

        /// <summary>
        /// Realiza o cancelamento de um Item da venda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("item/{id}")]
        public async Task<IActionResult> CancelItem(int id)
        {
            await _vendaService.CancelItemAsync(id);
            return Ok();
        }
    }
}
