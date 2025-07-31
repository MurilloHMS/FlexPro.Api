using AutoMapper;
using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, ClienteResponseDTO>
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public CreateClienteHandler(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ClienteResponseDTO> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = _mapper.Map<Domain.Entities.Cliente>(request.Dto);
        await _repository.UpdateOrInsert(cliente);
        return _mapper.Map<ClienteResponseDTO>(cliente);
    }
}