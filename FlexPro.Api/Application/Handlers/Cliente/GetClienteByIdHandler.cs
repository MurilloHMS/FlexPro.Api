using AutoMapper;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Application.Queries.Cliente;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, ClienteResponseDTO>
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public GetClienteByIdHandler(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ClienteResponseDTO> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = _repository.GetById(request.Id);
        return _mapper.Map<ClienteResponseDTO>(cliente);
    }
}