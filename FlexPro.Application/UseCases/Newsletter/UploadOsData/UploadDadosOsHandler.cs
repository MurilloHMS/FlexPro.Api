using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Newsletter.UploadOsData;

public class UploadDadosOsHandler : IRequestHandler<UploadDadosOsCommand, IActionResult>
{
    private readonly InformativoService _service;

    public UploadDadosOsHandler(InformativoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Handle(UploadDadosOsCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length == 0)
            return new BadRequestObjectResult("Não foi possivel obter os dados do arquivo");

        var dados = await _service.ReadOsData(request.File);
        return dados.Any()
            ? new OkObjectResult(dados)
            : new BadRequestObjectResult("Não foi possivel retornar os dados do arquivo");
    }
}