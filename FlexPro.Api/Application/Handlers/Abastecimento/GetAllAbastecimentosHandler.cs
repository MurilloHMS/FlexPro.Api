using AutoMapper;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Queries.Abastecimento;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Abastecimento;

public class GetAllAbastecimentosHandler : IRequestHandler<GetAllAbastecimentoQuery, IEnumerable<AbastecimentoDTO>>
{
    private readonly IAbastecimentoRepository _repo;
    private readonly IMapper _mapper;
    
    public GetAllAbastecimentosHandler(IAbastecimentoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AbastecimentoDTO>> Handle(GetAllAbastecimentoQuery request, CancellationToken token)
    {
        var entities = await _repo.GetFuelSupply();
        return _mapper.Map<IEnumerable<AbastecimentoDTO>>(entities);
    }
}