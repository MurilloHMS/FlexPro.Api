using FlexPro.Application.DTOs;
using FlexPro.Application.DTOs.FuelSupply;
using FluentValidation;

namespace FlexPro.Application.Validators.Abastecimento;

public class AbastecimentoDtoValidator : AbstractValidator<FuelSupplyResponse>
{
    public AbastecimentoDtoValidator()
    {
        RuleFor(x => x.DataDoAbastecimento).NotEmpty().WithMessage("Data de Abastecimento é Obrigatória");
        RuleFor(x => x.NomeDoMotorista).NotEmpty().WithMessage("Nome do motorista é obrigatório");
    }
}