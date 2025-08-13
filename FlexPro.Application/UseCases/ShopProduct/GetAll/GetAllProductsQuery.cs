using FlexPro.Domain.Entities;
using MediatR;

namespace FlexPro.Application.UseCases.ShopProduct.GetAll;

public record GetAllProductsQuery : IRequest<IEnumerable<ProdutoLoja>>;