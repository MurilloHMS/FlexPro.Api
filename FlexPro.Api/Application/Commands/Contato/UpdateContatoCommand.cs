using FlexPro.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Api.Application.Commands.Contato;

public record UpdateContatoCommand(int Id, ContatoRequestDto Dto) : IRequest;