using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexPro.Domain.Entities;

[Table("inventory_movements")]
public class InventoryMovement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("system_code")]
    public string SystemId { get; set; } = String.Empty;
    [Column("date")]
    public DateTime? Data { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
}