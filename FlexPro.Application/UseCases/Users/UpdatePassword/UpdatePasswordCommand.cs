using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Application.UseCases.Users.UpdatePassword;

public record UpdatePasswordCommand(UpdatePasswordDto Dto) : IRequest<string?>;