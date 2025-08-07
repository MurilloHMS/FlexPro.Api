using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.DeleteById;

public sealed class DeleteVehicleByIdHandler(IVeiculoRepository repository) : IRequestHandler<DeleteVehicleByIdCommand>
{
    public async Task Handle(DeleteVehicleByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id);
        if (entity != null)
            await repository.DeleteAsync(entity);
    }
}