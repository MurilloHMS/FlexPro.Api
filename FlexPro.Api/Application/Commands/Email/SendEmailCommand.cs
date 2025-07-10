using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FlexPro.Api.Application.Commands.Email;

public class SendEmailCommand : IRequest<IActionResult>
{
    public Domain.Models.Email EmailData { get; set; }

    public SendEmailCommand(Domain.Models.Email emailData)
    {
        EmailData = emailData;
    }
}