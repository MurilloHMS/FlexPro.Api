using FlexPro.Application.DTOs;
using MediatR;

namespace FlexPro.Api.Application.Queries.Veiculo;

public class GetVeiculoByIdQuery : IRequest<VeiculoDto>
{
    public int Id { get; set; }
}