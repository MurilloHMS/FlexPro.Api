﻿using FlexPro.Api.Application.DTOs;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class RegisterCommand : IRequest<string>
    {
        public RegisterDTO Register { get; set; } = default!;
    }
}
