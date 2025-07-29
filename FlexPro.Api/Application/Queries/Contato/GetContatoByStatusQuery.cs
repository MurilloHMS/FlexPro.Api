using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Domain.Enums;
using MediatR;

namespace FlexPro.Api.Application.Queries.Contato;

public record GetContatoByStatusQuery(StatusContato_e status) : IRequest<List<ContatoResponseDTO>>;