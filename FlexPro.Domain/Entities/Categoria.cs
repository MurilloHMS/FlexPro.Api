using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class Categoria : Entity
{
    public string Nome { get; set; } = string.Empty;
}