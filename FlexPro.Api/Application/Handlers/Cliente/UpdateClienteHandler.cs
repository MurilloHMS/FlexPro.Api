using AutoMapper;
using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Api.Application.Interfaces;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand>
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public UpdateClienteHandler(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = _mapper.Map<Domain.Entities.Cliente>(request.Dto);
        cliente.Id = request.Id;
        await _repository.UpdateOrInsert(cliente);
    }
}