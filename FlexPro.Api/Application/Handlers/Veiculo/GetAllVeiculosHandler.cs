using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Queries.Veiculo;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var entities = await _repo.GetAll();
            return _mapper.Map<IEnumerable<VeiculoDTO>>(entities);
        }
    }
}
