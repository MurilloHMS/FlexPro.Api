using AutoMapper;
using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Contact.GetAll;

public class GetAllContatoHandler : IRequestHandler<GetAllContatoQuery, IEnumerable<ContatoResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IContatoRepository _repository;

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