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
    public class CategoriasEquipamentosController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public CategoriasEquipamentosController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaEquipamento>>> GetCategoriaEquipamentos()
        {
            if (_context.CategoriaEquipamentos == null)
            {
                return NotFound();
            }

            var customers = await _context.CategoriaEquipamentos.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEquipamento>> GetCategoriaEquipamento(int id)
        {
            if (_context.CategoriaEquipamentos == null)
            {
                return NotFound();
            }

            var categoriaEquipamentos = await _context.CategoriaEquipamentos.FindAsync(id);

            if (categoriaEquipamentos == null)
            {
                return NotFound();
            }

            return categoriaEquipamentos;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEquipamento(int id, CategoriaEquipamento categoriaEquipamentos)
        {
            if (id != categoriaEquipamentos.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriaEquipamentos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaEquipamentoExists(id))
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
        public async Task<ActionResult<CategoriaEquipamento>> PostCategoriaEquipamento(CategoriaEquipamento categoriaEquipamento)
        {
            if (_context.CategoriaEquipamentos == null)
            {
                return Problem("Entity is null.");
            }
            _context.CategoriaEquipamentos.Add(categoriaEquipamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaEquipamento", new { id = categoriaEquipamento.Id }, categoriaEquipamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaEquipamento(int id)
        {
            if (_context.CategoriaEquipamentos == null)
            {
                return NotFound();
            }

            var categoriaEquipamento = await _context.CategoriaEquipamentos.FindAsync(id);
            if (categoriaEquipamento == null)
            {
                return NotFound();
            }

            _context.CategoriaEquipamentos.Remove(categoriaEquipamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaEquipamentoExists(int id)
        {
            return (_context.CategoriaEquipamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
 }
