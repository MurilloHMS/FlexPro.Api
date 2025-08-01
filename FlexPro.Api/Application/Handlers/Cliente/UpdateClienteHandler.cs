using AutoMapper;
using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand>
{
    private readonly IMapper _mapper;
    private readonly IClienteRepository _repository;

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