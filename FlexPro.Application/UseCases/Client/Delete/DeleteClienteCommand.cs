using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Application.UseCases.Client.Delete;

public record DeleteClienteCommand(ClienteRequestDto Cliente) : IRequest;