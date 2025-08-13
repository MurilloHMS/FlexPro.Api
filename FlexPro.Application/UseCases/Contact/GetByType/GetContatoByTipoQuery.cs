using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Enums;
using MediatR;

namespace FlexPro.Application.UseCases.Contact.GetByType;

public record GetContatoByTipoQuery(TipoContatoE Tipo) : IRequest<List<ContatoResponseDto>>;