using AutoMapper;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.FuelSupply.Create;

public class CreateAbastecimentoHandler : IRequestHandler<CreateAbastecimentoCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAbastecimentoRepository _repository;

    public CreateAbastecimentoHandler(IMapper mapper, IAbastecimentoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<int> Handle(CreateAbastecimentoCommand request, CancellationToken cancellationToken)
    {
        var abastecimento = _mapper.Map<Domain.Entities.Abastecimento>(request);
        await _repository.AddFuelSupply(abastecimento);
        return abastecimento.Id;
    }
}