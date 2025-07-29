using FlexPro.Application.DTOs;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed record Response(IEnumerable<VeiculoDTO> Veiculos);