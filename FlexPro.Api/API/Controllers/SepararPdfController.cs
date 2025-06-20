using System.IO.Compression;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// TODO: Migrate this to Mediator Pattern
namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SepararPdfController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<ActionResult<List<SepararPDF>>> Upload(IFormFile file)
        {
            if (file is null || file.Length is 0) { return BadRequest("Nenhum arquivo ou arquivo inválido enviado"); }

            var inputPdfpath = Path.Combine(Path.GetTempPath(), "input.pdf");
            using (var stream = new FileStream(inputPdfpath, FileMode.Create))
            {
                await file.OpenReadStream().CopyToAsync(stream);
            }

            var paginas = SepararPdfService.GetPdfByPage(inputPdfpath);
            if(paginas is null || paginas.Count is 0)
            {
                return BadRequest("Não é possivel extrair as paginas do pdf informado");
            }

            return Ok(paginas);
        }

        [HttpPost("save")]
        public async Task<ActionResult> Save(List<SepararPDF> paginasSeparadas)
        {
            if(paginasSeparadas is not null && paginasSeparadas.Any())
            {
                var outputFolder = Path.Combine(Path.GetTempPath(), "Separados");
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                var inputPdfpath = Path.Combine(Path.GetTempPath(), "input.pdf");
                SepararPdfService.SeparatedPdfByPage(inputPdfpath, outputFolder, paginasSeparadas);

                var zipFilePath = Path.Combine(Path.GetTempPath(), "PDF_Separados.zip");
                if(System.IO.File.Exists(zipFilePath))
                   System.IO.File.Delete(zipFilePath);

                ZipFile.CreateFromDirectory(outputFolder, zipFilePath);
                var zipBytes = await System.IO.File.ReadAllBytesAsync(zipFilePath);

                Directory.Delete(outputFolder, true);
                System.IO.File.Delete(zipFilePath);

                return File(zipBytes, "application/zip", "PDF_Separados.zip");
            }
            return BadRequest();
        }
    }
}
