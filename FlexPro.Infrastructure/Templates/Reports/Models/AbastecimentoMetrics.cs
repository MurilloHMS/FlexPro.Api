using FlexPro.Domain.Entities;

namespace FlexPro.Infrastructure.Templates.Reports.Models
{
    public class AbastecimentoMetrics
    {
        public string Departamento { get; set; }
        public double Litros { get; set; }
        public double KmPercorridos { get; set; }
        public double TotalGasto { get; set; }
        public double MediaKmL { get; set; }

        public static IEnumerable<AbastecimentoMetrics> CalculateMetrics(IEnumerable<Abastecimento> abastecimentos)
        {
            return abastecimentos
                .GroupBy(a => a.Departamento)
                .Select(g => new AbastecimentoMetrics
                {
                    Departamento = g.Key,
                    Litros = g.Sum(a => a.Litros),
                    KmPercorridos = g.Sum(a => a.DiferencaHodometro),
                    TotalGasto = g.Sum(a => (double)a.ValorTotalTransacao),
                    MediaKmL = g.Average(a => a.MediaKm)
                });
        }
    }
}
