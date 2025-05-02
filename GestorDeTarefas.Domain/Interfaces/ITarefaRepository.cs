using GestorDeTarefas.Domain.Entities;
using GestorDeTarefas.Domain.Enums;

namespace GestorDeTarefas.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task<Tarefa> ObterPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTodasAsync();
        Task<IEnumerable<Tarefa>> ObterTodasComFiltroAsync(StatusTarefa? status, DateTime? data);
        Task AdicionarAsync(Tarefa tarefa);
        void Atualizar(Tarefa tarefa);
        void Remover(Tarefa tarefa);
    }
}