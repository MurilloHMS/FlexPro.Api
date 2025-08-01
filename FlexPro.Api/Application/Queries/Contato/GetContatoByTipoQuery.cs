using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Enums;
using MediatR;

namespace FlexPro.Api.Application.Queries.Contato;

public record GetContatoByTipoQuery(TipoContatoE Tipo) : IRequest<List<ContatoResponseDto>>;