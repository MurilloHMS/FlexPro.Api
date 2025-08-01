using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Email;

public class SendEmailCommand : IRequest<IActionResult>
{
    public SendEmailCommand(Domain.Models.Email emailData)
    {
        EmailData = emailData;
    }

    public Domain.Models.Email EmailData { get; set; }
}