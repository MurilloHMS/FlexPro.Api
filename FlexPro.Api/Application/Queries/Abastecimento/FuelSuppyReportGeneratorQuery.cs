using MediatR;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class FuelSuppyReportGeneratorQuery : IRequest<byte[]>
{
    public FuelSuppyReportGeneratorQuery(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; set; }
}