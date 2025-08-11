using AutoMapper;
using FlexPro.Application.DTOs.FuelSupply;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.FuelSupply.GetAll;

public class GetAllAbastecimentosHandler : IRequestHandler<GetAllAbastecimentoQuery, IEnumerable<FuelSupplyResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAbastecimentoRepository _repo;

    public GetAllAbastecimentosHandler(IAbastecimentoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FuelSupplyResponse>> Handle(GetAllAbastecimentoQuery request, CancellationToken token)
    {
        var entities = await _repo.GetFuelSupply();
        return _mapper.Map<IEnumerable<FuelSupplyResponse>>(entities);
    }
}