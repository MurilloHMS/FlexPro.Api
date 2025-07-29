using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Services;
using FlexPro.Infrastructure.Templates.Reports;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Companion;

namespace FlexPro.Api.Controllers
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

        
    }
}
