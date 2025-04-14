using FlexPro.Api.Application.Commands;
using FluentValidation;

namespace FlexPro.Api.Application.Validators
{
    public class CreateVeiculoCommandValidator : AbstractValidator<CreateVeiculoCommand>
    {
        public CreateVeiculoCommandValidator()
        {
            RuleFor(x => x.Nome).NotEmpty();
            RuleFor(x => x.Placa).NotEmpty();
            RuleFor(x => x.Marca).NotEmpty();
        }
    }
}
