namespace FlexPro.Infrastructure.Services;

public class AbastecimentoMetricsResult
{
    public string MetricasGeral { get; set; } = string.Empty;
    public Dictionary<string, string> MetricasPorDepartamento { get; set; } = new();
}