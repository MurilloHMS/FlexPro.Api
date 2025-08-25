using FlexPro.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Application.UseCases.Contact.Update;

public record UpdateContatoCommand(int Id, ContatoRequestDto Dto) : IRequest;