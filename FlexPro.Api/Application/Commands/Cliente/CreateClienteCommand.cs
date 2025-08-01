using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Api.Application.Commands.Cliente;

public record CreateClienteCommand(ClienteRequestDto Dto) : IRequest<ClienteResponseDto>;