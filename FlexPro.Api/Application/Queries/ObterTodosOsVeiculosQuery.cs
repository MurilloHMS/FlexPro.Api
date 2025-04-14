using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Domain.Entities;
using MediatR;

namespace FlexPro.Api.Application.Queries
{
    public class ObterTodosOsVeiculosQuery : IRequest<List<VeiculoDTO>>{ }
}
