using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;
public class Products : Entity
{
    public string SystemCode { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int? MinimumStock { get; set; }
}