using FlexPro.Api.Application.DTOs;
using FluentValidation;

namespace FlexPro.Api.Application.Validators.Abastecimento;

public class AbastecimentoDTOValidator : AbstractValidator<AbastecimentoDTO>
{
    public AbastecimentoDTOValidator()
    {
        RuleFor(x => x.DataDoAbastecimento).NotEmpty().WithMessage("Data de Abastecimento é Obrigatória");
        RuleFor(x => x.NomeDoMotorista).NotEmpty().WithMessage("Nome do motorista é obrigatório");
    }
}