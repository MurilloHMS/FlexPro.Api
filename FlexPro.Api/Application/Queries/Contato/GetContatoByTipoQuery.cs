using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Api.Domain;
using MediatR;

namespace FlexPro.Api.Application.Queries.Contato;

public record GetContatoByTipoQuery(TipoContato_e tipo) : IRequest<List<ContatoResponseDTO>>;