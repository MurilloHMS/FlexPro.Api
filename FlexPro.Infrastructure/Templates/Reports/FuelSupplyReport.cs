using FlexPro.Domain.Entities;
using FlexPro.Infrastructure.Services;
using FlexPro.Infrastructure.Templates.Reports.Components;
using FlexPro.Infrastructure.Templates.Reports.Constants;
using FlexPro.Infrastructure.Templates.Reports.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;

namespace FlexPro.Infrastructure.Templates.Reports;

public class FuelSupplyReport : IDocument
{
    private readonly List<Abastecimento> _abastecimentosLista;
    private readonly List<AbastecimentoMetrics> _departmentMetrics;
    private readonly byte[] _logoImage;
    private readonly AbastecimentoMetricsResult _metricsResult;
    private readonly byte[] _postoImage;

    public FuelSupplyReport(List<Abastecimento> abastecimentos, byte[] logoImage, byte[] postoImage,
        AbastecimentoMetricsResult metricsResult)
    {
        _abastecimentosLista = abastecimentos ?? throw new ArgumentNullException(nameof(abastecimentos));
        if (!abastecimentos.Any())
            throw new ArgumentException("A lista de abastecimentos não pode estar vazia.", nameof(abastecimentos));
        _logoImage = logoImage ?? throw new ArgumentNullException(nameof(logoImage));
        _postoImage = postoImage ?? throw new ArgumentNullException(nameof(postoImage));
        _departmentMetrics = AbastecimentoMetrics.CalculateMetrics(abastecimentos).ToList();
        _metricsResult = metricsResult ?? throw new ArgumentNullException(nameof(metricsResult));
    }

    public string MetricasGeral { get; set; }

    public DocumentMetadata GetMetadata()
    {
        return DocumentMetadata.Default;
    }

