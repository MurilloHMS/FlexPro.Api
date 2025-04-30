using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Services;
using FlexPro.Api.Infrastructure.Templates.Reports;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Companion;
using QuestPDF.Fluent;

namespace FlexPro.Api.API.Controllers
{
    [ApiController]
    [Route("preview/report")]
    public class ReportPreviewController : ControllerBase
    {
        private readonly AbastecimentoService _abastecimentoService;
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        private readonly IReportService _reportService;

        public ReportPreviewController(AbastecimentoService abastecimentoService, IAbastecimentoRepository abastecimentoRepository,IReportService reportService)
        {
            _reportService = reportService;
            _abastecimentoService = abastecimentoService;
            _abastecimentoRepository = abastecimentoRepository;
        }

        [HttpGet("preview")]
        public async Task<IActionResult> PreviewReport()
        {
            var startDate = new DateTime(2025, 1, 1).ToUniversalTime();
            var endDate = new DateTime(2025, 1, 31).ToUniversalTime();
            var abastecimentos = await _abastecimentoRepository.GetFuelSupplyByDate(startDate, endDate);
            var metricsResult = await _abastecimentoService.CalcularMetricasAbastecimento(startDate);
            var report = new FuelSupplyReport(abastecimentos, GetLogoImage(), GetPostoImage(), metricsResult);
            report.ShowInCompanion();
            return Ok("Report preview generated successfully.");
        }

        [HttpGet("consultoria")]
        public async Task<IActionResult> Consultoria()
        {
            var report = new Consultoria_Report();
            report.ShowInCompanion();
            return Ok("Report preview generated successfully.");
        }
        [HttpGet("fuel-supply")]
        public async Task<IActionResult> GetFuelSupplyReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var pdf = await _reportService.GenerateFuelSupplyReportAsync(startDate.ToUniversalTime());
            return File(pdf, "application/pdf", "FuelSupplyReport.pdf");
        }

        private byte[] GetLogoImage() => Properties.Resources.Logo_Proauto;
        private byte[] GetPostoImage() => Properties.Resources.Posto03;
    }
}
