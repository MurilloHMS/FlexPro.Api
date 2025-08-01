using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class Arquivo : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Extensions { get; set; }
    public long Size { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public bool IsPublic { get; set; } = false;
}