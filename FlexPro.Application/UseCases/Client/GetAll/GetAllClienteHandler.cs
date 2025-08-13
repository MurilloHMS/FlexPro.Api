using AutoMapper;
using FlexPro.Application.DTOs.Cliente;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Client.GetAll;

public class GetAllClienteHandler : IRequestHandler<GetAllClienteQuery, IEnumerable<ClienteResponseDto>>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public GetAllClienteHandler(IMapper mapper, IClienteRepository repository)
    {
        _mapper = mapper;
        _clienteRepository = repository;
    }

    public async Task<IEnumerable<ClienteResponseDto>> Handle(GetAllClienteQuery request,
        CancellationToken cancellationToken)
    {
        var clientes = await _clienteRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }
}