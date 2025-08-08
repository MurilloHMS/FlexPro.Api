using AutoMapper;
using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.Create;

public sealed class CreateVehicleHandler(IVeiculoRepository repository, IMapper mapper) : IRequestHandler<CreateVehicleCommand, Result<CreateVehicleResponse>>
{
    public async Task<Result<CreateVehicleResponse>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Veiculo>(request.Dto);
        await repository.InsertOrUpdateAsync(entity);
        return Result.Success(new CreateVehicleResponse("Veiculo criado com sucesso!"));
    }
}