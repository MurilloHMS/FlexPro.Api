using FlexPro.Application.DTOs;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.UseCases.Vehicles.GetById;

public sealed record Response(VeiculoDto Dto);