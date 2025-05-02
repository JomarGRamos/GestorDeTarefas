using GestorDeTarefas.Application.Interfaces;
using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Commands.Handlers
{
    public class DeleteTarefaCommandHandler : IRequestHandler<DeleteTarefaCommand, Unit>
    {
        private readonly ITarefaService _tarefaService;

        public DeleteTarefaCommandHandler(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        public async Task<Unit> Handle(DeleteTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefaDeletada = await _tarefaService.DeletarTarefaAsync(request.Id);

            return Unit.Value;
        }
    }
}