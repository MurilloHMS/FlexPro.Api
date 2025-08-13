namespace FlexPro.Application.DTOs.Inventory;

public class InventoryMovementDto
{
    public int Id { get; set; }
    public int SystemId { get; set; }
    public DateTime? Data { get; set; }
    public int Quantity { get; set; }
}