using AutoMapper;
using FlexPro.Api.Application.DTOs.Parceiro;
using FlexPro.Api.Application.Queries.Parceiro;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Parceiro;

public class GetAllParceiroHandler : IRequestHandler<GetAllParceiroQuery,  IEnumerable<ParceiroResponseDTO>>
{
    private IMapper _mapper;
    private IParceiroRepository _repository;

    public GetAllParceiroHandler(IMapper mapper, IParceiroRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ParceiroResponseDTO>> Handle(GetAllParceiroQuery request,
        CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ParceiroResponseDTO>>(entityList);
    }
}