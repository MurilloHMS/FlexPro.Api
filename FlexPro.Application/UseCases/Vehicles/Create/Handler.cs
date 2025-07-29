using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.Create;

public sealed class Handler(IVeiculoRepository repository, IMapper mapper) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Veiculo>(request.Dto);
        await repository.InsertOrUpdateAsync(entity);
        return Result.Success(new Response("Veiculo criado com sucesso!"));
    }
}