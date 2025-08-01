using AutoMapper;
using FlexPro.Api.Application.Queries.Cliente;
using FlexPro.Application.DTOs.Cliente;
using FlexPro.Domain.Repositories;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Cliente;

public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, ClienteResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IClienteRepository _repository;

    public GetClienteByIdHandler(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ClienteResponseDto> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.Id);
        return _mapper.Map<ClienteResponseDto>(cliente);
    }
}