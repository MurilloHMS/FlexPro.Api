using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Application.DTOs.Vehicle;
using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.GetById;

public sealed class GetVehicleByIdHandler(IVeiculoRepository repository, IMapper mapper) :  IRequestHandler<GetVehicleByIdQuery, Result<GetVehicleByIdResponse>>
{
    public async Task<Result<GetVehicleByIdResponse>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await repository.GetByIdAsync(request.Id);
        return vehicle != null
            ? Result.Success(new GetVehicleByIdResponse(mapper.Map<VehicleResponseDto>(vehicle)))
            : Result.Failure<GetVehicleByIdResponse>(new Error("404", "Veiculo não encontrado"));
    }
}