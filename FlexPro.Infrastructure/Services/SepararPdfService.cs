using System;
using System.Collections.Generic;
using System.IO;
using FlexPro.Domain.Models;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using UglyToad.PdfPig;

namespace FlexPro.Infrastructure.Services
{
    public class SepararPdfService
    {
        public static List<SepararPDF> GetPdfByPage(string inputPdfPath)
        {
            if (!File.Exists(inputPdfPath))
                return new List<SepararPDF>();

            var files = new List<SepararPDF>();

            using (var document = UglyToad.PdfPig.PdfDocument.Open(inputPdfPath))
            {
                int pageIndex = 0;
                foreach(var page in document.GetPages())
                {
                    string textPagina = page.Text;
                    string nomeFuncionario = ExtrairNomeFuncionario(textPagina);

                    files.Add(new SepararPDF
                    {
                        Nome = !string.IsNullOrEmpty(nomeFuncionario) ? nomeFuncionario : $"Pagina {pageIndex + 1}"
                    });

                    pageIndex++;
                }
            }
            return files;
        }

        public static void SeparatedPdfByPage(string inputPdfPath, string outputFolder, List<SepararPDF> lista)
        {
            if (!File.Exists(inputPdfPath) || lista == null || lista.Count == 0)
                return;

            Directory.CreateDirectory(outputFolder);

            using (var inputDocument = PdfReader.Open(inputPdfPath, PdfDocumentOpenMode.Import))
            {
                for (int pageNumber = 0; pageNumber < inputDocument.PageCount; pageNumber++)
                {
                    using (var outputDocument = new PdfSharp.Pdf.PdfDocument())
                    {
                        outputDocument.AddPage(inputDocument.Pages[pageNumber]);

                        string nomeArquivo = SanitizeFileName(lista[pageNumber].Nome);
                        string outputFilePath = Path.Combine(outputFolder, $"{nomeArquivo}.pdf");

                        outputDocument.Save(outputFilePath);
                    }
                }
            }
        }

        private static string ExtrairNomeFuncionario(string text)
        {
            string key = "FL";
            int indexName = text.IndexOf(key, StringComparison.OrdinalIgnoreCase);

            if(indexName != -1)
            {
                int start = indexName + key.Length;
                string restante = text.Substring(start).Trim();
                string[] parts = restante.Split(' ');

                List<string> nameParts = new List<string>();
                bool foundName = false;
                foreach(string part in parts)
                {
                    if (!foundName && int.TryParse(part, out _))
                        continue;

                    foundName = true;

                    if (int.TryParse(part, out _))
                        break;

                    nameParts.Add(part);
                }
                return string.Join(' ', nameParts).Trim();
            }
            return string.Empty;
        }

        private static string SanitizeFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c.ToString(), "_");
            }
            return fileName;
        }
    }
}
