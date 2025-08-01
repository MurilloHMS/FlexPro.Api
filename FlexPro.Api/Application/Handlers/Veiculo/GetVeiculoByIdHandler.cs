using AutoMapper;
using FlexPro.Api.Application.Queries.Veiculo;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Veiculo;

public class GetVeiculoByIdHandler : IRequestHandler<GetVeiculoByIdQuery, VeiculoDto>
{
    private readonly IMapper _mapper;
    private readonly IVeiculoRepository _repo;

    public GetVeiculoByIdHandler(IVeiculoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<VeiculoDto> Handle(GetVeiculoByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        return _mapper.Map<VeiculoDto>(entity);
    }
}