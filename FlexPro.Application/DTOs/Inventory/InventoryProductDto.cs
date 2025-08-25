namespace FlexPro.Application.DTOs.Inventory;

public class InventoryProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? MinimumStock { get; set; }

    public List<InventoryMovementDto> Movements { get; set; } = new();
}