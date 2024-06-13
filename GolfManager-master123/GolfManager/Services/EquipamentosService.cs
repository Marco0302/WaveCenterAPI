using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;

namespace WaveCenter.Services
{
    public class EquipamentosService
    {
        private readonly WaveCenterContext _context;

        public EquipamentosService(WaveCenterContext context)
        {
            _context = context;
        }


        // Nova função para inserir um equipamento em uma marcação
        public async Task<bool> InsertEquipamentoInMarcacaoAsync(int equipamentoId, int marcacaoId)
        {
            var equipamento = await _context.Equipamentos.FindAsync(equipamentoId);
            var marcacao = await _context.Marcacoes.FindAsync(marcacaoId);

            if (equipamento == null || marcacao == null)
            {
                return false; // Ou lançar uma exceção apropriada
            }

            // Verificar se o equipamento está disponível
            var equipamentosDisponiveis = await GetEquipamentosDisponiveisAsync(equipamento.IdCategoriaEquipamento, marcacaoId);
            if (!equipamentosDisponiveis.Any(e => e.Id == equipamentoId))
            {
                return false; // Equipamento não está disponível
            }

            var equipamentoMarcacao = new EquipamentoMarcacao
            {
                IdEquipamento = equipamentoId,
                IdMarcacao = marcacaoId
            };

            _context.EquipamentosMarcacoes.Add(equipamentoMarcacao);
            await _context.SaveChangesAsync();

            return true;
        }


        // Nova função para buscar equipamentos disponíveis
        public async Task<List<Equipamento>> GetEquipamentosDisponiveisAsync(int equipamentoId, int marcacaoId)
        {
            var marcacaoAtual = await _context.Marcacoes
                .FirstOrDefaultAsync(m => m.Id == marcacaoId);

            if (marcacaoAtual == null)
            {
                return null; // Ou lançar uma exceção apropriada
            }

            var equipamentosIndisponiveis = await _context.EquipamentosMarcacoes
                .Where(em => em.Equipamento.Id == equipamentoId
                             && em.Marcacao.Data == marcacaoAtual.Data
                             && ((em.Marcacao.HoraInicio < marcacaoAtual.HoraFim && em.Marcacao.HoraInicio >= marcacaoAtual.HoraInicio)
                                 || (em.Marcacao.HoraFim > marcacaoAtual.HoraInicio && em.Marcacao.HoraFim <= marcacaoAtual.HoraFim)))
                .Select(em => em.Equipamento.Id)
                .ToListAsync();

            return await _context.Equipamentos
                .Where(e => e.IdCategoriaEquipamento == equipamentoId && !equipamentosIndisponiveis.Contains(e.Id))
                .ToListAsync();
        }


    }
}
