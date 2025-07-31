using AutoMapper;
using FlexPro.Api.Application.Commands.ProdutoLoja;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.ProdutoLoja;

public class
    CreateProdutoLojaWithEmbalagemHandler : IRequestHandler<CreateProdutoLojaWithEmbalagemCommand, IActionResult>
{
    private readonly IMapper _mapper;
    private readonly IProdutoLojaRepository _repository;

    public CreateProdutoLojaWithEmbalagemHandler(IProdutoLojaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(CreateProdutoLojaWithEmbalagemCommand request,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.ProdutoLoja>(request.Dto);

        if (entity.Embalagems != null && entity.Embalagems.Any())
        {
            foreach (var embalagem in entity.Embalagems)
            {
                embalagem.ProdutoLoja = entity;
            }
        }

        await _repository.InsertOrUpdateAsync(entity);
        return new OkObjectResult("Produto Loja adicionado com sucesso");
    }
}