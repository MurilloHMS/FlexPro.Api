using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Abastecimento;

public class UploadAbastecimentoCommand : IRequest<IActionResult>
{
    public UploadAbastecimentoCommand(IFormFile file)
    {
        File = file;
    }

    public IFormFile File { get; set; }
}