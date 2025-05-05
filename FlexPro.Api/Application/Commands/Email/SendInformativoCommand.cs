using FlexPro.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Email;

public class SendInformativoCommand : IRequest<IActionResult>
{
    public List<Informativo> Informativos { get; set; }

    public SendInformativoCommand(List<Informativo> informativos)
    {
        Informativos = informativos;
    }
}