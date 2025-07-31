using AutoMapper;
using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand>
{
    private readonly IMapper _mapper;
    private readonly IClienteRepository _repository;

    public DeleteClienteHandler(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = _mapper.Map<Domain.Entities.Cliente>(request.Cliente);
        await _repository.DeleteAsync(cliente);
    }
}