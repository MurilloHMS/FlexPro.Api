using AutoMapper;
using FlexPro.Api.Application.Commands.ProdutoLoja;
using FlexPro.Api.Application.DTOs.ProdutoLoja;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.ProdutoLoja;

public class AddEmbalagemHandler : IRequestHandler<AddEmbalagemCommand, IActionResult>
{
    private readonly IProdutoLojaRepository _repository;
    private readonly IMapper _mapper;

    public AddEmbalagemHandler(IProdutoLojaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(AddEmbalagemCommand request, CancellationToken cancellationToken)
    {
        var produto = await _repository.GetByIdAsync(request.produtoLojaId);

        if (produto == null)
            return new NotFoundObjectResult("Produto no encontrado");
        
        var novasEmbalagems =  _mapper.Map<List<Embalagem>>(request.embalagens);

        foreach (var embalagem in novasEmbalagems)
        {
            embalagem.ProdutoLojaId = produto.Id;
            produto.Embalagems.Add(embalagem);
        }
        
        await _repository.InsertOrUpdateAsync(produto);
        return new OkObjectResult("Embalagens adicionadas com sucesso");
    }
}