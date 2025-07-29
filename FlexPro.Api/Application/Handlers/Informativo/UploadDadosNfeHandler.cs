using FlexPro.Api.Application.Commands.Informativo;
using FlexPro.Domain.Models;
using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Informativo;

public class UploadDadosNfeHandler :  IRequestHandler<UploadDadosNfeCommand, IActionResult>
{
    private readonly InformativoService _service;
    
    public UploadDadosNfeHandler(InformativoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Handle(UploadDadosNfeCommand request, CancellationToken cancellationToken)
    {
        if (request.file == null || request.file.Length == 0)
            return new BadRequestObjectResult("Não foi possível obter os dados do arquivo");

        IEnumerable<InformativoNFe> dados = await _service.ReadNfeData(request.file);
        return dados.Any() ? new OkObjectResult(dados) : new BadRequestObjectResult("Não foi possivel retornar os dados do arquivo");
    }
}