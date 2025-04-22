using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Services;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Collections.Generic;
using System.Resources;
using ScottPlot;
using Colors = QuestPDF.Helpers.Colors;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MimeKit.Cryptography;


namespace FlexPro.Api.Infrastructure.Templates.Reports
{
    public class FuelSuppy_Report : IDocument
    {
        private readonly AbastecimentoService _abastecimentoService;
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        private List<Abastecimento> _abastecimentosLista;

        public string MetricasGeral { get; set; }
        
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public FuelSuppy_Report(AbastecimentoService service, IAbastecimentoRepository repository, List<Abastecimento> abastecimentos)
        {
            _abastecimentoService = service;
            _abastecimentoRepository = repository;
            _abastecimentosLista = abastecimentos;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(15);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });

            container.Page(page =>
            {
                page.Margin(15);
                page.Header().Element(ComposeHeader);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            var logo = Properties.Resources.Logo_Proauto;
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Relatório de Abastecimento").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                    column.Item().Text($"Data de Emissão: {DateTime.Now:dd/MM/yyyy}")
                    .FontSize(10)
                    .FontColor(Colors.Grey.Darken2);
                });

                row.ConstantItem(200).Height(50).Image(logo).FitWidth();
            });
        }

        private void ComposeContent(IContainer container)
        {
            
            container.Padding(20)
                .Column(col =>
                {
                    col.Item().PaddingBottom(20).Text("Controle Geral").FontSize(24).Bold().AlignCenter();

                    col.Item().AspectRatio(2)
                        .Svg(size =>
                        {
                            Plot plot = new();

                            var dados = _abastecimentosLista.GroupBy(a => a.Departamento).Select(g => new
                            {
                                Departamento = g.Key,
                                Litros = g.Sum(a => a.Litros),
                                KmPercorridos = g.Sum(a => a.DiferencaHodometro),
                                TotalGasto = g.Sum(a => (double)a.ValorTotalTransacao),
                                MediaKmL = g.Average(a => a.MediaKm)
                            }).ToList();

                            int departamentosCount = dados.Count;
                            double barSpacing = 1.0;
                            double barWidth = 0.3;

                            double[] xBase = Enumerable.Range(0, departamentosCount).Select(i => (double)i).ToArray();

                            var barsLitros = plot.Add.Bars(xBase.Select(x => x - barWidth).ToArray(), dados.Select(d => d.Litros).ToArray());
                            barsLitros.Color = ScottPlot.Colors.Blue;
                            barsLitros.LegendText = "Litros";

                            var barsKm = plot.Add.Bars(xBase, dados.Select(d => d.KmPercorridos).ToArray());
                            barsKm.Color = ScottPlot.Colors.Green;
                            barsKm.LegendText = "KM";

                            var barsGasto = plot.Add.Bars(xBase.Select(x => x + barWidth).ToArray(), dados.Select(d => d.TotalGasto).ToArray());
                            barsGasto.Color = ScottPlot.Colors.Orange;
                            barsGasto.LegendText = "R$ Gasto";

                            

                            var ticks = xBase.Select((x, i) => new Tick(x, dados[i].Departamento)).ToArray();
                            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);

                            // Estilo
                            plot.Legend.IsVisible = true;
                            plot.Legend.Alignment = Alignment.UpperRight;
                            plot.Axes.Bottom.TickLabelStyle.FontSize = 10;
                            plot.Grid.XAxisStyle.IsVisible = false;
                            plot.Axes.Margins(bottom: 0.2f, top: 0.2f);

                            return plot.GetSvgXml((int)size.Width, (int)size.Height);
                        });

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(col =>
                        {
                            col.ConstantColumn(120);
                            col.ConstantColumn(100);
                            col.ConstantColumn(100);
                            col.ConstantColumn(100);
                            col.ConstantColumn(100);
                        });

                        table.Header(h =>
                        {
                            h.Cell().BorderBottom(2).Padding(8).Text("Departamentos").AlignCenter();
                            h.Cell().BorderBottom(2).Padding(8).Text("Litros").AlignCenter();
                            h.Cell().BorderBottom(2).Padding(8).Text("Km Percorrido").AlignCenter();
                            h.Cell().BorderBottom(2).Padding(8).Text("Total Gasto").AlignCenter();
                            h.Cell().BorderBottom(2).Padding(8).Text("Média KM").AlignCenter();
                        });

                        var dados = _abastecimentosLista.GroupBy(a => a.Departamento).Select(g => new
                        {
                            Departamento = g.Key,
                            Litros = g.Sum(a => a.Litros),
                            KmPercorridos = g.Sum(a => a.DiferencaHodometro),
                            TotalGasto = g.Sum(a => (double)a.ValorTotalTransacao),
                            MediaKmL = g.Average(a => a.MediaKm)
                        }).ToList();

                        foreach (var i in dados)
                        {
                            table.Cell().Padding(8).Text(i.Departamento).AlignCenter().FontSize(10);
                            table.Cell().Padding(8).Text(i.Litros.ToString("N0")).AlignCenter();
                            table.Cell().Padding(8).AlignCenter().Text(i.KmPercorridos);
                            table.Cell().Padding(8).Text($"R$ {i.TotalGasto.ToString("N")}").AlignCenter();
                            table.Cell().Padding(8).Text(i.MediaKmL.ToString("N0")).AlignCenter();
                        }
                    });

                    col.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().Text("Metricas").FontSize(14);
                        col.Item().Text(MetricasGeral).FontSize(11);
                    });
                });
            
        }

        private void ComposeFooter(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    col.Item().Text("FlexPro • Sistema de Automação").FontSize(9).FontColor(Colors.Grey.Darken1);
                });

                row.ConstantItem(100).AlignRight().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        }
    }
}
