using AutoMapper;
using FlexPro.Api.Application.Queries.Contato;
using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Contato;

public class GetAllContatoHandler : IRequestHandler<GetAllContatoQuery, IEnumerable<ContatoResponseDto>>
{
    private readonly IContatoRepository _repository;
    private readonly IMapper _mapper;

    public GetAllContatoHandler(IContatoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContatoResponseDto>> Handle(GetAllContatoQuery request,
        CancellationToken cancellationToken)
    {
        var contatos = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ContatoResponseDto>>(contatos);
    }
}