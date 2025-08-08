using FlexPro.Application.DTOs;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed record GetAllVehicleResponse(IEnumerable<VehicleDto> Veiculos);