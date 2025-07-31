using FlexPro.Api.Application.Commands.Email;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Email;

public class SendEmailHandler : IRequestHandler<SendEmailCommand, IActionResult>
{
    private readonly IEmailService _emailService;

    public SendEmailHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<IActionResult> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        if (request.EmailData == null) return new BadRequestObjectResult("Dados do e-mail são obrigatórios");

        try
        {
            await _emailService.SendEmailAsync(request.EmailData.To, request.EmailData.Subject, request.EmailData.Body,
                request.EmailData.Cc!, request.EmailData.Bcc!);
            return new OkObjectResult("E-mail enviado com sucesso");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult($"Erro ao enviar e-mail. Erro: {ex.Message}");
        }
    }
}