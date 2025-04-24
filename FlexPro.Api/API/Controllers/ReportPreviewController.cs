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

        public ReportPreviewController(AbastecimentoService abastecimentoService, IAbastecimentoRepository abastecimentoRepository)
        {
            _abastecimentoService = abastecimentoService;
            _abastecimentoRepository = abastecimentoRepository;
        }
        [HttpGet]
        public async Task<IActionResult> PreviewReport()
        {
            var abastecimentos = await _abastecimentoRepository.GetFuelSupplyByDate(new DateTime(2025, 1, 1).ToUniversalTime(), new DateTime(2025, 1, 31).ToUniversalTime());
            var report = new FuelSuppy_Report(_abastecimentoService, _abastecimentoRepository, abastecimentos);
            report.MetricasGeral = "Abastecimento Geral\r\nQuantidade de litros abastecidos: aumentou em 10,63% de 6.568,647 para 7.266,577\r\nMédia de KM/L: aumentou em 0,97% de 12,232 para 12,351\r\nMédia de Preço/L: aumentou em 1,23% de R$ 5,45 para R$ 5,52\r\nValor Total Gasto: aumentou em 11,48% de R$ 36.018,95 para R$ 40.152,77\r\nDistancia percorrida: aumentou em 11,36% de 80.954 para 90.153\r\n\r\n";
            report.ShowInCompanion(); // Isso abre o PDF no Companion
            return Ok("Report preview generated successfully.");
        }

        [HttpGet("consultoria")]
        public async Task<IActionResult> Consultoria()
        {
            var report = new Consultoria_Report();
            report.ShowInCompanion();
            return Ok("Report preview generated successfully.");
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {

            var abastecimentos = await _abastecimentoRepository.GetFuelSupplyByDate(new DateTime(2025, 1, 1).ToUniversalTime(), new DateTime(2025, 1, 31).ToUniversalTime());
            var report = new FuelSuppy_Report(_abastecimentoService, _abastecimentoRepository, abastecimentos);
            report.MetricasGeral = "Abastecimento Geral\r\nQuantidade de litros abastecidos: aumentou em 10,63% de 6.568,647 para 7.266,577\r\nMédia de KM/L: aumentou em 0,97% de 12,232 para 12,351\r\nMédia de Preço/L: aumentou em 1,23% de R$ 5,45 para R$ 5,52\r\nValor Total Gasto: aumentou em 11,48% de R$ 36.018,95 para R$ 40.152,77\r\nDistancia percorrida: aumentou em 11,36% de 80.954 para 90.153\r\n\r\n";

            var stream = new MemoryStream();
            report.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream, "application/pdf", "Relatorio-consultoria.pdf");
        }



    }
}
