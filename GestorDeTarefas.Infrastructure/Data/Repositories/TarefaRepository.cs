using GestorDeTarefas.Domain.Entities;
using GestorDeTarefas.Domain.Enums;
using GestorDeTarefas.Domain.Interfaces;
using GestorDeTarefas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTarefas.Infrastructure.Data.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly TarefaDbContext _context;

        public TarefaRepository(TarefaDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefa> ObterPorIdAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada.");
            }
            return tarefa;
        }

        
        public async Task<IEnumerable<Tarefa>> ObterTodasAsync()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasComFiltroAsync(StatusTarefa? status, DateTime? data)
        {
            
            var query = _context.Tarefas.AsQueryable();

           
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

           
            if (data.HasValue)
            {
                query = query.Where(t => t.DataVencimento.Date == data.Value.Date);
            }

           
            return await query.ToListAsync();
        }

       
        public async Task AdicionarAsync(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
        }

        
        public void Atualizar(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
        }

       
        public void Remover(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
        }
    }
}