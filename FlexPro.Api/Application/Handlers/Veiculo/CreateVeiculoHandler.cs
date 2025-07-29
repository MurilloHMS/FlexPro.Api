using AutoMapper;
using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Veiculo
{
    public class CreateVeiculoHandler : IRequestHandler<CreateVeiculoCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IVeiculoRepository _repository;

        public CreateVeiculoHandler(IMapper mapper, IVeiculoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Handle(CreateVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = _mapper.Map<Domain.Entities.Veiculo>(request);
            await _repository.InsertOrUpdateAsync(veiculo);
            return veiculo.Id;
        }
    }

}
