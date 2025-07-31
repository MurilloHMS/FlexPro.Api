using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth;

public class RegisterCommand : IRequest<string>
{
    public RegisterDto Register { get; set; } = default!;
}