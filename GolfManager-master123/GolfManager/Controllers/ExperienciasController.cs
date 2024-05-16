﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienciasController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public ExperienciasController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experiencia>>> GetExperiencias()
        {
            if (_context.Experiencias == null)
            {
                return NotFound();
            }

            var experiencias = await _context.Experiencias.Include(x => x.CategoriaExperiencia).Include(x => x.TipoExperiencia).ToListAsync();

            return Ok(experiencias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Experiencia>> GetExperiencia(int id)
        {
            if (_context.Experiencias == null)
            {
                return NotFound();
            }
            var experiencia = await _context.Experiencias.Include(x => x.CategoriaExperiencia).Include(x => x.TipoExperiencia).FirstOrDefaultAsync(x => x.Id == id);

            if (experiencia == null)
            {
                return NotFound();
            }

            return experiencia;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperiencia(int id, Experiencia experiencia)
        {
            if (id != experiencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(experiencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienciaExists(id))
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
        public async Task<ActionResult<Experiencia>> PostExperiencia(Experiencia experiencia)
        {
            if (_context.Experiencias == null)
            {
                return Problem("Entity set 'Experiencias'  is null.");
            }
            _context.Experiencias.Add(experiencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExperiencia", new { id = experiencia.Id }, experiencia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperiencia(int id)
        {
            if (_context.Experiencias == null)
            {
                return NotFound();
            }
            var experiencia = await _context.Experiencias.FindAsync(id);
            if (experiencia == null)
            {
                return NotFound();
            }

            _context.Experiencias.Remove(experiencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExperienciaExists(int id)
        {
            return (_context.Experiencias?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
