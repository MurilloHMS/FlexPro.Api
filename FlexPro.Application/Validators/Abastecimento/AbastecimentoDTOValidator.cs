using FlexPro.Api.Application.DTOs;
using FlexPro.Application.DTOs;
using FluentValidation;

namespace FlexPro.Application.Validators.Abastecimento;

public class AbastecimentoDTOValidator : AbstractValidator<AbastecimentoDto>
{
    public AbastecimentoDTOValidator()
    {
        RuleFor(x => x.DataDoAbastecimento).NotEmpty().WithMessage("Data de Abastecimento é Obrigatória");
        RuleFor(x => x.NomeDoMotorista).NotEmpty().WithMessage("Nome do motorista é obrigatório");
    }
}