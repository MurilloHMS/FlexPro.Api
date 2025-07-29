using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace FlexPro.Application.Validators.Auth
{
    public class RegisterCommandValidator : AbstractValidator<LoginRequest>
    {
        // public RegisterCommandValidator()
        // {
        //     RuleFor(x => x.Register.Username)
        //         .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
        //         .EmailAddress().WithMessage("O nome de usuário deve ser um endereço de e-mail válido.");
        //     RuleFor(x => x.Register.Password)
        //         .NotEmpty().WithMessage("A senha é obrigatória.")
        //         .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.")
        //         .MaximumLength(20).WithMessage("A senha deve ter no máximo 20 caracteres.")
        //         .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
        //         .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
        //         .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um número.")
        //         .Matches(@"[\W_]").WithMessage("A senha deve conter pelo menos um caractere especial.");
        // }
    }
}
