using AutoMapper;
using FlexPro.Api.Application.Commands.Parceiro;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Parceiro;

public class CreateParceiroHandler : IRequestHandler<CreateParceiroCommand, IActionResult>
{
    private readonly IMapper _mapper;
    private readonly IParceiroRepository _parceiroRepository;

    public CreateParceiroHandler(IMapper mapper, IParceiroRepository parceiroRepository)
    {
        _mapper = mapper;
        _parceiroRepository = parceiroRepository;
    }

    public async Task<IActionResult> Handle(CreateParceiroCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Parceiro>(request.dto);
        await _parceiroRepository.InsertOrUpdateAsync(entity);
        return new OkObjectResult("Parceiro criado com sucesso");
    }
}