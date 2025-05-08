using AutoMapper;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Application.Queries.Cliente;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class GetAllClienteHandler : IRequestHandler<GetAllClienteQuery, IEnumerable<ClienteResponseDTO>>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public GetAllClienteHandler(IMapper mapper, IClienteRepository repository)
    {
        _mapper = mapper;
        _clienteRepository = repository;
    }

    public async Task<IEnumerable<ClienteResponseDTO>> Handle(GetAllClienteQuery request,
        CancellationToken cancellationToken)
    {
        var clientes = await  _clienteRepository.GetAll();
        return _mapper.Map<IEnumerable<ClienteResponseDTO>>(clientes);
    }
}