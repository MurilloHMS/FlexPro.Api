using FlexPro.Api.Application.DTOs.Contato;
using FluentValidation;

namespace FlexPro.Application.Validators.Contato;

public class CreateContatoDtoValidator : AbstractValidator<ContatoRequestDTO>
{
    public CreateContatoDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email invalido");
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome invalido");
        RuleFor(x => x.StatusContato).NotNull().IsInEnum().WithMessage("StatusContato invalido");
        RuleFor(x => x.TipoContato).NotNull().IsInEnum().WithMessage("TipoContato invalido");
    }
}