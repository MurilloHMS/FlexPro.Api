using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.CalculoICMS;

public class CalculoIcmsCommand : IRequest<Stream>
{
    public List<IFormFile> Files { get; set; }

    public CalculoIcmsCommand(List<IFormFile> files)
    {
        Files = files;
    }
}