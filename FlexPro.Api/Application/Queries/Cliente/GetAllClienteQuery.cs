using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Api.Application.Queries.Cliente;

public record GetAllClienteQuery : IRequest<IEnumerable<ClienteResponseDto>>;