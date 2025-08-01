using MediatR;

namespace FlexPro.Api.Application.Commands.Veiculo;

public class DeleteVeiculoCommand : IRequest
{
    public int Id { get; set; }
}