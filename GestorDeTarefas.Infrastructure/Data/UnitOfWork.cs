using GestorDeTarefas.Domain.Interfaces;
using GestorDeTarefas.Infrastructure.Context;
using GestorDeTarefas.Infrastructure.Data.Repositories;

namespace GestorDeTarefas.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TarefaDbContext _context;
        private ITarefaRepository _tarefaRepository;

        public UnitOfWork(TarefaDbContext context)
        {
            _context = context;
        }

        public ITarefaRepository TarefaRepository =>
            _tarefaRepository ??= new TarefaRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task RollbackAsync()
        {
            return Task.CompletedTask;
        }
    }
}