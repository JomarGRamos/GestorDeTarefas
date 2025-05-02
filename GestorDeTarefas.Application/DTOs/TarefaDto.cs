using GestorDeTarefas.Domain.Enums;

namespace GestorDeTarefas.Application.DTOs
{
    public class TarefaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public StatusTarefa Status { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
