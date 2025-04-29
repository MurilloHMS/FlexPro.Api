using FlexPro.Api.Application.DTOs;
using MediatR;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetAllAbastecimentoQuery : IRequest<IEnumerable<AbastecimentoDTO>>{ }