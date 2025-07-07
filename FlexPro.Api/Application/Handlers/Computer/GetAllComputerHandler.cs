using AutoMapper;
using FlexPro.Api.Application.DTOs.Computer;
using FlexPro.Api.Application.DTOs.Parceiro;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Application.Queries.Computer;
using FlexPro.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Computer;

public class GetAllComputerHandler : IRequestHandler<GetAllComputerQuery, IActionResult>
{
    private readonly IMapper _mapper;
    private readonly IComputadorRepository _repository;

    public GetAllComputerHandler(IMapper mapper, IComputadorRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IActionResult> Handle(GetAllComputerQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.GetAllAsync();
        var mappedResponse = _mapper.Map<IEnumerable<ComputerResponseDTO>>(response);
        return response.Any() ? new OkObjectResult(response) : new NotFoundResult();
    }
}