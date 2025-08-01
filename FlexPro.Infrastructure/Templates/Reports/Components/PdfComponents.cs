using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FlexPro.Infrastructure.Templates.Reports.Components;

public static class PdfComponents
{
    public static void Header(IContainer container, byte[] logoImage)
    {
        container.Row(row =>
        {
            row.ConstantItem(300).Column(col =>
            {
                col.Item().Height(15).Background(Color.FromHex("#1f305b"));
                col.Item().Height(15).Background(Color.FromHex("#01a396"));
            });
            row.RelativeItem().AlignRight().PaddingTop(2).Row(descriptor =>
            {
                descriptor.ConstantItem(290).PaddingRight(10).Height(30).Image(logoImage).FitWidth();
            });
        });
    }

    public static void Footer(IContainer container)
    {
        container.Background(Color.FromHex("#1f305b")).Height(20).PaddingHorizontal(10).Row(row =>
        {
            row.ConstantItem(400).AlignLeft().AlignMiddle()
                .Text("FlexPro • Sistema de Automação").FontSize(10).FontColor(Colors.White);
            row.RelativeItem().AlignRight().AlignMiddle()
                .Text($"Relatório emitido em {DateTime.Today:dd/MM/yyyy}").FontSize(10).FontColor(Colors.White);
        });
    }
}