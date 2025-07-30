using FlexPro.Api.Application.Commands.Abastecimento;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Abastecimento;

public class UploadAbastecimentoHandler : IRequestHandler<UploadAbastecimentoCommand, IActionResult>
{
    private readonly AbastecimentoService _service;
    private readonly AppDbContext _context;
    private readonly IAbastecimentoRepository _repository;
    
    
    public UploadAbastecimentoHandler(AppDbContext context, AbastecimentoService service, IAbastecimentoRepository repository)
    {
        _context = context;
        _service = service;
        _repository = repository;
    }

    public async Task<IActionResult> Handle(UploadAbastecimentoCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0) return new BadRequestObjectResult("Arquivo inválido");

        List<Domain.Entities.Abastecimento> dadosAbastecimento = await _service.ColetarDadosAbastecimento(request.File);

        if (dadosAbastecimento != null && dadosAbastecimento.Any())
        {
            foreach (var abastecimento in dadosAbastecimento)
            {
                var departamento = _context.Funcionarios.FirstOrDefault(f =>
                    f.Nome.ToUpper().Contains(abastecimento.NomeDoMotorista.ToUpper()));
                abastecimento.Departamento = departamento != null ? departamento.Departamento : "Sem Departamento";
            }

            try
            {
                await _repository.AddRangeFuelSupply(dadosAbastecimento);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult($"Ocorreu um erro ao salvar os dados de abastecimento {e.Message}");
            }
        }
        return new OkObjectResult(dadosAbastecimento);
    }
}