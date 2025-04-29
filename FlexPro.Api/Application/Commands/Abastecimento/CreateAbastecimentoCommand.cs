using MediatR;

namespace FlexPro.Api.Application.Commands.Abastecimento;

public class CreateAbastecimentoCommand : IRequest<int>
{
    public DateTime DataDoAbastecimento { get; set; }
    public string Uf { get; set; }
    public string Placa { get; set; }
    public string NomeDoMotorista { get; set; }
    public string Departamento { get; set; }
    public double HodometroAtual { get; set; }
    public double HodometroAnterior { get; set; }
    public double DiferencaHodometro { get; set; }
    public double MediaKm { get; set; }
    public string Combustivel { get; set; }
    public double Litros { get; set; }
    public decimal Preco { get; set; }
    public decimal ValorTotalTransacao { get; set; }
}