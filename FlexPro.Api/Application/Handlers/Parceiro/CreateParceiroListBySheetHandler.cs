using ClosedXML.Excel;
using FlexPro.Api.Application.Commands.Parceiro;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Parceiro;

public class CreateParceiroListBySheetHandler : IRequestHandler<CreateParceiroListBySheetCommand, IActionResult>
{
    private readonly IParceiroRepository _repository;

    public CreateParceiroListBySheetHandler(IParceiroRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Handle(CreateParceiroListBySheetCommand request,
        CancellationToken cancellationToken)
    {
        if (request.File == null && request.File.Length == 0)
        {
            return new BadRequestObjectResult("Nenhum arquivo enviado");
        }

        List<Domain.Entities.Parceiro> parceiros = new();
        using (var stream = new MemoryStream())
        {
            await request.File.CopyToAsync(stream);
            stream.Position = 0;

            using (XLWorkbook wb = new(stream))
            {
                var sheet = wb.Worksheets.FirstOrDefault();
                parceiros = sheet.RowsUsed().Skip(1).Select(row => new Domain.Entities.Parceiro
                {
                    CodigoSistema = row.Cell(1).TryGetValue<string>(out var codigoSistema) ?  codigoSistema : default!,
                    Nome = row.Cell(2).TryGetValue<string>(out var nome) ?  nome : default!,
                    Email = row.Cell(3).TryGetValue<string>(out var email) ? email : default!,
                    RazaoSocial = nome,
                    Ativo = true,
                    RecebeEmail = true
                }).ToList();
            }
        }

        if (!parceiros.Any())
        {
            new BadRequestObjectResult("Ocorreu um erro ao coletar os dados");
        }
        await _repository.IncludeParceiroByRangeAsync(parceiros);
        return new OkObjectResult(parceiros);
    }
}