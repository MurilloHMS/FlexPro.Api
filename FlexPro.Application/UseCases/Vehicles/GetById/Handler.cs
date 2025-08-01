using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetById;

public sealed class Handler(IVeiculoRepository repository, IMapper mapper) :  IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var vehicle = await repository.GetByIdAsync(request.Id);
        return vehicle != null
            ? Result.Success(new Response(mapper.Map<VeiculoDto>(vehicle)))
            : Result.Failure<Response>(new Error("404", "Veiculo não encontrado"));
    }
}