using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Domain.Enums;
using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Commands
{
    public class UpdateTarefaCommand : IRequest<TarefaDto>
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public StatusTarefa Status { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}