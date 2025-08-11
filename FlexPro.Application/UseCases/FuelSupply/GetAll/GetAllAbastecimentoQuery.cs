using FlexPro.Application.DTOs.FuelSupply;
using MediatR;

namespace FlexPro.Application.UseCases.FuelSupply.GetAll;

public sealed record GetAllAbastecimentoQuery : IRequest<IEnumerable<FuelSupplyResponse>>;