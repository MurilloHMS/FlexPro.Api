using MediatR;
using Microsoft.AspNetCore.Http;

namespace FlexPro.Application.UseCases.Nfe.CalculateIcms;

public record CalculateIcmsCommand(List<IFormFile> Files) :  IRequest<CalculateIcmsResult>;