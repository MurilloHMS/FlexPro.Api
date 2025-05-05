using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FlexPro.Api.Application.Commands.Email;

public class SendEmailCommand : IRequest<IActionResult>
{
    public Domain.Entities.Email EmailData { get; set; }

    public SendEmailCommand(Domain.Entities.Email emailData)
    {
        EmailData = emailData;
    }
}