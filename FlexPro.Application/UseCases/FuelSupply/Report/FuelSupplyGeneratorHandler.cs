using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.FuelSupply.Report;

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