using FlexPro.Application.DTOs.Parceiro;
using MediatR;

namespace FlexPro.Application.UseCases.Partners.GetAll;

public record GetAllParceiroQuery : IRequest<IEnumerable<ParceiroResponseDto>>;