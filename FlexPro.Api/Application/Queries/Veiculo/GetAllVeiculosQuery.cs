using FlexPro.Application.DTOs;
using MediatR;

namespace FlexPro.Api.Application.Queries.Veiculo;

public class GetAllVeiculosQuery : IRequest<IEnumerable<VeiculoDto>>
{
}