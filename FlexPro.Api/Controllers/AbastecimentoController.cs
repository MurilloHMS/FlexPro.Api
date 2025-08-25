using FlexPro.Application.UseCases.FuelSupply.GetAll;
using FlexPro.Application.UseCases.FuelSupply.GetGeneralMetrics;
using FlexPro.Application.UseCases.FuelSupply.GetIndividualMetrics;
using FlexPro.Application.UseCases.FuelSupply.GetSectorMetrics;
using FlexPro.Application.UseCases.FuelSupply.Report;
using FlexPro.Application.UseCases.FuelSupply.Upload;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbastecimentoController : ControllerBase
{
    private readonly IMediator _mediator;

    public AbastecimentoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> Getall()
    {
        var result = await _mediator.Send(new GetAllAbastecimentoQuery());
        return Ok(result);
    }

    [HttpGet("report")]
    public async Task<IActionResult> DownloadReport([FromQuery] DateTime date)
    {
        var pdf = await _mediator.Send(new FuelSuppyReportGeneratorQuery(date));
        return File(pdf, "application/pdf", "Relatório de Abastecimento.pdf");
    }

    [HttpGet("Calcular/Individual/{data}")]
    public async Task<IActionResult> GetIndividualMetrics(DateTime data)
    {
        var retorno = await _mediator.Send(new GetIndividualMetricsQuery(data));
        return retorno;
    }

    [HttpGet("Calcular/Setor/{data}")]
    public async Task<IActionResult> GetSetorMetrics(DateTime data)
    {
        var retorno = await _mediator.Send(new GetSetorMetricsQuery(data));
        return retorno;
    }

    [HttpGet("Calcular/Geral/{data}")]
    public async Task<IActionResult> GetGeralMetrics(DateTime data)
    {
        var retorno = await _mediator.Send(new GetGeralMetricsQuery(data));
        return retorno;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var retorno = await _mediator.Send(new UploadAbastecimentoCommand(file));
        return Ok(retorno);
    }
}