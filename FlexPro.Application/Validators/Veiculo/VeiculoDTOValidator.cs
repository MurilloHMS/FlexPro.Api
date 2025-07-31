
using FlexPro.Application.DTOs;
using FluentValidation;

namespace FlexPro.Application.Validators.Veiculo
{
    public class VeiculoDTOValidator : AbstractValidator<VeiculoDto>
    {
        public VeiculoDTOValidator()
        {
            RuleFor(v => v.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(v => v.Placa).NotEmpty().Length(7).WithMessage("Placa deve conter 7 caracteres");
            RuleFor(v => v.Marca).NotEmpty();
            RuleFor(v => v.ConsumoUrbanoAlcool).GreaterThan(0).When(v => v.ConsumoUrbanoAlcool.HasValue);
            RuleFor(v => v.ConsumoRodoviarioGasolina).GreaterThan(0).When(v => v.ConsumoRodoviarioGasolina.HasValue);
        }
    }
}
