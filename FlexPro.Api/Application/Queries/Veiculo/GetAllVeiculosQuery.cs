using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Domain.Entities;
using MediatR;

namespace FlexPro.Api.Application.Queries.Veiculo
{
    public class GetAllVeiculosQuery : IRequest<IEnumerable<VeiculoDTO>>{ }
}
