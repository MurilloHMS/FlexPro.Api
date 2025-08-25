using FlexPro.Domain.Models;
using MediatR;

namespace FlexPro.Application.UseCases.Pdf.Save;

public record SavePdfCommand(List<PdfPageInfo> Pages) : IRequest<SavePdfResult>;