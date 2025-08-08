using MediatR;

namespace FlexPro.Application.UseCases.Vehicles.DeleteById;

public sealed record DeleteVehicleByIdCommand(int Id) :  IRequest;