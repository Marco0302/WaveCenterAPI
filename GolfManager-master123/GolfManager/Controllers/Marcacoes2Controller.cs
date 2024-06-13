using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;
using WaveCenter.ModelsAPI;
using Microsoft.AspNetCore.Authorization;

namespace WaveCenter.Controllers
{
    [ApiController]
    public class Marcacoes2Controller : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public Marcacoes2Controller(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet("api/[controller]/marcacoes/")]
        public async Task<ActionResult<IEnumerable<Marcacao>>> GetMarcacoes()
        {
            if (_context.Marcacoes == null)
            {
                return NotFound();
            }

            var marcacoes = await _context.Marcacoes.Include(x => x.Experiencia).ToListAsync();

            return Ok(marcacoes);
        }

        [HttpGet("api/[controller]/marcacoes/{id}")]
        [Authorize]
        public async Task<ActionResult<Marcacao>> GetMarcacao(int id)
        {
            if (_context.Marcacoes == null)
            {
                return NotFound();
            }
            var marcacao = await _context.Marcacoes.Include(x => x.Experiencia).FirstOrDefaultAsync(x => x.Id == id);

            if (marcacao == null)
            {
                return NotFound();
            }

            return marcacao;
        }

        [HttpGet("api/[controller]/{clienteId}/cliente-marcacoes")]
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

        [HttpPost("api/[controller]/cliente/{idCliente}")]
        public async Task<IActionResult> CreateMarcacao(string idCliente, InsertMarcacao marcacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insertMarcacao = new Marcacao()
            {
                IdExperiencia = marcacao.IdExperiencia,
                Data = marcacao.Data,
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
                UserId = idCliente,
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

        [HttpPost("api/[controller]/partilhada/{marcacaoId}/{clienteId}")]
        public async Task<IActionResult> AddUserToMarcacao(string clienteId, int marcacaoId, InsertClienteMarcacao insertclienteMarcacao)
        {
            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClientesMarcacao clienteMarcacao = new()
            {
                Preco = insertclienteMarcacao.Preco,
                NumeroParticipantesUser = insertclienteMarcacao.NumeroParticipantesUser
            };

            // Add the client to the Marcacao (appointment) in the ClientesMarcacoes table
            clienteMarcacao.MarcacaoId = marcacaoId;
            clienteMarcacao.UserId = clienteId;
            clienteMarcacao.Status = "Pendente";

            _context.ClientesMarcacoes.Add(clienteMarcacao);
            await _context.SaveChangesAsync();  // Save changes to the database

            // Return a response indicating that the resource was created
            return CreatedAtAction(nameof(AddUserToMarcacao), new { id = marcacaoId });
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



        // Novo endpoint para mudar o estado da marcação
        [HttpPost("api/ChangeEstado")]
        public async Task<ActionResult> ChangeMarcacaoEstados(int marcacaoId, string novoEstado)
        {
            var estadosValidos = new List<string> { "Pendente", "Confirmada", "Cancelada", "Completa" };

            if (!estadosValidos.Contains(novoEstado))
            {
                return BadRequest("Estado inválido.");
            }

            var clientesMarcacao = await _context.ClientesMarcacoes
                .Where(cm => cm.MarcacaoId == marcacaoId)
                .ToListAsync();

            if (clientesMarcacao == null || !clientesMarcacao.Any())
            {
                return BadRequest("Marcação não encontrada.");
            }

            // Verifica se o estado é "Confirmada"
            if (novoEstado == "Confirmada")
            {
                var marcacao = await _context.Marcacoes.Where(m=>m.Id == clientesMarcacao.FirstOrDefault().MarcacaoId).ToListAsync();
                if (marcacao == null)
                {
                    return BadRequest("Marcação não encontrada.");
                }

                var experienciaId = marcacao.FirstOrDefault().IdExperiencia;

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

                var quantidadeAtual = await _context.EquipamentosMarcacoes
                    .Where(em => em.Marcacao.Id == marcacaoId)
                    .GroupBy(em => em.Equipamento.CategoriaEquipamento.Id)
                    .Select(g => new
                    {
                        CategoriaEquipamentoId = g.Key,
                        QuantidadeAtual = g.Count()
                    })
                    .ToListAsync();

                foreach (var categoriaEquipamento in categoriaEquipamentos)
                {
                    var quantidade = quantidadeAtual.FirstOrDefault(q => q.CategoriaEquipamentoId == categoriaEquipamento.Id);
                    categoriaEquipamento.QuantidadeAtual = quantidade?.QuantidadeAtual ?? 0;

                    if (categoriaEquipamento.QuantidadeNecessaria > categoriaEquipamento.QuantidadeAtual)
                    {
                        return BadRequest("Não há quantidade suficiente de equipamentos para confirmar a marcação.");
                    }
                }
            }

            foreach (var clienteMarcacao in clientesMarcacao)
            {
                clienteMarcacao.Status = novoEstado;
            }

            await _context.SaveChangesAsync();

            return Ok("Estado da marcação alterado com sucesso.");
        }

        [HttpGet("api/[controller]/marcacoes/ultimodia")]
        public async Task<string> GetMarcacoesUltimoDia()
        {
            if (_context.Marcacoes == null)
            {
                return "0";
            }

            return _context.Marcacoes.Where(x => x.Data.Day == DateTime.Now.Day).Count().ToString();
        }

        [HttpGet("api/[controller]/marcacoes/ultimos7dias")]
        public async Task<string> GetMarcacoesUltimos7Dias()
        {
            if (_context.Marcacoes == null)
            {
                return "0";
            }

            return _context.Marcacoes.Where(x => x.Data >= DateTime.Now.AddDays(-7)).Count().ToString();
        }


        ////MARCACAO -> EQUIPAMENTOS
        //[HttpGet("disponiveis")]
        //public async Task<ActionResult<IEnumerable<Equipamento>>> GetEquipamentosDisponiveis(double horaInicio, double horaFim)
        //{
        //    var equipamentosDisponiveis = await ObterEquipamentosDisponiveis(horaInicio, horaFim);
        //    return Ok(equipamentosDisponiveis);
        //}

        //private async Task<List<Equipamento>> ObterEquipamentosDisponiveis(double horaInicio, double horaFim)
        //{
        //    // Hora de início e fim recebidas convertidas para TimeSpan
        //    TimeSpan inicio = TimeSpan.FromHours(horaInicio);
        //    TimeSpan fim = TimeSpan.FromHours(horaFim);

        //    // Obter todas as marcações que conflitam com o horário fornecido
        //    var marcacoesConflitantes = await _context.Marcacoes
        //        .Where(m => (m.HoraInicio < fim.TotalHours && m.HoraFim > inicio.TotalHours))
        //        .Select(m => m.Id)
        //        .ToListAsync();

        //    // Obter os equipamentos que estão associados às marcações conflitantes
        //    var equipamentosIndisponiveis = await _context.EquipamentosMarcacoes
        //        .Where(em => marcacoesConflitantes.Contains(em.IdMarcacao))
        //        .Select(em => em.IdEquipamento)
        //        .Distinct()
        //        .ToListAsync();

        //    // Obter todos os equipamentos que não estão na lista de indisponíveis
        //    var equipamentosDisponiveis = await _context.Equipamentos
        //        .Where(e => !equipamentosIndisponiveis.Contains(e.Id))
        //        .ToListAsync();

        //    return equipamentosDisponiveis;
        //}




    }
}
