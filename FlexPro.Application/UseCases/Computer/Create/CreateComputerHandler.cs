using AutoMapper;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Computer.Create;

public class CreateComputerHandler : IRequestHandler<CreateComputerCommand, IActionResult>
{
    private readonly IMapper _mapper;
    private readonly IComputadorRepository _repository;

    public CreateComputerHandler(IMapper mapper, IComputadorRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IActionResult> Handle(CreateComputerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<Computador>(request.Dto);
            await _repository.InsertOrUpdateAsync(entity);
            return new OkObjectResult("Computador criado com sucesso");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}