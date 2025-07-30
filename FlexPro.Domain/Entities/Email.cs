using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

[Table("emails_smtp")]
public class Email : Entity
{
    [Column("email_address")]
    [MaxLength(50)]
    public string? Address { get; set; }
    [Column("is_enabled")]
    public bool IsEnabled { get; set; } = true;

}
