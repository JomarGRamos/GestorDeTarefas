using AutoMapper;
using FluentValidation;
using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Application.Interfaces;
using GestorDeTarefas.Application.Services.Tarefas.Commands;
using GestorDeTarefas.Domain.Entities;
using GestorDeTarefas.Domain.Enums;
using GestorDeTarefas.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace GestorDeTarefas.Application.Services.Tarefas
{
    public class TarefaService : ITarefaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Tarefa> _tarefaValidator;
        private readonly ILogger<TarefaService> _logger;

        public TarefaService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Tarefa> tarefaValidator, ILogger<TarefaService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tarefaValidator = tarefaValidator;
            _logger = logger;
        }

        public async Task<TarefaDto> CriarTarefaAsync(CreateTarefaCommand command)
        {
            try
            {
                var tarefa = new Tarefa(command.Titulo, command.Descricao, command.Status, command.DataVencimento);

                var validationResult = await _tarefaValidator.ValidateAsync(tarefa);

                if (!validationResult.IsValid)
                {
                    throw new Exception(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }


                await _unitOfWork.TarefaRepository.AdicionarAsync(tarefa);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<TarefaDto>(tarefa);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar tarefa.");
                throw;
            }
        }

        public async Task<TarefaDto> AtualizarTarefaAsync(UpdateTarefaCommand command)
        {
            try
            {
                var tarefa = await _unitOfWork.TarefaRepository.ObterPorIdAsync(command.Id);

                if (tarefa == null)
                {
                    throw new KeyNotFoundException("Tarefa não encontrada");
                }

                tarefa.Atualizar(command.Titulo, command.Descricao, command.Status, command.DataVencimento);

                var validationResult = await _tarefaValidator.ValidateAsync(tarefa);

                if (!validationResult.IsValid)
                {
                   
                    throw new Exception(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                _unitOfWork.TarefaRepository.Atualizar(tarefa);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar tarefa.");
                throw;
            }
        }

        public async Task<bool> DeletarTarefaAsync(int id)
        {
            try
            {
                var tarefa = await _unitOfWork.TarefaRepository.ObterPorIdAsync(id);

                if (tarefa == null)
                {
                    return false;
                }

                _unitOfWork.TarefaRepository.Remover(tarefa);
                await _unitOfWork.CommitAsync();

                return true; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover tarefa.");
                throw; 
            }
        }

        public async Task<IEnumerable<TarefaDto>> ListarTodasTarefasAsync()
        {
            try
            {
                var tarefas = await _unitOfWork.TarefaRepository.ObterTodasAsync();
                return _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tarefa.");
                throw;
            }
        }

        public async Task<IEnumerable<TarefaDto>> ListarTodasTarefasComFiltroAsync(StatusTarefa? status, DateTime? data)
        {
            try
            {
                var tarefas = await _unitOfWork.TarefaRepository.ObterTodasComFiltroAsync(status, data);
                return _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tarefa.");
                throw; 
            }
        }

        public async Task<TarefaDto> ObterTarefaPorIdAsync(int id)
        {
            try
            {
                var tarefa = await _unitOfWork.TarefaRepository.ObterPorIdAsync(id);

                if (tarefa == null)
                {
                    throw new KeyNotFoundException("Tarefa não encontrada");
                }

                return _mapper.Map<TarefaDto>(tarefa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tarefa.");
                throw;
            }
        }
    }
}