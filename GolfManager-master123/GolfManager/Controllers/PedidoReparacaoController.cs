﻿using System;
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
    public class PedidoReparacaoController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public PedidoReparacaoController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PedidoReparacao>>> GetPedidosReparacao()
        {
            if (_context.PedidoReparacao == null)
            {
                return NotFound();
            }

            var pedidosReparacao = await _context.PedidoReparacao.Include(x => x.Equipamento).Include(x => x.User).ToListAsync();

            return Ok(pedidosReparacao);
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PedidoReparacao>>> GetPedidosReparacaoUser(string id)
        {
            if (_context.PedidoReparacao == null)
            {
                return NotFound();
            }

            var pedidosReparacao = await _context.PedidoReparacao.Include(x => x.Equipamento).Include(x => x.User).Where(x => x.UserId == id) .ToListAsync();

            return Ok(pedidosReparacao);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PedidoReparacao>> GetPedidoReparacao(int id)
        {
            if (_context.PedidoReparacao == null)
            {
                return NotFound();
            }
            var pedidoReparacao = await _context.PedidoReparacao.Include(x => x.Equipamento).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            if (pedidoReparacao == null)
            {
                return NotFound();
            }

            return pedidoReparacao;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPedidoReparacao(int id, PedidoReparacao pedidoReparacao)
        {
            if (id != pedidoReparacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidoReparacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoReparacaoExists(id))
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
        public async Task<ActionResult<PedidoReparacao>> PostPedidoReparacao(PedidoReparacao pedidoReparacao)
        {
            if (_context.PedidoReparacao == null)
            {
                return Problem("Entity is null.");
            }
            _context.PedidoReparacao.Add(pedidoReparacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoReparacao", new { id = pedidoReparacao.Id }, pedidoReparacao);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePedidoReparacao(int id)
        {
            if (_context.PedidoReparacao == null)
            {
                return NotFound();
            }
            var pedidoReparacao = await _context.PedidoReparacao.FindAsync(id);
            if (pedidoReparacao == null)
            {
                return NotFound();
            }

            _context.PedidoReparacao.Remove(pedidoReparacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoReparacaoExists(int id)
        {
            return (_context.PedidoReparacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
