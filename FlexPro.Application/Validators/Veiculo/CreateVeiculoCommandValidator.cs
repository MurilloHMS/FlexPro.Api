using FlexPro.Application.DTOs;
using FluentValidation;

namespace FlexPro.Application.Validators.Veiculo;

public class CreateVeiculoCommandValidator : AbstractValidator<VeiculoDto>
{
    public CreateVeiculoCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Placa).NotEmpty();
        RuleFor(x => x.Marca).NotEmpty();
    }
}