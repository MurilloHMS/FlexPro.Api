namespace FlexPro.Application.DTOs.FuelSupply;

public class FuelSupplyRequest
{
    public DateTime DataDoAbastecimento { get; set; }
    public string Uf { get; set; } = string.Empty;
    public string Placa { get; set; } = string.Empty;
    public string NomeDoMotorista { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public double HodometroAtual { get; set; }
    public double HodometroAnterior { get; set; }
    public double DiferencaHodometro { get; set; }
    public double MediaKm { get; set; }
    public string Combustivel { get; set; } = string.Empty;
    public double Litros { get; set; }
    public decimal Preco { get; set; }
    public decimal ValorTotalTransacao { get; set; }
}