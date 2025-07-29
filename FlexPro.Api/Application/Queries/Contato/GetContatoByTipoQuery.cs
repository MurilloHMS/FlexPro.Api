using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Domain.Enums;
using MediatR;

namespace FlexPro.Api.Application.Queries.Contato;

public record GetContatoByTipoQuery(TipoContato_e tipo) : IRequest<List<ContatoResponseDTO>>;