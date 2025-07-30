using FlexPro.Api.Application.DTOs;
using FlexPro.Application.DTOs;
using MediatR;

namespace FlexPro.Api.Application.Queries.Veiculo
{
    public class GetVeiculoByIdQuery : IRequest<VeiculoDTO>
    {
        public int Id { get; set; }
    }
}
