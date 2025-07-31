using AutoMapper;
using FlexPro.Api.Application.Commands.Contato;
using FlexPro.Api.Hubs;
using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace FlexPro.Api.Application.Handlers.Contato;

public class CreateContatoHandler : IRequestHandler<CreateContatoCommand, ContatoResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IContatoRepository _contatoRepository;
    private readonly IHubContext<NotificationHub> _hubContext;

    public CreateContatoHandler(IMapper mapper, IContatoRepository contatoRepository,
        IHubContext<NotificationHub> hubContext)
    {
        _mapper = mapper;
        _contatoRepository = contatoRepository;
        _hubContext = hubContext;
    }

    public async Task<ContatoResponseDto> Handle(CreateContatoCommand request, CancellationToken cancellationToken)
    {
        var contatoMap = _mapper.Map<Domain.Entities.Contato>(request.Dto);
        await _contatoRepository.InsertOrUpdateContatoAsync(contatoMap);
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", contatoMap);
        return _mapper.Map<ContatoResponseDto>(contatoMap);
    }
}