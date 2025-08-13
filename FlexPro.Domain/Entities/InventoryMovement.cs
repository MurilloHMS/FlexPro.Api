using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class InventoryMovement : Entity
{
    public int SystemId { get; set; }
    public DateTime? Data { get; set; }
    public int Quantity { get; set; }
    
    public virtual InventoryProducts? InventoryProduct { get; set; }
}