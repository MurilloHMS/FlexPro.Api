using MediatR;

namespace FlexPro.Application.UseCases.Contact.Delete;

public record DeleteContatoCommand(int Id) : IRequest;