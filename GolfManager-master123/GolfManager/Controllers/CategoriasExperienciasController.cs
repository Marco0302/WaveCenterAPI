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
    public class CategoriasExperienciasController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public CategoriasExperienciasController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoriaExperiencia>>> GetCategoriaExperiencias()
        {
            if (_context.CategoriaExperiencias == null)
            {
                return NotFound();
            }

            var customers = await _context.CategoriaExperiencias.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoriaExperiencia>> GetCategoriaExperiencia(int id)
        {
            if (_context.CategoriaExperiencias == null)
            {
                return NotFound();
            }

            var categoriaExperiencias = await _context.CategoriaExperiencias.FindAsync(id);

            if (categoriaExperiencias == null)
            {
                return NotFound();
            }

            return categoriaExperiencias;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCategoriaExperiencia(int id, CategoriaExperiencia categoriaExperiencias)
        {
            if (id != categoriaExperiencias.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriaExperiencias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExperienciaExists(id))
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
        public async Task<ActionResult<CategoriaExperiencia>> PostCategoriaExperiencia(CategoriaExperiencia categoriaExperiencia)
        {
            if (_context.CategoriaExperiencias == null)
            {
                return Problem("Entity is null.");
            }
            _context.CategoriaExperiencias.Add(categoriaExperiencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaExperiencia", new { id = categoriaExperiencia.Id }, categoriaExperiencia);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategoriaExperiencia(int id)
        {
            if (_context.CategoriaExperiencias == null)
            {
                return NotFound();
            }

            var categoriaExperiencia = await _context.CategoriaExperiencias.FindAsync(id);
            if (categoriaExperiencia == null)
            {
                return NotFound();
            }

            _context.CategoriaExperiencias.Remove(categoriaExperiencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExperienciaExists(int id)
        {
            return (_context.CategoriaExperiencias?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
 }
