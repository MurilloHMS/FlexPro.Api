using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class Equipamento : Entity
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}