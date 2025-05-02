using Moq;
using Xunit;
using GestorDeTarefas.Application.Services.Tarefas;
using GestorDeTarefas.Domain.Entities;
using GestorDeTarefas.Application.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using GestorDeTarefas.Domain.Interfaces;
using FluentValidation;
using GestorDeTarefas.Domain.Enums;
using GestorDeTarefas.Application.Services.Tarefas.Commands;

namespace GestorDeTarefas.Tests
{
    public class TarefaServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<TarefaService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<Tarefa>> _mockValidator;
        private readonly TarefaService _tarefaService;

        public TarefaServiceTests()
        {
            // Mock do construtor
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<TarefaService>>();  
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<Tarefa>>();

            // Inicializa o serviço mockado
            _tarefaService = new TarefaService(
                _mockUnitOfWork.Object,
                _mockMapper.Object,
                _mockValidator.Object,
                _mockLogger.Object 
            );
        }

        [Fact]
        public async Task ObterTarefaPorId_Sucesso()
        {
            // Arrange
            var idTarefa = 10;
            var dataAtual = DateTime.Now;
            var tarefa = new Tarefa(
                "Tarefa Teste 1",
                 "Descrição da tarefa teste 1",
                 StatusTarefa.EmAndamento,
                 dataAtual
            );

            var tarefaDto = new TarefaDto
            {
                Id = idTarefa,
                Titulo = "Tarefa Teste 1",
                Descricao = "Descrição da tarefa teste 1",
                Status = StatusTarefa.EmAndamento,
                DataVencimento = dataAtual
            };

            // Configura o mock para retornar a tarefa simulada
            _mockUnitOfWork.Setup(uow => uow.TarefaRepository.ObterPorIdAsync(idTarefa))
                .ReturnsAsync(tarefa);

            // Configura o mock do AutoMapper
            _mockMapper.Setup(m => m.Map<TarefaDto>(tarefa))
                .Returns(tarefaDto);

            // Act
            var result = await _tarefaService.ObterTarefaPorIdAsync(idTarefa);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(idTarefa, result.Id);
            Assert.Equal("Tarefa Teste 1", result.Titulo);
            Assert.Equal("Descrição da tarefa teste 1", result.Descricao);
            Assert.Equal(StatusTarefa.EmAndamento, result.Status);
            Assert.Equal(dataAtual, result.DataVencimento);

        }


        [Fact]
        public async Task ObterTarefaPorId_Falha()
        {
            // Arrange
            var idTarefa = 30;

            // Mock para retornar null
            _mockUnitOfWork.Setup(uow => uow.TarefaRepository.ObterPorIdAsync(idTarefa))
                .ReturnsAsync((Tarefa?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _tarefaService.ObterTarefaPorIdAsync(idTarefa));
        }

        [Fact]
        public async Task CriarTarefa_Sucesso()
        {
            // Arrange
            var command = new CreateTarefaCommand
            {
                Titulo = "Nova tarefa",
                Descricao = "Teste",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Now.AddDays(1)
            };

            var tarefa = new Tarefa(command.Titulo, command.Descricao, command.Status, command.DataVencimento);
            var tarefaDto = new TarefaDto { Id = 1, Titulo = tarefa.Titulo };

            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<Tarefa>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _mockUnitOfWork.Setup(u => u.TarefaRepository.AdicionarAsync(It.IsAny<Tarefa>()));
            _mockUnitOfWork.Setup(u => u.CommitAsync());

            _mockMapper.Setup(m => m.Map<TarefaDto>(It.IsAny<Tarefa>()))
                .Returns(tarefaDto);

            // Act
            var result = await _tarefaService.CriarTarefaAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Nova tarefa", result.Titulo);
           
        }

        [Fact]
        public async Task CriarTarefa_Falha()
        {
            // Arrange
            var command = new CreateTarefaCommand
            {
                Titulo = "",
                Descricao = "Descrição",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Now
            };

            var validationResult = new FluentValidation.Results.ValidationResult(
                new List<FluentValidation.Results.ValidationFailure>
                {
            new("Titulo", "O título é obrigatório")
                });

            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<Tarefa>(), default))
                .ReturnsAsync(validationResult);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _tarefaService.CriarTarefaAsync(command));
            Assert.Contains("O título é obrigatório", ex.Message);
            
        }

    }
}