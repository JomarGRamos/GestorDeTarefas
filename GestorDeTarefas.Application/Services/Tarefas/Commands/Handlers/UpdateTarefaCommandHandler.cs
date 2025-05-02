using AutoMapper;
using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Application.Interfaces;
using GestorDeTarefas.Domain.Interfaces;
using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Commands.Handlers
{
    public class UpdateTarefaCommandHandler : IRequestHandler<UpdateTarefaCommand, TarefaDto>
    {
        private readonly ITarefaService _tarefaService;
        private readonly IMapper _mapper;

        public UpdateTarefaCommandHandler(ITarefaService tarefaService, IMapper mapper)
        {
            _tarefaService = tarefaService;
            _mapper = mapper;
        }

        public async Task<TarefaDto> Handle(UpdateTarefaCommand request, CancellationToken cancellationToken)
        {
           
            var tarefaAtualizada = await _tarefaService.AtualizarTarefaAsync(request);

            if (tarefaAtualizada == null)
                return null!;

            
            return _mapper.Map<TarefaDto>(tarefaAtualizada);
        }
    }
}