using FlexPro.Api.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Api.Application.Queries.Cliente;

public record GetClienteByIdQuery(int Id) : IRequest<ClienteResponseDTO>;