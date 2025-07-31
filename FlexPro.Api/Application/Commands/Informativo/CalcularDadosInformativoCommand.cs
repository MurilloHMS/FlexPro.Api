using FlexPro.Application.DTOs.Informativo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Informativo;

public record CalcularDadosInformativoCommand(InformativoRequestDto InformativoRequest) : IRequest<IActionResult>;