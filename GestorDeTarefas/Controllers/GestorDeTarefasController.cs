using GestorDeTarefas.Application.DTOs;
using GestorDeTarefas.Application.Services.Tarefas.Commands;
using GestorDeTarefas.Application.Services.Tarefas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeTarefas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestorDeTarefasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GestorDeTarefasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Método para obter todas as tarefas cadastradas
        [HttpGet]
        public async Task<IActionResult> ListarTodasTarefas()
        {
            var query = new GetAllTarefasQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Método para obter a tarefa filtrada
        [HttpGet("filtrar")]
        public async Task<IActionResult> Filtrar([FromQuery] FiltroTarefaDto filtro)
        {
            var query = new GetAllTarefasQuery(filtro);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Método para obter a tarefa pelo id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarefaById(int id)
        {
            var query = new GetTarefaByIdQuery(id);
            var tarefa = await _mediator.Send(query);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        // Método para criação da tarefa
        [HttpPost]
        public async Task<IActionResult> CreateTarefa([FromBody] CreateTarefaCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTarefaById), new { id = response.Id }, response);
        }

        // Método para alteração da tarefa
        [HttpPut]
        public async Task<IActionResult> AtualizarTarefa([FromBody] UpdateTarefaCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // Método para remoção da tarefa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            await _mediator.Send(new DeleteTarefaCommand(id));
            return NoContent();
        }

    }
}
