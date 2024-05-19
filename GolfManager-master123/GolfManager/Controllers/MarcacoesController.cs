using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;
using WaveCenter.ModelsAPI;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcacoesController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public MarcacoesController(WaveCenterContext context)
        {
            _context = context;
        }


        [HttpGet("{clienteId}/cliente-marcacoes")]
        public async Task<IActionResult> GetClienteMarcacoes(string clienteId)
        {
            // Retrieve the Marcacao records for the specified clienteId
            var marcacoes = await _context.Marcacoes
                .Where(m => m.ClientesMarcacoes.Any(cm => cm.UserId == clienteId))
                .ToListAsync();

            if (marcacoes == null)
            {
                return NotFound();
            }

            // Retrieve the ClientesMarcacoes
            return Ok(marcacoes);
        }

        [HttpPost("{clienteId}")]
        public async Task<IActionResult> CreateMarcacao(string clienteId, InsertMarcacao marcacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insertMarcacao = new Marcacao()
            {
                IdExperiencia = marcacao.IdExperiencia,
                HoraInicio = marcacao.HoraInicio,
                HoraFim = marcacao.HoraFim,
                NumeroParticipantesTotal = marcacao.NumeroParticipantes,
                ExperienciaPartilhada = marcacao.ExperienciaPartilhada,
            };

            // Inserir na tabela Marcacoes
            _context.Marcacoes.Add(insertMarcacao);
            await _context.SaveChangesAsync();  // Salvar mudanças para obter o ID gerado

            var insertClientMarcacao = new ClientesMarcacao()
            {
                UserId = clienteId,
                NumeroParticipantesUser = marcacao.NumeroParticipantes,
                Rating = 0,
                Status = "Pendente",
                Preco = marcacao.Preco,
                MarcacaoId = insertMarcacao.Id // Usar o ID gerado após salvar a primeira entidade
            };

            // Depois o id inserido inserir na tabela ClientesMarcacoes
            _context.ClientesMarcacoes.Add(insertClientMarcacao);
            await _context.SaveChangesAsync();  // Salvar mudanças novamente

            return CreatedAtAction(nameof(GetMarcacaoById), new { id = insertMarcacao.Id }, marcacao);
        }


        // Optional: Add a method to get a single Marcacao by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarcacaoById(int id)
        {
            var marcacao = await _context.Marcacoes
                .Include(m => m.ClientesMarcacoes) // Include related ClientesMarcacoes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (marcacao == null)
            {
                return NotFound();
            }

            return Ok(marcacao);
        }


    }
}
