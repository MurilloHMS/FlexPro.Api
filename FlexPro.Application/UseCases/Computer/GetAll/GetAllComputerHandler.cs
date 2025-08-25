using AutoMapper;
using FlexPro.Application.DTOs.Computer;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Computer.GetAll;

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
        var mappedResponse = _mapper.Map<IEnumerable<ComputerResponseDto>>(response);
        return response.Any() ? new OkObjectResult(mappedResponse) : new NotFoundResult();
    }
}