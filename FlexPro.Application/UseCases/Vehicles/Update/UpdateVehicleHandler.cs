using AutoMapper;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.Update;

public sealed class UpdateVehicleHandler(IVeiculoRepository repository, IMapper mapper) : IRequestHandler<UpdateVehicleCommand>
{
    public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity  = mapper.Map<Vehicle>(request);
        await repository.InsertOrUpdateAsync(entity, x => x.Id.Equals(request.ResponseDto.Id));
    }
}