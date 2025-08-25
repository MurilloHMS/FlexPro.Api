using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Application.UseCases.Client.GetById;

public record GetClienteByIdQuery(int Id) : IRequest<ClienteResponseDto>;