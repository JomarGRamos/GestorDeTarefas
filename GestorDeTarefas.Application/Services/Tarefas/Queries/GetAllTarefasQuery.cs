using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Domain.Enums;
using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Queries
{
    public class GetAllTarefasQuery : IRequest<IEnumerable<TarefaDto>>
    {
        public StatusTarefa? Status { get; set; }
        public DateTime? DataVencimento { get; set; }

        public GetAllTarefasQuery(StatusTarefa? status = null, DateTime? dataVencimento = null)
        {
            Status = status;
            DataVencimento = dataVencimento;
        }

        public GetAllTarefasQuery(FiltroTarefaDto filtro)
        {
            Status = filtro.Status.HasValue ? (StatusTarefa?)filtro.Status : null;
            DataVencimento = filtro.DataVencimento;
        }
    }
}