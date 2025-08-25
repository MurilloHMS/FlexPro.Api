using FlexPro.Application.DTOs;
using FlexPro.Application.DTOs.Vehicle;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.Update;

public sealed record UpdateVehicleCommand(VehicleResponseDto ResponseDto): IRequest;