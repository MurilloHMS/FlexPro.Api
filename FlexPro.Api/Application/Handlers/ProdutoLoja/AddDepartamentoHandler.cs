using AutoMapper;
using FlexPro.Api.Application.Commands.ProdutoLoja;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.ProdutoLoja;

public class AddDepartamentoHandler : IRequestHandler<AddDepartamentoCommand, IActionResult>
{
    private readonly IMapper _mapper;
    private readonly IProdutoLojaRepository _repository;

    public AddDepartamentoHandler(IProdutoLojaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(AddDepartamentoCommand request, CancellationToken cancellationToken)
    {
        var produto = await _repository.GetByIdAsync(request.ProdutoLojaId);

        if (produto == null)
            return new NotFoundObjectResult("Produto no encontrado");

        var novosDepartamentos = _mapper.Map<List<Departamento>>(request.Departamentos);
        foreach (var departamento in novosDepartamentos)
        {
            departamento.ProdutoLojaId = produto.Id;
            produto.Departamentos?.Add(departamento);
        }

        await _repository.InsertOrUpdateAsync(produto);
        return new OkObjectResult("Departamentos adicionados com sucesso");
    }
}