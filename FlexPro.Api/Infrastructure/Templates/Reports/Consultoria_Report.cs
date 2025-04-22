using DocumentFormat.OpenXml.Drawing.Charts;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace FlexPro.Api.Infrastructure.Templates.Reports;

public class Consultoria_Report : IDocument
{
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;
    public Consultoria_Report()
    {
        
    }

    public void Compose(IDocumentContainer container)
    {
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

    private void ComposeContent(IContainer container)
    {
        container.Row(row =>
        {
            row.ConstantItem(130).Background(Color.FromHex("#01a396")).Height(10);
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