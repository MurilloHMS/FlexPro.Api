using AutoMapper;
using FlexPro.Api.Application.Commands.Parceiro;
using FlexPro.Api.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Parceiro;

public class UpdateParceiroHandler : IRequestHandler<UpdateParceiroCommand, IActionResult>
{
    private readonly IParceiroRepository _parceiroRepository;
    private readonly IMapper _mapper;

    public UpdateParceiroHandler(IParceiroRepository parceiroRepository, IMapper mapper)
    {
        _parceiroRepository = parceiroRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(UpdateParceiroCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Parceiro>(request.dto);
        await  _parceiroRepository.InsertOrUpdateAsync(entity, x => x.Id == request.id);
        return new OkObjectResult("Parceiro Updated");
    }
}