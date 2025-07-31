using FlexPro.Application.DTOs.Parceiro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Parceiro;

public record GetAllParceiroQuery() : IRequest<IEnumerable<ParceiroResponseDto>>;