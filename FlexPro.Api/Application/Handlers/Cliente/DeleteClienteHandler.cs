using AutoMapper;
using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Api.Application.Interfaces;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand>
{
    private IClienteRepository _repository;
    private readonly IMapper _mapper;
    
    public DeleteClienteHandler(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = _mapper.Map<Domain.Entities.Cliente>(request.cliente);
        await _repository.DeleteAsync(cliente);
    }
}