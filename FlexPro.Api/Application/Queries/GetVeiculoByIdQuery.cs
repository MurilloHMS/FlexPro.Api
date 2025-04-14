using FlexPro.Api.Application.DTOs;
using MediatR;

namespace FlexPro.Api.Application.Queries
{
    public class GetVeiculoByIdQuery : IRequest<VeiculoDTO>
    {
        public int Id { get; set; }
    }
}
