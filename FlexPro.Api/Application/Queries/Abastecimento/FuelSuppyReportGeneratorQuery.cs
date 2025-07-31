using MediatR;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class FuelSuppyReportGeneratorQuery : IRequest<byte[]>
{
    public DateTime Date { get; set; }

    public FuelSuppyReportGeneratorQuery(DateTime date)
    {
        Date = date;
    }
}