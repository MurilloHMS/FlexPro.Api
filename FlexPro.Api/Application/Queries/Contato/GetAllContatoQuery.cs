using FlexPro.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Api.Application.Queries.Contato;

public record GetAllContatoQuery : IRequest<IEnumerable<ContatoResponseDto>>;