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
    public class TiposExperienciasController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public TiposExperienciasController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoExperiencia>>> GetTipoExperiencias()
        {
            if (_context.TipoExperiencias == null)
            {
                return NotFound();
            }

            var tiposExperiencia = await _context.TipoExperiencias.ToListAsync();
            return Ok(tiposExperiencia);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoExperiencia>> GetTipoExperiencia(int id)
        {
            if (_context.TipoExperiencias == null)
            {
                return NotFound();
            }

            var tipoExperiencia = await _context.TipoExperiencias.FindAsync(id);

            if (tipoExperiencia == null)
            {
                return NotFound();
            }

            return tipoExperiencia;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoExperiencia(int id, TipoExperiencia tipoExperiencia)
        {
            if (id != tipoExperiencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoExperiencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExperienciaExists(id))
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
        public async Task<ActionResult<TipoExperiencia>> PostTipoExperiencia(TipoExperiencia tipoExperiencia)
        {
            if (_context.TipoExperiencias == null)
            {
                return Problem("Entity set 'TipoExperiencia'  is null.");
            }
            _context.TipoExperiencias.Add(tipoExperiencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoExperiencia", new { id = tipoExperiencia.Id }, tipoExperiencia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoExperiencia(int id)
        {
            if (_context.TipoExperiencias == null)
            {
                return NotFound();
            }

            var tipoExperiencia = await _context.TipoExperiencias.FindAsync(id);
            if (tipoExperiencia == null)
            {
                return NotFound();
            }

            _context.TipoExperiencias.Remove(tipoExperiencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoExperienciaExists(int id)
        {
            return (_context.TipoExperiencias?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
 }
