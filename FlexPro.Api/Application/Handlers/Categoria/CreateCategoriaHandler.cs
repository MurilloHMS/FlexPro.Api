using AutoMapper;
using FlexPro.Api.Application.Commands.Categoria;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Categoria;

public class CreateCategoriaHandler : IRequestHandler<CreateCategoriaCommand, IActionResult>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CreateCategoriaHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Categoria>(request.dto);
        await _categoriaRepository.SaveOrUpdate(entity);
        return new OkObjectResult("Categoria criada com sucesso");
    }
}