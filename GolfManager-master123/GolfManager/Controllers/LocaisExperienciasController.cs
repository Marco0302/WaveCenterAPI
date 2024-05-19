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
    public class LocaisExperienciasController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public LocaisExperienciasController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoExperiencia>>> GetLocalExperiencias()
        {
            if (_context.Locais == null)
            {
                return NotFound();
            }

            var locaisExperiencia = await _context.Locais.ToListAsync();
            return Ok(locaisExperiencia);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Local>> GetTipoExperiencia(int id)
        {
            if (_context.Locais == null)
            {
                return NotFound();
            }

            var localExperiencia = await _context.Locais.FindAsync(id);

            if (localExperiencia == null)
            {
                return NotFound();
            }

            return localExperiencia;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoExperiencia(int id, Local localExperiencia)
        {
            if (id != localExperiencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(localExperiencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalExperienciaExists(id))
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
        public async Task<ActionResult<Local>> PostTipoExperiencia(Local tipoExperiencia)
        {
            if (_context.Locais == null)
            {
                return Problem("Entity set 'TipoExperiencia'  is null.");
            }
            _context.Locais.Add(tipoExperiencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoExperiencia", new { id = tipoExperiencia.Id }, tipoExperiencia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoExperiencia(int id)
        {
            if (_context.Locais == null)
            {
                return NotFound();
            }

            var tipoExperiencia = await _context.Locais.FindAsync(id);
            if (tipoExperiencia == null)
            {
                return NotFound();
            }

            _context.Locais.Remove(tipoExperiencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalExperienciaExists(int id)
        {
            return (_context.Locais?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
 }
