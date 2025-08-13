using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Enums;
using MediatR;

namespace FlexPro.Application.UseCases.Contact.GetByStatus;

public record GetContatoByStatusQuery(StatusContatoE Status) : IRequest<List<ContatoResponseDto>>;