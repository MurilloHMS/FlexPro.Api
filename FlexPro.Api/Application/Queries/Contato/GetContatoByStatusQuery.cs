using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Enums;
using MediatR;

namespace FlexPro.Api.Application.Queries.Contato;

public record GetContatoByStatusQuery(StatusContatoE Status) : IRequest<List<ContatoResponseDto>>;