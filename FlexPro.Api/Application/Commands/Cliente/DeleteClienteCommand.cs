using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Api.Application.Commands.Cliente;

public record DeleteClienteCommand(ClienteRequestDto Cliente) : IRequest;