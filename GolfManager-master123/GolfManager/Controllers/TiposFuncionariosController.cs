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
    public class TiposFuncionariosController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public TiposFuncionariosController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoFuncionario>>> GetTipoFuncionarios()
        {
            if (_context.TipoFuncionarios == null)
            {
                return NotFound();
            }

            var customers = await _context.TipoFuncionarios.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoFuncionario>> GetTipoFuncionario(int id)
        {
            if (_context.TipoFuncionarios == null)
            {
                return NotFound();
            }

            var tipoFuncionario = await _context.TipoFuncionarios.FindAsync(id);

            if (tipoFuncionario == null)
            {
                return NotFound();
            }

            return tipoFuncionario;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoFuncionario(int id, TipoFuncionario tipoFuncionario)
        {
            if (id != tipoFuncionario.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoFuncionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoFuncionarioExists(id))
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
        public async Task<ActionResult<TipoFuncionario>> PostTipoFuncionario(TipoFuncionario tipoFuncionario)
        {
            if (_context.TipoFuncionarios == null)
            {
                return Problem("Entity set 'TipoFuncionario'  is null.");
            }
            _context.TipoFuncionarios.Add(tipoFuncionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoFuncionario", new { id = tipoFuncionario.Id }, tipoFuncionario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoFuncionario(int id)
        {
            if (_context.TipoFuncionarios == null)
            {
                return NotFound();
            }

            var tipoFuncionario = await _context.TipoFuncionarios.FindAsync(id);
            if (tipoFuncionario == null)
            {
                return NotFound();
            }

            _context.TipoFuncionarios.Remove(tipoFuncionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoFuncionarioExists(int id)
        {
            return (_context.TipoFuncionarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
 }
