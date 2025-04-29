using MediatR;

namespace FlexPro.Api.Application.Commands.Abastecimento;

public class DeleteVeiculoCommand : IRequest
{
    public int Id { get; set; }
}