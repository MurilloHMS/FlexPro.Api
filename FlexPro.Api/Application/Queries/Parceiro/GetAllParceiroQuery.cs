using FlexPro.Application.DTOs.Parceiro;
using MediatR;

namespace FlexPro.Api.Application.Queries.Parceiro;

public record GetAllParceiroQuery : IRequest<IEnumerable<ParceiroResponseDto>>;