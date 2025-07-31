using FlexPro.Application.DTOs;
using FluentValidation;

namespace FlexPro.Application.Validators.Veiculo
{
    public class UpdateVeiculoCommandValidator : AbstractValidator<VeiculoDto>
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
