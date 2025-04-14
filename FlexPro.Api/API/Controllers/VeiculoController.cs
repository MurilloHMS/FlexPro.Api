using FlexPro.Api.Application.Commands;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VeiculoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateVeiculoCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateVeiculoCommand command)
        {
            if (id != command.Id) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteVeiculoCommand { Id = id });
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetVeiculoByIdQuery { Id = id });
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllVeiculosQuery());
            return Ok(result);
        }
    }
}
