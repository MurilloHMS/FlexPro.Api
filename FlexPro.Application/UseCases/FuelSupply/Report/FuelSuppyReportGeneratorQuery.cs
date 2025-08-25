using MediatR;

namespace FlexPro.Application.UseCases.FuelSupply.Report;

public class FuelSuppyReportGeneratorQuery : IRequest<byte[]>
{
    public FuelSuppyReportGeneratorQuery(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; set; }
}