using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Commands
{
    public class DeleteTarefaCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteTarefaCommand(int id)
        {
            Id = id;
        }
    }
}