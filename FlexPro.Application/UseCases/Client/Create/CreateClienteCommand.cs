using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Application.UseCases.Client.Create;

public record CreateClienteCommand(ClienteRequestDto Dto) : IRequest<ClienteResponseDto>;