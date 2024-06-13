using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;
using Microsoft.AspNetCore.Authorization;
using WaveCenter.ModelsAPI;
using WaveCenter.Services;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentosController : ControllerBase
    {
        private readonly WaveCenterContext _context;
        private EquipamentosService _service;
        public EquipamentosController(WaveCenterContext context)
        {
            _context = context;
            _service = new EquipamentosService(context);
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<Equipamento>> PostEquipamento(Equipamento equipamento)
        {
            if (_context.Equipamentos == null)
            {
                return Problem("Entity is null.");
            }
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipamento", new { id = equipamento.Id }, equipamento);
        }

        [HttpDelete("{id}")]
        [Authorize]
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


        // GET: api/Equipamentos/Marcacao/10
        [HttpGet("Marcacao/{marcacaoId}")]
        public async Task<ActionResult<IEnumerable<CategoriaEquipamentoDto>>> GetEquipamentosMarcacao(int marcacaoId)
        {
            // Obtendo o IdExperiencia a partir da marcacaoId
            var marcacao = await _context.Marcacoes
                .Where(m => m.Id == marcacaoId)
                .Select(m => new { m.IdExperiencia })
                .FirstOrDefaultAsync();

            if (marcacao == null)
            {
                return NotFound();
            }

            var experienciaId = marcacao.IdExperiencia;

            var categoriaEquipamentos = await _context.EquipamentosExperiencias
                .Where(ee => ee.IdExperiencia == experienciaId)
                .Include(ee => ee.CategoriaEquipamento)
                .Select(ee => new CategoriaEquipamentoDto
                {
                    Id = ee.CategoriaEquipamento.Id,
                    Designacao = ee.CategoriaEquipamento.Designacao,
                    QuantidadeNecessaria = ee.QuantidadeNecessaria
                })
                .ToListAsync();


            //if (categoriaEquipamentos == null || categoriaEquipamentos.Count == 0)
            //{
            //    return NotFound();
            //}

            var quantidadeAtual = await _context.EquipamentosMarcacoes
                .Where(em => em.Marcacao.Id == marcacaoId && em.Marcacao.IdExperiencia == experienciaId)
                .GroupBy(em => em.Equipamento.CategoriaEquipamento.Id)
                .Select(g => new
                {
                    CategoriaEquipamentoId = g.Key,
                    QuantidadeAtual = g.Count()
                })
                .ToListAsync();


            foreach (var categoria in categoriaEquipamentos)
            {
                var quantidade = quantidadeAtual.FirstOrDefault(q => q.CategoriaEquipamentoId == categoria.Id);
                categoria.QuantidadeAtual = quantidade?.QuantidadeAtual ?? 0;
            }

            return Ok(categoriaEquipamentos);
        }


        // Buscar equipamentos disponíveis
        [HttpGet("EquipamentosDisponiveis/Categoria/{categoriaEquipamentoId}/Marcacao/{marcacaoId}")]
        public async Task<ActionResult<List<Equipamento>>> GetEquipamentosDisponiveis(int categoriaEquipamentoId, int marcacaoId)
        {
            var marcacaoAtual = await _context.Marcacoes
          .FirstOrDefaultAsync(m => m.Id == marcacaoId);

            if (marcacaoAtual == null)
            {
                return NotFound();
            }

            var equipamentosIndisponiveis = await _context.EquipamentosMarcacoes
                .Where(em => em.Equipamento.IdCategoriaEquipamento == categoriaEquipamentoId
                             && em.Marcacao.Data == marcacaoAtual.Data
                             && ((em.Marcacao.HoraInicio < marcacaoAtual.HoraFim && em.Marcacao.HoraInicio >= marcacaoAtual.HoraInicio)
                                 || (em.Marcacao.HoraFim > marcacaoAtual.HoraInicio && em.Marcacao.HoraFim <= marcacaoAtual.HoraFim)))
                .Select(em => em.Equipamento.Id)
                .ToListAsync();


            var equipamentosDisponiveis = await _context.Equipamentos
                .Where(e => e.IdCategoriaEquipamento == categoriaEquipamentoId && !equipamentosIndisponiveis.Contains(e.Id))
                .ToListAsync();

            //if (equipamentosDisponiveis == null || equipamentosDisponiveis.Count == 0)
            //{
            //    return NotFound();
            //}

            return Ok(equipamentosDisponiveis);
        }


        // Novo endpoint para inserir um equipamento em uma marcação
        [HttpPost("EquipamentoMarcacao")]
        public async Task<ActionResult> InsertEquipamentoInMarcacao(int equipamentoId, int marcacaoId)
        {
            var result = await _service.InsertEquipamentoInMarcacaoAsync(equipamentoId, marcacaoId);

            if (!result)
            {
                return BadRequest("Não foi possível adicionar o equipamento à marcação. Equipamento pode não estar disponível.");
            }

            return Ok("Equipamento adicionado com sucesso.");
        }


        [HttpGet("CategoriasEquipamentoNecessarios/{id}")]
        public async Task<ActionResult<IEnumerable<EquipamentosExperiencia>>> GetCategoriasEquipamentosNecessarios(int id)
        {
            if (_context.EquipamentosExperiencias == null)
            {
                return NotFound();
            }

            var equipamentosExperiencias = await _context.EquipamentosExperiencias.Include(x => x.CategoriaEquipamento).Where(x => x.IdExperiencia == id).ToListAsync();

            return Ok(equipamentosExperiencias);
        }

        //TODO rever
        [HttpPost("CategoriasEquipamentoNecessarios")]
        public async Task<ActionResult<EquipamentosExperiencia>> PostCategoriaEquipamentoNecessario(EquipamentosExperiencia equipamento)
        {
            if (_context.EquipamentosExperiencias == null)
            {
                return Problem("Entity is null.");
            }
            _context.EquipamentosExperiencias.Add(equipamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriasEquipamentosNecessarios", new { id = equipamento.Id }, equipamento);
        }

        private bool EquipamentoExists(int id)
        {
            return (_context.Equipamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
