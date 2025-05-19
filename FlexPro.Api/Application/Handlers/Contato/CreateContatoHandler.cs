using AutoMapper;
using FlexPro.Api.Application.Commands.Contato;
using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Api.Application.Interfaces;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Contato;

public class CreateContatoHandler : IRequestHandler<CreateContatoCommand, ContatoResponseDTO>
{
    private readonly IMapper _mapper;
    private readonly IContatoRepository _contatoRepository;

    public CreateContatoHandler(IMapper mapper, IContatoRepository contatoRepository)
    {
        _mapper = mapper;
        _contatoRepository = contatoRepository;
    }
    
    public async Task<ContatoResponseDTO> Handle(CreateContatoCommand request, CancellationToken cancellationToken)
    {
        var contatoMap = _mapper.Map<Domain.Entities.Contato>(request.Dto);
        await _contatoRepository.InsertOrUpdateContatoAsync(contatoMap);
        return _mapper.Map<ContatoResponseDTO>(contatoMap);
    }
}