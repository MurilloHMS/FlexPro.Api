using FlexPro.Application.DTOs;
using FlexPro.Application.UseCases.Vehicles.Create;
using FlexPro.Application.UseCases.Vehicles.GetAll;
using FlexPro.Application.UseCases.Vehicles.GetById;
using FlexPro.Application.UseCases.Vehicles.DeleteById;
using FlexPro.Application.UseCases.Vehicles.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

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
        var command = new GetAllVehicleQuery();
        var result = await _mediator.Send(command);
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetById(int id)
    {
        var command = new GetVehicleByIdQuery(id);
        var result = await _mediator.Send(command);
        return  result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IResult> Create([FromBody] VeiculoDto dto)
    {
        var command = new CreateVehicleCommand(dto);
        var result = await _mediator.Send(command);
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] VeiculoDto dto)
    {
        if (id != dto.Id) return BadRequest();
        await _mediator.Send(new UpdateVehicleCommand(dto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteVehicleByIdCommand(id));
        return NoContent();
    }
}