using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities
{
    public class Veiculo : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public double? ConsumoUrbanoAlcool { get; set; }
        public double? ConsumoUrbanoGasolina { get; set; }
        public double? ConsumoRodoviarioAlcool { get; set; }
        public double? ConsumoRodoviarioGasolina { get; set; }

        public override string ToString()
        {
            return $"{Nome}, {Marca} - {Placa}";
        }
    }
}
