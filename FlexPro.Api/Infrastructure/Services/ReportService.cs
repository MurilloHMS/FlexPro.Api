using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Repositories;
using FlexPro.Api.Infrastructure.Templates.Reports;
using QuestPDF.Fluent;

namespace FlexPro.Api.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        private readonly AbastecimentoService _abastecimentoService;

        public ReportService(IAbastecimentoRepository abastecimentoRepository, AbastecimentoService abastecimentoService)
        {
            _abastecimentoRepository = abastecimentoRepository;
            _abastecimentoService = abastecimentoService;
        }

        public async Task<byte[]> GenerateFuelSupplyReportAsync(DateTime date)
        {
            var startDate = date.ToUniversalTime();
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var abastecimentos = await _abastecimentoRepository.GetFuelSupplyByDate(startDate, endDate);
            var metricsResult = await _abastecimentoService.CalcularMetricasAbastecimento(startDate);
            var report = new FuelSupplyReport(
                abastecimentos,
                GetLogoImage(),
                GetPostoImage(),
                metricsResult)
            {
                MetricasGeral = "Métricas gerais do relatório de abastecimento."
            };
            return report.GeneratePdf();
        }

        private byte[] GetLogoImage() => Properties.Resources.Logo_Proauto;
        private byte[] GetPostoImage() => Properties.Resources.Posto03;
    }
}