    public DocumentSettings GetSettings()
    {
        return DocumentSettings.Default;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(0);
            page.Header().Element(c => PdfComponents.Header(c, _logoImage));
            page.Content().Element(ComposeContentHero);
            page.Footer().Element(PdfComponents.Footer);
        });

        container.Page(page =>
        {
            page.Margin(0);
            page.Header().Element(c => PdfComponents.Header(c, _logoImage));
            page.Content().Element(ComposeContentGeral);
            page.Footer().Element(PdfComponents.Footer);
        });

        container.Page(page =>
        {
            page.Margin(0);
            page.Header().Element(c => PdfComponents.Header(c, _logoImage));
            page.Content().Element(ComposeContentSetor);
            page.Footer().Element(PdfComponents.Footer);
        });
    }

    private void ComposeContentHero(IContainer container)
    {
        container.PaddingHorizontal(40).PaddingTop(120).Column(col =>
        {
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Relatório de Abastecimento").FontSize(ReportStyles.SubtitleFontSize);
                    col.Item().Width(212).LineHorizontal(5).LineColor(ReportStyles.AccentColor);
                    col.Item().Text("Abastecimento de veículos.");
                    col.Item().Text("");
                    col.Item().Text("2025");
                });
            });
            col.Item().PaddingTop(20).Image(_postoImage).FitWidth();
        });
    }

    private void ComposeContentGeral(IContainer container)
    {
        container.Padding(20).Column(col =>
        {
            col.Item().PaddingBottom(20).Text("Abastecimento Geral").FontSize(ReportStyles.TitleFontSize).Bold()
                .AlignCenter();

            col.Item().Row(row =>
            {
                row.ConstantColumn(550).Column(c =>
                {
                    c.Item().Text("Litros Abastecidos").AlignCenter();
                    c.Item().AspectRatio(2).Svg(size =>
                        ChartGenerator.GenerateBarChartSvg(
                            _departmentMetrics,
                            d => d.Litros,
                            d => d.Departamento,
                            ScottPlot.Colors.Blue,
                            "Litros",
                            (int)size.Width,
                            (int)size.Height,
                            true,
                            7,
                            marginTop: 0.5f));
                });
            });

            col.Item().Row(row =>
            {
                row.ConstantColumn(550).Column(c =>
                {
                    c.Item().Text("Total Gasto").AlignCenter();
                    c.Item().AspectRatio(3).Svg(size =>
                        ChartGenerator.GenerateBarChartSvg(
                            _departmentMetrics,
                            d => d.TotalGasto,
                            d => d.Departamento,
                            ScottPlot.Colors.Green,
                            "Total Gasto",
                            (int)size.Width,
                            (int)size.Height,
                            true,
                            7,
                            marginTop: 0.5f));
                });
            });

            col.Item().Row(row =>
            {
                row.ConstantColumn(550).Column(c =>
                {
                    c.Item().Text("Distância Percorrida").AlignCenter();
                    c.Item().AspectRatio(3).Svg(size =>
                        ChartGenerator.GenerateBarChartSvg(
                            _departmentMetrics,
                            d => d.KmPercorridos,
                            d => d.Departamento,
                            ScottPlot.Colors.Purple,
                            "Distância Percorrida",
                            (int)size.Width,
                            (int)size.Height,
                            true,
                            7,
                            marginTop: 0.5f));
                });
            });

            col.Item().Row(row =>
            {
                row.ConstantColumn(550).Column(c =>
                {
                    c.Item().PaddingTop(40).Text("Média KM/L").AlignCenter();
                    c.Item().AspectRatio(3).Svg(size =>
                        ChartGenerator.GenerateBarChartSvg(
                            _departmentMetrics,
                            d => d.MediaKmL,
                            d => d.Departamento,
                            ScottPlot.Colors.Orange,
                            "Média KM/L",
                            (int)size.Width,
                            (int)size.Height,
                            true,
                            7,
                            marginTop: 0.5f));
                });
            });

            col.Item().Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(120);
                    cols.ConstantColumn(100);
                    cols.ConstantColumn(100);
                    cols.ConstantColumn(100);
                    cols.ConstantColumn(100);
                });

                table.Header(h =>
                {
                    h.Cell().BorderBottom(2).Padding(8).Text("Departamentos").AlignCenter();
                    h.Cell().BorderBottom(2).Padding(8).Text("Litros").AlignCenter();
                    h.Cell().BorderBottom(2).Padding(8).Text("Km Percorrido").AlignCenter();
                    h.Cell().BorderBottom(2).Padding(8).Text("Total Gasto").AlignCenter();
                    h.Cell().BorderBottom(2).Padding(8).Text("Média KM").AlignCenter();
                });

                foreach (var i in _departmentMetrics)
                {
                    table.Cell().Padding(8).Text(i.Departamento).AlignCenter().FontSize(ReportStyles.DefaultFontSize);
                    table.Cell().Padding(8).Text(i.Litros.ToString("N0")).AlignCenter();
                    table.Cell().Padding(8).Text(i.KmPercorridos.ToString("N0")).AlignCenter();
                    table.Cell().Padding(8).Text($"R$ {i.TotalGasto:N}").AlignCenter();
                    table.Cell().Padding(8).Text(i.MediaKmL.ToString("N0")).AlignCenter();
                }
            });

            col.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(c =>
            {
                c.Spacing(5);
                c.Item().Text("Métricas").FontSize(14);
                c.Item().Text(_metricsResult.MetricasGeral).FontSize(ReportStyles.DefaultFontSize);
            });
        });
    }

    private void ComposeContentSetor(IContainer container)
    {
        container.Padding(20).Column(col =>
        {
            foreach (var departamento in _departmentMetrics)
            {
                col.Item().PaddingBottom(10).Text($"Abastecimento Setor - {departamento.Departamento}")
                    .FontSize(ReportStyles.TitleFontSize).Bold().AlignCenter();

                var funcionarios = _abastecimentosLista
                    .Where(x => x.Departamento == departamento.Departamento)
                    .GroupBy(y => y.NomeDoMotorista)
                    .Select(x => new
                    {
                        Funcionario = x.Key,
                        Litros = x.Sum(x => x.Litros),
                        Total = x.Sum(x => x.ValorTotalTransacao),
                        DistanciaPercorrida = x.Sum(x => x.DiferencaHodometro),
                        MediaKmL = x.Average(x => x.MediaKm)
                    }).ToList();

                col.Item().Row(row =>
                {
                    row.ConstantColumn(ReportConstants.DepartmentChartWidth).Column(c =>
                    {
                        c.Item().Text("Litros Abastecidos").AlignCenter();
                        c.Item().AspectRatio(4).Svg(size =>
                            ChartGenerator.GenerateBarChartSvg(
                                funcionarios,
                                f => f.Litros,
                                f => FormatName(f.Funcionario),
                                ScottPlot.Colors.Blue,
                                "Litros",
                                (int)size.Width,
                                (int)size.Height,
                                true,
                                6,
                                marginTop: 0.5f));
                    });
                });

                col.Item().Row(row =>
                {
                    row.ConstantColumn(ReportConstants.DepartmentChartWidth).Column(c =>
                    {
                        c.Item().Text("Total Gasto").AlignCenter();
                        c.Item().AspectRatio(4).Svg(size =>
                            ChartGenerator.GenerateBarChartSvg(
                                funcionarios,
                                f => (double)f.Total,
                                f => FormatName(f.Funcionario),
                                ScottPlot.Colors.Green,
                                "Total Gasto",
                                (int)size.Width,
                                (int)size.Height,
                                true,
                                6,
                                marginTop: 0.5f));
                    });
                });

                col.Item().Row(row =>
                {
                    row.ConstantColumn(ReportConstants.DepartmentChartWidth).Column(c =>
                    {
                        c.Item().Text("Distância Percorrida").AlignCenter();
                        c.Item().AspectRatio(4).Svg(size =>
                            ChartGenerator.GenerateBarChartSvg(
                                funcionarios,
                                f => f.DistanciaPercorrida,
                                f => FormatName(f.Funcionario),
                                ScottPlot.Colors.Purple,
                                "Distância Percorrida",
                                (int)size.Width,
                                (int)size.Height,
                                true,
                                6,
                                marginTop: 0.5f));
                    });
                });

                col.Item().Row(row =>
                {
                    row.ConstantColumn(ReportConstants.DepartmentChartWidth).Column(c =>
                    {
                        c.Item().Text("Média KM/L").AlignCenter();
                        c.Item().AspectRatio(4).Svg(size =>
                            ChartGenerator.GenerateBarChartSvg(
                                funcionarios,
                                f => f.MediaKmL,
                                f => FormatName(f.Funcionario),
                                ScottPlot.Colors.Orange,
                                "Média KM/L",
                                (int)size.Width,
                                (int)size.Height,
                                true,
                                6,
                                marginTop: 0.5f));
                    });
                });

                col.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(c =>
                {
                    c.Spacing(5);
                    c.Item().Text("Métricas").FontSize(14);
                    c.Item().Text(
                        _metricsResult.MetricasPorDepartamento.TryGetValue(departamento.Departamento, out var metricas)
                            ? metricas
                            : "Sem métricas disponíveis").FontSize(ReportStyles.DefaultFontSize);
                });
            }
        });
    }

    private static string FormatName(string nome)
    {
        var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return partes.Length > 1 ? $"{partes[0]} {partes[1][0]}." : partes[0];
    }
}