using FlexPro.Api.Application.DTOs.Cliente;
using FluentValidation;

namespace FlexPro.Application.Validators.Cliente;

public class CreateClienteDtoValidator : AbstractValidator<ClienteRequestDTO>
{
    public CreateClienteDtoValidator()
    {
        RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("O Nome do cliente é Obrigatório");
        RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("O email do cliente precisa ser válido");
    }
}