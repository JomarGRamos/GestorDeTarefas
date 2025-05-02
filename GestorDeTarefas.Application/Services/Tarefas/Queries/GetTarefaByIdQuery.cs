using GestorDeTarefas.Application.DTOs;
using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Queries
{
    public class GetTarefaByIdQuery : IRequest<TarefaDto>
    {
        public int Id { get; set; }

        public GetTarefaByIdQuery(int id)
        {
            Id = id;
        }
    }
}