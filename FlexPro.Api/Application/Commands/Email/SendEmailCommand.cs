using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Email;

public class SendEmailCommand : IRequest<IActionResult>
{
    public FlexPro.Domain.Models.Email EmailData { get; set; }

    public SendEmailCommand(FlexPro.Domain.Models.Email emailData)
    {
        EmailData = emailData;
    }
}