using FlexPro.Api.Application.Commands.Veiculo;
using FluentValidation;

namespace FlexPro.Api.Application.Validators.Veiculo
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
