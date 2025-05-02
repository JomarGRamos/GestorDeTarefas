using AutoMapper;
using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Application.Interfaces;
using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Queries.Handlers
{
    public class GetTarefaByIdQueryHandler : IRequestHandler<GetTarefaByIdQuery, TarefaDto>
    {
        private readonly ITarefaService _tarefaService;
        private readonly IMapper _mapper;


        public GetTarefaByIdQueryHandler(ITarefaService tarefaService, IMapper mapper)
        {
            _tarefaService = tarefaService;
            _mapper = mapper;
        }

        public async Task<TarefaDto> Handle(GetTarefaByIdQuery request, CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaService.ObterTarefaPorIdAsync(request.Id);

            if (tarefa == null)
            {
                return null!;
            }

            return _mapper.Map<TarefaDto>(tarefa);
        }
    }
}