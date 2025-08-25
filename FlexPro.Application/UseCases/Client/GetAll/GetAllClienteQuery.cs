using FlexPro.Application.DTOs.Cliente;
using MediatR;

namespace FlexPro.Application.UseCases.Client.GetAll;

public record GetAllClienteQuery : IRequest<IEnumerable<ClienteResponseDto>>;