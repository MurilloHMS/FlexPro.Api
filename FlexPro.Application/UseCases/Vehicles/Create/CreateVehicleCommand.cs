using FlexPro.Application.DTOs;
using FlexPro.Domain.Abstractions;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.Create;

public sealed record CreateVehicleCommand(VeiculoDto Dto) : IRequest<Result<CreateVehicleResponse>>;