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
    public class FuncionariosController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public FuncionariosController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            if (_context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios
                                .Select(f => new Funcionario
                                {
                                    Id = f.Id,
                                    Nome = f.Nome,
                                    Apelido = f.Apelido,
                                    Nif = f.Nif,
                                    Morada = f.Morada,
                                    Email = f.Email
                                })
                                .ToListAsync();

            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            if (_context.Funcionarios == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionarios.Include(x => x.TipoFuncionario).FirstOrDefaultAsync(x => x.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'GolfClubContext.Clubs'  is null.");
            }
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionario", new { id = funcionario.Id }, funcionario);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            if (_context.Funcionarios == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuncionarioExists(int id)
        {
            return (_context.Funcionarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
