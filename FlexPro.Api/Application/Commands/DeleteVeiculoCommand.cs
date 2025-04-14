using MediatR;

namespace FlexPro.Api.Application.Commands
{
    public class DeleteVeiculoCommand : IRequest
    {
        public int Id { get; set; }
    }
}
