namespace FlexPro.Domain.Repositories;

public interface IReportService
{
    Task<byte[]> GenerateFuelSupplyReportAsync(DateTime date);
}