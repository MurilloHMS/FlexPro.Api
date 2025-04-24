using DocumentFormat.OpenXml.Drawing.Charts;
using ScottPlot;
using ScottPlot.TickGenerators;

namespace FlexPro.Api.Infrastructure.Templates.Reports.Components
{
    public static class ChartGenerator
    {
        public static string GenerateBarChartSvg<T>(IEnumerable<T> data, Func<T, double> valueSelector, Func<T, string> labelSelector, ScottPlot.Color color, string legendText, int width, int height,
            bool showBarLabels = false, float fontSize = 10, float marginBottom = 0.2f, float marginTop = 0.2f)
        {
            var plot = new Plot();
            var count = data.Count();
            var xBase = Enumerable.Range(0, count).Select(i => (double)i).ToArray();

            var bars = plot.Add.Bars(xBase, data.Select(valueSelector).ToArray());
            bars.Color = color;
            bars.LegendText = legendText;

            if (showBarLabels)
            {
                foreach(var bar in bars.Bars)
                {
                    bar.Label = bar.Value.ToString("N0");
                }
            }

            var ticks = xBase.Select((x, i) => new Tick(x, labelSelector(data.ElementAt(i)))).ToArray();
            plot.Axes.Bottom.TickGenerator = new NumericManual(ticks);

            plot.Legend.IsVisible = false;
            plot.Axes.Bottom.TickLabelStyle.FontSize = fontSize;
            plot.Grid.XAxisStyle.IsVisible = false;
            plot.Axes.Margins(bottom: marginBottom, top: marginTop);

            return plot.GetSvgXml(width, height);
        }
    }
}
