using FlexPro.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FlexPro.Application.UseCases.Pdf.Upload;

public record UploadPdfCommand(IFormFile FileName) : IRequest<List<PdfPageInfo>>;