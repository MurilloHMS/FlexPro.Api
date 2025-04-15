using AutoMapper;
using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using MediatR;

namespace FlexPro.Api.Application.Handlers
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
            await _repository.UpdateOrInsert(veiculo);
            return veiculo.Id;
        }
    }

}
