using AutoMapper;
using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Veiculo;

public class UpdateVeiculoHandler : IRequestHandler<UpdateVeiculoCommand>
{
    private readonly IMapper _mapper;
    private readonly IVeiculoRepository _repo;

    public UpdateVeiculoHandler(IVeiculoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task Handle(UpdateVeiculoCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Veiculo>(request);
        await _repo.InsertOrUpdateAsync(entity, x => x.Id.Equals(request.Id));
    }
}