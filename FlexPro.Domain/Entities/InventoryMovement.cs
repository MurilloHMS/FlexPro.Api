using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

[Table("inventory_movements")]
public class InventoryMovement : Entity
{
    
    [Column("system_code")]
    public string SystemId { get; set; } = String.Empty;
    [Column("date")]
    public DateTime? Data { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
}