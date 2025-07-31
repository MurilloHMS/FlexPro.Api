using FlexPro.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Api.Application.Commands.Contato;

public record CreateContatoCommand(ContatoRequestDto Dto) : IRequest<ContatoResponseDto>;
