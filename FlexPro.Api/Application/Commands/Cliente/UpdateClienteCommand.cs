using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Api.Application.Commands.Cliente;

public record UpdateClienteCommand(int Id, ClienteRequestDto Dto) : IRequest;