using MediatR;

namespace FlexPro.Api.Application.Commands.Auth;

public class LoginCommand : IRequest<string>
{
    public LoginCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}