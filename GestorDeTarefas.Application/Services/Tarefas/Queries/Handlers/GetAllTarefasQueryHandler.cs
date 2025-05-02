using AutoMapper;
using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Application.Interfaces;
using MediatR;

namespace GestorDeTarefas.Application.Services.Tarefas.Queries.Handlers
{
    public class GetAllTarefasQueryHandler : IRequestHandler<GetAllTarefasQuery, IEnumerable<TarefaDto>>
    {
        private readonly ITarefaService _tarefaService;
        private readonly IMapper _mapper;

        public GetAllTarefasQueryHandler(ITarefaService tarefaService, IMapper mapper)
        {
            _tarefaService = tarefaService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TarefaDto>> Handle(GetAllTarefasQuery request, CancellationToken cancellationToken)
        {
            //Caso seja sem filtro
            if (request.DataVencimento == null && request.Status == null)
            {
                
                var tarefas = await _tarefaService.ListarTodasTarefasAsync();
               
                return _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
            }
            else
            {
               
                var tarefas = await _tarefaService.ListarTodasTarefasComFiltroAsync(request.Status, request.DataVencimento);
               
                return _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
            }
           

          
        }
    }
}