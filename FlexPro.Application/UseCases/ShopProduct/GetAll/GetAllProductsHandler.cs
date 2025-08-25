using AutoMapper;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.ShopProduct.GetAll;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Domain.Entities.ProdutoLoja>>
{
    private readonly IMapper _mapper;
    private readonly IProdutoLojaRepository _repository;

    public GetAllProductsHandler(IMapper mapper, IProdutoLojaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.Entities.ProdutoLoja>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<Domain.Entities.ProdutoLoja>>(await _repository.GetAllAsync());
    }
}