using MediatR;

namespace FlexPro.Api.Application.Commands.Contato;

public record DeleteContatoCommand(int Id) : IRequest;