using AutoMapper;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Client.Delete;

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