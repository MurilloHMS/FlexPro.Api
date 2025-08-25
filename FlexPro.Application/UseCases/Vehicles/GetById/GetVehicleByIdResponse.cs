using FlexPro.Application.DTOs;
using FlexPro.Application.DTOs.Vehicle;

namespace FlexPro.Application.UseCases.Vehicles.GetById;

public sealed record GetVehicleByIdResponse(VehicleResponseDto ResponseDto);