using GestorDeTarefas.Domain.Enums;

namespace GestorDeTarefas.Domain.Entities
{
    public class Tarefa
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public StatusTarefa Status { get; private set; }
        public DateTime DataVencimento { get; private set; }

        public Tarefa(string titulo, string descricao, StatusTarefa status, DateTime dataVencimento)
        {
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataVencimento = dataVencimento.Date;
        }

        public void Atualizar(string titulo, string descricao, StatusTarefa status, DateTime dataVencimento)
        {
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataVencimento = dataVencimento.Date;
        }
    }
}