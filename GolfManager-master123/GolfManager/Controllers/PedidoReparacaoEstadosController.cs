using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;
using Microsoft.AspNetCore.Authorization;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoReparacaoEstadosController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public PedidoReparacaoEstadosController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet("pedido/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PedidoReparacaoEstados>>> GetPedidosReparacao(int id)
        {
            if (_context.PedidoReparacaoEstados == null)
            {
                return NotFound();
            }

            var pedidosReparacao = await _context.PedidoReparacaoEstados.Where(x => x.IdPedidoReparacao == id).ToListAsync();

            return Ok(pedidosReparacao);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PedidoReparacaoEstados>> GetPedidoReparacaoEstados(int id)
        {
            if (_context.PedidoReparacaoEstados == null)
            {
                return NotFound();
            }
            var pedidoReparacaoEstados = await _context.PedidoReparacaoEstados.FirstOrDefaultAsync(x => x.IdPedidoReparacao == id);

            if (pedidoReparacaoEstados == null)
            {
                return NotFound();
            }

            return pedidoReparacaoEstados;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPedidoReparacaoEstados(int id, PedidoReparacaoEstados pedidoReparacaoEstados)
        {
            if (id != pedidoReparacaoEstados.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidoReparacaoEstados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoReparacaoEstadosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PedidoReparacaoEstados>> PostPedidoReparacaoEstados(PedidoReparacaoEstados pedidoReparacaoEstados)
        {
            if (_context.PedidoReparacaoEstados == null)
            {
                return Problem("Entity is null.");
            }
            _context.PedidoReparacaoEstados.Add(pedidoReparacaoEstados);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoReparacaoEstados", new { id = pedidoReparacaoEstados.Id }, pedidoReparacaoEstados);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePedidoReparacaoEstados(int id)
        {
            if (_context.PedidoReparacaoEstados == null)
            {
                return NotFound();
            }
            var pedidoReparacaoEstados = await _context.PedidoReparacaoEstados.FindAsync(id);
            if (pedidoReparacaoEstados == null)
            {
                return NotFound();
            }

            _context.PedidoReparacaoEstados.Remove(pedidoReparacaoEstados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoReparacaoEstadosExists(int id)
        {
            return (_context.PedidoReparacaoEstados?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
