using FlexPro.Api.Application.DTOs.Cliente;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Cliente;

public record CreateClienteCommand(ClienteRequestDTO Dto) : IRequest<ClienteResponseDTO>;