﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;
using WaveCenter.ModelsAPI;

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

            var experiencias = await _context.Experiencias
                .Include(x => x.Local)
                .Include(x => x.CategoriaExperiencia)
                .Include(x => x.TipoExperiencia)
                .Select(x => new
                {
                    Experiencia = x,
                    AverageRating = _context.Marcacoes
                        .Where(m => m.IdExperiencia == x.Id)
                        .SelectMany(m => m.ClientesMarcacoes)
                        .Average(cm => (double?)cm.Rating) ?? 0,
                    RatingCount = _context.Marcacoes
                        .Where(m => m.IdExperiencia == x.Id)
                        .SelectMany(m => m.ClientesMarcacoes)
                        .Count(cm => cm.Rating != null)
                })
                .OrderByDescending(x => x.Experiencia.DataInicio)
                .ThenByDescending(x => x.AverageRating)
                .ToListAsync();

            var result = experiencias.Select(x => new
            {
                x.Experiencia,
                x.AverageRating,
                x.RatingCount
            }).ToList();

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Experiencia>>> GetExperiencias(string userId)
        {
            if (_context.Experiencias == null)
            {
                return NotFound();
            }

            var result1 = await _context.Experiencias
                .Include(x => x.Local)
                .Include(x => x.CategoriaExperiencia)
                .Include(x => x.TipoExperiencia)
                .Join(_context.Marcacoes,
                      experiencia => experiencia.Id,
                      marcacao => marcacao.IdExperiencia,
                      (experiencia, marcacao) => new { Experiencia = experiencia, Marcacao = marcacao })
                .Join(_context.ClientesMarcacoes,
                      combined => combined.Marcacao.Id,
                      clienteMarcacao => clienteMarcacao.MarcacaoId,
                      (combined, clienteMarcacao) => new { combined.Experiencia, combined.Marcacao, ClienteMarcacao = clienteMarcacao })
                .Select(x => new
                {
                    x.Experiencia,
                    x.Marcacao,
                    x.ClienteMarcacao,
                    AverageRating = _context.Marcacoes
                        .Where(m => m.IdExperiencia == x.Experiencia.Id)
                        .SelectMany(m => m.ClientesMarcacoes)
                        .Average(cm => (double?)cm.Rating) ?? 0,
                    RatingCount = _context.Marcacoes
                        .Where(m => m.IdExperiencia == x.Experiencia.Id)
                        .SelectMany(m => m.ClientesMarcacoes)
                        .Count(cm => cm.Rating != null),
                    TotalParticipants = _context.Marcacoes
                        .Where(m => m.IdExperiencia == x.Experiencia.Id)
                        .SelectMany(m => m.ClientesMarcacoes)
                        .Sum(cm => (int)cm.NumeroParticipantesUser)
                })
                .Where(x => x.ClienteMarcacao.UserId == userId)
                .OrderByDescending(x => x.Marcacao.Data)
                .ThenByDescending(x => x.AverageRating)
                .ToListAsync();

            var result2 = result1.Select(x => new
            {
                x.Experiencia,
                x.Marcacao,
                x.ClienteMarcacao,
                x.AverageRating,
                x.RatingCount,
                x.TotalParticipants,
            }).ToList();

            return Ok(result2);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Experiencia>> GetExperiencia(int id)
        {
            if (_context.Experiencias == null)
            {
                return NotFound();
            }
            var experiencia = await _context.Experiencias.Include(x => x.Local).Include(x => x.CategoriaExperiencia).Include(x => x.TipoExperiencia).FirstOrDefaultAsync(x => x.Id == id);

            if (experiencia == null)
            {
                return NotFound();
            }

            return experiencia;
        }


        [HttpGet("ExperienciasPopulares")]
        public async Task<ActionResult<IEnumerable<Experiencia>>> GetPopularExperiencias()
        {
            if (_context.Experiencias == null)
            {
                return NotFound();
            }

            var experiencias = await _context.Experiencias
                .Include(x => x.Local)
                .Include(x => x.CategoriaExperiencia)
                .Include(x => x.TipoExperiencia)
                .Select(x => new
                {
                    Experiencia = x,
                    AverageRating = _context.Marcacoes
                        .Where(m => m.IdExperiencia == x.Id)
                        .SelectMany(m => m.ClientesMarcacoes)
                        .Average(cm => (double?)cm.Rating) ?? 0,
                    RatingCount = _context.Marcacoes
                        .Where(m => m.IdExperiencia == x.Id)
                        .SelectMany(m => m.ClientesMarcacoes)
                        .Count(cm => cm.Rating != null)
                })
                .Where(x => x.Experiencia.DataInicio > DateTime.Today.Date)
                .OrderByDescending(x => x.Experiencia.DataInicio)
                .ThenByDescending(x => x.AverageRating)
                .Take(8)
                .ToListAsync();

            var result = experiencias.Select(x => new
            {
                x.Experiencia,
                x.AverageRating,
                x.RatingCount
            }).ToList();

            return Ok(result);
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
        public async Task<ActionResult<Experiencia>> PostExperiencia(InsertExperiencia experiencia)
        {
            if (_context.Experiencias == null)
            {
                return Problem("Entity is null.");
            }

            _context.Experiencias.Add(new Experiencia()
            {
                Nome = experiencia.Nome,
                Descricao = experiencia.Descricao,
                DataInicio = experiencia.DataInicio,
                DataFim = experiencia.DataFim,
                IdLocal = experiencia.IdLocal,
                Imagem = experiencia.Imagem,
                NumeroMaximoPessoas = experiencia.NumeroMaximoPessoas,
                NumeroMinimoPessoas = experiencia.NumeroMinimoPessoas,
                DuracaoMaxima = experiencia.DuracaoMaxima,
                DuracaoMinima = experiencia.DuracaoMinima,
                HoraComecoDia = experiencia.HoraComecoDia,
                HoraFimDia = experiencia.HoraFimDia,
                IdTipoExperiencia = experiencia.IdTipoExperiencia,
                PrecoHora = experiencia.PrecoHora,
                IdCategoriaExperiencia = experiencia.IdCategoriaExperiencia,
                Ativo = true
            });

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
