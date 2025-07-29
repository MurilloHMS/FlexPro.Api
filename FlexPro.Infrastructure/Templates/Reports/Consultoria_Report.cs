using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ScottPlot;
using Color = QuestPDF.Infrastructure.Color;
using Colors = QuestPDF.Helpers.Colors;


namespace FlexPro.Infrastructure.Templates.Reports;

public class Consultoria_Report : IDocument
{
    public List<DadosRelatorio> DadosRelatorios { get; set; }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;
    public Consultoria_Report()
    {
        DadosRelatorios = new List<DadosRelatorio>
        {
            new DadosRelatorio{ Mes = new DateTime(2025,1,1), Produto = "teste", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025,1,1), Produto = "teste2", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 2, 1), Produto = "teste4", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 2, 1), Produto = "teste2", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 2, 1), Produto = "teste2", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 5, 1), Produto = "teste2", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 5, 1), Produto = "teste4", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 2, 1), Produto = "teste2", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 3, 1), Produto = "teste", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 4, 1), Produto = "teste4", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 2, 1), Produto = "teste2", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12},
            new DadosRelatorio{ Mes = new DateTime(2025, 4, 1), Produto = "teste2", Quantidade= 12, Valor= 12.55, NumeroRefeicoes= 15676, CustoPorRefeicao = 0.12}
        };
        
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(0);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContentHero);
            page.Footer().Element(ComposeFooter);
        });

        container.Page(page =>
        {
            page.Margin(0);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        var logo = Properties.Resources.Logo_Proauto;
        container.Row(row =>
        {
            row.ConstantItem(300).Column(col =>
            {
                col.Item().Height(15).Background(Color.FromHex("#1f305b"));
                col.Item().Height(15).Background(Color.FromHex("#01a396"));
            });
            row.RelativeItem().AlignRight().PaddingTop(2).Row(row =>
            {
                row.ConstantItem(290).PaddingRight(10).Height(30).Image(logo).FitWidth();
            });
        });
    }

    private void ComposeContentHero(IContainer container)
    {
        container.PaddingHorizontal(40).PaddingTop(60).Column(col =>
        {
            var logoCliente = Properties.Resources.Logo_Proauto;
            var logoPaginaInicial = Properties.Resources.Capa_Consultoria;
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Relatório de Consultoria").FontSize(16);
                    col.Item().Width(212).LineHorizontal(5).LineColor(Color.FromHex("#01a396"));
                    col.Item().Text("Consultoria em higienização profissional.");
                    col.Item().Text("");
                    col.Item().Text("2024");
                });
                row.RelativeItem().AlignCenter().Row(row =>
                {
                    row.ConstantItem(200).PaddingRight(10).AlignMiddle().AlignCenter().Image(logoCliente).FitWidth();
                });
            });

            col.Item().PaddingTop(90).Image(logoPaginaInicial).FitWidth();

        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingHorizontal(30).PaddingTop(60).Column(col =>
        {
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Consultoria").Bold().FontSize(14);
                    col.Item().Text("Agende uma visita com nossa equipe de consultores, para mapeamento dos produtos e processos que envolvem Higiene e Limpeza").FontSize(11);
                });
                row.RelativeItem().AlignCenter().Row(row =>
                {
                    row.ConstantItem(200).Height(85).PaddingRight(10).Image(Properties.Resources.consultoria_imagem).FitUnproportionally();
                });
            });

            col.Item().PaddingTop(30).Row(row =>
            {
                row.ConstantColumn(300).Column(col =>
                {
                    col.Item().AspectRatio(2).Svg(size =>
                    {
                        Plot plot = new();

                        var mesValores = DadosRelatorios.GroupBy(a => a.Mes).Select(g => new
                        {
                            Mes = g.Key,
                            ValorTotal = g.Sum(x => x.Valor)
                        }).ToList();

                        double barSpacing = 1.0;
                        double barWidth = 0.05;

                        double[] xbase = Enumerable.Range(0, mesValores.Count).Select(i => (double)i).ToArray();

                        var totalBar = plot.Add.Bars(xbase.Select(x => x - barWidth).ToArray(), mesValores.Select(y => y.ValorTotal).ToArray());
                        totalBar.Color = ScottPlot.Colors.Blue;
                        totalBar.LegendText = "Valor Total";
                        
                        foreach(var bar in totalBar.Bars)
                        {
                            bar.Label = bar.Value.ToString("F2");
                        }

                        var ticks = xbase.Select((x, i) => new Tick(x, mesValores[i].Mes.ToString("MMM"))).ToArray();
                        plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);

                        plot.Legend.IsVisible = false;
                        plot.Legend.Alignment = Alignment.UpperCenter;
                        plot.Axes.Bottom.TickLabelStyle.FontSize = 10;
                        plot.Grid.XAxisStyle.IsVisible = false;
                        plot.Axes.Margins(bottom: 0 , top: .4);


                        return plot.GetSvgXml((int)size.Width, (int)size.Height);
                    });

                });
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Indicadores de consumo mês").Bold().FontSize(14);
                    col.Item().Text("Acompanhamento de consumo para identificação de inconformidades e pontos de melhorias para aplicação de ferramentas de controle de consumo e redução de custos");
                });
            });

            col.Item().PaddingTop(30).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Indicadores de consumo litros").Bold().FontSize(14);
                    col.Item().Text("Análize mês a mês para identificar pontos de maior consumo.");
                });
                row.ConstantColumn(350).Column(col =>
                {
                    col.Item().Table(table =>
                    {
                        // Pega todos os meses distintos em ordem
                        var meses = DadosRelatorios.Select(x => x.Mes).Distinct().OrderBy(x => x).ToList();

                        // Agrupa por produto, e calcula as quantidades por mês
                        var produtosAgrupados = DadosRelatorios
                            .GroupBy(x => x.Produto)
                            .Select(g => new
                            {
                                Produto = g.Key,
                                QuantidadesPorMes = meses.ToDictionary(
                                    mes => mes,
                                    mes => g.Where(x => x.Mes == mes).Sum(x => x.Quantidade)
                                ),
                                Total = g.Sum(x => x.Quantidade)
                            })
                            .ToList();

                        // Definição das colunas
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100); // Produto
                            foreach (var mes in meses)
                            {
                                columns.RelativeColumn(); // Um por mês
                            }
                            columns.RelativeColumn(); // Total
                        });

                        // Cabeçalho
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Border(1).Padding(5).Text("Produto").AlignCenter().FontSize(10);
                            foreach (var mes in meses)
                            {
                                header.Cell().Element(CellStyle).Border(1).Padding(5).Text(mes.ToString("MMM")).AlignCenter().FontSize(10);
                            }
                            header.Cell().Element(CellStyle).Border(1).Padding(5).Text("Total").AlignCenter().FontSize(10);

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .Background(Colors.Blue.Darken2)
                                    .DefaultTextStyle(x => x.FontColor(Colors.White).Bold());
                            }
                        });
                        uint linha = 1;
                        // Corpo da tabela
                        foreach (var item in produtosAgrupados)
                        {
                            table.Cell().Element(CellStyle).Border(1).Padding(5).Text(item.Produto).FontSize(9);
                            foreach (var mes in meses)
                            {
                                var valor = item.QuantidadesPorMes[mes];
                                table.Cell().Element(CellStyle).Border(1).Padding(5).Text($"{valor.ToString()}LT").FontSize(9).AlignCenter();
                            }
                            table.Cell().Element(CellStyle).Border(1).Padding(5).Text(item.Total.ToString()).FontSize(9).AlignCenter();

                            IContainer CellStyle(IContainer container)
                            {
                                var backgroundColor = linha % 2 == 0
                                    ? Colors.Blue.Lighten5
                                    : Colors.Blue.Lighten4;

                                return container
                                    .Background(backgroundColor);
                            }
                            linha++;
                        }
                    });

                });
            });

            col.Item().PaddingTop(30).Column(col =>
            {
                col.Item().Text("Indicadores de valores gasto por mês por produto").Bold().FontSize(14);
                col.Item().Text("Identificar os itens de maior consumo e realizar treinamentos para garantir o uso de forma correta");
            });

            col.Item().PaddingTop(5).Row(row =>
            {

                var meses = DadosRelatorios.Select(x => x.Mes).Distinct().OrderBy(x => x).ToList();
                var produtosAgrupados = DadosRelatorios
                    .GroupBy(x => x.Produto)
                    .Select(g => new
                    {
                        Produto = g.Key,
                        ValoresPorMes = meses.ToDictionary(
                            mes => mes,
                            mes => g.Where(x => x.Mes == mes).Sum(x => x.Valor)
                        ),
                        Total = g.Sum(x => x.Valor)
                    })
                    .ToList();

                row.ConstantColumn(350).Column(col =>
                {
                    col.Item().Table(table =>
                    {

                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(90);
                            foreach (var mes in meses)
                            {
                                columns.RelativeColumn(); 
                            }
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Border(1).Padding(5).Text("Produto").AlignCenter().FontSize(10);
                            foreach (var mes in meses)
                            {
                                header.Cell().Element(CellStyle).Border(1).Padding(5).Text(mes.ToString("MMM")).AlignCenter().FontSize(10);
                            }
                            header.Cell().Element(CellStyle).Border(1).Padding(5).Text("Total").AlignCenter().FontSize(10);

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .Background(Colors.Blue.Darken2)
                                    .DefaultTextStyle(x => x.FontColor(Colors.White).Bold());
                            }
                        });
                        uint linha = 1;
                        foreach (var item in produtosAgrupados)
                        {
                            table.Cell().Element(CellStyle).Border(1).Padding(5).Text(item.Produto).FontSize(9);
                            foreach (var mes in meses)
                            {
                                var valor = item.ValoresPorMes[mes];
                                table.Cell().Element(CellStyle).Border(1).Padding(5).Text($"{valor.ToString("N2")}").FontSize(9).AlignCenter();
                            }
                            table.Cell().Element(CellStyle).Border(1).Padding(5).Text(item.Total.ToString("N2")).FontSize(9).AlignCenter();

                            IContainer CellStyle(IContainer container)
                            {
                                var backgroundColor = linha % 2 == 0
                                    ? Colors.Blue.Lighten5
                                    : Colors.Blue.Lighten4;

                                return container
                                    .Background(backgroundColor);
                            }
                            linha++;
                        }
                    });
                });
                row.RelativeColumn().Column(col =>
                {
                    col.Item().ScaleToFit().Height(105).Svg(size =>
                    {
                        Plot plot = new();
                        var meses = DadosRelatorios.Select(x => x.Mes).Distinct().OrderBy(x => x).ToList();
                        var produtosAgrupados = DadosRelatorios
                            .GroupBy(x => x.Produto)
                            .Select(g => new
                            {
                                Produto = g.Key,
                                Total = g.Sum(x => x.Valor)
                            }).ToList();

                        double[] slices = produtosAgrupados.Select(x => x.Total).ToArray();
                        var cores = new[]
                        {
                            Colors.Blue.Medium.Hex,
                            Colors.Green.Medium.Hex,
                            Colors.Yellow.Medium.Hex,
                            Colors.Red.Medium.Hex,
                            Colors.Grey.Medium.Hex,
                            Colors.Purple.Medium.Hex
                        };

                        var PieSlices = produtosAgrupados.Select((x, i) => new PieSlice
                        {
                            Value = x.Total,
                            Label = x.Produto,
                            FillColor = new ScottPlot.Color(cores[i % cores.Length])
                        }).ToArray();

                        var pie = plot.Add.Pie(PieSlices);
                        pie.DonutFraction = 0.5;
                        pie.SliceLabelDistance = 1.5;
                        pie.LineColor = ScottPlot.Colors.White;
                        pie.LineWidth = 3;


                        plot.Axes.Frameless();
                        plot.HideGrid();

                        return plot.GetSvgXml((int)size.Width, (int)size.Height);
                    });
                });
            });

            col.Item().PaddingTop(30).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("");
                });
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Controle de custo por refeição").Bold().FontSize(14);
                    col.Item().Text("Controle e garantia que o custo servido não vai ser diferente a cada ponto de atendimento.");
                });
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Background(Color.FromHex("#1f305b")).Height(20)
            .PaddingHorizontal(10).Row(row =>
            {
                row.ConstantItem(400).AlignLeft().AlignMiddle()
                    .Text("Proauto Kimium - Av. João do prado, 300 - Santo André - SP 11 4576-7181")
                    .FontSize(10).FontColor(Colors.White);

                row.RelativeItem().AlignRight().AlignMiddle().Text("www.proautokiumium.com.br")
                    .FontSize(10).FontColor(Colors.White);
            });
    }
}

public class DadosRelatorio
{
    public DateTime Mes { get; set; }
    public string Produto { get; set; }
    public double Quantidade { get; set; }
    public double Valor { get; set; }
    public int NumeroRefeicoes { get; set; }
    public double CustoPorRefeicao { get; set; }
}