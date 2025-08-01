using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed class Handler(IVeiculoRepository repository, IMapper mapper) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var vehicle = await repository.GetAllAsync();
        return !vehicle.Any()
            ? Result.Failure<Response>(new Error("404", "Vehicle not found"))
            : Result.Success(new Response(mapper.Map<IEnumerable<VeiculoDto>>(vehicle)));
    }
}