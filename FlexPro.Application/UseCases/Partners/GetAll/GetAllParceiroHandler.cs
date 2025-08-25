using AutoMapper;
using FlexPro.Application.DTOs.Parceiro;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Partners.GetAll;

public class GetAllParceiroHandler : IRequestHandler<GetAllParceiroQuery, IEnumerable<ParceiroResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IParceiroRepository _repository;

    public GetAllParceiroHandler(IMapper mapper, IParceiroRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ParceiroResponseDto>> Handle(GetAllParceiroQuery request,
        CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ParceiroResponseDto>>(entityList);
    }
}