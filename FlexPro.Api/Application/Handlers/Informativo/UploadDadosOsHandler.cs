using FlexPro.Api.Application.Commands.Informativo;
using FlexPro.Domain.Models;
using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Informativo;

public class UploadDadosOsHandler :  IRequestHandler<UploadDadosOsCommand, IActionResult>
{
    private readonly InformativoService _service;

    public UploadDadosOsHandler(InformativoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Handle(UploadDadosOsCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0) 
            return new BadRequestObjectResult("Não foi possivel obter os dados do arquivo");

        IEnumerable<InformativoOS> dados = await _service.ReadOsData(request.File);
        return dados.Any() ? new OkObjectResult(dados) : new BadRequestObjectResult("Não foi possivel retornar os dados do arquivo");
    }
}