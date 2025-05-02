using GestorDeTarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTarefas.Infrastructure.Context
{
    public class TarefaDbContext : DbContext
    {
        public TarefaDbContext(DbContextOptions<TarefaDbContext> options)
            : base(options)
        {
        }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.ToTable("Tarefas");
                entity.HasKey(e => e.Id); 

                // Configurações de propriedades
                entity.Property(e => e.Titulo)
                    .IsRequired()   
                    .HasMaxLength(255); 

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(500); 

               
                entity.Property(e => e.Status)
                   .HasConversion<int>();

                
                entity.Property(e => e.DataVencimento)
                    .IsRequired()
                    .HasColumnType("datetime");
            });
        }
    }
}