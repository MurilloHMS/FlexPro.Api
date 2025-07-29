using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Services;
using FlexPro.Infrastructure.Templates.Reports.Components;
using FlexPro.Infrastructure.Templates.Reports.Constants;
using FlexPro.Infrastructure.Templates.Reports.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ScottPlot;
using ScottPlot.TickGenerators;
using Colors = QuestPDF.Helpers.Colors;
using Color = QuestPDF.Infrastructure.Color;


namespace FlexPro.Infrastructure.Templates.Reports
{
    public class FuelSuppy_Report : IDocument
    {
        private readonly AbastecimentoService _abastecimentoService;
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        private List<Abastecimento> _abastecimentosLista;
        private readonly byte[] _logoImage;
        private readonly byte[] _postoImage;
        private readonly List<AbastecimentoMetrics> _departmentMetrics;

        public string MetricasGeral { get; set; }
        
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public FuelSuppy_Report(AbastecimentoService service, IAbastecimentoRepository repository, List<Abastecimento> abastecimentos, byte[] logoImage, byte[] postoImage)
        {
            _abastecimentoService = service;
            _abastecimentoRepository = repository;
            _abastecimentosLista = abastecimentos ?? throw new ArgumentNullException(nameof(abastecimentos));
            if (!abastecimentos.Any())
                throw new ArgumentException("A lista de abastecimentos não pode estar vazia.", nameof(abastecimentos));
            _logoImage = logoImage ?? throw new ArgumentNullException(nameof(logoImage));
            _postoImage = postoImage ?? throw new ArgumentNullException(nameof(postoImage));
            _departmentMetrics = AbastecimentoMetrics.CalculateMetrics(abastecimentos).ToList();
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
                        col.Item().Text("Relatório de Abastecimento").FontSize(16);
                        col.Item().Width(212).LineHorizontal(5).LineColor(Color.FromHex("#01a396"));
                        col.Item().Text("Abastecimento de veiculos - Janeiro.");
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
                var dados = _abastecimentosLista.GroupBy(a => a.Departamento).Select(g => new
                {
                    Departamento = g.Key,
                    Litros = g.Sum(a => a.Litros),
                    KmPercorridos = g.Sum(a => a.DiferencaHodometro),
                    TotalGasto = g.Sum(a => (double)a.ValorTotalTransacao),
                    MediaKmL = g.Average(a => a.MediaKm)
                }).ToList();


                col.Item().PaddingBottom(20).Text("Abastecimento Geral").FontSize(ReportStyles.TitleFontSize).Bold().AlignCenter();
                col.Item().Row(row =>
                {
                    row.ConstantColumn(ReportConstants.ChartColumnWidth).Column(col =>
                    {
                        col.Item().Text("Litros Abastecidos").AlignCenter();
                        col.Item().AspectRatio(2).Svg(size =>
                            ChartGenerator.GenerateBarChartSvg(_departmentMetrics, d => d.Litros, d => d.Departamento, ScottPlot.Colors.Blue,
                                "Litros", (int)size.Width,(int)size.Height));
                    });
                    row.ConstantColumn(ReportConstants.ChartColumnWidth).Column(c =>
                    {
                        c.Item().Text("Total Gasto").AlignCenter();
                        c.Item().AspectRatio(2).Svg(size =>
                            ChartGenerator.GenerateBarChartSvg(
                                _departmentMetrics,
                                d => d.TotalGasto,
                                d => d.Departamento,
                                ScottPlot.Colors.Green,
                                "Total Gasto",
                                (int)size.Width,
                                (int)size.Height));
                    });
                });

                col.Item().Row(row =>
                {
                    row.ConstantColumn(270).Column(col =>
                    {
                        
                    });
                    row.ConstantColumn(270).Column(col =>
                    {
                        col.Item().Text("Média KM/L").AlignCenter();
                        col.Item().AspectRatio(2).Svg(size =>
                        {
                            Plot plot = new();

                            int departamentosCount = dados.Count;
                            double barSpacing = 1.0;
                            double barWidth = 0.3;

                            double[] xBase = Enumerable.Range(0, departamentosCount).Select(i => (double)i).ToArray();

                            var barsKm = plot.Add.Bars(xBase, dados.Select(d => d.MediaKmL).ToArray());
                            barsKm.Color = ScottPlot.Colors.Orange;
                            barsKm.LegendText = "Media KM/L";

                            var ticks = xBase.Select((x, i) => new Tick(x, dados[i].Departamento)).ToArray();
                            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);

                            // Estilo
                            plot.Legend.IsVisible = false;
                            plot.Legend.Alignment = Alignment.UpperRight;
                            plot.Axes.Bottom.TickLabelStyle.FontSize = 10;
                            plot.Grid.XAxisStyle.IsVisible = false;
                            plot.Axes.Margins(bottom: 0.2f, top: 0.2f);

                            return plot.GetSvgXml((int)size.Width, (int)size.Height);
                        });
                    });
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

        private void ComposeContentSetor(IContainer container)
        {
            var dados = _abastecimentosLista.GroupBy(a => a.Departamento).Select(g => new
            {
                Departamento = g.Key,
                Litros = g.Sum(a => a.Litros),
                KmPercorridos = g.Sum(a => a.DiferencaHodometro),
                TotalGasto = g.Sum(a => (double)a.ValorTotalTransacao),
                MediaKmL = g.Average(a => a.MediaKm)
            }).ToList();
            container.Padding(20).Column(col =>
            {
                foreach(var departamento in dados)
                {
                    
                    col.Item().PaddingBottom(10).Text($"Abastecimento Setor - {departamento.Departamento}").FontSize(24).Bold().AlignCenter();

                    var funcionarios = _abastecimentosLista.Where(x => x.Departamento == departamento.Departamento).GroupBy(y => y.NomeDoMotorista).Select(x => new
                    {
                        Funcionario = x.Key,
                        Litros = x.Sum(x => x.Litros),
                        Total = x.Sum(x => x.ValorTotalTransacao),
                        DistanciaPercorrida = x.Sum(x => x.DiferencaHodometro),
                        MediaKmL = x.Average(x => x.MediaKm)
                    }).ToList();

                    col.Item().Row(row =>
                    {
                        row.ConstantColumn(500).Column(col =>
                        {
                            col.Item().Text("Litros Abastecidos").AlignCenter();
                            col.Item().AspectRatio(4).Svg(size =>
                            {
                                Plot plot = new();

                                int funcionariosCount = funcionarios.Count;
                                double barSpacing = 1.0;
                                double barWidth = 0.3;

                                double[] xBase = Enumerable.Range(0, funcionariosCount).Select(i => (double)i).ToArray();

                                var barsLitro = plot.Add.Bars(xBase, funcionarios.Select(d => d.Litros).ToArray());
                                barsLitro.Color = ScottPlot.Colors.Blue;
                                barsLitro.LegendText = "Litros";


                                foreach (var bar in barsLitro.Bars)
                                {
                                    bar.Label = bar.Value.ToString("N0");
                                }

                                var ticks = xBase.Select((x, i) =>
                                {
                                    var nome = funcionarios[i].Funcionario;
                                    var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                    var nomeFormatado = partes.Length > 1
                                        ? $"{partes[0]} {partes[1][0]}."
                                        : partes[0];

                                    return new Tick(x, nomeFormatado);
                                }).ToArray();

                                plot.Axes.Bottom.TickGenerator = new NumericManual(ticks);

                                plot.Legend.IsVisible = false;
                                plot.Legend.Alignment = Alignment.UpperRight;
                                plot.Axes.Bottom.TickLabelStyle.FontSize = 8;
                                plot.Grid.XAxisStyle.IsVisible = false;
                                plot.Axes.Margins(bottom: 0.2f, top: 0.5f);

                                return plot.GetSvgXml((int)size.Width, (int)size.Height);
                            });
                        });
                    });
                    col.Item().Row(row =>
                    {
                        row.ConstantColumn(500).Column(col =>
                        {
                            col.Item().Text("Total Gasto").AlignCenter();
                            col.Item().AspectRatio(4).Svg(size =>
                            {
                                Plot plot = new();

                                int funcionariosCount = funcionarios.Count;
                                double barSpacing = 1.0;
                                double barWidth = 0.3;

                                double[] xBase = Enumerable.Range(0, funcionariosCount).Select(i => (double)i).ToArray();

                                var barsLitro = plot.Add.Bars(xBase, funcionarios.Select(d => d.Total).ToArray());
                                barsLitro.Color = ScottPlot.Colors.Green;
                                barsLitro.LegendText = "Total Gasto";


                                foreach (var bar in barsLitro.Bars)
                                {
                                    bar.Label = bar.Value.ToString("F2");
                                }

                                var ticks = xBase.Select((x, i) =>
                                {
                                    var nome = funcionarios[i].Funcionario;
                                    var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                    var nomeFormatado = partes.Length > 1
                                        ? $"{partes[0]} {partes[1][0]}."
                                        : partes[0];

                                    return new Tick(x, nomeFormatado);
                                }).ToArray();

                                plot.Axes.Bottom.TickGenerator = new NumericManual(ticks);

                                plot.Legend.IsVisible = false;
                                plot.Legend.Alignment = Alignment.UpperRight;
                                plot.Axes.Bottom.TickLabelStyle.FontSize = 8;
                                plot.Grid.XAxisStyle.IsVisible = false;
                                plot.Axes.Margins(bottom: 0.2f, top: 0.5f);

                                return plot.GetSvgXml((int)size.Width, (int)size.Height);
                            });
                        });
                    });
                    col.Item().Row(row =>
                    {
                        row.ConstantColumn(500).Column(col =>
                        {
                            col.Item().Text("Distancia Percorrida").AlignCenter();
                            col.Item().AspectRatio(4).Svg(size =>
                            {
                                Plot plot = new();

                                int funcionariosCount = funcionarios.Count;
                                double barSpacing = 1.0;
                                double barWidth = 0.3;

                                double[] xBase = Enumerable.Range(0, funcionariosCount).Select(i => (double)i).ToArray();

                                var barsLitro = plot.Add.Bars(xBase, funcionarios.Select(d => d.DistanciaPercorrida).ToArray());
                                barsLitro.Color = ScottPlot.Colors.Purple;
                                barsLitro.LegendText = "Distancia Percorrida";


                                foreach (var bar in barsLitro.Bars)
                                {
                                    bar.Label = bar.Value.ToString("N0");
                                }

                                var ticks = xBase.Select((x, i) =>
                                {
                                    var nome = funcionarios[i].Funcionario;
                                    var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                    var nomeFormatado = partes.Length > 1
                                        ? $"{partes[0]} {partes[1][0]}."
                                        : partes[0];

                                    return new Tick(x, nomeFormatado);
                                }).ToArray();

                                plot.Axes.Bottom.TickGenerator = new NumericManual(ticks);

                                plot.Legend.IsVisible = false;
                                plot.Legend.Alignment = Alignment.UpperRight;
                                plot.Axes.Bottom.TickLabelStyle.FontSize = 8;
                                plot.Grid.XAxisStyle.IsVisible = false;
                                plot.Axes.Margins(bottom: 0.2f, top: 0.5f);

                                return plot.GetSvgXml((int)size.Width, (int)size.Height);
                            });
                        });
                    });
                    col.Item().Row(row =>
                    {
                        row.ConstantColumn(500).Column(col =>
                        {
                            col.Item().Text("Media KM/L").AlignCenter();
                            col.Item().AspectRatio(4).Svg(size =>
                            {
                                Plot plot = new();

                                int funcionariosCount = funcionarios.Count;
                                double barSpacing = 1.0;
                                double barWidth = 0.3;

                                double[] xBase = Enumerable.Range(0, funcionariosCount).Select(i => (double)i).ToArray();

                                var barsLitro = plot.Add.Bars(xBase, funcionarios.Select(d => d.MediaKmL).ToArray());
                                barsLitro.Color = ScottPlot.Colors.Green;
                                barsLitro.LegendText = "Total Gasto";


                                foreach (var bar in barsLitro.Bars)
                                {
                                    bar.Label = bar.Value.ToString("N0");
                                }

                                var ticks = xBase.Select((x, i) =>
                                {
                                    var nome = funcionarios[i].Funcionario;
                                    var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                    var nomeFormatado = partes.Length > 1
                                        ? $"{partes[0]} {partes[1][0]}."
                                        : partes[0];

                                    return new Tick(x, nomeFormatado);
                                }).ToArray();

                                plot.Axes.Bottom.TickGenerator = new NumericManual(ticks);

                                plot.Legend.IsVisible = false;
                                plot.Legend.Alignment = Alignment.UpperRight;
                                plot.Axes.Bottom.TickLabelStyle.FontSize = 8;
                                plot.Grid.XAxisStyle.IsVisible = false;
                                plot.Axes.Margins(bottom: 0.2f, top: 0.5f);

                                return plot.GetSvgXml((int)size.Width, (int)size.Height);
                            });
                        });
                    });

                    col.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().Text("Metricas").FontSize(14);
                        col.Item().Text(MetricasGeral).FontSize(11);
                    });
                }
            });
        }

        private void ComposeFooter(IContainer container)
        {

            container.Background(Color.FromHex("#1f305b")).Height(20).PaddingHorizontal(10).Row(row =>
            {
                row.ConstantItem(400).AlignLeft().AlignMiddle()
                    .Text("FlexPro • Sistema de Automação").FontSize(10)
                    .FontColor(Colors.White);
                row.RelativeItem().AlignRight().AlignMiddle().Text($"Relatório emitido em {DateTime.Today:dd/MM/yyyy}")
                    .FontSize(10).FontColor(Colors.White);
            });
        }
    }
}
