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
    public class TiposUserController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public TiposUserController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoUser>>> GetTipoUsers()
        {
            if (_context.TipoUsers == null)
            {
                return NotFound();
            }

            var tiposUser = await _context.TipoUsers.ToListAsync();
            return Ok(tiposUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUser>> GetTipoUser(int id)
        {
            if (_context.TipoUsers == null)
            {
                return NotFound();
            }

            var tipoUser = await _context.TipoUsers.FindAsync(id);

            if (tipoUser == null)
            {
                return NotFound();
            }

            return tipoUser;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUser(int id, TipoUser tipoUser)
        {
            if (id != tipoUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUserExists(id))
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
        public async Task<ActionResult<TipoUser>> PostTipoUser(TipoUser tipoUser)
        {
            if (_context.TipoUsers == null)
            {
                return Problem("Entity is null.");
            }
            _context.TipoUsers.Add(tipoUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoUser", new { id = tipoUser.Id }, tipoUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUser(int id)
        {
            if (_context.TipoUsers == null)
            {
                return NotFound();
            }

            var tipoUser = await _context.TipoUsers.FindAsync(id);
            if (tipoUser == null)
            {
                return NotFound();
            }

            _context.TipoUsers.Remove(tipoUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoUserExists(int id)
        {
            return (_context.TipoUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
 }
