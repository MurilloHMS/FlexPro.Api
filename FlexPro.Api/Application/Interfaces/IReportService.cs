namespace FlexPro.Api.Application.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GenerateFuelSupplyReportAsync(DateTime date);
    }
}
