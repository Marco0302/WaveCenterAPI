using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentosController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public EquipamentosController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipamento>>> GetEquipamentos()
        {
            if (_context.Equipamentos == null)
            {
                return NotFound();
            }

            var equipamentos = await _context.Equipamentos.ToListAsync();

            return Ok(equipamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipamento>> GetEquipamento(int id)
        {
            if (_context.Equipamentos == null)
            {
                return NotFound();
            }
            var equipamento = await _context.Equipamentos.Include(x => x.CategoriaEquipamento).FirstOrDefaultAsync(x => x.Id == id);

            if (equipamento == null)
            {
                return NotFound();
            }

            return equipamento;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipamento(int id, Equipamento equipamento)
        {
            if (id != equipamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipamentoExists(id))
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
        public async Task<ActionResult<Equipamento>> PostEquipamento(Equipamento equipamento)
        {
            if (_context.Equipamentos == null)
            {
                return Problem("Entity set 'Equipamentos'  is null.");
            }
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipamento", new { id = equipamento.Id }, equipamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipamento(int id)
        {
            if (_context.Equipamentos == null)
            {
                return NotFound();
            }
            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null)
            {
                return NotFound();
            }

            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipamentoExists(int id)
        {
            return (_context.Equipamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
