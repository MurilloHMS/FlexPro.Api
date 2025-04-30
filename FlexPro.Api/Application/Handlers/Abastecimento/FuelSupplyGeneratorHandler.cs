using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Application.Queries.Abastecimento;
using FlexPro.Api.Infrastructure.Services;
using MediatR;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FlexPro.Api.Application.Handlers.Abastecimento;

public class FuelSupplyGeneratorHandler : IRequestHandler<FuelSuppyReportGeneratorQuery, byte[]>
{
    private readonly IReportService _reportService;
    
    public FuelSupplyGeneratorHandler(IReportService service)
    {
        _reportService = service;
    }

    public async Task<byte[]> Handle(FuelSuppyReportGeneratorQuery request, CancellationToken cancellationToken)
    {
        return await _reportService.GenerateFuelSupplyReportAsync(request.Date);
    }
}