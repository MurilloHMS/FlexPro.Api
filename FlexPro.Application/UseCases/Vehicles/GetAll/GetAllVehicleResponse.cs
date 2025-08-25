using FlexPro.Application.DTOs;
using FlexPro.Application.DTOs.Vehicle;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed record GetAllVehicleResponse(IEnumerable<VehicleResponseDto> Veiculos);