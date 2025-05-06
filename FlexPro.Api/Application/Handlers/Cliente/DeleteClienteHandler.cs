using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Api.Application.Interfaces;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand>
{
    private IClienteRepository _repository;
    
    public DeleteClienteHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.id);
        return Unit.Value;
    }
}