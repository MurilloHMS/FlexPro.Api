using FlexPro.Application.DTOs;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed record Response(IEnumerable<VeiculoDto> Veiculos);