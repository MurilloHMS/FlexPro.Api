using FlexPro.Domain.Entities;
using MediatR;

namespace FlexPro.Api.Application.Queries.Produtos;

public record GetAllProductsQuery : IRequest<IEnumerable<ProdutoLoja>>;