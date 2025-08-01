using MediatR;

namespace FlexPro.Api.Application.Commands.CalculoICMS;

public class CalculoIcmsCommand : IRequest<Stream>
{
    public CalculoIcmsCommand(List<IFormFile> files)
    {
        Files = files;
    }

    public List<IFormFile> Files { get; set; }
}