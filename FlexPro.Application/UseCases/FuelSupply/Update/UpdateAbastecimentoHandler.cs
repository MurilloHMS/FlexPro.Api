using AutoMapper;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.FuelSupply.Update;

public class UpdateAbastecimentoHandler : IRequestHandler<UpdateAbastecimentoCommand>
{
    private readonly IMapper _mapper;
    private readonly IAbastecimentoRepository _repo;

    public UpdateAbastecimentoHandler(IAbastecimentoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task Handle(UpdateAbastecimentoCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Abastecimento>(request);
        //await _repo.UpdateOrInsert(entity);
    }
}