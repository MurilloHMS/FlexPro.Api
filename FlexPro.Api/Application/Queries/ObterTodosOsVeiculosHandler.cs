using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Application.Queries
{
    public class ObterTodosOsVeiculosHandler : IRequestHandler<ObterTodosOsVeiculosQuery, List<VeiculoDTO>>
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ObterTodosOsVeiculosHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VeiculoDTO>> Handle(ObterTodosOsVeiculosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Veiculo.ProjectTo<VeiculoDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
