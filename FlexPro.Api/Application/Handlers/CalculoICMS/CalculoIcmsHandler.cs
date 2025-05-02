using FlexPro.Api.Application.Commands.CalculoICMS;
using FlexPro.Api.Application.Interfaces;
using MediatR;

namespace FlexPro.Api.Application.Handlers.CalculoICMS;

public class CalculoIcmsHandler : IRequestHandler<CalculoIcmsCommand, Stream>
{
    private readonly IIcmsService _service;

    public CalculoIcmsHandler(IIcmsService service)
    {
        _service = service;
    }

    public async Task<Stream> Handle(CalculoIcmsCommand request, CancellationToken cancellationToken)
    {
        return await _service.CalcularAsync(request.Files);
    }
}