using FlexPro.Domain.Abstractions;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetById;

public sealed record Command(int Id) : IRequest<Result<Response>>;