using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class Equipamento : Entity
{
    public string Nome { get; set; } = string.Empty;
}