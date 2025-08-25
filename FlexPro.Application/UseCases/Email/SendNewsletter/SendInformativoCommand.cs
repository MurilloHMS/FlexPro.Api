using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Email.SendNewsletter;

public class SendInformativoCommand : IRequest<IActionResult>
{
    public SendInformativoCommand(IEnumerable<Domain.Models.Informativo> informativos)
    {
        Informativos = informativos;
    }

    public IEnumerable<Domain.Models.Informativo> Informativos { get; set; }
}