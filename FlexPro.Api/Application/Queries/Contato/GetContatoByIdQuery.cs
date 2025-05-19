using FlexPro.Api.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Api.Application.Queries.Contato;

public record GetContatoByIdQuery(int Id) : IRequest<ContatoResponseDTO>;