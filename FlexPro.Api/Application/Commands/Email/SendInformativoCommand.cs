using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Email;

public class SendInformativoCommand : IRequest<IActionResult>
{
    public SendInformativoCommand(IEnumerable<Domain.Models.Informativo> informativos)
    {
        Informativos = informativos;
    }

    public IEnumerable<Domain.Models.Informativo> Informativos { get; set; }
}