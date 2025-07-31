using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class InventoryMovement : Entity
{
    public string SystemId { get; set; } = String.Empty;
    public DateTime? Data { get; set; }
    public int Quantity { get; set; }
}