using FlexPro.Application.DTOs.FuelSupply;
using MediatR;

namespace FlexPro.Application.UseCases.FuelSupply.Create;

public sealed record CreateAbastecimentoCommand(FuelSupplyRequest Dto) : IRequest<int>;