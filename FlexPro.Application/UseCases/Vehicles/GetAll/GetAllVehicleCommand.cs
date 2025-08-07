using FlexPro.Domain.Abstractions;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed record GetAllVehicleCommand : IRequest<Result<GetAllVehicleResponse>>;