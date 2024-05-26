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
    public class LocaisExperienciasController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public LocaisExperienciasController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Local>>> GetLocalExperiencias()
        {
            if (_context.Locais == null)
            {
                return NotFound();
            }

            var locaisExperiencia = await _context.Locais.ToListAsync();
            return Ok(locaisExperiencia);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Local>> GetLocalExperiencia(int id)
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
        [Authorize]
        public async Task<IActionResult> PutLocalExperiencia(int id, Local localExperiencia)
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
        [Authorize]
        public async Task<ActionResult<Local>> PostLocalExperiencia(Local localExperiencia)
        {
            if (_context.Locais == null)
            {
                return Problem("Entity is null.");
            }
            _context.Locais.Add(localExperiencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocalExperiencia", new { id = localExperiencia.Id }, localExperiencia);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLocalExperiencia(int id)
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

            _context.Locais.Remove(localExperiencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalExperienciaExists(int id)
        {
            return (_context.Locais?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
 }
