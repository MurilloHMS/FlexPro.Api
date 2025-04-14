using AutoMapper;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Queries;
using FlexPro.Api.Interfaces;
using MediatR;
using NuGet.Protocol.Plugins;

namespace FlexPro.Api.Application.Handlers
{
    public class GetVeiculoByIdHandler : IRequestHandler<GetVeiculoByIdQuery, VeiculoDTO>
    {
        private readonly IVeiculoRepository _repo;
        private readonly IMapper _mapper;

        public GetVeiculoByIdHandler(IVeiculoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<VeiculoDTO> Handle(GetVeiculoByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetById(request.Id);
            return _mapper.Map<VeiculoDTO>(entity);
        }
    }
}
