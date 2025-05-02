using AutoMapper;
using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Application.Services.Tarefas.Commands;
using GestorDeTarefas.Domain.Entities;

namespace GestorDeTarefas.Application.Mappings
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaDto>().ReverseMap();
            CreateMap<CreateTarefaCommand, Tarefa>();
            CreateMap<UpdateTarefaCommand, Tarefa>();
        }
    }
}