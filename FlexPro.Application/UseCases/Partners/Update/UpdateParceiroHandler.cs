using AutoMapper;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Partners.Update;

public class UpdateParceiroHandler : IRequestHandler<UpdateParceiroCommand, IActionResult>
{
    private readonly IMapper _mapper;
    private readonly IParceiroRepository _parceiroRepository;

    public UpdateParceiroHandler(IParceiroRepository parceiroRepository, IMapper mapper)
    {
        _parceiroRepository = parceiroRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(UpdateParceiroCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Parceiro>(request.Dto);
        entity.Id = request.Id;
        await _parceiroRepository.InsertOrUpdateAsync(entity, x => x.Id == request.Id);
        return new OkObjectResult("Parceiro Updated");
    }
}