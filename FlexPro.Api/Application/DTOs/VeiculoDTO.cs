namespace FlexPro.Api.Application.DTOs
{
    public class VeiculoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public double? ConsumoUrbanoAlcool { get; set; }
        public double? ConsumoUrbanoGasolina { get; set; }
        public double? ConsumoRodoviarioAlcool { get; set; }
        public double? ConsumoRodoviarioGasolina { get; set; }
    }
}
