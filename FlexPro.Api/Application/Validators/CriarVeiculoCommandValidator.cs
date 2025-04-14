using FlexPro.Api.Application.Commands;
using FluentValidation;

namespace FlexPro.Api.Application.Validators
{
    public class CriarVeiculoCommandValidator : AbstractValidator<CriarVeiculoCommand>
    {
        public CriarVeiculoCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome do veículo é obrigatório.")
                .Length(2, 50)
                .WithMessage("O nome do veículo deve ter entre 2 e 50 caracteres.");

            RuleFor(x => x.Marca)
                .NotEmpty()
                .WithMessage("A marca do veículo é obrigatória.")
                .Length(4, 100)
                .WithMessage("A marca do veículo deve ter entre 4 e 100 caracteres.");

            RuleFor(x => x.Placa)
                .NotEmpty()
                .WithMessage("A placa do veiculo é obrigatoria")
                .Length(0, 10)
                .WithMessage("A placa do veiculo deve ter entre 0 e 10 caracteres");
        }
    }
}
