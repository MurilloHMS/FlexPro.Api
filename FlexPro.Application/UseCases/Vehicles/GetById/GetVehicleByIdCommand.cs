using FlexPro.Domain.Abstractions;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetById;

public sealed record GetVehicleByIdCommand(int Id) : IRequest<Result<GetVehicleByIdResponse>>;