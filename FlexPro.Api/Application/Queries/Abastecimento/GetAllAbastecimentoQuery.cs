using FlexPro.Api.Application.DTOs;
using FlexPro.Application.DTOs;
using MediatR;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetAllAbastecimentoQuery : IRequest<IEnumerable<AbastecimentoDto>>{ }