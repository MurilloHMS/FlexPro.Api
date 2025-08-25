using FlexPro.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Application.UseCases.Contact.Create;

public record CreateContatoCommand(ContatoRequestDto Dto) : IRequest<ContatoResponseDto>;