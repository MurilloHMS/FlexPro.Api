using FlexPro.Application.DTOs;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.Update;

public sealed record UpdateVehicleCommand(VeiculoDto Dto): IRequest;