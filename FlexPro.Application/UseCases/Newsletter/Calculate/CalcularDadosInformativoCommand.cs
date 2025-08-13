using FlexPro.Application.DTOs.Informativo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Newsletter.Calculate;

public record CalcularDadosInformativoCommand(InformativoRequestDto InformativoRequest) : IRequest<IActionResult>;