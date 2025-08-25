using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class InventoryProducts : Entity
{
    public string SystemCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? MinimumStock { get; set; }
    public virtual ICollection<InventoryMovement>? Movements { get; set; }
}