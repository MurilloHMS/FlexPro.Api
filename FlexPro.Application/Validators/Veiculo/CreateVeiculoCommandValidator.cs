using FlexPro.Api.Application.DTOs;
using FlexPro.Application.DTOs;
using FluentValidation;

namespace FlexPro.Application.Validators.Veiculo
{
    public class CreateVeiculoCommandValidator : AbstractValidator<VeiculoDTO>
    {
        public CreateVeiculoCommandValidator()
        {
            RuleFor(x => x.Nome).NotEmpty();
            RuleFor(x => x.Placa).NotEmpty();
            RuleFor(x => x.Marca).NotEmpty();
        }
    }
}
