using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Email;

public class SendInformativoCommand : IRequest<IActionResult>
{
    public IEnumerable<FlexPro.Domain.Models.Informativo> Informativos { get; set; }

    public SendInformativoCommand(IEnumerable<FlexPro.Domain.Models.Informativo> informativos)
    {
        Informativos = informativos;
    }
}