using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexPro.Domain.Entities;

[Table("emails_smtp")]
public class Email
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("email_address")]
    [MaxLength(50)]
    public string? Address { get; set; }
    [Column("is_enabled")]
    public bool IsEnabled { get; set; } = true;

}
