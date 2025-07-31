using AutoMapper;
using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Application.DTOs.Cliente;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, ClienteResponseDto>
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public CreateClienteHandler(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ClienteResponseDto> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = _mapper.Map<Domain.Entities.Cliente>(request.Dto);
        await _repository.UpdateOrInsert(cliente);
        return _mapper.Map<ClienteResponseDto>(cliente);
    }
}