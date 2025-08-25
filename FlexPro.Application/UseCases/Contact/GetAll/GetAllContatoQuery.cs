using FlexPro.Application.DTOs.Contato;
using MediatR;

namespace FlexPro.Application.UseCases.Contact.GetAll;

public record GetAllContatoQuery : IRequest<IEnumerable<ContatoResponseDto>>;