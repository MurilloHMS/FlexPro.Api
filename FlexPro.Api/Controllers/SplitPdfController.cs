using System.IO.Compression;
using FlexPro.Application.UseCases.Pdf.Save;
using FlexPro.Application.UseCases.Pdf.Upload;
using FlexPro.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// TODO: Migrate this to Mediator Pattern
namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SplitPdfController(IMediator mediator) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<ActionResult<List<PdfPageInfo>>> Upload(IFormFile? file)
    {
        if (file is null || file.Length is 0) return BadRequest("Nenhum arquivo ou arquivo inválido enviado");

        var command = new UploadPdfCommand(file);
        var result = await mediator.Send(command);
        return result.Count > 0 
            ? Ok(result)
            : BadRequest("Não foi possível extrair as páginas do PDF informado.");
    }

    [HttpPost("save")]
    public async Task<ActionResult> Save([FromBody] List<PdfPageInfo>? pages)
    {
        if (pages == null || !pages.Any())
            return BadRequest("Nenhuma página fornecida para salvar.");

        var commnad = new SavePdfCommand(pages);
        var result = await mediator.Send(commnad);
        return File(result.FileBytes, "application/zip", "PDF_Separados.zip");
    }
}