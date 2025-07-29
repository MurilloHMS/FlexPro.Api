using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexPro.Domain.Entities;

[Table("archives")]
public class Arquivo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("file_name")] 
    [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;
    [Column("file_extensions")]
    public string? Extensions { get; set;}
    [Column("file_size")]
    public long Size { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [Column("is_active")]
    public bool IsActive { get; set; } = true;
    [Column("is_public")]
    public bool IsPublic { get; set; } = false;

}