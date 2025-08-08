using FlexPro.Domain.Abstractions;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed record GetAllVehicleQuery : IRequest<Result<GetAllVehicleResponse>>;