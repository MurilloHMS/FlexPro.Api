using AutoMapper;
using FlexPro.Api.API.Hubs;
using FlexPro.Api.Application.Commands.Contato;
using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Api.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace FlexPro.Api.Application.Handlers.Contato;

public class CreateContatoHandler : IRequestHandler<CreateContatoCommand, ContatoResponseDTO>
{
    private readonly IMapper _mapper;
    private readonly IContatoRepository _contatoRepository;
    private readonly IHubContext<NotificationHub> _hubContext;

    public CreateContatoHandler(IMapper mapper, IContatoRepository contatoRepository,  IHubContext<NotificationHub> hubContext)
    {
        _mapper = mapper;
        _contatoRepository = contatoRepository;
        _hubContext = hubContext;
    }
    
    public async Task<ContatoResponseDTO> Handle(CreateContatoCommand request, CancellationToken cancellationToken)
    {
        var contatoMap = _mapper.Map<Domain.Entities.Contato>(request.Dto);
        await _contatoRepository.InsertOrUpdateContatoAsync(contatoMap);
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", contatoMap);
        return _mapper.Map<ContatoResponseDTO>(contatoMap);
    }
}