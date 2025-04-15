using FlexPro.Api.Application.Commands.Veiculo;
using FluentValidation;

namespace FlexPro.Api.Application.Validators.Veiculo
{
    public class UpdateVeiculoCommandValidator : AbstractValidator<UpdateVeiculoCommand>
    {
        public UpdateVeiculoCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Nome).NotEmpty();
            RuleFor(x => x.Placa).NotEmpty();
            RuleFor(x => x.Marca).NotEmpty();
        }
    }
}
