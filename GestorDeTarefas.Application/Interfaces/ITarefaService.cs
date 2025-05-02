using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Application.Services.Tarefas.Commands;
using GestorDeTarefas.Domain.Enums;

namespace GestorDeTarefas.Application.Interfaces
{
    public interface ITarefaService
    {
        Task<TarefaDto> CriarTarefaAsync(CreateTarefaCommand command);
        Task<TarefaDto> AtualizarTarefaAsync(UpdateTarefaCommand command);
        Task<bool> DeletarTarefaAsync(int id);
        Task<IEnumerable<TarefaDto>> ListarTodasTarefasAsync();
        Task<IEnumerable<TarefaDto>> ListarTodasTarefasComFiltroAsync(StatusTarefa? status, DateTime? data);
        Task<TarefaDto> ObterTarefaPorIdAsync(int id);
    }
}