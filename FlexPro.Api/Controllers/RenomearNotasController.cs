using System.IO.Compression;
using ClosedXML.Excel;
using FlexPro.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// TODO: Migrate this to Mediator Pattern
namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RenomearNotasController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<ActionResult> RenomearNotasFiscais(List<IFormFile> files)
    {
        if (files.Count == 0)
            return BadRequest("Nenhum arquivo enviado.");

        var dados = new List<DadosNota>();

        foreach (var file in files)
            if (Path.GetExtension(file.FileName).Contains("xlsx"))
                dados = await ObterIndices(file);

        var outputFolder = Path.Combine(Path.GetTempPath(), "NotasRenomeadas");
        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

        foreach (var file in files)
            if (Path.GetExtension(file.FileName).Contains("pdf"))
            {
                var tempFilePath = Path.Combine(Path.GetTempPath(), file.FileName);

                await using var stream = file.OpenReadStream();
                await using var fileStream = new FileStream(tempFilePath, FileMode.Create);
                await stream.CopyToAsync(fileStream);

                // Alterar os nomes dos arquivos PDF com base nos dados extraídos do Excel
                await AlterarNomesNotas(tempFilePath, dados, outputFolder);
            }

        var zipFilePath = Path.Combine(Path.GetTempPath(), "NotasRenomeadas.zip");
        if (System.IO.File.Exists(zipFilePath))
            System.IO.File.Delete(zipFilePath);

        ZipFile.CreateFromDirectory(outputFolder, zipFilePath);

        var zipBytes = await System.IO.File.ReadAllBytesAsync(zipFilePath);

        // Limpeza dos arquivos temporários
        Directory.Delete(outputFolder, true);
        System.IO.File.Delete(zipFilePath);

        return File(zipBytes, "application/zip", "NotasRenomeadas.zip");
    }

    private async Task<List<DadosNota>> ObterIndices(IFormFile file)
    {
        try
        {
            var dados = new List<DadosNota>();

            using (var stream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(stream);
                stream.Position = 0;

                using (var wb = new XLWorkbook(stream))
                {
                    var worksheet = wb.Worksheet(1);
                    var fileData = worksheet.RowsUsed()
                        .Skip(1)
                        .Select(row => new DadosNota
                        {
                            Identificador = row.Cell(1).TryGetValue<string>(out var identificador)
                                ? identificador
                                : default,
                            NumeroNFe = row.Cell(2).TryGetValue<string>(out var numeroNFe) ? numeroNFe : default
                        }).ToList();

                    dados.AddRange(fileData);
                }
            }

            return dados;
        }
        catch (Exception ex)
        {
            // Melhorar a captura de erro
            Console.WriteLine($"Erro ao processar o arquivo Excel: {ex.Message}");
            return new List<DadosNota>();
        }
    }

    private async Task AlterarNomesNotas(string filePath, List<DadosNota> dados, string outputFolder)
    {
        if (!dados.Any()) return;

        var nomeDaNota = Path.GetFileNameWithoutExtension(filePath);

        foreach (var nota in dados)
            if (nomeDaNota.Equals(nota.Identificador, StringComparison.OrdinalIgnoreCase))
            {
                var novoCaminho = Path.Combine(outputFolder, $"NFe_{nota.NumeroNFe}.pdf");

                System.IO.File.Copy(filePath, novoCaminho, true);
                return;
            }
    }
}