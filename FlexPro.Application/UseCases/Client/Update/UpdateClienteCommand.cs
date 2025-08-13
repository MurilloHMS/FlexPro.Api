using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Application.UseCases.Client.Update;

public record UpdateClienteCommand(int Id, ClienteRequestDto Dto) : IRequest;