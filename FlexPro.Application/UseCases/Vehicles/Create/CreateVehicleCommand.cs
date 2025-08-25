using FlexPro.Application.DTOs;
using FlexPro.Application.DTOs.Vehicle;
using FlexPro.Domain.Abstractions;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.Create;

public sealed record CreateVehicleCommand(VehicleRequestDto ResponseDto) : IRequest<Result<CreateVehicleResponse>>;