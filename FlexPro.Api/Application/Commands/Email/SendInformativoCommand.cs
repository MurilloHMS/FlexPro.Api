using FlexPro.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Email;

public class SendInformativoCommand : IRequest<IActionResult>
{
    public IEnumerable<Informativo> Informativos { get; set; }

    public SendInformativoCommand(IEnumerable<Informativo> informativos)
    {
        Informativos = informativos;
    }
}