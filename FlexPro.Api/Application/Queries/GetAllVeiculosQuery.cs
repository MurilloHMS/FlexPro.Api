using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Domain.Entities;
using MediatR;

namespace FlexPro.Api.Application.Queries
{
    public class GetAllVeiculosQuery : IRequest<IEnumerable<VeiculoDTO>>{ }
}
