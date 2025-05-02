namespace GestorDeTarefas.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ITarefaRepository TarefaRepository { get; }

        Task<int> CommitAsync();
        Task RollbackAsync();
    }
}