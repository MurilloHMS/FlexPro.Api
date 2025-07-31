using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Api.Application.Queries.Veiculo;
using FlexPro.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using ISender = MediatR.ISender;

namespace FlexPro.Api.Controllers
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

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var command = new FlexPro.Application.UseCases.Vehicles.GetAll.Command();
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetVeiculoByIdQuery { Id = id });
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] VeiculoDto dto)
        {
            var command = new FlexPro.Application.UseCases.Vehicles.Create.Command(dto);
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error);
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
    }
}