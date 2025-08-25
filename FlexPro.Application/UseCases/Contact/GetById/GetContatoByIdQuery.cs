using FlexPro.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Application.UseCases.Contact.GetById;

public record GetContatoByIdQuery(int Id) : IRequest<ContatoResponseDto>;