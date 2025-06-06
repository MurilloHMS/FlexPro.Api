using AutoMapper;
using FlexPro.Api.Application.Commands.ProdutoLoja;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.ProdutoLoja;

public class AddDepartamentoHandler : IRequestHandler<AddDepartamentoCommand, IActionResult>
{
    private readonly IProdutoLojaRepository _repository;
    private readonly IMapper _mapper;

    public AddDepartamentoHandler(IProdutoLojaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(AddDepartamentoCommand request, CancellationToken cancellationToken)
    {
        var produto = await _repository.GetByIdAsync(request.produtoLojaId);
        
        if (produto == null)
            return new NotFoundObjectResult("Produto no encontrado");
        
        var novosDepartamentos = _mapper.Map<List<Departamento>>(request.departamentos);
        foreach (var departamento in novosDepartamentos)
        {
            departamento.ProdutoLojaId = produto.Id;
            produto.Departamentos.Add(departamento);
        }

        await _repository.InsertOrUpdateAsync(produto);
        return new OkObjectResult("Departamentos adicionados com sucesso");
    }
}