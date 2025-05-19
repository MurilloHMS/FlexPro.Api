using FlexPro.Api.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Api.Application.Commands.Contato;

public record CreateContatoCommand(ContatoRequestDTO Dto) : IRequest<ContatoResponseDTO>;
