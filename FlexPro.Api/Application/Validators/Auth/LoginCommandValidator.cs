using FlexPro.Api.Application.Commands.Auth;
using FluentValidation;

namespace FlexPro.Api.Application.Validators.Auth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .EmailAddress().WithMessage("Username must be a valid email address.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password precisa ser maior que 6 caracteres");
        }
    }
}
