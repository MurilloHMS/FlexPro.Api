using FlexPro.Api.Application.Commands.Informativo;
using FlexPro.Domain.Models;
using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Informativo;

public class UploadDadosPecasTrocadasHandler : IRequestHandler<UploadDadosPecasTrocadasCommand, IActionResult>
{
    private readonly InformativoService _service;

    public UploadDadosPecasTrocadasHandler(InformativoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Handle(UploadDadosPecasTrocadasCommand request,
        CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0)
            return new BadRequestObjectResult("Arquivo inválido ou vazio");

        IEnumerable<InformativoPecasTrocadas> dados = await _service.ReadPecasTrocadasData(request.File);
        return dados.Any()
            ? new OkObjectResult(dados)
            : new BadRequestObjectResult("Não foi possivel obter os dados do arquivo");
    }
}