using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Email.SendNewsletter;

public class SendInformativoHandler : IRequestHandler<SendInformativoCommand, IActionResult>
{
    private readonly IEmailService _emailService;

    public SendInformativoHandler(IEmailService service)
    {
        _emailService = service;
    }

    public async Task<IActionResult> Handle(SendInformativoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Informativos.Any()) return new BadRequestObjectResult("Lista com dados está vazia");

        await _emailService.EnviarInformativos(request.Informativos);
        return new OkObjectResult("Informativos enviados com sucesso");
    }
}