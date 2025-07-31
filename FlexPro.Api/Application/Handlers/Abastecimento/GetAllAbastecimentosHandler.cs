using AutoMapper;
using FlexPro.Api.Application.Queries.Abastecimento;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Abastecimento;

public class GetAllAbastecimentosHandler : IRequestHandler<GetAllAbastecimentoQuery, IEnumerable<AbastecimentoDto>>
{
    private readonly IAbastecimentoRepository _repo;
    private readonly IMapper _mapper;

    public GetAllAbastecimentosHandler(IAbastecimentoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AbastecimentoDto>> Handle(GetAllAbastecimentoQuery request, CancellationToken token)
    {
        var entities = await _repo.GetFuelSupply();
        return _mapper.Map<IEnumerable<AbastecimentoDto>>(entities);
    }
}