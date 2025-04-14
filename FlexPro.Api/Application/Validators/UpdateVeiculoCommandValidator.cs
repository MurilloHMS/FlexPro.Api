using FlexPro.Api.Application.Commands;
using FluentValidation;

namespace FlexPro.Api.Application.Validators
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
