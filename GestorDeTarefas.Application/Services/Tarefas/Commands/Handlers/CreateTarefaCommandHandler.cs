using AutoMapper;
using GestorDeTarefas.Application.DTOs;
using MediatR;
using GestorDeTarefas.Domain.Entities;
using GestorDeTarefas.Application.Interfaces;

namespace GestorDeTarefas.Application.Services.Tarefas.Commands.Handlers
{
    public class CreateTarefaCommandHandler : IRequestHandler<CreateTarefaCommand, TarefaDto>
    {
        private readonly IMapper _mapper;
        private readonly ITarefaService _tarefaService;

        public CreateTarefaCommandHandler(IMapper mapper, ITarefaService tarefaService)
        {
            _mapper = mapper;
            _tarefaService = tarefaService;
        }

        public async Task<TarefaDto> Handle(CreateTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = _mapper.Map<Tarefa>(request);

            var tarefaCriada = await _tarefaService.CriarTarefaAsync(request);

            return _mapper.Map<TarefaDto>(tarefaCriada);
        }
    }
}