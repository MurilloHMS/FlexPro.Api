using AutoMapper;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Queries.Veiculo;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Veiculo
{
    public class GetAllVeiculosHandler : IRequestHandler<GetAllVeiculosQuery, IEnumerable<VeiculoDTO>>
    {
        private readonly IVeiculoRepository _repo;
        private readonly IMapper _mapper;

        public GetAllVeiculosHandler(IVeiculoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VeiculoDTO>> Handle(GetAllVeiculosQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<VeiculoDTO>>(entities);
        }
    }
}
