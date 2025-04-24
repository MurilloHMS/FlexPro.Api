namespace FlexPro.Api.Infrastructure.Services
{
    public class AbastecimentoMetricsResult
    {
        public string MetricasGeral { get; set; }
        public Dictionary<string, string> MetricasPorDepartamento { get; set; } = new Dictionary<string, string>();
    }
}
