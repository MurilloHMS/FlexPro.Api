using NuGet.Protocol.Plugins;
using FlexPro.Api.Interfaces;
using AutoMapper;
using MediatR;
using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Handlers.Veiculo
{
    public class UpdateVeiculoHandler : IRequestHandler<UpdateVeiculoCommand>
    {
        private readonly IVeiculoRepository _repo;
        private readonly IMapper _mapper;

        public UpdateVeiculoHandler(IVeiculoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateVeiculoCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Veiculo>(request);
            await _repo.UpdateOrInsert(entity);
            return Unit.Value;
        }
    }

}

