using ClosedXML.Excel;
using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Api.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class CreateClienteListBySheetsHandler : IRequestHandler<CreateClienteListBySheetsCommand, IActionResult>
{
    private readonly IClienteRepository _clienteRepository;

    public CreateClienteListBySheetsHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IActionResult> Handle(CreateClienteListBySheetsCommand command,
        CancellationToken cancellationToken)
    {
        if (command.file == null && command.file.Length == 0)
        {
            return new BadRequestObjectResult("Nenhum arquivo enviado");
        }

        List<Domain.Entities.Cliente> clientes = new();
        using (var stream = new MemoryStream())
        {
            await command.file.CopyToAsync(stream);
            stream.Position = 0;

            using (XLWorkbook wb = new XLWorkbook(stream))
            {
                var planilha = wb.Worksheets.FirstOrDefault();
                var dados = planilha.RowsUsed().Skip(1).Select(row => new Domain.Entities.Cliente
                {
                    CodigoSistema = row.Cell(1).TryGetValue<string>(out var codigoSistemaCliente)
                        ? codigoSistemaCliente
                        : default,
                    Nome = row.Cell(2).TryGetValue<string>(out var nomeParceiro) ? nomeParceiro : default,
                    Email = row.Cell(3).TryGetValue<string>(out var email) ? email : default,
                    Status = "true"
                }).ToList();
                    
                clientes.AddRange(dados);
            }
        }
        await _clienteRepository.IncludeClienteByRange(clientes);
        return new OkObjectResult(clientes);
    }
}