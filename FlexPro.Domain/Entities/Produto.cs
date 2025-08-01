using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class Produto : Entity
{
    public string CodigoSistema { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}