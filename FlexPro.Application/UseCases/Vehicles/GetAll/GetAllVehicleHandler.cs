using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetAll;

public sealed class GetAllVehicleHandler(IVeiculoRepository repository, IMapper mapper) : IRequestHandler<GetAllVehicleCommand, Result<GetAllVehicleResponse>>
{
    public async Task<Result<GetAllVehicleResponse>> Handle(GetAllVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await repository.GetAllAsync();
        return !vehicle.Any()
            ? Result.Failure<GetAllVehicleResponse>(new Error("404", "Vehicle not found"))
            : Result.Success(new GetAllVehicleResponse(mapper.Map<IEnumerable<VeiculoDto>>(vehicle)));
    }
}